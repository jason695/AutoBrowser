using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WatiN.Core;
using WatiN.Core.DialogHandlers;
using System.IO;
using Outlook = Microsoft.Office.Interop.Outlook;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Net;
using Newtonsoft.Json;
using System.Collections.Specialized;

//using System.Data.SqlClient; // 改用MONGO
//using System.Xml;
//using System.Net.Mail; // 改用OUTLOOK
//using System.Diagnostics;     // to use Missing.Value

namespace AutoBrowser
{
    public partial class Form1 : System.Windows.Forms.Form
    {
        private IE _IE;
        Class1 cs = new Class1();
        DataTable dt;

        string txt_path = AutoBrowser.Properties.Settings.Default.path_local.ToString(); //list_p.txt
        string log_path = AutoBrowser.Properties.Settings.Default.logDir.ToString(); //截圖+LOG用

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.Text = "打卡-" + cs.getVer();

            getPARM();
            load_ini();
            getData_ini();
            getTips();

            

            backgroundWorker1.RunWorkerAsync(); 
        }

        #region ---SUB/FUN---
        //打卡主程式
        public void card()
        {
            string msg = "";
            
            try
            {
                if (dataCheck() == false)
                {
                    return;
                }

                _IE = new IE();
                _IE.ShowWindow(WatiN.Core.Native.Windows.NativeMethods.WindowShowStyle.Maximize);

                _IE.GoTo(@"http://sinocloud.sph/Login.aspx");
                _IE.TextField(Find.ById("txtUserID_txtData")).TypeText(txtID.Text);
                _IE.TextField(Find.ById("txtPassword_txtData")).TypeText(cs.getDES(txtPWD2.Text));
                _IE.Button(Find.ByName("btnLogin")).ClickNoWait();

                System.Threading.Thread.Sleep(1500); //等待程式執行完成

                //跑過ALERT--start-- (REF: https://blog.csdn.net/iteye_982/article/details/81574936)
                AlertDialogHandler AlertDialog = new AlertDialogHandler();
               
                //Code for checking if the case number is invalid and a dialog box appears.
                Settings.AutoStartDialogWatcher = true;
                Settings.AutoCloseDialogs = true;
                _IE.AddDialogHandler(AlertDialog);
              
                //If alert dialog is found then close the dialog and log error
                if (AlertDialog.Exists())
                {
                    AlertDialog.WaitUntilExists();
                    //Click OK button of the dialog so that dialog gets closed
                    AlertDialog.OKButton.Click();
                    _IE.WaitForComplete();
                    _IE.RemoveDialogHandler(AlertDialog);
                }
                //跑過ALERT --end--

                if (_IE.Title != "Home")
                {
                    msg = "登入失敗";
                    
                    cs.wrLog(msg, txtID.Text);
                    msgBar(msg, 1);
                    sendLine(labTOKEN.Text.ToString(), txtID.Text.ToString() + "-" + labNAME.Text.ToString() + msg);
                }
                else {
                    //登入成功 
                    _IE.GoTo(@"http://sinocloud.sph/Main.aspx");
                    _IE.Link(Find.ByText(new System.Text.RegularExpressions.Regex("打卡"))).Click();
      
                    IE _NEWIE = IE.AttachTo<IE>(Find.ByTitle(new System.Text.RegularExpressions.Regex("打卡")));

                    //if (_NEWIE.Span(Find.ById("ResultMsg")).Text == "打卡成功")
                    if (_NEWIE.Title == "打卡完成")
                    {
                        msg = "打卡成功";

                        cs.wrLog(msg, txtID.Text);
                        msgBar(msg);
                        sendLine(labTOKEN.Text.ToString(), txtID.Text.ToString() + "-" + labNAME.Text.ToString() + msg);
                        _IE.GoTo(@"http://sinocloud.sph/Login.aspx"); //登出   
                    }
                    else if (_NEWIE.Title == "打卡異常")
                    {
                        //異常處理(超時or提前15分),預設選下拉選單的第一個
                        _NEWIE.SelectList(Find.ById("ddlAbnormalReason")).SelectByValue("11");

                        //CLICK"確定"
                        _NEWIE.Button(Find.ById("btnDefine")).Click();

                        IE _NEWIE1 = IE.AttachTo<IE>(Find.ByTitle(new System.Text.RegularExpressions.Regex("打卡")));

                        if (_NEWIE1.Title == "打卡完成")
                        {
                            msg = "打卡(超過時間)成功";

                            cs.wrLog(msg, txtID.Text);
                            msgBar(msg);
                            sendLine(labTOKEN.Text.ToString(), txtID.Text.ToString() + "-" + labNAME.Text.ToString() + msg);
                        }
                        else
                        {
                            msg = "打卡異常";

                            cs.wrLog(msg, txtID.Text);
                            msgBar(msg,1);
                            sendLine(labTOKEN.Text.ToString(), txtID.Text.ToString() + "-" + labNAME.Text.ToString() + "打卡異常[" + _NEWIE.Span(Find.ById("ShowMsg")).ToString() + "],請人工處理!!");
                        }                        
                    }
                    else
                    {
                        msg = "打卡失敗";

                        cs.wrLog(msg, txtID.Text);
                        msgBar(msg, 1);
                        sendLine(labTOKEN.Text.ToString(), txtID.Text.ToString() + "-" + labNAME.Text.ToString() + msg +"(請查明原因)");
                    }
                }

                _IE.Close();

                //System.Threading.Thread.Sleep(1000); //等待程式執行完成
                prScrn();  
                
                //////
                //IE ie = new IE("http://localhost/Test/");
                //点击按钮，打开新窗口test2
                //ie.Button(Find.ById("Button1")).Click();
                //查找新窗口test2并赋给新的IE对象
                //IE newie = IE.AttachTo<IE>(Find.ByTitle("test2"));
                //使用新的IE对象就可以继续对新窗口进行操作了
                //newie.TextField(Find.ById("Text1")).TypeText("this is new ie");
                //////                
            }
            catch (Exception ex)
            {
                cs.wrLog(ex.ToString(), txtID.Text);
                //throw;
                return;
            }
        }

