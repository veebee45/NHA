

using CsvHelper;
using CsvHelper.Configuration;
using NHA_TOOL.Classes;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;



internal class Sql_Data_insertion
{

    public static string val_check = Global_Data.dashboard_selectedItems;
    private System.Windows.Forms.RichTextBox richTextBox1;
    public static bool IsNull(string input)
    {
        return (Regex.IsMatch(input, @"^\s*$") || (input.ToUpper() == "NA") || (input.ToUpper() == "NULL"));
    }

    private static void Error_log(string sExceptionName, string id, string filename)
    {
        StreamWriter log;
        if (!File.Exists("Error_File.txt"))
        {
            log = new StreamWriter("Error_File.txt");
        }
        else
        {
            log = File.AppendText("Error_File.txt");
        }
        log.WriteLine("Data Time:" + DateTime.Now + " : " + sExceptionName + " : " + id + " : " + filename);

        //MessageBox.Show("Data Time:" + DateTime.Now + "  " + id + " : " + sExceptionName + "   File_Name : " + filename);

        log.Close();
    }

   

   



    public static string ImportFilesToDatabase_new(string outputDirectory, string data_table_name, int lotid)

    {
        System.Windows.Forms.RichTextBox richTextBox1 = new System.Windows.Forms.RichTextBox();
        string connectionString = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;
        string output_1 = "";
        //string outputDirectory_image = outputDirectory + @"\IMAGES_1\";
        //Console.WriteLine(outputDirectory_image);

        string[] csvFiles = Directory.GetFiles(outputDirectory, "*_split_*.csv");
        string gender_val = "", dob_val = "";
        string flag;

        //int batchSize = 10000, i = 0;

        //int table_check = nha_create_table(data_table_name);

        foreach (string file in csvFiles)

        {
            //string connectionString = "Data Source=your_server;Initial Catalog=your_database;User ID=your_username;Password=your_password;";
            string filePath = file;

            string sourceFilePath = filePath;
            string baseFilePath_original = "Base_header.csv";
            string inputdirectory_input = System.IO.Path.GetDirectoryName(filePath);
            string baseFilePath = inputdirectory_input + "\\" + System.IO.Path.GetFileNameWithoutExtension(filePath) + "_Base_header_1.csv";
            //string baseFilePath = "Base_header.csv";

            // Read the CSV file, clean lines, and write back to the same file
            CleanAndWriteCSV(filePath);



            try
            {
                // Check if the source file exists
                if (File.Exists(baseFilePath_original))
                {
                    // Copy the file
                    File.Copy(baseFilePath_original, baseFilePath);

                    Console.WriteLine("CSV file copied successfully.");
                }
                else
                {
                    Console.WriteLine("Source CSV file does not exist.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }


            UpdateSourceFileHeaders(sourceFilePath);
            var (sourceColumnHeaders, cleanedData) = GetCleanedData(sourceFilePath);
            var baseColumnHeaders = GetColumnHeaders(baseFilePath);
            if (headersMatch(sourceColumnHeaders, baseColumnHeaders))
            {
                var mappedData = MapData(cleanedData, baseColumnHeaders);
                WriteToBaseCsv(baseFilePath, baseColumnHeaders, mappedData);
            }
            else
            {
                var incorrectHeaders = GetIncorrectHeaders(baseColumnHeaders, sourceColumnHeaders);
                if (incorrectHeaders.Any())
                {

                    richTextBox1.Text+=(string.Join(", ", incorrectHeaders) + "  is incorrect in the source file" + filePath);
                    output_1 += (string.Join(", ", incorrectHeaders) + "  is incorrect in the source file" + filePath);
                }
                return output_1;
            }

            
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();

                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                {
                    // Set the destination table name
                    bulkCopy.DestinationTableName = data_table_name;

                    // Map the columns in the file to the columns in the database table
                    bulkCopy.ColumnMappings.Add("pmrssm_id", "pmrssm_id");
                    bulkCopy.ColumnMappings.Add("name_ben", "name_ben");
                    bulkCopy.ColumnMappings.Add("dob_ben", "dob_ben");
                    bulkCopy.ColumnMappings.Add("gender_ben", "gender_ben");
                    bulkCopy.ColumnMappings.Add("address_ben", "address_ben");
                    bulkCopy.ColumnMappings.Add("state_codelgd_ben", "state_codelgd_ben");
                    bulkCopy.ColumnMappings.Add("district_codelgd_ben", "district_codelgd_ben");
                    bulkCopy.ColumnMappings.Add("subdistrict_codelgd_ben", "subdistrict_codelgd_ben");
                    bulkCopy.ColumnMappings.Add("villagetown_code_lgd_ben", "villagetown_code_lgd_ben");
                    bulkCopy.ColumnMappings.Add("rural_urban_ben", "rural_urban_ben");
                    bulkCopy.ColumnMappings.Add("doc_pic", "doc_pic");
                    bulkCopy.ColumnMappings.Add("ahl_hhid", "ahl_hhid");
                    bulkCopy.ColumnMappings.Add("health_id", "health_id");
                    bulkCopy.ColumnMappings.Add("mobile_member", "mobile_member");
                    bulkCopy.ColumnMappings.Add("care_of_dec", "care_of_dec");
                    bulkCopy.ColumnMappings.Add("userid", "userid");
                    bulkCopy.ColumnMappings.Add("user_name", "user_name");
                    bulkCopy.ColumnMappings.Add("user_districtcode", "user_districtcode");
                    bulkCopy.ColumnMappings.Add("user_statecode", "user_statecode");
                    bulkCopy.ColumnMappings.Add("user_blockcode", "user_blockcode");
                    bulkCopy.ColumnMappings.Add("user_villagecode", "user_villagecode");
                    bulkCopy.ColumnMappings.Add("user_towncode", "user_towncode");
                    bulkCopy.ColumnMappings.Add("user_wardcode", "user_wardcode");
                    bulkCopy.ColumnMappings.Add("source_of_data", "source_of_data");
                    bulkCopy.ColumnMappings.Add("app_flag", "app_flag");
                    bulkCopy.ColumnMappings.Add("pmrssm_id2", "pmrssm_id2");
                    bulkCopy.ColumnMappings.Add("ahl_hhid2", "ahl_hhid2");
                    bulkCopy.ColumnMappings.Add("state_codelgd_ben2", "state_codelgd_ben2");
                    bulkCopy.ColumnMappings.Add("district_codelgd_ben2", "district_codelgd_ben2");
                    bulkCopy.ColumnMappings.Add("subdistrict_codelgd_ben2", "subdistrict_codelgd_ben2");
                    bulkCopy.ColumnMappings.Add("villagetown_code_lgd_ben2", "villagetown_code_lgd_ben2");
                    bulkCopy.ColumnMappings.Add("state_name_english", "state_name_english");
                    bulkCopy.ColumnMappings.Add("district_name_english", "district_name_english");
                    bulkCopy.ColumnMappings.Add("block_name_english", "block_name_english");
                    bulkCopy.ColumnMappings.Add("village_name_english", "village_name_english");
                    bulkCopy.ColumnMappings.Add("IBN", "IBN");
                    bulkCopy.ColumnMappings.Add("OBN", "OBN");
                    bulkCopy.ColumnMappings.Add("Sheet_no", "Sheet_no");
                    bulkCopy.ColumnMappings.Add("IBN_max", "IBN_max");
                    bulkCopy.ColumnMappings.Add("OBN_max", "OBN_max");
                    bulkCopy.ColumnMappings.Add("Sheet_max", "Sheet_max");
                    bulkCopy.ColumnMappings.Add("Seperator", "Seperator");
                    bulkCopy.ColumnMappings.Add("yob", "yob");
                    bulkCopy.ColumnMappings.Add("File_Name", "File_Name");
                    bulkCopy.ColumnMappings.Add("Upload_Date_Time", "Upload_Date_Time");
                    bulkCopy.ColumnMappings.Add("FLAG", "FLAG");
                    bulkCopy.ColumnMappings.Add("counter", "counter");
                    bulkCopy.ColumnMappings.Add("pdf_page_1", "pdf_page_1");
                    bulkCopy.ColumnMappings.Add("pdf_page_2", "pdf_page_2");
                    bulkCopy.ColumnMappings.Add("Lot_ID", "Lot_ID");
                    // Add more mappings for other columns as needed

                    // Set any additional options
                    bulkCopy.BatchSize = 15000;  // Number of rows to be sent in each batch
                    bulkCopy.BulkCopyTimeout = 0; // Timeout in seconds

                   

                    try
                    {
                        // Read the data from the file into a DataTable
                        
                        //DataTable data = ReadDataFromFile(baseFilePath, lotid);
                        
                        
                        DataTable data = read_dynamic(baseFilePath, lotid);
                        
                        // Perform the bulk copy
                        bulkCopy.WriteToServer(data);

                        Console.WriteLine("Bulk import completed successfully.");

                        richTextBox1.Text += $"{filePath} data inserted into database table {data_table_name}";
                        richTextBox1.Text += "\r\n";
                        richTextBox1.SelectionStart = richTextBox1.TextLength;
                        richTextBox1.ScrollToCaret();
                        richTextBox1.Refresh();

                        try { System.IO.File.Delete(filePath); }
                        catch (Exception ex)
                        {
                            Console.WriteLine("An error occurred: " + ex.Message);
                            Error_log(ex.Message, "", filePath);

                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred while Bulk import : " + ex.Message);
                        Error_log(ex.Message, "", filePath);
                        connection.Close();
                    }
                
                }
                connection.Close();
                
                

            }
            messageshow($"{filePath} file inserted into database");
            File.Delete(baseFilePath);
        }


        richTextBox1.Dispose();

        return output_1;
    }


    //public static DataTable ReadDataFromFile(string filePath, int lot_id_current)
    //{
    //    // Implement the logic to read data from the file and return it as a DataTable
    //    // You can use libraries like CsvHelper or read data manually based on your file format

    //    // Example code to create a DataTable and populate data from a CSV file using CsvHelper
    //    //DataTable data = new DataTable();
    //    //using (var reader = new StreamReader(filePath))
    //    //using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
    //    //{
    //    //    csv.Read();
    //    //    csv.ReadHeader();

    //    //    foreach (string header in csv.HeaderRecord)
    //    //    {
    //    //        data.Columns.Add(header);
    //    //    }

    //    //    while (csv.Read())
    //    //    {
    //    //        DataRow row = data.NewRow();
    //    //        foreach (DataColumn column in data.Columns)
    //    //        {
    //    //            row[column.ColumnName] = csv.GetField(column.DataType, column.ColumnName);
    //    //        }
    //    //        data.Rows.Add(row);
    //    //    }
    //    //}
    //    string maininputfile = System.IO.Path.GetFileNameWithoutExtension(filePath);
    //    string[] values_maininputfile = maininputfile.Split('_');
    //    maininputfile = values_maininputfile[0];
    //    string gender_val = "", dob_val = "";
    //    string flag;

    //    System.Data.DataTable tbl = new System.Data.DataTable();
    //    //System.Data.DataTable tbl = new System.Data.DataTable();
    //    tbl.Columns.Add("pmrssm_id", typeof(string));
    //    tbl.Columns.Add("name_ben", typeof(string));
    //    tbl.Columns.Add("dob_ben", typeof(string));
    //    tbl.Columns.Add("gender_ben", typeof(string));
    //    tbl.Columns.Add("address_ben", typeof(string));
    //    tbl.Columns.Add("state_codelgd_ben", typeof(string));
    //    tbl.Columns.Add("district_codelgd_ben", typeof(string));
    //    tbl.Columns.Add("subdistrict_codelgd_ben", typeof(string));
    //    tbl.Columns.Add("villagetown_code_lgd_ben", typeof(string));
    //    tbl.Columns.Add("rural_urban_ben", typeof(string));
    //    tbl.Columns.Add("doc_pic", typeof(string));
    //    tbl.Columns.Add("ahl_hhid", typeof(string));
    //    tbl.Columns.Add("health_id", typeof(string));
    //    tbl.Columns.Add("mobile_member", typeof(string));
    //    tbl.Columns.Add("care_of_dec", typeof(string));
    //    tbl.Columns.Add("userid", typeof(string));
    //    tbl.Columns.Add("user_name", typeof(string));
    //    tbl.Columns.Add("user_districtcode", typeof(string));
    //    tbl.Columns.Add("user_statecode", typeof(string));
    //    tbl.Columns.Add("user_blockcode", typeof(string));
    //    tbl.Columns.Add("user_villagecode", typeof(string));
    //    tbl.Columns.Add("user_towncode", typeof(string));
    //    tbl.Columns.Add("user_wardcode", typeof(string));
    //    tbl.Columns.Add("source_of_data", typeof(string));
    //    tbl.Columns.Add("app_flag", typeof(string));
    //    tbl.Columns.Add("pmrssm_id2", typeof(string));
    //    tbl.Columns.Add("ahl_hhid2", typeof(string));
    //    tbl.Columns.Add("state_codelgd_ben2", typeof(string));
    //    tbl.Columns.Add("district_codelgd_ben2", typeof(string));
    //    tbl.Columns.Add("subdistrict_codelgd_ben2", typeof(string));
    //    tbl.Columns.Add("villagetown_code_lgd_ben2", typeof(string));
    //    tbl.Columns.Add("state_name_english", typeof(string));
    //    tbl.Columns.Add("district_name_english", typeof(string));
    //    tbl.Columns.Add("block_name_english", typeof(string));
    //    tbl.Columns.Add("village_name_english", typeof(string));
    //    tbl.Columns.Add("IBN", typeof(int));
    //    tbl.Columns.Add("OBN", typeof(int));
    //    tbl.Columns.Add("Sheet_no", typeof(int));
    //    tbl.Columns.Add("IBN_max", typeof(int));
    //    tbl.Columns.Add("OBN_max", typeof(int));
    //    tbl.Columns.Add("Sheet_max", typeof(int));
    //    tbl.Columns.Add("Seperator", typeof(string));
    //    tbl.Columns.Add("yob", typeof(string));
    //    tbl.Columns.Add("File_Name", typeof(string));
    //    tbl.Columns.Add("Upload_Date_Time", typeof(string));
    //    tbl.Columns.Add("FLAG", typeof(string));
    //    tbl.Columns.Add("counter", typeof(int));
    //    tbl.Columns.Add("pdf_page_1", typeof(int));
    //    tbl.Columns.Add("pdf_page_2", typeof(int));
    //    tbl.Columns.Add("Lot_ID", typeof(int));
    //    using (var reader = new StreamReader(filePath))
    //    {
    //        reader.ReadLine();
    //        while (!reader.EndOfStream)
    //        {
    //            String newline = reader.ReadLine();
    //            string[] values = newline.Split('|');
    //            flag = "";
    //            try
    //            {

    //                try
    //                {
    //                    gender_val = values[3].ToString();

    //                    if (gender_val == "M")
    //                    {
    //                        gender_val = "Male";
    //                    }
    //                    else if (gender_val == "F")
    //                    {
    //                        gender_val = "Female";
    //                    }
    //                    else if (gender_val == "T")
    //                    {
    //                        gender_val = "Transgender";
    //                    }

    //                    else if (IsNull(gender_val))
    //                    {
    //                        flag += "_R_Wrong_gender";
    //                    }
    //                    else {
    //                        flag += "_R_Wrong_gender";
    //                    }



    //                }
    //                catch (Exception exe)
    //                {
    //                    // yob = row["dob_ben"].ToString();
    //                    flag += "_R_Wrong_gender";
    //                }

    //                try
    //                {
    //                    dob_val = values[2].ToString();

    //                    //if ((dob_val.Substring(0, 4).IndexOf('-') > 0) || (dob_val.Substring(0, 4).IndexOf('/') > 0) || (dob_val.Substring(0, 4).IndexOf('.') > 0))
    //                    dob_val = TryGetYearFromDate(dob_val, out int year);
    //                    if (dob_val == "Wrong_date")
    //                    {
    //                        flag += "_R_Wrong_dob_ben";
    //                        dob_val = values[2].ToString();
    //                    }




    //                }
    //                catch (Exception exe)
    //                {
    //                    // yob = row["dob_ben"].ToString();
    //                    flag += "_R_Wrong_dob_ben";
    //                }

    //                if ((IsNull(values[0].ToString())))
    //                {
    //                    flag += "_R_INVALID_PMJAY_ID";
    //                }
    //                if (IsNull(values[1].ToString()))
    //                {
    //                    flag += "_R_INVALID_NAME";
    //                }

                    

    //                if (IsNull(values[12].ToString()))
    //                {
    //                    flag += "_R_ABHA_ID";
    //                }

    //                if (IsNull(values[8].ToString()))
    //                {
    //                    flag += "_R_INVALID_Village_Name";
    //                }

    //                if (IsNull(values[7].ToString()))
    //                {
    //                    flag += "_R_INVALID_Block_Name";
    //                }

    //                if (IsNull(values[6].ToString()))
    //                {
    //                    flag += "_R_INVALID_District_Name";
    //                }

    //                if (IsNull(values[10].ToString()))
    //                {
    //                    flag += "_R_INVALID_IMAGE";
    //                }

    //                if (IsNull(values[5].ToString()))
    //                {
    //                    flag += "_R_INVALID_State";
    //                }


    //                string pmrssm_id_1 = values[0].ToString();
    //                string name_ben_1 = values[1].ToString();
    //                string dob_ben_1 = dob_val;
    //                string gender_ben_1 = gender_val;
    //                string address_ben_1 = values[4].ToString();
    //                string state_codelgd_ben_1 = "";
    //                string district_codelgd_ben_1 = "";
    //                string subdistrict_codelgd_ben_1 = "";
    //                string villagetown_code_lgd_ben_1 = values[9].ToString();
    //                string rural_urban_ben_1 = values[10].ToString();
    //                string doc_pic_1 = values[11].ToString();
    //                string ahl_hhid_1 = values[12].ToString();
    //                string health_id_1 = values[13].ToString();
    //                string mobile_member_1 = values[14].ToString();
    //                string care_of_dec_1 = values[15].ToString();
    //                string userid_1 = values[16].ToString();
    //                string user_name_1 = values[17].ToString();
    //                string user_districtcode_1 = values[18].ToString();
    //                string user_statecode_1 = values[19].ToString();
    //                string user_blockcode_1 = values[20].ToString();
    //                string user_villagecode_1 = values[21].ToString();
    //                string user_towncode_1 = values[22].ToString();
    //                string user_wardcode_1 = values[23].ToString();
    //                string source_of_data_1 = values[24].ToString();
    //                string app_flag_1 = values[25].ToString();
    //                string pmrssm_id2_1 = "";
    //                string ahl_hhid2_1 = "";
    //                string state_codelgd_ben2_1 = "";
    //                string district_codelgd_ben2_1 = "";
    //                string subdistrict_codelgd_ben2_1 = "";
    //                string villagetown_code_lgd_ben2_1 = "";
    //                string state_name_english_1 = values[5].ToString();
    //                string district_name_english_1 = values[6].ToString();
    //                string block_name_english_1 = values[7].ToString();
    //                string village_name_english_1 = values[8].ToString();
    //                int IBN_1 = 0;
    //                int OBN_1 = 0;
    //                int Sheet_no_1 = 0;
    //                int IBN_max_1 = 0;
    //                int OBN_max_1 = 0;
    //                int Sheet_max_1 = 0;
    //                string Seperator_1 = "";
    //                string yob_1 = "";
    //                string File_Name_1 = maininputfile;
    //                string Upload_Date_Time_1 = DateTime.Now.ToString().ToString();
    //                string FLAG_1 = flag.ToString();
    //                int counter_1 = 0;
    //                int pdf_page_1_1 = 0;
    //                int pdf_page_2_1 = 0;
    //                int Lot_ID_1 = lot_id_current;




    //                try
    //                {
    //                    //tbl.Rows.Add(values[0].ToString(), values[1].ToString(), dob_val, gender_val, values[4].ToString(), values[5].ToString(), values[6].ToString(), values[7].ToString(), values[8].ToString(), values[9].ToString(), values[10].ToString(), values[11].ToString(), values[12].ToString(), values[13].ToString(), values[14].ToString(), values[15].ToString(), values[16].ToString(), values[17].ToString(), values[18].ToString(), values[19].ToString(), values[20].ToString(), values[21].ToString(), values[22].ToString(), values[23].ToString(), values[24].ToString(), values[25].ToString(), values[26].ToString(), values[27].ToString(), values[28].ToString(), values[29].ToString(), values[30].ToString(), values[31].ToString(), values[32].ToString(), values[33].ToString(), values[34].ToString(), 0, 0, 0, 0, 0, 0, "", "", filePath.ToString(), DateTime.Now.ToString().ToString(), flag.ToString(), 0, 0, 0, lot_id_current);
    //                    tbl.Rows.Add(pmrssm_id_1.Trim(), name_ben_1.Trim(), dob_ben_1.Trim(), gender_ben_1.Trim(), address_ben_1.Trim(), state_codelgd_ben_1.Trim(), district_codelgd_ben_1.Trim(), subdistrict_codelgd_ben_1.Trim(), villagetown_code_lgd_ben_1.Trim(), rural_urban_ben_1.Trim(), doc_pic_1.Trim(), ahl_hhid_1.Trim(), health_id_1.Trim(), mobile_member_1.Trim(), care_of_dec_1.Trim(), userid_1.Trim(), user_name_1.Trim().Trim(), user_districtcode_1.Trim(), user_statecode_1.Trim(), user_blockcode_1.Trim(), user_villagecode_1.Trim(), user_towncode_1.Trim(), user_wardcode_1.Trim(), source_of_data_1.Trim(), app_flag_1.Trim(), pmrssm_id2_1.Trim(), ahl_hhid2_1.Trim(), state_codelgd_ben2_1.Trim(), district_codelgd_ben2_1.Trim(), subdistrict_codelgd_ben2_1.Trim(), villagetown_code_lgd_ben2_1.Trim(), state_name_english_1.Trim(), district_name_english_1.Trim(), block_name_english_1.Trim(), village_name_english_1.Trim(), IBN_1, OBN_1, Sheet_no_1, IBN_max_1, OBN_max_1, Sheet_max_1, Seperator_1.Trim(), yob_1.Trim(), File_Name_1.Trim(), Upload_Date_Time_1.Trim(), FLAG_1, counter_1, pdf_page_1_1, pdf_page_2_1, Lot_ID_1);//,lotid);}
    //                }
    //                catch (Exception exe)
    //                {
    //                    Error_log(exe.Message, values[0].ToString(), filePath);
    //                }

    //            }
    //            catch (Exception exe)
    //            {
    //                Console.WriteLine("Error inserting into data table " + values.Length);
    //                Error_log(exe.Message, values[0].ToString(), filePath);

    //            }

    //        }
    //    }

    //    return tbl;
    //}


    public static string TryGetYearFromDate(string dateString, out int year)
    {
        // Define the expected date formats
        string[] formats = {
                "yyyy",
            "yyyy-MM-dd",
            "dd-MM-yyyy",
            "MM/dd/yyyy",
            "dd-MMM-yyyy",
             "yyyy-MMM-dd",
            "MMMM dd, yyyy"
            // Add more formats as needed...
        };

        // Try to parse the date string into a DateTime object using the specified formats
        if (DateTime.TryParseExact(dateString, formats, null, System.Globalization.DateTimeStyles.None, out DateTime date))
        {
            // If parsing is successful, extract the year from the DateTime object
            year = date.Year;
            return year.ToString();
        }

        // If parsing fails, set year to default value (could be anything, based on your requirement)
        year = 0;
        return "Wrong_date";
    }

    public static void messageshow(string message)
    {
        //System.Windows.Forms.RichTextBox richTextBox1 = new System.Windows.Forms.RichTextBox();
        
        //richTextBox1.Text += $"{message}";
        //richTextBox1.Text += "\r\n";
        //richTextBox1.SelectionStart = richTextBox1.TextLength;
        //richTextBox1.ScrollToCaret();
        //richTextBox1.Refresh();
    }

    public static DataTable read_dynamic(string filePath, int lot_id_current)
    {



        string maininputfile = System.IO.Path.GetFileNameWithoutExtension(filePath);
        string[] values_maininputfile = maininputfile.Split('_');
        maininputfile = values_maininputfile[0];
        string gender_val = "", dob_val = "";
        string flag;

        System.Data.DataTable tbl = new System.Data.DataTable();
        //System.Data.DataTable tbl = new System.Data.DataTable();
        tbl.Columns.Add("pmrssm_id", typeof(string));
        tbl.Columns.Add("name_ben", typeof(string));
        tbl.Columns.Add("dob_ben", typeof(string));
        tbl.Columns.Add("gender_ben", typeof(string));
        tbl.Columns.Add("address_ben", typeof(string));
        tbl.Columns.Add("state_codelgd_ben", typeof(string));
        tbl.Columns.Add("district_codelgd_ben", typeof(string));
        tbl.Columns.Add("subdistrict_codelgd_ben", typeof(string));
        tbl.Columns.Add("villagetown_code_lgd_ben", typeof(string));
        tbl.Columns.Add("rural_urban_ben", typeof(string));
        tbl.Columns.Add("doc_pic", typeof(string));
        tbl.Columns.Add("ahl_hhid", typeof(string));
        tbl.Columns.Add("health_id", typeof(string));
        tbl.Columns.Add("mobile_member", typeof(string));
        tbl.Columns.Add("care_of_dec", typeof(string));
        tbl.Columns.Add("userid", typeof(string));
        tbl.Columns.Add("user_name", typeof(string));
        tbl.Columns.Add("user_districtcode", typeof(string));
        tbl.Columns.Add("user_statecode", typeof(string));
        tbl.Columns.Add("user_blockcode", typeof(string));
        tbl.Columns.Add("user_villagecode", typeof(string));
        tbl.Columns.Add("user_towncode", typeof(string));
        tbl.Columns.Add("user_wardcode", typeof(string));
        tbl.Columns.Add("source_of_data", typeof(string));
        tbl.Columns.Add("app_flag", typeof(string));
        tbl.Columns.Add("pmrssm_id2", typeof(string));
        tbl.Columns.Add("ahl_hhid2", typeof(string));
        tbl.Columns.Add("state_codelgd_ben2", typeof(string));
        tbl.Columns.Add("district_codelgd_ben2", typeof(string));
        tbl.Columns.Add("subdistrict_codelgd_ben2", typeof(string));
        tbl.Columns.Add("villagetown_code_lgd_ben2", typeof(string));
        tbl.Columns.Add("state_name_english", typeof(string));
        tbl.Columns.Add("district_name_english", typeof(string));
        tbl.Columns.Add("block_name_english", typeof(string));
        tbl.Columns.Add("village_name_english", typeof(string));
        tbl.Columns.Add("IBN", typeof(int));
        tbl.Columns.Add("OBN", typeof(int));
        tbl.Columns.Add("Sheet_no", typeof(int));
        tbl.Columns.Add("IBN_max", typeof(int));
        tbl.Columns.Add("OBN_max", typeof(int));
        tbl.Columns.Add("Sheet_max", typeof(int));
        tbl.Columns.Add("Seperator", typeof(string));
        tbl.Columns.Add("yob", typeof(string));
        tbl.Columns.Add("File_Name", typeof(string));
        tbl.Columns.Add("Upload_Date_Time", typeof(string));
        tbl.Columns.Add("FLAG", typeof(string));
        tbl.Columns.Add("counter", typeof(int));
        tbl.Columns.Add("pdf_page_1", typeof(int));
        tbl.Columns.Add("pdf_page_2", typeof(int));
        tbl.Columns.Add("Lot_ID", typeof(int));
        using (var reader = new StreamReader(filePath))
        {
            reader.ReadLine();
            while (!reader.EndOfStream)
            {
                String newline = reader.ReadLine();
                //string[] values = SplitCSVLine(newline);


                string[] values = newline.Split('|');
                flag = "";
                try
                {

                    string pmrssm_id_1 = values[0].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string name_ben_1 = values[1].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string dob_ben_1 = values[2].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string gender_ben_1 = values[3].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string address_ben_1 = values[4].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    //try
                    //{string address_ben_1 = values[4].ToString().Replace("\"", "").Replace(",", "-").Trim();}
                    //catch
                    //{ string address_ben_1 = values[4].ToString().Replace(",", "-").Trim(); }
                    string state_codelgd_ben_1 = values[5].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string district_codelgd_ben_1 = values[6].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string subdistrict_codelgd_ben_1 = values[7].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string villagetown_code_lgd_ben_1 = values[8].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string rural_urban_ben_1 = values[9].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string doc_pic_1 = values[10].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string ahl_hhid_1 = values[11].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string health_id_1 = values[12].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string mobile_member_1 = values[13].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string care_of_dec_1 = values[14].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string userid_1 = values[15].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string user_name_1 = values[16].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string user_districtcode_1 = values[17].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string user_statecode_1 = values[18].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string user_blockcode_1 = values[19].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string user_villagecode_1 = values[20].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string user_towncode_1 = values[21].ToString().Replace("\"", "").Replace(",", "-").Trim(); 
                    string user_wardcode_1 = values[22].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string source_of_data_1 = values[23].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string app_flag_1 = values[24].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string pmrssm_id2_1 = "";
                    string ahl_hhid2_1 = "";
                    string state_codelgd_ben2_1 = "";
                    string district_codelgd_ben2_1 = "";
                    string subdistrict_codelgd_ben2_1 = "";
                    string villagetown_code_lgd_ben2_1 = "";
                    //string state_name_english_1 = values[25].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string state_name_english_1 = Global_Data.state_name;// making it entered by user only not from file
                    string district_name_english_1 = values[26].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string block_name_english_1 = values[27].ToString().Replace("\"", "").Replace(",", "-").Trim();
                    string village_name_english_1 = values[28].ToString().Replace("\"", "").Replace(",", "-").Trim();

                    int IBN_1 = 0;
                    int OBN_1 = 0;
                    int Sheet_no_1 = 0;
                    int IBN_max_1 = 0;
                    int OBN_max_1 = 0;
                    int Sheet_max_1 = 0;
                    string Seperator_1 = "";
                    string yob_1 = "";
                    string File_Name_1 = maininputfile;
                    string Upload_Date_Time_1 = DateTime.Now.ToString().ToString();
                    string FLAG_1 = flag.ToString();
                    int counter_1 = 0;
                    int pdf_page_1_1 = 0;
                    int pdf_page_2_1 = 0;
                    int Lot_ID_1 = lot_id_current;

                    try
                    {
                        //gender_val = gender_ben_1;

                        if (gender_ben_1 == "M")
                        {
                            gender_ben_1 = "Male";
                        }
                        else if (gender_ben_1 == "F")
                        {
                            gender_ben_1 = "Female";
                        }
                        else if (gender_ben_1 == "T")
                        {
                            gender_ben_1 = "Transgender";
                        }
                        else if (gender_ben_1 == "O")
                        {
                            gender_ben_1 = "Other";
                        }

                        else if (IsNull(gender_ben_1) && val_check.Contains("gender"))
                        {
                            flag += "_R_Blank_gender";
                        }
                        else
                        {
                            flag += "_R_Wrong_gender";
                        }



                    }
                    catch (Exception exe)
                    {
                        // yob = row["dob_ben"].ToString();
                        flag += "_R_Wrong_gender";
                    }

                    try
                    {
                        //dob_ben_1 = values[2].ToString();

                        //if ((dob_val.Substring(0, 4).IndexOf('-') > 0) || (dob_val.Substring(0, 4).IndexOf('/') > 0) || (dob_val.Substring(0, 4).IndexOf('.') > 0))
                        dob_val = TryGetYearFromDate(dob_ben_1, out int year);
                        if ((dob_val == "Wrong_date") && val_check.Contains("dob"))
                        {
                            flag += "_R_Invalid_Format_Dob_ben";
                            dob_ben_1 = dob_ben_1;
                        }
                        else 
                        {
                            dob_ben_1 = dob_val;


                        }




                    }
                    catch (Exception exe)
                    {
                        // yob = row["dob_ben"].ToString();
                        flag += "_R_INVALID_dob_ben";
                    }

                    if (IsNull(pmrssm_id_1) && val_check.Contains("pmjayid"))
                    {
                        flag += "_R_INVALID_PMJAY_ID";
                    }
                    if (IsNull(name_ben_1) && val_check.Contains("name_ben"))
                    {
                        flag += "_R_INVALID_NAME";
                    }



                    if (IsNull(health_id_1) && val_check.Contains("abhaid"))
                    {
                        flag += "_R_INVALID_ABHA_ID_";
                    }

                    if (IsNull(village_name_english_1) && val_check.Contains("village"))
                    {
                        flag += "_R_INVALID_Village_Name";
                    }

                    if (IsNull(block_name_english_1) && val_check.Contains("block"))
                    {
                        flag += "_R_INVALID_Block_Name";
                    }

                    if (IsNull(district_name_english_1) && val_check.Contains("district"))
                    {
                        flag += "_R_INVALID_District_Name";
                    }

                    if (IsNull(doc_pic_1) && val_check.Contains("dobpic"))
                    {
                        flag += "_R_INVALID_IMAGE";
                    }

                    if (IsNull(state_name_english_1) && val_check.Contains("state"))
                    {
                        flag += "_R_INVALID_State";
                    }


                    




                    try
                    {
                        //tbl.Rows.Add(values[0].ToString(), values[1].ToString(), dob_val, gender_val, values[4].ToString(), values[5].ToString(), values[6].ToString(), values[7].ToString(), values[8].ToString(), values[9].ToString(), values[10].ToString(), values[11].ToString(), values[12].ToString(), values[13].ToString(), values[14].ToString(), values[15].ToString(), values[16].ToString(), values[17].ToString(), values[18].ToString(), values[19].ToString(), values[20].ToString(), values[21].ToString(), values[22].ToString(), values[23].ToString(), values[24].ToString(), values[25].ToString(), values[26].ToString(), values[27].ToString(), values[28].ToString(), values[29].ToString(), values[30].ToString(), values[31].ToString(), values[32].ToString(), values[33].ToString(), values[34].ToString(), 0, 0, 0, 0, 0, 0, "", "", filePath.ToString(), DateTime.Now.ToString().ToString(), flag.ToString(), 0, 0, 0, lot_id_current);
                        tbl.Rows.Add(pmrssm_id_1.Trim(), name_ben_1.Trim(), dob_ben_1.Trim(), gender_ben_1.Trim(), address_ben_1.Trim(), state_codelgd_ben_1.Trim(), district_codelgd_ben_1.Trim(), subdistrict_codelgd_ben_1.Trim(), villagetown_code_lgd_ben_1.Trim(), rural_urban_ben_1.Trim(), doc_pic_1.Trim(), ahl_hhid_1.Trim(), health_id_1.Trim(), mobile_member_1.Trim(), care_of_dec_1.Trim(), userid_1.Trim(), user_name_1.Trim(), user_districtcode_1.Trim(), user_statecode_1.Trim(), user_blockcode_1.Trim(), user_villagecode_1.Trim(), user_towncode_1.Trim(), user_wardcode_1.Trim(), source_of_data_1.Trim(), app_flag_1.Trim(), pmrssm_id2_1.Trim(), ahl_hhid2_1.Trim(), state_codelgd_ben2_1.Trim(), district_codelgd_ben2_1.Trim(), subdistrict_codelgd_ben2_1.Trim(), villagetown_code_lgd_ben2_1.Trim(), state_name_english_1.Trim(), district_name_english_1.Trim(), block_name_english_1.Trim(), village_name_english_1.Trim(), IBN_1, OBN_1, Sheet_no_1, IBN_max_1, OBN_max_1, Sheet_max_1, Seperator_1.Trim(), yob_1.Trim(), File_Name_1.Trim(), Upload_Date_Time_1.Trim(), flag, counter_1, pdf_page_1_1, pdf_page_2_1, Lot_ID_1);//,lotid);}
                    }
                    catch (Exception exe)
                    {
                        Error_log(exe.Message, pmrssm_id_1.Trim(), filePath);
                    }

                }
                catch (Exception exe)
                {
                    Console.WriteLine("Error inserting into data table " + values.Length);
                    Error_log(exe.Message, values[0].ToString(), filePath);

                }

            }
        }

        return tbl;


    }


    //public static string[] SplitCSVLine(string line)
    //{
    //    var values = new System.Collections.Generic.List<string>();

    //    bool inQuotes = false;
    //    int startIndex = 0;

    //    for (int i = 0; i < line.Length; i++)
    //    {
    //        char c = line[i];
    //        if (c == '\"')
    //        {
    //            inQuotes = !inQuotes;
    //        }
    //        else if (c == ',' && !inQuotes)
    //        {
    //            values.Add(line.Substring(startIndex, i - startIndex));
    //            startIndex = i + 1;
    //        }
    //    }

    //    values.Add(line.Substring(startIndex)); // Add the last value after the last comma

    //    return values.ToArray();
    //}


    private static string[] GetColumnHeaders(string filePath)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "|"
        };



        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {
            csv.Read();
            csv.ReadHeader();
            return csv.HeaderRecord;
        }
    }




    private static List<string> GetIncorrectHeaders(string[] baseHeaders, string[] sourceHeaders)
    {
        return sourceHeaders.Except(baseHeaders, StringComparer.OrdinalIgnoreCase).ToList();
    }



    private static void UpdateSourceFileHeaders(string sourceFilePath)
    {
        try
        {


            var lines = File.ReadAllLines(sourceFilePath); // Read all lines from the source file
            var headerRow = lines.FirstOrDefault(); // Get the header row

            if (headerRow != null)
            {
                var updatedHeaderRow = string.Join("|", headerRow.Split('|').Select(header => header.ToUpperInvariant().Replace("_", "")));





                // Replace the header row with the updated one
                lines[0] = updatedHeaderRow;





                File.WriteAllLines(sourceFilePath, lines); // Write the updated lines back to the file
            }


            //for (int i = 1; i <= lines.Length; i++)
            //{
            //    var updatedline = lines[i].replace("\"", "");
            //    lines[i] = updatedline;



            //}
            //File.WriteAllLines(sourceFilePath, lines);
            //string tempFilePath = Path.GetTempFileName();
            //using (StreamReader reader = new StreamReader(sourceFilePath))
            //using (StreamWriter writer = new StreamWriter(tempFilePath))
            //{
            //    while (!reader.EndOfStream)
            //    {
            //        string line = reader.ReadLine().Replace("\"", "");

            //        writer.WriteLine(line);
            //    }
            //}
            //// Replace the original file with the cleaned temporary file
            //File.Delete(sourceFilePath);
            //File.Move(tempFilePath, sourceFilePath);


        }
        catch (Exception ex)
        {
            // Handle any exceptions that might occur during the update
            // For example, log the error or throw a custom exception
            Console.WriteLine("An error occurred while updating the source file headers: " + ex.Message);
        }
    }

    private static char DetectQuotingCharacter(string csvFilePath)
    {

        
        // Read a portion of the file and analyze the content
        using (var reader = new StreamReader(csvFilePath))
        {
            string line = reader.ReadLine();
            if (line != null)
            {
                if (line.Contains("\""))
                {
                    return '"';
                }
                else if (line.Contains("'"))
                {
                    return '\'';
                }
                // You might add additional checks for other quoting characters
               
            }
            return '\0';
        }
    }

    //private static (string[] Headers, List<dynamic> CleanedData) GetCleanedData(string filePath)
    //{

    //    char c = DetectQuotingCharacter(filePath);

    //    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
    //    {

    //        Delimiter = "|",
    //        Quote = '"'
    //    };



    //    using (var reader = new StreamReader(filePath))
    //    using (var csv = new CsvReader(reader, config))
    //    {



    //        csv.Read();
    //        csv.ReadHeader();



    //        var headerDict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
    //        var headerRecord = new List<string>();
    //        var cleanedData = new List<dynamic>();





    //        foreach (var header in csv.HeaderRecord)
    //        {
    //            var cleanedHeader = header;



    //            if (!headerDict.ContainsKey(cleanedHeader))
    //            {
    //                headerDict[cleanedHeader] = 1;
    //                headerRecord.Add(cleanedHeader);
    //            }

    //        }





    //            while (csv.Read())
    //        {
    //            var cleanedRecord = new ExpandoObject();
    //            var expandoDict = (IDictionary<string, object>)cleanedRecord;





    //            foreach (var header in headerRecord)
    //            {

    //                if (!csv.HeaderRecord.Contains(header))
    //                {
    //                    expandoDict[header] = null;
    //                    continue; // Skip duplicate headers and their corresponding values
    //                }



    //                string data_value = csv.GetField(header);

    //                expandoDict[header] = data_value.replace("\"","");
    //                string test = expandoDict[header].ToString();


    //            }





    //            cleanedData.Add(cleanedRecord);
    //        }





    //        return (headerRecord.ToArray(), cleanedData);
    //    }
    //}


    private static (string[] Headers, List<dynamic> CleanedData) GetCleanedData(string filePath)
    {

        char c = DetectQuotingCharacter(filePath);

        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {

            Delimiter = "|",
            Quote = '"'
        };



        using (var reader = new StreamReader(filePath))
        using (var csv = new CsvReader(reader, config))
        {



            csv.Read();
            csv.ReadHeader();


            string line_1 = csv.Context.Parser.RawRecord;
            int separatorCount_header = CountSeparators(line_1, '|');


            var headerDict = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);
            var headerRecord = new List<string>();
            var cleanedData = new List<dynamic>();





            foreach (var header in csv.HeaderRecord)
            {
                var cleanedHeader = header;



                if (!headerDict.ContainsKey(cleanedHeader))
                {
                    headerDict[cleanedHeader] = 1;
                    headerRecord.Add(cleanedHeader);
                }

            }




            while (csv.Read())
            {
                var cleanedRecord = new ExpandoObject();
                var expandoDict = (IDictionary<string, object>)cleanedRecord;


                string line = csv.Context.Parser.RawRecord;
                int separatorCount = CountSeparators(line, '|');

                if (separatorCount_header == separatorCount)
                {
                    foreach(var header in headerRecord)
                    {

                        if (!csv.HeaderRecord.Contains(header))
                        {
                            expandoDict[header] = null;
                            continue; // Skip duplicate headers and their corresponding values
                        }



                        string data_value = csv.GetField(header);

                        expandoDict[header] = data_value.Replace("\"", "");
                        string test = expandoDict[header].ToString();


                    }
                    cleanedData.Add(cleanedRecord);
                }
                else
                {
                    string filePath_error = System.IO.Path.GetDirectoryName(filePath) + "//" +"Error_" +System.IO.Path.GetFileNameWithoutExtension(filePath) + "Error.txt"; // Change this to your desired output file path


                    StreamWriter log;
                    if (!File.Exists(filePath_error))
                    {
                        log = new StreamWriter(filePath_error);
                    }
                    else
                    {
                        log = File.AppendText(filePath_error);
                    }
                    log.WriteLine(line.Trim());

                    //MessageBox.Show("Data Time:" + DateTime.Now + "  " + id + " : " + sExceptionName + "   File_Name : " + filename);

                    log.Close();


                    
                }






                
            }





            return (headerRecord.ToArray(), cleanedData);
        }
    }
    //private static string ReplaceCharacters(string input)
    //{
    //    // Replace specific characters here, for example:
    //    string modified = input.Replace("\"", "");

