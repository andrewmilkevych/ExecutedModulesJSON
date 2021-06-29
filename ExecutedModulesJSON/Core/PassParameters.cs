using System;
using ExecutedModulesJSON.Core.Model;

namespace ExecutedModulesJSON.Core
{
    class PassParameters
    {
        private bool _PassParameters = true; // Для повторного вводу параметрів
        private ConsoleKeyInfo _key;
        private Root _listModules;
        public PassParameters(ref Root listModules)
        {
            this._listModules = listModules;
        }

        public void PassParameter()
        {
            try
            {
                while (this._PassParameters)
                {
                    Console.WriteLine($"Передавати параметри цим об'єктам до їх виконання? Y/N?",
                        Console.ForegroundColor = ConsoleColor.DarkYellow);

                    _key = Console.ReadKey();
                    Console.WriteLine();

                    if (_key.Key == ConsoleKey.N)
                    {
                        Console.WriteLine($"\nStart\n", Console.ForegroundColor = ConsoleColor.Green);
                        _PassParameters = false;
                    }
                    else if (_key.Key == ConsoleKey.Y)
                    {
                        Console.WriteLine($"\nВиберіть модуль для редагування: ",
                            Console.ForegroundColor = ConsoleColor.DarkYellow);
                        Console.WriteLine("---------------------");

                        for (int j = 1, i = 0; i < _listModules.Modules.Count; i++, j++) // Виводимо список модулів
                        {
                            Console.WriteLine(j + ". " + _listModules.Modules[i].name);
                        }
                        Console.WriteLine();
                        _key = Console.ReadKey();
                        Console.WriteLine();

                        var NumberModule =
                            Convert.ToInt32(_key.KeyChar.ToString()) - 1; // Номер модуля що буде редагуватися

                        Type type = typeof(Module);

                        Console.WriteLine("\nВиберіть властивіть для редагування: \n");

                        for (int j = 1, i = 0;
                            i < type.GetProperties().Length;
                            i++, j++) // Виводимо список властивостей модуля
                        {
                            Console.Write(j + ". " + type.GetProperties()[i].Name + ' ');
                            Console.WriteLine(
                                $"{type.GetProperties()[i].GetValue(_listModules.Modules[NumberModule])}");
                        }

                        Console.WriteLine();
                        _key = Console.ReadKey();
                        Console.WriteLine();

                        var NumberProp =
                            Convert.ToInt32(_key.KeyChar.ToString()) - 1; // Номер властивості що буде редагуватися

                        Console.Write("\nРедагування на : ");

                        type.GetProperties()[NumberProp].SetValue(

                            _listModules.Modules[NumberModule], // Встановлюємо нове значення властивості
                            Console.ReadLine());

                    }
                    else
                    {
                        Console.WriteLine($"Натиснута інша кнопка!", Console.ForegroundColor = ConsoleColor.Red);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}
