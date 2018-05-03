using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;


namespace customerIDConnection
{
    public partial class Form1 : Form
    {
        // establishing a connection between sql and C#
        const string connstring = @"server=PL3\MTCDB;Database=AdventureWorks2012;
                           Trusted_Connection=True;User ID=AdvWorks2012;Password=1234";
        SqlConnection sqlconnection = new SqlConnection(connstring);


        public Form1()
        {


            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {

            // loading the stored procedure
            SqlDataAdapter dataAdapt = new SqlDataAdapter("dbo.saleid", connstring);
            DataTable customerdata = new DataTable();

            // variables

            int custid;
            string customer;


            try
            {
                // filling in the grid with the sql specfified data 
                dataAdapt.Fill(customerdata);


                foreach (DataRow rcustomer in customerdata.Rows)
                {
                    custid = int.Parse(rcustomer.ItemArray[0].ToString());
                    customer = rcustomer.ItemArray[1].ToString();
                    CustomerComboBox.Items.Add(new cObject(custid, customer));

                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow row = this.DataGrid.Rows[e.RowIndex];

                SaleidTextBox.Text = row.Cells[0].Value.ToString();

            }

        }

        private void CustomerComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            cObject current = (cObject)CustomerComboBox.SelectedItem;

            int customerID = current.custid;
            DataTable customerdatatable = new DataTable();


            try
            {

                SqlCommand sqlcom = new SqlCommand("dbo.customerid", sqlconnection);
                sqlcom.CommandType = CommandType.StoredProcedure;

                SqlParameter custpm = new SqlParameter("@customerID", customerID);
                sqlcom.Parameters.Add(custpm);

                SqlDataAdapter datafeed = new SqlDataAdapter(sqlcom);

                datafeed.Fill(customerdatatable);


                DataGrid.DataSource = customerdatatable;

            }

            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }


        public class cObject
        {
            int customerID;
            string customername;



            public cObject(int cusid, string custname)
            {
                customerID = cusid;
                customername = custname;


            }

            public cObject(string custname)
            {
                customername = custname;
            }


            public int custid
            {
                get { return customerID; }
                set { customerID = value; }
            }


            public string custname
            {
                get { return customername; }
                set { customername = value; }
            }


            public override string ToString()
            {
                return custname;
            }



        }



    }

}
    
    



