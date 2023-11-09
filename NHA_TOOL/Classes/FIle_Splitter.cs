using CsvHelper.Configuration;
using CsvHelper;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using NHA_TOOL.Classes;

internal class FIle_Splitter
{
    public static char sep_input_file = ' ';
    public static string sep_check = Global_Data.dashboard_selectedItems;
    public static string SplitCSVFile(string sourceFilePath, string outputDirectory, int batchSize)
    {
        string output_1 = "";
        string baseFilePath_original = "Base_header.csv";
        string inputdirectory_input = System.IO.Path.GetDirectoryName(outputDirectory);
        string baseFilePath = inputdirectory_input + "\\" + "Base_header_1.csv";
        
        //string sep_check = Global_Data.dashboard_selectedItems;
        //MessageBox.Show(sep_check);
        //string baseFilePath = "Base_header.csv";
        if (sep_check.Contains('|'))
        { sep_input_file = '|'; }
        else if (sep_check.Contains('~'))
        { sep_input_file = '~'; }
        // Read the CSV file, clean lines, and write back to the same file




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


        using (var reader = new StreamReader(sourceFilePath))

        {

            int fileCount = 1;

            int rowCount = 0;

            string header = reader.ReadLine().Replace(sep_input_file, '|');


            //if (System.IO.File.ReadAllLines((sourceFilePath)).Length > batchSize)
            //{
            while (!reader.EndOfStream)

            {
                string justFileName = Path.GetFileNameWithoutExtension(sourceFilePath);
                string outputFile = System.IO.Path.Combine(outputDirectory, $"{justFileName}_split_{fileCount}.csv");

                using (var writer = new StreamWriter(outputFile))

                {

                    writer.WriteLine(header);




                    for (int i = 0; i < batchSize && !reader.EndOfStream; i++)

                    {

                        writer.WriteLine(reader.ReadLine().Replace(sep_input_file,'|'));

                        rowCount++;

                    }

                }
                if (fileCount == 1)
                {
                    CleanAndWriteCSV(outputFile);
                    UpdateSourceFileHeaders(outputFile);
                    var (sourceColumnHeaders, cleanedData) = GetCleanedData(outputFile);
                    var baseColumnHeaders = GetColumnHeaders(baseFilePath);
                    var incorrectHeaders = GetIncorrectHeaders(baseColumnHeaders, sourceColumnHeaders);
                    if (incorrectHeaders.Any())
                    {

                        //richTextBox1.Text += (string.Join(", ", incorrectHeaders) + "  is incorrect in the source file" + filePath);
                        output_1 += (string.Join(", ", incorrectHeaders) + "  is incorrect in the source file" + sourceFilePath);
                        return output_1;
                    }
                   
                }

                fileCount++;

            }


            //}

            Console.WriteLine($"CSV file split into {fileCount - 1} smaller files with {rowCount} rows total.");

        }
        return output_1;
    }


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


    //        string line_1 = csv.Context.Parser.RawRecord;
    //        int separatorCount_header = CountSeparators(line_1, '|');


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




    //        while (csv.Read())
    //        {
    //            var cleanedRecord = new ExpandoObject();
    //            var expandoDict = (IDictionary<string, object>)cleanedRecord;


    //            string line = csv.Context.Parser.RawRecord;
    //            int separatorCount = CountSeparators(line, '|');

    //            if (separatorCount_header == separatorCount)
    //            {
    //                foreach (var header in headerRecord)
    //                {

    //                    if (!csv.HeaderRecord.Contains(header))
    //                    {
    //                        expandoDict[header] = null;
    //                        continue; // Skip duplicate headers and their corresponding values
    //                    }



    //                    string data_value = csv.GetField(header);

    //                    expandoDict[header] = data_value.replace("\"", "");
    //                    string test = expandoDict[header].ToString();


    //                }
    //            }
    //            else
    //            {
    //                string filePath_error  = System.IO.Path.GetDirectoryName(filePath) + "//" + "Error_"+System.IO.Path.GetFileNameWithoutExtension(filePath)+".txt"; // Change this to your desired output file path

    //                // Create a new StreamWriter and open the file for writing
    //                using (StreamWriter writer = new StreamWriter(filePath_error))
    //                {
    //                    // Write some lines of text
    //                    writer.WriteLine(line);

    //                    // Don't forget to close the writer when you're done
    //                    // The 'using' statement will automatically close it for you
    //                }
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



                foreach (var header in headerRecord)
                {

                    if (!csv.HeaderRecord.Contains(header))
                    {
                        expandoDict[header] = null;
                        continue; // Skip duplicate headers and their corresponding values
                    }


                    try
                    {
                        string data_value = csv.GetField(header);

                        expandoDict[header] = data_value.Replace("\"", "");
                        string test = expandoDict[header].ToString();
                    }

                    catch (Exception ex)
                    {
                        Console.WriteLine("An error occurred: " + ex.Message);
                    }
                }
                cleanedData.Add(cleanedRecord);
            }
            return (headerRecord.ToArray(), cleanedData);
        }
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
    private static string ReplaceCharacters(string input)
    {
        // Replace specific characters here, for example:
        string modified = input.Replace("\"", "");

        return modified;
    }

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
                string line = reader.ReadLine().Replace("\"", "").Replace("'", ""); 

                writer.WriteLine(line);
            }
        }



        // Replace the original file with the cleaned temporary file
        File.Delete(filePath);
        File.Move(tempFilePath, filePath);
    }
}
