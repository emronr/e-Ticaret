using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
                                //METHODLARIN İÇİNDE AMAÇLARI YORUM SATIRI OLARAK BELİRTİLMİŞTİR....
public partial class ProductControl : System.Web.UI.Page
{
    ServiceReference1.ServiceClient proxy;  //  Servise bağlanmasını sağlıyor.

    public static Encoding Users { get; private set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Convert.ToInt32(Session["RoleID"]) != 1)
        {
            Response.Redirect("HomePage.aspx");

        }
        if (Session["User"] == null) // Oturum hala açık mı diye kontrol ediyor.
        {
            Response.Redirect("Login.aspx");

        }
        proxy = new ServiceReference1.ServiceClient();
        if (!IsPostBack) // Sayfa ilk defa mı yüklendi yoksa yenilendi mi diye kontrol ediyor.
        {
            rptView.DataSource = proxy.GetProduct();
            rptView.DataBind();
        }
    }



    protected void btnUrunEkle_Click(object sender, EventArgs e)
    {   // YENİ ÜRÜN KAYDI YAPILIYOR
        if (UploadTest.HasFile == false) //  GÖRSEL SEÇİLMEDİ İSE HATA VERİYOR.
        {    
            // No file uploaded!
            Label1.Text = "Ürün için uygun bir görsel giriniz.";
        }
        else
        {
            try
            {
                proxy = new ServiceReference1.ServiceClient();
                ServiceReference1.product objcust =               //EKLENECEK ÜRÜNÜ TANIMLIYOR
                new ServiceReference1.product()                   //EKLENECEK ÜRÜNÜ TANIMLIYOR
                {                                                 //EKLENECEK ÜRÜNÜ TANIMLIYOR
                    pname = TextBox1.Text,                        //EKLENECEK ÜRÜNÜ TANIMLIYOR
                    brand = TextBox2.Text,                        //EKLENECEK ÜRÜNÜ TANIMLIYOR
                    comment = TextBox3.Text,                      //EKLENECEK ÜRÜNÜ TANIMLIYOR
                    price = Convert.ToDouble(TextBox4.Text),      //EKLENECEK ÜRÜNÜ TANIMLIYOR
                    pimage = UploadTest.FileName,                 //EKLENECEK ÜRÜNÜ TANIMLIYOR
                    stoch = Convert.ToInt32(TextBox6.Text)        //EKLENECEK ÜRÜNÜ TANIMLIYOR
                };
                proxy.InsertProduct(objcust);                     // ÜRÜNÜ EKLİYOR

                ClearTextBoxes(this);// Sayfa içindeki TextBoxları temizliyor.
                rptView.DataSource = proxy.GetProduct();
                rptView.DataBind();
                Label1.Text = "Ürün depoya eklenmiştir.";
            }
            catch (Exception)
            {
                Label1.Text = "Ürün kaydı oluşturulamamıştır. Lütfen girmiş olduğunuz bilgileri kontrol ediniz.";
                //throw;
            }
        }
    }

    protected void rptView_ItemCommand(object source, RepeaterCommandEventArgs e)
    {   // ÜRÜNÜ DEPODAN KALDIRIR
        if (e.CommandName == "Delete")
        {
            int pID = Convert.ToInt32(e.CommandArgument); // Tıklanılan satırdaki ürünün productid'sini çeker.

            proxy = new ServiceReference1.ServiceClient();

            bool check = proxy.DeleteProduct(pID);
            rptView.DataSource = proxy.GetProduct();
            rptView.DataBind();
        }
    }


    public void ClearTextBoxes(Control parent)
    {//TextBoxları temizleme işlemi
        foreach (Control x in parent.Controls)
        {
            if ((x.GetType() == typeof(TextBox)))
            {
                ((TextBox)(x)).Text = "";
            }
            if (x.HasControls())
            {
                ClearTextBoxes(x);
            }
        }
    }
    protected void btnFilter_Click(object sender, EventArgs e)
    { // Kullanıcın  TextBox ile brand(marka) veya pname(ürün) üzerinde arama yaparak ürünlerin filtrelenmesini sağlar
        if (TextBox5.Text != "")
        { //TextBox'ın içi boş ise filtre yapmadan bütün ürünleri listeler
            proxy = new ServiceReference1.ServiceClient();

            rptView.DataSource = proxy.GetProductFilter(TextBox5.Text.ToString());
            rptView.DataBind();
        }
        else
        {
            rptView.DataSource = proxy.GetProduct();
            rptView.DataBind();
        }
    }
}