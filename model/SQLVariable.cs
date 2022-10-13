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

        public Random random = new Random();
        public SQLVariable(string type, string name)
        {
            this.type = type;
            this.name = name;


            generateValues();
        }


        public void generateValues()
        {
            switch (type)
            {
                case "SMALLMONEY":
                case "INT":
                    this.value = getRandomNumbers(int.MinValue, int.MaxValue);
                    break;
                case "MONEY":
                case "BIGINT":
                    this.value = LongRandom(long.MinValue, long.MaxValue).ToString();
                    break;
                case "SMALLINT":
                    this.value = getRandomNumbers(short.MinValue, short.MaxValue);
                    break;
                case "TINYINT":
                    this.value = getRandomNumbers(0, 255);
                    break;
                case "FLOAT":
                    this.value = GetRandomFloat(double.MinValue, double.MaxValue);
                    break;
                case "REAL":
                    this.value = GetRandomFloat(float.MinValue, float.MaxValue);
                    break;
                case "TEXT":
                case "NTEXT":
                    checkForName(int.MaxValue);
                    break;
            }
        }

        public void checkForName(int maxchars)
        {
            switch (name.ToLower())
            {
                case "phone":
                    this.value = getPhoneNumber();
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
            }
            this.value = "random";
            return;

        }

        public override string ToString()
        {
            return $"{type} {name} {value}";
        }


        public string generateRandomWord()
        {
            return "random";
        }

        //
        //Номер
        //
        public string getPhoneNumber()
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
        //Возраст
        //
        public string getAge()
        {
            string age;
            age = random.Next(4, 85).ToString();
            return age;
        }

        //
        //gmail
        //
        public string getMail()
        {
            int n = random.Next(100000);
            string mail = $"newmail{n}@gmail.com";
            return mail;
        }

        //
        //randomNumbers
        //
        public string getRandomNumbers(int min, int max)
        {
            return random.Next(min, max).ToString();
        }


        public string GetRandomFloat(double minimum, double maximum)
        {
            return (random.NextDouble() * (maximum - minimum) + minimum).ToString();
        }



        //
        //  random long
        //
        public long LongRandom(long min, long max)
        {
            long result = random.Next((Int32)(min >> 32), (Int32)(max >> 32));
            result = (result << 32);
            result = result | (long)random.Next((Int32)min, (Int32)max);
            return result;
        }

        //
        //FIO
        //
        public string getFIO()
        {
            string[] name = { "Alex", "Ivan", "Alica", "Ilya", "Viktoria", "Davud", "Diana", "Eva", "Nastya", "Yaroslav" };
            string[] surname = { "Williams", "Peters", "Gibson", "Martin", "Jordan", "Jackson", "Grant", "Collins", "Bradley", "Barlow" };
            int rName = random.Next(10);
            int rSurname = random.Next(10);
            return $"{name[rName]} {surname[rSurname]}";
        }
        //
        //Name
        //
        public string getName()
        {
            string[] name = { "Alex", "Ivan", "Alica", "Ilya", "Viktoria", "Davud", "Diana", "Eva", "Nastya", "Yaroslav" };
            int rName = random.Next(10);
            return $"{name[rName]}";
           
        }
    }
}
