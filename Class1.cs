using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Data;

namespace AutoBrowser
{
    class Class1
    {
        #region 加解密
        string key = "abcdefgh";
        string iv = "12345678";
        
        //加密
        public string setDES(string original)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = Encoding.ASCII.GetBytes(key);
            des.IV = Encoding.ASCII.GetBytes(iv);
            byte[] s = Encoding.ASCII.GetBytes(original);
            ICryptoTransform desencrypt = des.CreateEncryptor();
            return BitConverter.ToString(desencrypt.TransformFinalBlock(s, 0, s.Length)).Replace("-", string.Empty);
        }

        //解密
        public string getDES(string hexString)
        {
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            des.Key = Encoding.ASCII.GetBytes(key);
            des.IV = Encoding.ASCII.GetBytes(iv);
            byte[] s = new byte[hexString.Length / 2];
            int j = 0;
            for (int i = 0; i < hexString.Length / 2; i++)
            {
                s[i] = Byte.Parse(hexString[j].ToString() + hexString[j + 1].ToString(), System.Globalization.NumberStyles.HexNumber);
                j += 2;
            }
            ICryptoTransform desencrypt = des.CreateDecryptor();
            return Encoding.ASCII.GetString(desencrypt.TransformFinalBlock(s, 0, s.Length));
        }
        #endregion

        #region NET USE
        public bool Connect(string remoteHost, string userName, string passWord)
        {
            bool Flag = true;
            Process proc = new Process();
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;

            try
            {
                proc.Start();
                string command;
                command = @"net use \\" + remoteHost + " " + passWord + " /user:" + userName + ">NUL";
                proc.StandardInput.WriteLine(command);
                command = "exit";
                proc.StandardInput.WriteLine(command);

                while (proc.HasExited == false)
                {
                    proc.WaitForExit(1000);
                }

                string errormsg = proc.StandardError.ReadToEnd();
                if (errormsg != "")
                { 
                    Flag = false;
                    DisConnect(remoteHost);
                    wrLog("[ERR:"+errormsg.ToString()+"]", "Connect");
                }
                proc.StandardError.Close();
            }
            catch (Exception ex)
            {
                Flag = false;
                wrLog(ex.ToString(), "Connect");
                throw ex;
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }

        public bool DisConnect(string remoteHost)
        {

            bool Flag = true;
            Process proc = new Process();
            proc.StartInfo.FileName = "cmd.exe";
            proc.StartInfo.UseShellExecute = false;
            proc.StartInfo.RedirectStandardInput = true;
            proc.StartInfo.RedirectStandardOutput = true;
            proc.StartInfo.RedirectStandardError = true;
            proc.StartInfo.CreateNoWindow = true;

            try
            {
                proc.Start();
                string command = @"net use \\" + remoteHost + " /delete";
                proc.StandardInput.WriteLine(command);
                command = "exit";
                proc.StandardInput.WriteLine(command);

                while (proc.HasExited == false)
                {
                    proc.WaitForExit(1000);
                }

                string errormsg = proc.StandardError.ReadToEnd();
                if (errormsg != "")
                    Flag = false;

                proc.StandardError.Close();
            }
            catch (Exception ex)
            {
                Flag = false;
                wrLog(ex.ToString(), "DisConnect");
                throw ex;
            }
            finally
            {
                proc.Close();
                proc.Dispose();
            }
            return Flag;
        }
        #endregion

        #region 轉換
        //CSV TO DT
        public DataTable CsvToDt(string FilePath, string TableName, string delimiter)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            
            try
            {   
                StreamReader s = new StreamReader(FilePath, System.Text.Encoding.Default);
                //string ss = s.ReadLine();//skip the first line
                string[] columns = s.ReadLine().Split(delimiter.ToCharArray());
                ds.Tables.Add(TableName);
                foreach (string col in columns)
                {
                    bool added = false;
                    string next = "";
                    int i = 0;
                    while (!added)
                    {
                        string columnname = col + next;
                        columnname = columnname.Replace("#", "");
                        columnname = columnname.Replace("'", "");
                        columnname = columnname.Replace("&", "");

                        if (!ds.Tables[TableName].Columns.Contains(columnname))
                        {
                            ds.Tables[TableName].Columns.Add(columnname.ToUpper());
                            added = true;
                        }
                        else
                        {
                            i++;
                            next = "_" + i.ToString();
                        }
                    }
                }

                string AllData = s.ReadToEnd();
                string[] rows = AllData.Split("\n".ToCharArray());
                Array.Resize(ref rows, rows.GetUpperBound(0)); //移掉最後一筆

                foreach (string r in rows)
                {
                    string[] items = r.Split(delimiter.ToCharArray());
                    ds.Tables[TableName].Rows.Add(items);
                }

                s.Close();

                dt = ds.Tables[0];

            }
             catch (Exception ex)
             {
                 wrLog(ex.ToString(), "");
                 //throw;
             }
             //finally
             //{                
             //}

            return dt;
           
        }

