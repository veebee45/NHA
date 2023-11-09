

using CsvHelper;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


internal class Sql_Data_insertion
{
    private System.Windows.Forms.RichTextBox richTextBox1;
    public static bool IsNull(string input)
    {
        return Regex.IsMatch(input, @"^\s*$");
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

   

   



    public static void ImportFilesToDatabase_new(string outputDirectory, string data_table_name, int lotid)

    {
        System.Windows.Forms.RichTextBox richTextBox1 = new System.Windows.Forms.RichTextBox();
        string connectionString = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;

        //string outputDirectory_image = outputDirectory + @"\IMAGES_1\";
        //Console.WriteLine(outputDirectory_image);

        string[] csvFiles = Directory.GetFiles(outputDirectory, "output_*.csv");
        string gender_val = "", dob_val = "";
        string flag;

        //int batchSize = 10000, i = 0;

        //int table_check = nha_create_table(data_table_name);

        foreach (string file in csvFiles)

        {
            //string connectionString = "Data Source=your_server;Initial Catalog=your_database;User ID=your_username;Password=your_password;";
            string filePath = file;

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
                    bulkCopy.BulkCopyTimeout = 600; // Timeout in seconds

                   

                    try
                    {
                        // Read the data from the file into a DataTable
                        DataTable data = ReadDataFromFile(filePath, lotid);

                        // Perform the bulk copy
                        bulkCopy.WriteToServer(data);

                        Console.WriteLine("Bulk import completed successfully.");

                        richTextBox1.Text += $"{filePath} data inserted into database table {data_table_name}";
                        richTextBox1.Text += "\r\n";
                        richTextBox1.SelectionStart = richTextBox1.TextLength;
                        richTextBox1.ScrollToCaret();
                        richTextBox1.Refresh();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred: " + ex.Message);
                        connection.Close();
                    }
                }
                connection.Close();
            }
        }



        
                            
    }


    public static DataTable ReadDataFromFile(string filePath, int lot_id_current)
    {
        // Implement the logic to read data from the file and return it as a DataTable
        // You can use libraries like CsvHelper or read data manually based on your file format

        // Example code to create a DataTable and populate data from a CSV file using CsvHelper
        //DataTable data = new DataTable();
        //using (var reader = new StreamReader(filePath))
        //using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
        //{
        //    csv.Read();
        //    csv.ReadHeader();

        //    foreach (string header in csv.HeaderRecord)
        //    {
        //        data.Columns.Add(header);
        //    }

        //    while (csv.Read())
        //    {
        //        DataRow row = data.NewRow();
        //        foreach (DataColumn column in data.Columns)
        //        {
        //            row[column.ColumnName] = csv.GetField(column.DataType, column.ColumnName);
        //        }
        //        data.Rows.Add(row);
        //    }
        //}

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
                string[] values = newline.Split('|');
                flag = "";
                if (values.Length == 35)
                {

                    try
                    {
                        gender_val = values[3].ToString();

                        if (gender_val == "M")
                        {
                            gender_val = "Male";
                        }
                        else if (gender_val == "F")
                        {
                            gender_val = "Female";
                        }
                        else if (gender_val == "T")
                        {
                            gender_val = "Transgender";
                        }



                    }
                    catch (Exception exe)
                    {
                        // yob = row["dob_ben"].ToString();
                        flag += "R_Wrong_gender";
                    }

                    try
                    {
                        dob_val = values[2].ToString();

                        if (dob_val.Substring(0, 4).IndexOf('_') >= 0)
                        {
                            flag += "R_Wrong_dob_ben";
                        }
                        else
                        {
                            dob_val = dob_val.Substring(0, 4);
                        }



                    }
                    catch (Exception exe)
                    {
                        // yob = row["dob_ben"].ToString();
                        flag += "_R_Wrong_dob_ben";
                    }

                    if (IsNull(values[0].ToString()))
                    {
                        flag += "_R_INVALID_PMJAY_ID";
                    }
                    if (IsNull(values[1].ToString()))
                    {
                        flag += "_R_INVALID_NAME";
                    }

                    if (IsNull(values[12].ToString()))
                    {
                        flag += "_R_ABHA_ID";
                    }

                    if (IsNull(values[34].ToString()))
                    {
                        flag += "_R_INVALID_Village_Name";
                    }

                    if (IsNull(values[33].ToString()))
                    {
                        flag += "_R_INVALID_Block_Name";
                    }

                    if (IsNull(values[32].ToString()))
                    {
                        flag += "_R_INVALID_District_Name";
                    }

                    if (IsNull(values[10].ToString()))
                    {
                        flag += "_R_INVALID_IMAGE";
                    }


                    

                    //tbl.Rows.Add(values[0].ToString(), values[1].ToString(), dob_val, gender_val, values[4].ToString(), values[5].ToString(), values[6].ToString(), values[7].ToString(), values[8].ToString(), values[9].ToString(), values[10].ToString(), values[11].ToString(), values[12].ToString(), values[13].ToString(), values[14].ToString(), values[15].ToString(), values[16].ToString(), values[17].ToString(), values[18].ToString(), values[19].ToString(), values[20].ToString(), values[21].ToString(), values[22].ToString(), values[23].ToString(), values[24].ToString(), values[25].ToString(), values[26].ToString(), values[27].ToString(), values[28].ToString(), values[29].ToString(), values[30].ToString(), values[31].ToString(), values[32].ToString(), values[33].ToString(), values[34].ToString(), 0, 0, 0, 0, 0, 0, "", "", filePath.ToString(), DateTime.Now.ToString().ToString(), flag.ToString(), 0, 0, 0, lot_id_current);
                    tbl.Rows.Add(values[0].ToString(), values[1].ToString(), dob_val, gender_val, values[4].ToString(), "", "", "", values[9].ToString(), values[10].ToString(), values[11].ToString(), values[12].ToString(), values[13].ToString(), values[14].ToString(), values[15].ToString(), values[16].ToString(), values[17].ToString(), values[18].ToString(), values[19].ToString(), values[20].ToString(), values[21].ToString(), values[22].ToString(), values[23].ToString(), values[24].ToString(), values[25].ToString(), "", "", "", "", "", values[5].ToString(), values[6].ToString(), values[7].ToString(), values[8].ToString(), 0, 0, 0, 0, 0, 0, "", "", filePath.ToString(), DateTime.Now.ToString().ToString(), flag.ToString(), 0, 0, 0, lot_id_current);//,lotid);
                }
                else
                {
                    Console.WriteLine("Error inserting into data table " + values.Length);

                }

            }
        }

        return tbl;
    }


    public static void importfilestodatabase_1(string outputdirectory, string data_table_name)

    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;

        string outputdirectory_image = outputdirectory + @"\images_1\";
        //Console.WriteLine(outputdirectory_image);

        string[] csvfiles = Directory.GetFiles(outputdirectory, "output_*.csv");
        string gender_val = "", dob_val = "";
        string flag = "";

        int batchsize = 10000, i = 0;

        //int table_check = nha_create_table(data_table_name);

        using (var connection = new SqlConnection(connectionString))

        {

            connection.Open();




            foreach (string file in csvfiles)

            {

                using (var transaction = connection.BeginTransaction())

                {

                    using (var reader = new StreamReader(file))

                    {

                        using (var bulkCopy = new SqlBulkCopy(connection,SqlBulkCopyOptions.Default,transaction))
                        //using (var bulkCopy = new sqlbulkCopy(connectionstring))
                        //using (var bulkCopy = new sqlbulkCopy(connection, sqlbulkCopyoptions.default))
                        //, transaction))


                        {

                            bulkCopy.DestinationTableName = data_table_name;

                            //bulkCopy.batchsize = 1000; // set an appropriate batch size for efficient imports




                            // map the columns in the csv file to the corresponding database columns
                            // map the columns from your datatable to the destination table columns
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
                            bulkCopy.ColumnMappings.Add("ibn", "ibn");
                            bulkCopy.ColumnMappings.Add("obn", "obn");
                            bulkCopy.ColumnMappings.Add("sheet_no", "sheet_no");
                            bulkCopy.ColumnMappings.Add("ibn_max", "ibn_max");
                            bulkCopy.ColumnMappings.Add("obn_max", "obn_max");
                            bulkCopy.ColumnMappings.Add("sheet_max", "sheet_max");
                            bulkCopy.ColumnMappings.Add("seperator", "seperator");
                            bulkCopy.ColumnMappings.Add("yob", "yob");
                            bulkCopy.ColumnMappings.Add("file_name", "file_name");
                            bulkCopy.ColumnMappings.Add("upload_date_time", "upload_date_time");
                            bulkCopy.ColumnMappings.Add("flag", "flag");
                            bulkCopy.ColumnMappings.Add("counter", "counter");
                            bulkCopy.ColumnMappings.Add("pdf_page_1", "pdf_page_1");
                            bulkCopy.ColumnMappings.Add("pdf_page_2", "pdf_page_2");

                            // using the ColumnMappings property of the sqlbulkCopy instance




                            System.Data.DataTable tbl = new System.Data.DataTable();
                            //system.data.datatable tbl = new system.data.datatable();
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
                            tbl.Columns.Add("ibn", typeof(int));
                            tbl.Columns.Add("obn", typeof(int));
                            tbl.Columns.Add("sheet_no", typeof(int));
                            tbl.Columns.Add("ibn_max", typeof(int));
                            tbl.Columns.Add("obn_max", typeof(int));
                            tbl.Columns.Add("sheet_max", typeof(int));
                            tbl.Columns.Add("seperator", typeof(string));
                            tbl.Columns.Add("yob", typeof(string));
                            tbl.Columns.Add("file_name", typeof(string));
                            tbl.Columns.Add("upload_date_time", typeof(string));
                            tbl.Columns.Add("flag", typeof(string));
                            tbl.Columns.Add("counter", typeof(int));
                            tbl.Columns.Add("pdf_page_1", typeof(int));
                            tbl.Columns.Add("pdf_page_2", typeof(int));

                            reader.ReadLine();
                            while (!reader.EndOfStream)
                            {
                                string newline = reader.ReadLine();
                                string[] values = newline.Split('|');
                                flag = "";
                                if (values.Length == 35)
                                {

                                    try
                                    {
                                        gender_val = values[3].ToString();

                                        if (gender_val == "m")
                                        {
                                            gender_val = "male";
                                        }
                                        else if (gender_val == "f")
                                        {
                                            gender_val = "female";
                                        }
                                        else if (gender_val == "t")
                                        {
                                            gender_val = "transgender";
                                        }



                                    }
                                    catch (Exception exe)
                                    {
                                        // yob = row["dob_ben"].ToString();
                                        flag += "r_wrong_gender";
                                    }

                                    try
                                    {
                                        dob_val = values[2].ToString();

                                        if (dob_val.Substring(0, 4).IndexOf('_') >= 0)
                                        {
                                            flag += "r_wrong_dob_ben";
                                        }
                                        else
                                        {
                                            dob_val = dob_val.Substring(0, 4);
                                        }



                                    }
                                    catch (Exception exe)
                                    {
                                        // yob = row["dob_ben"].ToString();
                                        flag += "_r_wrong_dob_ben";
                                    }

                                    if (IsNull(values[0].ToString()))
                                    {
                                        flag += "_r_invalid_pmjay_id";
                                    }
                                    if (IsNull(values[1].ToString()))
                                    {
                                        flag += "_r_invalid_name";
                                    }

                                    if (IsNull(values[12].ToString()))
                                    {
                                        flag += "_r_abha_id";
                                    }

                                    if (IsNull(values[34].ToString()))
                                    {
                                        flag += "_r_invalid_village_name";
                                    }

                                    if (IsNull(values[33].ToString()))
                                    {
                                        flag += "_r_invalid_block_name";
                                    }

                                    if (IsNull(values[32].ToString()))
                                    {
                                        flag += "_r_invalid_district_name";
                                    }

                                    if (IsNull(values[10].ToString()))
                                    {
                                        flag += "_r_invalid_image";
                                    }


                                    //if (image_convertor(values[10].ToString(), outputdirectory_image, values[0].ToString()) != 1)
                                    //{
                                    //    flag += "_r_invalid_image";
                                    //}


                                    tbl.Rows.Add(values[0].ToString(), values[1].ToString(), dob_val, gender_val, values[4].ToString(), values[5].ToString(), values[6].ToString(), values[7].ToString(), values[8].ToString(), values[9].ToString(), values[10].ToString(), values[11].ToString(), values[12].ToString(), values[13].ToString(), values[14].ToString(), values[15].ToString(), values[16].ToString(), values[17].ToString(), values[18].ToString(), values[19].ToString(), values[20].ToString(), values[21].ToString(), values[22].ToString(), values[23].ToString(), values[24].ToString(), values[25].ToString(), values[26].ToString(), values[27].ToString(), values[28].ToString(), values[29].ToString(), values[30].ToString(), values[31].ToString(), values[32].ToString(), values[33].ToString(), values[34].ToString(), 0, 0, 0, 0, 0, 0, "", "", file.ToString(), DateTime.Now.ToString().ToString(), flag.ToString(), 0, 0, 0);

                                }
                                else
                                {
                                    Console.WriteLine("error inserting into data table " + values.Length);

                                }


                            }
                            //datagridview1.datasource = tbl;
                            if ((i == batchsize && !reader.EndOfStream) || (reader.EndOfStream))
                            {

                                bulkCopy.WriteToServer(tbl);

                               
                                Console.WriteLine($"{i} data inserted into database table {data_table_name}");


                                i = 0;

                            }
                            tbl.Clear();
                            i = i + 1;

                        }

                    }




                    transaction.Commit();
                }

            }
            connection.Close();

        }

    }

    public static void messageshow(string message)
    {
        System.Windows.Forms.RichTextBox richTextBox1 = new System.Windows.Forms.RichTextBox();
        richTextBox1.Text += $"{message}";
        richTextBox1.Text += "\r\n";
        richTextBox1.SelectionStart = richTextBox1.TextLength;
        richTextBox1.ScrollToCaret();
        richTextBox1.Refresh();
    }

}

