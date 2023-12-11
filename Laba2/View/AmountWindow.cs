using Laba2Model;
using System;
using System.Windows.Forms;

namespace Laba2View
{
    public partial class AmountWindow : Form
    {
        private bool Online { get; set; }
        private DataGridViewRow selectedRow { get; set; }
        private string NameOfProduct { get; set; }
        private int ArticleOfProduct { get; set; }
        private double PriceOfProduct { get; set; }
        private string TypeOfProduct { get; set; }
        private int AmountOfThings { get; set; }
        private double _operation { get; set; }
        private bool CountForNewAmount { get; set; }
        private delegate void AmountBase();
        private event AmountBase AmountThingOnBase;
        public AmountWindow(DataGridViewRow selectedRow, bool online)
        {
            Online = online;
            AmountThingOnBase += MessageAmountDeficit;
            NameOfProduct = selectedRow.Cells["NameOfProduct"].Value.ToString();
            ArticleOfProduct = (int)selectedRow.Cells["ArticleOfProduct"].Value;
            PriceOfProduct = (double)selectedRow.Cells["PriceOfProduct"].Value;
            TypeOfProduct = selectedRow.Cells["TypeOfProduct"].Value.ToString();
            this.selectedRow = selectedRow;
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _operation = double.Parse(comboBox1.Text);
            _operation++;
            comboBox1.Text = _operation.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            _operation = double.Parse(comboBox1.Text);
            if (_operation > 0)
            {
                _operation--;
                comboBox1.Text = _operation.ToString();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            int index = 0;
            AmountOfThings = int.Parse(comboBox1.Text);
            CountForNewAmount = false;
            if (AmountOfThings != default)
            {
                if (Online)
                {
                    for (int i = 0; i < Products.ProductsListClient.Count; i++)
                    {
                        if (NameOfProduct == Products.ProductsListClient[i].NameOfProduct)
                        {
                            CountForNewAmount = true;
                            index = i;
                            if (Products.ProductsListBase[i].NumberOfProducts >= AmountOfThings)
                            {
                                Products.ProductsListClient[i].NumberOfProducts += AmountOfThings;
                                Products.ProductsListBase[i].NumberOfProducts -= AmountOfThings;
                                break;
                            }
                            else
                            {
                                AmountThingOnBase.Invoke();
                                break;
                            }
                        }
                    }

                    if (CountForNewAmount != true)
                    {
                        if (Products.ProductsListBase[index].NumberOfProducts >= AmountOfThings)
                        {
                            Products.ProductsListClient.Add(new Products(NameOfProduct, ArticleOfProduct, PriceOfProduct, TypeOfProduct, AmountOfThings));
                            Products.ProductsListBase[index].NumberOfProducts -= AmountOfThings;
                        }
                        else
                            AmountThingOnBase.Invoke();
                    }
                }
                else
                {
                    for (int i = 0; i < Products.ProductsListClient.Count; i++)
                    {
                        if (NameOfProduct == Products.ProductsListClient[i].NameOfProduct)
                        {
                            Products.ProductsListClient[i].NumberOfProducts += AmountOfThings;
                            CountForNewAmount = true;
                            break;
                        }
                    }

                    if (CountForNewAmount != true)
                        Products.ProductsListClient.Add(new Products(NameOfProduct, ArticleOfProduct, PriceOfProduct, TypeOfProduct, AmountOfThings));
                }
               
            }
            MainView.MainUpdateView = true;
            this.Close();
        }

        private void MessageAmountDeficit()
        {
            MessageBox.Show("Too many products! Not enough products in stock", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
