using Laba2EnableOrDisableModel;
using Laba2Model;
using System;
using System.Windows.Forms;

namespace Laba2View
{
    public partial class LastWindowPay : Form
    {
        private byte TypeOfOperation { get; set; }
        private double AverageAmountThings { get; set; }
        private bool DeliveryWasUsed { get; set; }
        private bool LoyalCardWasUsed { get; set; }
        private double TotalSum { get; set; }
        private string DeliveryAddress { get; set; }
        private static Guid CasaID { get; set; }
        private static string NameOfCasa { get; set; }
        public LastWindowPay()
        {
            InitializeComponent();
        }

        public LastWindowPay(Guid id, string name)
        {
            CasaID = id;
            NameOfCasa = name;
        }
        public void GettingInformation(byte WhatTypeOperation, double TotalSum, double AverageAmountThings, bool DeliveryWasUsed, bool LoyalCardWasUsed)
        {
            TypeOfOperation = WhatTypeOperation;
            this.AverageAmountThings = AverageAmountThings;
            this.DeliveryWasUsed = DeliveryWasUsed;
            this.TotalSum = TotalSum;
            this.LoyalCardWasUsed = LoyalCardWasUsed;
            switch (WhatTypeOperation)
            {
                case 1:
                    {
                        EnableOrDisable.EnableAndVisible(true, true, label3, label5, textBox2, button1, label11, label12, label1, label2, label4);
                        label2.Text = $"{TotalSum.ToString()} UAH";
                        if (DeliveryWasUsed == true) EnableOrDisable.EnableAndVisible(true, true, label6, textBox1);
                        if (DeliveryWasUsed == false)
                        {
                            label5.Location = new System.Drawing.Point(16, 160);
                            textBox2.Location = new System.Drawing.Point(282, 160);
                            button1.Location = new System.Drawing.Point(120, 208);
                        }
                        break;
                    }
                case 2:
                    {
                        EnableOrDisable.EnableAndVisible(true, true, label3, button2, label11, label12, label1, label2, label4);
                        label2.Text = $"{TotalSum.ToString()} UAH";
                        label3.Text = "Card";
                        if (DeliveryWasUsed == true) EnableOrDisable.EnableAndVisible(true, true, label6, textBox1);
                        break;
                    }
                case 3:
                    {
                        label2.Text = $"{TotalSum.ToString()} UAH";
                        EnableOrDisable.EnableAndVisible(true, true, label3, button1, label11, label12, label1, label2, label4, label5, textBox2, label6, textBox1);
                        label3.Text = "Cash";
                        label11.Text = "Online";
                        break;
                    }
            }
        }

