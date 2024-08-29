using ProjectLABPSD.Controllers;
using System;
using System.Web;

namespace ProjectLABPSD.Views
{
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["user"] != null || HttpContext.Current.Request.Cookies["user_cookie"] != null)
            {
                Response.Redirect("~/Views/HomePage.aspx");
            }
        }
        protected void ButtonLogin_Click(object sender, EventArgs e)
        {
            String userName = TextBoxName.Text;
            String userPassword = TextBoxPassword.Text;
            bool isChecked = CBRemember.Checked;

            if (AuthController.CheckNameAndPassword(userName, userPassword) != "")
            {
                ErrorMessageLabel.Text = AuthController.CheckNameAndPassword(userName, userPassword);
                ErrorMessageLabel.Visible = true;
                return;
            }

            Boolean user = AuthController.Login(userName, userPassword, isChecked);
            if (!user)
            {
                ErrorMessageLabel.Text = "Incorrect name or password. Please try again.";
                ErrorMessageLabel.CssClass = "errorMessage2";
                ErrorMessageLabel.Visible = true;
            }
            else
            {
                Response.Redirect("~/Views/HomePage.aspx");
            }
        }
    }
}