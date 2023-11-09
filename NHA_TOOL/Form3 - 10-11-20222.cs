
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace NHA_TOOL
{
    public partial class Form3 : Form
    {
        string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
        Loader loader;

        string header = "Sr. No.,Member_Name,PMJAY_ID,YOB,Gender,Village,Taluka/Block,District,@Image,@QR,Card_PDF,Abha_ID,Sheet No.,Sr. No. with Barcode,IBN,OBN,Batch";
        
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {

            loader = new Loader();
            loader.Show();

            DataTable dt_1 = new DataTable();
            using (SqlConnection con0 = new SqlConnection(connectionString))
            {
                SqlCommand com0 = new SqlCommand("select distinct OP_Dist_Name from [dbo].[NHA_DATA_CONSOLE] order by OP_Dist_Name", con0);
                con0.Open();
                SqlDataAdapter reader01= new SqlDataAdapter(com0);
                reader01.Fill(dt_1);
                dataGridView1.DataSource = dt_1;
                con0.Close();



            }
            loader.Close();
        }
        public void WriteCSV(string coloumnsName, string rowsdata, string filename, string statename)
        {
            StreamWriter log;
            if (!File.Exists("NHA_DATA_" + statename + "_" + filename + ".csv"))
            {
                log = new StreamWriter("NHA_DATA_" + statename + "_" + filename + ".csv");
            }
            else
            {
                log = File.AppendText("NHA_DATA_" + statename + "_" + filename + ".csv");
            }
            log.WriteLine(coloumnsName);
            log.WriteLine(rowsdata);
            log.Close();

            //StreamReader sr = new StreamReader("NHA_DATA_" + statename + "_" + filename + ".csv");
            //string[] columnnames = sr.ReadLine().Split(',');
            //DataTable table = new DataTable();
            //foreach (string c in columnnames)
            //{
            //    table.Columns.Add(c);
            //}

            //string newline;

            //while ((newline = sr.ReadLine()) != null)
            //{
            //    DataRow dr = table.NewRow();
            //    string[] values = newline.Split(',');
            //    for (int i = 0; i < values.Length; i++)
            //    {
            //        dr[i] = values[i];
            //    }
            //    table.Rows.Add(dr);

            //}

            //sr.Close();

            //MessageBox.Show(table.Rows.Count.ToString());

            //dataGridView1.DataSource = table;
            //dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;


        }

        public void WriteSummaryCSV(string distname, int totaldata, int bencount , int opcount)
        {
            StreamWriter log;
            if (!File.Exists("NHA_DATA_summary_sheet.csv"))
            {
                log = new StreamWriter("NHA_DATA_summary_sheet.csv");
                log.WriteLine("DISTRICT_NAME,BEN_COUNT,OP_COUNT,TOTOAL_COUNT");
            }
            else
            {
                log = File.AppendText("NHA_DATA_summary_sheet.csv");
                
            }
            log.WriteLine(distname +","+ bencount+","+ opcount+","+ totaldata);
            //log.WriteLine();
            log.Close();
        }


        //public void loaddata()
        //{
        //    string FilePath = Directory.GetCurrentDirectory();

        //    StreamReader sr = new StreamReader(FilePath + @"\test_1.csv");

        //    string[] columnnames = sr.ReadLine().Split(',');
        //    DataTable table = new DataTable();
        //    foreach (string c in columnnames)
        //    {
        //        table.Columns.Add(c);

        //    }

        //    string newline;

        //    while ((newline = sr.ReadLine()) != null)
        //    {
        //        DataRow dr = table.NewRow();
        //        string[] values = newline.Split(',');
        //        for (int i = 0; i < values.Length; i++)
        //        {
        //            dr[i] = values[i];


        //        }
        //        table.Rows.Add(dr);

        //    }
        //    sr.Close();
        //    dataGridView1.DataSource = table;
        //    dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

        //}

        public int gettotalvalue( string op_district_name_1)
        {
            int tot = 0;
            int ben_count = 0;
            int op_count = 0;


            using (SqlConnection con = new SqlConnection(connectionString))
            {
                SqlCommand com = new SqlCommand("select count(*)  from [dbo].[NHA_DATA_CONSOLE] where OP_Dist_Name = '" + op_district_name_1 + "'", con);
                con.Open();
                SqlDataReader reader1;
                reader1 = com.ExecuteReader();

                while (reader1.Read())
                {
                    
                    ben_count = Int32.Parse(reader1.GetValue(0).ToString());
                    

                }
                con.Close();
            }
            
            using (SqlConnection con1 = new SqlConnection(connectionString))
            {
                SqlCommand com = new SqlCommand("select count(distinct OP_Name_Mobile)  from [dbo].[NHA_DATA_CONSOLE] where OP_Dist_Name = '" + op_district_name_1 + "'", con1);
                con1.Open();
                SqlDataReader reader1;
                reader1 = com.ExecuteReader();

                while (reader1.Read())
                {
                    
                    op_count = Int16.Parse(reader1.GetValue(0).ToString());

                }
                con1.Close();
            }

            tot = op_count + ben_count;

            WriteSummaryCSV(op_district_name_1, tot, ben_count, op_count);

            return tot;
        }
        public void update(string pmjay_id_ben, string sheet, string barcode,string ibn,string obn ,string batch, int page)
        {
            using (SqlConnection con1 = new SqlConnection(connectionString))
            {
                string Query = "update NHA_DATA_CONSOLE  set  Sheet_No = '" + sheet + "' ,  Barcode ='" + barcode + "',  Inner_box = '" + ibn + "',  Outter_box= '" + obn + "',  Batch= '" + batch + "',  Page= '" + page+ "',Data_Status= '1'  where ben_pmjay_id = '" + pmjay_id_ben + "'";
                using (SqlCommand com1 = new SqlCommand(Query, con1))

                {
                   
                    try
                    {
                        con1.Open();
                        com1.ExecuteNonQuery();
                        con1.Close();
                        //MessageBox.Show("Data Saved Successfully!");
                    }
                    catch (Exception exe)
                    {
                        MessageBox.Show(exe.Message);
                    }
                }

            }
           // return 1;
        }

       

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ALL_DATA_Click(object sender, EventArgs e)
        {
            loader = new Loader();
            loader.Show();
            string FilePath_1 = Directory.GetCurrentDirectory();
            string[] directoryFiles = System.IO.Directory.GetFiles(FilePath_1, "*.csv");
            foreach (string directoryFile in directoryFiles)
            {
                System.IO.File.Delete(directoryFile);
            }
            loader.Close();
            MessageBox.Show("Old csv deleted");




            using (SqlConnection con0 = new SqlConnection(connectionString))
            {
                loader = new Loader();
                loader.Show();
                SqlCommand com0 = new SqlCommand("select distinct OP_Dist_Name from [dbo].[NHA_DATA_CONSOLE] where DATA_STATUS =0  order by OP_Dist_Name", con0);
                con0.Open();
                SqlDataReader reader0;
                reader0 = com0.ExecuteReader();
                string state_name = "";

                while (reader0.Read())
                {
                    string datarows = "";
                    int counter = 0;
                    string op_district_name = reader0.GetValue(0).ToString().Replace(',', '-').Trim();
                    int totalRows = gettotalvalue(op_district_name);

                    string FilePath = Directory.GetCurrentDirectory();

                    //System.IO.File.Delete(FilePath + @"\test_1.csv");

                    int innerboxcount = 0, outerboxcount = 0, sheetcount = 0;

                    if (totalRows % 240 == 0)
                    {
                        innerboxcount = totalRows / 240;
                    }
                    else
                    {
                        innerboxcount = totalRows / 240 + 1;
                    }

                    if (totalRows % 1920 == 0)
                    {
                        outerboxcount = totalRows / 1920;
                    }
                    else
                    {
                        outerboxcount = totalRows / 1920 + 1;
                    }

                    if (totalRows % 24 == 0)
                    {
                        sheetcount = totalRows / 24;
                    }
                    else
                    {
                        sheetcount = totalRows / 24 + 1;
                    }

                    int num = 1, num1 = 1, num2 = 1, mycount = 0;//, mycount1 = 0, mycount2 = 0;

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        SqlCommand com = new SqlCommand("select distinct OP_Name_Mobile,OP_Dist_Name,OP_Taluka from [dbo].[NHA_DATA_CONSOLE] where OP_Dist_Name = '" + op_district_name + "' order by OP_Name_Mobile", con);
                        con.Open();
                        SqlDataReader reader1;
                        reader1 = com.ExecuteReader();

                        while (reader1.Read())
                        {
                            counter++;

                            // richTextBox1.Text += reader1.GetValue(0).ToString() + "\n\r" + reader1.GetValue(1).ToString() + "\n\r" + reader1.GetValue(2).ToString() + "\n\r";
                            string op_name = reader1.GetValue(0).ToString().Replace(',', '-').Trim();
                            string op_dist_1 = reader1.GetValue(1).ToString().Replace(',', '-').Trim();
                            string op_taluka_1 = reader1.GetValue(2).ToString().Replace(',', '-').Trim();
                            string Card_PDF = @"\Pdf\Front_Gujrat NHA Card_CP-22-6014-02_Blank_K-00.pdf";
                            string Sheet_No_1 = "*" + num + "-of-" + sheetcount + "*";
                            string IBN_1 = num1 + " of " + innerboxcount;
                            string OBN_1 = num2 + " of " + outerboxcount;
                            string op_dist_2 = "";
                            string op_taluka_2 = "";


                            try
                            {
                                op_dist_2 = op_dist_1.Substring(0, 3).ToUpper();
                                op_taluka_2 = op_taluka_1.Substring(0, 3).ToUpper();
                            }
                            catch
                            {
                                op_dist_2 = "";
                                op_taluka_2 = "";
                            }

                            string Batch_1 = "/GJ-BULK-01/" + op_dist_2 + "/" + op_taluka_2 + "/" + DateTime.Now.ToString("ddMMyyyy");
                            string SrNo_with_Barcode_1 = "*" + num + "-of-" + sheetcount + Batch_1 + "*";


                            datarows += counter + "," + op_name + "," + "," + "," + "," + "," + "Operator Taluka - " + op_taluka_1 + "," + "Operator District - " + op_taluka_1 + "," + "," + "," + Card_PDF + "," + "," + Sheet_No_1 + "," + SrNo_with_Barcode_1 + "," + IBN_1 + "," + OBN_1 + "," + Batch_1 + "\n";

                            mycount++;

                            if (mycount % 24 == 0)
                            {
                                num = num + 1;
                                // mycount = 0;
                            }

                            if (mycount % 240 == 0)
                            {
                                num1 = num1 + 1;
                                //mycount1 = 0;
                            }

                            if (mycount % 1920 == 0)
                            {
                                num2 = num2 + 1;
                                // mycount2 = 0;
                            }

                            using (SqlConnection con1 = new SqlConnection(connectionString))
                            {
                                SqlCommand com1 = new SqlCommand("select  * from [dbo].[NHA_DATA_CONSOLE] where OP_Name_Mobile='" + reader1.GetValue(0).ToString() + "' order by Name_Ben", con1);
                                con1.Open();
                                SqlDataReader reader2;
                                reader2 = com1.ExecuteReader();

                                while (reader2.Read())
                                {
                                    counter++;
                                    string ben_name = reader2.GetValue(0).ToString().Replace(',', '-').Trim();
                                    string yob = reader2.GetValue(1).ToString().Replace(',', '-').Trim();
                                    string Gender = reader2.GetValue(2).ToString().Replace(',', '-').Trim();
                                    string ben_mobile = reader2.GetValue(3).ToString().Replace(',', '-').Trim();

                                    string Taluka = reader2.GetValue(5).ToString().Replace(',', '-').Trim();
                                    string Village = reader2.GetValue(6).ToString().Replace(',', '-').Trim();
                                    string District = reader2.GetValue(7).ToString().Replace(',', '-').Trim();
                                    string State = reader2.GetValue(8).ToString().Replace(',', '-').Trim();
                                    state_name = State;
                                    string ben_pmjy_id = reader2.GetValue(9).ToString().Replace(',', '-').Trim();
                                    string r_u_ben = reader2.GetValue(10).ToString().Replace(',', '-').Trim();
                                    //string op_dist = reader2.GetValue(14).ToString().Replace(',', '-').Trim();
                                    //string op_taluka = reader2.GetValue(15).ToString().Replace(',', '-').Trim();



                                    //try
                                    //{
                                    //    op_dist = op_dist.Substring(0, 3);
                                    //    op_taluka = op_taluka.Substring(0, 3);
                                    //}
                                    //catch
                                    //{
                                    //    op_dist = "";
                                    //    op_taluka = "";
                                    //}

                                    string Abha_ID = reader2.GetValue(17).ToString().Replace(',', ':');
                                    string Image = @"\IMAGES\" + ben_pmjy_id + ".jpg";
                                    string QR = ben_pmjy_id + @"\n" + ben_name + @"\n" + State + @"\n" + District + @"\n" + Taluka + @"\n" + Village + @"\n" + @"\n" + yob + @"\n" + Gender + @"\n" + ben_mobile + @"\n" + r_u_ben;
                                    //string Card_PDF = "";
                                    string Sheet_No = "*" + num + "-of-" + sheetcount + "*";
                                    string IBN = num1 + " of " + innerboxcount;
                                    string OBN = num2 + " of " + outerboxcount;
                                    string Batch = "/GJ-BULK-01/" + op_dist_2 + "/" + op_taluka_2 + "/" + DateTime.Now.ToString("ddMMyyyy");
                                    string SrNo_with_Barcode = "*" + num + "-of-" + sheetcount + Batch + "*";
                                    int page_data = num1;
                                    datarows += counter + "," + ben_name + "," + ben_pmjy_id + "," + yob + "," + Gender + "," + Village + "," + Taluka + "," + District + "," + Image + "," + QR + "," + "," + Abha_ID + "," + Sheet_No + "," + SrNo_with_Barcode + "," + IBN + "," + OBN + "," + Batch + "\n";
                                    update(ben_pmjy_id, Sheet_No, SrNo_with_Barcode, IBN, OBN, Batch, page_data);

                                    mycount++;
                                    //mycount1++;
                                    //mycount2++;

                                    if (mycount % 24 == 0)
                                    {
                                        num = num + 1;
                                        // mycount = 0;
                                    }

                                    if (mycount % 240 == 0)
                                    {
                                        num1 = num1 + 1;
                                        //mycount1 = 0;
                                    }

                                    if (mycount % 1920 == 0)
                                    {
                                        num2 = num2 + 1;
                                        // mycount2 = 0;
                                    }

                                    //MessageBox.Show(reader2.GetValue(0).ToString() + "\n"
                                    //                + reader2.GetValue(1).ToString() + "\n"
                                    //                + reader2.GetValue(2).ToString());

                                    //  richTextBox2.Text += reader2.GetValue(0).ToString() + "\n\r" + reader2.GetValue(1).ToString() + "\n\r" + reader2.GetValue(2).ToString() + "\n\r\n\r";

                                }

                                con1.Close();
                            }
                            //  break;





                        }
                        con.Close();

                        //
                        //MessageBox.Show("NHA_DATA_" + state_name + "_" + op_district_name + ".csv created ");
                        //loaddata();
                        //Console.WriteLine("NHA_DATA_" + state_name + "_" + op_district_name + ".csv created ");
                    }
                    // Console.ReadKey();

                    WriteCSV(header, datarows, op_district_name, state_name);

                }
                loader.Close();
                MessageBox.Show("Done");
            }
        }
    }
}
