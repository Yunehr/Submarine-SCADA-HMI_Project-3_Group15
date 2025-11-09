using System;
using System.Globalization;
using System.IO;
using System.Linq;

namespace FileHandler
{
    public class FileReader
    {
        public string ReadStringFromNextLine(string fp, int skipNumber)
        {
            string content = File.ReadLines(fp).Skip(skipNumber).FirstOrDefault();
            if (content != null)
            {
                return content;
            }
            throw new FileNotFoundException("The specified line does not exist in the file.");
        }
        public double ReadDoubleFromNextLine(string fp, int skipNumber)   // does it need to be static?
        {
            string content = File.ReadLines(fp).Skip(skipNumber).FirstOrDefault();
            if (double.TryParse(content, out double value))
            {
                return value;
            }
            throw new FormatException("The file does not contain a valid double value.");
        }
    }

    public class FileWriter
    {
        public void AppendStringToFile(string fp, string content)
        {
            //File.AppendAllText(fp, content);
            using (StreamWriter sw = File.AppendText(fp))
            {
                sw.WriteLine(content);
            }
        }

        public void AppendDoubleToFile(string fp, double value)
        {
            using (StreamWriter sw = File.AppendText(fp))
            {
                // I'm leaving some of my attmpts here as comments for future reference
                // string content = value.ToString(CultureInfo.InvariantCulture);  //  Attempt 1: Ensures dot as decimal separator
                // sw.WriteLine(content);
                // sw.WriteLine(value.ToString(CultureInfo.InvariantCulture));  // Atempt 2: Ensures dot as decimal separator 
                sw.WriteLine(value);    // Attempt 3: Default ToString should work in most cases
            }
        }
    }
}