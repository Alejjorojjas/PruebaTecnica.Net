using System;

namespace GrupoDigitalBank.Web
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("~/Usuario.aspx", endResponse: true);
        }
    }
}
