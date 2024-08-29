using ProjectLABPSD.Controllers;
using System;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace ProjectLABPSD.Views
{
    public partial class CartPage : System.Web.UI.Page
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

                LoadStationeriesCart();
            }
        }

        private void LoadStationeriesCart()
        {
            string userID = AuthController.GetUserIDbyCookieOrSession();

            if (CartController.GetCartList(Convert.ToInt32(userID)).Count() == 0)
            {
                LabelEmpty.Text = "Cart is empty";
                LabelEmpty.Visible = true;
                btnCek.Visible = false;
                GridView.Visible = false;
                return;
            }
            GridView.DataSource = CartController.GetCartList(Convert.ToInt32(userID));
            GridView.DataBind();
        }

        protected void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string stationeryID = GridView.DataKeys[e.RowIndex]["StationeryID"].ToString();
            string userID = AuthController.GetUserIDbyCookieOrSession();

            CartController.DeleteFromCart(Convert.ToInt32(userID), stationeryID);
            LoadStationeriesCart();
        }

        protected void BtnCek_Click(object sender, EventArgs e)
        {
            string userID = AuthController.GetUserIDbyCookieOrSession();
            DateTime transactionDate = DateTime.Now;
            int transactionID = TransactionController.AddNewTransaction(int.Parse(userID), transactionDate);

            foreach (GridViewRow row in GridView.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    string stationeryID = GridView.DataKeys[row.RowIndex]["StationeryID"].ToString();
                    int quantity = Convert.ToInt32(row.Cells[2].Text);
                    TransactionController.AddTransactionDetail(transactionID, int.Parse(stationeryID), quantity);
                }
            }
            CartController.DeleteAllItem(int.Parse(userID));
            Response.Write("<script>window.alert('Checkout Success'); window.location='HomePage.aspx'</script>");
        }

        protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string stationeryID = GridView.DataKeys[e.NewEditIndex]["StationeryID"].ToString();
            Response.Redirect($"UpdateCart.aspx?id={stationeryID}");
        }
    }
}