using NHA_TOOL.Classes;
using NHA_TOOL.Forms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;
using ZXing;

namespace NHA_TOOL
{
    public partial class Dashboard : Form
    {
        public static int total_data_global = 0;
        static string connectionString = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        private string summaryFilePath = ConfigurationManager.AppSettings["SummaryFilePath"];
        private string generatedFilePath = ConfigurationManager.AppSettings["GeneratedFilePath"];
        public static string folderPath="";
        public static string state="";
        private static int lotid = 0;
        private static int printlotid = 0;

        List<string> listItems = new List<string>();
        List<int> lotIds = new List<int>();
        public Dashboard()
        {
            InitializeComponent();
            GetStateData();
            GetUnprocessedStateData();

            //if (GetLotID() != null)
            //    lotIds = GetLotID().ToList();
            //else
            //    System.Windows.Forms.MessageBox.Show("No data found for processing");
        }
        private void GetUnprocessedStateData()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT ROW_NUMBER() OVER (ORDER BY state_name_english) AS Srn, state_name_english,MIN(Lot_ID) As firstLotId,MAX(Lot_ID) As lastLotId,STRING_AGG(district_name_english, ',') AS unique_districts FROM data_nha_Base where isnull(flag,'') ='' AND Lot_ID NOT IN(SELECT Lot_Id FROM Filtered_data group by Lot_Id) group by state_name_english", con))
                {

                    sda.Fill(dt);

                    //Insert the Default Item to DataTable.
                    DataRow row = dt.NewRow();
                    row[0] = 0;
                    row[1] = "---Please select---";
                    dt.Rows.InsertAt(row, 0);

                    cbxUnprocessedState.DataSource = dt;
                    cbxUnprocessedState.DisplayMember = "state_name_english";
                    cbxUnprocessedState.ValueMember = "Srn";
                }
            }
        }

        private void GetDistrictData()
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter("SELECT  ROW_NUMBER() OVER (ORDER BY district_name_english) AS Srn, district_name_english,MIN(Lot_ID) As firstLotId,MAX(Lot_ID) As lastLotId,STRING_AGG(district_name_english, ',') AS unique_districts FROM data_nha_Base where isnull(flag,'') ='' AND Lot_ID NOT IN(SELECT Lot_Id FROM Filtered_data group by Lot_Id) group by district_name_english", con))
                {

                    sda.Fill(dt);

                    //Insert the Default Item to DataTable.
                    DataRow row = dt.NewRow();
                    row[0] = 0;
                    row[1] = "---Please select---";
                    dt.Rows.InsertAt(row, 0);

                    cbxDistrict.DataSource = dt;
                    cbxDistrict.DisplayMember = "district_name_english";
                    cbxDistrict.ValueMember = "firstLotId";
                }
            }
        }
        private void GetStateData()
        {
            DataTable dt = new DataTable();

            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using(SqlDataAdapter sda = new SqlDataAdapter("Select *from StateConfiguration", con))
                {

                    sda.Fill(dt);

                    //Insert the Default Item to DataTable.
                    DataRow row = dt.NewRow();
                    row[0] = 0;
                    row[1] = "---Please select---";
                    dt.Rows.InsertAt(row, 0);

                    cbxState.DataSource = dt;
                    cbxState.DisplayMember = "StateName";
                    cbxState.ValueMember = "ID";
                }
            }
        }
        private static List<int> GetLotID()
        {
            List<int> idList = new List<int>();

            SqlDataReader reader = null;

            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT Lot_Id FROM data_nha_Base WHERE isnull(flag,'') ='' and Lot_ID NOT IN(SELECT Lot_Id FROM Filtered_data group by Lot_Id) group by Lot_ID", con))
                {
                    cmd.CommandType = CommandType.Text;
                    reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        idList.Add(reader.GetInt32(0));
                    }
                }
            }
            return idList;
        }
        public static string DecodeQRCode(string imagePath)
        {
            try
            {
                BarcodeReader reader = new BarcodeReader();
                Result result = reader.Decode(new Bitmap(imagePath));
                if (result != null)
                {
                    return result.Text;
                }
                return null;
            }
            catch (Exception ex)
            {
                // Handle exceptions, such as file not found or decoding errors
                Console.WriteLine("Error decoding QR code: " + ex.Message);
                return null;
            }
        }

        private void GetRecoudsByOrder(string order)
        {
            DataTable dt = new DataTable();

            using(SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using(SqlCommand cmd = new SqlCommand("Nha_test",con))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@sorting", order.Replace('|',','));

                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
                
            }
        }
   
        public void messageshow(string message)
        {
            richTextBox1.Text += $"{message}";
            richTextBox1.Text += "\r\n";
            richTextBox1.SelectionStart = richTextBox1.TextLength;
            richTextBox1.ScrollToCaret();
            richTextBox1.Refresh();
        }


        private void checkedListBoxItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool state;
            if (checkedListBoxItems.GetItemCheckState(checkedListBoxItems.SelectedIndex) == CheckState.Checked)
            {
                state = true;
                this.checkedListBoxItems.SetItemChecked(checkedListBoxItems.SelectedIndex, !state);
            }
            else
            {
                state = false;
                this.checkedListBoxItems.SetItemChecked(checkedListBoxItems.SelectedIndex, !state);
            }


            if (checkedListBoxItems.CheckedItems.Count != 0)
            {
                // If so, loop through all checked items and print results.  
                for (int x = 0; x <= checkedListBoxItems.CheckedItems.Count - 1; x++)
                {
                    if (!listItems.Contains(checkedListBoxItems.CheckedItems[x].ToString()))
                        listItems.Add(checkedListBoxItems.CheckedItems[x].ToString());
                }
            }

            lblSortingInput.Text = String.Join("|", listItems);

        }

        private void checkedListBoxItems_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (e.NewValue == CheckState.Unchecked)
            {
                listItems.Remove(checkedListBoxItems.Items[e.Index].ToString());
            }
            lblSortingInput.Text = String.Join("|", listItems);
        }

        private void btnProcessing_Click(object sender, EventArgs e)
        {
            string database_name = "DATA_NHA_BASE";
            string processing_database_name = "Filtered_data";

            string stateName = (gbNewState.Visible == true && !string.IsNullOrEmpty(txtState.Text.Trim()))? txtState.Text : cbxState.Text;
            string imgExtension = string.Empty;
            string innerboxQty = txtInnerboxQty.Text;
            string outerboxQty = txtOuterboxQty.Text;
            string sortingOrder = lblSortingInput.Text;
            state = cbxUnprocessedState.Text;
            if(cbxDistrict.SelectedIndex > 0)
            {
                //lotIds = GetLotID();
                lotIds.Add(Convert.ToInt32(cbxDistrict.SelectedValue));
            }

            if (cbjpeg.Checked == true)
                imgExtension = cbjpeg.Text;
            else if (cbjpg.Checked == true)
                imgExtension = cbjpg.Text;
            else if (cbpng.Checked == true)
                imgExtension = cbpng.Text;
            else
                imgExtension = cbpng.Text;

            Global_Data.image_file_ext = "."+imgExtension;

            try
            {
                if (lotIds.Count > 0)
                {
                    if(cbxUnprocessedState.SelectedIndex < 1 || cbxDistrict.SelectedIndex < 1)
                    {
                        MessageBox.Show("Please select State and district first!");
                    }
                    else if(string.IsNullOrEmpty(stateName))
                    {
                        System.Windows.Forms.MessageBox.Show("State name not provided");
                    }
                    else if(string.IsNullOrEmpty(imgExtension))
                    {
                        System.Windows.Forms.MessageBox.Show("Please select file extension");
                    }
                    else if(string.IsNullOrEmpty(innerboxQty))
                    {
                        System.Windows.Forms.MessageBox.Show("Please provide inner box quantity");
                    }
                    else if(string.IsNullOrEmpty(outerboxQty))
                    {
                        System.Windows.Forms.MessageBox.Show("Please provide outer box quantity");
                    }
                    else if(string.Equals(lblSortingInput.Text, "Null") || lblSortingInput.Text.Length == 0)
                    {
                        System.Windows.Forms.MessageBox.Show("Please select atleast one item from sorting list");
                    }
                    else
                    {
                        if (gbNewState.Visible == true && !string.IsNullOrEmpty(txtState.Text.Trim()))
                        {
                            stateName = txtState.Text;

                            //save new state
                            using (SqlConnection con = new SqlConnection(connectionString))
                            {
                                con.Open();
                                using (SqlCommand cmd = new SqlCommand("usp_SaveStateConfiguration", con))
                                {
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    cmd.Parameters.AddWithValue("@StateName", stateName);
                                    cmd.Parameters.AddWithValue("@InnerBoxQty", innerboxQty);
                                    cmd.Parameters.AddWithValue("@OuterBoxQty", outerboxQty);
                                    cmd.Parameters.AddWithValue("@ImageExtension", imgExtension);
                                    cmd.Parameters.AddWithValue("@ShortingOrder", sortingOrder);
                                    cmd.ExecuteReader();
                                }
                            }

                        }
                        else if (cbxState.Visible = true && cbxState.SelectedIndex > 0)
                        {
                            stateName = cbxState.Text;
                        }

                        sortingOrder = lblSortingInput.Text;
                        GetRecoudsByOrder(sortingOrder);

                        for (int i = 0; i < lotIds.Count; i++)
                        {
                            string district_name = Database.sql_data_value("SELECT distinct [district_name_english] FROM " + database_name + " where Lot_ID =  '" + lotIds[i] + "' ", "district_name_english");

                            int total_data = Int32.Parse(Database.sql_data_value("SELECT count(*) as data_count FROM " + database_name + " where Lot_ID =  '" + lotIds[i] + "' and flag not like '%R%' ", "data_count"));

                            total_data_global = total_data;
                            string strFilePath = generatedFilePath+"\\" + DateTime.Now.ToString("dd.MM.yyyy") + "\\" + district_name + "\\";
                            if (!Directory.Exists(strFilePath)) //checking for filegeneration folder
                            {
                                Directory.CreateDirectory(strFilePath);//creating for filegeneration folder
                            }


                            string outputDirectory_image = strFilePath + "IMAGES\\";
                            string outputDirectory_qr = strFilePath + "QR\\";
                            folderPath = strFilePath;
                            if (!Directory.Exists(outputDirectory_image))// checking for image folder
                            {
                                Directory.CreateDirectory(outputDirectory_image);//creating for image folder
                            }
                            if (!Directory.Exists(outputDirectory_qr)) //checking for qr folder
                            {
                                Directory.CreateDirectory(outputDirectory_qr);//creating for qr folder
                            }

                            messageshow($"Image conversion starts for {district_name} total data count {total_data}");
                            image_conversion.image_generator_thread_new(outputDirectory_image, outputDirectory_qr, database_name, lotIds[i]);
                            messageshow("Image generated successfully");

                            //image folder zip
                            string folderToZip = strFilePath + "IMAGES";
                            string zipFilePath = strFilePath + "IMAGES.zip";
                            ZipDirectory(folderToZip, zipFilePath);
                            messageshow($"{folderToZip}\n {zipFilePath} ");
                            // qr folder zip
                            folderToZip = strFilePath + "QR";
                            zipFilePath = strFilePath + "QR.zip";
                            ZipDirectory(folderToZip, zipFilePath);
                            messageshow($"{folderToZip}\n {zipFilePath} ");


                            //inserting data into filtered tabel after every necessary validation...
                            int filtered_data_insertion_status = Filtered_data.filtered_data_insertion(database_name, lotIds[i]);
                            if (filtered_data_insertion_status == 1)
                            {
                                messageshow("Data Inserted into filtered table ");
                            }

                            int unfiltered_data_insertion_status = Filtered_data.unfiltered_data_insertion(database_name, lotIds[i]);
                            if (unfiltered_data_insertion_status == 1)
                            {
                                messageshow("Data Inserted into unfiltered table ");
                            }

                            // now onwards databse used for processing will be filtered data....

                            int filtered_data_prcocessing_status = Filtered_data.filtered_data_prcocessing(processing_database_name, lotIds[i],Convert.ToInt32(innerboxQty),Convert.ToInt32(outerboxQty), sortingOrder);
                            if (filtered_data_prcocessing_status == 1)
                            {
                                messageshow("Filtered data processed successfully");

                            }

                            Generate_files.Generate_prepress_files(lotIds[i], processing_database_name, district_name, strFilePath, stateName);
                            messageshow($"{district_name} prepress generated successfully");
                            Generate_files.Generate_mis_files(lotIds[i], processing_database_name, district_name, strFilePath, stateName);
                            messageshow($"{district_name} mis generated successfully");
                            Generate_files.Generate_packing_list_files(lotIds[i], processing_database_name, district_name, strFilePath);
                            messageshow($"{district_name} packaging list generated successfully");
                            Generate_files.Generate_inner_label_files(lotIds[i], processing_database_name, district_name, strFilePath);
                            messageshow($"{district_name} inner   label file generated successfully");

                            Generate_files.Generate_outter_label_files(lotIds[i], processing_database_name, district_name, strFilePath);
                            messageshow($"{district_name} outter label file generated successfully");

                        }
                        GerateSummaryXLS(lotIds[0]);
                        printlotid = lotIds[0];
                        cbxState.SelectedIndex = 0;
                        System.Windows.Forms.MessageBox.Show("Success");
                        btnProcessing.Enabled = false;
                        btnPrinting.Visible = true;
                        ViewGenerateFileSummary viewGenerateFileSummary = new ViewGenerateFileSummary();
                        viewGenerateFileSummary.ShowDialog();
                        //clear list after use
                        lotIds.Clear();
                    }
                }
                else
                {
                    System.Windows.Forms.MessageBox.Show("No data found for processing");
                }
                

            }
            catch(Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }
        }
        private void GerateSummaryXLS(int firstLotId)
        {
            if (firstLotId > 0)
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
                        using (SqlCommand cmd = new SqlCommand("usp_SepratorSummary", con))
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.AddWithValue("@lotid", firstLotId);

                            using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                            {
                                sda.Fill(ds);
                            }
                        }
                    }


                    dgvSeparator.DataSource = ds.Tables[0];


                    ExportToXLS(firstLotId, dgvSeparator, summaryFilePath + @"\" + firstLotId + $"_SeparatorWiseSmry_{currentDate}.xls");

                    System.Windows.Forms.MessageBox.Show("Exported successfully");

                }
                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message);
                }
            }
            else
            {
                System.Windows.Forms.MessageBox.Show("LotID not find");
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

        public static void ZipDirectory(string folderPath, string zipFilePath)
        {
            try
            {
                ZipFile.CreateFromDirectory(folderPath, zipFilePath);
                Console.WriteLine($"Successfully zipped folder: {folderPath} -> {zipFilePath}");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error while zipping folder: {e.Message}");
            }
        }

        private void pbClose_Click(object sender, EventArgs e)
        {
            this.Close();
            System.Windows.Forms.Application.Exit();
        }

        private void cbxState_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = cbxState.SelectedIndex;
            if (index > 0)
            {
                Reset();
                PopulateDataStatewise(index);
                txtInnerboxQty.ReadOnly = true;
                txtOuterboxQty.ReadOnly = true;
                checkedListBoxItems.Enabled = false;


            }
            else if(cbxState.SelectedIndex == 0) // if selected index 0 clear previos
            {
                Reset();
                txtInnerboxQty.ReadOnly = false;
                txtOuterboxQty.ReadOnly = false;
                checkedListBoxItems.Enabled = true;
            }
        }
        private void Reset()
        {
            cbjpeg.Checked = false;
            cbjpg.Checked = false;
            cbpng.Checked = false;

            txtInnerboxQty.Clear();
            txtOuterboxQty.Clear();

            for (int i = 0; i < checkedListBoxItems.Items.Count; i++)
            {
                checkedListBoxItems.SetItemChecked(i, false);
            }

            lblSortingInput.Text = null;

            //bool state = false;

            //for (int i = 0; i < checkedListBoxItems.Items.Count; i++)
            //{
            //    checkedListBoxItems.SetItemCheckState(i, (state ? CheckState.Checked : CheckState.Unchecked));

            //}
        }
        private void PopulateDataStatewise(int id)
        {
            DataTable dt = new DataTable();

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                using (SqlDataAdapter sda = new SqlDataAdapter($"Select *from StateConfiguration WHERE id ={id}", con))
                {
                    sda.Fill(dt);
                }
            }
            if(dt.Rows.Count > 0)
            {
                string  imgExtension = dt.Rows[0]["ImageExtension"].ToString();

                if (imgExtension == cbjpeg.Text)
                {
                    cbjpeg.Checked = true;
                    cbjpeg.Enabled = true;
                    cbjpg.Enabled = false; 
                    cbpng.Enabled=false;
                    
                }  
                else if (imgExtension == cbjpg.Text)
                {
                    cbjpg.Checked = true;
                    cbjpg.Enabled = true;
                    cbpng.Enabled = false;
                    cbjpeg.Enabled = false;
                }
                else if (imgExtension == cbpng.Text)
                {
                    cbpng.Checked = true;
                    cbpng.Enabled = true;
                    cbjpg.Enabled = false;
                    cbjpeg.Enabled = false;
                }
                    

                Global_Data.image_file_ext = imgExtension;

                txtInnerboxQty.Text = dt.Rows[0]["InnerBoxQty"].ToString();
                txtOuterboxQty.Text = dt.Rows[0]["OuterBoxQty"].ToString();

                string[] arr = dt.Rows[0]["ShortingOrder"].ToString().Split('|');

                foreach(string item in arr)
                {
                    int index = checkedListBoxItems.FindString(item);

                    checkedListBoxItems.SetSelected(index, true);
                }
            }
        }

        private void pbAddState_Click(object sender, EventArgs e)
        {
            gbExistingState.Visible = false;
            cbxState.SelectedIndex = 0;
            cbpng.Enabled = true;
            cbjpg.Enabled = true;
            cbjpeg.Enabled = true;

            gbNewState.Visible = true;
        }

        private void pbReturn_Click(object sender, EventArgs e)
        {
            gbExistingState.Visible = true;

            cbxState.SelectedIndex = 0;

            gbNewState.Visible = false;

        }

        private void cbxUnprocessedState_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxUnprocessedState.SelectedIndex > 0)
            {
                GetDistrictData();
            }
            if (cbxUnprocessedState.SelectedIndex == 0)
            {
                List<string> myList = new List<string>();
                myList.Add("---Please select---");

                cbxDistrict.DataSource = myList;
            }
        }

        private void btnPrinting_Click(object sender, EventArgs e)
        {
            this.Hide();
            NHA_24UPS card_print = new NHA_24UPS(folderPath,printlotid,state);
            card_print.ShowDialog();
            
        }
    }
}
