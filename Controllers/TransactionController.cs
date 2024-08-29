using ProjectLABPSD.Handlers;
using ProjectLABPSD.Model;
using System;
using System.Collections.Generic;
namespace ProjectLABPSD.Controllers
{
    public class TransactionController
    {
        public static List<TransactionHeader> GetTransactions()
        {
            return TransactionsHandler.GetTransactions();
        }

        public static List<object> GetTransactionHistoryByUserID(int userID)
        {
            return TransactionsHandler.GetTransactionHistoryByUserID(userID);
        }

        public static List<object> GetTransactionDetails(int transactionID)
        {
            return TransactionsHandler.GetTransactionDetails(transactionID);
        }

        public static void AddTransactionDetail(int transactionID, int stationeryID, int quantity)
        {
            TransactionsHandler.AddTransactionDetail(transactionID, stationeryID, quantity);
        }

        public static int AddNewTransaction(int userID, DateTime transactionDate)
        {
            return TransactionsHandler.AddNewTransaction(userID, transactionDate);
        }

    }
}