using ProjectLABPSD.Controllers;
using System;
using System.Web;

namespace ProjectLABPSD.Views
{
    public partial class RegisterPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (HttpContext.Current.Session["user"] != null || HttpContext.Current.Request.Cookies["user_cookie"] != null)
            {
                Response.Redirect("~/Views/HomePage.aspx");
            }
        }
        protected void ButtonRegister_Click(object sender, EventArgs e)
        {
            String userName = TextBoxName.Text.Trim();
            String userPassword = TextBoxPassword.Text.Trim();
            String userGender = RadioButtonGender.SelectedValue;
            String userAddress = TextBoxAddress.Text.Trim();
            String userPhone = TextBoxPhone.Text.Trim();

            if (RegisterController.CheckRegister(userName, TextBoxDOB.Text, userGender, userAddress, userPassword, userPhone) != "")
            {
                ErrorMessageLabel.Text = RegisterController.CheckRegister(userName, TextBoxDOB.Text, userGender, userAddress, userPassword, userPhone);
                ErrorMessageLabel.Visible = true;
                return;
            }
            Response.Write("<script>window.alert('Successfully Registered'); window.location='RegisterPage.aspx'</script>");

        }
    }
}