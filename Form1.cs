using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//
using MySql.Data.MySqlClient;
using System.Xml;
using System.IO.Ports;
using System.Threading;
using System.Text.RegularExpressions;
using System.IO;
using System.Configuration;

namespace ReadTerminals
{
    public partial class Form1 : Form
    {
        SerialPort sp;
        int datab = 0;
        bool dtr = false;
        bool encod;
        Handshake h;
        Parity p;
        int wtimeout = 0;
        StopBits s;
        //
        DataTable _dtPlaces = null;
        //

        public Form1()
        {
            InitializeComponent();
            this.sp = new SerialPort();
            this.datab = sp.DataBits = 8;
            this.dtr = sp.DtrEnable = true;
            //this.encod = sp.Encoding.Equals("iso-8859-1");
            this.encod = sp.Encoding.Equals("utf-8");
            this.h = sp.Handshake = Handshake.RequestToSend;
            this.p = sp.Parity = Parity.None;
            this.wtimeout = sp.WriteTimeout = 300;
            this.s = sp.StopBits = StopBits.One;
            checkPort();            
        }
        XmlDocument _doc = new XmlDocument();

        private void Form1_Load(object sender, EventArgs e)
        {
            this.DisplayPlaces();
            setTimer();
        }

        //===============================================Read & Save=================================
        bool ConnectDevice(string ipAdd)
        {             
            short i = axBioBridgeSDK1.Connect_TCPIP("R2", 1, ipAdd, 4370, 0);
            if (i == 0)
            {
                //btnConnect.Enabled = false;
                //listBox1.Items.Add("Manufacturer: " + axBioBridgeSDK1.VC);
                //listBox1.Items.Add("Serial No: " + axBioBridgeSDK1.SN);
                //listBox1.Items.Add("Mac Add: " + axBioBridgeSDK1.MAC);
                //listBox1.Items.Add("Model Name: " + axBioBridgeSDK1.DC);
                return true;
            }
            else
            {
                listBox1.Items.Add(DateTime.Now + ": " + "Unable to Connect to device: " + ipAdd+"!!");
                //listBox1.Items.Add("Error: " +DateTime.Now+": "+ axBioBridgeSDK1.VC);
                return false;
            }
        }

        void DisconnectDevice()
        {
            if (axBioBridgeSDK1.Disconnect() == 0)
            {
                listBox1.Items.Add("Disconnect from Device");
                //btnConnect.Enabled = true;
            }
            else
            {
                listBox1.Items.Add("Unable to disconnect!");
            }
        }

        bool SaveAccessLogMysql(DataTable dtlog, ref string msg)
        {
            string SyncSql = @"delete n1 from attendance_attendancehistory n1, attendance_attendancehistory n2
                            where n1.id > n2.id
                            and n1.emp_card_id=n2.emp_card_id
                            and n1.date=n2.date
                            and n1.time=n2.time
                            and n1.verification=n2.verification
                            and n1.in_out=n2.in_out
                            and n1.workCode=n2.workCode
                            and n1.location=n2.location";
            //MySqlConnection con = new MySqlConnection("Data Source=127.0.0.1;User ID=root;Password=root;Initial Catalog=hrmis_db");
            MySqlConnection con = new MySqlConnection(ConfigurationSettings.AppSettings["DbConnectionString"]);
            try
            {
                con.Open();
                MySqlDataAdapter adapt = new MySqlDataAdapter("select emp_card_id,date,time,verification,in_out,workCode,location,insertDtm,field2,field3 from attendance_attendancehistory", con);                
                MySqlCommandBuilder builder = new MySqlCommandBuilder(adapt);
                int result = adapt.Update(dtlog);
                //MySqlCommand cmd = new MySqlCommand(SyncSql, con);
                //cmd.CommandTimeout = 0;
                //int i = cmd.ExecuteNonQuery();                
                if (con.State == ConnectionState.Open) con.Close();
                if (result> 0) return true;
                else return false;
            }
            catch(Exception ex)
            {
                msg = ex.Message;
                if (con.State == ConnectionState.Open) con.Close();
                return false;
            }
            //return true;
        }

