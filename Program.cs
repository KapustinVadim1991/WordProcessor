using System;
using System.Linq;

namespace WordProcessor
{
    class Program
    {
        private static string firstMessage = "Команды приложения:\nсоздание словаря [путь к файлу]\nобновление словаря [путь к файлу]\nочистка словаря\n";
        private static string createStr = "создание словаря";
        private static string updateStr = "обновление словаря";
        private static string clearStr = "очистка словаря";

        static void Main(string[] args)
        {
            // Указываем текущую директорию для работы с базой данных
            AppDomain.CurrentDomain.SetData("DataDirectory", Environment.CurrentDirectory);
            Console.WriteLine(firstMessage);
            
            while (true)
            {
                string input = ConsoleExtension.CancelableReadLine().ToLower();
                if (input == "" || input.All(c => char.IsWhiteSpace(c))) break;

                if(input.StartsWith(createStr))
                {
                    RequestsToDatabase.CreateDictionary(GetPath(input, createStr));
                }
                else if (input.StartsWith(updateStr))
                {
                    RequestsToDatabase.UpdateDictionary(GetPath(input, updateStr));
                }
                else if (input.StartsWith(clearStr))
                {
                    RequestsToDatabase.ClearDictionary();
                }
                else
                {
                    RequestsToDatabase.FindTopFive(input);
                }
            }            
        }
        
        /// <summary>
        /// Извлечение пути к файлу из строки вводимой пользователем
        /// </summary>
        /// <param name="input">Строка вводимая пользователем</param>
        /// <param name="command">Команда, с которой начинается строка</param>
        private static string GetPath(string input, string command)
        {
            return input.Substring(command.Length).Trim(new char[] { ' ', '\"'});
        }
    }
}
