using ProductLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProductStore
{
    public partial class frmMainProduct : Form
    {
       
        private ProductDB db = new ProductDB();
        private List<Product> products;
        
        public frmMainProduct()
        {
            InitializeComponent();
        }

        private void frmMainProduct_Load(object sender, EventArgs e)
        { 
           
            LoadProduct();
        }
        private void LoadProduct()
        {
           
            products = db.GetProductList();
            
            txtProductID.DataBindings.Clear();
            txtProductName.DataBindings.Clear();
            txtPrice.DataBindings.Clear();
            txtQuantity.DataBindings.Clear();
           
            
            dgvListProduct.DataSource = products;
        }

        // try-catch loi
        private string validData(bool check) 
        {
            string mes = "";
            //ID    
            if (check)
            {
                try
                {
                    int Id = int.Parse(txtProductID.Text);
                    db.GetProductList().ForEach(delegate (Product tp)
                    {
                        if (tp.ProductID == Id)
                        {
                            mes += "ID is existed!\n";
                        }
                    });
                }
                catch (Exception e)
                {
                    mes += "ID must be a integer number!\n";
                }
            }
            //Name
            if (txtProductName.Text.Trim().Length == 0)
            {
                mes += "Name cannot be empty!\n";
            }
            //UnitPrice
            try
            {
                float price = float.Parse(txtPrice.Text);
                if (price < 0)
                {
                    mes += "UnitPrice must be greater or equal than 0!\n";
                }
            }
            catch (Exception e)
            {
                mes += "UnitPrice must be a number!\n";
            }
            //Quantity
            try
            {
                int quantity = int.Parse(txtQuantity.Text);
                if (quantity < 0)
                {
                    mes += "Quantity must be greater or equal than 0!\n";
                }
            }
            catch (Exception e)
            {
                mes += "Quantity must be a integer number!\n";
            }
            return mes;
        }
       
        private void btnAddNew_Click(object sender, EventArgs e)
        {
            if(txtProductID.ReadOnly)
             {
                txtProductID.ReadOnly = false;
                txtProductName.Clear();
                txtQuantity.Clear();
                txtPrice.Clear();
            }
            else {
                string mes = validData(true);
                if (mes.Length != 0)
                {
                    MessageBox.Show(mes);
                    return;
                }
                Product p = new Product()
                {
                    ProductID = int.Parse(txtProductID.Text),
                    ProductName = txtProductName.Text,
                    UnitPrice = float.Parse(txtPrice.Text),
                    Quantity = int.Parse(txtQuantity.Text)
                };
                if (db.AddProduct(p))
                {
                    MessageBox.Show("Insert sucessfull.");
                }
                else
                {
                    MessageBox.Show("Insert Failed...");
                }
                LoadProduct();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            string mes = validData(false);
            if (mes.Length != 0)
            {
                MessageBox.Show(mes);
                return;
            }
            Product p = new Product()
            {
               ProductID = int.Parse(txtProductID.Text),
                ProductName = txtProductName.Text,
                UnitPrice = float.Parse(txtPrice.Text),
                Quantity = int.Parse(txtQuantity.Text)
            };
            if (db.UpdateProduct(p))
            {
                MessageBox.Show("Update sucessfull.");
                LoadProduct();
            }
            else
            {
                MessageBox.Show("Update Failed...");
            }
            LoadProduct();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            Product p = new Product() { ProductID = int.Parse(txtProductID.Text) };
            if (db.DeleteProduct(p))
            {
                MessageBox.Show("Delete sucessfull.");
                LoadProduct();
            }
            else
            {
                MessageBox.Show("Delete Failed...");
            }
            LoadProduct();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            frmSearch frm = new frmSearch();
            frm.ShowDialog();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Do You Want to Exit", "Notifications", MessageBoxButtons.YesNoCancel)==DialogResult.Yes )
            {
                Application.Exit();
            }

        }

        private void dgvListProduct_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            txtProductID.ReadOnly = true;
            DataGridViewRow dgv = this.dgvListProduct.Rows[e.RowIndex];
            txtProductID.Text = dgv.Cells[0].Value.ToString();
            txtProductName.Text = dgv.Cells[1].Value.ToString();
            txtPrice.Text = dgv.Cells[2].Value.ToString();
            txtQuantity.Text = dgv.Cells[3].Value.ToString();
        }
    }
}
