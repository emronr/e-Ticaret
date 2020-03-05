using ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
                                //METHODLARIN İÇİNDE AMAÇLARI YORUM SATIRI OLARAK BELİRTİLMİŞTİR....
public partial class Login : System.Web.UI.Page
{
    ServiceReference1.ServiceClient proxy; // Servis bağlantısı yapıyor.
    public static Encoding Users { get; private set; }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.UrlReferrer != null)
        {
            if (!IsPostBack)
            {
                Session["referer"] = Request.UrlReferrer.AbsoluteUri;
                
               // proxy.WriteDebugLogInfo(DateTime.Now.ToString() + "  userid = " + Session["UserID"].ToString() + " , oturumu zaman aşımına uğradı.");

            }
        }
    }

     
    protected void btnLogin_Click(object sender, EventArgs e)
    { // KULLANICI KAYDINI SORGULUYOR
        try
        {
            proxy = new ServiceReference1.ServiceClient();
            users currentuser = proxy.Login(TextBox1.Text, TextBox2.Text);

            if (currentuser == null) // KULLANICI YOKSA UYARI VERİYOR.
            {
                lblResult.Text = "Kullanıcı adı veya şifre hatalı.Lütfen tekrar deneyiniz.";
                ScriptManager.RegisterClientScriptBlock(this.Page, this.GetType(), "MyopenModal", "openModal();", true);


                proxy.WriteDebugLogError(DateTime.Now.ToString() +"     "+ TextBox1.Text + "    mail adresi ile hatalı giriş yapılmıştır");
            }
            else if (currentuser.roleid == 1 || currentuser.roleid == 2 || currentuser.roleid == 3) //  KAYIT OLURKEN roleid GİRİLMİŞ Mİ DİYE KONTROL EDİYOR.
            {
                Session["UserID"] = currentuser.userid;
                Session["User"] = currentuser.name.ToString() + "  " + currentuser.surname.ToString();

                if (currentuser.roleid == 1)
                {
                    Session["Role"] = "Admin";
                    Session["RoleID"] = 1;
                }
                else if (currentuser.roleid == 2)
                {
                    Session["Role"] = "Müşteri";
                    Session["RoleID"] = 2;
                }
                else
                {
                    Session["Role"] = "Satıcı";
                    Session["RoleID"] = 3;
                }
                proxy.WriteDebugLogInfo(DateTime.Now.ToString() + "  userid = " + Session["UserID"].ToString() + " , oturum açtı.");
                if (Session["referer"] != null)
                {
                    Response.Redirect(Session["referer"].ToString());
                }
                Response.Redirect("HomePage.aspx");
            }
            else
            {
                Session["UserID"] = currentuser.userid;
                proxy.WriteDebugLogError(DateTime.Now.ToString() + "  userid = " + Session["UserID"].ToString() + " , rol atanmamıştır.");
            }
        }
        catch (Exception)
        {
           // throw ex; 
        }
    }
}