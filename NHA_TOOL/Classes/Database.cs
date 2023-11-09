using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace NHA_TOOL
{
    class Database
    {
        public static string sql_data_value(string query_sql, string data_name)
        {
            string sql_data_val = "";
            //string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
            using (SqlConnection con1 = new SqlConnection(connectionString))
            {

                using (SqlCommand cmd = new SqlCommand(query_sql, con1))
                {
                    try
                    {
                        con1.Open();
                        SqlDataReader myReader = cmd.ExecuteReader();
                        if (myReader.HasRows)
                        {
                            while (myReader.Read())
                                sql_data_val = myReader[data_name].ToString().Trim();
                        }
                        else
                        { sql_data_val = "0"; }

                        //MessageBox.Show(sql_data_val);
                        con1.Close();

                    }
                    catch (Exception exe)
                    {
                        con1.Close();
                        MessageBox.Show(exe.Message);
                    }
                }



            }
            return sql_data_val;
        }


        public static string sql_data_update(string query_sql)
        {
            string sql_data_val = "";
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con1 = new SqlConnection(connectionString))
            {

                using (SqlCommand cmd = new SqlCommand(query_sql, con1))
                {
                    try
                    {


                        con1.Open();
                        SqlDataReader myReader = cmd.ExecuteReader();

                        //MessageBox.Show(sql_data_val);
                        con1.Close();

                    }
                    catch (Exception exe)
                    {
                        MessageBox.Show(exe.Message);
                    }
                }



            }
            return sql_data_val;
        }

        public static DataTable sql_data_value_extra(string query_sql)
        {
            string sql_data_val = "";
            string connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            using (SqlConnection con1 = new SqlConnection(connectionString))
            {

                using (SqlCommand cmd = new SqlCommand(query_sql, con1))
                {
                    try
                    {
                        con1.Open();
                        SqlDataReader myReader = cmd.ExecuteReader();

                        DataTable dt = new DataTable();
                        dt.Load(myReader);
                        con1.Close();
                        return dt;



                    }
                    catch (Exception exe)
                    {
                        MessageBox.Show(exe.Message);
                        return null;
                    }
                }



            }
        }


        public static int DupCheck(String Cust_Name, String File_Upload, string Folder)
        {
            using (SqlConnection sqlCon = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["DBCon"].ConnectionString))
            {
                sqlCon.Open();

                //using (SqlCommand sqlCmd1 = new SqlCommand { CommandText = "INSERT INTO [Page_Setup] ([TOP], [BOTTOM], [LEFT], [RIGHT],[TEMPLATE],[DateTime],[Insertedby]) select @top, @bottom, @left, @right, @template, @datetime, @insertedby  where not exists (select [TOP], [BOTTOM], [LEFT], [RIGHT],[TEMPLATE] from [Page_Setup] where [TOP] = @top and [BOTTOM] = @bottom and [LEFT] = @left and [right] = @right and [TEMPLATE] = @template)", Connection = sqlCon })
                using (SqlCommand sqlCmd1 = new SqlCommand { CommandText = "INSERT INTO [DupCheck] ([Client], [Folder], [FileName], [datetime], [System_Name]) select @Cust_Name, @Folder, @File_Upload, @datetime, @System_Name  where not exists (select [FileName] from [DupCheck] where  [FileName] = @File_Upload)", Connection = sqlCon })
                {
                    int Added = 0;
                    sqlCmd1.Parameters.AddWithValue("@File_Upload", File_Upload);
                    sqlCmd1.Parameters.AddWithValue("@Cust_Name", Cust_Name);
                    sqlCmd1.Parameters.AddWithValue("@Folder", Folder);
                    sqlCmd1.Parameters.AddWithValue("@datetime", DateTime.Now);
                    sqlCmd1.Parameters.AddWithValue("@System_Name", System.Environment.MachineName);
                    try
                    {
                        Added = sqlCmd1.ExecuteNonQuery();
                        sqlCon.Close();
                        //WriteLog.Log("File_Upload: " + File_Upload + Added.ToString());
                        return Added;
                    }
                    catch (Exception e)
                    {
                        //WriteLog.Log("Error Occoured in File Insertion: " + e.ToString());
                        sqlCon.Close();
                        return -1;
                    }

                }

            }

        }

    }
}
