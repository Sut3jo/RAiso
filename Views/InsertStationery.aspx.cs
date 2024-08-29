using ProjectLABPSD.Controllers;
using System;
using System.Web;

namespace ProjectLABPSD.Views
{
    public partial class InsertStationery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie isAdmin = HttpContext.Current.Request.Cookies["user_cookie"];
                if ((HttpContext.Current.Session["userRole"] != null && HttpContext.Current.Session["userRole"].ToString() != "Admin")|| (isAdmin != null && isAdmin["userRole"] != "Admin"))
                {
                    Response.Redirect("~/Views/HomePage.aspx");
                }
                else if (HttpContext.Current.Session["userRole"] == null && isAdmin == null)
                {
                    Response.Redirect("~/Views/LoginPage.aspx");
                }
            }
        }

        protected void ButtonInsert_Click(object sender, EventArgs e)
        {
            string itemName = TextBoxName.Text;
            string Price = TextBoxPrice.Text;


            if (StationeryController.CheckInsert(itemName, Price) != "")
            {
                ErrorMessageLabel.Text = StationeryController.CheckInsert(itemName, Price);
                ErrorMessageLabel.Visible = true;
                return;
            }

            Response.Write("<script>window.alert('Successfully Insert Item'); window.location='InsertStationery.aspx'</script>");
        }
    }
}