using QRCoder;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using NHA_TOOL.Classes;

namespace NHA_TOOL
{
    internal class image_conversion
    {

        public static string images_extention = Global_Data.image_file_ext;
        //public static int image_generator(string FilePAth_image, string FilePAth_qr, string TableNAme , int lotid)
        //{
        //    string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;
        //    int count = 0;
        //    int pass = 0;
        //    try
        //    {

        //        using (var connection_image = new SqlConnection(connectionString))

        //        {

        //            connection_image.Open();

        //            SqlCommand cmd = new SqlCommand("select pmrssm_id,doc_pic,health_id,pmrssm_id,'' as hid,name_ben,gender_ben,state_codelgd_ben,district_codelgd_ben,dob_ben,state_name_english,district_name_english,mobile_member,address_ben from " + TableNAme + " where pmrssm_id <>'' and [Lot_ID] = '"+lotid+"' ", connection_image);
        //            SqlDataReader reader = cmd.ExecuteReader();

        //            if (reader.HasRows)
        //            {
        //                while (reader.Read())
        //                {
        //                    //ProId = reader.GetInt32(0);
        //                    Console.WriteLine(reader.GetString(0));
        //                    pass = 0;
        //                    count += 1;
        //                    try
        //                    {

        //                        System.Drawing.Image image;
        //                        byte[] imageBytes = Convert.FromBase64String(reader.GetString(1));
        //                        using (MemoryStream ms = new MemoryStream(imageBytes, 0, imageBytes.Length))
        //                        {
        //                            ms.Write(imageBytes, 0, imageBytes.Length);
        //                            //return Image.FromStream(ms, true);
        //                            image = System.Drawing.Image.FromStream(ms, true);
        //                            image.Save(FilePAth_image + reader.GetString(0) + ".jpg", System.Drawing.Imaging.ImageFormat.jpg);
        //                            //if (count % 200 == 0) { messageshow($"{count} images generated"); }

        //                        }
        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Console.WriteLine("Error :" + ex.Message);
        //                        //cnn_1 = new SqlConnection(connectionString);
        //                        using (var connection_error_update = new SqlConnection(connectionString))

        //                        {

        //                            connection_error_update.Open();
        //                            string updateQuery = "update '" + TableNAme + "' set FLAG = FLAG + R_Image_Not_valid where pmrssm_id = '" + reader.GetString(0) + "' ";

        //                            // Create a SqlCommand with the UPDATE query and SqlConnection
        //                            using (SqlCommand command = new SqlCommand(updateQuery, connection_error_update))
        //                                //SqlCommand cmd_1 = new SqlCommand("update " + TableNAme + " set FLAG = 'R_Image_Not_valid where pmrssm_id = "+ reader.GetString(0) + "'", cnn_1);

        //                                command.ExecuteNonQuery();
        //                            connection_error_update.Close();
        //                        }
        //                    }


        //                    //qr_generation logic
        //                    try
        //                    {


        //                        string inputValue = reader.GetString(2) + "\n" + reader.GetString(3) + "\n" + reader.GetString(4) + "\n" + reader.GetString(5) + "\n" + reader.GetString(6) + "\n" + reader.GetString(7) + "\n" + reader.GetString(8) + "\n" + reader.GetString(9) + "\n" + reader.GetString(10) + "\n" + reader.GetString(11) + "\n" + reader.GetString(12) + "\n" + reader.GetString(13);


        //                        // Generate QR code
        //                        QRCodeGenerator qrGenerator = new QRCodeGenerator();
        //                        QRCodeData qrCodeData = qrGenerator.CreateQrCode(inputValue, QRCodeGenerator.ECCLevel.Q);
        //                        QRCode qrCode = new QRCode(qrCodeData);

        //                        // Customize color settings
        //                        Color foregroundColor = Color.Black; // Set the foreground color (e.g., black)
        //                        Color backgroundColor = Color.White; // Set the background color (e.g., white)

        //                        Bitmap qrCodeImage = qrCode.GetGraphic(10, foregroundColor, backgroundColor, null, 15, 6, false);

        //                        // Save the QR code image to a file
        //                        qrCodeImage.Save(FilePAth_qr + reader.GetString(3) + ".jpg", ImageFormat.jpg);


