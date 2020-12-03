using System;

namespace GraphConnectionTest
{
    class Program
    {

        private static void AppConfigTest()
        {
            var myList = GetAuth.GetTest();

            foreach (var value in myList)
            {
                Console.WriteLine(value);
            }
        }

        static void Main()
        {
            //AppConfigTest();

            GetAuth.DoAuth();
        }

    }
}
