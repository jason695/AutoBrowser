using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;

namespace AutoBrowser
{
    public partial class Form3 : Form
    {
        Class1 cs = new Class1();
        Form1 frm1 = new Form1();
        DataTable dt;
        List<string> list = new List<string>();
        
        string path = AutoBrowser.Properties.Settings.Default.path_local.ToString();
        
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            txtMSG.Text = @"["+ DateTime.Today.ToString("yyyy-MM-dd") +"]" + Environment.NewLine + "已提供新版本下載(" + cs.getVer().ToString() + @")，路徑:\\10.11.36.201\e$\Howard\exe\";            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            dt = cs.CsvToDt(path, "tmp", ",");
                    
            foreach (DataRow row in dt.Rows){
                if (!list.Contains(row["TOKEN"].ToString()))
                {
                    list.Add(row["TOKEN"].ToString());
                    frm1.sendLine(row["TOKEN"].ToString(),txtMSG.Text.ToString());
                }   
            }
            MessageBox.Show("LINE推播完成");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dt = cs.CsvToDt(path, "tmp", ",");

            foreach (DataRow row in dt.Rows)
            {
                if (!list.Contains(row["MAIL"].ToString()))
                {
                    list.Add(row["MAIL"].ToString());
                    frm1.sendMail(row["MAIL"].ToString(), txtMSG.Text.ToString());
                }
            }
            MessageBox.Show("MAIL推播完成");
        }        
    }
}
