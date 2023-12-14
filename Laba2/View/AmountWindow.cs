using Laba2Model;
using System;
using System.IO;
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
        private int _indexForOnline { get; set; }
        private event EventHandler AmountThingOnBase;
        private Func<int, int, int> BalanceThings;
        public AmountWindow(DataGridViewRow selectedRow, bool online)
        {
            Online = online;
            AmountThingOnBase += MessageAmountDeficit;//2
            BalanceThings += CreateOrderProducts;//4
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
                            if (Products.ProductsListBase[i].NumberOfProducts >= AmountOfThings)
                            {
                                Products.ProductsListClient[i].NumberOfProducts += AmountOfThings;
                                Products.ProductsListBase[i].NumberOfProducts -= AmountOfThings;
                                BalanceThings.Invoke(i, AmountOfThings);
                                break;
                            }
                            else
                            {
                                AmountThingOnBase.Invoke(this, EventArgs.Empty);
                                break;
                            }
                        }
                    }
                    for (int i = 0; i < Products.ProductsListBase.Count; i++)
                    {
                        if (NameOfProduct == Products.ProductsListBase[i].NameOfProduct)
                            _indexForOnline = i;
                    }

                    if (CountForNewAmount != true)
                    {
                        if (Products.ProductsListBase[_indexForOnline].NumberOfProducts >= AmountOfThings)
                        {
                            Products.ProductsListClient.Add(new Products(NameOfProduct, ArticleOfProduct, PriceOfProduct, TypeOfProduct, AmountOfThings));
                            Products.ProductsListBase[_indexForOnline].NumberOfProducts -= AmountOfThings;
                            BalanceThings.Invoke(_indexForOnline, AmountOfThings);
                        }
                        else
                            AmountThingOnBase.Invoke(this, EventArgs.Empty);
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

        private void MessageAmountDeficit(object sender, EventArgs e)
        {
            MessageBox.Show("Too many products! Not enough products in stock", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private int CreateOrderProducts(int i, int number)
        {
            var FilePath = "OrderProducts.txt";
            using (StreamWriter writer = new StreamWriter(FilePath, true))
            {
                writer.WriteLine($"{Products.ProductsListBase[i].NameOfProduct}: {number}");
            }
            return 0;
        }
    }
}
