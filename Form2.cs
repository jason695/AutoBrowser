using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;


namespace AutoBrowser
{
    public partial class Form2 : Form
    {
        Class1 cs = new Class1();
        //Form1 frm1 = new Form1();
        DataTable dt;
        string path = AutoBrowser.Properties.Settings.Default.path_local.ToString();
        int curRow; //修改哪筆資料
                
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            dt = cs.CsvToDt(path, "tmp", ",");
            dataGridView1.DataSource = dt;

            dataGridView1.Columns["PWD"].ReadOnly = true;
            dataGridView1.Columns["PWD"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; 
            dataGridView1.Columns["MAIL"].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; 
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == -1)
            {
                curRow = this.dataGridView1.CurrentCell.RowIndex;
                btnDEL.Enabled = true;
                labID.Text = "NONE";
                btnOK.Enabled = false;
                btnCLS.Enabled = false;
            }            
            
            if (e.ColumnIndex > -1)
            {
                if (this.dataGridView1[e.ColumnIndex, this.dataGridView1.CurrentCell.RowIndex].OwningColumn.HeaderText.ToString() == "PWD")
                {
                    curRow = this.dataGridView1.CurrentCell.RowIndex;
                    labID.Text = this.dataGridView1["ID", this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
                    txtPWD1.Text = "";
                    txtPWD2.Text = "";
                    btnOK.Enabled = true;
                    btnCLS.Enabled = true;
                }
                else
                {
                    labID.Text = "NONE";
                    btnOK.Enabled = false;
                    btnCLS.Enabled = false;
                }
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            //修改
            dt.Rows[e.RowIndex][e.ColumnIndex] = this.dataGridView1[e.ColumnIndex, this.dataGridView1.CurrentCell.RowIndex].Value.ToString();
            dt.AcceptChanges();
            cs.DtToCsv(dt, path);

            //COPY TXT
            //File.Copy(path, AutoBrowser.Properties.Settings.Default.path1.ToString(), true);
            //File.Copy(path, AutoBrowser.Properties.Settings.Default.path_local.ToString(), true);            

            //上送DB
            cs.mongo_upload();
        }

        private void txtID_TextChanged(object sender, EventArgs e)
        {
            if (txtID.Text != "")
            {
                btnNEW.Enabled = true;
                btnCLS2.Enabled = true;
            }
            else
            {
                btnNEW.Enabled = false;
                btnCLS2.Enabled = false;
            }

        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (txtPWD1.Text == "" || txtPWD2.Text =="")
            {
                MessageBox.Show("請勿輸入空白!!");
            }
            else if (txtPWD1.Text == txtPWD2.Text)
            {
                    dt.Rows[curRow]["PWD"] = cs.setDES(txtPWD1.Text);
                    dt.AcceptChanges();
                    cs.DtToCsv(dt, path);

                    MessageBox.Show("修改完成!!");
            }
            else 
            {
                MessageBox.Show("請確認二次密碼輸入相同!!");
            }

            //上送DB
            cs.mongo_upload();
        }

        private void btnNEW_Click(object sender, EventArgs e)
        {
            DataRow workRow = dt.NewRow();
            workRow[0] = txtID.Text;
            workRow[1] = "";
            workRow[2] = txtMAIL.Text;
            workRow[3] = txtNAME.Text;
            dt.Rows.Add(workRow);
            cs.DtToCsv(dt, path);

            txtID.Text = "";
            txtMAIL.Text = "";
            txtNAME.Text = "";
            
            //上送DB
            cs.mongo_upload();
        }

        private void btnDEL_Click(object sender, EventArgs e)
        {
            //刪除
            dataGridView1.Rows.Remove(this.dataGridView1.Rows[curRow]); 
            dt.AcceptChanges();
            cs.DtToCsv(dt, path);

            //上送DB
            cs.mongo_upload();
        }
        
        private void btnCLS_Click(object sender, EventArgs e)
        {
            txtPWD1.Text = "";
            txtPWD2.Text = "";
        }

        private void btnCLS2_Click(object sender, EventArgs e)
        {
            txtID.Text = "";
            txtMAIL.Text = "";
            txtNAME.Text = "";
        }
        
    }
}
