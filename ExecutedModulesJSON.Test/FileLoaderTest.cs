using System;
using System.IO;
using ExecutedModulesJSON.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ExecutedModulesJSON.Test
{
    [TestClass]
    public class FileLoaderTest
    {
       [TestMethod]
        public void FileLoader_GetWorkDirectory_ReturnTrue()
        {
            FileLoader fileLoader = new FileLoader("modules.json");

            Assert.IsTrue(fileLoader._pathToFile != null);
        }
    }
}
