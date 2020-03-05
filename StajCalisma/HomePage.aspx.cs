using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
//METHODLARIN İÇİNDE AMAÇLARI YORUM SATIRI OLARAK BELİRTİLMİŞTİR....
public partial class HomePage : System.Web.UI.Page
{
    ServiceReference1.ServiceClient proxy; //Servis bağlantısı yapıyor

    public static Encoding Users { get; private set; }


    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["User"] == null) // Oturum hala açık mı diye kontrol ediyor.
        {
            Response.Redirect("Login.aspx");

        }
        proxy = new ServiceReference1.ServiceClient();

        if (!IsPostBack) //Sayfa ilk defa mı yüklendi yoksa yenilendi mi diye kontrol ediyor.
        {
            var result = proxy.GetProduct();
            rptProduct.DataSource = result.Where(x => x.stoch > 0).ToList();
            rptProduct.DataBind();
        }
       
    }

    protected void btnFilter_Click(object sender, EventArgs e)
    { // Kullanıcın  TextBox ile brand(marka) veya pname(ürün) üzerinde arama yaparak ürünlerin filtrelenmesini sağlar
        if (TextBox1.Text != "")
        {  //TextBox'ın içi boş ise filtre yapmadan bütün ürünleri listeler
            proxy = new ServiceReference1.ServiceClient();

            var result1 = proxy.GetProductFilter(TextBox1.Text.ToString());
            rptProduct.DataSource = result1.Where(x => x.stoch > 0).ToList();
            rptProduct.DataBind();
        }
        else
        {
            var result2 = proxy.GetProduct();
            rptProduct.DataSource = result2.Where(x => x.stoch > 0).ToList();
            rptProduct.DataBind();
        }
    }
    protected void rptProduct_ItemCommand(object source, RepeaterCommandEventArgs e)
    {
        if (e.CommandName == "AddHamper")  // Ekleme işlemi 
        {
            try
            {
                proxy = new ServiceReference1.ServiceClient();

                int line = (e.Item.ItemIndex); // Tıkladığımız satırı çekiyoruz.
                int count = Convert.ToInt32(((rptProduct.Items[line].FindControl("txtAdet")) as TextBox).Text); // Kullanıcın seçtiği adeti algılıyoruz.
                if (count < 1 || count == 0) { count = 1; } // Eğer kişi kendi isteğiyle ürün adedini 1'in altında seçmesi durumunda; seçilen ürün adedi otomatik olarak 1 algılanır.

                ServiceReference1.hampers objcust =
                    new ServiceReference1.hampers()
                    {
                        userid = Convert.ToInt32(Session["UserID"]),
                        productid = Convert.ToInt32(e.CommandArgument),
                        quantity = count,
                        date = DateTime.Now
                    };
                Boolean control = proxy.UpdateHampers(objcust);

                var result = proxy.GetProduct();
                rptProduct.DataSource = result.Where(x => x.stoch > 0).ToList();
                rptProduct.DataBind();
                //Pop-Up
                if (control == true)
                { // olumlu sonuç ürünün stoklarımızda mevcut olduğunu ve seçimin onaylandığını gösteriyor.
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", $"alert('Sepete {count} adet ürün eklendi.')", true);

                    proxy.WriteDebugLogInfo(DateTime.Now.ToString() + "  userid = " + Session["UserID"].ToString() + " , pid =" + Convert.ToInt32(e.CommandArgument) + "  üründen  " + count + "  adet sepete ekledi.");
                }
                else // stoklar yetersiz
                {
                    ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('İsteğinizi şuanda gerçekleştiremiyoruz.Lütfen seçtiğiniz ürünün adedini düşürünüz.')", true);

               
                }


            }
            catch (Exception)
            {
                Response.Write("Sepete Eklenemedi");
            }
        }
    }

  
}