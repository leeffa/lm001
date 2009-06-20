using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Data.Odbc;

namespace Library_Pro
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        private string query;
        private int sum;
        private int kt = 0;
        private void load_cmb(object sender, EventArgs e, ComboBox x, String query)
        {
            SqlConnection ly = new SqlConnection();
            ly.ConnectionString = "Server=.\\SQLEXPRESS;Database=Library3;Trusted_connection=true";

            ly.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(query, ly);

            DataTable dtLoad = new DataTable();
            da.Fill(dtLoad);
            
            if ((dtLoad != null) && (dtLoad.Rows.Count > 0))
            {
                x.DataSource = dtLoad;
                x.DisplayMember = "Ma_S";
                x.ValueMember = "Ma_S";
                
            }
            dtLoad = null;
            da.Dispose();
            ly.Close();
        }
        private void load_cmb0(object sender, EventArgs e, ComboBox x, String query)
        {
            SqlConnection ly = new SqlConnection();
            ly.ConnectionString = "Server=.\\SQLEXPRESS;Database=Library3;Trusted_connection=true";
            ly.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(query, ly);
            DataTable dtLoad = new DataTable();
            da.Fill(dtLoad);
            if ((dtLoad != null) && (dtLoad.Rows.Count > 0))
            {
                x.DataSource = dtLoad;
                x.DisplayMember = "Ma_m";
                x.ValueMember = "Ma_m";
            }
            dtLoad = null;
            da.Dispose();
            ly.Close();
        }
        private void load_cmb1(object sender, EventArgs e)
        {
            query = "select ma_s from sach where ma_s not in (select ma_s from muon)";
            load_cmb(sender, e, comboBox1, query);
        }
        private void load_cmb2(object sender, EventArgs e)
        {
            query = "select ma_m from THE";
            load_cmb0(sender, e, comboBox2, query);
        }
        private void load_cmb3(object sender, EventArgs e)
        {
            query = "select distinct ma_m from muon";
            load_cmb0(sender, e, comboBox3, query);
        }
        private void load_cmb8(object sender, EventArgs e)
        {
            query = "select ma_s from sach where ma_s in (select ma_s from muon)";
            load_cmb(sender, e, comboBox8, query);
        }
        private void load_cmb6(object sender, EventArgs e)
        {
            query = "select ma_s from sach";
            load_cmb(sender, e, comboBox6, query);
        }
        private void load_cmb7(object sender, EventArgs e)
        {
            query = "select ma_m from the where ma_m not in (select ma_m from muon)";
            load_cmb0(sender, e, comboBox7, query);
        }
        private void load_lv(object sender, EventArgs e, ListView y, String query, Label l)
        {

            SqlConnection ly = new SqlConnection();
            ly.ConnectionString = "Server=.\\SQLEXPRESS;Database=Library3;Trusted_connection=true";
            ly.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(query, ly);
            da.Fill(ds, "sach");
            DataTable table = ds.Tables["sach"];
            ListViewItem it;
            sum = 0;
            y.Items.Clear();
            foreach (DataRow dr in table.Rows)
            {
                sum++;
                it = y.Items.Add(dr["ma_s"].ToString());
                for (int j = 1; j < table.Columns.Count; j++)
                {
                    it.SubItems.Add(dr[j].ToString());
                }
            }
            ly.Close();
            l.Text = sum.ToString();
        }
        private void load_lv1(object sender, EventArgs e, ListView x, String query, Label l)
        {
            SqlConnection ly = new SqlConnection();
            ly.ConnectionString = "Server=.\\SQLEXPRESS;Database=Library3;Trusted_connection=true";
            ly.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(query, ly);
            da.Fill(ds, "the");
            DataTable table = ds.Tables["the"];
            sum = 0;
            ListViewItem it;
            x.Items.Clear();
            foreach (DataRow dr in table.Rows)
            {
                it = x.Items.Add(dr["ma_m"].ToString());
                sum++;
                for (int j = 1; j < table.Columns.Count; j++)
                {
                    it.SubItems.Add(dr[j].ToString());
                }
            }
            ly.Close();
            l.Text = sum.ToString();
        }
        private void load_sachchuamuon(object sender, EventArgs e)
        {
            query = "select ma_s,ten_s,ten_tg from sach where ma_s not in (select ma_s from muon)";
            load_lv(sender, e, listView1, query, label19);
            if (label19.Text == "0")
            {
                comboBox1.Enabled = false;
                comboBox2.Enabled = false;
                button2.Enabled = false;
                textBox7.Enabled = false;
            }
            else
            {
                comboBox1.Enabled = true;
                comboBox2.Enabled = true;
                button2.Enabled = true;
                textBox7.Enabled = true;
            }
        }
        private void load_sachmuon(object sender, EventArgs e)
        {
            query = "select b.ma_s,ten_s,ten_tg, m.ma_m,ngaymuon,hantra from sach b,muon m where b.ma_s=m.ma_s";
            load_lv(sender, e, listView2, query, label22);
        }
        private void load_sachmuon2(object sender, EventArgs e)
        {
            query = "select b.ma_s,ten_s,ten_tg, m.ma_m,ngaymuon,hantra from sach b,muon m where b.ma_s=m.ma_s";
            load_lv(sender, e, listView3, query, label20);

        }
        private void load_toanbosach(object sender, EventArgs e)
        {
            query = "select ma_s,ten_s,ten_tg,gia from sach";
            load_lv(sender, e, listView4, query, label16);
            if (label16.Text == "0")
            {
                comboBox6.Enabled = false;
                button5.Enabled = false;

            }
            else
            {
                comboBox6.Enabled = true;
                button5.Enabled = true;

            }
        }
        private void load_sachquahan(object sender, EventArgs e)
        {
            query = "select b.ma_s,ten_s,ten_tg, m.ma_m,ngaymuon,hantra from sach b,muon m where b.ma_s=m.ma_s and cast(datediff(dd,m.ngaymuon,getdate())as varchar)>m.hantra";
            load_lv(sender, e, listView5, query, label18);
        }
        private void load_the(object sender, EventArgs e)
        {
            string query = "select ma_m,ten_m,ngaysinh,gioitinh,ngaycap from the";
            load_lv1(sender, e, listView6, query, label29);
            if (label29.Text == "0")
            {
                comboBox7.Enabled = false;
                button9.Enabled = false;
            }
            else
            {
                comboBox7.Enabled = true;
                button9.Enabled = true;
            }
        }
        private void load_the2(object sender, EventArgs e)
        {
            string query = "select ma_m,ten_m,ngaysinh,gioitinh,ngaycap from the";
            load_lv1(sender, e, listView7, query, label32);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            load_cmb1(sender, e);
            load_cmb2(sender, e);
            load_cmb3(sender, e);
            load_cmb8(sender, e);
            load_cmb6(sender, e);
            load_cmb7(sender, e);
            load_sachchuamuon(sender, e);
            load_sachmuon(sender, e);
            load_sachmuon2(sender, e);
            load_toanbosach(sender, e);
            load_sachquahan(sender, e);
            load_the(sender, e);
            load_the2(sender, e);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (label39.Text == "0" || label40.Text == "0" || comboBox1.Text.Length < 6 || comboBox2.Text.Length < 4)
            {
                MessageBox.Show("Thong tin khong dung. Xin Kiem tra lai!");
            }
            else
            {
                
                if (!IsNumeric(textBox7.Text) || int.Parse(textBox7.Text) <= 0 || int.Parse(textBox7.Text) > 30)
                {
                    MessageBox.Show("Vui long kiem tra!Han tra tu 1 ngay va khong qua 30 ngay.");
                    textBox7.Clear();
                }
                else
                {

                    SqlConnection ly = new SqlConnection();
                    ly.ConnectionString = "Server=.\\SQLEXPRESS;Database=Library3;Trusted_connection=true";
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandText = "insert into muon values(@mm,@ms,getdate(),@ht)";
                    cmd.Parameters.Add("@ms", SqlDbType.Char);
                    cmd.Parameters.Add("@mm", SqlDbType.Char);
                    cmd.Parameters.Add("@ht", SqlDbType.Int);
                    //--------------------------------------------
                    cmd.Parameters["@ms"].Value = comboBox1.Text;
                    cmd.Parameters["@mm"].Value = comboBox2.Text;
                    cmd.Parameters["@ht"].Value = int.Parse(textBox7.Text);
                    cmd.Connection = ly;
                    ly.Open();
                    int coubt = (int)cmd.ExecuteNonQuery();
                    ly.Close();
                    load_cmb1(sender, e);
                    load_cmb3(sender, e);
                    load_cmb2(sender, e);
                    load_sachchuamuon(sender, e);
                    load_sachmuon(sender, e);
                    load_sachmuon2(sender, e);
                    load_sachquahan(sender, e);
                    load_toanbosach(sender, e);
                }


            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlConnection ly = new SqlConnection();
            ly.ConnectionString = "Server=.\\SQLEXPRESS;Database=Library3;Trusted_connection=true";
            if (label4.Text == "0" || comboBox3.Text == "0" || comboBox3.Text.Length < 4)
            {
                MessageBox.Show("Khong co du lieu hoac du lieu nhap khong dung. Xin kiem tra lai!");
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "delete from muon where ma_m=@mm";
                cmd.Parameters.Add("@mm", SqlDbType.Char);

                cmd.Parameters["@mm"].Value = comboBox3.Text;
                cmd.Connection = ly;
                ly.Open();
                int coubt = (int)cmd.ExecuteNonQuery();
                ly.Close();
                if (label20.Text == "1")
                {
                    comboBox3.Enabled = false;
                    comboBox8.Enabled = false;
                    button3.Enabled = false;
                    button10.Enabled = false;
                }
                load_sachchuamuon(sender, e);
                load_sachmuon(sender, e);
                load_sachmuon2(sender, e);
                load_sachquahan(sender, e);
                load_toanbosach(sender, e);
                load_cmb3(sender, e);
                load_cmb1(sender, e);
                load_cmb2(sender, e);
                load_cmb8(sender, e);

            }
        }
        private void button7_Click(object sender, EventArgs e)
        {
            SqlConnection ly = new SqlConnection();
            ly.ConnectionString = "Server=.\\SQLEXPRESS;Database=Library3;Trusted_connection=true";
            string query = "select ma_s from sach";
            ly.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(query, ly);
            da.Fill(ds, "sach");
            DataTable table = ds.Tables["sach"];
            sum = 100000;
            foreach (DataRow dr in table.Rows)
            {
                sum++;
            }
            if (kt == 1)
            {
                sum++;
                kt = 0;
            }
            sum++;
            ly.Close();
            if (IsNumeric(textBox3.Text) && textBox4.Text != "" && textBox6.Text != "")
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into sach values(@ms,@ts,@ttg,@g)";
                cmd.Parameters.Add("@ms", SqlDbType.Char);
                cmd.Parameters.Add("@ts", SqlDbType.Char);
                cmd.Parameters.Add("@ttg", SqlDbType.Char);
                cmd.Parameters.Add("@g", SqlDbType.Float);

                cmd.Parameters["@ms"].Value = sum.ToString();
                cmd.Parameters["@ts"].Value = textBox4.Text;
                cmd.Parameters["@ttg"].Value = textBox6.Text;
                cmd.Parameters["@g"].Value = float.Parse(textBox3.Text);
                cmd.Connection = ly;
                ly.Open();
                int coubt = (int)cmd.ExecuteNonQuery();
                ly.Close();
                load_sachchuamuon(sender, e);
                load_toanbosach(sender, e);
                load_cmb1(sender, e);
                button8_Click(sender, e);
                load_cmb6(sender, e);
            }
            else
            {
                MessageBox.Show("Xin kiem tra lai thong tin sach moi!");
            }

        }

        private void button8_Click(object sender, EventArgs e)
        {
            textBox3.Text = "";
            textBox4.Text = "";
            textBox6.Text = "";
        }

        private void button5_Click(object sender, EventArgs e)
        {

            if (label16.Text == "0" || comboBox6.Text.Length < 6)
            {
                MessageBox.Show("Khong co du lieu hoac du lieu khong chinh xac. Xin kiem tra lai!");
            }
            else
            {
                SqlConnection ly = new SqlConnection();
                ly.ConnectionString = "Server=.\\SQLEXPRESS;Database=Library3;Trusted_connection=true";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "delete from muon where ma_s=@ms";
                cmd.Parameters.Add("@ms", SqlDbType.Char);
                cmd.Parameters["@ms"].Value = comboBox6.Text;
                cmd.Connection = ly;
                ly.Open();
                int coubt = (int)cmd.ExecuteNonQuery();
                ly.Close();
                cmd.CommandText = "delete from sach where ma_s=@ms1";
                cmd.Parameters.Add("@ms1", SqlDbType.Char);
                cmd.Parameters["@ms1"].Value = comboBox6.Text;
                cmd.Connection = ly;
                ly.Open();
                int count = (int)cmd.ExecuteNonQuery();
                kt = 1;
                ly.Close();
                load_sachchuamuon(sender, e);
                load_sachmuon(sender, e);
                load_sachmuon2(sender, e);
                load_sachquahan(sender, e);
                load_toanbosach(sender, e);
                load_cmb1(sender, e);
                load_cmb3(sender, e);
                load_cmb8(sender, e);
                load_cmb6(sender, e);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            SqlConnection ly = new SqlConnection();
            ly.ConnectionString = "Server=.\\SQLEXPRESS;Database=Library3;Trusted_connection=true";
            string query = "select ma_m from the";
            ly.Open();
            DataSet ds = new DataSet();
            SqlDataAdapter da = new SqlDataAdapter(query, ly);
            da.Fill(ds, "the");
            DataTable table = ds.Tables["the"];
            sum = 2000;
            foreach (DataRow dr in table.Rows)
            {
                sum++;
            }
            if (kt == 1)
            {
                sum++;
                kt = 0;
            }
            sum++;
            if (textBox2.Text != "")
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "insert into the values(@mm,@ht,@ns,@gt,getdate())";
                cmd.Parameters.Add("@mm", SqlDbType.Char);
                cmd.Parameters.Add("@ht", SqlDbType.Char);
                cmd.Parameters.Add("@ns", SqlDbType.DateTime);
                cmd.Parameters.Add("@gt", SqlDbType.Char);

                cmd.Parameters["@mm"].Value = sum.ToString();
                cmd.Parameters["@ht"].Value = textBox2.Text;
                cmd.Parameters["@gt"].Value = comboBox5.Text;
                cmd.Parameters["@ns"].Value = dateTimePicker1.Text;
                cmd.Connection = ly;
                int coubt = (int)cmd.ExecuteNonQuery();
                ly.Close();
                load_the(sender, e);
                load_cmb2(sender, e);
                load_cmb7(sender, e);
                textBox2.Clear();
            }
            else
            {
                MessageBox.Show("Xin nhap day du thong tin the moi!");
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (label29.Text == "0" || comboBox7.Text.Length < 4)
            {
                MessageBox.Show("Khong co du lieu hoac du lieu khong chinh xac. Xin kiem tra lai!");
            }
            else
            {
                SqlConnection ly = new SqlConnection();
                ly.ConnectionString = "Server=.\\SQLEXPRESS;Database=Library3;Trusted_connection=true";
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "delete from the where ma_m=@mm";
                cmd.Parameters.Add("@mm", SqlDbType.Char);
                cmd.Parameters["@mm"].Value = comboBox7.Text;
                cmd.Connection = ly;
                ly.Open();
                int coubt = (int)cmd.ExecuteNonQuery();
                ly.Close();
                kt = 1;
                load_the(sender, e);
                load_cmb2(sender, e);
                load_cmb3(sender, e);
                load_cmb7(sender, e);
                load_the(sender, e);
            }
        }

        private void comboBox1_TextUpdate(object sender, EventArgs e)
        {
            query = "select ma_s,ten_s,ten_tg from sach where ma_s not in (select ma_s from muon) and ma_s Like '" + comboBox1.Text + "%'";
            load_lv(sender, e, listView1, query, label40);

        }

        private void comboBox2_TextUpdate(object sender, EventArgs e)
        {
            query = "select * from the where ma_m Like '" + comboBox2.Text + "%'";
            load_lv1(sender, e, listView7, query, label39);
        }

        private void comboBox3_TextUpdate(object sender, EventArgs e)
        {
            query = "select b.ma_s,ten_s,ten_tg, m.ma_m,ngaymuon,hantra from sach b,muon m where b.ma_s=m.ma_s and m.ma_m Like '" + comboBox3.Text + "%'";
            load_lv(sender, e, listView3, query, label4);


        }

        private void comboBox8_TextUpdate(object sender, EventArgs e)
        {
            string query = "select b.ma_s,ten_s,ten_tg, m.ma_m,ngaymuon,hantra from sach b,muon m where b.ma_s=m.ma_s and b.ma_s Like '" + comboBox8.Text + "%'";
            load_lv(sender, e, listView3, query, label43);
        }

        private void button10_Click(object sender, EventArgs e)
        {
            SqlConnection ly = new SqlConnection();
            ly.ConnectionString = "Server=.\\SQLEXPRESS;Database=Library3;Trusted_connection=true";
            if (label43.Text == "0" || comboBox8.Text == "0" || comboBox8.Text.Length < 4)
            {
                MessageBox.Show("Khong co du lieu hoac du lieu nhap khong dung. Xin kiem tra lai!");
            }
            else
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "delete from muon where ma_s=@ms";
                cmd.Parameters.Add("@ms", SqlDbType.Char);
                cmd.Parameters["@ms"].Value = comboBox8.Text;
                cmd.Connection = ly;
                ly.Open();
                int coubt = (int)cmd.ExecuteNonQuery();
                ly.Close();
                if (label20.Text == "0")
                {
                    comboBox8.Enabled = false;
                    button10.Enabled = false;
                }
                load_sachchuamuon(sender, e);
                load_sachmuon(sender, e);
                load_sachmuon2(sender, e);
                load_sachquahan(sender, e);
                load_toanbosach(sender, e);
                load_cmb1(sender, e);
                load_cmb3(sender, e);
                load_cmb8(sender, e);
                load_cmb6(sender, e);
            }
        }

        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            comboBox1_TextUpdate(sender, e);
        }

        private void comboBox2_TextChanged(object sender, EventArgs e)
        {
            comboBox2_TextUpdate(sender, e);
        }

        private void comboBox6_TextUpdate(object sender, EventArgs e)
        {
            query = "select ma_s,ten_s,ten_tg,gia from sach where ma_s Like '" + comboBox6.Text + "%'";
            load_lv(sender, e, listView4, query, label16);
        }

        private void comboBox6_TextChanged(object sender, EventArgs e)
        {
            comboBox6_TextUpdate(sender, e);
        }

        private void comboBox3_TextChanged(object sender, EventArgs e)
        {
            comboBox3_TextUpdate(sender, e);
        }

        private void comboBox8_TextChanged(object sender, EventArgs e)
        {
            comboBox8_TextUpdate(sender, e);
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                load_cmb3(sender, e);
                comboBox3.Enabled = true;
                button3.Enabled = true;
                load_sachmuon2(sender, e);
                comboBox8.Enabled = false;
                button10.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                load_cmb8(sender, e);
                comboBox3.Enabled = false;
                button3.Enabled = false;
                load_sachmuon2(sender, e);
                comboBox8.Enabled = true;
                button10.Enabled = true;
            }
        }

        private void comboBox7_TextUpdate(object sender, EventArgs e)
        {
            string query = "select ma_m,ten_m,ngaysinh,gioitinh,ngaycap from the where ma_m Like '" + comboBox7.Text + "%'";
            load_lv1(sender, e, listView6, query, label29);
        }

        private void comboBox7_TextChanged(object sender, EventArgs e)
        {
            comboBox7_TextUpdate(sender, e);
        }

        public static System.Boolean IsNumeric(System.Object Expression)
        {
            if (Expression == null || Expression is DateTime)
                return false;

            if (Expression is Int16 || Expression is Int32 || Expression is Int64 || Expression is Decimal || Expression is Single || Expression is Double || Expression is Boolean)
                return true;

            try
            {
                if (Expression is string)
                    Double.Parse(Expression as string);
                else
                    Double.Parse(Expression.ToString());
                return true;
            }
            catch { } // just dismiss errors but return false
            return false;
        }
    }
}
