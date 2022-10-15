using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using SQLFiller.model;

namespace SQLFiller
{
    class Program
    {

        public static string PATH = "C:\\Users\\thespiker\\Documents\\sqlfiller";
        public static List<SQLVariable> SQLVariables = new List<SQLVariable>();

        public static Random random = new Random();
        public static string TableName = "TableName";

        static void Main(string[] args)
        {
            if (!Directory.Exists(PATH))
            {
                Directory.CreateDirectory(PATH);
            }
            string[] files = Directory.GetFiles(PATH);


            foreach (var i in files)
            {
                if (i.EndsWith(".sql"))
                {
                    try
                    {
                        // bigint numeric bit smallint decimal smallmoney int tinyint money

                        string text = File.ReadAllText(i);

                        int startindex = text.IndexOf('(') + 1;
                        TableName = text.Substring(text.IndexOf("TABLE ")+6 ,startindex- text.IndexOf("TABLE ")-7);
                        

                        text = text.Substring(startindex, text.LastIndexOf(')') - startindex);

                        string[] variables = text.Split(',');
                        for (int j = 0; j < variables.Length; j++)
                        {

                            string[] TEMP = variables[j].Split(' ');


                            if (j == variables.Length - 1)
                            {
                                TEMP[1] = TEMP[1].Substring(0, TEMP[1].Length - 2);
                            }

                            SQLVariables.Add(new SQLVariable(TEMP[1].ToUpper(), TEMP[0].Substring(3, TEMP[0].Length - 4)));
                        }
                    }
                    catch (FileNotFoundException e)
                    {
                        Console.WriteLine(e.Message);
                        continue;
                    }
                }
            }

            string sqlstring = $"INSERT INTO {TableName}(";
            for (int i = 0; i < SQLVariables.Count; i++)
            {
                sqlstring += $"[{SQLVariables[i].name}]";
                if (i != SQLVariables.Count - 1)
                {
                    sqlstring += ", ";
                }

            }
            sqlstring += ") VALUES";

            int count = 10;
            for (int j = 0; j < count; j++)
            {
                sqlstring += "\n(";
                for (int i = 0; i < SQLVariables.Count; i++)
                {

                    if (SQLVariables[i].isTextValue)
                    {
                        sqlstring += $"'{SQLVariables[i].value}'";
                    }
                    else
                    {
                        sqlstring += $"{SQLVariables[i].value}";
                    }

                    if (i != SQLVariables.Count - 1)
                    {
                        sqlstring += ", ";
                    }
                }
                sqlstring += ")";
                if (j != count - 1)
                {
                    sqlstring += ",";
                }
                else
                {
                    sqlstring += ";";
                }

                regenerateList();

            }
            File.WriteAllText($"{PATH}\\sqlinsert.sql", sqlstring);
            Console.WriteLine(sqlstring);
        }

        public static void regenerateList()
        {
            for (int i = 0; i < SQLVariables.Count; i++)
            {
                SQLVariables[i].generateValues();
            }
        }

    }
}
