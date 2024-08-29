using ProjectLABPSD.Controllers;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Web;
using System.Web.UI.WebControls;

namespace ProjectLABPSD.Views
{
    public partial class TransactionHistoryPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
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

                string userID = AuthController.GetUserIDbyCookieOrSession();

                ShowTransactionHistory(int.Parse(userID));
            }
        }

        protected void ShowTransactionHistory(int userID)
        {
            List<object> t = TransactionController.GetTransactionHistoryByUserID(userID);
            if(t.Count == 0)
            {
                LabelEmpty.Text = "No Transaction Yet";
                LabelEmpty.Visible = true;
                return;
            }
            GridView.DataSource = t;
            GridView.DataBind();
        }

        protected void GridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            string transactionID = GridView.DataKeys[e.NewSelectedIndex]["transactionID"].ToString();

            Response.Redirect("TransactionDetailPage.aspx?id=" + transactionID);
        }
    }
}