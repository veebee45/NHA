using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfPage = PdfSharp.Pdf.PdfPage;
using DateTime = System.DateTime;
using DocumentFormat.OpenXml.Drawing;
using NHA_TOOL.Classes;

namespace NHA_TOOL.Forms
{
    public partial class NHA_24UPS : Form
    {
        public static string folderPath = "";
        public static int lotid = 0;
        public static string artwork = "";
        public NHA_24UPS(string path,int lot,string state)
        {
            InitializeComponent();
            folderPath = path;  
            lotid=lot;
            artwork = state.Replace(' ','_').ToUpper();
        }
        
        public static void log(string id, string message)
        {
            using (FileStream file = new FileStream(folderPath+"log.txt", FileMode.Append, FileAccess.Write))
            {
                using (StreamWriter wr = new StreamWriter(file))
                {
                    wr.WriteLine($"At {DateTime.Now} Data is Processed with PMJID={id} in which {message}");
                }

            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            try
            {
                string jobBag = textBox1.Text;
                
                if (jobBag == "")
                {
                    MessageBox.Show("Please Enter The Job Bag Data For Further Processing");
                }
                else
                {
                    string constr = "Data Source=192.168.5.22; Database=TestDB;User Id=CopalU;Password=Csplcolor#43205";
                    DataTable dataTable = new DataTable();
                    using (SqlConnection con = new SqlConnection(constr))
                    {
                        con.Open();
                        using (SqlCommand command = new SqlCommand("usp_nha_process_presspress", con))
                        {
                            command.CommandType = CommandType.StoredProcedure;
                            command.Parameters.AddWithValue("@lotid", lotid);
                            command.Parameters.AddWithValue("@tablename", "Filtered_data");
                            command.Parameters.AddWithValue("@statecode", "up01");
                            command.Parameters.AddWithValue("@imgext", Global_Data.image_file_ext);

                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                adapter.Fill(dataTable);
                            }
                        }
                        
                    }
                    int totalsize = dataTable.Rows.Count;
                    //int totalsize =24;

                    MessageBox.Show($"{totalsize} data Fetched sucessfully.");
                    Stopwatch sw = Stopwatch.StartNew();
                    sw.Start();
                    int batchsize = totalsize / 24;
                    if (totalsize % 24 != 0)
                        batchsize++;

                    List<int> numbers = Enumerable.Range(0, (batchsize / 300) + 1).ToList(); //batchsize
                    Parallel.ForEach(numbers, new ParallelOptions { MaxDegreeOfParallelism = 4 }, number =>
                    {
                        int count = 0;
                        Console.WriteLine($"Processing Batch {number + 1}");
                        if (number == batchsize / 300)
                        {
                            count = batchsize % 300;
                        }
                        else
                        {
                            count = 300;
                        }

                        using (PdfSharp.Pdf.PdfDocument outputPdf = new PdfSharp.Pdf.PdfDocument())
                        {

                            for (int j = number * 300; j < (number * 300 + count); j++)
                            {
                                outputPdf.AddPage();

                            }

                            int rowIndex1 = number * 300 * 24;
                            var background = XImage.FromFile("NHA\\Template.pdf");
                            foreach (PdfSharp.Pdf.PdfPage page in outputPdf.Pages)
                            {
                                page.Width = XUnit.FromMillimeter(295);
                                page.Height = XUnit.FromMillimeter(483);
                                double x = 42.944882;
                                double y = 38.324409;
                                XGraphics graphics = XGraphics.FromPdfPage(page);
                                graphics.DrawImage(background, 0, 22.75);//conversion is in points(mm to points)
                                for (int i = 1; i <= 24; i++)
                                {
                                    var front = XImage.FromFile($"NHA\\Front_{artwork}.pdf");
                                    string sep = null;
                                    if (rowIndex1 < dataTable.Rows.Count)
                                    {
                                        sep = Convert.ToString(dataTable.Rows[rowIndex1][11]);
                                        rowIndex1++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Empty card processed");
                                    }
                                    if (sep == "PDF\\Separator.pdf")
                                    {
                                        var sep_img = XImage.FromFile("NHA\\Separator.pdf");
                                        graphics.DrawImage(sep_img, x, y);
                                        sep_img.Dispose();
                                        if (i % 8 == 0)
                                        {
                                            y = 38.324409;
                                            x += front.PointWidth + 7.08661;
                                        }
                                        else
                                        {
                                            y += front.PointHeight;
                                        }
                                    }
                                    else
                                    {
                                        graphics.DrawImage(front, x, y);
                                        if (i % 8 == 0)
                                        {
                                            y = 38.324409;
                                            x += front.PointWidth + 7.08661;
                                        }
                                        else
                                        {
                                            y += front.PointHeight;
                                        }
                                    }
                                    front.Dispose();
                                }
                                graphics.Dispose();
                            }
                            int rowIndex = number * 300 * 24;
                            foreach (PdfPage page in outputPdf.Pages)
                            {
                                double x = 42.944882;
                                double y = 38.324409;
                                XGraphics graphics = XGraphics.FromPdfPage(page);
                                PdfSharp.Drawing.XBrush b = XBrushes.Black;
                                PdfSharp.Drawing.XFont fontTemplate = new PdfSharp.Drawing.XFont("Arial", 7, XFontStyle.Bold);
                                string innerOuterBox = "Inner Box-" + Convert.ToString(dataTable.Rows[rowIndex][14]) + "/Outer Box-" + Convert.ToString(dataTable.Rows[rowIndex][15]);
                                string sheetNoOf = Convert.ToString(dataTable.Rows[rowIndex][12]);
                                string pdfPageno = Convert.ToString(dataTable.Rows[rowIndex][20]);
                                string sheetNo = sheetNoOf.Split('-')[0].Replace('*', ' ');
                                string batch = Convert.ToString(dataTable.Rows[rowIndex][16]);

                                graphics.DrawString(innerOuterBox, fontTemplate, XBrushes.Black, 86.0394, 33.3);
                                graphics.DrawString(pdfPageno, fontTemplate, XBrushes.Black, 654.134, 33.3);
                                graphics.DrawString(sheetNo, fontTemplate, XBrushes.Black, 723.58268, 33.3);
                                graphics.DrawString(sheetNoOf, fontTemplate, XBrushes.Black, 86.0394, 1343);
                                graphics.DrawString(batch, fontTemplate, XBrushes.Black, 167.244, 1343);
                                graphics.DrawString(jobBag, new XFont("Arial", 6, XFontStyle.Regular), XBrushes.Black, 603.78, 1343);

                                for (int i = 1; i <= 24; i++)
                                {
                                    var front = XImage.FromFile($"NHA\\Front_{artwork}.pdf");
                                    string name = "";
                                    string pmjid = "";
                                    string abhano = "";
                                    string dob = "";
                                    string gender = "";
                                    string ward = "";
                                    string town = "";
                                    string imgpath = "";
                                    string qrpath = "";
                                    string district = "";
                                    string sep = null;
                                    if (rowIndex < dataTable.Rows.Count)
                                    {
                                        name = Convert.ToString(dataTable.Rows[rowIndex][1]);
                                        pmjid = Convert.ToString(dataTable.Rows[rowIndex][2]);
                                        dob = Convert.ToString(dataTable.Rows[rowIndex][3]);
                                        gender = Convert.ToString(dataTable.Rows[rowIndex][4]);
                                        town = Convert.ToString(dataTable.Rows[rowIndex][5]);
                                        ward = Convert.ToString(dataTable.Rows[rowIndex][6]);
                                        district = Convert.ToString(dataTable.Rows[rowIndex][7]);
                                        imgpath = folderPath+Convert.ToString(dataTable.Rows[rowIndex][8]);
                                        qrpath = folderPath + Convert.ToString(dataTable.Rows[rowIndex][9]);
                                        abhano = Convert.ToString(dataTable.Rows[rowIndex][10]);
                                        sep = Convert.ToString(dataTable.Rows[rowIndex][11]);
                                        rowIndex++;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Empty card processed");
                                    }
                                    if (sep == "PDF\\Separator.pdf")
                                    {
                                        if (i % 8 == 0)
                                        {
                                            y = 38.324409;
                                            x += front.PointWidth + 7.08661;
                                        }
                                        else
                                        {
                                            y += front.PointHeight;
                                        }
                                    }
                                    else if (!File.Exists(imgpath) || !File.Exists(qrpath))
                                    {
                                        Console.WriteLine($"No image and qr data Data Found for {pmjid}. ");
                                        log(pmjid, "Image or Qr Misssing");
                                        if (i % 8 == 0)
                                        {
                                            y = 38.324409;
                                            x += front.PointWidth + 7.08661;
                                        }
                                        else
                                        {
                                            y += front.PointHeight;
                                        }
                                    }
                                    else
                                    {
                                        var img = XImage.FromFile(imgpath);
                                        var qr = XImage.FromFile(qrpath);
                                        XBrush brush = XBrushes.Black;
                                        XFont Fontname = new XFont("Hind Bold", 10, XFontStyle.Bold);
                                        XFont printFont = new XFont("Gotham Narrow Bold", 5, XFontStyle.Bold);

                                        graphics.DrawString(name, Fontname, brush, x + 59, y + 72.3741732);
                                        graphics.DrawString(pmjid, printFont, brush, x + 195.043465, y + 126);
                                        graphics.DrawString(abhano, printFont, brush, x + 54.4790551, y + 126.5);
                                        graphics.DrawString(dob, printFont, brush, x + 95, y + 85.3766929);
                                        graphics.DrawString(gender, printFont, brush, x + 174.682205, y + 85.8699213);
                                        graphics.DrawString(ward, printFont, brush, x + 133.71024, y + 93.2286614);
                                        graphics.DrawString(town, printFont, brush, x + 133.71024, y + 101.5);
                                        graphics.DrawString(district, printFont, brush, x + 133.71024, y + 110.5);
                                        graphics.DrawImage(img, x + 11.794331, y + 53.9748, 41.5445669, 51.5);
                                        graphics.DrawRoundedRectangle(new XPen(XColor.FromArgb(255, 165, 0), 0.5f), new XRect(x + 10.794331, y + 53.5748, 43.6, 52.214173), new XSize(5, 5));
                                        graphics.DrawImage(qr, x + 204, y + 64, 34.0157, 34.0157);

                                        img.Dispose();
                                        qr.Dispose();

                                        if (i % 8 == 0)
                                        {
                                            y = 38.324409;
                                            x += front.PointWidth + 7.08661;
                                        }
                                        else
                                        {
                                            y += front.PointHeight;
                                        }

                                    }
                                    front.Dispose();

                                }
                                graphics.Dispose();
                            }

                            MemoryStream stream = new MemoryStream();
                            outputPdf.Save($"Final_Output\\NHA_Batch_{number + 1}.pdf");
                        }
                        Console.WriteLine($"Processed Batch {number + 1}");
                    });
                    sw.Stop();
                    Console.WriteLine($"Execution Time: {sw.ElapsedMilliseconds} ms");
                    long time = sw.ElapsedMilliseconds / 1000;
                    MessageBox.Show($"Process completed and Execution Time: {time} sec : {time / 60} mins");
                    Process.Start("Final_Output");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        
    }
}