        void ReadLog(string places,string dType)
        {
            int yr = 0;
            int mth = 0;
            int day_Renamed = 0;
            int hr = 0;
            int min = 0;
            int sec = 0;

            int enrollNo = 0;
            int ver = 0;
            int io = 0;
            int work = 0;
            int log = 0;

            //axBioBridgeSDK1.SetLastCount(Convert.ToInt32(txtSetLastCount.Text));
            axBioBridgeSDK1.SetLastCount(0);
            short logs = axBioBridgeSDK1.ReadGeneralLog(ref log);
            if (logs == 0)
            {                
                DataTable dtlog = new DataTable();
                dtlog.Columns.Add("emp_card_id");
                dtlog.Columns.Add("date");
                dtlog.Columns.Add("time");
                dtlog.Columns.Add("verification");
                dtlog.Columns.Add("in_out");
                dtlog.Columns.Add("workCode");
                dtlog.Columns.Add("location");
                dtlog.Columns.Add("insertDtm");
                dtlog.Columns.Add("field2");
                dtlog.Columns.Add("field3");

                using (var writer = new StreamWriter("Access_AttendanceLog.log", true))
                {
                    DateTime insDtm = DateTime.Now;
                    string insDateTime = insDtm.ToString("yyyy-MM-dd hh:mm:ss");
                    do
                    {
                        //listBox1.Invoke(() => listBox1.Items.Add(("No: " + Convert.ToString(enrollNo) + " Date:" + Convert.ToString(day_Renamed) + "/" + Convert.ToString(mth) + "/" + Convert.ToString(yr) + " Time: " + Convert.ToString(hr) + ":" + Convert.ToString(min) + ":" + Convert.ToString(sec) + " Verify: " + Convert.ToString(ver) + " I/O: " + Convert.ToString(io) + " Work Code: " + Convert.ToString(work))));
                        string date = Convert.ToString(yr) + "-" + Convert.ToString(mth) + "-" + Convert.ToString(day_Renamed);
                        if (date != "0-0-0")
                        {
                            DataRow row = dtlog.NewRow();
                            row["emp_card_id"] = Convert.ToString(enrollNo);
                            row["date"] = Convert.ToString(yr) + "-" + Convert.ToString(mth) + "-" + Convert.ToString(day_Renamed);
                            row["time"] = Convert.ToString(hr) + ":" + Convert.ToString(min) + ":" + Convert.ToString(sec);
                            row["verification"] = Convert.ToString(ver);
                            if (dType.ToUpper() == "N")
                                io = io == 0 ? 1 : 0;
                            row["in_out"] = Convert.ToString(io);
                            row["workCode"] = Convert.ToString(work);
                            //row["location"] = cbxPlace.Text;//places to be inserted in database
                            row["location"] = places;//places to be inserted in database
                            row["insertDtm"] = insDateTime;
                            row["field2"] = "";
                            row["field3"] = "";
                            dtlog.Rows.Add(row);
                            //Writing log
                            writer.WriteLine(enrollNo + "," + date + "," + hr + ":" + min + ":" + sec + "," + ver + "," + io + "," + work + "," + places);
                        }

                    } while (axBioBridgeSDK1.GetGeneralLog(ref enrollNo, ref yr, ref mth, ref day_Renamed, ref hr, ref min, ref sec, ref ver, ref io, ref work) == 0);
                }
                    
                //axBioBridgeSDK1.Disconnect();
                string message = "";
                if (!SaveAccessLogMysql(dtlog, ref message))
                    listBox1.Items.Add(DateTime.Now+": "+places + ": " + message + "");
                else
                {
                    listBox1.Items.Add(places + ": Read " + log + " log(s) and save into database.");
                    this.ClearLog();
                    axBioBridgeSDK1.Disconnect();
                }
            }
            else
            {                
                listBox1.Items.Add(DateTime.Now + ": " + places + ": Unable to Read. There may be no Log in this device!!");
            }
        }

