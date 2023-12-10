using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Laba2Model
{
    internal class Transactions
    {
        public delegate void SaveDataStorage();
        public event SaveDataStorage SaveInfoInStorage;
        private List<Transactions> Storage = new List<Transactions>();
        private Guid TransactionID { get; set; }
        private Guid CasaID { get; set; }
        private DateTime TransactionDataTime { get; set; }
        private double TotalSum  { get; set; }
        private string OnlineOfline { get; set; }
        private string NameOfCasa { get; set; }
        private string CashCard { get; set; }
        private bool LoyaltyCard { get; set; }
        private bool Delivery { get; set; }
        public Transactions()
        {
        
        }

        private void WriteDataInStorage()
        {

        }

        private Transactions(Guid CasaID, Guid TransactionID, string nameOfCasa, DateTime dateTime, double sum, string status, string typePaymant, bool loyaltyCard, bool delivery)
        {
            this.TransactionID = TransactionID;
            this.CasaID = CasaID;
            NameOfCasa = nameOfCasa;
            TransactionDataTime = dateTime;
            TotalSum = sum;
            OnlineOfline = status;
            CashCard = typePaymant;
            LoyaltyCard = loyaltyCard;
            Delivery = delivery;
        }

        public void AddTransaction(Guid CasaID, Guid TransactionID, string nameOfCasa, DateTime dateTime, double sum, string status, string typePaymant, bool loyaltyCard, bool delivery)
        {
            Storage.Add(new Transactions(CasaID, TransactionID, nameOfCasa, dateTime, sum, status, typePaymant, loyaltyCard, delivery));
        }

        public void WriteTransactionsIntoFile()
        {
            var FilePath = "Transactions.txt";
            using (StreamWriter writer = new StreamWriter(FilePath, true))
            {
                foreach (var item in Storage)
                {
                    writer.WriteLine($"Transaction ID: {item.TransactionID}");
                    writer.WriteLine($"Casa ID: {item.CasaID}");
                    writer.WriteLine($"Name of casa: {item.NameOfCasa}");
                    writer.WriteLine($"Data and time: {item.TransactionDataTime}");
                    writer.WriteLine($"Total sum: {item.TotalSum}");
                    writer.WriteLine($"Online or offline: {item.OnlineOfline}");
                    writer.WriteLine($"Cash or card: {item.CashCard}");
                    writer.WriteLine($"Loyalty card: {item.LoyaltyCard}");
                    writer.WriteLine($"Delivery: {item.Delivery}");
                    writer.WriteLine($" ");
                }
            }
        }
    }
}
