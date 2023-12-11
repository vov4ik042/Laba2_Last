using System.Windows.Forms;
using Laba2EnableOrDisableModel;

namespace Laba2View
{
    public partial class OperationPay : Form
    {
        private double AverageAmountThings { get; set; }
        private bool Radiobutton1Clicked { get; set; }
        private bool CloseWindowOrSetValue { get; set; }
        private bool Radiobutton2Clicked { get; set; }
        private bool Radiobutton3Clicked { get; set; }
        private bool LoyalCardWasUsed { get; set; }
        private bool DeliveryWasUsed { get; set; }
        private double CardDiscount { get; set; }
        private double DeliverySumPayRetail { get; set; }
        private double TotalSum { get; set; }
        private double ConstTotalSum { get; set; }
        private bool LoyaltyCardResult { get; set; }
        private int[] ExamplesOfLoyaltyCard = { 1111, 2222, 3333, 4444, 5555, 66666, 7777, 8888 };
        public OperationPay()
        {
            InitializeComponent();
        }
        public void InitializeForm(bool PossibleCardPay, double AverageAmountThings, double SumPay, bool online)
        {
            this.AverageAmountThings = AverageAmountThings;
            if (this.TotalSum == 0)
            {
                this.TotalSum += SumPay;
                ConstTotalSum = SumPay;
            }
            label6.Text = $"{SumPay.ToString()} UAH";
            label4.Text = $"{TotalSum.ToString()} UAH";

            if (online == true)
            {
                if (AverageAmountThings >= 50) label1.Text = "Available payment methods(Wholesale)";
                else label1.Text = "Available payment methods(Retail)";
                EnableOrDisable.EnableAndVisible(true, true, radioButton3);
                EnableOrDisable.EnableAndVisible(false, false, radioButton1, radioButton2, radioButton4, radioButton5, label5, label2, button2, comboBox1);
            }
            else
            {
                if (AverageAmountThings >= 30) label1.Text = "Available payment methods(Wholesale)";
                else label1.Text = "Available payment methods(Retail)";
                if (PossibleCardPay == true) EnableOrDisable.EnableAndVisible(true, true, radioButton2);
                EnableOrDisable.EnableAndVisible(true, true, radioButton1);
                EnableOrDisable.EnableAndVisible(false, false, radioButton3, radioButton4, radioButton5, label5, label2, button2, comboBox1);
            }
        }

        private void radioButton1_Click(object sender, System.EventArgs e)//Cash payment in store
        {
            Radiobutton1Clicked = true;
            EnableOrDisable.EnableAndVisible(false, true, radioButton2);
            EnableOrDisable.EnableAndVisible(true, true, radioButton4, radioButton5, label5, label7, label8, label9, label11);
            if (LoyalCardWasUsed != true) EnableOrDisable.EnableAndVisible(true, true, label2, comboBox1, button2);
        }

        private void radioButton2_Click(object sender, System.EventArgs e)//Card payment in store
        {
            Radiobutton2Clicked = true;
            EnableOrDisable.EnableAndVisible(false, true, radioButton1);
            EnableOrDisable.EnableAndVisible(true, true, radioButton4, radioButton5, label5, label7, label8, label9, label11);
            if (LoyalCardWasUsed != true) EnableOrDisable.EnableAndVisible(true, true, label2, comboBox1, button2);
        }

        private void radioButton3_Click(object sender, System.EventArgs e)//Payment in cash to the courier
        {
            Radiobutton3Clicked = true;
            EnableOrDisable.EnableAndVisible(true, true, label7, label8, label9, label11);
            EnableOrDisable.EnableAndVisible(false, false, label2, comboBox1, button2, radioButton4, radioButton5, label5);
            if (AverageAmountThings < 50)
            {
                DeliveryWasUsed = false;
                DeliverySumPayRetail = 250;
            }
            if (AverageAmountThings >= 50)
            {
                DeliveryWasUsed = true;
                DeliverySumPayRetail = 0;
            }
            TotalSum += DeliverySumPayRetail;
            label7.Text = $"{DeliverySumPayRetail.ToString()} UAH";
            label4.Text = $"{TotalSum.ToString()} UAH";
            radioButton3.Enabled = false;
        }

