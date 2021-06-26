using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using ExecutedModulesJSON.Core.Model;
using Newtonsoft.Json;
using Module = ExecutedModulesJSON.Core.Model.Module;

namespace ExecutedModulesJSON
{
    class Program
    {
        public static Root myDeserializedClass { get;  set; }

        static async Task Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            string path = @"C:\Users\Andrew\Desktop\MyProjects\ExecutedModulesJSON\ExecutedModulesJSON\modules.json";
            Console.WriteLine($"Считуємо файл!",Console.ForegroundColor = ConsoleColor.DarkYellow);
            try
            {
                using (StreamReader sr = new StreamReader(path))
                {
                    myDeserializedClass = JsonConvert.DeserializeObject<Root>(await sr.ReadToEndAsync());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            Console.WriteLine($"Sucsses!\n", Console.ForegroundColor = ConsoleColor.Green);

            var QueueExecuteModules = new Queue<Module>();
            Console.WriteLine($"Створено чергу виконання модулів!", Console.ForegroundColor = ConsoleColor.DarkYellow);

            var QueueExecuteParameters = new Queue<string>();
            Console.WriteLine($"Створено чергу виконання параметрів!", Console.ForegroundColor = ConsoleColor.DarkYellow);


            foreach (var module in myDeserializedClass.modules)
            {
                QueueExecuteModules.Enqueue(module);
                Console.WriteLine($"Додано модулів {QueueExecuteModules.Count}", Console.ForegroundColor = ConsoleColor.Blue);
            }

            while (QueueExecuteModules.Count != 0)
            {
                var dequeveModule = QueueExecuteModules.Dequeue();
                Console.WriteLine($"\nExecuted module: {dequeveModule.name}", Console.ForegroundColor = ConsoleColor.Green);
                PropertyInfo[] properties = dequeveModule.GetType().GetProperties();
                
                foreach (var parametr in properties.Skip(1))
                {
                    QueueExecuteParameters.Enqueue(parametr.Name);
                    Console.WriteLine($"Додано параметрів {QueueExecuteParameters.Count}", Console.ForegroundColor = ConsoleColor.Blue);
                }
                Console.WriteLine();
                while (QueueExecuteParameters.Count != 0)
                {
                    var dequeveParametr = QueueExecuteParameters.Dequeue();
                    Console.WriteLine($"Executed parametr: {dequeveParametr}", Console.ForegroundColor = ConsoleColor.DarkYellow);
                    Console.WriteLine(dequeveParametr+ '\n', Console.ForegroundColor = ConsoleColor.Green);
                }
            }
            Console.ReadLine();

        }
    }
}
