using System;
namespace PPProject
{
    public class Common 
    {
        public Common()
        {
        }

        public decimal ParseDecimal(string str)
        {
            decimal result;
            decimal.TryParse(str, out result);

            return result;
        }

        public int ParseInteger(string str)
        {
            int result;

            int.TryParse(str, out result);

            return result;
        }

        public bool ParseBool(string str)
        {
            bool result;

            Boolean.TryParse(str, out result);

            return result;
        }

        public decimal ReadDecimal()
        {
            decimal result;

            decimal.TryParse(Console.ReadLine(), out result);

            return result;
        }

        public int ReadInteger()
        {
            int result;

            int.TryParse(Console.ReadLine(), out result);

            return result;
        }
    }
}
