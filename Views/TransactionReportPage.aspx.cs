using CrystalDecisions.CrystalReports.Engine;
using ProjectLABPSD.Controllers;
using ProjectLABPSD.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Web;
using DataSet = ProjectLABPSD.Dataset.DataSet;

namespace ProjectLABPSD.Views
{
    public partial class TransactionReportPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie isAdmin = HttpContext.Current.Request.Cookies["user_cookie"];
            if ((HttpContext.Current.Session["userRole"] != null && HttpContext.Current.Session["userRole"].ToString() != "Admin") || (isAdmin != null && isAdmin["userRole"] != "Admin"))
            {
                Response.Redirect("~/Views/HomePage.aspx");
            }
            else if (HttpContext.Current.Session["userRole"] == null && isAdmin == null)
            {
                Response.Redirect("~/Views/LoginPage.aspx");
            }

            ReportDocument report = new ReportDocument();
            DataSet data = getData(TransactionController.GetTransactions());

            if (data.Tables.Count > 0)
            {
                foreach (DataRow headerRow in data.Tables["TransactionHeader"].Rows)
                {
                    decimal grandTotal = 0;
                    foreach (DataRow detailRow in headerRow.GetChildRows("TransactionHeader_TransactionDetail"))
                    {
                        grandTotal += Convert.ToDecimal(detailRow["subtotal"]);
                    }
                    headerRow["grandtotal"] = grandTotal;
                }

                report.Load(Server.MapPath("~/Report/Report.rpt"));
                report.SetDataSource(data);
                CrystalReportViewer1.ReportSource = report;
            }
            else
            {
                ErrorMessage.Visible = true;
            }
        }

        private DataSet getData(List<TransactionHeader> transactionHeaders)
        {
            DataSet dataSet = new DataSet();
            var headerTable = dataSet.TransactionHeader;
            var detailTable = dataSet.TransactionDetail;

            foreach (TransactionHeader t in transactionHeaders)
            {
                var hrow = headerTable.NewRow();
                hrow["transactionID"] = t.transactionID;
                hrow["userID"] = t.userID;
                hrow["transactionDate"] = t.transactionDate;
                headerTable.Rows.Add(hrow);

                foreach (TransactionDetail d in t.TransactionDetails)
                {
                    var drow = detailTable.NewRow();
                    drow["transactionID"] = d.transactionID;
                    drow["stationeryID"] = d.stationeryID;
                    drow["quantity"] = d.quantity;
                    drow["subtotal"] = d.quantity * StationeryController.GetStationeryPrice(d.stationeryID);
                    detailTable.Rows.Add(drow);
                }
            }

            return dataSet;
        }
    }
}