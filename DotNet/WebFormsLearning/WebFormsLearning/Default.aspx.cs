using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebFormsLearning
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //String currurl = HttpContext.Current.Request.RawUrl;
            //String querystring = null;

            //int iqs = currurl.IndexOf('?');
            //if (iqs >= 0)
            //{
            //    querystring = (iqs < currurl.Length - 1) ? currurl.Substring(iqs + 1) : String.Empty;
            //    var queryStr = HttpUtility.ParseQueryString(querystring);
            //    var page = queryStr["action"];
            //    Response.Redirect("~/About.aspx", true);
            //}
        }
    }
}