    //    return modified;
    //}

    private static bool headersMatch(string[] sourceHeaders, string[] baseHeaders)
    {
        return sourceHeaders.All(header => baseHeaders.Contains(header));
    }



    private static List<ExpandoObject> MapData(List<dynamic> records, string[] baseHeaders)
    {
        var mappedData = new List<ExpandoObject>();





        foreach (var record in records)
        {
            dynamic mappedRecord = new ExpandoObject();
            var expandoDict = (IDictionary<string, object>)mappedRecord;





            foreach (var header in baseHeaders)
            {
                expandoDict[header] = ((IDictionary<string, object>)record).ContainsKey(header)
                    ? ((IDictionary<string, object>)record)[header]
                    : null;
            }





            mappedData.Add(mappedRecord);
        }





        return mappedData;
    }



    private static void WriteToBaseCsv(string filePath, string[] baseHeaders, IEnumerable<dynamic> data)
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            Delimiter = "|"
        };



        using (var writer = new StreamWriter(filePath))
        using (var csv = new CsvWriter(writer, config))
        {
            // Write the header row
            foreach (var header in baseHeaders)
            {
                csv.WriteField(header);
            }
            csv.NextRecord();





            // Write the data
            foreach (var record in data)
            {
                var expandoDict = (IDictionary<string, object>)record;
                foreach (var header in baseHeaders)
                {
                    csv.WriteField(expandoDict.ContainsKey(header) ? expandoDict[header]?.ToString() : null);
                }
                csv.NextRecord();
            }
        }
    }


    static void CleanAndWriteCSV(string filePath)
    {
        string tempFilePath = Path.GetTempFileName();



        using (StreamReader reader = new StreamReader(filePath))
        using (StreamWriter writer = new StreamWriter(tempFilePath))
        {
            while (!reader.EndOfStream)
            {
                string line = reader.ReadLine().Replace("\"", "").Replace("'","");
                
                writer.WriteLine(line);
            }
        }



        // Replace the original file with the cleaned temporary file
        File.Delete(filePath);
        File.Move(tempFilePath, filePath);
    }


    private static int CountSeparators(string line, char separator)
    {
        int count = 0;
        foreach (char c in line)
        {
            if (c == separator)
            {
                count++;
            }
        }
        return count;
    }




}




