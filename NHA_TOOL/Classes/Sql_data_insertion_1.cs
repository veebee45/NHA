using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;


class Sql_data_insertion_1
{
    public static void ImportFilesToDatabase_new(string outputDirectory, string data_table_name, int lotid,string input_file_name )

    {
        string connectionString = ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString;

        string tableName = data_table_name;

        DataTable dataTable = ReadDataFromFile(input_file_name);


        using (DbContext dbContext = new DbContext(connectionString))
        {
            using (var transaction = dbContext.Database.BeginTransaction())
            {
                try
                {
                    ////Database database = dbContext.Database;
                    //using (var bulkCopy = new SqlBulkCopy(dbContext, SqlBulkCopyOptions.Default, transaction))

                    //{
                    //    bulkCopy.DestinationTableName = tableName;
                    //    bulkCopy.WriteToServer(dataTable);
                    //}



                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Handle the exception
                }
            }
        }
    }

    static DataTable ReadDataFromFile(string filePath)
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
        //tbl.Columns.Add("Lot_ID", typeof(int));
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

                    //try
                    //{
                    //    gender_val = values[3].ToString();

                    //    if (gender_val == "M")
                    //    {
                    //        gender_val = "Male";
                    //    }
                    //    else if (gender_val == "F")
                    //    {
                    //        gender_val = "Female";
                    //    }
                    //    else if (gender_val == "T")
                    //    {
                    //        gender_val = "Transgender";
                    //    }



                    //}
                    //catch (Exception exe)
                    //{
                    //    // yob = row["dob_ben"].ToString();
                    //    flag += "R_Wrong_gender";
                    //}

                    //try
                    //{
                    //    dob_val = values[2].ToString();

                    //    if (dob_val.Substring(0, 4).IndexOf('_') >= 0)
                    //    {
                    //        flag += "R_Wrong_dob_ben";
                    //    }
                    //    else
                    //    {
                    //        dob_val = dob_val.Substring(0, 4);
                    //    }



                    //}
                    //catch (Exception exe)
                    //{
                    //    // yob = row["dob_ben"].ToString();
                    //    flag += "_R_Wrong_dob_ben";
                    //}

                    //if (IsNull(values[0].ToString()))
                    //{
                    //    flag += "_R_INVALID_PMJAY_ID";
                    //}
                    //if (IsNull(values[1].ToString()))
                    //{
                    //    flag += "_R_INVALID_NAME";
                    //}

                    //if (IsNull(values[12].ToString()))
                    //{
                    //    flag += "_R_ABHA_ID";
                    //}

                    //if (IsNull(values[34].ToString()))
                    //{
                    //    flag += "_R_INVALID_Village_Name";
                    //}

                    //if (IsNull(values[33].ToString()))
                    //{
                    //    flag += "_R_INVALID_Block_Name";
                    //}

                    //if (IsNull(values[32].ToString()))
                    //{
                    //    flag += "_R_INVALID_District_Name";
                    //}

                    //if (IsNull(values[10].ToString()))
                    //{
                    //    flag += "_R_INVALID_IMAGE";
                    //}


                    //if (image_convertor(values[10].ToString(), outputDirectory_image, values[0].ToString()) != 1)
                    //{
                    //    flag += "_r_invalid_image";
                    //}


                    tbl.Rows.Add(values[0].ToString(), values[1].ToString(), dob_val, gender_val, values[4].ToString(), values[5].ToString(), values[6].ToString(), values[7].ToString(), values[8].ToString(), values[9].ToString(), values[10].ToString(), values[11].ToString(), values[12].ToString(), values[13].ToString(), values[14].ToString(), values[15].ToString(), values[16].ToString(), values[17].ToString(), values[18].ToString(), values[19].ToString(), values[20].ToString(), values[21].ToString(), values[22].ToString(), values[23].ToString(), values[24].ToString(), values[25].ToString(), values[26].ToString(), values[27].ToString(), values[28].ToString(), values[29].ToString(), values[30].ToString(), values[31].ToString(), values[32].ToString(), values[33].ToString(), values[34].ToString(), 0, 0, 0, 0, 0, 0, "", "", filePath.ToString(), DateTime.Now.ToString().ToString(), flag.ToString(), 0, 0, 0);//,lotid);

                }
                else
                {
                    Console.WriteLine("Error inserting into data table " + values.Length);

                }

            }
        }

        return tbl;
    }
}
