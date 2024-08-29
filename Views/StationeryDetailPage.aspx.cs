using ProjectLABPSD.Controllers;
using ProjectLABPSD.Model;
using System;

namespace ProjectLABPSD.Views
{
    public partial class StationeryDetailPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string stationeryID = Request.QueryString["id"];
                if (!string.IsNullOrEmpty(stationeryID))
                {
                    LoadStationery(stationeryID);
                    CheckUser();
                }
            }
        }

        private void LoadStationery(string stationeryID)
        {
            MsStationery stationery = StationeryController.GetStationeryByID(int.Parse(stationeryID));
            if (stationery != null)
            {
                LblStatName.Text = stationery.StationeryName;
                LblStatPrice.Text = stationery.StationeryPrice.ToString();
            }
        }

        private void CheckUser()
        {
            if (AuthController.UserIsLoggedIn() && AuthController.UserIsCustomer())
            {
                cartCustomer.Visible = true;
            }
            else
            {
                cartCustomer.Visible = false;
            }
        }

        protected void ButtonAdd_Click(object sender, EventArgs e)
        {
            if (StationeryController.CheckQuantity(txtQuantity.Text) != "")
            {
                ErrorMessageLabel.Text = StationeryController.CheckQuantity(txtQuantity.Text);
                ErrorMessageLabel.Visible = true;
                return;
            }
            string loggedUser = AuthController.GetUserIDbyCookieOrSession();
            string stationeryID = Request.QueryString["id"];

            CartController.AddToCart(loggedUser, stationeryID, int.Parse(txtQuantity.Text));
            Response.Write("<script>window.alert('Added to cart');window.location = 'StationeryDetailPage.aspx?id=" + stationeryID + "'</script>");
        }
    }
}