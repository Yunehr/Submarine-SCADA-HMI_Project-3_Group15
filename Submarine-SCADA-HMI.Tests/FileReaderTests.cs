using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileReader;

namespace Submarine_SCADA_HMI.Tests;

[TestClass]
public class FileReaderTests
{
    // ReadStringFromNextLine Tests
    [TestMethod]
    public void ReadStringFromNextLine_ValidString_ReturnsString()
    {
        // Arrange
        var fr = new FileReader.FileReader();
        string fp = "TestData/TestFileReaderData.txt";

        // Act
        string result = fr.ReadStringFromNextLine(fp, 8); // should read "Hello, World!"

        // Assert
        Assert.AreEqual("Hello, World!", result);
    }

    [TestMethod]
    [ExpectedException(typeof(FileNotFoundException))]
    public void ReadStringFromNextLine_InvalidLine_ThrowsFileNotFoundException()
    {
        // Arrange
        var fr = new FileReader.FileReader();
        string testFilePath = "TestData/TestFileReaderData.txt";

        // Act
        fr.ReadStringFromNextLine(testFilePath, 10); // line 10 does not exist

        // Assert is handled by ExpectedException
    }

    
    // ReadDoubleFromNextLine Tests
    [TestMethod]
    public void ReadDoubleFromNextLine_ValidDouble_ReturnsDouble()
    {
        // Arrange
        var fr = new FileReader.FileReader();
        string fp = "TestData/TestFileReaderData.txt";

        // Act
        double result = fr.ReadDoubleFromNextLine(fp, 0); // should read 5.0

        // Assert
        Assert.AreEqual(5.0, result);
    }

    [TestMethod]
    public void ReadDouble_GetZeroLine_ReturnsZero()
    {
        // Arrange
        var fr = new FileReader.FileReader();
        string fp = "TestData/TestFileReaderData.txt";

        // Act
        double result = fr.ReadDoubleFromNextLine(fp, 1); // should read 0

        // Assert
        Assert.AreEqual(0.0, result);
    }

    [TestMethod]
    public void ReadNegativeDouble_ReturnsNegativeDouble()
    {
        // Arrange
        var fr = new FileReader.FileReader();
        string fp = "TestData/TestFileReaderData.txt";

        // Act
        double result = fr.ReadDoubleFromNextLine(fp, 5); // should read -5

        // Assert
        Assert.AreEqual(-5.0, result);
    }

    [TestMethod]
    public void ReadDoubleGreaterThan100_ReturnsGreaterThan100()
    {
        // Arrange
        var fr = new FileReader.FileReader();
        string fp = "TestData/TestFileReaderData.txt";

        // Act
        double result = fr.ReadDoubleFromNextLine(fp, 7); // should read 120

        // Assert
        Assert.AreEqual(120.0, result);
    }

    [TestMethod]
    [ExpectedException(typeof(FormatException))]
    public void ReadDoubleFromNextLine_InvalidDouble_ThrowsFormatException()
    {
        // Arrange
        var fr = new FileReader.FileReader();
        string testFilePath = "TestData/TestFileReaderData.txt";

        // Act
        fr.ReadDoubleFromNextLine(testFilePath, 8); // line 8 does not exist

        // Assert is handled by ExpectedException
    }
}