        private void button2_Click(object sender, System.EventArgs e)//Loyalty card button
        {
            bool result = int.TryParse(comboBox1.Text, out int LoyaltyNumber);
            if (result == true && LoyaltyNumber != default)
            {
                for (int i = 0; i < ExamplesOfLoyaltyCard.Length; i++)
                {
                    if (ExamplesOfLoyaltyCard[i] == int.Parse(comboBox1.Text))
                    {
                        LoyaltyCardResult = true; break;
                    }
                }
                if (LoyaltyCardResult == true)
                {
                    if (AverageAmountThings >= 30)
                    {
                        LoyalCardWasUsed = true;
                        TotalSum -= 50;
                        CardDiscount = (TotalSum * 8) / 100;
                        TotalSum -= CardDiscount;
                        TotalSum += 50;
                    }
                    else
                    {
                        LoyalCardWasUsed = true;
                        TotalSum -= 250;
                        CardDiscount = (TotalSum * 5) / 100;
                        TotalSum -= CardDiscount;
                        TotalSum += 250;
                    }
                    label8.Text = $"{CardDiscount.ToString()} UAH";
                    label4.Text = $"{TotalSum.ToString()} UAH";
                    EnableOrDisable.EnableAndVisible(false, false, button2);
                    EnableOrDisable.EnableAndVisible(false, true, comboBox1);
                }
                else
                {
                    MessageBox.Show("No card found!", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Please put correct number", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void radioButton4_Click(object sender, System.EventArgs e)//Delivery yes
        {
            if (DeliverySumPayRetail == 0)
            {
                if (AverageAmountThings < 30)
                    DeliverySumPayRetail = 250;
                if (AverageAmountThings >= 30)
                    DeliverySumPayRetail = 50;
                TotalSum += DeliverySumPayRetail;
                label7.Text = $"{DeliverySumPayRetail.ToString()} UAH";
                label4.Text = $"{TotalSum.ToString()} UAH";
                DeliveryWasUsed = true;
            }
        }

        private void radioButton5_Click(object sender, System.EventArgs e)//Delivery no
        {
            if (DeliverySumPayRetail == 250)
            {
                TotalSum -= DeliverySumPayRetail;
                DeliverySumPayRetail = default;
                label7.Text = $"{DeliverySumPayRetail} UAH";
                label4.Text = $"{TotalSum.ToString()} UAH";
                DeliveryWasUsed = false;
            }
            if (DeliverySumPayRetail == 50)
            {
                TotalSum -= DeliverySumPayRetail;
                DeliverySumPayRetail = default;
                label7.Text = $"{DeliverySumPayRetail} UAH";
                label4.Text = $"{TotalSum.ToString()} UAH";
                DeliveryWasUsed = false;
            }
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (Radiobutton1Clicked == true || Radiobutton2Clicked == true || Radiobutton3Clicked == true)
            {
                LastWindowPay lastWindowPay = new LastWindowPay();
                lastWindowPay.Show();
                if (Radiobutton1Clicked == true)
                {
                    lastWindowPay.GettingInformation(1, TotalSum, AverageAmountThings, DeliveryWasUsed, LoyalCardWasUsed);
                    CloseWindowOrSetValue = true;
                }
                if (Radiobutton2Clicked == true)
                {
                    lastWindowPay.GettingInformation(2, TotalSum, AverageAmountThings, DeliveryWasUsed, LoyalCardWasUsed);
                    CloseWindowOrSetValue = true;
                }
                if (Radiobutton3Clicked == true)
                {
                    lastWindowPay.GettingInformation(3, TotalSum, AverageAmountThings, DeliveryWasUsed, LoyalCardWasUsed);
                    CloseWindowOrSetValue = true;
                }
                this.Close();
            }
            else MessageBox.Show("Please choose your payment method", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void OperationPay_FormClosing(object sender, FormClosingEventArgs e)
        {
            MainView mainView;
            if (CloseWindowOrSetValue == true) mainView = new MainView(ConstTotalSum, true);
            else
            {
                mainView = new MainView();
                mainView.Show();
            }
        }
    }
}
