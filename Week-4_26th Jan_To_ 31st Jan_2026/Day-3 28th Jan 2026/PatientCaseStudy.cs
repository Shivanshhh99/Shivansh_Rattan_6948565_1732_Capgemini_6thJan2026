using System;
using System.Collections.Generic;
using System.Linq;

namespace PatientCaseStudy
{
    internal class Patient
    {
        private string _name;
        private int _age;
        private string _illness;
        private string _city;

        public Patient() { }

        public Patient(string name, int age, string illness, string city)
        {
            _name = name;
            _age = age;
            _illness = illness;
            _city = city;
        }

        public string Name
        {
            get { return _name; }
            set { _name = value; }
        }

        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }

        public string Illness
        {
            get { return _illness; }
            set { _illness = value; }
        }

        public string City
        {
            get { return _city; }
            set { _city = value; }
        }

        public override string ToString()
        {
            return string.Format("{0,-21}{1,-6}{2,-17}{3,-20}", _name, _age, _illness, _city);
        }
    }

    internal class PatientBO
    {
        public void DisplayPatientDetails(List<Patient> patientList, string name)
        {
            var result = patientList.Where(p => p.Name == name).ToList();

            if (result.Count == 0)
            {
                Console.WriteLine("Patient named " + name + " not found");
            }
            else
            {
                Console.WriteLine("Name                 Age   Illness          City");
                foreach (var p in result)
                {
                    Console.WriteLine(p.ToString());
                }
            }
        }

        public void DisplayYoungestPatientDetails(List<Patient> patientList)
        {
            int minAge = patientList.Min(p => p.Age);
            var patient = patientList.First(p => p.Age == minAge);

            Console.WriteLine("The Youngest Patient Details");
            Console.WriteLine("Name                 Age   Illness          City");
            Console.WriteLine(patient.ToString());
        }

        public void DisplayPatientsFromCity(List<Patient> patientList, string cname)
        {
            var result = patientList.Where(p => p.City == cname).ToList();

            if (result.Count == 0)
            {
                Console.WriteLine("City named " + cname + " not found");
            }
            else
            {
                Console.WriteLine("Name                 Age   Illness          City");
                foreach (var p in result)
                {
                    Console.WriteLine(p.ToString());
                }
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            List<Patient> patientList = new List<Patient>();

            Console.WriteLine("Enter the number of patients");
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                Console.WriteLine("Enter patient " + (i + 1) + " details:");

                Console.WriteLine("Enter the name");
                string name = Console.ReadLine();

                Console.WriteLine("Enter the age");
                int age = int.Parse(Console.ReadLine());

                Console.WriteLine("Enter the illness");
                string illness = Console.ReadLine();

                Console.WriteLine("Enter the city");
                string city = Console.ReadLine();

                patientList.Add(new Patient(name, age, illness, city));
            }

            PatientBO patientBO = new PatientBO();
            string opt;

            do
            {
                Console.WriteLine("Enter your choice:");
                Console.WriteLine("1)Display Patient Details");
                Console.WriteLine("2)Display Youngest Patient Details");
                Console.WriteLine("3)Display Patients from City");

                int choice = int.Parse(Console.ReadLine());

                switch (choice)
                {
                    case 1:
                        Console.WriteLine("Enter patient name:");
                        patientBO.DisplayPatientDetails(patientList, Console.ReadLine());
                        break;

                    case 2:
                        patientBO.DisplayYoungestPatientDetails(patientList);
                        break;

                    case 3:
                        Console.WriteLine("Enter city");
                        patientBO.DisplayPatientsFromCity(patientList, Console.ReadLine());
                        break;
                }

                Console.WriteLine("Do you want to continue(Yes/No)?");
                opt = Console.ReadLine();

            } while (opt.Equals("Yes"));
        }
    }
}