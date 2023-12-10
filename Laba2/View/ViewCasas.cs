using Laba2EnableOrDisableModel;
using Laba2Model;
using System.Drawing;
using System.Windows.Forms;

namespace Laba2View
{
    public partial class ViewCasas : Form
    {
        private bool DeleteCasa { get; set; }
        private BindingSource _bs { get; set; }
        public ViewCasas(bool deleteCasa = false)
        {
            DeleteCasa = deleteCasa;
            InitializeComponent();
        }

        private void ViewProductsAndCasas_Activated(object sender, System.EventArgs e)
        {
            if (DeleteCasa == true)
            {
                EnableOrDisable.EnableAndVisible(false, false, dataGridView1);
                EnableOrDisable.EnableAndVisible(true, true, button1, label1, comboBox1);
            }
            else
            {
                EnableOrDisable.EnableAndVisible(false, false, button1, label1, comboBox1);
                _bs = new BindingSource(Supermarket.dict, null);
                _bs.ResetBindings(true);
                Font myFont = new Font("Arial", 12, FontStyle.Regular);
                dataGridView1.DefaultCellStyle.Font = myFont;
                dataGridView1.DataSource = _bs;
            }
        }

        private void ViewProductsAndCasas_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Owner != null)
                this.Owner.Show();
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            if (comboBox1.Text != "")
            {
                foreach (var item in Supermarket.dict)
                {
                    if (item.Key.ToString() == comboBox1.Text)
                    {
                        MessageBox.Show($"{item.Value} was deleted", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        Supermarket.dict.Remove(item.Key);
                        ViewInformationSupermarket.DecreaseValueDictSumDefault();
                        Supermarket.DecreaseValueDictSumDefault();
                        break;
                    }
                }
            }
            else
                MessageBox.Show("Please enter id of casa", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
