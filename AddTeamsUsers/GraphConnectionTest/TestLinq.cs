using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphConnectionTest
{
    class TestLinq
    {

        public static void confronta()
        {
            var myTestDate = "2020-11-30T13:30:00.0000000"; // 1
            //var myTestDate = "2020-12-30T13:30:00.0000000"; // -1

            var today = DateTime.Now.Date;

            var formattedToday = $"{today:s}";

            Console.WriteLine(formattedToday.CompareTo(myTestDate));

        }

        public static void SearchStudent()
        {
            var todayDateTime = DateTime.Now.Date;
            var today = $"{todayDateTime:s}";
            //var today = DateTime.Now.Date;





            IEnumerable<Student> studentsResults = from student in students
                                                       //where student.BirthDate < today
                                                   where Convert.ToDateTime(student.BirthDate).Date.CompareTo(Convert.ToDateTime(today)) == 1
                                                   //where student.BirthDate == today
                                                   orderby student.BirthDate ascending
                                                   select student;

            foreach (var student in studentsResults)
            {
                Console.WriteLine($"{student.First}, {student.Last}, {student.BirthDate}");
            }

        }

        // Create a data source by using a collection initializer.
        static List<Student> students = new List<Student>
        {
            //new Student {First="Svetlana", Last="Omelchenko", ID=111, Scores= new List<int> {97, 92, 81, 60}, BirthDate = new DateTime(2020,12,07)},
            //new Student {First="Claire", Last="O'Donnell", ID=112, Scores= new List<int> {75, 84, 91, 39}, BirthDate = new DateTime(2020,12,08)},
            //new Student {First="Sven", Last="Mortensen", ID=113, Scores= new List<int> {88, 94, 65, 91}, BirthDate = new DateTime(2020,11,07) },
            //new Student {First="Cesar", Last="Garcia", ID=114, Scores= new List<int> {97, 89, 85, 82}, BirthDate = new DateTime(2021,01,06)},
            //new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {35, 72, 91, 70}, BirthDate = new DateTime(2020,12,07)},
            //new Student {First="Fadi", Last="Fakhouri", ID=116, Scores= new List<int> {99, 86, 90, 94}, BirthDate = new DateTime(2020,12,07)},
            //new Student {First="Hanying", Last="Feng", ID=117, Scores= new List<int> {93, 92, 80, 87}, BirthDate = new DateTime(2020,12,08)},
            //new Student {First="Hugo", Last="Garcia", ID=118, Scores= new List<int> {92, 90, 83, 78}, BirthDate = new DateTime(2020,12,05)},
            new Student {First="Debra", Last="Garcia", ID=115, Scores= new List<int> {35, 72, 91, 70}, BirthDate = "2020-11-30T13:30:00.0000000"},
            new Student {First="Fadi", Last="Fakhouri", ID=116, Scores= new List<int> {99, 86, 90, 94}, BirthDate = "2020-12-07T13:30:00.0000000"},
            new Student {First="Hanying", Last="Feng", ID=117, Scores= new List<int> {93, 92, 80, 87}, BirthDate = "2020-12-08T13:30:00.0000000"},
            new Student {First="Hugo", Last="Garcia", ID=118, Scores= new List<int> {92, 90, 83, 78}, BirthDate = "2020-12-05T13:30:00.0000000"},
        };
    }

    internal class Student
    {
        public string First { get; set; }
        public string Last { get; set; }
        public int ID { get; set; }
        public List<int> Scores;
        //public DateTime BirthDate { get; set; }
        public string BirthDate { get; set; }

    }


}
