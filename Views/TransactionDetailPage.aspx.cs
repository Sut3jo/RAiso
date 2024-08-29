using ProjectLABPSD.Controllers;
using System;
using System.Web;
using System.Web.UI.WebControls;

namespace ProjectLABPSD.Views
{
    public partial class TransactionDetailPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie isAdmin = HttpContext.Current.Request.Cookies["user_cookie"];
            if ((HttpContext.Current.Session["userRole"] != null && HttpContext.Current.Session["userRole"].ToString() != "Customer") || (isAdmin != null && isAdmin["userRole"] != "Customer"))
            {
                Response.Redirect("~/Views/HomePage.aspx");
            }
            else if (HttpContext.Current.Session["userRole"] == null && isAdmin == null)
            {
                Response.Redirect("~/Views/LoginPage.aspx");
            }

            string transactionID = Request.QueryString["id"];

            if (!string.IsNullOrEmpty(transactionID))
            {
                LoadTransactionDetails(int.Parse(transactionID));
            }
        }

        protected void LoadTransactionDetails(int id)
        {
            GridView.DataSource = TransactionController.GetTransactionDetails(id);
            GridView.DataBind();
        }
    }
}