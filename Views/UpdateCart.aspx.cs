using ProjectLABPSD.Controllers;
using ProjectLABPSD.Model;
using System;
using System.Web;

namespace ProjectLABPSD.Views
{
    public partial class UpdateCart : System.Web.UI.Page
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

                if (Request.QueryString["id"] != null)
                {
                    string stationeryID = Request.QueryString["id"];
                    LoadExistingData(stationeryID);
                }

            }
        }

        private void LoadExistingData(string stationeryID)
        {
            string userID = AuthController.GetUserIDbyCookieOrSession();

            MsStationery existingStationery = StationeryController.GetStationeryByID(int.Parse(stationeryID));
            Cart quantity = CartController.GetCartItemByID(int.Parse(userID), int.Parse(stationeryID));
            if (existingStationery != null)
            {
                txtQuantity.Text = quantity.quantity.ToString();
                LblStatName.Text = existingStationery.StationeryName;
                LblStatPrice.Text = existingStationery.StationeryPrice.ToString();
            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            if (StationeryController.CheckQuantity(txtQuantity.Text) != "")
            {
                ErrorMessageLabel.Text = StationeryController.CheckQuantity(txtQuantity.Text);
                ErrorMessageLabel.Visible = true;
                return;
            }
            string loggedUser = AuthController.GetUserIDbyCookieOrSession();

            string stationeryID = Request.QueryString["id"];

            CartController.UpdateCart(loggedUser, stationeryID, int.Parse(txtQuantity.Text));
            Response.Write("<script>window.alert('Added to cart');window.location = 'CartPage.aspx'</script>");
        }
    }
}