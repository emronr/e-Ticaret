using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.Entity;
                                                //METHODLARIN İÇİNDE AMAÇLARI YORUM SATIRI OLARAK BELİRTİLMİŞTİR....
public partial class UserAddOrDelete : System.Web.UI.Page
{
    public static string name, surName, roleName;   //Static değişkenler oluşturuldu.
    public static int x;                            //Static değişkenler oluşturuldu.
    ServiceReference1.ServiceClient proxy;  //Servis bağlantısı yapıyor

    public static Encoding Users { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["RoleID"]) != 1)
        {
            Response.Redirect("HomePage.aspx");

        }
        if (Session["User"] == null) // Oturum hala açık mı diye kontrol ediyor.
        {
            Response.Redirect("Login.aspx");

        }
        if (!IsPostBack)     //Sayfa ilk defa mı yüklendi yoksa yenilendi mi diye kontrol ediyor.
        { // MEVCUT KULLANICILARIN DataGridView İÇİNDE LİSTELENMESİNİ SAĞLIYOR.
            proxy = new ServiceReference1.ServiceClient();
            GridView1.DataSource = proxy.GetUsers();
            GridView1.DataBind();
        }
    }

    protected void GridView1_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {  //VAR OLAN KULLANICININ SİSTEMDEN KAYDINI SİLER
        int uID = Convert.ToInt32(GridView1.DataKeys // Tıklanılan satırdaki userid'yi çeker.
        [e.RowIndex].Values["userid"].ToString());
        proxy = new ServiceReference1.ServiceClient();

        bool check = proxy.DeleteUsers(uID);
        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "alertMessage", "alert('Kullanıcı kaydı başarılı bir şekilde silinmiştir.')", true);
        GridView1.DataSource = proxy.GetUsers();
        GridView1.DataBind();
    }

    protected void btnUserAdd_Click(object sender, EventArgs e)
    { // SİSTEME YENİ KULLANICI EKLER.
        if (RadioButton1.Checked) x = 2;
        if (RadioButton2.Checked) x = 3;

        try
        {
            proxy = new ServiceReference1.ServiceClient();
            ServiceReference1.users objcust =
            new ServiceReference1.users()
            {
                name = TextBox1.Text,
                surname = TextBox2.Text,
                mail = TextBox3.Text,
                phone = TextBox4.Text,
                roleid = x,
                password = proxy.MD5(TextBox5.Text.ToString())

            };
            proxy.InsertUsers(objcust);   // yeni kullanıcı ekliyor.
            ClearTextBoxes(this);//Sayfa içindeki TextBoxları temizliyor
            GridView1.DataSource = proxy.GetUsers();
            GridView1.DataBind();
            Label1.Text = "Yeni kullanıcı oluşturuldu ";
        }
        catch (Exception)
        {
            Label1.Text = "Servis çalışmamız mevcuttur. Lütfen daha sonra tekrar deneyiniz.";
            throw;
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
   

    
}