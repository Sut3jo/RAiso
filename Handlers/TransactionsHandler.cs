using ProjectLABPSD.Model;
using ProjectLABPSD.Repository;
using ProjectLABPSD.Views.Repository;
using System;
using System.Collections.Generic;

namespace ProjectLABPSD.Handlers
{
    public class TransactionsHandler
    {
        public static List<TransactionHeader> GetTransactions()
        {
            return TransactionHeaderRepository.GetTransactions();
        }
        public static List<object> GetTransactionHistoryByUserID(int userID)
        {
            return TransactionHeaderRepository.GetTransactionHistoryByUserID(userID);
        }

        public static int AddNewTransaction(int userID, DateTime transactionDate)
        {
            return TransactionHeaderRepository.AddNewTransaction(userID, transactionDate);
        }

        public static void AddTransactionDetail(int transactionID, int stationeryID, int quantity)
        {
            TransactionDetailRepository.AddNewTransactionDetail(transactionID, stationeryID, quantity);
        }

        public static List<object> GetTransactionDetails(int transactionID)
        {
            return TransactionDetailRepository.GetTransactionDetails(transactionID);
        }
    }
}