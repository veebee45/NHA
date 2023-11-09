using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.Office.Interop.Excel;
using NHA_TOOL.Classes;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using Font = iTextSharp.text.Font;
using Paragraph = iTextSharp.text.Paragraph;
using Rectangle = iTextSharp.text.Rectangle;

namespace NHA_TOOL
{
    class Generate_files
    {
        static string csv_filename = "";
        public static void Generate_prepress_files(int lot_id_current, string datatable_name, string District, string strFilePath , string statecode_prepress )
        {

            // Replace the connection string with your own
            string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
            DataSet ds = new DataSet();

            // Create a SqlConnection object
            using (SqlConnection connection_preress = new SqlConnection(connectionString))
            {
                // Open the connection
                connection_preress.Open();
                // Create a SqlCommand object
                using (SqlCommand cmd = new SqlCommand($"usp_nha_process_presspress", connection_preress))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@lotid", lot_id_current);
                    cmd.Parameters.AddWithValue("@tablename", datatable_name);
                    cmd.Parameters.AddWithValue("@statecode", statecode_prepress);
                    cmd.Parameters.AddWithValue("@imgext", Global_Data.image_file_ext);

                    // Create a SqlDataReader object
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(ds);
                    }
                    messageshow("Data generated successfully for prepress");
                }
                connection_preress.Close();
            }

            ToCSV(lot_id_current, ds.Tables[0], District, "For_Prepress", strFilePath);
            //MessageBox.Show($"{District}_Prepress File Created Successfully!");

            string csvFilePath = $"{csv_filename}.csv";     // Path of the CSV file
            string excelFilePath = $"{csv_filename}.xlsx"; // Path of the output Excel file

