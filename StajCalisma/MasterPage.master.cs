using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterPage : System.Web.UI.MasterPage
{
    ServiceReference1.ServiceClient proxy; // Servis bağlantısı yapıyor.

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Convert.ToInt32(Session["RoleID"]) != 1)
        {
            users.Visible = false;
            product.Visible = false;
        }
    }

    protected void users_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("UserAddOrDelete.aspx");
    }

    protected void product_ServerClick(object sender, EventArgs e)
    {
        Response.Redirect("ProductControl.aspx");
    }

    protected void cksYap_ServerClick(object sender, EventArgs e)
    {
        proxy = new ServiceReference1.ServiceClient();
        proxy.WriteDebugLogInfo(DateTime.Now.ToString() + "  userid = " + Session["UserID"].ToString() + " , oturumunu sonlandırdı.");
        Session.Abandon();
        Response.Redirect("Login.aspx");
    }
}