        void ClearLog()
        {
            if (axBioBridgeSDK1.DeleteGeneralLog() == 0)
            {
                listBox1.Items.Add(("All Logs Cleared"));
            }
            else
            {
                listBox1.Items.Add(("Unable to Clear!!"));
            }
        }

        void ReadAllAccessLogs()
        {
            cbxPlace.Enabled = false;
            btnReadSave.Enabled = false;

            chkForTime.Stop();

            for (int i = 0; i < _dtPlaces.Rows.Count; i++)
            {
                if (this.ConnectDevice(_dtPlaces.Rows[i]["IpAddress"].ToString()))                    
                    this.ReadLog(_dtPlaces.Rows[i]["Places"].ToString(),_dtPlaces.Rows[i]["DeviceID"].ToString());
            }

            //cbxPlace.Enabled = true;
            //btnReadSave.Enabled = true;
            chkForTime.Start();
        }

        private void btnReadSave_Click(object sender, EventArgs e)
        {
            cbxPlace.Enabled = false;
            btnReadSave.Enabled = false;

            chkForTime.Stop();

            string searchExpression = "Places = '" + cbxPlace.Text.ToString()+"'";
            DataRow[] drow = _dtPlaces.Select(searchExpression);
            string s = drow[0]["DeviceID"].ToString();
            if (this.ConnectDevice(cbxPlace.SelectedValue.ToString()))
                this.ReadLog(cbxPlace.Text,s);

            label5.Text = s;
            cbxPlace.Enabled = true;
            btnReadSave.Enabled = true;
            chkForTime.Start();
        }

        private void btnClearList_Click(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
        }

        //=====================================Settings==============================================

        private void btnAddDevice_Click(object sender, EventArgs e)
        {            
            this.WriteXmlDevice();
            DisplayPlaces();
        }

        DataTable ReadXmlDevice()
        {
            _doc.Load("Devices.xml");
            DataTable dt = new DataTable();
            dt.Columns.Add("Places");
            dt.Columns.Add("DeviceID");
            dt.Columns.Add("Model");
            dt.Columns.Add("IpAddress");
            XmlNodeList nodeList = _doc.GetElementsByTagName("Device");
            foreach(XmlNode node in nodeList)
            {
                DataRow row = dt.NewRow();
                row["Places"] = node.ChildNodes.Item(0).InnerText;
                row["DeviceID"] = node.ChildNodes.Item(1).InnerText;
                row["Model"] = node.ChildNodes.Item(2).InnerText;
                row["IpAddress"] = node.ChildNodes.Item(3).InnerText;
                dt.Rows.Add(row);
            }
            return dt;
        }

        void WriteXmlDevice()
        {
            _doc.Load("Devices.xml");
            XmlElement newDevice = _doc.CreateElement("Device");

            XmlElement newPlaces = _doc.CreateElement("Places");
            newPlaces.InnerText = txtPlace.Text.Trim();
            newDevice.AppendChild(newPlaces);

            XmlElement newDevid = _doc.CreateElement("DeviceID");
            newDevid.InnerText = txtDevID.Text.Trim();
            newDevice.AppendChild(newDevid);

            XmlElement newModel = _doc.CreateElement("Model");
            newModel.InnerText = txtModel.Text.Trim();
            newDevice.AppendChild(newModel);

            XmlElement newIpAdd = _doc.CreateElement("IpAddress");
            newIpAdd.InnerText = txtIPAdd.Text.Trim();
            newDevice.AppendChild(newIpAdd);

            _doc.DocumentElement.AppendChild(newDevice);

            XmlTextWriter xtr = new XmlTextWriter("Devices.xml", null);
            xtr.Formatting = Formatting.Indented;
            _doc.WriteContentTo(xtr);
            xtr.Close();
        }

        void DisplayPlaces()
        {
            //DataTable dt= this.ReadXmlDevice();
            _dtPlaces = this.ReadXmlDevice();
            dgvDevice.DataSource = _dtPlaces;
            cbxPlace.DataSource = _dtPlaces;
            cbxPlace.ValueMember= _dtPlaces.Columns["IpAddress"].ToString();
            cbxPlace.DisplayMember = _dtPlaces.Columns["Places"].ToString();
            cbxPlace.Text = "All";
        }
                