        //欄位CHECK
        public Boolean dataCheck()
        {
            if (txtID.Text == "" || txtPWD2.Text == "")
            {
                msgBar("帳密請勿空白!!", 2);

                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// 關機
        /// </summary>
        /// <param name="flg">true:強制; false:有失敗則不關機</param>
        public void shutdown(Boolean flg = false)
        {
            if (dataCheck() == false)
            {
                return;
            }

            if (flg == false)
            {
                if (lstMsg.SelectedItem.ToString().IndexOf("失敗") > 0)
                {
                    return;
                }
            }   

            sendLine(labTOKEN.Text.ToString(), txtID.Text.ToString() + "-" + labNAME.Text.ToString() + "電腦關機!!");
            System.Diagnostics.Process.Start("C:\\WINDOWS\\system32\\shutdown.exe", "-f -s -t 0");            
        }
        
        //程式初始化
        public void load_ini()
        {
            //建立目錄
            if (!Directory.Exists(log_path))
            {
                DirectoryInfo di = Directory.CreateDirectory(log_path);
            }
        }
        
        //下拉選單_啟動用,抓本機
        public void getData_ini()
        {
            if (File.Exists(txt_path) == true)
            {
                dt = cs.CsvToDt(txt_path, "DT", ",");

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

        //寄MAIL
        public void sendMail(string email,string msg){
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
                oMsg.Subject = "AutoBrowser";

                // Set HTMLBody.
                //String sHtml;
                //sHtml = "";
                //oMsg.HTMLBody = sHtml;
                oMsg.HTMLBody = msg;

                // Add a recipient.
                Outlook.Recipients oRecips = (Outlook.Recipients)oMsg.Recipients;
                // TODO: Change the recipient in the next line if necessary.
                //Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add(txtMAIL.Text.ToString());
                Outlook.Recipient oRecip = (Outlook.Recipient)oRecips.Add(email.ToString());
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

        //執行打卡,呼叫WEB SERVICE,送訊息到LINE NOTIFY
        //-> 改寫為WEB API
        public void sendLine(string token,string msg)
        {
            if (token != "")
            {
                //string result = null;

                try
                {
                    //***web service***
                    //ServiceReference1.WSSoapClient ws = new ServiceReference1.WSSoapClient();
                    //result = ws.Send(token, msg);

                    //***script.google WebClient***
                    var wb = new WebClient();
                    ServicePointManager.SecurityProtocol =
                        SecurityProtocolType.Ssl3 | SecurityProtocolType.Tls |
                        SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12;
                    
                    var data = new NameValueCollection();
                    string url = "https://script.google.com/macros/s/AKfycbzeXyf9uZiqqVVCeNny8q9sGdEbrBlJLHo_LWw_hu5MwyCQt-1ADM0KcNJyR32Lppk/exec";
                    data["msg"] = msg;
                    data["token"] = token;
                    var response = wb.UploadValues(url, "POST", data);
                }
                catch (WebException ex)
                {
                    //throw;
                    sendMail(txtMAIL.Text.ToString(),"LINE通知功能異常,改用EMAIL通知");                    
                }                   
            }
        }

        //截畫面
        public void prScrn(){
            try
            {
                //path = logDir;
                String file = @"screen" + DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + txtID.Text.ToString() + ".jpg";

                Bitmap myImage = new Bitmap(1920, 1080);
                Graphics g = Graphics.FromImage(myImage);
                g.CopyFromScreen(new Point(0, 0), new Point(0, 0), new Size(1920, 1080));
                myImage.Save(log_path+file);
            }
            catch (Exception ex)
            {
                cs.wrLog(ex.ToString(), txtID.Text);
                //throw;
            }
        }

        /// <summary>
        /// 訊息欄控制
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="clr">0:正常,1:異常,2:彈跳訊息</param>
        public void msgBar(string msg, int clr = 0)
        {
            try
            {
                if (clr == 0)
                {
                    lstMsg.Items.Add(DateTime.Now.ToString() + "," + msg);    
                }
                else if (clr == 1) {
                    lstMsg.Items.Add("[ERROR]" + DateTime.Now.ToString() + "," + msg);
                }
                else if (clr == 2)
                {
                    lstMsg.Items.Add("[ALERT]" + DateTime.Now.ToString() + "," + msg);
                    MessageBox.Show(msg,"ALERT");
                }
                
                lstMsg.SelectedIndex = lstMsg.Items.Count - 1;
            }
            catch (Exception ex)
            {
                cs.wrLog(ex.ToString(), txtID.Text);
                //throw;
            }
        }

        //設定預設值
        public void setPARM() {
            AutoBrowser.Properties.Settings.Default.USR = txtID.Text.ToString();
            AutoBrowser.Properties.Settings.Default.PWD = txtPWD2.Text.ToString();
            AutoBrowser.Properties.Settings.Default.MAL = txtMAIL.Text.ToString();
            if (dateTimePicker1.Checked == true)
            {
                AutoBrowser.Properties.Settings.Default.TIME = dateTimePicker1.Text;
            }
            AutoBrowser.Properties.Settings.Default.TOKEN = labTOKEN.Text.ToString();
            AutoBrowser.Properties.Settings.Default.NAME = labNAME.Text.ToString();
            AutoBrowser.Properties.Settings.Default.Save();
        }

        //取得預設值
        public void getPARM()
        {
            txtID.Text = AutoBrowser.Properties.Settings.Default.USR.ToString();
            txtPWD2.Text = AutoBrowser.Properties.Settings.Default.PWD.ToString();
            txtMAIL.Text = AutoBrowser.Properties.Settings.Default.MAL.ToString();
            labTOKEN.Text = AutoBrowser.Properties.Settings.Default.TOKEN.ToString();
            labNAME.Text = AutoBrowser.Properties.Settings.Default.NAME.ToString();
        }
        #endregion

        #region ---控制項---
        private void btnCARD_Click(object sender, EventArgs e)
        {
            card();            
        }

        //設定登入帳密
        private void btnSET_Click(object sender, EventArgs e)
        {
            setPARM();
            msgBar("本機帳號、密碼、MAIL、排程時間預設值設定完成!!",2);
        }

        //立即關機
        private void btnSHUTDOWN_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("OS會立即關機，請再次確認!!", "Confirm Message", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                card();
                shutdown();
            }
        }

        //名單維護
        private void btnForm2_Click(object sender, EventArgs e)
        {
            Form2 frm = new Form2();
            frm.ShowDialog(this);
            frm.Dispose();

            //接收form2修改後的資料
            txtPWD2.Text = AutoBrowser.Properties.Settings.Default.PWD;
        }

        //排程打卡
        private void btnSHUTDOWN2_Click(object sender, EventArgs e)
        {
            DateTime dttx1 = DateTime.Parse(DateTime.Now.ToLongTimeString());
            DateTime dttx2 = DateTime.Parse(dateTimePicker1.Text);

            TimeSpan ts = dttx2 - dttx1;
            Int16 s = Convert.ToInt16(ts.TotalSeconds);

            if (ts.TotalSeconds <= 0)
            {
                msgBar("關機時間不得小於現在時間!!",2);
            }
            else
            {
                if (chkSHUTDOWN.Checked == true)
                {
                    msgBar(btnSHUTDOWN2.Text.ToString() + "+" + chkSHUTDOWN.Text.ToString() +"設定完成!!");
                } else {
                    msgBar(btnSHUTDOWN2.Text.ToString() + "設定完成!!");
                }
                
                timer1.Enabled = true;
            }
        }
        
        //同步MONGO的資料下來
        private void btnSYNC_Click(object sender, EventArgs e)
        {
            if (cs.mongo_sync()==true)
            {
                msgBar("名單已同步完成!!");
            }
               
        }

        //下拉選單
        private void comboBox1_Click(object sender, EventArgs e)
        {
            getData_ini();
        }

        //下拉選單取得帳密
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //接續getData_ini後的動作
            txtID.Text = dt.Rows[comboBox1.SelectedIndex]["ID"].ToString();
            txtPWD2.Text = dt.Rows[comboBox1.SelectedIndex]["PWD"].ToString();
            txtMAIL.Text = dt.Rows[comboBox1.SelectedIndex]["MAIL"].ToString();
            labTOKEN.Text = dt.Rows[comboBox1.SelectedIndex]["TOKEN"].ToString();
            labNAME.Text = dt.Rows[comboBox1.SelectedIndex]["NAME"].ToString();

            getTips();
        }

        //加密
        private void txtPWD1_Leave(object sender, EventArgs e)
        {
            if (txtPWD1.Text != "")
            {
                txtPWD2.Text = cs.setDES(txtPWD1.Text);
            }
        }
        #endregion
        
        #region ---排程關機---
        private void dateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            if (dateTimePicker1.Checked == true)
            {
                btnSHUTDOWN2.Enabled = true;               
            }
            else
            {
                btnSHUTDOWN2.Enabled = false;                
                msgBar("排程設定取消!!");                
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
                if (chkSHUTDOWN.Checked == true)
                {
                    shutdown(true); //強制關機,不管有沒有打成功
                }                
            }
        }
        #endregion

        #region ---APP_PUSH---
        string app_push(BackgroundWorker worker, DoWorkEventArgs e)
        {
            System.Threading.Thread.Sleep(6000);    //等待時間，不要馬上執行
            
            string result = cs.mongo_read();
            return result;
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            e.Result = app_push(worker, e);
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
           
            if (e.Error != null)
            {
                MessageBox.Show(e.Error.Message); 
            }
            else
            {
                DateTime dt = File.GetLastWriteTime(txt_path);
                //MessageBox.Show(e.Result.ToString() + Environment.NewLine + dt.ToString(), "TopMostMessageBox");
                
                if (Convert.ToDateTime(e.Result.ToString()) > dt) //本機文字檔有更新
                {
                    string str = "來源名單有更新，請同步!!";                    
                    //MessageBox.Show(str, "TopMostMessageBox");
                    msgBar(str, 2);
                }                
            }
        }
        #endregion

        #region ---打卡_舊版---
        //public void card_OLD()
        //{
        //    try
        //    {
        //        if (dataCheck() == false)
        //        {
        //            return;
        //        }

        //        _IE = new IE();
        //        _IE.ShowWindow(WatiN.Core.Native.Windows.NativeMethods.WindowShowStyle.Maximize);

        //        //------------測試網頁--------------
        //        //_IE.GoTo(@"C:\inetpub\wwwroot\test_err.htm");
        //        //_IE.GoTo(@"C:\inetpub\wwwroot\test.htm");
        //        //_IE.TextField(Find.ByName("TextBox1")).TypeText(textBox1.Text);
        //        //_IE.Button(Find.ByName("Button1")).Click();

        //        _IE.GoTo("http://signio.bsp/");
        //        _IE.TextField(Find.ByName("txtUserId")).TypeText(txtID.Text);
        //        _IE.TextField(Find.ByName("txtPassword")).TypeText(cs.getDES(txtPWD2.Text));
        //        _IE.Button(Find.ByName("btnSignIn")).Click();

        //        _IE.WaitForComplete(2);

        //        if (_IE.Image(Find.ById("imgNo")).Exists)
        //        {
        //            cs.wrLog("LOG失敗", txtID.Text);

        //            labMsg.ForeColor = Color.Red;
        //            labMsg.Text = DateTime.Now.ToString() + "失敗!";
        //        }
        //        else
        //        {
        //            //sendMail();   //OULOOK會擋呼叫寄信功能,先移除"排程"和"關機"
        //            cs.wrLog("LOG成功", txtID.Text);
        //            prScrn();

        //            _IE.Close();

        //            labMsg.ForeColor = SystemColors.ControlText;
        //            labMsg.Text = DateTime.Now.ToString() + "送出!";                    
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        cs.wrLog(ex.ToString(), txtID.Text);
        //        //throw;
        //        return;
        //    }
        //}

        //人工同步
        //private void btnSYNC_Click(object sender, EventArgs e)
        //{
        //    string str = "";

        //    try
        //    {
        //        if (cs.Connect(@"10.11.34.172\c$", "administrator", "1qaz!QAZ") == true)  //遠端1
        //        {
        //            if (cs.Connect(@"10.11.22.51\d$", "113720", "113720") == true) //遠端2
        //            {
        //                File.Copy(AutoBrowser.Properties.Settings.Default.path2.ToString()
        //                         , AutoBrowser.Properties.Settings.Default.path1.ToString(), true);

        //                str += "同步遠端1->2完成" + Environment.NewLine;
        //            }
        //            else {
        //                str += "遠端2連線失敗" + Environment.NewLine;                    
        //            }

        //            File.Copy(AutoBrowser.Properties.Settings.Default.path2.ToString()
        //                    , AutoBrowser.Properties.Settings.Default.path_local.ToString(), true);

        //            str += "同步遠端1->本機完成" + Environment.NewLine;
        //        }
        //        else if (cs.Connect(@"10.11.22.51\d$", "113720", "113720") == true) //遠端2
        //        {
        //            File.Copy(AutoBrowser.Properties.Settings.Default.path1.ToString()
        //                    , AutoBrowser.Properties.Settings.Default.path_local.ToString(), true);

        //            str += "遠端1連線失敗" + Environment.NewLine;
        //            str += "同步遠端2->本機完成";
        //        }
        //        else
        //        {
        //            str += "遠端1連線失敗" + Environment.NewLine;
        //            str += "遠端2連線失敗" + Environment.NewLine;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        cs.wrLog(ex.ToString(), txtID.Text);
        //        //throw;
        //    }
        //    finally
        //    {
        //        MessageBox.Show(str);
        //    }
        //}
        #endregion
                    
        private void btnLINE_Click(object sender, EventArgs e)
        {
            sendLine(labTOKEN.Text.ToString(), "TEST");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("https://github.com/jason695/AutoBrowser#readme");
        }
    }

    public class PostData
    {
        public string MSG { get; set; }
        public string TOKEN { get; set; }
    }
      
}
