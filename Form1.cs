using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
//using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WatiN.Core;
using System.Net.Mail;
using System.IO;
using System.Xml;
using System.Data.SqlClient;
//using WindowsFormsControlLibrary1;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Runtime.InteropServices;
using System.Reflection;
//using System.Diagnostics;     // to use Missing.Value

namespace AutoBrowser
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        private IE _IE;
        Class1 cs = new Class1();
        DataTable dt;

        string path = ""; //AutoBrowser.Properties.Settings.Default.path1.ToString();
        //string path = @"d:\\list.txt"; //測試

        string logDir = @"c:\AutoBrowser_log\";
        
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "打卡-" + cs.getVer();
            
            txtID.Text = AutoBrowser.Properties.Settings.Default.USR.ToString();
            txtPWD2.Text = AutoBrowser.Properties.Settings.Default.PWD.ToString();
            txtMAIL.Text = AutoBrowser.Properties.Settings.Default.MAL.ToString();

            //UC1
            //this.userControl1.myClick += new EventHandler(myButtonHandler); 

            load_ini();
            getData_ini();
            getTips();
        }
        
        public Boolean dataCheck()
        {
            if (txtID.Text == "" || txtPWD2.Text == "")
            {
                MessageBox.Show("帳密請勿空白!!");
                return false;
            }
            else
            {
                return true;
            }     
        }

        //打卡
        public void card()
        {
            try
            {
                if (dataCheck() == false)
                {
                    return;
                }
                        
                _IE = new IE();
                _IE.ShowWindow(WatiN.Core.Native.Windows.NativeMethods.WindowShowStyle.Maximize);

                //------------測試網頁--------------
                //_IE.GoTo(@"C:\inetpub\wwwroot\test_err.htm");
                //_IE.GoTo(@"C:\inetpub\wwwroot\test.htm");
                //_IE.TextField(Find.ByName("TextBox1")).TypeText(textBox1.Text);
                //_IE.Button(Find.ByName("Button1")).Click();

                _IE.GoTo("http://signio.bsp/");
                _IE.TextField(Find.ByName("txtUserId")).TypeText(txtID.Text);
                _IE.TextField(Find.ByName("txtPassword")).TypeText(cs.getDES(txtPWD2.Text));
                _IE.Button(Find.ByName("btnSignIn")).Click();

                _IE.WaitForComplete(2);

                if (_IE.Image(Find.ById("imgNo")).Exists)
                {
                    cs.wrLog("LOG失敗", txtID.Text);

                    labMsg.ForeColor = Color.Red;
                    labMsg.Text = DateTime.Now.ToString() + "失敗!";
                }
                else
                {
                    sendMail();                    
                    cs.wrLog("LOG成功", txtID.Text);
                    prScrn();

                    _IE.Close();

                    labMsg.ForeColor = SystemColors.ControlText;
                    labMsg.Text = DateTime.Now.ToString() + "送出!";                    
                }
            }
            catch (Exception ex)
            {
                cs.wrLog(ex.ToString(), txtID.Text);
                //throw;
                return;
            }
        }

        //關機
        public void shutdown()
        {
            if (dataCheck() == false)
            {
                return;
            }
            
            if (labMsg.Text.IndexOf("失敗") == -1)
            {
                System.Diagnostics.Process.Start("C:\\WINDOWS\\system32\\shutdown.exe", "-f -s -t 0");
            }
        }

        //下拉選單_同步檔案
        public string getData()
        {
            string vSTR = "";
           
            //1->2 & 1->3
            if (cs.Connect(@"10.11.34.172\d$", "administrator", "1qaz!QAZ") == true)
            {
                File.Copy(AutoBrowser.Properties.Settings.Default.path1.ToString()
                    , AutoBrowser.Properties.Settings.Default.path2.ToString(), true);

                File.Copy(AutoBrowser.Properties.Settings.Default.path1.ToString()
                    , AutoBrowser.Properties.Settings.Default.path3.ToString(), true);

                vSTR = "(使用遠端1)";
            }
            else
            {
                //1不通,2->3
                if (cs.Connect(@"10.11.22.51\c$", "113720", "113720") == true)
                {
                    File.Copy(AutoBrowser.Properties.Settings.Default.path2.ToString()
                        , AutoBrowser.Properties.Settings.Default.path3.ToString(), true);
                    vSTR = "(使用遠端2)";
                }
                else 
                { 
                    vSTR = "(使用本機)"; 
                }
            }            
            
            return vSTR;
        }
        

        //建立程式執行目錄
        public void load_ini()
        {
            path = logDir;
            String file = @"screen" + DateTime.Now.Date.ToString("yyyyMMdd") + ".jpg";

            //建立目錄
            if (!Directory.Exists(path))
            {
                DirectoryInfo di = Directory.CreateDirectory(path);
            }
        }
        
        //下拉選單_啟動用,抓本機
        public void getData_ini()
        {
            path = AutoBrowser.Properties.Settings.Default.path3.ToString();

            if (File.Exists(path) == true)
            { 
                dt = cs.CsvToDt(path, "DT", ",");

                comboBox1.Items.Clear();
                comboBox1.Text = "請選擇";

                foreach (DataRow od in dt.Rows)
                {
                    comboBox1.Items.Add(od["ID"] + "-" + od["NAME"].ToString());
                }
            }
        }

        //提示
        public void getTips()
        {
            ToolTip tips = new ToolTip();
            //tips.ToolTipTitle = "";
            tips.IsBalloon = true;
            tips.UseAnimation = true;
            tips.UseFading = true;
            tips.AutoPopDelay = 3000;
            //tips.SetToolTip(this.txtPWD2, cs.getDES(txtPWD2.Text.ToString()));           
        }

        //UC1
        //public void myButtonHandler(object sender, EventArgs e)
        //{
        //    UserControl1 uc1 = sender as UserControl1;            
        //    txtID.Text = uc1.getID().ToString();
        //    txtPWD2.Text = uc1.getPWD().ToString();
        //    txtMAIL.Text = uc1.getMail().ToString();
        //}        

        //寄MAIL
        public void sendMail(){
            try
            {
                //------------SMTP--------------
                //MailMessage myMail = new MailMessage();
                //myMail.From = new MailAddress("oasys@sinopac.com"); //發送者 	
                //myMail.To.Add(textBox3.Text); //收件者
                ////myMail.Bcc.Add("456@gmail.com") //隱藏收件者 
                ////myMail.CC.Add("789@gmail.com")  //副本 
                //myMail.SubjectEncoding = Encoding.UTF8; //主題編碼格式 
                //myMail.Subject = "AutoBrowser_LOG"; //主題 
                ////myMail.IsBodyHtml = true; //HTML語法(true:開啟false:關閉) 	
                ////myMail.BodyEncoding = Encoding.UTF8; //內文編碼格式 
                ////myMail.Body = ""; //內文 
                ////Attachment data = new Attachment(@"c:\AutoBrowser_log\screen" + DateTime.Now.Date.ToString("yyyyMMdd") + ".jpg");
                ////myMail.Attachments.Add(data);  //附件 

                //SmtpClient mySmtp = new SmtpClient(); //建立SMTP連線 	
                //mySmtp.Port = 25; //SMTP Port 
                //mySmtp.Host = "10.240.12.20"; //SMTP主機名 	
                //mySmtp.Send(myMail); //發送 	

                //------------改為透過SQL SCRIPT發送--------------
                //SqlConnection sqlConnection1 = new SqlConnection("server=10.11.35.80;uid=113275;pwd=113275;database=lnsys");
                //SqlCommand cmd = new SqlCommand();
                //Int32 rowsAffected;

                //cmd.CommandText = "exec msdb.dbo.sp_send_dbmail " +
                //                                        "@profile_name='SQL JOB'," +
                //                                        "@recipients='" + txtMAIL.Text.ToString() + "'," +
                //                                        "@subject='AutoBrowser_LOG'," +
                //                                        "@body_format='HTML',"+
                //                                        "@body ='' ";
                //cmd.CommandType = CommandType.Text;
                //cmd.Connection = sqlConnection1;

                //sqlConnection1.Open();
                //rowsAffected = cmd.ExecuteNonQuery();
                //sqlConnection1.Close();


                //------------改為透過本機OUTLOOK--------------
                // Create the Outlook application.
                Outlook.Application oApp = new Outlook.Application();

                // Get the NameSpace and Logon information.
                Outlook.NameSpace oNS = oApp.GetNamespace("mapi");

                // Log on by using a dialog box to choose the profile.
                //oNS.Logon(Missing.Value, Missing.Value, true, true);

                // Alternate logon method that uses a specific profile.
                // TODO: If you use this logon method, 
                //  change the profile name to an appropriate value.
                oNS.Logon("outlook", Missing.Value, false, true); 

                // Create a new mail item.
                //Outlook.MailItem oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);
                Outlook._MailItem oMsg = (Outlook.MailItem)oApp.CreateItem(Outlook.OlItemType.olMailItem);

                // Set the subject.
                oMsg.Subject = "AutoBrowser_LOG";

                // Set HTMLBody.
                String sHtml;
                sHtml = "";
                oMsg.HTMLBody = sHtml;

                // Add a recipient.
                Outlook.Recipients oRecips = (Outlook.Recipients)oMsg.Recipients;
                // TODO: Change the recipient in the next line if necessary.
                Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add(txtMAIL.Text.ToString());
                oRecip.Resolve();

                // Send.
                oMsg.Send();
                
                // Log off.
                oNS.Logoff();

                // Clean up.
                oRecip = null;
                oRecips = null;
                oMsg = null;
                oNS = null;
                oApp = null;

            }
            catch (Exception ex)
            {
                cs.wrLog(ex.ToString(), txtID.Text);
                //throw;
            }
        }

        //截畫面
        public void prScrn(){
            try
            {
                path = logDir;
                String file = @"screen" + DateTime.Now.Date.ToString("yyyyMMdd") + ".jpg";

                Bitmap myImage = new Bitmap(1920, 1080);
                Graphics g = Graphics.FromImage(myImage);
                g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(1920, 1080));
                myImage.Save(path+file);
            }
            catch (Exception ex)
            {
                cs.wrLog(ex.ToString(), txtID.Text);
                //throw;
            }
        }

        #region BUTTON
        private void btnCARD_Click(object sender, EventArgs e)
        {
            card();
        }

        //設定登入帳密
        private void btnSET_Click(object sender, EventArgs e)
        {
            AutoBrowser.Properties.Settings.Default.USR = txtID.Text.ToString();
            AutoBrowser.Properties.Settings.Default.PWD = txtPWD2.Text.ToString();
            AutoBrowser.Properties.Settings.Default.MAL = txtMAIL.Text.ToString();
            if (dateTimePicker1.Checked == true)
            {
                AutoBrowser.Properties.Settings.Default.TIME = dateTimePicker1.Text;
            }
            AutoBrowser.Properties.Settings.Default.Save();

            MessageBox.Show("帳號、密碼、MAIL、排程時間預設值設定完成!!");
        }

        private void btnSHUTDOWN_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("OS會立即關機，請再次確認!!", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                card();
                shutdown();
            }
        }

        private void btnLIST_Click(object sender, EventArgs e)
        {
            getData();            
            Form2 frm = new Form2();
            frm.ShowDialog(this);
            frm.Dispose();            
        }

        private void btnSHUTDOWN2_Click(object sender, EventArgs e)
        {
            DateTime dttx1 = DateTime.Parse(DateTime.Now.ToLongTimeString());
            DateTime dttx2 = DateTime.Parse(dateTimePicker1.Text);

            TimeSpan ts = dttx2 - dttx1;
            Int16 s = Convert.ToInt16(ts.TotalSeconds);

            if (ts.TotalSeconds <= 0)
            {
                MessageBox.Show("關機時間不得小於現在時間!!");
            }
            else
            {
                label4.Text = "設定完成!!";
                timer1.Enabled = true;
            }
        }
        #endregion
        
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtID.Text = dt.Rows[comboBox1.SelectedIndex]["ID"].ToString();
            txtPWD2.Text = dt.Rows[comboBox1.SelectedIndex]["PWD"].ToString();
            txtMAIL.Text = dt.Rows[comboBox1.SelectedIndex]["MAIL"].ToString();

            getTips();
        }

        private void txtPWD1_Leave(object sender, EventArgs e)
        {
            if (txtPWD1.Text != "")
            {
                txtPWD2.Text = cs.setDES(txtPWD1.Text);
            }
        }
        
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Checked == true)
            {
                btnSHUTDOWN2.Enabled = true;
                label4.Text = " ";
            }
            else {
                btnSHUTDOWN2.Enabled = false;
                if (label4.Text != " ")
                {
                    label4.Text = "設定取消!!";
                }                
                timer1.Enabled = false;
            }
        }

        private void btnSHUTDOWN2_EnabledChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Checked == true)
            {
                
                if (AutoBrowser.Properties.Settings.Default.TIME != "")
                {
                    dateTimePicker1.Text = AutoBrowser.Properties.Settings.Default.TIME.ToString();
                }
                else 
                {
                    dateTimePicker1.Text = "18:00"; //預設值為下班時間
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
             string time = DateTime.Now.ToShortTimeString();
             if (dateTimePicker1.Value.ToShortTimeString() == time)
             {
                 timer1.Enabled = false;
                 card();
                 shutdown();
             }             
        }

        private void comboBox1_Click(object sender, EventArgs e)
        {
            if (this.Text.IndexOf("使用") == -1)
            {             
                //觸發非同步(COPY遠端檔案到本機)
                IAsyncResult asyncResult = BeginCalculate();
                string result = EndCalculate(asyncResult);
                this.Text += result;
                getData_ini();
            }            
        }

        #region 非同步
        public Func<string> m_calculateDelegate;

        public IAsyncResult BeginCalculate()
        {
            this.m_calculateDelegate = getData;
            return this.m_calculateDelegate.BeginInvoke(null,null);
        }

        public string EndCalculate(IAsyncResult asyncResult)
        {
            return this.m_calculateDelegate.EndInvoke(asyncResult);
        }
        #endregion

        private void btnSYNC_Click(object sender, EventArgs e)
        {
            string vStr = getData();

            if (vStr != "")
            { 
                MessageBox.Show(vStr + "同步資料完成");
            }
        }

    }
}