        //                        if (count % 200 == 0) { messageshow($"{count} qr generated"); }

        //                    }
        //                    catch (Exception ex)
        //                    {
        //                        Console.WriteLine("Error _ qr_generation: " + ex.Message);

        //                    }



        //                }
        //            }

        //            //Console.Write("Sql Connected");



        //            connection_image.Close();
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine("Error :" + ex.Message);
        //        //cnn_1 = new SqlConnection(connectionString);

        //    }
        //    //Console.ReadKey();
        //    return pass;

        //}

        //public static void messageshow_1(string message)
        //{
        //     //System.Windows.Forms.RichTextBox richTextBox1 = new System.Windows.Forms.RichTextBox();
        //   // richTextBox1.AppendText($"{message}");
        //    //richTextBox1.Text += $"{message}";
        //    //richTextBox1.Text += "\r\n";
        //    //richTextBox1.SelectionStart = richTextBox1.TextLength;
        //    //richTextBox1.ScrollToCaret();
        //    //richTextBox1.Refresh();

        //}


        //private static  void messageshow(string text)
        //{
        //    //System.Windows.Forms.RichTextBox richTextBox1 = new System.Windows.Forms.RichTextBox();
        //    //richTextBox1.AppendText($"{text}");
        //    //if (richTextBox1.InvokeRequired)
        //    //{
        //    //    richTextBox1.Invoke((MethodInvoker)(() =>
        //    //    {
        //    //        richTextBox1.AppendText(text);
        //    //    }));
        //    //}
        //    //else
        //    //{
        //    //    richTextBox1.AppendText(text);
        //    //}
        //}


        public static void ConvertImagesFromBase64ToJpg(List<string> base64Images, string FilePAth_image)

        {

            // Create a directory to save the converted JPG images

            //Directory.CreateDirectory($"{FilePAth_image}/ConvertedImages");


            Task.WhenAll(base64Images.Select((base64Image, index) =>

               Task.Run(() =>


               {
                   string[] values = base64Image.Split(',');
                   values = values.Select(s => s.Replace("\"", "")).ToArray();
                   string image_data = values[1].ToString();
                   string pmjay_id = values[0].ToString();

                   byte[] imageBytes = Convert.FromBase64String(image_data);

                   string imagePath = $"{FilePAth_image}/{pmjay_id}"+ images_extention;




                   using (MemoryStream ms = new MemoryStream(imageBytes))
                   {

                       using (FileStream fs = new FileStream(imagePath, FileMode.Create))

                       {

                           // Save the image as a JPG file

                           System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

                           image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);

                           image.Dispose();// clearing the ImageStream memory
                                           //ms.Position = 0;
                                           //ms.SetLength(0);// clearing the MemoryStream memory
                                           //fs.SetLength(0);// clearing the FileStream memory
                       }
                       //Array.Clear(imageBytes, 0, imageBytes.Length);
                       //Array.Clear(values, 0, values.Length);

                       //if (index % 100 == 0) { messageshow($"{index} images generated successfully"); }

                   }
                   //System.Windows.Forms.RichTextBox richTextBox1 = new System.Windows.Forms.RichTextBox();

                   //richTextBox1.Text += $"Images Generated";
                   //richTextBox1.Text += "\r\n";
                   //richTextBox1.SelectionStart = richTextBox1.TextLength;
                   //richTextBox1.ScrollToCaret();
                   //richTextBox1.Refresh();
               })));

            // Use parallel processing to convert images

            //Task.WhenAll(base64Images.Select((base64Image, index) =>

            //   Task.Run(() =>


            //   {
            //       string[] values = base64Image.Split(',');
            //       string image_data = values[1].ToString();
            //       string pmjay_id = values[0].ToString();

            //       byte[] imageBytes = Convert.FromBase64String(image_data);

            //       string imagePath = $"{FilePAth_image}/ConvertedImages/{pmjay_id}.jpg";




            //       using (MemoryStream ms = new MemoryStream(imageBytes))
            //       {

            //           using (FileStream fs = new FileStream(imagePath, FileMode.Create))

            //           {

            //               // Save the image as a JPG file

