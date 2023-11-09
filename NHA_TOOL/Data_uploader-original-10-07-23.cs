using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Forms;



class Data_uploader
{
    private System.Windows.Forms.RichTextBox richTextBox1;
    public static bool IsNull(string input)
    {
        return Regex.IsMatch(input, @"^\s*$");
    }
    public static void WriteLog(string sExceptionName, string sEventName, string sControlName, string sFormName)
    {
        StreamWriter log;
        if (!File.Exists("Duplicatelogfile.txt"))
        {
            log = new StreamWriter("Duplicatelogfile.txt");
        }
        else
        {
            log = File.AppendText("Duplicatelogfile.txt");
        }
        log.WriteLine("Data Time:" + DateTime.Now);
        log.WriteLine("Exception Name:" + sExceptionName);
        log.WriteLine("Event Name:" + sEventName);
        log.WriteLine("Control Name:" + sControlName);
        log.WriteLine("Form Name:" + sFormName);
        log.Close();
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



    public static int nha_create_table(string table_name)
    {
        string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
        int pass = 0;
        using (SqlConnection con = new SqlConnection(connectionString))
        {
            con.Open();
            using (SqlCommand cmd = new SqlCommand("usp_nha_table_create", con))
            {
                try
                {
                    cmd.Parameters.AddWithValue("@tablename", table_name);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    pass = 1;
                }
                catch (Exception exe)
                {
                    Error_log(exe.Message, table_name, "");
                }

            }
            con.Close();
        }
        return pass;
    }


    public static void SplitCSVFile(string sourceFilePath, string outputDirectory, int batchSize)



    {

        using (var reader = new StreamReader(sourceFilePath))

        {

            int fileCount = 1;

            int rowCount = 0;

            string header = reader.ReadLine();


            //if (System.IO.File.ReadAllLines((sourceFilePath)).Length > batchSize)
            //{
            while (!reader.EndOfStream)

            {

                string outputFile = System.IO.Path.Combine(outputDirectory, $"output_{fileCount}.csv");

                using (var writer = new StreamWriter(outputFile))

                {

                    writer.WriteLine(header);




                    for (int i = 0; i < batchSize && !reader.EndOfStream; i++)

                    {

                        writer.WriteLine(reader.ReadLine());

                        rowCount++;

                    }

                }




                fileCount++;

            }


            //}

            Console.WriteLine($"CSV file split into {fileCount - 1} smaller files with {rowCount} rows total.");

        }

    }

    public static void ImportFilesToDatabase(string outputDirectory, string data_table_name, int lotid)

    {
        string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

        //string outputDirectory_image = outputDirectory + @"\IMAGES_1\";
        //Console.WriteLine(outputDirectory_image);

        string[] csvFiles = Directory.GetFiles(outputDirectory, "output_*.csv");
        string gender_val = "", dob_val = "";
        string flag = "";

        int batchSize = 10000, i = 0;

        //int table_check = nha_create_table(data_table_name);


        //using (var connection = new SqlConnection(connectionString))

        //{


        //connection.Open();



        foreach (string file in csvFiles)

        {
            using (var connection = new SqlConnection(connectionString))

            {


                connection.Open();

                using (var transaction = connection.BeginTransaction())

                {

                    using (var reader = new StreamReader(file))

                    {

                        using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                        //using (var bulkCopy = new SqlBulkCopy(connectionString))
                        //using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default))
                        //, transaction))


                        {

                            bulkCopy.DestinationTableName = data_table_name;

                            //bulkCopy.BatchSize = 1000; // Set an appropriate batch size for efficient imports




                            // Map the columns in the CSV file to the corresponding database columns
                            // Map the columns from your DataTable to the destination table columns
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
                            //bulkCopy.ColumnMappings.Add("Lot_ID", "Lot_ID");

                            // using the ColumnMappings property of the SqlBulkCopy instance




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
                            //tbl.Columns.Add("Lot_ID", typeof(int));

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


                                    //if (image_convertor(values[10].ToString(), outputDirectory_image, values[0].ToString()) != 1)
                                    //{
                                    //    flag += "_r_invalid_image";
                                    //}


                                    tbl.Rows.Add(values[0].ToString(), values[1].ToString(), dob_val, gender_val, values[4].ToString(), values[5].ToString(), values[6].ToString(), values[7].ToString(), values[8].ToString(), values[9].ToString(), values[10].ToString(), values[11].ToString(), values[12].ToString(), values[13].ToString(), values[14].ToString(), values[15].ToString(), values[16].ToString(), values[17].ToString(), values[18].ToString(), values[19].ToString(), values[20].ToString(), values[21].ToString(), values[22].ToString(), values[23].ToString(), values[24].ToString(), values[25].ToString(), values[26].ToString(), values[27].ToString(), values[28].ToString(), values[29].ToString(), values[30].ToString(), values[31].ToString(), values[32].ToString(), values[33].ToString(), values[34].ToString(), 0, 0, 0, 0, 0, 0, "", "", file.ToString(), DateTime.Now.ToString().ToString(), flag.ToString(), 0, 0, 0);//,lotid);

                                }
                                else
                                {
                                    Console.WriteLine("Error inserting into data table " + values.Length);

                                }

                            }

                            try
                            {
                                bulkCopy.WriteToServer(tbl);
                                Console.WriteLine($"{i} data inserted into database table {data_table_name}");
                                tbl.Clear();
                            }
                            catch (Exception ex)
                            {
                                connection.Close();
                                transaction.Commit();
                                Console.WriteLine();
                            }
                            //connection.Close();
                            //MessageBox.Show($"File instered into database {file}");
                            ////dataGridView1.DataSource = tbl;
                            //if ((i == batchSize && !reader.EndOfStream) || (reader.EndOfStream))
                            //{

                            //    bulkCopy.WriteToServer(tbl);

                            //    //richTextBox1.Text += $"{i} data inserted into database table {data_table_name}";
                            //    //richTextBox1.Text += "\r\n";
                            //    //richTextBox1.SelectionStart = richTextBox1.TextLength;
                            //    //richTextBox1.ScrollToCaret();
                            //    //richTextBox1.Refresh();


                            //    Console.WriteLine($"{i} data inserted into database table {data_table_name}");


                            //    i = 0;
                            //    //tbl.Clear();
                            //}
                            ////MessageBox.Show(tbl.ToString());
                            //tbl.Clear();
                            ////MessageBox.Show(tbl.len());
                            //i = i + 1;


                        }

                    }




                    transaction.Commit();
                }

                connection.Close();

            }
            //connection.Close();

        }



    }

    public static void ImportFilesToDatabase_old(string outputDirectory, string data_table_name, int lotid)

    {
        string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

        System.Windows.Forms.RichTextBox richTextBox1 = new System.Windows.Forms.RichTextBox();

        //string outputDirectory_image = outputDirectory + @"\IMAGES_1\";
        //Console.WriteLine(outputDirectory_image);

        string[] csvFiles = Directory.GetFiles(outputDirectory, "output_*.csv");
        string gender_val = "", dob_val = "";
        string flag = "";

        int batchSize = 10000, i = 0;

        //int table_check = nha_create_table(data_table_name);


        using (var connection = new SqlConnection(connectionString))

        {


            //connection.Open();



            foreach (string file in csvFiles)

            {



                connection.Open();

                using (var transaction = connection.BeginTransaction())

                {

                    using (var reader = new StreamReader(file))

                    {

                        using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                        //using (var bulkCopy = new SqlBulkCopy(connectionString))
                        //using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default))
                        //, transaction))


                        {

                            bulkCopy.DestinationTableName = data_table_name;

                            //bulkCopy.BatchSize = 1000; // Set an appropriate batch size for efficient imports




                            // Map the columns in the CSV file to the corresponding database columns
                            // Map the columns from your DataTable to the destination table columns
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
                            //bulkCopy.ColumnMappings.Add("Lot_ID", "Lot_ID");

                            // using the ColumnMappings property of the SqlBulkCopy instance




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
                            //tbl.Columns.Add("Lot_ID", typeof(int));

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


                                    //if (image_convertor(values[10].ToString(), outputDirectory_image, values[0].ToString()) != 1)
                                    //{
                                    //    flag += "_r_invalid_image";
                                    //}


                                    tbl.Rows.Add(values[0].ToString(), values[1].ToString(), dob_val, gender_val, values[4].ToString(), values[5].ToString(), values[6].ToString(), values[7].ToString(), values[8].ToString(), values[9].ToString(), values[10].ToString(), values[11].ToString(), values[12].ToString(), values[13].ToString(), values[14].ToString(), values[15].ToString(), values[16].ToString(), values[17].ToString(), values[18].ToString(), values[19].ToString(), values[20].ToString(), values[21].ToString(), values[22].ToString(), values[23].ToString(), values[24].ToString(), values[25].ToString(), values[26].ToString(), values[27].ToString(), values[28].ToString(), values[29].ToString(), values[30].ToString(), values[31].ToString(), values[32].ToString(), values[33].ToString(), values[34].ToString(), 0, 0, 0, 0, 0, 0, "", "", file.ToString(), DateTime.Now.ToString().ToString(), flag.ToString(), 0, 0, 0);//,lotid);

                                }
                                else
                                {
                                    Console.WriteLine("Error inserting into data table " + values.Length);

                                }

                            }


                            bulkCopy.WriteToServer(tbl);
                            Console.WriteLine($"{i} data inserted into database table {data_table_name}");
                            tbl.Clear();
                            transaction.Commit();
                            connection.Close();

                            richTextBox1.Text += $"{file} data inserted into database table {data_table_name}";
                            richTextBox1.Text += "\r\n";
                            richTextBox1.SelectionStart = richTextBox1.TextLength;
                            richTextBox1.ScrollToCaret();
                            richTextBox1.Refresh();

                            //connection.Close();
                            //MessageBox.Show($"File instered into database {file}");
                            ////dataGridView1.DataSource = tbl;
                            //if ((i == batchSize && !reader.EndOfStream) || (reader.EndOfStream))
                            //{

                            //    bulkCopy.WriteToServer(tbl);

                            //    //richTextBox1.Text += $"{i} data inserted into database table {data_table_name}";
                            //    //richTextBox1.Text += "\r\n";
                            //    //richTextBox1.SelectionStart = richTextBox1.TextLength;
                            //    //richTextBox1.ScrollToCaret();
                            //    //richTextBox1.Refresh();


                            //    Console.WriteLine($"{i} data inserted into database table {data_table_name}");


                            //    i = 0;
                            //    //tbl.Clear();
                            //}
                            ////MessageBox.Show(tbl.ToString());
                            //tbl.Clear();
                            ////MessageBox.Show(tbl.len());
                            //i = i + 1;


                        }

                    }


                    try { transaction.Commit(); }
                    catch { Console.WriteLine($"transaction already closed"); }

                    //transaction.Commit();
                }

            //    connection.Close();

            }
            try { connection.Close(); }
            catch { Console.WriteLine($"connection already closed data inserted into database table "); }
            //connection.Close();

        }



    }

    public static void ImportFilesToDatabase_2(string outputDirectory, string data_table_name, int lotid)

    {
        string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

        //string outputDirectory_image = outputDirectory + @"\IMAGES_1\";
        //Console.WriteLine(outputDirectory_image);

        string[] csvFiles = Directory.GetFiles(outputDirectory, "output_*.csv");
        string gender_val = "", dob_val = "";
        string flag = "";

        int batchSize = 10000, i = 0;

        //int table_check = nha_create_table(data_table_name);


        using (var connection = new SqlConnection(connectionString))

        {


            //connection.Open();



            foreach (string file in csvFiles)

            {


                StreamReader reader = null;
                try
                {


                    using (reader = new StreamReader(file))

                    {

                        using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))

                        //using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                        //using (var bulkCopy = new SqlBulkCopy(connectionString))
                        //using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default))
                        //, transaction))


                        {

                            bulkCopy.DestinationTableName = data_table_name;

                            //bulkCopy.BatchSize = 1000; // Set an appropriate batch size for efficient imports




                            // Map the columns in the CSV file to the corresponding database columns
                            // Map the columns from your DataTable to the destination table columns
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
                            //bulkCopy.ColumnMappings.Add("Lot_ID", "Lot_ID");

                            // using the ColumnMappings property of the SqlBulkCopy instance




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
                            //tbl.Columns.Add("Lot_ID", typeof(int));

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


                                    //if (image_convertor(values[10].ToString(), outputDirectory_image, values[0].ToString()) != 1)
                                    //{
                                    //    flag += "_r_invalid_image";
                                    //}


                                    tbl.Rows.Add(values[0].ToString(), values[1].ToString(), dob_val, gender_val, values[4].ToString(), values[5].ToString(), values[6].ToString(), values[7].ToString(), values[8].ToString(), values[9].ToString(), values[10].ToString(), values[11].ToString(), values[12].ToString(), values[13].ToString(), values[14].ToString(), values[15].ToString(), values[16].ToString(), values[17].ToString(), values[18].ToString(), values[19].ToString(), values[20].ToString(), values[21].ToString(), values[22].ToString(), values[23].ToString(), values[24].ToString(), values[25].ToString(), values[26].ToString(), values[27].ToString(), values[28].ToString(), values[29].ToString(), values[30].ToString(), values[31].ToString(), values[32].ToString(), values[33].ToString(), values[34].ToString(), 0, 0, 0, 0, 0, 0, "", "", file.ToString(), DateTime.Now.ToString().ToString(), flag.ToString(), 0, 0, 0);//,lotid);

                                }
                                else
                                {
                                    Console.WriteLine("Error inserting into data table " + values.Length);

                                }

                            }
                            connection.Open();

                            bulkCopy.WriteToServer(tbl);
                            Console.WriteLine($"{i} data inserted into database table {data_table_name}");
                            tbl.Clear();
                            connection.Close();
                            
                            //connection.Close();
                            //MessageBox.Show($"File instered into database {file}");
                            ////dataGridView1.DataSource = tbl;
                            //if ((i == batchSize && !reader.EndOfStream) || (reader.EndOfStream))
                            //{

                            //    bulkCopy.WriteToServer(tbl);

                            //    //richTextBox1.Text += $"{i} data inserted into database table {data_table_name}";
                            //    //richTextBox1.Text += "\r\n";
                            //    //richTextBox1.SelectionStart = richTextBox1.TextLength;
                            //    //richTextBox1.ScrollToCaret();
                            //    //richTextBox1.Refresh();


                            //    Console.WriteLine($"{i} data inserted into database table {data_table_name}");


                            //    i = 0;
                            //    //tbl.Clear();
                            //}
                            ////MessageBox.Show(tbl.ToString());
                            //tbl.Clear();
                            ////MessageBox.Show(tbl.len());
                            //i = i + 1;


                        }

                    }
                }
                finally
                {
                    // Close the StreamReader and release the file resources
                    if (reader != null)
                    {
                        reader.Close();
                    }
                }









            }
            //connection.Close();

        }



    }
    public static void ImportFilesToDatabase_1(string outputDirectory, string data_table_name, int lotid)

    {
        string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

        //string outputDirectory_image = outputDirectory + @"\IMAGES_1\";
        //Console.WriteLine(outputDirectory_image);

        string[] csvFiles = Directory.GetFiles(outputDirectory, "output_*.csv");
        string gender_val = "", dob_val = "";
        string flag = "";

        int batchSize = 10000, i = 0;

        //int table_check = nha_create_table(data_table_name);


        using (var connection = new SqlConnection(connectionString))

        {


            //connection.Open();



            foreach (string file in csvFiles)

            {
                System.Data.DataTable tbl = new System.Data.DataTable();
                using (var reader = new StreamReader(file))

                {

                    
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
                    //tbl.Columns.Add("Lot_ID", typeof(int));

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


                            //if (image_convertor(values[10].ToString(), outputDirectory_image, values[0].ToString()) != 1)
                            //{
                            //    flag += "_r_invalid_image";
                            //}


                            tbl.Rows.Add(values[0].ToString(), values[1].ToString(), dob_val, gender_val, values[4].ToString(), values[5].ToString(), values[6].ToString(), values[7].ToString(), values[8].ToString(), values[9].ToString(), values[10].ToString(), values[11].ToString(), values[12].ToString(), values[13].ToString(), values[14].ToString(), values[15].ToString(), values[16].ToString(), values[17].ToString(), values[18].ToString(), values[19].ToString(), values[20].ToString(), values[21].ToString(), values[22].ToString(), values[23].ToString(), values[24].ToString(), values[25].ToString(), values[26].ToString(), values[27].ToString(), values[28].ToString(), values[29].ToString(), values[30].ToString(), values[31].ToString(), values[32].ToString(), values[33].ToString(), values[34].ToString(), 0, 0, 0, 0, 0, 0, "", "", file.ToString(), DateTime.Now.ToString().ToString(), flag.ToString(), 0, 0, 0);//,lotid);

                        }
                        else
                        {
                            Console.WriteLine("Error inserting into data table " + values.Length);

                        }

                    }


                }

                connection.Open();

                using (var transaction = connection.BeginTransaction())

                {

                   

                    using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                    //using (var bulkCopy = new SqlBulkCopy(connectionString))
                    //using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default))
                    //, transaction))


                    {

                        bulkCopy.DestinationTableName = data_table_name;

                        //bulkCopy.BatchSize = 1000; // Set an appropriate batch size for efficient imports




                        // Map the columns in the CSV file to the corresponding database columns
                        // Map the columns from your DataTable to the destination table columns
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
                        //bulkCopy.ColumnMappings.Add("Lot_ID", "Lot_ID");

                        // using the ColumnMappings property of the SqlBulkCopy instance




                        bulkCopy.WriteToServer(tbl);
                        Console.WriteLine($"{i} data inserted into database table {data_table_name}");
                        tbl.Clear();

                    }



                    //connection.Close();
                    //MessageBox.Show($"File instered into database {file}");
                    ////dataGridView1.DataSource = tbl;
                    //if ((i == batchSize && !reader.EndOfStream) || (reader.EndOfStream))
                    //{

                    //    bulkCopy.WriteToServer(tbl);

                    //    //richTextBox1.Text += $"{i} data inserted into database table {data_table_name}";
                    //    //richTextBox1.Text += "\r\n";
                    //    //richTextBox1.SelectionStart = richTextBox1.TextLength;
                    //    //richTextBox1.ScrollToCaret();
                    //    //richTextBox1.Refresh();


                    //    Console.WriteLine($"{i} data inserted into database table {data_table_name}");


                    //    i = 0;
                    //    //tbl.Clear();
                    //}
                    ////MessageBox.Show(tbl.ToString());
                    //tbl.Clear();
                    ////MessageBox.Show(tbl.len());
                    //i = i + 1;

                    transaction.Commit();
                }



                

                connection.Close();

            }
            //connection.Close();

        }



    }
    //public static void ImportbulkFilesToDatabase(string sourceFilePath, string data_table_name)

    //{
    //    string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

    //    //string outputDirectory_image = outputDirectory + @"\IMAGES_1\";
    //    //Console.WriteLine(outputDirectory_image);

    //    string[] csvFiles = Directory.GetFiles(sourceFilePath, "*.csv");
    //    string gender_val = "", dob_val = "";
    //    string flag = "";

    //    int batchSize = 10000, i = 0;

    //    int table_check = nha_create_table(data_table_name);

    //    using (var connection = new SqlConnection(connectionString))

    //    {

    //        connection.Open();




    //        foreach (string file in csvFiles)

    //        {

    //            using (var transaction = connection.BeginTransaction())

    //            {

    //                using (var reader = new StreamReader(file))

    //                {

    //                    using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
    //                    //using (var bulkCopy = new SqlBulkCopy(connectionString))
    //                    //using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default))
    //                    //, transaction))


    //                    {

    //                        bulkCopy.DestinationTableName = data_table_name;

    //                        //bulkCopy.BatchSize = 1000; // Set an appropriate batch size for efficient imports




    //                        // Map the columns in the CSV file to the corresponding database columns
    //                        // Map the columns from your DataTable to the destination table columns
    //                        bulkCopy.ColumnMappings.Add("pmrssm_id", "pmrssm_id");
    //                        bulkCopy.ColumnMappings.Add("name_ben", "name_ben");
    //                        bulkCopy.ColumnMappings.Add("dob_ben", "dob_ben");
    //                        bulkCopy.ColumnMappings.Add("gender_ben", "gender_ben");
    //                        bulkCopy.ColumnMappings.Add("address_ben", "address_ben");
    //                        bulkCopy.ColumnMappings.Add("state_codelgd_ben", "state_codelgd_ben");
    //                        bulkCopy.ColumnMappings.Add("district_codelgd_ben", "district_codelgd_ben");
    //                        bulkCopy.ColumnMappings.Add("subdistrict_codelgd_ben", "subdistrict_codelgd_ben");
    //                        bulkCopy.ColumnMappings.Add("villagetown_code_lgd_ben", "villagetown_code_lgd_ben");
    //                        bulkCopy.ColumnMappings.Add("rural_urban_ben", "rural_urban_ben");
    //                        bulkCopy.ColumnMappings.Add("doc_pic", "doc_pic");
    //                        bulkCopy.ColumnMappings.Add("ahl_hhid", "ahl_hhid");
    //                        bulkCopy.ColumnMappings.Add("health_id", "health_id");
    //                        bulkCopy.ColumnMappings.Add("mobile_member", "mobile_member");
    //                        bulkCopy.ColumnMappings.Add("care_of_dec", "care_of_dec");
    //                        bulkCopy.ColumnMappings.Add("userid", "userid");
    //                        bulkCopy.ColumnMappings.Add("user_name", "user_name");
    //                        bulkCopy.ColumnMappings.Add("user_districtcode", "user_districtcode");
    //                        bulkCopy.ColumnMappings.Add("user_statecode", "user_statecode");
    //                        bulkCopy.ColumnMappings.Add("user_blockcode", "user_blockcode");
    //                        bulkCopy.ColumnMappings.Add("user_villagecode", "user_villagecode");
    //                        bulkCopy.ColumnMappings.Add("user_towncode", "user_towncode");
    //                        bulkCopy.ColumnMappings.Add("user_wardcode", "user_wardcode");
    //                        bulkCopy.ColumnMappings.Add("source_of_data", "source_of_data");
    //                        bulkCopy.ColumnMappings.Add("app_flag", "app_flag");
    //                        bulkCopy.ColumnMappings.Add("pmrssm_id2", "pmrssm_id2");
    //                        bulkCopy.ColumnMappings.Add("ahl_hhid2", "ahl_hhid2");
    //                        bulkCopy.ColumnMappings.Add("state_codelgd_ben2", "state_codelgd_ben2");
    //                        bulkCopy.ColumnMappings.Add("district_codelgd_ben2", "district_codelgd_ben2");
    //                        bulkCopy.ColumnMappings.Add("subdistrict_codelgd_ben2", "subdistrict_codelgd_ben2");
    //                        bulkCopy.ColumnMappings.Add("villagetown_code_lgd_ben2", "villagetown_code_lgd_ben2");
    //                        bulkCopy.ColumnMappings.Add("state_name_english", "state_name_english");
    //                        bulkCopy.ColumnMappings.Add("district_name_english", "district_name_english");
    //                        bulkCopy.ColumnMappings.Add("block_name_english", "block_name_english");
    //                        bulkCopy.ColumnMappings.Add("village_name_english", "village_name_english");
    //                        bulkCopy.ColumnMappings.Add("IBN", "IBN");
    //                        bulkCopy.ColumnMappings.Add("OBN", "OBN");
    //                        bulkCopy.ColumnMappings.Add("Sheet_no", "Sheet_no");
    //                        bulkCopy.ColumnMappings.Add("IBN_max", "IBN_max");
    //                        bulkCopy.ColumnMappings.Add("OBN_max", "OBN_max");
    //                        bulkCopy.ColumnMappings.Add("Sheet_max", "Sheet_max");
    //                        bulkCopy.ColumnMappings.Add("Seperator", "Seperator");
    //                        bulkCopy.ColumnMappings.Add("yob", "yob");
    //                        bulkCopy.ColumnMappings.Add("File_Name", "File_Name");
    //                        bulkCopy.ColumnMappings.Add("Upload_Date_Time", "Upload_Date_Time");
    //                        bulkCopy.ColumnMappings.Add("FLAG", "FLAG");
    //                        bulkCopy.ColumnMappings.Add("counter", "counter");
    //                        bulkCopy.ColumnMappings.Add("pdf_page_1", "pdf_page_1");
    //                        bulkCopy.ColumnMappings.Add("pdf_page_2", "pdf_page_2");

    //                        // using the ColumnMappings property of the SqlBulkCopy instance




    //                        System.Data.DataTable tbl = new System.Data.DataTable();
    //                        //System.Data.DataTable tbl = new System.Data.DataTable();
    //                        tbl.Columns.Add("pmrssm_id", typeof(string));
    //                        tbl.Columns.Add("name_ben", typeof(string));
    //                        tbl.Columns.Add("dob_ben", typeof(string));
    //                        tbl.Columns.Add("gender_ben", typeof(string));
    //                        tbl.Columns.Add("address_ben", typeof(string));
    //                        tbl.Columns.Add("state_codelgd_ben", typeof(string));
    //                        tbl.Columns.Add("district_codelgd_ben", typeof(string));
    //                        tbl.Columns.Add("subdistrict_codelgd_ben", typeof(string));
    //                        tbl.Columns.Add("villagetown_code_lgd_ben", typeof(string));
    //                        tbl.Columns.Add("rural_urban_ben", typeof(string));
    //                        tbl.Columns.Add("doc_pic", typeof(string));
    //                        tbl.Columns.Add("ahl_hhid", typeof(string));
    //                        tbl.Columns.Add("health_id", typeof(string));
    //                        tbl.Columns.Add("mobile_member", typeof(string));
    //                        tbl.Columns.Add("care_of_dec", typeof(string));
    //                        tbl.Columns.Add("userid", typeof(string));
    //                        tbl.Columns.Add("user_name", typeof(string));
    //                        tbl.Columns.Add("user_districtcode", typeof(string));
    //                        tbl.Columns.Add("user_statecode", typeof(string));
    //                        tbl.Columns.Add("user_blockcode", typeof(string));
    //                        tbl.Columns.Add("user_villagecode", typeof(string));
    //                        tbl.Columns.Add("user_towncode", typeof(string));
    //                        tbl.Columns.Add("user_wardcode", typeof(string));
    //                        tbl.Columns.Add("source_of_data", typeof(string));
    //                        tbl.Columns.Add("app_flag", typeof(string));
    //                        tbl.Columns.Add("pmrssm_id2", typeof(string));
    //                        tbl.Columns.Add("ahl_hhid2", typeof(string));
    //                        tbl.Columns.Add("state_codelgd_ben2", typeof(string));
    //                        tbl.Columns.Add("district_codelgd_ben2", typeof(string));
    //                        tbl.Columns.Add("subdistrict_codelgd_ben2", typeof(string));
    //                        tbl.Columns.Add("villagetown_code_lgd_ben2", typeof(string));
    //                        tbl.Columns.Add("state_name_english", typeof(string));
    //                        tbl.Columns.Add("district_name_english", typeof(string));
    //                        tbl.Columns.Add("block_name_english", typeof(string));
    //                        tbl.Columns.Add("village_name_english", typeof(string));
    //                        tbl.Columns.Add("IBN", typeof(int));
    //                        tbl.Columns.Add("OBN", typeof(int));
    //                        tbl.Columns.Add("Sheet_no", typeof(int));
    //                        tbl.Columns.Add("IBN_max", typeof(int));
    //                        tbl.Columns.Add("OBN_max", typeof(int));
    //                        tbl.Columns.Add("Sheet_max", typeof(int));
    //                        tbl.Columns.Add("Seperator", typeof(string));
    //                        tbl.Columns.Add("yob", typeof(string));
    //                        tbl.Columns.Add("File_Name", typeof(string));
    //                        tbl.Columns.Add("Upload_Date_Time", typeof(string));
    //                        tbl.Columns.Add("FLAG", typeof(string));
    //                        tbl.Columns.Add("counter", typeof(int));
    //                        tbl.Columns.Add("pdf_page_1", typeof(int));
    //                        tbl.Columns.Add("pdf_page_2", typeof(int));

    //                        reader.ReadLine();
    //                        while (!reader.EndOfStream)
    //                        {
    //                            String newline = reader.ReadLine();
    //                            string[] values = newline.Split('|');
    //                            flag = "";
    //                            if (values.Length == 35)
    //                            {

    //                                try
    //                                {
    //                                    gender_val = values[3].ToString();

    //                                    if (gender_val == "M")
    //                                    {
    //                                        gender_val = "Male";
    //                                    }
    //                                    else if (gender_val == "F")
    //                                    {
    //                                        gender_val = "Female";
    //                                    }
    //                                    else if (gender_val == "T")
    //                                    {
    //                                        gender_val = "Transgender";
    //                                    }



    //                                }
    //                                catch (Exception exe)
    //                                {
    //                                    // yob = row["dob_ben"].ToString();
    //                                    flag += "R_Wrong_gender";
    //                                }

    //                                try
    //                                {
    //                                    dob_val = values[2].ToString();

    //                                    if (dob_val.Substring(0, 4).IndexOf('_') >= 0)
    //                                    {
    //                                        flag += "R_Wrong_dob_ben";
    //                                    }
    //                                    else
    //                                    {
    //                                        dob_val = dob_val.Substring(0, 4);
    //                                    }



    //                                }
    //                                catch (Exception exe)
    //                                {
    //                                    // yob = row["dob_ben"].ToString();
    //                                    flag += "_R_Wrong_dob_ben";
    //                                }

    //                                if (IsNull(values[0].ToString()))
    //                                {
    //                                    flag += "_R_INVALID_PMJAY_ID";
    //                                }
    //                                if (IsNull(values[1].ToString()))
    //                                {
    //                                    flag += "_R_INVALID_NAME";
    //                                }

    //                                if (IsNull(values[12].ToString()))
    //                                {
    //                                    flag += "_R_ABHA_ID";
    //                                }

    //                                if (IsNull(values[34].ToString()))
    //                                {
    //                                    flag += "_R_INVALID_Village_Name";
    //                                }

    //                                if (IsNull(values[33].ToString()))
    //                                {
    //                                    flag += "_R_INVALID_Block_Name";
    //                                }

    //                                if (IsNull(values[32].ToString()))
    //                                {
    //                                    flag += "_R_INVALID_District_Name";
    //                                }

    //                                if (IsNull(values[10].ToString()))
    //                                {
    //                                    flag += "_R_INVALID_IMAGE";
    //                                }


    //                                //if (image_convertor(values[10].ToString(), outputDirectory_image, values[0].ToString()) != 1)
    //                                //{
    //                                //    flag += "_r_invalid_image";
    //                                //}


    //                                tbl.Rows.Add(values[0].ToString(), values[1].ToString(), dob_val, gender_val, values[4].ToString(), values[5].ToString(), values[6].ToString(), values[7].ToString(), values[8].ToString(), values[9].ToString(), values[10].ToString(), values[11].ToString(), values[12].ToString(), values[13].ToString(), values[14].ToString(), values[15].ToString(), values[16].ToString(), values[17].ToString(), values[18].ToString(), values[19].ToString(), values[20].ToString(), values[21].ToString(), values[22].ToString(), values[23].ToString(), values[24].ToString(), values[25].ToString(), values[26].ToString(), values[27].ToString(), values[28].ToString(), values[29].ToString(), values[30].ToString(), values[31].ToString(), values[32].ToString(), values[33].ToString(), values[34].ToString(), 0, 0, 0, 0, 0, 0, "", "", file.ToString(), DateTime.Now.ToString().ToString(), flag.ToString(), 0, 0, 0);

    //                            }
    //                            else
    //                            {
    //                                Console.WriteLine("Error inserting into data table " + values.Length);

    //                            }


    //                        }
    //                        //dataGridView1.DataSource = tbl;
    //                        if ((i == batchSize && !reader.EndOfStream) || (reader.EndOfStream))
    //                        {

    //                            bulkCopy.WriteToServer(tbl);

    //                            //richTextBox1.Text += $"{i} data inserted into database table {data_table_name}";
    //                            //richTextBox1.Text += "\r\n";
    //                            //richTextBox1.SelectionStart = richTextBox1.TextLength;
    //                            //richTextBox1.ScrollToCaret();
    //                            //richTextBox1.Refresh();
    //                            Console.WriteLine($"{i} data inserted into database table {data_table_name}");


    //                            i = 0;

    //                        }
    //                        tbl.Clear();
    //                        i = i + 1;

    //                    }

    //                }




    //                transaction.Commit();
    //            }

    //        }
    //        connection.Close();

    //    }

    //}


    public static void ImportFilesToDatabase_4(string outputDirectory, string data_table_name, int lotid)

    {
        string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

        //string outputDirectory_image = outputDirectory + @"\IMAGES_1\";
        //Console.WriteLine(outputDirectory_image);

        string[] csvFiles = Directory.GetFiles(outputDirectory, "output_*.csv");
        string gender_val = "", dob_val = "";
        string flag = "";

        int batchSize = 10000, i = 0;

        //int table_check = nha_create_table(data_table_name);


        //using (var connection = new SqlConnection(connectionString))

        //{


        //connection.Open();



        using (var connection = new SqlConnection(connectionString))

        {


            connection.Open();

            using (var transaction = connection.BeginTransaction())

            {


                using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, transaction))
                //using (var bulkCopy = new SqlBulkCopy(connectionString))
                //using (var bulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default))
                //, transaction))


                {

                    bulkCopy.DestinationTableName = data_table_name;

                    //bulkCopy.BatchSize = 1000; // Set an appropriate batch size for efficient imports




                    // Map the columns in the CSV file to the corresponding database columns
                    // Map the columns from your DataTable to the destination table columns
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
                    //bulkCopy.ColumnMappings.Add("Lot_ID", "Lot_ID");

                    // using the ColumnMappings property of the SqlBulkCopy instance




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
                    //tbl.Columns.Add("Lot_ID", typeof(int));
                    
                    foreach (string file in csvFiles)

                    {

                        using (var reader = new StreamReader(file))

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


                                    //if (image_convertor(values[10].ToString(), outputDirectory_image, values[0].ToString()) != 1)
                                    //{
                                    //    flag += "_r_invalid_image";
                                    //}


                                    tbl.Rows.Add(values[0].ToString(), values[1].ToString(), dob_val, gender_val, values[4].ToString(), values[5].ToString(), values[6].ToString(), values[7].ToString(), values[8].ToString(), values[9].ToString(), values[10].ToString(), values[11].ToString(), values[12].ToString(), values[13].ToString(), values[14].ToString(), values[15].ToString(), values[16].ToString(), values[17].ToString(), values[18].ToString(), values[19].ToString(), values[20].ToString(), values[21].ToString(), values[22].ToString(), values[23].ToString(), values[24].ToString(), values[25].ToString(), values[26].ToString(), values[27].ToString(), values[28].ToString(), values[29].ToString(), values[30].ToString(), values[31].ToString(), values[32].ToString(), values[33].ToString(), values[34].ToString(), 0, 0, 0, 0, 0, 0, "", "", file.ToString(), DateTime.Now.ToString().ToString(), flag.ToString(), 0, 0, 0);//,lotid);

                                }
                                else
                                {
                                    Console.WriteLine("Error inserting into data table " + values.Length);

                                }

                            }

                            try
                            {
                                bulkCopy.WriteToServer(tbl);
                                Console.WriteLine($"{i} data inserted into database table {data_table_name}");
                                tbl.Clear();
                            }
                            catch (Exception ex)
                            {
                                connection.Close();
                                transaction.Commit();
                                Console.WriteLine();
                            }
                            

                        }

                    }




                    transaction.Commit();
                }

                connection.Close();

            }
            //connection.Close();

        }



    }


    


}