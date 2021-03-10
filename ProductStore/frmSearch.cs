using ProductLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductStore
{
    public partial class frmSearch : Form
    {
        private ProductDB db = new ProductDB();
        public string SearchProductName { get; set; }
        public frmSearch()
        {
            InitializeComponent();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            Product p = db.FindProduct(int.Parse(txtProductID.Text));
            if (p != null)
            {
          //      lbRs.Text = "Result: 1";
                txtID.Text = p.ProductID.ToString();
                txtName.Text = p.ProductName;
                txtPrice.Text = p.UnitPrice.ToString();
                txtQuantity.Text = p.Quantity.ToString();
                txtSubTotal.Text = p.SubTotal.ToString();
            }
            else
            {
                MessageBox.Show("ID not exit");
                txtID.Text = "";
                txtName.Text = "";
                txtPrice.Text = "";
                txtQuantity.Text = "";
                txtSubTotal.Text = "";
            }
        }

        private void AA_Click(object sender, EventArgs e)
        {
            txtProductID.Text = SearchProductName;
        }

        
    }
}
