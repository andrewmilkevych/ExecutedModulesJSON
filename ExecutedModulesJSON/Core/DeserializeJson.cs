using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace ExecutedModulesJSON.Core
{
    public class DeserializeJson
    {
        private string _pathToFile { get; set; }
        
        // Читаємо файл та десереалізуємо в модель
        public DeserializeJson(string pathToFile)
        {
            this._pathToFile = pathToFile;
        }

        public async Task<T> ReadAsync<T>()
        {
            Console.WriteLine($"Считуємо файл!", Console.ForegroundColor = ConsoleColor.DarkYellow);

            try
            {
                using (StreamReader sr = new StreamReader(_pathToFile))
                {
                    return JsonConvert.DeserializeObject<T>(await sr.ReadToEndAsync()); //Десереалізація
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default(T);
            }
            
        }

    }
}
