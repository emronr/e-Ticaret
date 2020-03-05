using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
//METHODLARIN İÇİNDE AMAÇLARI YORUM SATIRI OLARAK BELİRTİLMİŞTİR....
public partial class Order : System.Web.UI.Page
{
    ServiceReference1.ServiceClient proxy; // Servis bağlantısı yapıyor.
    public static Encoding Users { get; private set; }

    public static int oID;
    public static int oID1 = 0, oID2 = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        proxy = new ServiceReference1.ServiceClient();

        if (!IsPostBack) //Sayfa ilk defa mı yüklendi yoksa yenilendi mi diye kontrol ediyor.
        {
            try
            {
                int uID = Convert.ToInt32(Session["UserID"]);
                var order = proxy.GetOrder(uID); // Geçmiş siparişlere bakıyor.

                if (order.Count() == 0) // Yoksa bilgilendirme mesajı veriyor.
                {
                    rptOrder.Visible = false;
                    Label2.Visible = true;
                    Label2.Text = "Sepetinizde ürün bulunmamaktadır";
                }
                else
                { // Varsa listeliyor.
                    rptOrder.DataSource = order.ToList();
                    rptOrder.DataBind();
                }
            }
            catch (Exception)
            {
                
            }
        }
    }
    protected void rptOrder_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        proxy = new ServiceReference1.ServiceClient();

        if (e.CommandName == "Detay")
        {  // DETAY BUTONUNA TIKLANDIĞINDA SİPARİŞ EDİLEN ÜRÜNLERİN DETAYLI HALİNİ GÖSTERİYOR.
            int uID = Convert.ToInt32(Session["UserID"]);

            oID = Convert.ToInt32(e.CommandArgument);

            rptOrderDetail.DataSource = proxy.GetOrderDetail(uID, oID);
            rptOrderDetail.DataBind();

            if (oID1 == 0 && oID2 == 0) { oID1 = oID; }
            else if (oID1 != 0 && oID2 == 0) { oID2 = oID; }
            if (oID1 != 0 && oID2 != 0 && oID1 != oID2) { oID1 = oID2; oID2 = 0; }


            if (rptOrderDetail.Visible == false)               // AÇILIP KAPANMASINI SAĞLIYOR
            {                                                  
                rptOrderDetail.Visible = true;                 
            }                                                  
            else if (rptOrderDetail.Visible == true && oID1 == oID2)           
            {                                                 
                rptOrderDetail.Visible = false;
                oID1 = 0;
                oID2 = 0;
            }
        }
    } 
}