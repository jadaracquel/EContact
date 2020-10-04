using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EContact.EContactClasses
{
    class Contact
    {
        //Getter Setter Properties
        //Acts as Data Carrier in the Application
        public int ContactID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ContactNo { get; set; }
        public string Address { get; set; }
        public string Gender { get; set; }
        //connect the name of the connection string in App.config file
        static string myConnString = ConfigurationManager.ConnectionStrings["connStr"].ConnectionString;

        //Selecting data from DB
        public DataTable Select()
        {
            //Step1: DB Connection
            SqlConnection conn = new SqlConnection(myConnString);
            //Create object for datatable
            DataTable DT = new DataTable();
            try
            {
                //Step 3: Write SQL query
                string sql = "SELECT * FROM ContactTable";
                //Create command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create SQL Data Adapter using sql command
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                conn.Open();
                adapter.Fill(DT);
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return DT;
        }

        //Inserting Data into DB
        public bool Insert(Contact c)
        {
            //Creating default return type and setting its value to false
            bool isInserted = false;
            //Step 1: Connect DB
            SqlConnection conn = new SqlConnection(myConnString);
            try
            {
                //Step 1: Create SQL query to insert data
                string sql = "INSERT INTO ContactTable (FirstName, LastName, ContactNo, Address, Gender) " +
                    "VALUES (@FirstName, @LastName, @ContactNo, @Address, @Gender)";
                //Create SQL command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create parameters to add data
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);

                //Open connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //If the query runs successfully, then the value of rows will be greater than 0 else its value will be 0
                if (rows > 0)
                {
                    isInserted = true;
                }
                else
                {
                    isInserted = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isInserted;
        }

        //Method to update data in DB from the application
        public bool Update(Contact c)
        {
            //Create a default return type and set its default value to false
            bool isUpdated = false;
            SqlConnection conn = new SqlConnection(myConnString);
            try
            {
                //SQL to update data in the DB
                string sql = "UPDATE ContactTable SET FirstName= @FirstName, LastName = @LastName, " +
                    "ContactNo = @ContactNo, Address = @Address, Gender = @Gender WHERE ContactID = @ContactID";
                //Create SQL Command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create parameters to add values
                cmd.Parameters.AddWithValue("@FirstName", c.FirstName);
                cmd.Parameters.AddWithValue("@LastName", c.LastName);
                cmd.Parameters.AddWithValue("@ContactNo", c.ContactNo);
                cmd.Parameters.AddWithValue("@Address", c.Address);
                cmd.Parameters.AddWithValue("@Gender", c.Gender);
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);
                //Open DB Connection
                conn.Open();
                int rows = cmd.ExecuteNonQuery();
                //If the query runs successfully, then the value of rows will be greater than 0 else its value will be 0
                if (rows > 0)
                {
                    isUpdated = true;
                }
                else
                {
                    isUpdated = false;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isUpdated;
        }

        //Method to Delete data in DB from the application
        public bool Delete(Contact c)
        {
            //Create a default return value and set to false
            bool isDeleted = false;
            //Create sql connection
            SqlConnection conn = new SqlConnection(myConnString);
            try
            {
                //Sql to delete data
                string sql = "DELETE FROM ContactTable WHERE ContactID=@ContactID";
                //Create sql command
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create parameters to add values
                cmd.Parameters.AddWithValue("@ContactID", c.ContactID);

                //Open DB Connection
                conn.Open();
                //Check if query runs successfully
                int rows = cmd.ExecuteNonQuery();
                //If the query runs successfully, then the value of rows will be greater than 0 else its value will be 0
                if (rows > 0)
                {
                    isDeleted = true;
                }
                else
                {
                    isDeleted = false;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return isDeleted;
        }

        public DataTable Search(string keyword)
        {
            //Connect DB
            SqlConnection conn = new SqlConnection(myConnString);
            DataTable DT = new DataTable();
            try
            {
                string sql = "SELECT * FROM ContactTable WHERE FirstName LIKE '%" + keyword + "%' OR LastName LIKE '%" + keyword + "%' " +
                    "OR Address LIKE '%" + keyword + "%' OR FirstName LIKE '%" + keyword + "%' OR ContactNo LIKE '%" + keyword + "%'";
                //Create command using sql and conn
                SqlCommand cmd = new SqlCommand(sql, conn);
                //Create SQL Data Adapter using sql command
                SqlDataAdapter SDA = new SqlDataAdapter(sql, conn);
                conn.Open();
                SDA.Fill(DT);
//                dgv_ContactList.DataSource = DT;
//                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                conn.Close();
            }
            return DT;
        }
    }
}
