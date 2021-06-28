using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ExecutedModulesJSON.Model;
using Newtonsoft.Json;
using Module = ExecutedModulesJSON.Model.Module;

namespace ExecutedModulesJSON
{
    class Program
    {
        public static Root myDeserializedClass { get;  set; }

        static async Task Main(string[] args)
        {
            #region Path to file

            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            string workingDirectory = Environment.CurrentDirectory;                                     //  \ExecutedModulesJSON\bin\Debug
            string projectDirectory = Directory.GetParent(workingDirectory).Parent.FullName;            //  \ExecutedModulesJSON
            string pathToFile = projectDirectory + @"\modules.json";                                    //  \ExecutedModulesJSON\modules.json

            #endregion

            #region Read Deserialize JSON                                                               
                                                                                                        // Читаємо файл та десереалізуємо в модель
            Console.WriteLine("Считуємо файл!",
            Console.ForegroundColor = ConsoleColor.DarkYellow);

            try
            {
                using (StreamReader sr = new StreamReader(pathToFile))
                {
                    myDeserializedClass = JsonConvert.DeserializeObject<Root>(await sr.ReadToEndAsync()); //Десереалізація
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Sucsses!\n", 
            Console.ForegroundColor = ConsoleColor.Green);
            #endregion

            #region Module in Queue
            //  Додаємо в чергу виконання модулів

            var QueueExecuteModules = new Queue<Module>();                                              //  Черга із модулів
            Console.WriteLine("Створено чергу виконання модулів!", 
            Console.ForegroundColor = ConsoleColor.DarkYellow);

            foreach (var module in myDeserializedClass.modules)                                         //  Заповнюємо чергу
            {
                QueueExecuteModules.Enqueue(module);
                Console.WriteLine($"Додано модулів {QueueExecuteModules.Count}", 
                Console.ForegroundColor = ConsoleColor.Blue);
            }

            #endregion

            #region Execute Modules
            // Виконуємо по черзі модулі

            while (QueueExecuteModules.Count != 0)                                                      
            {
                Module dequeveModule = QueueExecuteModules.Dequeue();                                   //  Витягли модуль
                PropertyInfo[] properties = dequeveModule.GetType().GetProperties();                    //  Масив властивостей що належать типу Module

                foreach (var parametr in properties)                                          //  Перебираємо властивості типу Module
                {
                    if (parametr.Name == "name")                                                        //  Якщо всластивість ім'я то повідомляємо, що виконується модуль із ім'ям "name"
                    {
                        Console.WriteLine($"\nExecuted module: {parametr.GetValue(dequeveModule)}",     //  Беремо значення імені із властивостей об'єкта витягнутого модуля 
                        Console.ForegroundColor = ConsoleColor.Green);
                    }
                    else                                                                                // Інакше це параметр то повідомляємо, що виконується параметр із ім'ям "parametr[X]"
                    {
                        Console.WriteLine($"Executed parametr: {parametr.Name}",                        //  Беремо ім'я поля із типу модуля
                        Console.ForegroundColor = ConsoleColor.DarkYellow);
                        Console.WriteLine($"{parametr.GetValue(dequeveModule)}",                        //  Беремо значення параметрів із властивостей об'єкта витягнутого модуля
                        Console.ForegroundColor = ConsoleColor.Green);
                    }
                }
            }
            Console.ReadLine();
            #endregion

        }
    }
}
