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
using System.Security.Cryptography;

namespace DB_ACT_TOLENTINO_CAR_BRAND
{
    public partial class frmAddUpdate : Form
    {
        string connStr = "server=localhost;database=db_act1;uid=root;pwd=mysql;port=3306;";
        MySqlConnection conn;
        public frmAddUpdate()
        {
            InitializeComponent();
        }

        private void frmAddUpdate_Load(object sender, EventArgs e)
        {
            string filePath = @"""C:\Users\franc\Downloads\car_brands (1).xlsx""";
            OleDbConnection conn = new OleDbConnection ("Provider=Microsoft.ACE.OLEDB.12.0;Data source=" + filePath + ";Extended Properties='Excel 12.0 Xml;HDR=Yes' ");
            conn.Open();
            OleDbDataAdapter da = new OleDbDataAdapter("select * from [Sheet1$]",conn);
            DataTable dt = new DataTable();
            da.Fill(dt);
            cboBrand.DataSource = dt;   
            cboBrand.ValueMember = "acura";
            cboBrand.DisplayMember = "acura";
            cboBrand.DataSource = dt;

            display();
        }


        void search()
        {
            string retri = "select model,brand,year from car where model=@model";
            conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(retri, conn);
            cmd.Parameters.AddWithValue("@model", txtModel.Text);

            MySqlDataReader dr = cmd.ExecuteReader();

            if (dr.HasRows)
            {
                dr.Read();
            }
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {

            string query = "select model from car where model=@model";
            conn = new MySqlConnection(connStr);

            conn.Open();

            MySqlCommand cmd = new MySqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@model",txtModel.Text);
            MySqlDataReader mySqlDataReader = cmd.ExecuteReader();



            if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtModel.Text) || string.IsNullOrEmpty(txtYr.Text) || string
                .IsNullOrEmpty(cboBrand.Text))
            {
                MessageBox.Show("All fields required");
            }
            else if (mySqlDataReader.HasRows)
            {
                mySqlDataReader.Read();
                MessageBox.Show("Already exists");
            }
            else
            {
                mySqlDataReader.Close();
                string aquery = "insert into car(id,model,brand,year) values(@id,@model,@brand,@year)";

                MySqlCommand acmd = new MySqlCommand(aquery, conn);
                acmd.Parameters.AddWithValue("@id", txtID.Text);
                acmd.Parameters.AddWithValue("@model", txtModel.Text);
                acmd.Parameters.AddWithValue("@brand", cboBrand.Text);
                acmd.Parameters.AddWithValue("@year", txtYr.Text);

                acmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Record Added");
                clear();
            }
            display();


        }

        void clear()
        {
            txtID.Clear ();
            txtModel.Clear ();
            txtYr.Clear ();
            cboBrand.SelectedIndex = -1;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtModel.Text) || string.IsNullOrEmpty(txtYr.Text) || string
               .IsNullOrEmpty(cboBrand.Text))
            {
                MessageBox.Show("All fields required");
            }
            else
            {
                string query = "update car set model=@model,brand=@brand,year=@year " +
                    "where id=@id";
                conn = new MySqlConnection(connStr);
                conn.Open();
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@id", txtID.Text);
                cmd.Parameters.AddWithValue("@model", txtModel.Text);
                cmd.Parameters.AddWithValue("@brand", cboBrand.Text);
                cmd.Parameters.AddWithValue("@year", txtYr.Text);

                cmd.ExecuteNonQuery();

                conn.Close();

                MessageBox.Show("Record Updated");
                clear();
                
            }
            display();
        }
        void display()
        {
            DataTable dt = new DataTable();

            conn = new MySqlConnection(connStr);
            string query = "select * from car";
            conn = new MySqlConnection(connStr);
            conn.Open();
            MySqlCommand cmd = new MySqlCommand(query, conn);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);

            da.Fill(dt);


            grdResults.DataSource = dt;
            conn.Close();
        }
    }   
}