            //               System.Drawing.Image image = System.Drawing.Image.FromStream(ms);

            //               image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);

            //               image.Dispose();// clearing the ImageStream memory
            //                               //ms.Position = 0;
            //                               //ms.SetLength(0);// clearing the MemoryStream memory
            //                               //fs.SetLength(0);// clearing the FileStream memory
            //           }
            //           //Array.Clear(imageBytes, 0, imageBytes.Length);
            //           //Array.Clear(values, 0, values.Length);

            //           //if (index % 100 == 0) { messageshow($"{index} images generated successfully"); }

            //       }
            //       //System.Windows.Forms.RichTextBox richTextBox1 = new System.Windows.Forms.RichTextBox();

            //       //richTextBox1.Text += $"Images Generated";
            //       //richTextBox1.Text += "\r\n";
            //       //richTextBox1.SelectionStart = richTextBox1.TextLength;
            //       //richTextBox1.ScrollToCaret();
            //       //richTextBox1.Refresh();
            //   })));

        }

       

        //public static int image_generator_thread(string FilePAth_image, string FilePAth_qr, string TableNAme, int lotid)
        //{
        //    int pass = 0,count = 0;
            
        //    // Create a List<string> to store the column values
        //    List<string> dataList = new List<string>();
        //    List<string> qrList = new List<string>();
        //    //List<string> dataList = new List<string> { "First", "Second" };
        //    string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

        //    // Create a SqlConnection and specify the connection string
        //    using (SqlConnection connection = new SqlConnection(connectionString))
        //    {
        //        // Open the connection
        //        connection.Open();

        //        // Create a SqlCommand to retrieve the data from the database
        //        using (SqlCommand command = new SqlCommand("select  pmrssm_id+','+doc_pic as image_data from " + TableNAme + " where pmrssm_id <>'' and [Lot_ID] = '"+lotid+"' ", connection))
        //        {
        //            //command.CommandTimeout = 1800; //setting command timeout for 30 minss
        //            // Create a SqlDataReader to read the data
        //            using (SqlDataReader reader = command.ExecuteReader())
        //            {
        //                // Check if there are any rows
        //                if (reader.HasRows)
        //                {
        //                    // Iterate through the rows
        //                    while (reader.Read())
        //                    {
        //                        count += 1;
        //                        // Get the value from the desired column (e.g., ColumnName)
        //                        string image_value = reader.GetString(reader.GetOrdinal("image_data"));
        //                        //string image_value = reader.GetString(0);
        //                        // Add the value to the List<string>
        //                        dataList.Add(image_value);
        //                        if (count % 5000 == 0)
        //                        {
        //                            try
        //                            {
        //                                ConvertImagesFromBase64ToJpg(dataList, FilePAth_image);
        //                                dataList.Clear();
        //                               // messageshow("15000 images generated ");
        //                            }
        //                            catch (Exception ex)
        //                            {
        //                                Console.WriteLine("Error _ qr_generation: " + ex.Message);

        //                            }
        //                        }
                                
        //                        //qrList.Add(value);
        //                        //if (count % 5000 == 0)
        //                        //{
        //                        //    try
        //                        //    {
        //                        //        ConvertImagesFromBase64ToJpg(qrList, FilePAth_image);
        //                        //        qrList.Clear();
        //                        //        // messageshow("15000 images generated ");
        //                        //    }
        //                        //    catch (Exception ex)
        //                        //    {
        //                        //        Console.WriteLine("Error _ qr_generation: " + ex.Message);

        //                        //    }
        //                        //}
        //                    }
        //                    if (dataList.Count > 0)
        //                    {
        //                        try
        //                        {
        //                            ConvertImagesFromBase64ToJpg(dataList, FilePAth_image);
        //                            dataList.Clear();
        //                            //messageshow(dataList.Count.ToString()+" images batch generated ");
        //                            System.Windows.Forms.RichTextBox richTextBox1 = new System.Windows.Forms.RichTextBox();

        //                            richTextBox1.Text += $"Images Generated";
        //                            richTextBox1.Text += "\r\n";
        //                            richTextBox1.SelectionStart = richTextBox1.TextLength;
        //                            richTextBox1.ScrollToCaret();
        //                            richTextBox1.Refresh();

