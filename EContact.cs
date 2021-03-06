﻿using EContact.EContactClasses;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EContact
{
    public partial class EContact : Form
    {
        Contact c = new Contact();

        public EContact()
        {
            InitializeComponent();
        }

        //Method to load data in datagrid
        public void LoadDataGrid()
        {
            //Load data in datagrid
            DataTable DT = c.Select();
            dgv_ContactList.DataSource = DT; 
            //clear fields
            Clear();
        }

        private void EContact_Load(object sender, EventArgs e)
        {
            LoadDataGrid();
        }

        private void btn_add_Click(object sender, EventArgs e)
        {
            //Get the value from the input fields
            c.FirstName = txtbx_fName.Text;
            c.LastName = txtbx_LName.Text;
            c.ContactNo = txtbx_contactNum.Text;
            c.Address = txtbx_Address.Text;
            c.Gender = cmbx_gender.Text;

            //Insert data into DB using methods created in Contact class
            bool isAdded = c.Insert(c);
            if (isAdded == true)
            {
                //Successfully added contact
                MessageBox.Show("New Contact Added Successfully!");
                LoadDataGrid();
            } else
            {
                //Failed to add contact
                MessageBox.Show("Failed to Add Contact. Try Again.");
                Clear();
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            //close the form
            this.Close();
        }

        //Method to Clear Fields
        public void Clear()
        {
            txtbx_contactID.Text = "";
            txtbx_fName.Text = "";
            txtbx_LName.Text = "";
            txtbx_contactNum.Text = "";
            txtbx_Address.Text = "";
            cmbx_gender.Text = "";
        }

        private void btn_update_Click(object sender, EventArgs e)
        {
            //Get the data from textboxes
            c.ContactID = int.Parse(txtbx_contactID.Text);
            c.FirstName = txtbx_fName.Text;
            c.LastName = txtbx_LName.Text;
            c.ContactNo = txtbx_contactNum.Text;
            c.Address = txtbx_Address.Text;
            c.Gender = cmbx_gender.Text;
            //Update data in DB
            bool isUpdated = c.Update(c);
            if (isUpdated == true)
            {
                //Updated contact successfully
                MessageBox.Show("Contact has been updated successfully!");
                LoadDataGrid();
            }
            else
            {
                //Failed to update contact
                MessageBox.Show("Failed to update contact. Try again.");
            }
        }

        private void r(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgv_ContactList_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            //Get data from datagrid view and load it in the text boxes
            //Identify the row on which the mouse is clicked
            int rowIndex = e.RowIndex;
            txtbx_contactID.Text = dgv_ContactList.Rows[rowIndex].Cells[0].Value.ToString();
            txtbx_fName.Text = dgv_ContactList.Rows[rowIndex].Cells[1].Value.ToString();
            txtbx_LName.Text = dgv_ContactList.Rows[rowIndex].Cells[2].Value.ToString();
            txtbx_contactNum.Text = dgv_ContactList.Rows[rowIndex].Cells[3].Value.ToString();
            txtbx_Address.Text = dgv_ContactList.Rows[rowIndex].Cells[4].Value.ToString();
            cmbx_gender.Text = dgv_ContactList.Rows[rowIndex].Cells[5].Value.ToString();

        }

        private void btn_clear_Click(object sender, EventArgs e)
        {
            //call the clear() method
            Clear();
        }

        private void btn_delete_Click(object sender, EventArgs e)
        {
            //Get the contact ID from the application
            c.ContactID = Convert.ToInt32(txtbx_contactID.Text);
            bool isDeleted = c.Delete(c);
            if (isDeleted == true)
            {
                //Successfully deleted
                MessageBox.Show("Contact deleted successfully!");
                LoadDataGrid();
            }
            else
            {
                //Failed to delete
                MessageBox.Show("Failed to delete contact. Try again.");
                Clear();
            }
        }

        private void txtbx_search_TextChanged(object sender, EventArgs e)
        {
            //Get the value from the textbox
            string keyword = txtbx_search.Text;
            DataTable DT = c.Search(keyword);
            dgv_ContactList.DataSource = DT;    //load data in datagrid
        }
    }
}