        //==================================================SMS========================================
        private void checkPort()
        {
            //GetValues value = new GetValues();
            //string com = "COM7";
            string com = ConfigurationSettings.AppSettings["ComPort"];
            int baud = 9600;
            int timeot = 300;
            sp.PortName = com;
            sp.BaudRate = baud;
            sp.ReadTimeout = timeot;
            //sp.DataReceived += new SerialDataReceivedEventHandler(getResponse);
            try
            {
                sp.Open();
            }
            catch
            {
                listBox1.Items.Add("Modem/Port for SMS is not ready");
            }            
            
            //sp.Close();
        }                
                
        private void setTimer()
        {
            //chkForTime.Interval = 1000 * 60 * 5;//for interval = 5 min
            chkForTime.Interval = 1000 * 60 *Convert.ToInt32(ConfigurationSettings.AppSettings["TimerIntervals"]);//for interval = 5 min
            /*if (!sp.IsOpen)
                chkForTime.Stop();*/
            //sp.Open();                        
        }

        private void getSMS()
        {
            sp.WriteLine("AT+CMGF=1" + System.Environment.NewLine);
            //sp.WriteLine("AT+CMGF: (0,1)" + System.Environment.NewLine);
            //Thread.Sleep(1000);
            sp.WriteLine("AT+CMGL=\"ALL\"\r" + System.Environment.NewLine); //("AT+CMGL=\"REC UNREAD\"\r");
            //sp.WriteLine("AT+CMGL=REC READ" + System.Environment.NewLine); //("AT+CMGL=\"REC UNREAD\"\r");            
            Thread.Sleep(3000);
                        
            Regex r = new Regex(@"\+CMGL: (\d+),""(.+)"",""(.+)"",(.*),""(.+)""\r\n(.+)\r\n");
            //test enter Regex r = new Regex(@"\+CMGL: (\d+),""(.+)"",""(.+)"",(.*),""(.+)""\r\n(.+|.+\n*.*\n*.*)\r\n");
            string mtext = sp.ReadExisting();
            //MessageBox.Show(sp.ReadExisting());
            string nmtext = mtext.Replace("\n", " ");
            nmtext = nmtext.Replace("\r", "\r\n");
            Match m = r.Match(nmtext);
            //test enter Match m = r.Match(sp.ReadExisting());                        

            DataTable dtSMS = new DataTable();
            dtSMS.Columns.Add("mobile_no");
            dtSMS.Columns.Add("date_time");
            dtSMS.Columns.Add("sms_text");
            dtSMS.Columns.Add("insert_date");

            DateTime insDt = DateTime.Now;
            string insDate = insDt.ToString("yyyy-MM-dd hh:mm:ss");
            string AllIndex = "";
            string[] smsIndexs = {""};
            //To write log before save in database and delete from modem
            
            using (var writer = new StreamWriter("SMSlog.log", true))
            {
            while (m.Success)
                {
                    string sl = m.Groups[1].Value;
                    string state = m.Groups[2].Value;
                    string cell_no = m.Groups[3].Value;
                    string d = m.Groups[4].Value;                                        
                    string date_time = m.Groups[5].Value.ToString();
                    date_time = date_time.Substring(0, 19);
                    string text = m.Groups[6].Value;
                    text = text.Replace("  ", " ");

                    DataRow row = dtSMS.NewRow();
                    row["mobile_no"] = Convert.ToString(cell_no);                    
                    row["date_time"] = date_time;
                    row["sms_text"] = Convert.ToString(text);
                    row["insert_date"] = insDate;

                    dtSMS.Rows.Add(row);

                    //ListViewItem item = new ListViewItem(new string[] { a, b, c, d, ee, f });
                    string item = sl + " " + state + " " + cell_no + " " + date_time + " " + text;
                    //listBox1.Items.Add(item);
                    m = m.NextMatch();

                    //To write log before save in database and delete from modem                
                    writer.WriteLine("SMS: " + item);
                    
                    AllIndex += sl+",";
                }
            }
            if(AllIndex!="")
            {
                AllIndex = AllIndex.Substring(0, AllIndex.Length - 1);
                smsIndexs = AllIndex.Split(',');
            }            
            
            if (dtSMS.Rows.Count>0)
            {
                DataTable dtsmsLate = new DataTable();
                if (SaveSMSMysql(dtSMS, insDate, ref dtsmsLate))
                {
                    if (!SaveLateMysql(ref dtsmsLate))
                        listBox1.Items.Add("Cell no may not be in Employee table or May not new SMS!!");
                    for(int i=0;i < dtSMS.Rows.Count; i++)
                    {
                        Thread.Sleep(1000);
                        sp.WriteLine("AT+CMGD=" + smsIndexs[i] + "" + System.Environment.NewLine); //("AT+CMGL=\"REC UNREAD\"\r");
                    }

                }
                else
                { listBox1.Items.Add("Unable to save SMS in database!!"); }
            }
            else
            {
                //listBox1.Items.Add("No new SMS!!");
            }
            
        }

