using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;
//METHODLARIN İÇİNDE AMAÇLARI YORUM SATIRI OLARAK BELİRTİLMİŞTİR....
public partial class Hamper : System.Web.UI.Page
{
    ServiceReference1.ServiceClient proxy; //Servis bağlantısı yapıyor

    public static Encoding Users { get; private set; }
    public static DateTime dt;
    public static int line, count;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null) // Oturum hala açık mı diye kontrol ediyor.
        {
            Response.Redirect("Login.aspx");

        }
      

        proxy = new ServiceReference1.ServiceClient();

        if (!IsPostBack) //Sayfa ilk defa mı yüklendi yoksa yenilendi mi diye kontrol ediyor.
        {
            try
            {
                int uID = Convert.ToInt32(Session["UserID"]);
                var hmp = proxy.GetHampers(uID);
                //sepette ürün var mı/yok mu kontrolü
                if (hmp.Count() == 0) // ürün yok ise
                {
                    rptHampers.Visible = false;
                    Label2.Visible = true;
                    Label2.Text = "Sepetinizde ürün bulunmamaktadır";
                    btnConfirm.Visible = false;
                    lblTotalPrice.Visible = false;
                    Label3.Visible = false;
                }
                else
                {
                    rptHampers.DataSource = hmp.ToList();
                    rptHampers.DataBind();
                    lblTotalPrice.Text = hmp.Sum(x => x.tprice).ToString();
                }
            }
            catch (Exception)
            {
               
            }
        }
    }

    protected void rptHampers_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        proxy = new ServiceReference1.ServiceClient();

        if (e.CommandName == "UpdateQuantityHamper")
        {

            int uID = Convert.ToInt32(Session["UserID"]);
            int hID = Convert.ToInt32(e.CommandArgument); // Tıkladığımız satırdaki ürünün hamperid'sini çekiyor

            line = (e.Item.ItemIndex); //Tıkladığımız satırı çekiyoruz.
            count = Convert.ToInt32(((rptHampers.Items[line].FindControl("txtAdet")) as TextBox).Text); //Tıkladığımız satırdaki ürünün adet sayısını çekiyor
            int count2 = Convert.ToInt32(((rptHampers.Items[line].FindControl("txtAdetOrj")) as TextBox).Text); //Tıkladığımız satırdaki ürünün adet sayısını çekiyor
            if (count2 != count)
            {
                bool check = proxy.UpdateQuantityHampers(hID, count);
                var hmp = proxy.GetHampers(uID);

                rptHampers.DataSource = hmp.ToList();
                rptHampers.DataBind();

                int countNew = Convert.ToInt32(((rptHampers.Items[line].FindControl("txtAdet")) as TextBox).Text);  //  Yeni  adedi çekiyor
                                                                                                                    //Değişimi kıyaslayıp Bootstrap - Modal Yapısını kullanıyoruz.
                if (check == true)
                {

                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", $"alert('Seçtiğiniz ürünün adet sayısı {countNew} olarak güncellendi.')", true);
                    proxy.WriteDebugLogInfo(DateTime.Now.ToString() + "  userid = " + Session["UserID"].ToString() + " , hID =" + Convert.ToInt32(e.CommandArgument) + "  ürünün adedini  " + countNew + " güncelledi.");

                }
                else
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Ürünümüzden istediğiniz miktarda bulunmamaktadır. Adet sayısını tekrar giriniz.')", true);
                }
                lblTotalPrice.Text = hmp.Sum(x => x.tprice).ToString();
            }

        }
        if (e.CommandName == "DeleteHamper") //Silme işlemi
        {
            int uID = Convert.ToInt32(Session["UserID"]);
            int hID = Convert.ToInt32(e.CommandArgument); // Tıkladığımız satırdaki ürünün hamperid'sini çekiyor

            line = (e.Item.ItemIndex); //Tıkladığımız satırı çekiyoruz.
            count = Convert.ToInt32(((rptHampers.Items[line].FindControl("txtAdet")) as TextBox).Text); //Tıkladığımız satırdaki ürünün adet sayısını çekiyor

            bool check = proxy.DeleteHampers(hID, uID);  //Fonksiyon çalışıyor
            var hmp = proxy.GetHampers(uID);               //Fonksiyon çalışıyor
            rptHampers.DataSource = hmp.ToList();
            rptHampers.DataBind();

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Seçtiğiniz ürün sepetinizden çıkarılmıştır.')", true);
            proxy.WriteDebugLogInfo(DateTime.Now.ToString() + "  userid = " + Session["UserID"].ToString() + " , hID =" + Convert.ToInt32(e.CommandArgument) + "  ürünü sepetinden çıkardı.");

            lblTotalPrice.Text = hmp.Sum(x => x.tprice).ToString();
            // Yukarıdaki satırda: Modal Yapısının gerçekleşmesini sağlayan Script fonksiyonuna değişken yolluyoruz.
        }
    }



    protected void btnConfirm_Click(object sender, EventArgs e)

    {//SEPETTEKİ ÜRÜNLERİ SİPARİŞ EDER. SEPETTEKİ ÜRÜNÜN STATUSUNU 3 YAPAR(statu = 3 : Sipariş edildi.) Sipariş edilmiş ürünleri order tablosuna insert eder.
        try
        {
            proxy = new ServiceReference1.ServiceClient();

            dt = DateTime.Now; //HEM order HEM DE orderdetail VERİTABANINA AYNI ANDA İNSERT YAPMAMIZ İÇİN STATIC BİR dt değişkeni oluşturarak ikisine de bu tarihi yolluyoruz.
            int uID = Convert.ToInt32(Session["UserID"]);

            ServiceReference1.order objcust =
                new ServiceReference1.order()
                {
                    userid = Convert.ToInt32(Session["UserID"]),
                    date = dt,
                    price = Convert.ToDouble(lblTotalPrice.Text),
                };
            proxy.InsertOrder(objcust);

            proxy = new ServiceReference1.ServiceClient();

            ServiceReference1.orderdetail objcust2 =
                new ServiceReference1.orderdetail()
                {
                    userid = Convert.ToInt32(Session["UserID"]),
                    date = dt
                };
            proxy.InsertOrderDetail(objcust2);

            var hmp = proxy.GetHampers(uID);

            rptHampers.DataSource = hmp.ToList();
            rptHampers.DataBind();

            rptHampers.Visible = false;
            btnConfirm.Visible = false;
            lblTotalPrice.Visible = false;
            Label3.Visible = false;

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Siparişiniz kuryemize teslim edilmiştir. Hayırlı olsun dileklerimizle...')", true);

            // Yukarıdaki satırda: Modal Yapısının gerçekleşmesini sağlayan Script fonksiyonuna değişken yolluyoruz.
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }


}