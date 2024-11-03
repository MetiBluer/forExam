using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.OleDb;
namespace DB_ACT_TOLENTINO_CAR_BRAND
{
    public partial class frmDelete : Form
    {
        string connStr = "server=localhost;database=db_act1;uid=root;pwd=mysql;port=3306; ";
        MySqlConnection conn;
        public frmDelete()
        {
            InitializeComponent();
        }

        private void frmDelete_Load(object sender, EventArgs e)
        {
            string filePath = "\"C:\\Users\\franc\\Downloads\\car_brands (1).xlsx\"";
          
            OleDbConnection conn = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + filePath + "; Extended Properties='Excel 12.0 Xml; HDR=Yes'");
            conn.Open();
            OleDbDataAdapter ada = new OleDbDataAdapter("SELECT * FROM [Sheet1$]", conn);
            DataTable dt = new DataTable();
            ada.Fill(dt);
            cboBrand.DataSource = dt;
            cboBrand.DisplayMember = "acura";
            cboBrand.ValueMember = "acura";
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if ( string.IsNullOrEmpty(txtModel.Text) || string.IsNullOrEmpty(cboBrand.Text))
            {
                MessageBox.Show("All fields required");
            }
            else
            {
                string query = "delete from car where model=@model and brand=@brand ";
                conn = new MySqlConnection(connStr);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@model", txtModel.Text);
                cmd.Parameters.AddWithValue("@brand", cboBrand.Text);

                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Record Deleted");
            }
        }
    }
}
