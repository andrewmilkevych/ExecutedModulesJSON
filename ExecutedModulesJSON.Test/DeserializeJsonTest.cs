using System;
using System.Linq;
using ExecutedModulesJSON.Core;
using ExecutedModulesJSON.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ExecutedModulesJSON.Test
{
    [TestClass]
    public class DeserializeJsonTest
    {
        static string _path =
            @"C:\Users\Andrew\Desktop\MyProjects\ExecutedModulesJSON\ExecutedModulesJSON\modules.json";

        private Root listModules;

        [TestInitialize]
        public  void Setup()
        {
            listModules = new DeserializeJson(_path).ReadAsync<Root>().Result;         // Створюємо десереалізовану модель
        }

        [TestMethod]
        public void DeserializeJson_CanDeserializeRootModel_ReturnTrue()
        {
            Assert.AreEqual(listModules.GetType(), new Root().GetType());       // Чи відповідає тип десереалізації моделі Root
        }
        [TestMethod]
        public void DeserializeJson_CanDeserializeModuleModel_ReturnTrue()
        {
            Assert.AreEqual(listModules.Modules.FirstOrDefault().GetType(), new Module().GetType());       // Чи відповідає тип десереалізації моделі Model
        }
    }
}
