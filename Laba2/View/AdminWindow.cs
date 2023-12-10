using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Laba2EnableOrDisableModel;
using Laba2Model;

namespace Laba2View
{
    public partial class AdminWindow : Form
    {
        private bool FoundCorrectLoginPassword { get; set; }
        private bool ValueForClosingViewInformation { get; set; }
        Dictionary<string, string> LoginPassword = new Dictionary<string, string>
        {
            { "admin", "admin" },
            { "1111", "1111" },
            { "2222", "2222" }
        };

        public AdminWindow()
        {
            InitializeComponent();
            EnableOrDisable.EnableAndVisible(false, false, button1, button2, button3, button4, button8, button9, button7);
        }
        private void button1_Click(object sender, EventArgs e)// Add new casa
        {
            Supermarket supermarket = new Supermarket();
            supermarket.AddNewCasa(supermarket);
            MessageBox.Show("Casa was successfully created", "New casa created", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void AdminWindow_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (this.Owner != null)
            {
                this.Owner.Show();
            }
        }
        private void button8_Click(object sender, EventArgs e)// View all casas
        {
            if (Supermarket.dict.Count != 0)
            {
                ViewCasas viewProductsAndCasas = new ViewCasas(false);
                viewProductsAndCasas.Owner = this;
                viewProductsAndCasas.Show();
            }
            else
                MessageBox.Show("Current number of casas 0", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text != "" && comboBox2.Text != "")
            {
                foreach (var item in LoginPassword)
                {
                    if (item.Key == comboBox1.Text && item.Value == comboBox2.Text)
                    {
                        EnableOrDisable.EnableAndVisible(false, false, label1, label2, comboBox1, comboBox2, button10);
                        EnableOrDisable.EnableAndVisible(true, true, button1, button2, button3, button4, button8, button9, button7);
                        FoundCorrectLoginPassword = true;
                        break;
                    }
                }
                if (!FoundCorrectLoginPassword)
                {
                    MessageBox.Show("Incorrect login or password", "Not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Please write login and password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void button2_Click(object sender, EventArgs e)// Delete casa
        {
            ViewCasas viewProductsAndCasas = new ViewCasas(true);
            viewProductsAndCasas.Show();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            ViewProducts viewProducts = new ViewProducts("VIEW");
            viewProducts.Owner = this;
            viewProducts.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ViewProducts viewProducts = new ViewProducts("ADD");
            viewProducts.Owner = this;
            viewProducts.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            ViewProducts viewProducts = new ViewProducts("DELETE");
            viewProducts.Owner = this;
            viewProducts.Show();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            ViewInformationSupermarket viewInformationSupermarket = new ViewInformationSupermarket();
            viewInformationSupermarket.Show();
        }
    }
}
