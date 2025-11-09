using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileHandler;

namespace Submarine_SCADA_HMI.Tests;

[TestClass]
public class FileWriterTests
{
    // WriteStringToFile Tests
    [TestMethod]
    public void AppendStringToFile_ValidString()
    {
        // Arrange
        var fw = new FileWriter();
        string fp = "TestData/TestAppendStringToFileData.txt";
        string content = "Test String";

        if (File.Exists(fp)) File.Delete(fp);

        // Act
        try
        {
            fw.AppendStringToFile(fp, content);
            string result = File.ReadAllText(fp);
            result = result.Trim(); // Remove any trailing newline characters

            // Assert
            Assert.AreEqual(content, result);
        }
        finally
        {
            if (File.Exists(fp)) File.Delete(fp);
        }
    }

    // WriteDoubleToFile Tests
    [TestMethod]
    public void AppendDoubleToFile_ValidDouble_WritesDouble()
    {
        // Arrange
        var fw = new FileWriter();
        string fp = "TestData/TestAppendDoubleToFileData.txt";
        double content = 42.0;
        string expected = "42";

        if (File.Exists(fp)) File.Delete(fp);

        // Act
        try
        {
            fw.AppendDoubleToFile(fp, content);
            var lastLine = File.ReadLines(fp).Last();

        // Assert
            //Assert.AreEqual(content.ToString(), result);
            Assert.AreEqual(expected, lastLine);
        }

        finally
        {
            if (File.Exists(fp)) File.Delete(fp);
        }
        
    }
}