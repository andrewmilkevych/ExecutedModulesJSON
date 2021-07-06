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
            FileLoader fileLoader = new FileLoader("modules.json");     // Створюємо екземпляр FileLoader

            Assert.IsTrue(fileLoader._pathToFile != null);                     // Перевіряємо чи об'єкт типу FileLoader має посилання на файл
        }
    }
}
