using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Office.Interop.Excel;

namespace NHA_TOOL
{
    class Filtered_data
    {
        
        public static int filtered_data_insertion( string data_table_name, int lot_id_current )
        {
            int status = 0;
            // Replace the connection string with your own
            string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

            //int lot_id_current  = 1+ Int32.Parse(Database.sql_data_value("SELECT top 1 [Lot_Id] FROM [dbo].[Filtered_data]  order by [Lot_Id] desc ", "Lot_Id"));

            // Create a SqlConnection object
            using (SqlConnection connection_data_insertion = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection_data_insertion.Open();

                    // Create a SqlCommand object with the stored procedure name and connection
                    using (SqlCommand command = new SqlCommand("insert_into_filtered_data", connection_data_insertion))
                    {
                        // Set the command type as stored procedure
                        command.CommandTimeout = 12000;
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@tablename", data_table_name);
                        command.Parameters.AddWithValue("@lot_id", lot_id_current);

                        // Execute the command
                        command.ExecuteNonQuery();

                        Console.WriteLine("Data Inserted into filtered table ");
                        status = 1;
                        messageshow($"nha filtered data inserted into filtered table for lot : {lot_id_current}");

                    }
                    connection_data_insertion.Close();
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    Console.WriteLine("Error while inserting data into filtered data: " + ex.Message);
                }
            }
            return  status;
        }


        public static int unfiltered_data_insertion(string data_table_name ,int lot_id_current)
        {
            int unfiltered_data_insertion_status = 0;
            // Replace the connection string with your own
            string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

            //int lot_id_current = 1 + Int32.Parse(Database.sql_data_value("SELECT top 1 [Lot_Id] FROM [dbo].[UnFiltered_data]  order by [Lot_Id] desc ", "Lot_Id"));

            // Create a SqlConnection object
            using (SqlConnection connection_unfiltered_data = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection_unfiltered_data.Open();

                    // Create a SqlCommand object with the stored procedure name and connection
                    using (SqlCommand command = new SqlCommand("insert_into_unfiltered_data", connection_unfiltered_data))
                    {
                        // Set the command type as stored procedure
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@tablename", data_table_name);
                        command.Parameters.AddWithValue("@lot_id", lot_id_current);
                        command.CommandTimeout = 0;

                        // Execute the command
                        command.ExecuteNonQuery();
                        unfiltered_data_insertion_status = 1;
                        messageshow($"nha unfiltered data inserted into unfiltered table for lot : {lot_id_current}");
                    }
                    connection_unfiltered_data.Close();
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    Console.WriteLine("Error while inserting data into filtered data: " + ex.Message);
                }
            }
            return unfiltered_data_insertion_status;
        }


        public static int  filtered_data_prcocessing(string data_table_name, int lot_id_current , int innerboxqtyfiltertable, int outerboxqtyfiltertable,string orderByData)
        {
            int filtered_data_prcocessing_status = 0;
            // Replace the connection string with your own
            string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

            //int lot_id_current = Int32.Parse(Database.sql_data_value("SELECT top 1 [Lot_Id] FROM [dbo].[Filtered_data]  order by [Lot_Id] desc ", "Lot_Id"));

            // Create a SqlConnection object
            using (SqlConnection connection_data_prcocessing = new SqlConnection(connectionString))
            {
                try
                {
                    // Open the connection
                    connection_data_prcocessing.Open();

                    // Create a SqlCommand object with the stored procedure name and connection
                    using (SqlCommand command = new SqlCommand("usp_nha_process_1", connection_data_prcocessing))
                    {
                        // Set the command type as stored procedure
                        command.CommandTimeout = 12000;
                        command.CommandType = CommandType.StoredProcedure;

                        // Add parameters to the command
                        command.Parameters.AddWithValue("@tablename", data_table_name);
                        command.Parameters.AddWithValue("@Lot_id", lot_id_current);
                        //command.Parameters.AddWithValue("@inner_box_qty", innerboxqtyfiltertable);
                        //command.Parameters.AddWithValue("@outter_box_qty", outerboxqtyfiltertable);

                        // Execute the command
                        command.ExecuteNonQuery();
                        filtered_data_prcocessing_status = 1;
                    }
                    connection_data_prcocessing.Close();
                    messageshow($"nha seperator added for lot : {lot_id_current}");
                }
                catch (Exception ex)
                {
                    // Handle any errors
                    Console.WriteLine("Error while inserting data into filtered data: " + ex.Message);
                }
            }

            if (filtered_data_prcocessing_status == 1)
            {
                // Create a SqlConnection object
                using (SqlConnection connection_data_prcocessing_2 = new SqlConnection(connectionString))
                {
                    try
                    {
                        // Open the connection
                        connection_data_prcocessing_2.Open();

                        // Create a SqlCommand object with the stored procedure name and connection
                        using (SqlCommand command = new SqlCommand("usp_nha_process_2", connection_data_prcocessing_2))
                        {
                            //Setting the command timeout 
                           // command.CommandTimeout = 
                           command.CommandTimeout = 12000;  // 120 min

                            // Set the command type as stored procedure
                            command.CommandType = CommandType.StoredProcedure;


                            // Add parameters to the command
                            command.Parameters.AddWithValue("@tablename", data_table_name);
                            command.Parameters.AddWithValue("@Lot_id", lot_id_current);
                            command.Parameters.AddWithValue("@inner_box_qty", innerboxqtyfiltertable);
                            command.Parameters.AddWithValue("@outter_box_qty", outerboxqtyfiltertable);
                            command.Parameters.AddWithValue("@sorting", orderByData.Replace('|',','));

                            // Execute the command
                            command.ExecuteNonQuery();
                            filtered_data_prcocessing_status = 1;
                        }
                        connection_data_prcocessing_2.Close();
                        messageshow($"nha data processed for lot : {lot_id_current}");
                    }
                    catch (Exception ex)
                    {
                        connection_data_prcocessing_2.Close();
                        // Handle any errors
                        Console.WriteLine("Error while inserting data into filtered data: " + ex.Message);
                    }
                }
            }
            
            return filtered_data_prcocessing_status;

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
}
