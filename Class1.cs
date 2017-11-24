using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using System.Diagnostics;
using System.Data;
using System.Net;
using MongoDB.Bson;
using MongoDB.Driver;
//using MongoDB.Driver.Builders;

using System.Collections;


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
                    wrLog("[ERR:" + errormsg.ToString() + "]", "Connect");
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

        #region 轉換CSV&DT
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
                    string[] items = r.Replace("\r","").Split(delimiter.ToCharArray());
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

        #region MONGO
        //ref:http://kenneth2011.pixnet.net/blog/post/112097794

        public class MongoProduct
        {
            public ObjectId _id { get; set; }
            public ArrayList txt { get; set; }
            public string ip { get; set; }
            public string dtime { get; set; }
        }

        string path = AutoBrowser.Properties.Settings.Default.path_local.ToString();

        public bool mongo_sync()
        {
            bool Flag = true;
            
            try
            {
                //-----改寫舊語法 START------
                //連結DB
                //MongoDatabase myDB;
                //List<MongoProduct> Products = new List<MongoProduct>();
                ////MongoClient _client = new MongoClient("Server=localhost:27017"); // 產生 MongoClient 物件
                //MongoClient _client = new MongoClient(AutoBrowser.Properties.Settings.Default.mongo.ToString()); // 產生 MongoClient 物件
                //MongoServer server = _client.GetServer(); // 取得 MongoServer 物件
                //myDB = server.GetDatabase("AutoBrowser"); // 取得 MongoDatabase 物件

                //讀DB
                //MongoCollection<MongoProduct> _Products = myDB.GetCollection<MongoProduct>("Products");
                //var _product = _Products.FindOne();
                //-----改寫舊語法 END------
                var client = new MongoClient(AutoBrowser.Properties.Settings.Default.mongo.ToString());
                var myDB = client.GetDatabase("AutoBrowser");
                IMongoCollection<MongoProduct> _Products = myDB.GetCollection<MongoProduct>("Products");

                //讀DB,第一筆
                var _product = _Products.Find(new BsonDocument()).First();
                                
                //寫檔
                StreamWriter file = new StreamWriter(path, false, Encoding.Default);

                ArrayList arrText = new ArrayList();
                arrText = _product.txt;
                
                for (int i = 0; i < arrText.Count; i++)
                {
                    file.WriteLine(arrText[i].ToString());
                }

                file.Close();
            }
            catch (Exception ex)
            {
                Flag = false;
                wrLog(ex.ToString(), "mongo_sync");
                throw ex;
            }

            return Flag;
        }

        //讀取
        public string mongo_read()
        {
            string str = "";

            try
            {
                //-----改寫舊語法 START------
                //連結DB
                //MongoDatabase myDB;
                //List<MongoProduct> Products = new List<MongoProduct>();
                
                //MongoClient _client = new MongoClient("Server=localhost:27017"); // 產生 MongoClient 物件
                ////MongoClient _client = new MongoClient(AutoBrowser.Properties.Settings.Default.mongo.ToString()); // 產生 MongoClient 物件
                //MongoServer server = _client.GetServer(); // 取得 MongoServer 物件
                //myDB = server.GetDatabase("AutoBrowser"); // 取得 MongoDatabase 物件
                
                //讀DB
                //MongoCollection<MongoProduct> _Products = myDB.GetCollection<MongoProduct>("Products");
                //var _product = _Products.FindOne();
                //-----改寫舊語法 END------
                var client = new MongoClient(AutoBrowser.Properties.Settings.Default.mongo.ToString());
                var myDB = client.GetDatabase("AutoBrowser");
                IMongoCollection<MongoProduct> _Products = myDB.GetCollection<MongoProduct>("Products");

                //讀DB,第一筆
                var _product = _Products.Find(new BsonDocument()).First();
                str = _product.dtime.ToString();                        
            }
            catch (Exception ex)
            {
                wrLog(ex.ToString(), "mongo_read");
                throw ex;
            }

            return str;
        }

        //更新
        public void mongo_upload()
        {
            try
            {
                //-----改寫舊語法 START------
                //連結DB
                //MongoDatabase myDB;
                //List<MongoProduct> Products = new List<MongoProduct>();
                ////MongoClient _client = new MongoClient("Server=localhost:27017"); // 產生 MongoClient 物件
                //MongoClient _client = new MongoClient(AutoBrowser.Properties.Settings.Default.mongo.ToString()); // 產生 MongoClient 物件
                //MongoServer server = _client.GetServer(); // 取得 MongoServer 物件
                //myDB = server.GetDatabase("AutoBrowser"); // 取得 MongoDatabase 物件
                //-----改寫舊語法 END------               
                var client = new MongoClient(AutoBrowser.Properties.Settings.Default.mongo.ToString());
                var myDB = client.GetDatabase("AutoBrowser");
                
                //讀檔
                StreamReader objReader = new StreamReader(path, Encoding.Default);
                string sLine = "";
                ArrayList arrText = new ArrayList();

                do
                {
                    sLine = objReader.ReadLine();
                    if (sLine != null)
                    {
                        arrText.Add(sLine);
                    }
                } while (sLine != null);
                objReader.Close();

                //寫DB,先TRUNCATE再INSERT,只留最新一筆
                //-----改寫舊語法 START------
                //MongoCollection<MongoProduct> _Products = myDB.GetCollection<MongoProduct>("Products"); // 取得 Collection
                //-----改寫舊語法 END------
                IMongoCollection<MongoProduct> _Products = myDB.GetCollection<MongoProduct>("Products");

                var newProduct = new MongoProduct();
                newProduct.txt = arrText;
                newProduct.ip = getIP().ToString();
                newProduct.dtime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                //-----改寫舊語法 START------
                //_Products.Drop();
                //_Products.Insert(newProduct);
                //-----改寫舊語法 END------
                myDB.DropCollection("Products");                
                _Products.InsertOne(newProduct);


                //類LOG,不TRUNCATE
                IMongoCollection<MongoProduct> _Products_log = myDB.GetCollection<MongoProduct>("Products_LOG"); // 取得 Collection
                var newProduct_log = new MongoProduct();
                newProduct_log.txt = arrText;
                newProduct_log.ip = getIP().ToString();
                newProduct_log.dtime = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");

                _Products_log.InsertOne(newProduct_log);
            }
            catch (Exception ex)
            {
                wrLog(ex.ToString(), "mongo_upload");
                throw ex;
            }
            
            
        }
        #endregion
        
        //版本
        public string getVer()
        {
            string str = "";
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
        public void wrLog(string msg, string txtID)
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

        //讀取本機IP
        public string getIP()
        {
            string str = "";

            // 取得本機名稱
            string strHostName = Dns.GetHostName();

            // 取得本機的IpHostEntry類別實體，MSDN建議新的用法
            IPHostEntry iphostentry = Dns.GetHostEntry(strHostName);

            // 取得所有 IP 位址
            foreach (IPAddress ipaddress in iphostentry.AddressList)
            {
                // 只取得IP V4的Address
                if (ipaddress.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    str = ipaddress.ToString();
                }
            }

            return str;
        }
    }
}