        bool SaveSMSMysql(DataTable dtSMS, string insertDt, ref DataTable dtsmsLate)
        {
            string SyncSql = @"delete n1 from attendance_sms n1, attendance_sms n2
                            where n1.id > n2.id
                            and n1.mobile_no=n2.mobile_no
                            and n1.date_time=n2.date_time
                            and n1.sms_text=n2.sms_text";
            string insLate_sms = @"select date_time as date,0 as isEndorsed, sms_text, 1 as isActive, NULL as isDelete,
                            'system' as insertUser, NULL as insertDate, NULL as updateUser, NULL as updateDate,
                            1 as project, employee_id as emp_id_id,NULL as endorsed_by_id,attendance_sms.id as sms_table_id_id from attendance_sms, hr_employee
                            where attendance_sms.mobile_no = hr_employee.mobile
                            and insert_date = '" + insertDt+"'";
            dtsmsLate.Columns.Add("date");
            dtsmsLate.Columns.Add("isEndorsed");
            dtsmsLate.Columns.Add("sms_text");
            dtsmsLate.Columns.Add("isActive");
            dtsmsLate.Columns.Add("isDelete");
            dtsmsLate.Columns.Add("insertUser");
            dtsmsLate.Columns.Add("insertDate");
            dtsmsLate.Columns.Add("updateUser");
            dtsmsLate.Columns.Add("updateDate");
            dtsmsLate.Columns.Add("project");
            dtsmsLate.Columns.Add("emp_id_id");
            dtsmsLate.Columns.Add("endorsed_by_id");
            dtsmsLate.Columns.Add("sms_table_id_id");
            //MySqlConnection con = new MySqlConnection("Data Source=127.0.0.1;User ID=root;Password=root;Initial Catalog=hrmis_db");
            MySqlConnection con = new MySqlConnection(ConfigurationSettings.AppSettings["DbConnectionString"]);

            try
            {
                con.Open();
                MySqlDataAdapter adapt = new MySqlDataAdapter("select mobile_no, date_time, sms_text, insert_date from attendance_sms", con);
                MySqlCommandBuilder builder = new MySqlCommandBuilder(adapt);
                int result = adapt.Update(dtSMS);
                MySqlCommand cmd = new MySqlCommand(SyncSql, con);
                //int i = cmd.ExecuteNonQuery(); commented out for testing
                
                DataSet ds = new DataSet();
                if (result > 0)
                {                    
                    cmd.CommandText = insLate_sms;
                    adapt.SelectCommand = cmd;
                    adapt.Fill(ds);
                    DataTable dt = ds.Tables[0].Copy();
                    foreach(DataRow drdt in dt.Rows)
                    {
                        DataRow row = dtsmsLate.NewRow();
                        row["date"] = drdt["date"];
                        row["isEndorsed"] = drdt["isEndorsed"];
                        row["sms_text"] = drdt["sms_text"];
                        row["isActive"] = drdt["isActive"];
                        row["isDelete"] = drdt["isDelete"];
                        row["insertUser"] = drdt["insertUser"];
                        row["insertDate"] = insertDt;
                        row["updateUser"] = drdt["updateUser"];
                        row["updateDate"] = drdt["updateDate"];
                        row["project"] = drdt["project"];
                        row["emp_id_id"] = drdt["emp_id_id"];
                        row["endorsed_by_id"] = drdt["endorsed_by_id"];
                        row["sms_table_id_id"] = drdt["sms_table_id_id"];
                        dtsmsLate.Rows.Add(row);
                    }
                    //dtsmsLate.AcceptChanges();
                    ds.Dispose();                    
                    if (con.State == ConnectionState.Open) con.Close();
                    return true;
                }
                else
                {
                    if (con.State == ConnectionState.Open) con.Close();
                    return false;
                }
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                if (con.State == ConnectionState.Open) con.Close();
                return false;
            }
        }

