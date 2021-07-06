using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ExecutedModulesJSON.Core;
using ExecutedModulesJSON.Core.Model;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Module = ExecutedModulesJSON.Core.Model.Module;

namespace ExecutedModulesJSON.Test
{
    [TestClass]
    public class PassParametersTest
    {
        private PassParameters passParameters;
        private Root listModules;
        private FieldInfo[] fieldInfos;

        [TestInitialize]
        public void Sutup()
        {
            listModules = new Root() { /*Modules = new List<Module>()*/};                                                         // Об'єкт моделі Root
            passParameters = new PassParameters(ref listModules);                             // Створюємо об'єкт PassParameters з посиланням на об'єкт моделі Root
            fieldInfos = passParameters.GetType().GetFields(BindingFlags.NonPublic); // Використовуємо рефлексію щоб взяти приватні поля класу
        }

        [TestMethod]
        public void PassParameter_CanLoadReferenceRootObject_ReturnTrue()
        {
            foreach (var field in fieldInfos)                                   // Перебираємо список приватних полів
            {
                if (field.Name == "_listModules")                               // Знаходимо поле _listModules (список модулів) у класі passParameters
                {
                    Assert.AreSame(listModules, field.GetValue(passParameters));           // Інкапсулюємо з об'єкта passParameters посилання на список _listModules (поле у класі PassParameters)

                                                                                                        // Та перевіряємо чи це об'єкт listModules, що був переданий у конструктор PassParameters
                }
            }
        }

    }
}
