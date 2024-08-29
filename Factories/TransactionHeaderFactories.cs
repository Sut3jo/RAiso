using ProjectLABPSD.Model;
using System;

namespace ProjectLABPSD.Factories
{
    public class TransactionHeaderFactories
    {
        public static TransactionHeader NewTransaction(int userID, DateTime transactionDate)
        {
            TransactionHeader t = new TransactionHeader();
            t.userID = userID;
            t.transactionDate = transactionDate;
            return t;
        }
    }
}