        bool SaveLateMysql(ref DataTable dtSMSLate)
        {
            /*string SyncSql = @"delete n1 from attendance_sms n1, attendance_sms n2
                            where n1.id > n2.id
                            and n1.mobile_no=n2.mobile_no
                            and n1.date_time=n2.date_time
                            and n1.sms_text=n2.sms_text";
            string insLate_sms = @"select date_time as date,0 as isEndorsed, sms_text, 1 as isActive, NULL as isDelete,
                            'system' as insertUser, sysdate() as insertDate, NULL as updateUser, NULL as updateDate,
                            1 as project, employee_id as emp_id_id,NULL as endorsed_by_id from attendance_sms, hr_employee
                            where attendance_sms.mobile_no = hr_employee.mobile
                            and insert_date = '" + insertDt + "'";*/

            
            //MySqlConnection con1 = new MySqlConnection("Data Source=127.0.0.1;User ID=root;Password=root;Initial Catalog=hrmis_db");
            MySqlConnection con1 = new MySqlConnection(ConfigurationSettings.AppSettings["DbConnectionString"]);

            try
            {
                con1.Open();
                MySqlDataAdapter ladapt = new MySqlDataAdapter("select date, isEndorsed, sms_text, isActive, isDelete, insertUser, insertDate, updateUser, updateDate, project, emp_id_id, endorsed_by_id, sms_table_id_id from attendance_late", con1);
                
                MySqlCommandBuilder lbuilder = new MySqlCommandBuilder(ladapt);
                int result = ladapt.Update(dtSMSLate);
                if (con1.State == ConnectionState.Open) con1.Close();
                if (result > 0) return true;                
                else return false;                
            }
            catch (Exception ex)
            {
                string s = ex.Message;
                if (con1.State == ConnectionState.Open) con1.Close();
                return false;
            }
        }

        void ReadSMS()
        {
            if (sp.IsOpen)
            {
                try
                {
                    getSMS();
                }
                catch(Exception ex)
                {
                    listBox1.Items.Add("There may be some problem in reading SMS. Error: "+ex.Message+"!!");
                }
            }
        }

        private void btnReadSaveSMS_Click(object sender, EventArgs e)
        {
            if (btnReadSaveSMS.Text == "Start")
            {
                cbxPlace.Enabled = false;
                btnReadSave.Enabled = false;

                ReadSMS();

                chkForTime.Start();
                btnReadSaveSMS.Text = "Stop";
                return;
            }
            if (btnReadSaveSMS.Text=="Stop")
            {
                chkForTime.Stop();

                cbxPlace.Enabled = true;
                btnReadSave.Enabled = true;

                btnReadSaveSMS.Text = "Start";
                return;
            }
            
            /*btnReadSaveSMS.Enabled = false;
            listBox1.Refresh();
            listBox1.Items.Clear();
            if (sp.IsOpen)
                getSMS();
            btnReadSaveSMS.Enabled = true;*/
        }

        private void chkForTime_Tick(object sender, EventArgs e)
        {
            ReadSMS();
            ReadAllAccessLogs();
        }        
    }
}
