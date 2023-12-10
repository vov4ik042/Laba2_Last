using Laba2EnableOrDisableModel;
using System;
using System.Drawing;
using System.Windows.Forms;
using Laba2Model;

namespace Laba2View
{
    public partial class ViewProducts : Form
    {
        private BindingSource _bs;
        private string NameOfProduct { get; set; }
        private int ArticleOfProduct { get; set; }
        private double PriceOfProduct { get; set; }
        private int NumberOfProducts { get; set; }
        private string TypeOfProduct { get; set; }
        private string OperationToDo { get; set; }
        public ViewProducts(string operationToDo)
        {
            OperationToDo = operationToDo;
            InitializeComponent();
            if (operationToDo == "ADD")
            {
                EnableOrDisable.EnableAndVisible(false, false, label5, comboBox5, dataGridView1);
                EnableOrDisable.EnableAndVisible(true, true, label1, label2, label3, label4, label6, comboBox1,
                    comboBox2, comboBox3, comboBox4, comboBox6, button1);
                button1.Location = new System.Drawing.Point(156, 232);
                button1.Text = "Add product";
            }
            if (operationToDo == "DELETE")
            {
                EnableOrDisable.EnableAndVisible(true, true, label5, comboBox5, button1);
                EnableOrDisable.EnableAndVisible(false, false, label1, label2, label3, label4, label6, comboBox1,
                    comboBox2, comboBox3, comboBox4, comboBox6, dataGridView1);
                button1.Location = new System.Drawing.Point(165, 99);
                button1.Text = "Delete product";
            }
            if (operationToDo == "VIEW")
            {
                EnableOrDisable.EnableAndVisible(true, true, dataGridView1);
                EnableOrDisable.EnableAndVisible(false, false, label1, label2, label3, label4, label6, comboBox1,
                    comboBox2, comboBox3, comboBox4, comboBox6, button1);
                _bs = new BindingSource();
                _bs.DataSource = Products.ProductsListBase;
                _bs.ResetBindings(true);
                Font myFont = new Font("Arial", 12, FontStyle.Regular);
                dataGridView1.DefaultCellStyle.Font = myFont;
                dataGridView1.DataSource = _bs;
            }
        }
        private bool FindExistArticle(int article, bool delete)
        {
            foreach (var item in Products.ProductsListBase)
                if (item.ArticleOfProduct == article)
                {
                    if (delete)
                    {
                        Products.ProductsListBase.Remove(item);
                        return true;
                    }
                    else
                        return true;
                }

            return false;
        }
        private bool IsComboBoxEmpty(ComboBox comboBox, string message)
        {
            if (comboBox.Text != "")
                return true;
            else
            {
                MessageBox.Show(message, "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (OperationToDo == "ADD")
            {
                if (IsComboBoxEmpty(comboBox1, "Please write name of product") &&
                    IsComboBoxEmpty(comboBox2, "Please write article of product") &&
                    IsComboBoxEmpty(comboBox3, "Please write price of product") &&
                    IsComboBoxEmpty(comboBox4, "Please write type of product") &&
                    IsComboBoxEmpty(comboBox6, "Please write number of products"))
                {
                    if (FindExistArticle(int.Parse(comboBox2.Text), false))
                    {
                        MessageBox.Show("Product with such article already exist", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        Products.ProductsListBase.Add(new Products(comboBox1.Text, int.Parse(comboBox2.Text),
                                                double.Parse(comboBox3.Text), comboBox4.Text, int.Parse(comboBox6.Text)));
                        DialogResult = MessageBox.Show("New product was successfully added", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        if (DialogResult == DialogResult.OK)
                            this.Close();
                    }
                }
            }
            if (OperationToDo == "DELETE")
            {
                if (FindExistArticle(int.Parse(comboBox5.Text), true))
                {
                    DialogResult = MessageBox.Show("Product was successfully deleted", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (DialogResult == DialogResult.OK)
                        this.Close();
                }
                else
                {
                    MessageBox.Show("Product with such article was not found", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void comboBox2_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // If this not a number ignore 
            }
        }

        private void comboBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsLetter(e.KeyChar))
            {
                e.Handled = true; // If this not a letter ignore
            }
        }
    }
}
