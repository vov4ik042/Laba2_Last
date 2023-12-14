using System;
using System.Drawing;
using System.Windows.Forms;
using Laba2Model;

namespace Laba2View
{
    public partial class MainView : Form
    {
        private BindingSource _bs;
        public static bool MainUpdateView { get; set; }
        private double PriceForThing { get; set; }
        private int AmountOfThings { get; set; }
        private int selectedRowIndex { get; set; }
        private static bool Online { get; set; }

        private DateTime date;
        private bool PossibleCardPay { get; set; }
        private static bool OperationPayWasClosed { get; set; }
        private int AverageWholesaleOrRetail { get; set; }
        private int CountWholesaleOrRetail { get; set; }

        private string NameOfCasa { get; set; }
        private Guid IdOfCasa { get; set; }

        private bool MainViewFormClosingBool { get; set; }
        private double _sumPay;
        private double SumPay
        {
            get => _sumPay;
            set
            {
                _sumPay = value;
                HappyNewYearChanged?.Invoke(this, EventArgs.Empty);
            }
        }
        public event EventHandler HappyNewYearChanged;

        public MainView(double ConstTotalSum, bool WasClosed)
        {
            SumPay = ConstTotalSum;
            OperationPayWasClosed = WasClosed;
        }
        public void OnlineUpdateValue(bool online)
        {
            Online = online;
        }
        public MainView(int YES)
        {

        }
        public MainView(Guid id, string name)
        {
            IdOfCasa = id;
            NameOfCasa = name;
        }
        public MainView()
        {
            HappyNewYearChanged += ShowNewYear;
            InitializeComponent();
            if (Products.ProductsListClient.Count > 0)
            {
                Products products = new Products();
                _bs = new BindingSource();
                _bs.DataSource = Products.ProductsListClient;
                _bs.ResetBindings(true);
                Font myFont = new Font("Arial", 12, FontStyle.Regular);
                dataGridView1.DefaultCellStyle.Font = myFont;
                dataGridView1.DataSource = _bs;
            }
        }
        private void ShowNewYear(object sender, EventArgs e)
        {
            button4.Text = "Happy New Year!";
            Transactions transactions = new Transactions();
            transactions.AddTransaction(IdOfCasa, Guid.NewGuid(), NameOfCasa, DateTime.Now, SumPay, "EVENT", "EVENT", false, false);
            transactions.WriteTransactionsIntoFile();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            Timer timer = new Timer();
            timer.Interval = 100;
            timer.Tick += Timer_Tick;
            timer.Start();
        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            date = DateTime.Now;
            label1.Text = date.ToString();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AddWindow AddProducts = new AddWindow(Online);
            AddProducts.Show();
        }

        private void MainView_Activated(object sender, EventArgs e)
        {
            if (OperationPayWasClosed != true)
            {
                for (int i = 0; i < Products.ProductsListClient.Count; i++)
                {
                    PriceForThing = Products.ProductsListClient[i].PriceOfProduct;
                    AmountOfThings = Products.ProductsListClient[i].NumberOfProducts;
                    SumPay += PriceForThing * AmountOfThings;
                }
            }
            label3.Text = $"{SumPay} UAH";
            if (MainUpdateView == true)
            {
                MainViewFormClosingBool = true;
                EntryWindow entryWindow = new EntryWindow(1);
                entryWindow.UpdateBasket(this.Text);
                this.Close();
            }
        }

        private void MainView_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MainViewFormClosingBool != true)
                Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (selectedRowIndex >= 0 && Products.ProductsListClient.Count > 0)
            {
                SumPay -= Products.ProductsListClient[selectedRowIndex].NumberOfProducts * Products.ProductsListClient[selectedRowIndex].PriceOfProduct;
                Products.ProductsListClient.RemoveAt(selectedRowIndex);
                MainUpdateView = true;
                MainView_Activated(sender, e);
            }
            else
            {
                MessageBox.Show("Click on an item's row to remove it", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            selectedRowIndex = dataGridView1.SelectedRows[0].Index;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (Products.ProductsListClient.Count > 0)
            {
                DialogResult result = MessageBox.Show("Are you sure to delete all?", "Confirmation", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
                if (result == DialogResult.OK)
                {
                    SumPay = 0;
                    Products.ProductsListClient.Clear();
                    MainUpdateView = true;
                    MainView_Activated(sender, e);
                }
            }
            else
                MessageBox.Show("Please, add your products", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            PossibleCardPay = true;
            if (Products.ProductsListClient.Count > 0)
            {
                for (int i = 0; i < Products.ProductsListClient.Count; i++)
                {
                    AverageWholesaleOrRetail += Products.ProductsListClient[i].NumberOfProducts;
                    CountWholesaleOrRetail++;
                }
                OperationPay operationpay = new OperationPay();
                operationpay.Show();
                operationpay.InitializeForm(PossibleCardPay, AverageWholesaleOrRetail / CountWholesaleOrRetail, SumPay, Online);
                this.Hide();
            }
            else
                MessageBox.Show("Please, add your products", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
