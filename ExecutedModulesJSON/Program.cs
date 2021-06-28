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

            #region Pass parameters to these objects before they are executed?

            bool PassParameters = true;                                                                 // Для повторного вводу параметрів
            while (PassParameters)
            {
                Console.WriteLine("Передавати параметри цим об'єктам до їх виконання? Y/N?");
                ConsoleKeyInfo key = Console.ReadKey();
                Console.WriteLine();

                if (key.Key == ConsoleKey.N)
                {
                    Console.WriteLine("Start");
                    PassParameters = false;
                }
                else if (key.Key == ConsoleKey.Y)
                {
                    Console.WriteLine("Виберіть модуль для редагування: ");
                    
                    for (int j = 1, i = 0; i < myDeserializedClass.modules.Count; i++, j++)             // Виводимо список модулів
                    {
                        Console.WriteLine(j + ". " + myDeserializedClass.modules[i].name);
                    }

                    key = Console.ReadKey();
                    Console.WriteLine();

                    var NumberModule = Convert.ToInt32(key.KeyChar.ToString()) - 1;                 // Номер модуля що буде редагуватися
                    
                    Type type = typeof(Module);

                    Console.WriteLine("Виберіть властивіть для редагування: ");

                    for (int j = 1, i = 0; i < type.GetProperties().Length; i++, j++)                   // Виводимо список властивостей модуля
                    {
                        Console.Write(j + ". " + type.GetProperties()[i].Name + ' ');
                        Console.WriteLine($"{type.GetProperties()[i].GetValue(myDeserializedClass.modules[NumberModule])}");
                    }

                    key = Console.ReadKey();
                    Console.WriteLine();

                    var NumberProp = Convert.ToInt32(key.KeyChar.ToString()) - 1;                   // Номер властивості що буде редагуватися

                    Console.Write("Редагування на : ");

                    type.GetProperties()[NumberProp].SetValue(
                        
                        myDeserializedClass.modules[NumberModule],                                  // Встановлюємо нове значення властивості
                        Console.ReadLine()); 
                    
                }
                else
                {
                    Console.WriteLine("Натиснута інша кнопка!");
                }
            }

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
