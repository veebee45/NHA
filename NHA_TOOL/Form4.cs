using NHA_TOOL.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Windows.Forms;
using DataTable = System.Data.DataTable;

//using System.IO.Path;

namespace NHA_TOOL
{
    public partial class Form4 : Form
    {

        public static int total_data_global = 0;
        private string summaryFilePath = ConfigurationManager.AppSettings["SummaryFilePath"];
        static string connectionString = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        public static string inputdirectory = string.Empty;
        private static int First_lot_ID = 0;

        System.Windows.Forms.RichTextBox myRichTextBox;

        string database_name = "";
        public Form4()
        {
            InitializeComponent();
            GetStateData();
        }
        private void GetStateData()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter("Select *from state_list", con))
                {

                    sda.Fill(dt);

                    //Insert the Default Item to DataTable.
                    DataRow row = dt.NewRow();
                    row[0] = 0;
                    row[1] = "---Please select---";
                    dt.Rows.InsertAt(row, 0);

                    cbxState.DataSource = dt;
                    cbxState.DisplayMember = "State";
                    cbxState.ValueMember = "ID";
                }
            }
        }
        private void btnBrowseFile_Click(object sender, EventArgs e)
        {

            List<string> selectedOptions_all = GetSelectedOptions();
            Global_Data.dashboard_selectedItems = string.Join(", ", selectedOptions_all);

            int lot_id_current = 0;

            string database_name = "DATA_NHA_BASE";

            OpenFileDialog fdlg = new OpenFileDialog();
            fdlg.Title = "Select file";
            fdlg.InitialDirectory = @"B:\DATA-FILES\NHA_TOOL\INPUt files\";
            fdlg.FileName = txtFilePath.Text;
            fdlg.Filter = "Text and CSV Files(*.txt, *.csv)|*.txt;*.csv|Text Files(*.txt)|*.txt|CSV Files(*.csv)|*.csv|All Files(*.*)|*.*";
            fdlg.FilterIndex = 1;
            fdlg.RestoreDirectory = true;

            if (fdlg.ShowDialog() == DialogResult.OK)
            {
                inputdirectory = System.IO.Path.GetDirectoryName(fdlg.FileName);
                txtFilePath.Text = inputdirectory;
            }


            inputdirectory += "\\";

            string[] csvFiles = Directory.GetFiles(inputdirectory, $"*{Global_Data.input_file_ext}");

            //All validations
            if(cbxState.SelectedIndex < 1)
            {
                MessageBox.Show("Please select state");

                txtFilePath.Clear();
            }
            else if(string.IsNullOrEmpty(Global_Data.input_file_ext))
            {
                MessageBox.Show("Please select a file extension");
                txtFilePath.Clear();
            }
            else if(string.IsNullOrEmpty(Global_Data.input_file_seprator))
            {
                MessageBox.Show("Please select a separator");
                txtFilePath.Clear();
            }
            else if(string.IsNullOrEmpty(txtFilePath.Text.Trim()))
            {
                MessageBox.Show("Please select a file");
                txtFilePath.Clear();
            }
            else if(csvFiles.Length < 1)
            {
                MessageBox.Show($"No file found at directory {inputdirectory}");
                txtFilePath.Clear();
            }
            else if(string.IsNullOrEmpty(Global_Data.validation_check))
            {
                MessageBox.Show("Please select atleast one validation");
                txtFilePath.Clear();
            }
            else if (cbxState.SelectedIndex > 0 && !string.IsNullOrEmpty(Global_Data.input_file_ext) && !string.IsNullOrEmpty(Global_Data.input_file_seprator) && !string.IsNullOrEmpty(txtFilePath.Text.Trim()) && csvFiles.Length > 0)
            {
                if (csvFiles.Length > 0)
                {
                    messageshow("Prcocessing starts");
                    //string[] csvFiles = { "144"};
                    lot_id_current = Int32.Parse(Database.sql_data_value("SELECT top 1 [Lot_ID] FROM " + database_name + "  order by [Lot_ID] desc ", "Lot_Id"));
                    First_lot_ID = lot_id_current + 1;
                    messageshow(lot_id_current.ToString());


                    foreach (string filename_1 in csvFiles)
                    {
                        string sourceFilePath = filename_1;

                        string main_filename = System.IO.Path.GetFileNameWithoutExtension(filename_1);

                        lot_id_current += 1;
                        messageshow(lot_id_current.ToString() + " lot id for district " + main_filename);

                        int splitterbatchSize = 2000;

                        string file_splitter = FIle_Splitter.SplitCSVFile(sourceFilePath, inputdirectory, splitterbatchSize);

                        if (file_splitter == "")
                        {
                            string data_insertion = Sql_Data_insertion.ImportFilesToDatabase_new(inputdirectory, database_name, lot_id_current);
                            messageshow("files inserted into database");
                        }
                        else
                        {
                            MessageBox.Show(file_splitter);
                        }
                    }
                    string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                        connection.Open();

                        using (SqlCommand command = new SqlCommand("AssignLotIDsToDistrict", connection))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@LotID", First_lot_ID);

                            int query_result = command.ExecuteNonQuery();
                            if (query_result == 0)
                            {
                                messageshow("Lot_id has been assign to district successfully");
                            }
                            else
                            {
                                messageshow("Error while assigning lotId to district ");
                            }
                        }
                    }
                    GerateSummaryXLS();
                    MessageBox.Show("Succcess");
                    File.Delete(inputdirectory + "Base_header_1.csv");

                }
                else
                {
                    MessageBox.Show($"No input files present wiht the extention {Global_Data.input_file_ext}");
                }
            }
            else
            {
                MessageBox.Show("Some validation issue occured");
            }
            

            //if (csvFiles.Length > 0)
            //{
            //    messageshow("Prcocessing starts");
            //    //string[] csvFiles = { "144"};
            //    lot_id_current = Int32.Parse(Database.sql_data_value("SELECT top 1 [Lot_ID] FROM " + database_name + "  order by [Lot_ID] desc ", "Lot_Id"));
            //    First_lot_ID = lot_id_current + 1;
            //    messageshow(lot_id_current.ToString());


            //    foreach (string filename_1 in csvFiles)
            //    {
            //        string sourceFilePath = filename_1;

            //        string main_filename = System.IO.Path.GetFileNameWithoutExtension(filename_1);

            //        lot_id_current += 1;
            //        messageshow(lot_id_current.ToString() + " lot id for district " + main_filename);

            //        int splitterbatchSize = 2000;

            //        string file_splitter = FIle_Splitter.SplitCSVFile(sourceFilePath, inputdirectory, splitterbatchSize);

            //        if (file_splitter == "")
            //        {
            //            string data_insertion = Sql_Data_insertion.ImportFilesToDatabase_new(inputdirectory, database_name, lot_id_current);
            //            messageshow("files inserted into database");
            //        }
            //        else
            //        {
            //            MessageBox.Show(file_splitter);
            //        }
            //    }
            //    string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
            //    using (SqlConnection connection = new SqlConnection(connectionString))
            //    {
            //        connection.Open();

            //        using (SqlCommand command = new SqlCommand("AssignLotIDsToDistrict", connection))
            //        {
            //            command.CommandType = CommandType.StoredProcedure;
            //            command.Parameters.AddWithValue("@LotID", First_lot_ID);

            //            int query_result= command.ExecuteNonQuery();
            //            if (query_result == 0)
            //            {
            //                messageshow("Lot_id has been assign to district successfully");
            //            }
            //            else 
            //            {
            //                messageshow("Error while assigning lotId to district ");
            //            }
            //        }
            //    }
            //    GerateSummaryXLS();
            //    MessageBox.Show("Succcess");
                
            //}
            //else
            //{
            //    MessageBox.Show($"No input files present wiht the extention {Global_Data.input_file_ext}");
            //}
            ////this.Hide();
            ////Dashboard dashboard = new Dashboard();
            ////dashboard.ShowDialog();
        }
        private List<string> GetSelectedOptions()
        {
            List<string> selectedOptions = new List<string>();

            //Extention of the input file
            if (cbtxt.Checked)
            {
                selectedOptions.Add("txt");
                Global_Data.input_file_ext = ".txt";
            }
            else if (cbcsv.Checked)
            {
                selectedOptions.Add("csv");
                Global_Data.input_file_ext = ".csv";
            }

            //seperator type
            if (cbpipe.Checked)
            {
                selectedOptions.Add("|");
                Global_Data.input_file_seprator = "|";
            }
            else if (cbtilt.Checked)
            {
                selectedOptions.Add("~");
                Global_Data.input_file_seprator = "~";
            }
            else if (cbslash.Checked)
            {
                selectedOptions.Add("/");
                Global_Data.input_file_seprator = "/";
            }


            ////images extension type
            //if (cbjpg.Checked)
            //{
            //    selectedOptions.Add("jpg");
            //    Global_Data.image_file_ext = "jpg";
            //}
            //if (cbjpeg.Checked)
            //{
            //    selectedOptions.Add("jpeg");
            //    Global_Data.image_file_ext = "jpeg";
            //}
            //if (cbpng.Checked)
            //{
            //    selectedOptions.Add("png");
            //    Global_Data.image_file_ext = "png";
            //}

            //validation
            if (cbname.Checked)
            {
                selectedOptions.Add("name_ben");
                Global_Data.validation_check += "name_ben";
            }
            if (cbdob.Checked)
            {
                selectedOptions.Add("dob");
                Global_Data.validation_check += "dob";
            }
            if (cbpmj.Checked)
            {
                selectedOptions.Add("pmjayid");
                Global_Data.validation_check += "pmjayid";
            }
            if (cbabhaid.Checked)
            {
                selectedOptions.Add("abhaid");
                Global_Data.validation_check += "abhaid";
            }
            if (cbpic.Checked)
            {
                selectedOptions.Add("dobpic");
                Global_Data.validation_check += "dobpic";
            }
            if (cbvillage.Checked)
            {
                selectedOptions.Add("village");
                Global_Data.validation_check += "village";
            }
            if (cbdistrict.Checked)
            {
                selectedOptions.Add("district");
                Global_Data.validation_check += "district";
            }
            if (cbstate.Checked)
            {
                selectedOptions.Add("state");
                Global_Data.validation_check += "state";
            }
            if (cbblock.Checked)
            {
                selectedOptions.Add("block");
                Global_Data.validation_check += "block";
            }
            if (cbgender.Checked)
            {
                selectedOptions.Add("gender");
                Global_Data.validation_check += "gender";
            }



            return selectedOptions;
        }
        public void messageshow(string message)
        {


            //System.Windows.Forms.RichTextBox myRichTextBox;
            ////System.Windows.Forms.RichTextBox myRichTextBox;
            //myRichTextBox = new System.Windows.Forms.RichTextBox();
            //myRichTextBox.Location = new System.Drawing.Point(421, 173); // Set the desired location
            //myRichTextBox.Size = new System.Drawing.Size(265, 260);    // Set the desired size


            ////this.richTextBox2.Location = new System.Drawing.Point(421, 173);
            ////this.richTextBox2.Name = "richTextBox2";
            ////this.richTextBox2.Size = new System.Drawing.Size(265, 260);
            ////this.richTextBox2.TabIndex = 48;
            ////this.richTextBox2.Text = "";

            //// Configure other properties and event handlers as needed
            //this.Controls.Add(myRichTextBox);


            //myRichTextBox.BeginInvoke((System.Action)(() =>
            //{
            //    myRichTextBox.AppendText("d thread.");
            //}));

            ////myRichTextBox.Invoke((System.Action)(() =>
            ////{
            ////    myRichTextBox.AppendText($"{message}");
            ////})); // Add the control to the form's controls collection

            //richTextBox1.Text = $"{message}";

            richTextBox1.Text += $"{message}";
            richTextBox1.Text += "\r\n";
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();
            richTextBox1.Refresh();

        }

        private void Form4_Load(object sender, EventArgs e)
        {

        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void GerateSummaryXLS()
        {
            //for testting
            //_lotID = 1;
            if (First_lot_ID > 0)
            {
                try
                {
                    if (!Directory.Exists(summaryFilePath))
                        Directory.CreateDirectory(summaryFilePath);

                    DataSet ds = new DataSet();
                    string currentDate = DateTime.Now.ToString("ddMMyyyy");

                    using (SqlConnection con = new SqlConnection(connectionString))
                    {
                        con.Open();
                        using (SqlCommand cmd = new SqlCommand("Summary_report", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@lotid", First_lot_ID);

                            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                            {
                                sda.Fill(ds);
                            }
                        }
                    }

                    dgvDistrict.DataSource = ds.Tables[0];
                    dgvBlock.DataSource = ds.Tables[1];
                    dgvVillage.DataSource = ds.Tables[2];
                    dgvFlag.DataSource = ds.Tables[3];

                    ExportToXLS(First_lot_ID, dgvDistrict, summaryFilePath + @"\" + First_lot_ID + $"_DistricWiseSmry_{currentDate}.xls");
                    ExportToXLS(First_lot_ID, dgvBlock, summaryFilePath + @"\" + First_lot_ID + $"_BlockWiseSmry_{currentDate}.xls");
                    ExportToXLS(First_lot_ID, dgvVillage, summaryFilePath + @"\" + First_lot_ID + $"_VillageWiseSmry_{currentDate}.xls");
                    ExportToXLS(First_lot_ID, dgvFlag, summaryFilePath + @"\" + First_lot_ID + $"_FlagSmry_{currentDate}.xls");


                    MessageBox.Show("Exported successfully");

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                MessageBox.Show("LotID not find");
            }
        }
        private void ExportToXLS(int lotid, DataGridView dataGridView, string filepath)
        {
            string currentDate = DateTime.Now.ToString("ddMMyyyy");
            // creating Excel Application  
            Microsoft.Office.Interop.Excel._Application app = new Microsoft.Office.Interop.Excel.Application();
            // creating new WorkBook within Excel application  
            Microsoft.Office.Interop.Excel._Workbook workbook = app.Workbooks.Add(Type.Missing);
            // creating new Excelsheet in workbook  
            Microsoft.Office.Interop.Excel._Worksheet worksheet = null;
            // see the excel sheet behind the program  
            app.Visible = false;
            // get the reference of first sheet. By default its name is Sheet1.  
            // store its reference to worksheet  
            worksheet = workbook.Sheets["Sheet1"];
            worksheet = workbook.ActiveSheet;
            // changing the name of active sheet  
            worksheet.Name = "Exported from gridview";
            // storing header part in Excel  
            for (int i = 1; i < dataGridView.Columns.Count + 1; i++)
            {
                worksheet.Cells[1, i] = dataGridView.Columns[i - 1].HeaderText;
            }
            // storing Each row and column value to excel sheet  
            for (int i = 0; i < dataGridView.Rows.Count - 1; i++)
            {
                for (int j = 0; j < dataGridView.Columns.Count; j++)
                {
                    worksheet.Cells[i + 2, j + 1] = dataGridView.Rows[i].Cells[j].Value.ToString();
                }
            }
            // save the application  
            workbook.SaveAs(filepath, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlExclusive, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            // Exit from the application  
            app.Quit();
        }

        private void btnExportReport_Click(object sender, EventArgs e)
        {
            GerateSummaryXLS();
        }

        private void btnStartProcessing_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.ShowDialog();
        }

        private void cbxState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if(cbxState.SelectedIndex > 0)
            {
                Global_Data.state_name = cbxState.Text;
            }
        }
    }
}