        //DT TO CSV
        public void DtToCsv(DataTable oTable, string FilePath)
        {
            string data = "";
            int i = 1;
            StreamWriter wr = new StreamWriter(FilePath, false, System.Text.Encoding.Default);

            try
            {
                foreach (DataColumn column in oTable.Columns)
                {
                    if (i < oTable.Columns.Count)
                    {
                        data += column.ColumnName + ",";
                    }
                    else
                    {
                        data += column.ColumnName;
                    }
                    i += 1;
                }
                data += "\n";
                wr.Write(data);
                data = "";

                foreach (DataRow row in oTable.Rows)
                {
                    i = 1;
                    foreach (DataColumn column in oTable.Columns)
                    {
                        if (i < oTable.Columns.Count)
                        {
                            data += row[column].ToString().Trim() + ",";
                        }
                        else
                        {
                            data += row[column].ToString().Trim();
                        }
                        i += 1;
                    }
                    data += "\n";
                    wr.Write(data);
                    data = "";
                }
                data += "\n";
            }
            catch (Exception e)
            {
                wr.Write(data);
                throw e;
            }
            finally 
            {
                wr.Dispose();
                wr.Close();
            }
        }
        
        //解密讀檔 file to string
        public string rdTXT(string FilePath)
        {
            string data = "";

            try
            {
                StreamReader s = new StreamReader(FilePath, System.Text.Encoding.Default);
                data = getDES(s.ReadToEnd());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            //finally
            //{             
            //}

            return data;
        }

        //加密寫檔 file to file
        public void wrDES(string FilePath_S, string FilePath_T)
        {
            string data = "";
            StreamWriter wr = new StreamWriter(FilePath_T, false, System.Text.Encoding.Default);                

            try
            {
                data = getDES(rdTXT(FilePath_S));
                wr.Write(data);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                wr.Dispose();
                wr.Close();
            }
        }

        //加密寫檔 file to file
        public void rdDES(string FilePath_S, string FilePath_T)
        {
            string data = "";
            StreamWriter wr = new StreamWriter(FilePath_T, false, System.Text.Encoding.Default);

            try
            {
                data = setDES(rdTXT(FilePath_S));
                wr.Write(data);
            }
            catch (Exception e)
            {
                throw e;
            }
            finally
            {
                wr.Dispose();
                wr.Close();
            }
        }
        #endregion        

        //版本
        public string getVer()
        {
            string str="";
            try
            {
                //str += System.Deployment.Application.ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString(); //發行
                str += typeof(Class1).Assembly.GetName().Version.ToString(); //組件版本編號
            }
            catch (Exception)
            {
                str = "開發程式階段";
            }
            return str;
        }

        //寫LOG
        public void wrLog(string msg,string txtID)
        {
            string fil = @"C:\AutoBrowser_log\AutoBrowser_log.txt";
            string dir = fil.Substring(0, fil.LastIndexOf(@"\"));
            try
            {
                //建立目錄
                if (!Directory.Exists(dir))
                {
                    DirectoryInfo di = Directory.CreateDirectory(dir);
                }

                //引用類別
                //FileStream myFile = File.Open(@"C:\test\log.txt", FileMode.OpenOrCreate, FileAccess.ReadWrite, FileShare.ReadWrite);
                FileStream myFile = File.Open(fil, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);

                //引用StringWriter類別
                StreamWriter myWriter = new StreamWriter(myFile);

                //寫字串至檔案
                myWriter.Write(msg + "，");
                myWriter.Write(txtID + "，");
                myWriter.WriteLine(DateTime.Now);

                //釋放資源
                myWriter.Dispose();
                myFile.Dispose();
            }
            catch (Exception ex)
            {
                //MessageBox.Show("ERR!!" + ex.ToString());
                throw ex;
            }
        }
    }

}
