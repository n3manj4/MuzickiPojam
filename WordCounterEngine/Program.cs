using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace WordCounterEngine
{
    class Program
    {
        static void Main(string[] args)
        {
            string inFileName = @"C:\Users\NMiladin\Downloads\Aca Lukas.txt";
            StreamReader sr = new StreamReader(inFileName);
            string text = System.IO.File.ReadAllText(inFileName);
            Regex reg_exp = new Regex("[^a-zA-Z0-9]");
            text = reg_exp.Replace(text, " ");
            string[] words = text.Split(new char[] {
                ' '
            }, StringSplitOptions.RemoveEmptyEntries);
            var word_query = (from string word in words orderby word select word).Distinct();
            string[] result = word_query.ToArray();
            int counter = 0;
            string delim = " ,.";
            string[] fields = null;
            string line = null;
            while (!sr.EndOfStream)
            {
                line = sr.ReadLine(); //each time you read a line you should split it into the words  
                line.Trim();
                fields = line.Split(delim.ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                counter += fields.Length; //and just add how many of them there is  
                foreach (string word in result)
                {
                    CountStringOccurrences(text, word);
                }
            }
            sr.Close();
            Console.WriteLine("The total word count is {0}", counter);
            Console.ReadLine();
        }
        //Count the frequency of each unique word.  
        public static void CountStringOccurrences(string text, string word)
        {
            int count = 0;
            int i = 0;
            while ((i = text.IndexOf(word, i)) != -1)
            {
                i += word.Length;
                count++;
            }
            Console.WriteLine("{0} {1}", count, word);
        }
    }
}

