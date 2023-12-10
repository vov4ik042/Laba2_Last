using System;
using System.Windows.Forms;
using Laba2Model;

namespace Laba2View
{

    public partial class ViewInformationSupermarket : Form
    {
        private static string NameSupermarket { get; set; }
        private static int NumberOfCasas { get; set; }
        private static double DefaultMoneyCasa { get; set; }
        private static double SumMoneySupermarket { get; set; }
        private static string AddressOfSupermarket { get; set; }
        private static string CodeEDRPOY { get; set; }
        private bool ResultParse { get; set; }
        private static bool ValueForCloseWindow { get; set; }
        public ViewInformationSupermarket()
        {
            InitializeComponent();
            comboBox1.Text = NumberOfCasas.ToString();
            comboBox2.Text = NameSupermarket;
            comboBox3.Text = DefaultMoneyCasa.ToString();
            comboBox4.Text = SumMoneySupermarket.ToString();
            comboBox5.Text = AddressOfSupermarket;
            comboBox6.Text = CodeEDRPOY;
        }
        public ViewInformationSupermarket(int yes)
        {

        }
        public void MethodForCloseWindow()
        {
            ValueForCloseWindow = true;
        }

        public static void DecreaseValueDictSumDefault()
        {
            NumberOfCasas--;
            SumMoneySupermarket -= DefaultMoneyCasa;
        }
        public void ViewInformationSupermarketUpdate(string Name, int Number, double Default, double Sum, string Address, string Code)
        {
            NameSupermarket = Name;
            NumberOfCasas = Number;
            SumMoneySupermarket = Sum;
            DefaultMoneyCasa = Default;
            AddressOfSupermarket = Address;
            CodeEDRPOY = Code;
        }
        public static void ViewInformationSupermarketUpdateNewCASA(int Number, double Default)
        {
            NumberOfCasas = Number;
            SumMoneySupermarket += Default;
            DefaultMoneyCasa = Default;
        }
        public void ViewInformationSupermarketBaseValues()
        {
            NameSupermarket = "Market+";
            NumberOfCasas = 0;
            SumMoneySupermarket = 0;
            DefaultMoneyCasa = 0;
            AddressOfSupermarket = "St. Kingsman 6400";
            CodeEDRPOY = "123d3112";
            Supermarket supermarket = new Supermarket();
            supermarket.UpdateGatheredInformationSupermarket(NameSupermarket, NumberOfCasas, 2600,
                2600, AddressOfSupermarket, CodeEDRPOY);
        }

        private void button5_Click(object sender, EventArgs e)//Save
        {
            if (CollectInformation())
            {
                Supermarket supermarket = new Supermarket();
                supermarket.UpdateGatheredInformationSupermarket(NameSupermarket, NumberOfCasas, DefaultMoneyCasa,
                    SumMoneySupermarket, AddressOfSupermarket, CodeEDRPOY);
                supermarket.SaveInformationSupermarket(saveFileDialog1);
            }
        }

        private void button6_Click(object sender, EventArgs e)//Load
        {
            Supermarket supermarket = new Supermarket();
            supermarket.LoadInformationSupermarket(openFileDialog1);
            this.Close();
        }

        private bool CollectInformation()
        {
            int resultNumberCasa;
            double resultDefaultMoney;
            double resultSumMoney;
            NameSupermarket = comboBox2.Text;
            ResultParse = int.TryParse(comboBox1.Text, out resultNumberCasa);
            if (ResultParse)
            {
                NumberOfCasas = resultNumberCasa;
                ResultParse = double.TryParse(comboBox3.Text, out resultDefaultMoney);
                if (ResultParse)
                {
                    DefaultMoneyCasa = resultDefaultMoney;
                    ResultParse = double.TryParse(comboBox4.Text, out resultSumMoney);
                    if (ResultParse && resultSumMoney == DefaultMoneyCasa * NumberOfCasas)
                    {
                        SumMoneySupermarket = resultSumMoney;
                        AddressOfSupermarket = comboBox5.Text;
                        CodeEDRPOY = comboBox6.Text;
                        return true;
                    }
                    else
                    {
                        MessageBox.Show("Invalid sum money", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return false;
                    }
                }
                else
                {
                    MessageBox.Show("Invalid default number of money for casa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                MessageBox.Show("Invalid number of casas", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            int numberOfCasas;
            double defaultsum;
            bool result = int.TryParse(comboBox1.Text, out numberOfCasas);
            if (result)
            {
                bool resultDefault = double.TryParse(comboBox3.Text, out defaultsum);
                if (resultDefault)
                {
                    comboBox4.Text = (defaultsum * numberOfCasas).ToString();
                    NumberOfCasas = numberOfCasas;
                    SumMoneySupermarket = defaultsum * numberOfCasas;
                    DefaultMoneyCasa = defaultsum;
                    if (comboBox2.Text != "" && comboBox5.Text != "" && comboBox6.Text != "")
                    {
                        NameSupermarket = comboBox2.Text;
                        AddressOfSupermarket = comboBox5.Text;
                        CodeEDRPOY = comboBox6.Text;
                        var supermarket = new Supermarket();
                        supermarket.AddOrDeleteCasa(NumberOfCasas, DefaultMoneyCasa, SumMoneySupermarket,
                            comboBox2.Text, comboBox5.Text, comboBox6.Text);
                    }
                }
            }
        }

        private void ViewInformationSupermarket_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (ValueForCloseWindow)
            {
                var viewInformationSupermarket = new ViewInformationSupermarket();
                viewInformationSupermarket.Show();
                ValueForCloseWindow = false;
            }
        }
    }
}
