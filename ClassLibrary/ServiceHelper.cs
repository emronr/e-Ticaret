using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace ClassLibrary
{               //METHODLARIN İÇİNDE AMAÇLARI YORUM SATIRI OLARAK BELİRTİLMİŞTİR....
    public class ServiceHelper
    {
        public static int orderid;

        /// Admin Kontrolü //Users
        public List<users> GetUsers()
        {
            using (StajEntities ent = new StajEntities())
            {
                return ent.users.ToList();
            }
        }
        public bool InsertUsers(users obj)
        {
            try
            {
                using (StajEntities ent = new StajEntities())
                {
                    ent.users.Add(obj);
                    ent.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool DeleteUsers(int uID)
        {
            try
            {
                using (StajEntities ent = new StajEntities())
                {
                    var item = ent.users.Where(x => x.userid == uID).FirstOrDefault();
                    ent.Entry(item).State = EntityState.Deleted;
                    ent.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        /// Admin Kontrolü

        //Products
        public List<product> GetProduct()
        {//ADMİN PANELİ İÇİN BÜTÜN ÜRÜNLER
            using (StajEntities ent = new StajEntities())
            {
                return ent.product.OrderBy(x => x.pid).ToList();
            }
        }
        public List<product> GetProductFilter(String query)
        {//KULLANICININ ÜRÜN ARAMASI İÇİN
            using (var ent = new StajEntities())
            {
                List<product> prd = ent.product.Where(x => x.pname.Contains(query) || x.brand.Contains(query)).OrderByDescending(x => x.pid).ToList();
                return prd.ToList();
            }
        }
        public bool InsertProduct(product obj)
        {//ADMİN PANELİ ÜRÜN EKLEME
            try
            {
                using (StajEntities ent = new StajEntities())
                {
                    ent.product.Add(obj);
                    ent.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool DeleteProduct(int pID)
        {//ADMİN PANELİ ÜRÜN SİLME
            try
            {
                using (StajEntities ent = new StajEntities())
                {
                    var item = ent.product.Where(x => x.pid == pID).FirstOrDefault();
                    ent.Entry(item).State = EntityState.Deleted;
                    ent.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        //Order
        public List<order> GetOrder(int uID)
        {//KULLANICININ GEÇMİŞ SİPARİŞLERİ
            using (StajEntities ent = new StajEntities())
            {
                List<order> ord = ent.order.Where(x => x.userid == uID).OrderByDescending(x => x.date).ToList();
                return ord.ToList();
            }
        }
        public bool InsertOrder(order obj)
        {//YENİ SİPARİŞ OLUŞTURULMASI - SİPARİŞ LİSTESİNE YENİ ÜRÜN EKLENMESİ
            try
            {
                using (StajEntities ent = new StajEntities())
                {
                    List<hampers> hmp = ent.hampers.Where(x => x.userid == obj.userid && x.status == 1).ToList();

                    obj.item = hmp.Count();
                    ent.order.Add(obj);
                    ent.SaveChanges();
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        //OrderDetaail
        public List<OrderDetailContract> GetOrderDetail(int uID, int oID)
        {//SİPARİŞTEKİ ÜRÜNLERİN DETAYLI HALİ
            using (StajEntities ent = new StajEntities())
            {
                var sql = $@"select               
                  				odt.odetailid,            
								odt.orderid,
								odt.userid,
								odt.pid,
								odt.price,
								odt.quantity,
								odt.date,
								p.pimage,
								p.pname,
								p.brand,
								p.comment
							
                               from deneme.orderdetail odt
                               JOIN deneme.product p ON (odt.pid = p.pid)
                               where odt.userid = {uID} and odt.orderid ={oID}
                               order by odt.odetailid";
                return ent.Database.SqlQuery<OrderDetailContract>(sql).ToList();
            }
        }
        public bool InsertOrderDetail(orderdetail obj)
        {//YENİ SİPARİŞLERİN DETAYLI HALİNİN EKLENMESİ
            try
            {
                using (StajEntities ent = new StajEntities())
                {
                    orderid = ent.order.Where(x => x.userid == obj.userid).Max(x => x.orderid);
                    int uID = Convert.ToInt32(obj.userid);

                    var hmpcopy = GetHampers(uID).ToList();
                    foreach (HamperContract hmpc in hmpcopy)
                    {
                        obj.orderid = orderid;
                        obj.userid = hmpc.userid;
                        obj.pid = hmpc.pid;
                        obj.price = hmpc.tprice;
                        obj.quantity = hmpc.quantity;
                        ent.orderdetail.Add(obj);
                        ent.SaveChanges();

                        WriteDebugLogInfo(DateTime.Now.ToString() + "  userid = " + uID.ToString() + " , pid =" + hmpc.pid.ToString() + "  üründen  " + hmpc.quantity + "  adet sipariş etti");
                    }


                    List<hampers> hmp = ent.hampers.Where(x => x.userid == obj.userid && x.status == 1).ToList();
                    if (hmp != null)
                    {
                        foreach (hampers hmpcpy in hmp)
                        {
                            hmpcpy.status = 3;
                            ent.SaveChanges();
                        }
                    }

                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        //Hamper
        public List<HamperContract> GetHampers(int uID)
        {//KİŞİNİN SEPETİNDEKİ ÜRÜNLERİN LİSTELENMESİ. 
            //Burada veritabanındaki iki farklı tabloyu birleştirerek kendi oluşturduğumuz HamperContract clasımıza veri çekmekteyiz.
            using (StajEntities ent = new StajEntities())
            {
                var sql = $@"select 
                                   h.userid,
                                   h.hamperid,
                                   p.pid,
                                   p.pimage,
                                   p.pname,
                                   p.brand,
                                   p.comment,
                                   p.price,
                                   h.quantity,
                                   h.date,
                                   p.price*h.quantity as tprice
                               from deneme.hampers h
                               JOIN deneme.product p ON (h.productid = p.pid)
                               where h.status = 1 and  h.userid = {uID}
                               order by h.date desc";

                return ent.Database.SqlQuery<HamperContract>(sql).ToList();
            }
        }
        public bool UpdateHampers(hampers obj)
        {//SEPETTEKİ ÜRÜNÜN ADEDİ İLE TARİHİNİN GÜNCELLENMESİ ------ ÜRÜN YOK İSE EKLEMESİ.
        try
            {
                using (StajEntities ent = new StajEntities())
                {
                    var prd = ent.product.Where(x => x.pid == obj.productid).FirstOrDefault();

                    if (prd.stoch >= obj.quantity) // Seçilen ürün adedinin stoklarımızda mevcut olup olmadığını kontrol ediyor
                    {
                        var hmp = ent.hampers.Where(x => x.userid == obj.userid && x.productid == obj.productid && x.status == 1).FirstOrDefault(); // bu üründen sepetimizde olup olmadığına bakıyor.

                        if (hmp != null) // ürün sepetimizde var ise adet ve tarihini güncelliyor.
                        {
                            hmp.quantity += obj.quantity;
                            hmp.date = DateTime.Now;
                            ent.Entry(hmp).State = EntityState.Modified;
                            ent.SaveChanges();
                        }
                        else // yok ise ürünü oluşturuyor.
                        {
                            obj.status = 1;
                            ent.hampers.Add(obj);
                            ent.SaveChanges();
                        }
                        prd.stoch -= obj.quantity; // seçilen ürün kadar stoktan düşüyor.
                        ent.SaveChanges();
                        return true;
                    }
                    else return false;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool UpdateQuantityHampers(int hID, int count)
        {
            try
            {
                using(StajEntities ent = new StajEntities())
                {
                    var item = ent.hampers.Where(x => x.hamperid == hID).FirstOrDefault();
                    var prd = ent.product.Where(x => x.pid == item.productid).FirstOrDefault();

                    int hmpQuantity = Convert.ToInt32(item.quantity);

                    if (hmpQuantity != count)
                    {
                        if (item.quantity < count)
                        {
                            if (prd.stoch >= (count - hmpQuantity))
                            {
                                prd.stoch -= count - hmpQuantity;
                                item.quantity = count;
                                ent.SaveChanges();
                            }
                            else
                            {
                                return false;
                            }
                        }
                        else if (hmpQuantity > count)
                        {
                            prd.stoch +=hmpQuantity - count;
                            item.quantity = count;
                            ent.SaveChanges();
                        }
                    }
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool DeleteHampers(int hID,int uID)
        {//SEPETTEKİ ÜRÜNÜN KALDIRILMASI
            try
            {
                using (StajEntities ent = new StajEntities())
                {
                    var item = ent.hampers.Where(x => x.hamperid == hID && x.userid == uID).FirstOrDefault();
                    var prd = ent.product.Where(x => x.pid == item.productid).FirstOrDefault();
                    prd.stoch += item.quantity;
                    item.date = DateTime.Now;
                    item.status = 2;
                    ent.SaveChanges();
                    
                }
                return true;
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public bool WriteDebugLogError(string error)
        {
            FileStream file = new FileStream("C:\\Logs\\JIRA\\WebLogs\\Errors\\Log.txt", FileMode.Append);
            StreamWriter stream = new StreamWriter(file);
            stream.WriteLine(error);
            stream.Flush();
            stream.Close();
            return true;
        }
        public bool WriteDebugLogInfo(string info)
        {
            FileStream file = new FileStream("C:\\Logs\\JIRA\\WebLogs\\Info\\Log.txt", FileMode.Append);
            StreamWriter stream = new StreamWriter(file);
            stream.WriteLine(info);
            stream.Flush();
            stream.Close();
            return true;
        }
        //LoginControls
        public users Login(string x, string y)
        {//GİRİŞ KONTROLÜ
            using (StajEntities ent = new StajEntities())
            {
                string text = MD5(y);
                var user = ent.users.Where(m => m.mail == x && m.password == text).FirstOrDefault();
                return user;
            }
        }

        //Şifreleme
        public string MD5(string Text)
        { //ŞİFRELEME ALGORİTMASI
            //AMAÇ: Kullanıcı şifresinin veritabanına doğrudan işlenmesini değil, Şifreli bir şekilde tutulmasını sağlar.
            MD5CryptoServiceProvider MB5Code = new MD5CryptoServiceProvider();
            byte[] byteSeries = Encoding.UTF8.GetBytes(Text);
            byteSeries = MB5Code.ComputeHash(byteSeries);
            StringBuilder sb = new StringBuilder();
            foreach (byte ba in byteSeries)
            {
                sb.Append(ba.ToString("x2").ToLower());
            }
            return sb.ToString();
        }
    }
}
