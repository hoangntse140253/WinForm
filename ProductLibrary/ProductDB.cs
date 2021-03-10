using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductLibrary
{
   public class ProductDB
    {
        
        string strConnection;
        public ProductDB()
        {
            getConnectionString();
           
        }
        public string getConnectionString()
        {
            strConnection = ConfigurationManager.ConnectionStrings["SaleDB"].ConnectionString;

            return strConnection;
        }

        public Product FindProduct(int ProductID)
        {
            string SQL = "Select * from Products Where ProductID=@ID";
            Product p = null;
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(SQL, connection);
                command.Parameters.AddWithValue("@ID", ProductID);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    int id = int.Parse(reader.GetValue(0).ToString());
                    string name = reader.GetValue(1).ToString();
                    float price = float.Parse(reader.GetValue(2).ToString());
                    int quantity = int.Parse(reader.GetValue(3).ToString());
                    p = new Product()
                    {
                        ProductID = id,
                        ProductName = name,
                        UnitPrice = price,
                        Quantity = quantity

                    };
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return p;
        }

        // lay cac product
        public List<Product> GetProductList()
        {
            string SQL = "Select * from Products ";
            List<Product> rs = new List<Product>();
            SqlConnection connection = null;
            try
            {
                connection = new SqlConnection(strConnection);
                SqlCommand command = new SqlCommand(SQL, connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Product p = null;
                while (reader.Read())
                {
                    int id = int.Parse(reader.GetValue(0).ToString());
                    string name = reader.GetValue(1).ToString();
                    float price = float.Parse(reader.GetValue(2).ToString());
                    int quantity = int.Parse(reader.GetValue(3).ToString());
                    p = new Product()
                    {
                        ProductID = id,
                        ProductName = name,
                        UnitPrice = price,
                        Quantity = quantity
                    };
                    rs.Add(p);
                }

            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            finally
            {
                connection.Close();
            }
            return rs;
        }

        // add new
        public bool AddProduct(Product p)
        {
            int row = 0;
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Insert Products values(@ID,@Name,@Price,@Quantity)";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@ID", p.ProductID);
            cmd.Parameters.AddWithValue("@Name", p.ProductName);
            cmd.Parameters.AddWithValue("@Price", p.UnitPrice);
            cmd.Parameters.AddWithValue("@Quantity", p.Quantity);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                row = cmd.ExecuteNonQuery();
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            return row==1;
        }

        public bool UpdateProduct(Product p)
        {
            int row = 0;
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Update Products set ProductName=@Name,UnitPrice=@Price,Quantity=@Quantity where ProductID=@ID";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@ID", p.ProductID);
            cmd.Parameters.AddWithValue("@Name", p.ProductName);
            cmd.Parameters.AddWithValue("@Price", p.UnitPrice);
            cmd.Parameters.AddWithValue("@Quantity", p.Quantity);
            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                row = cmd.ExecuteNonQuery();
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            return row==1;
        }

        // delete
        public bool DeleteProduct(Product p)
        {
            int row = 0;
            SqlConnection cnn = new SqlConnection(strConnection);
            string SQL = "Delete Products where ProductID=@ID";
            SqlCommand cmd = new SqlCommand(SQL, cnn);
            cmd.Parameters.AddWithValue("@ID", p.ProductID);

            try
            {
                if (cnn.State == ConnectionState.Closed)
                {
                    cnn.Open();
                }
                row = cmd.ExecuteNonQuery();
            }
            catch (SqlException se)
            {
                throw new Exception(se.Message);
            }
            return row==1;
        }

       


    }
}
