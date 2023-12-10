using System.Drawing;
using System.Windows.Forms;

namespace Laba2Model
{
    public partial class AddWindow : Form
    {
        private bool Online { get; set; }
        private BindingSource _bs;
        public AddWindow(bool online)
        {
            Online = online;
            InitializeComponent();
            _bs = new BindingSource();
            _bs.DataSource = Products.ProductsListBase;
            _bs.ResetBindings(true);
            Font myFont = new Font("Arial", 12, FontStyle.Regular);
            dataGridView1.DefaultCellStyle.Font = myFont;
            dataGridView1.DataSource = _bs;
            dataGridView1.Columns["NumberOfProducts"].Visible = false;
        }

        public AddWindow(int yes) { }

        private void dataGridView1_RowHeaderMouseClick_1(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dataGridView1.Rows[e.RowIndex];
                AmountWindow howManyThings = new AmountWindow(selectedRow, Online);
                howManyThings.ShowDialog();
            }
        }
    }
}
