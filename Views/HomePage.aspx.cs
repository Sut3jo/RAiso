using ProjectLABPSD.Controllers;
using System;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Web.UI.WebControls;

namespace ProjectLABPSD.Views
{
    public partial class HomePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ButtonInsert.Visible = AuthController.UserIsAdmin();
                LoadStationeries();
            }
        }

        private void LoadStationeries()
        {
            GridView.DataSource = StationeryController.GetStationery();
            GridView.DataBind();
            foreach (GridViewRow row in GridView.Rows)
            {
                Button btnUpdate = (Button)row.FindControl("btnUp");
                Button btnDetail = (Button)row.FindControl("btnDet");
                Button btnDelete = (Button)row.FindControl("btnDel");
                if (AuthController.UserIsAdmin())
                {
                    btnUpdate.Visible = true;
                    btnDetail.Visible = true;
                    btnDelete.Visible = true;
                }
                else
                {
                    btnUpdate.Visible = false;
                    btnDelete.Visible = false;
                    btnDetail.Visible = true; ;
                }
            }
        }

        protected void GridView_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            string stationeryID = GridView.DataKeys[e.RowIndex]["StationeryID"].ToString();
            StationeryController.DeleteStationery(stationeryID);
            LoadStationeries();
        }

        protected void ButtonInsert_Click(object sender, EventArgs e)
        {
            Response.Redirect("InsertStationery.aspx");
        }
        protected void GridView_SelectedIndexChanging(object sender, GridViewSelectEventArgs e)
        {
            string stationeryID = GridView.DataKeys[e.NewSelectedIndex]["StationeryID"].ToString();
            Response.Redirect("StationeryDetailPage.aspx?id=" + stationeryID);
        }

        protected void GridView_RowEditing(object sender, GridViewEditEventArgs e)
        {
            string stationeryID = GridView.DataKeys[e.NewEditIndex]["StationeryID"].ToString();
            Response.Redirect($"UpdateStationery.aspx?id={stationeryID}");
        }
    }
}