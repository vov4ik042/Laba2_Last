using System.Collections.Generic;

namespace Laba2Model
{
    internal class Structers
    {
        public Structers() { }
        public struct CashPayment
        {
            public List<Products> ProductsListClient { get; set; }
            public double SumToPay { get; set; }
            public double SumClientSet { get; set; }
            public double ChangeToClient { get; set; }
            public bool LoyaltyCard { get; set; }//discount 5% or 8% from SumToPay
            public bool Delivery { get; set; }//if true +250 or +50 to SumToPay
        }
        public struct CardPayment
        {
            public List<Products> ProductsListClient { get; set; }
            public double SumToPay { get; set; }
            public bool LoyaltyCard { get; set; }//discount 5% or 8% from SumToPay
            public bool Delivery { get; set; }//if true +250 or +50 to SumToPay
        }
        public struct CashPaymentCourier
        {
            public List<Products> ProductsListClient { get; set; }
            public double SumToPay { get; set; }
            public double SumHasClient { get; set; }
            public double ChangeToClient { get; set; }
            public int Delivery { get; set; }//+250 or +0 to SumToPay
        }

        public CashPayment CreateStruct(List<Products> values, double SumToPay, double SumClientSet, double ChangeToClient, bool LoyaltyCard, bool Delivery)
        {
            CashPayment cashPayment = new CashPayment();
            cashPayment.ProductsListClient = values;
            cashPayment.SumToPay = SumToPay;
            cashPayment.SumClientSet = SumClientSet;
            cashPayment.ChangeToClient = ChangeToClient;
            cashPayment.LoyaltyCard = LoyaltyCard;
            cashPayment.Delivery = Delivery;
            return cashPayment;
        }
        public CardPayment CreateStruct(List<Products> values, double SumToPay, bool LoyaltyCard, bool Delivery)
        {
            CardPayment cardPayment = new CardPayment();
            cardPayment.ProductsListClient = values;
            cardPayment.SumToPay = SumToPay;
            cardPayment.LoyaltyCard = LoyaltyCard;
            cardPayment.Delivery = Delivery;
            return cardPayment;
        }
        public CashPaymentCourier CreateStruct(List<Products> values, double SumToPay, double SumHasClient, double ChangeToClient, int Delivery)
        {
            CashPaymentCourier cashPaymentCourier = new CashPaymentCourier();
            cashPaymentCourier.ProductsListClient = values;
            cashPaymentCourier.SumToPay = SumToPay;
            cashPaymentCourier.SumHasClient = SumHasClient;
            cashPaymentCourier.ChangeToClient = ChangeToClient;
            cashPaymentCourier.Delivery = Delivery;
            return cashPaymentCourier;
        }
    }
}