        //                        }
        //                        catch (Exception ex)
        //                        {
        //                            Console.WriteLine("Error _ qr_generation: " + ex.Message);
        //                            messageshow("");
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        // Close the connection
        //        connection.Close();
        //    }

            
        //    //// Display the values in the List<string>
        //    //foreach (string value in dataList)
        //    //{
        //    //    Console.WriteLine(value);
        //    //}

        //    //Console.ReadLine();
        //    return pass;
        //}



        public static int image_generator_thread_new(string FilePAth_image, string FilePAth_qr, string TableNAme, int lotid)
        {
            int pass = 0, count = 0;

            // Create a List<string> to store the column values
            List<string> imageList = new List<string>();
            List<string> qrList = new List<string>();

            //int total_data = Int32.Parse(Database.sql_data_value("SELECT count(*) as data_count FROM " + TableNAme + " where Lot_ID =  '" + lotid + "' ", "data_count"));

            //List<string> dataList = new List<string> { "First", "Second" };
            string connectionString = ConfigurationManager.ConnectionStrings["dbcon"].ConnectionString;

            

            // Create a SqlConnection and specify the connection string
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                // Open the connection
                connection.Open();

                // Create a SqlCommand to retrieve the data from the database
                using (SqlCommand command = new SqlCommand("select pmrssm_id+','+doc_pic as image_data,health_id+','+pmrssm_id+','+ ''  +','+name_ben+','+gender_ben+','+state_codelgd_ben+','+district_codelgd_ben+','+dob_ben+','+state_name_english+','+district_name_english+','+mobile_member+','+replace(address_ben,',','-') as qr_data   from " + TableNAme + " where pmrssm_id <>'' and [Lot_ID] = '" + lotid + "' ", connection))
                
                //chasigarh
                //using (SqlCommand command = new SqlCommand("select pmrssm_id+','+doc_pic as image_data,district_name_english+','+block_name_english+','+ village_name_english +','+replace(address_ben,',','-')+','+gender_ben+','+dob_ben+','+name_ben+','+mobile_member+','+health_id+','+pmrssm_id+','+state_name_english as qr_data   from " + TableNAme + " where pmrssm_id <>'' and [Lot_ID] = '" + lotid + "' ", connection))
                {
                    //command.CommandTimeout = 1800; //setting command timeout for 30 minss
                    // Create a SqlDataReader to read the data
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        // Check if there are any rows
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                count += 1;
                                // Get the value from the desired column (e.g., ColumnName)
                                string image_value = reader.GetString(reader.GetOrdinal("image_data"));
                                string qr_value = reader.GetString(reader.GetOrdinal("qr_data"));
                                //string image_value = reader.GetString(0);
                                // Add the value to the List<string>
                                imageList.Add(image_value);
                                qrList.Add(qr_value);


                                if (count % 5000 == 0)
                                {
                                    try
                                    {
                                        ConvertImagesFromBase64ToJpg(imageList, FilePAth_image);
                                        imageList.Clear();
                                       // qrconversion(qrList, FilePAth_qr);
                                        //qrList.Clear();
                                        // messageshow("15000 images generated ");

                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error while the image generation: " + ex.Message);

                                    }
                                    try
                                    {
                                        qrconversion(qrList, FilePAth_qr);
                                        qrList.Clear();
                                        //messageshow("15000 images generated ");
                                    }
                                    catch (Exception ex)
                                    {
                                        Console.WriteLine("Error while qr_generation: " + ex.Message);

                                    }
                                }


                                
                            }
                            if (imageList.Count > 0)
                            {
                                try
                                {
                                    ConvertImagesFromBase64ToJpg(imageList, FilePAth_image);
                                    imageList.Clear();
                                    //qrconversion(qrList, FilePAth_qr);
                                    //qrList.Clear();
                                    //messageshow(dataList.Count.ToString()+" images batch generated ");
                                    //System.Windows.Forms.RichTextBox richTextBox1 = new System.Windows.Forms.RichTextBox();

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error _ qr_generation: " + ex.Message);
                                    //messageshow("");
                                }

                                //for qr generation
                                try
                                {
                                    qrconversion(qrList, FilePAth_qr);
                                    qrList.Clear();
                                    //messageshow(dataList.Count.ToString()+" images batch generated ");
                                    //System.Windows.Forms.RichTextBox richTextBox1 = new System.Windows.Forms.RichTextBox();

                                }
                                catch (Exception ex)
                                {
                                    Console.WriteLine("Error _ qr_generation: " + ex.Message);
                                   // messageshow("");
                                }
                            }
                        }
                    }
                }
                // Close the connection
                connection.Close();
            }


            //// Display the values in the List<string>
            //foreach (string value in dataList)
            //{
            //    Console.WriteLine(value);
            //}

            //Console.ReadLine();
            return pass;
        }



        public static void qrconversion(List<string> qrdata, string FilePAth_qr_1)

        {

            

            // Generate QR codes in parallel
            Parallel.ForEach(qrdata, qrdatamerge =>
            {

                string[] values = qrdatamerge.Split(',');
                values = values.Select(s => s.Replace("\"", "")).ToArray();
                //string image_data = values[1].ToString();
                //string pmjay_id = values[0].ToString();

                //qr_generation logic
                try
                {
                    string pmjay_id = values[1].ToString().Trim();
                    string qr_inputValue = values[0].ToString() + "\n" + values[1].ToString() + "\n" + values[2].ToString() + "\n" +
                    values[3].ToString() + "\n" + values[4].ToString() + "\n" + values[5].ToString() + "\n" + values[6].ToString() +
                    "\n" + values[7].ToString() + "\n" + values[8].ToString() + "\n" + values[9].ToString() + "\n" + values[10].ToString()+ 
                    "\n" + values[11].ToString();

                    //chastissgarh data
                    //string pmjay_id = values[9].ToString().Trim();
                    //string qr_inputValue = values[0].ToString() + "\n" + values[1].ToString() + "\n" + values[2].ToString() + "\n" +
                    //values[3].ToString() + "\n" + values[4].ToString() + "\n" + values[5].ToString() + "\n" + values[6].ToString() +
                    //"\n" + values[7].ToString() + "\n" + values[8].ToString() + "\n" + values[9].ToString() + "\n" + values[10].ToString() ;
                    // Create the QR code data
                    QRCodeGenerator qrGenerator = new QRCodeGenerator();
                    //QRCodeData qrCodeData = qrGenerator.CreateQrCode(qr_inputValue, QRCodeGenerator.ECCLevel.Q);
                    //changing ECCLevel.Q to ECCLevel.L for faster processing // 18-07-23
                    QRCodeData qrCodeData = qrGenerator.CreateQrCode(qr_inputValue, QRCodeGenerator.ECCLevel.L);

                    // Create the QR code
                    QRCode qrCode = new QRCode(qrCodeData);

                    // Render the QR code as an image
                    //Bitmap qrCodeImage = qrCode.GetGraphic(20);

                    //changing GetGraphic to (5) for faster processing // 18-07-23
                    Bitmap qrCodeImage = qrCode.GetGraphic(5);


                    


                    // Save the QR code image to a file
                    string fileName = $"{pmjay_id}"+ images_extention;
                    string filePath = Path.Combine(FilePAth_qr_1, fileName);
                    qrCodeImage.Save(filePath);
                    qrCodeImage.Dispose();

                    StartPrinting(string.Format(@"""{0}""", filePath), string.Format(@"""{0}""", filePath));
                    

                    Console.WriteLine($"Generated QR code for {pmjay_id} saved as {FilePAth_qr_1}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error _ qr_generation: " + ex.Message);

                }
            });



            
        }
    



        public static void StartPrinting(string sourceImage, string destinationImage)
        {
            try
            {

                string ghostScriptPath = @"convert.exe";
                String ars = "convert  " + sourceImage + "  -colorspace Gray   " + destinationImage + "";
                System.Diagnostics.Process proc = new System.Diagnostics.Process();
                proc.StartInfo.FileName = ghostScriptPath;
                proc.StartInfo.Arguments = ars;
                proc.StartInfo.CreateNoWindow = true;
                proc.StartInfo.UseShellExecute = false;
                proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                proc.Start();
                proc.WaitForExit();


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

        }
    }
   
    
}
