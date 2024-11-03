using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;
using MySql.Data.MySqlClient;
using MySql.Data;
using System.Runtime.CompilerServices;
namespace DB_ACT_TOLENTINO_CAR_BRAND
{
    public partial class Form1 : Form
    {
        string connStr = "server=localhost;database=db_act1;uid=root;pwd=mysql;port=3306;";
        MySqlConnection conn;
        public Form1()
        {
         
            InitializeComponent();
        }

        
        private void Form1_Load_1(object sender, EventArgs e)
        {

            //DISPLAY THE DATA FROM EXCEL

            //EXCEL'S FILE PATH 
            string carBrands = "    \"C:\\Users\\franc\\Downloads\\car_brands (1).xlsx\"";
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + carBrands + "; Extended Properties='Excel 12.0 Xml; HDR=Yes'");
            conn.Open();
            OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", conn);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            cboBrand.DataSource = dt;
            cboBrand.DisplayMember = "acura";
            cboBrand.ValueMember = "acura";

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {


            DataTable dt = new DataTable();

            dt.Columns.Add("MODEL");
            dt.Columns.Add("BRAND");
            dt.Columns.Add("YEAR");

            string query = "select model,brand,year from car where model = @model and brand = @brand ";
            conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@model",txtKeyword.Text);
            cmd.Parameters.AddWithValue("@brand", cboBrand.Text);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            da.Fill(dt);
            

            grdResults.DataSource = dt;
            conn.Close();


        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtKeyword.Clear();
            grdResults.DataSource= null;
            cboBrand.SelectedIndex = -1;
        }

        private void txtKeyword_TextChanged(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("MODEL");
            dt.Columns.Add("BRAND");
            dt.Columns.Add("YEAR");
            conn = new MySqlConnection(connStr);
            //DISPLAYS DATA AUTOMATICALLY WHILE TYPING ANY LETTER
            //use "%"
            string query = "select model,brand,year from car where model like '%" + txtKeyword.Text + "%' and brand ='" + cboBrand.Text + "' ";
            conn.Open();
            //DATAADAPTER fetches data from the database and fills it into a DataTable that can be manipulated in memory. 
            MySqlDataAdapter adapter = new MySqlDataAdapter(query, conn);
            adapter.Fill(dt);
            conn.Close();
            grdResults.DataSource = dt;
        }

        private void addUpadateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddUpdate frmAddUpdate = new frmAddUpdate(); 
            frmAddUpdate.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            frmDelete frmDelete = new frmDelete();
            frmDelete.ShowDialog();
        }
    }
}
