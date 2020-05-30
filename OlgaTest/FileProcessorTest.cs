using System;
using Olga.BLL.BusinessModels;
using Olga.DAL.Entities;
using Olga.Util;
using Xunit;

namespace OlgaTest
{
    public class FileProcessorTest
    {
        [Fact]
        public void GetAdditionalFileFolder_ReturnsFolder_WhenCorrect()
        {
            //Arrange
            FileProcessor processor = new FileProcessor();
            var expectedfolder = "D:\\folder\\Ean";
            ProductDocument doc = new ProductDocument()
            {
                IsEan = true
            };
            //ACT
            var res = processor.GetAdditionalFileFolder(@"D:\folder\",doc);
            //ASSERT
            Assert.Equal(expectedfolder, res);
        }

        [Fact]
        public void GetAdditionalFileFolder()
        {
            //Arrange
            FileProcessor processor = new FileProcessor();
            var expectedFilePath = "D:\\folder\\Ean\\testname";
            var serverPath = "D:\\folder\\";
            string fileName = "testname.txt";
            //ACT
            var res = processor.GetAdditionalFileUniquePath(serverPath, ProductAdditionalDocsType.Ean, fileName);
            //ASSERT
            Assert.Contains(expectedFilePath, res);
        }
    }
}