            ConvertCsvToExcel(csvFilePath, excelFilePath);
            File.Delete(csvFilePath);
            messageshow("Excel generated successfully for packing list");
        }


        public static void Generate_mis_files(int lot_id_current, string datatable_name, string District, string strFilePath,string statecode_mis)
        {


            string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
            DataSet ds = new DataSet();

            // Create a SqlConnection object
            using (SqlConnection connection_mis = new SqlConnection(connectionString))
            {
                // Open the connection
                connection_mis.Open();
                // Create a SqlCommand object
                using (SqlCommand cmd = new SqlCommand($"usp_nha_mis", connection_mis))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@lotid", lot_id_current);
                    cmd.Parameters.AddWithValue("@tablename", datatable_name);
                    cmd.Parameters.AddWithValue("@statecode", statecode_mis);

                    // Create a SqlDataReader object
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(ds);
                    }
                    messageshow("Data generated successfully for mis");
                }
                connection_mis.Close();
            }

            ToCSV(lot_id_current, ds.Tables[0], District, "For_MIS", strFilePath);


            string csvFilePath = $"{csv_filename}.csv";     // Path of the CSV file
            string excelFilePath = $"{csv_filename}.xlsx"; // Path of the output Excel file

            ConvertCsvToExcel(csvFilePath, excelFilePath);
            File.Delete(csvFilePath);
            messageshow("Excel generated successfully for mis");
            //MessageBox.Show("Kargil_MIS Excel File Created Successfully!");
        }

        public static void Generate_packing_list_files(int lot_id_current, string datatable_name, string District, string strFilePath)
        {


            string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
            DataSet ds = new DataSet();

            // Create a SqlConnection object
            using (SqlConnection connection_packing_list = new SqlConnection(connectionString))
            {
                // Open the connection
                connection_packing_list.Open();
                // Create a SqlCommand object
                using (SqlCommand cmd = new SqlCommand($"usp_nha_packing_list_file", connection_packing_list))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@lotid", lot_id_current);
                    cmd.Parameters.AddWithValue("@tablename", datatable_name);

                    // Create a SqlDataReader object
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(ds);
                    }
                    messageshow("data generated successfully for packing list");

                }
                connection_packing_list.Close();
            }

            ToCSV(lot_id_current, ds.Tables[0], District, "For_packing_list", strFilePath);

            //ExportExcel(ds.Tables[0],"output_1.xlsx");
            //MessageBox.Show($"{District}_Packing_List_File csv Created Successfully!");

            string csvFilePath = $"{csv_filename}.csv";     // Path of the CSV file
            string excelFilePath = $"{csv_filename}.xlsx"; // Path of the output Excel file
            string pdfFilePath = $"{csv_filename}.pdf";

            //pdf_page_break
            ConvertCsvToPdf(csvFilePath, pdfFilePath);

            ConvertCsvToExcel(csvFilePath, excelFilePath);
            File.Delete(csvFilePath);
            messageshow("Excel generated successfully for packing list");
            //MessageBox.Show($"{District}_Packing_List_File xlsx Created Successfully!");
        }

        public static void Generate_packing_list_files_pagebreak(int lot_id_current, string datatable_name, string District, string strFilePath)
        {


            string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
            DataSet ds = new DataSet();

            // Create a SqlConnection object
            using (SqlConnection connection_packing_list = new SqlConnection(connectionString))
            {
                // Open the connection
                connection_packing_list.Open();
                // Create a SqlCommand object
                using (SqlCommand cmd = new SqlCommand($"usp_nha_packing_list_file", connection_packing_list))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@lotid", lot_id_current);
                    cmd.Parameters.AddWithValue("@tablename", datatable_name);

                    // Create a SqlDataReader object
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(ds);
                    }
                    messageshow("data generated successfully for packing list");

                }
                connection_packing_list.Close();
            }

            ToCSV(lot_id_current, ds.Tables[0], District, "For_packing_list", strFilePath);


            GeneratePdf(ds.Tables[0], strFilePath);

            //ExportExcel(ds.Tables[0],"output_1.xlsx");
            //MessageBox.Show($"{District}_Packing_List_File csv Created Successfully!");

            string csvFilePath = $"{csv_filename}.csv";     // Path of the CSV file
            string excelFilePath = $"{csv_filename}.xlsx"; // Path of the output Excel file

            ConvertCsvToExcel(csvFilePath, excelFilePath);
            File.Delete(csvFilePath);
            messageshow("Excel generated successfully for packing list");
            //MessageBox.Show($"{District}_Packing_List_File xlsx Created Successfully!");
        }
        public static void Generate_label_files(int lot_id_current, string datatable_name, string District, string strFilePath)
        {


            string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
            DataSet ds = new DataSet();

            // Create a SqlConnection object
            using (SqlConnection connection_packing_list = new SqlConnection(connectionString))
            {
                // Open the connection
                connection_packing_list.Open();
                // Create a SqlCommand object
                using (SqlCommand cmd = new SqlCommand($"usp_nha_label", connection_packing_list))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@lotno", "");
                    cmd.Parameters.AddWithValue("@refno", "");
                    cmd.Parameters.AddWithValue("@date", "");
                    cmd.Parameters.AddWithValue("@type", "");
                    cmd.Parameters.AddWithValue("@file", "");
                    cmd.Parameters.AddWithValue("@tablename", datatable_name);
                    cmd.Parameters.AddWithValue("@lotid", lot_id_current);


                    // Create a SqlDataReader object
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(ds);
                    }
                    messageshow("data generated successfully for label");
                }
                connection_packing_list.Close();
            }

            ToCSV(lot_id_current, ds.Tables[0], District, "For_Lavel", strFilePath);

            //ExportExcel(ds.Tables[0],"output_1.xlsx");
            //MessageBox.Show($"{District}_Packing_List_File csv Created Successfully!");

            string csvFilePath = $"{csv_filename}.csv";     // Path of the CSV file
            string excelFilePath = $"{csv_filename}.xlsx"; // Path of the output Excel file

            ConvertCsvToExcel(csvFilePath, excelFilePath);
            File.Delete(csvFilePath);
            messageshow("Excel generated successfully for label");
            //MessageBox.Show($"{District}_Packing_List_File xlsx Created Successfully!");
        }


        public static void Generate_inner_label_files(int lot_id_current, string datatable_name, string District, string strFilePath)
        {

            int pass = 0;
            string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
            DataSet ds = new DataSet();



            using (SqlConnection connection_innerlabel_2 = new SqlConnection(connectionString))
            {
                // Open the connection
                connection_innerlabel_2.Open();
                // Create a SqlCommand object
                using (SqlCommand cmd = new SqlCommand($"usp_nha_label_1", connection_innerlabel_2))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Add parameters to the command


                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@lotno", "");
                    cmd.Parameters.AddWithValue("@refno", "");
                    cmd.Parameters.AddWithValue("@date", "");
                    cmd.Parameters.AddWithValue("@type", "");
                    cmd.Parameters.AddWithValue("@file", "");
                    cmd.Parameters.AddWithValue("@tablename", datatable_name);
                    cmd.Parameters.AddWithValue("@Lot_id", lot_id_current);


                    // Create a SqlDataReader object
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(ds);
                    }
                    messageshow("data generated successfully for label");
                }
                connection_innerlabel_2.Close();
            }
            ToCSV(lot_id_current, ds.Tables[0], District, "For_Inner_Label", strFilePath);

            //ExportExcel(ds.Tables[0],"output_1.xlsx");
            //MessageBox.Show($"{District}_Packing_List_File csv Created Successfully!");

            string csvFilePath = $"{csv_filename}.csv";     // Path of the CSV file
            string excelFilePath = $"{csv_filename}.xlsx"; // Path of the output Excel file

            ConvertCsvToExcel(csvFilePath, excelFilePath);
            File.Delete(csvFilePath);
            messageshow("Excel generated successfully for inner label");



            //MessageBox.Show($"{District}_Packing_List_File xlsx Created Successfully!");
        }


        public static void Generate_outter_label_files(int lot_id_current, string datatable_name, string District, string strFilePath)
        {


            string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
            DataSet ds = new DataSet();

            // Create a SqlConnection object
            using (SqlConnection connection_outer_label = new SqlConnection(connectionString))
            {
                // Open the connection
                connection_outer_label.Open();
                // Create a SqlCommand object
                using (SqlCommand cmd = new SqlCommand($"usp_nha_label_2", connection_outer_label))
                {
                    cmd.CommandTimeout = 1200;
                    cmd.CommandType = CommandType.StoredProcedure;
                    // Add parameters to the command
                    cmd.Parameters.AddWithValue("@Lot_id", lot_id_current);


                    // Create a SqlDataReader object
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(ds);
                    }
                    messageshow("data generated successfully for label");
                }
                connection_outer_label.Close();
            }

            ToCSV(lot_id_current, ds.Tables[0], District, "For_Outter_Label", strFilePath);

            //ExportExcel(ds.Tables[0],"output_1.xlsx");
            //MessageBox.Show($"{District}_Packing_List_File csv Created Successfully!");

            string csvFilePath = $"{csv_filename}.csv";     // Path of the CSV file
            string excelFilePath = $"{csv_filename}.xlsx"; // Path of the output Excel file

            ConvertCsvToExcel(csvFilePath, excelFilePath);
            File.Delete(csvFilePath);
            messageshow("Excel generated successfully for outer label");
            //MessageBox.Show($"{District}_Packing_List_File xlsx Created Successfully!");
        }
        private static void ToCSV(int lotNum, System.Data.DataTable dtDataTable, string District, string file_type, string strFilePath)
        {
            try
            {
                //csv_filename = strFilePath + lotNum + "_NHA_" + District + "_" + file_type + "_" + DateTime.Now.ToString("dd.MM.yyyy") + "--" + dtDataTable.Rows.Count ;
                csv_filename = strFilePath + lotNum + "_NHA_" + District + "_" + file_type + "_" + DateTime.Now.ToString("dd.MM.yyyy") + "--" + Form4.total_data_global;

                StreamWriter sw = new StreamWriter(csv_filename + ".csv", true, Encoding.UTF8);
                //headers    
                for (int i = 0; i < dtDataTable.Columns.Count; i++)
                {
                    sw.Write(dtDataTable.Columns[i]);
                    if (i < dtDataTable.Columns.Count - 1)
                    {
                        sw.Write("|");
                    }
                }
                sw.Write(sw.NewLine);
                //sw.WriteLine(string.Join(";", output).Replace(System.Environment.NewLine, "  "));


                foreach (DataRow dr in dtDataTable.Rows)
                {

                    for (int i = 0; i < dtDataTable.Columns.Count; i++)
                    {


                        if (!Convert.IsDBNull(dr[i]))
                        {
                            string value = dr[i].ToString();
                            value = value.Replace(System.Environment.NewLine, "  ");

                            if (value.Contains('|'))
                            {
                                value = String.Format("{0}", value);

                                sw.Write(value);
                            }
                            else
                            {
                                sw.Write(value);
                            }
                        }
                        if (i < dtDataTable.Columns.Count - 1)
                        {
                            sw.Write("|");
                        }
                    }
                    sw.Write(sw.NewLine);
                }
                sw.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }


        }






        public static void ConvertCsvToExcel(string csvFilePath, string excelFilePath)
        {
            // Read the CSV file
            string[] csvLines = File.ReadAllLines(csvFilePath);
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            // Create a new Excel package
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add("Sheet1");

                // Write the CSV data to the Excel worksheet
                for (int row = 0; row < csvLines.Length; row++)
                {
                    string[] csvValues = csvLines[row].Split('|');

                    for (int col = 0; col < csvValues.Length; col++)
                    {
                        worksheet.Cells[row + 1, col + 1].Value = csvValues[col];
                    }
                }

                // Save the Excel package to a file
                FileInfo excelFile = new FileInfo(excelFilePath);
                excelPackage.SaveAs(excelFile);
            }

            Console.WriteLine("CSV to Excel conversion complete.");
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


        public static void GeneratePdf(System.Data.DataTable dataTable, string filePath)
        {
            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, new System.IO.FileStream(filePath, FileMode.Create));



            doc.Open();



            string previousVillageName = string.Empty;



            foreach (DataRow row in dataTable.Rows)
            {
                string currentVillageName = row["village_name_english"].ToString();



                if (currentVillageName != previousVillageName)
                {
                    // Add a page break if the village name has changed
                    if (!string.IsNullOrEmpty(previousVillageName))
                    {
                        doc.NewPage();
                    }



                    // You can add extra formatting for the new village name here
                    Paragraph villageNameParagraph = new iTextSharp.text.Paragraph(currentVillageName, new Font(Font.FontFamily.HELVETICA, 14, Font.BOLD));

                    //Paragraph villageNameParagraph = new Paragraph(currentVillageName, new iTextSharp.text.Font(iTextSharp.text.Font.FontFamily.HELVETICA, 14, Font.BOLD));
                    doc.Add(villageNameParagraph);



                    // Update the previous village name for the next iteration
                    previousVillageName = currentVillageName;
                }



                // Add other data from the row to the PDF
                string SrNo = row["Sr. No."].ToString();
                string pmrssm_id = row["pmrssm_id"].ToString();
                string name_ben = row["name_ben"].ToString();
                string Address_Ben = row["Address_Ben"].ToString();
                string mobile_member = row["mobile_member"].ToString();
                string block_name_english = row["block_name_english"].ToString();
                string village_name_english = row["village_name_english"].ToString();
                string district_name_english = row["district_name_english"].ToString();







                // Create a formatted paragraph with the data
                string data = $"SrNo: 1|pmrssm_id: {pmrssm_id}|name_ben :{name_ben}|village_name_english:{village_name_english}\n";
                Paragraph dataParagraph = new Paragraph(data, new Font(Font.FontFamily.HELVETICA, 12));
                doc.Add(dataParagraph);
            }



            doc.Close();
            writer.Close();
        }

        public static void ConvertCsvToPdf(string csvFilePath, string pdfFilePath)
        {
            try
            {
                // Read the CSV data using DataTable
                System.Data.DataTable dt = new System.Data.DataTable();
                using (StreamReader sr = new StreamReader(csvFilePath))
                {
                    string header = sr.ReadLine();
                    string[] headers = header.Split('|');
                    foreach (string h in headers)
                    {
                        dt.Columns.Add(h);
                    }

                    while (!sr.EndOfStream)
                    {
                        string[] rows = sr.ReadLine().Split('|');
                        dt.Rows.Add(rows);
                    }
                }

                // Create a new document and set the page size

                Rectangle pageSize = PageSize.A4;
                float marginLeft = 0f; // Set your desired left margin in points
                float marginRight = 0f; // Set your desired right margin in points
                float marginTop = 30f; // Set your desired top margin in points
                float marginBottom = 30f;

                iTextSharp.text.Document doc = new Document(pageSize, marginLeft, marginRight, marginTop, marginBottom);


                // Create a PdfWriter instance to write the PDF file
                using (FileStream fs = new FileStream(pdfFilePath, FileMode.Create))
                {
                    PdfWriter.GetInstance(doc, fs);

                    // Open the document for writing
                    doc.Open();


                    AddHeaderRow(doc, dt.Columns);


                    string previousVillageName = string.Empty;



                    foreach (DataRow row in dt.Rows)
                    {
                        // Get the value of the village name column
                        string villageName = row["village_name_english"].ToString();

                        // If the village name changes, insert a page break
                        if (villageName != previousVillageName && !string.IsNullOrEmpty(previousVillageName))
                        {
                            doc.NewPage();

                            AddHeaderRow(doc, dt.Columns);
                        }




                        // Create a PdfPTable with the same number of columns as the CSV data
                        PdfPTable table = new PdfPTable(dt.Columns.Count);

                        // Add data rows to the table
                        foreach (var item in row.ItemArray)
                        {
                            Font customFont = new Font(Font.FontFamily.TIMES_ROMAN, 8f); // Change 8f to your desired font size
                            PdfPCell cell = new PdfPCell(new Phrase(item.ToString(), customFont));
                            table.AddCell(cell);
                            table.DefaultCell.HorizontalAlignment = Element.ALIGN_LEFT;
                        }


                        // Add the header row at the start of each page

                        // Add the table to the document


                        // Add content to the document using the customFont
                        Paragraph paragraph = new Paragraph();
                        paragraph.Add(table);
                        doc.Add(paragraph);




                        // Update the previous village name
                        previousVillageName = villageName;
                    }

                    // Close the document
                    doc.Close();
                }

                Console.WriteLine("CSV file converted to PDF successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public static void AddHeaderRow(Document doc, DataColumnCollection columns)
        {
            // Create a PdfPTable for the header row
            PdfPTable headerTable = new PdfPTable(columns.Count);

            // Add the header row data
            foreach (DataColumn column in columns)
            {
                Font customFont = new Font(Font.FontFamily.TIMES_ROMAN, 8f); // Change 8f to your desired font size
                PdfPCell cell = new PdfPCell(new Phrase(column.ToString(), customFont));
                headerTable.AddCell(cell);
            }

            // Set the style for the header row (optional)
            headerTable.DefaultCell.BackgroundColor = BaseColor.LIGHT_GRAY;
            headerTable.DefaultCell.HorizontalAlignment = Element.ALIGN_CENTER;
            headerTable.DefaultCell.Padding = 5;

            // Add the header table to the document
            doc.Add(headerTable);
        }



    }
}
