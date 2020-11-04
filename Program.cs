using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WordProcessor.DataModel;
using System.Configuration;

namespace WordProcessor
{
    class Program
    {
        static string firstMessage = "Команды приложения:\nсоздание словаря [путь к файлу]\nобновление словаря [путь к файлу]\nочистка словаря\n";
        static string createStr = "создание словаря";
        static string updateStr = "обновление словаря";
        static string clearStr = "очистка словаря";

        static void Main(string[] args)
        {
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
        
        private static string GetPath(string input, string command)
        {
            return input.Substring(command.Length).Trim(new char[] { ' ', '\"'});
        }
    }
}
