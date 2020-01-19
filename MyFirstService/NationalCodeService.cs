using System;
using System.IO;
using System.Linq;
using System.Timers;
using System.Threading;
using System.Diagnostics;
using System.ServiceProcess;
using System.Collections.Generic;
using System.Xml;

namespace MyFirstService
{
    public partial class NationalCodeService : ServiceBase
    {
        System.Timers.Timer timer =
            new System.Timers.Timer();
        static List<Person> people = new List<Person>();
        public static int periodsCounter = 0;

        public NationalCodeService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            timer.Elapsed +=
                new ElapsedEventHandler(OnElapsedTime);
            timer.Interval = 10000; //ms Periods
            timer.Enabled = true;
        }

        private void OnElapsedTime(object source, ElapsedEventArgs e)
        {
            CreateList();
            xmlWriter(CheckNationalCode("0410670030"));
        }

        public static void CreateList()
        {
            people = new List<Person>
            {
                new Person
                {
                    NationalCode = "0410670030",
                    BirthDate = "1378/08/25",
                },
                new Person
                {
                    NationalCode = "0410670031",
                    BirthDate = "1323/11/22" ,
                },
                new Person
                {
                    NationalCode = "0410674512",
                    BirthDate = "1338/05/02",
                },
                new Person
                {
                    NationalCode = "0410679854",
                    BirthDate = "1326/08/26",
                },
            };

        }
        static Person CheckNationalCode(string nationalCode)
        {
            var output =
                people.Where(person =>
                string.Compare(person.NationalCode,
                nationalCode) == 0).FirstOrDefault();

            return output;
        }

        static void xmlWriter(Person person)
        {
            periodsCounter++;
            XmlWriter xmlWriter = XmlWriter.Create(AppDomain.CurrentDomain.BaseDirectory + $"\\output\\Result {periodsCounter}" + ".xml");
            xmlWriter.WriteStartDocument();
            xmlWriter.WriteStartElement("Info");

            xmlWriter.WriteStartElement("Person");
            xmlWriter.WriteString("BirthDate:" + person.BirthDate + ", ");
            xmlWriter.WriteString("NationalCode:" + person.NationalCode);
            xmlWriter.WriteEndElement();

            xmlWriter.WriteEndDocument();
            xmlWriter.Close();
        }
    }
}
