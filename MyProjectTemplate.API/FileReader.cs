using System;
using System.IO;
using System.Linq;

namespace FileReader
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
}