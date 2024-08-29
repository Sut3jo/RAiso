using ProjectLABPSD.Controllers;
using ProjectLABPSD.Model;
using System;
using System.Web;

namespace ProjectLABPSD.Views
{
    public partial class UpdateStationery : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                HttpCookie isAdmin = HttpContext.Current.Request.Cookies["user_cookie"];
                if ((HttpContext.Current.Session["userRole"] != null && HttpContext.Current.Session["userRole"].ToString() != "Admin") || (isAdmin != null && isAdmin["userRole"] != "Admin"))
                {
                    Response.Redirect("~/Views/HomePage.aspx");
                } else if (HttpContext.Current.Session["userRole"] == null && isAdmin == null)
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
            MsStationery existingStationery = StationeryController.GetStationeryByID(int.Parse(stationeryID));
            if (existingStationery != null)
            {
                TextBoxName.Text = existingStationery.StationeryName;
                TextBoxPrice.Text = existingStationery.StationeryPrice.ToString();
            }
        }

        protected void ButtonUpdate_Click(object sender, EventArgs e)
        {
            string itemName = TextBoxName.Text;
            string Price = TextBoxPrice.Text;

            if (StationeryController.CheckUpdate(itemName, Price) != "")
            {
                ErrorMessageLabel.Text = StationeryController.CheckUpdate(itemName, Price);
                ErrorMessageLabel.Visible = true;
                return;
            }
            string stationeryID = Request.QueryString["id"];
            StationeryController.UpdateStationery(int.Parse(stationeryID), itemName, int.Parse(Price));

            Response.Write("<script>window.alert('Successfully Update Item'); window.location='HomePage.aspx'</script>");
        }
    }

}