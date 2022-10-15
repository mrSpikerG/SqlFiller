using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFiller.model
{
    public class SQLVariable
    {
        public string type;
        public string name;
        public string value;
        public bool isTextValue;

        public Random random = new Random();
        public SQLVariable(string type, string name)
        {
            this.type = type;
            this.name = name;


            generateValues();
        }


        public void generateValues()
        {
            this.isTextValue = false;
            switch (this.type)
            {
                case "SMALLMONEY":
                case "INT":
                    this.value = getRandomNumbers(int.MinValue, int.MaxValue);
                    return;
                case "MONEY":
                case "BIGINT":
                    this.value = LongRandom(long.MinValue, long.MaxValue).ToString();
                    return;
                case "SMALLINT":
                    this.value = getRandomNumbers(short.MinValue, short.MaxValue);
                    return;
                case "TINYINT":
                    this.value = getRandomNumbers(0, 255);
                    return;
                case "FLOAT":
                    this.value = GetRandomFloat(double.MinValue, double.MaxValue);
                    return;
                case "REAL":
                    this.value = GetRandomFloat(float.MinValue, float.MaxValue);
                    return;
                case "TEXT":
                case "NTEXT":
                    checkForName(int.MaxValue);
                    this.isTextValue = true;
                    return;
                case "BIT":
                    this.value = random.Next(0, 2) == 1 ? "TRUE" : "FALSE";
                    return;
            }
            if (this.type.StartsWith("VARCHAR") || this.type.StartsWith("NVARCHAR"))
            {
                int startindex = type.IndexOf('(') + 1;
                checkForName(int.Parse(type.Substring(startindex, type.LastIndexOf(')') - startindex)));
                this.isTextValue = true;
                return;
            }
        }

        private void checkForName(int maxchars)
        {
            switch (name.ToLower())
            {
                case "phone":
                    this.value = GetPhoneNumber();
                    return;
                case "age":
                    this.value = getAge();
                    return;
                case "email":
                case "mail":
                    this.value = getMail();
                    return;
                case "fio":
                    this.value = getFIO();
                    return;
                case "name":
                    this.value = getName();
                    return;
            }
            if (maxchars == int.MaxValue)
            {
                this.value = GenerateWorld(50);
            }
            else
            {
                this.value = GenerateWorld(maxchars);
            }
            return;

        }


        private string CharsForWord = "abcdefghijklmnopqrstuvwxyz1234567890 ";
        private string GenerateWorld(int maxchars)
        {
            string word = String.Empty;
            for (int i = 0; i < maxchars; i++)
            {
                word += CharsForWord[random.Next(0, CharsForWord.Length)];
            }
            return word;
        }


        public override string ToString()
        {
            return $"{type} {name} {value}";
        }

        //
        //  PhoneNumber
        //
        private string GetPhoneNumber()
        {

            string[] value2 = new string[3];
            string value3;
            value3 = random.Next(100, 1000).ToString();
            for (int i = 0; i < 3; i++)
            {
                value2[i] = random.Next(10, 100).ToString();
            }

            return $"+38(0{value2[0]}) {value3} {value2[1]} {value2[2]}";
        }

        //
        //  Age
        //
        private string getAge()
        {
            string age;
            age = random.Next(4, 85).ToString();
            return age;
        }

        //
        //  gmail
        //
        private string getMail()
        {
            int n = random.Next(100000);
            string mail = $"newmail{n}@gmail.com";
            return mail;
        }

        //
        //  randomNumbers
        //
        private string getRandomNumbers(int min, int max)
        {
            return random.Next(min, max).ToString();
        }


        private string GetRandomFloat(double minimum, double maximum)
        {
            return (random.NextDouble() * (maximum - minimum) + minimum).ToString();
        }



        //
        //  random long
        //
        private long LongRandom(long min, long max)
        {
            long result = random.Next((Int32)(min >> 32), (Int32)(max >> 32));
            result = (result << 32);
            result = result | (long)random.Next((Int32)min, (Int32)max);
            return result;
        }

        //
        //  FIO
        //
        private string getFIO()
        {
            string[] name = { "Alex", "Ivan", "Alica", "Ilya", "Viktoria", "Davud", "Diana", "Eva", "Nastya", "Yaroslav" };
            string[] surname = { "Williams", "Peters", "Gibson", "Martin", "Jordan", "Jackson", "Grant", "Collins", "Bradley", "Barlow" };
            int rName = random.Next(10);
            int rSurname = random.Next(10);
            return $"{name[rName]} {surname[rSurname]}";
        }
        //
        //  Name
        //
        private string getName()
        {
            string[] name = { "Alex", "Ivan", "Alica", "Ilya", "Viktoria", "Davud", "Diana", "Eva", "Nastya", "Yaroslav" };
            int rName = random.Next(10);
            return $"{name[rName]}";

        }
    }
}