        private void button1_Click_1(object sender, System.EventArgs e)
        {
            Structers structers = new Structers();
            if (TypeOfOperation == 1)
            {
                if (DeliveryWasUsed == true)
                {
                    if (textBox1.Text.Length > 4)
                    {
                        if (textBox2.Text != null)
                        {
                            bool result = double.TryParse(textBox2.Text, out var ClientCash);
                            if (result)
                            {
                                if (ClientCash - TotalSum >= 0)
                                {
                                    EnableOrDisable.EnableAndVisible(true, true, label8, label9, label10);
                                    label9.Text = $"{(ClientCash - TotalSum).ToString()} UAH";
                                    DeliveryAddress = textBox1.Text;
                                    structers.CreateStruct(Products.ProductsListClient, TotalSum, ClientCash, ClientCash - TotalSum, LoyalCardWasUsed, DeliveryWasUsed);
                                    Transactions transactions = new Transactions();
                                    transactions.AddTransaction(CasaID, Guid.NewGuid(), NameOfCasa, DateTime.Now, TotalSum, "Offline", "Cash", LoyalCardWasUsed, DeliveryWasUsed);
                                    transactions.WriteTransactionsIntoFile();
                                    DialogResult dialogResult = MessageBox.Show("Success", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    if (dialogResult == DialogResult.OK)
                                    {
                                        Application.Exit();
                                    }
                                }
                                else MessageBox.Show("Not enough money", "Erroe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else MessageBox.Show("Please write correct number of cash you have!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else MessageBox.Show("Please write the amount of cash you have!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else MessageBox.Show("Please write your address", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    if (textBox2.Text != null)
                    {
                        bool result = double.TryParse(textBox2.Text, out var ClientCash);
                        if (result)
                        {
                            if (ClientCash - TotalSum >= 0)
                            {
                                EnableOrDisable.EnableAndVisible(true, true, label8, label9, label10);
                                label9.Text = $"{(ClientCash - TotalSum).ToString()} UAH";
                                DeliveryAddress = textBox1.Text;
                                structers.CreateStruct(Products.ProductsListClient, TotalSum, ClientCash, ClientCash - TotalSum, LoyalCardWasUsed, DeliveryWasUsed);
                                Transactions transactions = new Transactions();
                                transactions.AddTransaction(CasaID, Guid.NewGuid(), NameOfCasa, DateTime.Now, TotalSum, "Offline", "Cash", LoyalCardWasUsed, DeliveryWasUsed);
                                transactions.WriteTransactionsIntoFile();
                                DialogResult dialogResult = MessageBox.Show("Success", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (dialogResult == DialogResult.OK)
                                {
                                    Application.Exit();
                                }
                            }
                            else MessageBox.Show("Not enough money", "Erroe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else MessageBox.Show("Please write correct number of cash you have!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else MessageBox.Show("Please write the amount of cash you have!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            if (TypeOfOperation == 3)
            {
                int CostDelivery = 250;
                if (AverageAmountThings >= 50) CostDelivery = 0;
                if (textBox1.Text.Length > 4)
                {
                    if (textBox2.Text != null)
                    {
                        bool result = double.TryParse(textBox2.Text, out var ClientCash);
                        if (result)
                        {
                            if (ClientCash - TotalSum >= 0)
                            {
                                EnableOrDisable.EnableAndVisible(true, true, label8, label9, label10);
                                label9.Text = $"{(ClientCash - TotalSum).ToString()} UAH";
                                DeliveryAddress = textBox1.Text;
                                structers.CreateStruct(Products.ProductsListClient, TotalSum, ClientCash, ClientCash - TotalSum, CostDelivery);
                                Transactions transactions = new Transactions();
                                transactions.AddTransaction(CasaID, Guid.NewGuid(), "-", DateTime.Now, TotalSum, "Online", "Cash", LoyalCardWasUsed, DeliveryWasUsed);
                                transactions.WriteTransactionsIntoFile();
                                DialogResult dialogResult = MessageBox.Show("Success", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                if (dialogResult == DialogResult.OK)
                                {
                                    Application.Exit();
                                }
                            }
                            else MessageBox.Show("Not enough money", "Erroe", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                        else MessageBox.Show("Please write correct number of cash you have!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else MessageBox.Show("Please write the amount of cash you have!", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else MessageBox.Show("Please write your address", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            if (TypeOfOperation == 2)
            {
                Structers structers = new Structers();
                if (DeliveryWasUsed == true)
                {
                    if (textBox1.Text.Length > 4)
                    {
                        EnableOrDisable.EnableAndVisible(true, true, label10);
                        DeliveryAddress = textBox1.Text;
                        structers.CreateStruct(Products.ProductsListClient, TotalSum, LoyalCardWasUsed, DeliveryWasUsed);
                        Transactions transactions = new Transactions();
                        transactions.AddTransaction(CasaID, Guid.NewGuid(), NameOfCasa, DateTime.Now, TotalSum, "Offline", "Card", LoyalCardWasUsed, DeliveryWasUsed);
                        transactions.WriteTransactionsIntoFile();
                        DialogResult dialogResult = MessageBox.Show("Success", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (dialogResult == DialogResult.OK)
                        {
                            Application.Exit();
                        }
                    }
                    else MessageBox.Show("Please write your address", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    EnableOrDisable.EnableAndVisible(true, true, label10);
                    DeliveryAddress = textBox1.Text;
                    structers.CreateStruct(Products.ProductsListClient, TotalSum, LoyalCardWasUsed, DeliveryWasUsed);
                    Transactions transactions = new Transactions();
                    transactions.AddTransaction(CasaID, Guid.NewGuid(), NameOfCasa, DateTime.Now, TotalSum, "Offline", "Card", LoyalCardWasUsed, DeliveryWasUsed);
                    transactions.WriteTransactionsIntoFile();
                    DialogResult dialogResult = MessageBox.Show("Success", "Congratulations", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (dialogResult == DialogResult.OK)
                    {
                        Application.Exit();
                    }
                }
            }
        }

        private void LastWindowPay_FormClosing(object sender, FormClosingEventArgs e)
        {
            //Transactions transactions = new Transactions();
            //transactions.GetTransactions();
            Application.Exit();
        }
    }
}
