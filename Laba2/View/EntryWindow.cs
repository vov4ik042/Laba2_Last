using Laba2EnableOrDisableModel;
using Laba2Model;
using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;

namespace Laba2View
{
    public partial class EntryWindow : Form
    {
        private bool Online { get; set; }
        private BindingSource _bs { get; set; }
        private bool PossibleOnlineBuying { get; set; }
        private bool PossibleOnlineBuyingViaCourier { get; set; }
        private bool PossibleOnlineBuyingViaCourierCASH { get; set; }
        private static string AddressOfLocation { get; set; }
        private byte CountForRadioButton { get; set; }//To view messagebox only once
        public EntryWindow()
        {
            var view = new ViewInformationSupermarket();
            view.ViewInformationSupermarketBaseValues();
            PossibleOnlineBuying = true;
            PossibleOnlineBuyingViaCourier = true;
            PossibleOnlineBuyingViaCourierCASH = true;
            AddressOfLocation = "St. New Yorker 41";
            Products products = new Products(1);
            InitializeComponent();
            EnableOrDisable.EnableAndVisible(false, false, comboBox1);
            Supermarket supermarket = new Supermarket();
            supermarket.AddNewCasa(supermarket);
        }
        public EntryWindow(int yes) { }

        public void UpdateBasket(string text)
        {
            MainView mainView = new MainView();
            if (MainView.MainUpdateView == true)
            {
                MainView.MainUpdateView = false;
                mainView.Text = text;
                mainView.Show();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked || comboBox1.SelectedIndex != -1)
            {
                if (radioButton1.Checked) Online = true;
                else Online = false;
                this.Hide();
                MainView mainView = new MainView();
                mainView.OnlineUpdateValue(Online);
                mainView.Show();
            }
            else
                MessageBox.Show("Please choose offline or online", "Warninig", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void EntryWindow_Activated(object sender, EventArgs e)
        {
            if (PossibleOnlineBuying == false || PossibleOnlineBuyingViaCourier == false || PossibleOnlineBuyingViaCourierCASH == false)
            {
                EnableOrDisable.EnableAndVisible(false, false, radioButton1, radioButton2);
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            CountForRadioButton++;
            if (CountForRadioButton == 1)
            {
                if (radioButton1.Checked != true)
                {
                    if (Supermarket.dict.Count == 0)
                    {
                        radioButton2.Checked = false;
                        MessageBox.Show("No available casas", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        EnableOrDisable.EnableAndVisible(true, true, comboBox1);
                        _bs = new BindingSource(Supermarket.dict.Values, null);
                        _bs.ResetBindings(true);
                        Font myFont = new Font("Arial", 11, FontStyle.Regular);
                        comboBox1.Font = myFont;
                        comboBox1.DataSource = _bs;
                    }
                }
            }
            else
                CountForRadioButton = default;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            EnableOrDisable.EnableAndVisible(false, false, comboBox1);
        }

        private void EntryWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Shift && e.KeyCode == Keys.D)
            {
                AdminWindow adminWindow = new AdminWindow();
                adminWindow.Owner = this;
                adminWindow.Show();
                this.Hide();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {
                LastWindowPay lastWindowPay = new LastWindowPay(Supermarket.dict.Keys.ElementAt(comboBox1.SelectedIndex),
                    Supermarket.dict[Supermarket.dict.Keys.ElementAt(comboBox1.SelectedIndex)]);
                MainView mainView = new MainView(Supermarket.dict.Keys.ElementAt(comboBox1.SelectedIndex),
                    Supermarket.dict[Supermarket.dict.Keys.ElementAt(comboBox1.SelectedIndex)]);
            }
        }

        public void UpdateAddressSupermarket(string address)
        {
            AddressOfLocation = address;
        }
    }
}
