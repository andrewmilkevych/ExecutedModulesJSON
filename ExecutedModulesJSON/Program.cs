using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ExecutedModulesJSON.Core;
using ExecutedModulesJSON.Core.Model;
using Module = ExecutedModulesJSON.Core.Model.Module;


namespace ExecutedModulesJSON
{
    class Program
    {
        static async Task Main(string[] args)
        {

            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding  = System.Text.Encoding.Unicode;
            
            Root listModules = new DeserializeJson(                         // Десереалізуємо текст
                new FileLoader("modules.json")._pathToFile).         // Загружаємо файл
                ReadAsync<Root>().                                          // Читаємо
                Result;                                                     // Повертаємо результат таска
            
            if (listModules!=null)
            {
                Console.WriteLine($"Sucsses!\n", Console.ForegroundColor = ConsoleColor.Green);
            }
            
            new PassParameters(ref listModules).PassParameter();            // Редагування параметрів у listModules перед виконанням (НЕ У ФАЙЛІ!!! module.json) 

            
            #region Module in Queue
            //  Додаємо в чергу виконання модулів

            var QueueExecuteModules = new Queue<Module>();                                              //  Черга із модулів
            Console.WriteLine("Створено чергу виконання модулів!", 
            Console.ForegroundColor = ConsoleColor.DarkYellow);

            foreach (var module in listModules.Modules)                                                 //  Заповнюємо чергу
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
