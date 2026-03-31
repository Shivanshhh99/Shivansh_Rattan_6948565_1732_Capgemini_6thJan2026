using System;
using System.Collections.Generic;

// Base Class
class Person
{
    private int id;
    private string name;

    public Person(int id, string name)
    {
        this.id = id;
        this.name = name;
    }

    public int GetId() => id;
    public string GetName() => name;

    public virtual void DisplayProfile()
    {
        Console.WriteLine("ID: " + id);
        Console.WriteLine("Name: " + name);
    }
}

// Patient Class
class Patient : Person
{
    private List<MedicalRecord> medicalHistory = new List<MedicalRecord>();

    public Patient(int id, string name) : base(id, name) { }

    public void AddMedicalRecord(MedicalRecord record)
    {
        medicalHistory.Add(record);
    }

    public void ViewMedicalHistory()
    {
        Console.WriteLine("\n--- Medical History ---");
        if (medicalHistory.Count == 0)
        {
            Console.WriteLine("No records found.");
            return;
        }

        foreach (var record in medicalHistory)
            record.DisplayRecord();
    }

    public override void DisplayProfile()
    {
        base.DisplayProfile();
        Console.WriteLine("Role: Patient");
    }
}

// Doctor Class
class Doctor : Person
{
    private string specialization;

    public Doctor(int id, string name, string specialization)
        : base(id, name)
    {
        this.specialization = specialization;
    }

    public string GetSpecialization() => specialization;

    public override void DisplayProfile()
    {
        base.DisplayProfile();
        Console.WriteLine("Role: Doctor");
        Console.WriteLine("Specialization: " + specialization);
    }
}

// Nurse Class
class Nurse : Person
{
    public Nurse(int id, string name) : base(id, name) { }

    public override void DisplayProfile()
    {
        base.DisplayProfile();
        Console.WriteLine("Role: Nurse");
    }
}

// Appointment Class
class Appointment
{
    private Patient patient;
    private Doctor doctor;
    private DateTime date;

    public Appointment(Patient patient, Doctor doctor, DateTime date)
    {
        this.patient = patient;
        this.doctor = doctor;
        this.date = date;
    }

    public void DisplayAppointment()
    {
        Console.WriteLine("\n--- Appointment Details ---");
        Console.WriteLine("Patient: " + patient.GetName());
        Console.WriteLine("Doctor: " + doctor.GetName());
        Console.WriteLine("Specialization: " + doctor.GetSpecialization());
        Console.WriteLine("Date: " + date.ToShortDateString());
    }
}

// Medical Record Class
class MedicalRecord
{
    private string diagnosis;
    private string treatment;
    private DateTime recordDate;

    public MedicalRecord(string diagnosis, string treatment)
    {
        this.diagnosis = diagnosis;
        this.treatment = treatment;
        recordDate = DateTime.Now;
    }

    public void DisplayRecord()
    {
        Console.WriteLine("Date: " + recordDate.ToShortDateString());
        Console.WriteLine("Diagnosis: " + diagnosis);
        Console.WriteLine("Treatment: " + treatment);
        Console.WriteLine("----------------------------");
    }
}

// Main System
class HospitalManagementSystem
{
    static void Main()
    {
        List<Patient> patients = new List<Patient>();
        List<Doctor> doctors = new List<Doctor>();
        List<Appointment> appointments = new List<Appointment>();

        int choice;

        do
        {
            Console.WriteLine("\n--- Hospital Management System ---");
            Console.WriteLine("1. Register Patient");
            Console.WriteLine("2. Register Doctor");
            Console.WriteLine("3. Schedule Appointment");
            Console.WriteLine("4. Add Medical Record");
            Console.WriteLine("5. View Patient Medical History");
            Console.WriteLine("6. Exit");
            Console.Write("Enter choice: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("Patient ID: ");
                    int pid = int.Parse(Console.ReadLine());
                    Console.Write("Patient Name: ");
                    string pname = Console.ReadLine();

                    patients.Add(new Patient(pid, pname));
                    Console.WriteLine("Patient registered successfully.");
                    break;

                case 2:
                    Console.Write("Doctor ID: ");
                    int did = int.Parse(Console.ReadLine());
                    Console.Write("Doctor Name: ");
                    string dname = Console.ReadLine();
                    Console.Write("Specialization: ");
                    string spec = Console.ReadLine();

                    doctors.Add(new Doctor(did, dname, spec));
                    Console.WriteLine("Doctor registered successfully.");
                    break;

                case 3:
                    Console.Write("Patient ID: ");
                    int apPid = int.Parse(Console.ReadLine());
                    Console.Write("Doctor ID: ");
                    int apDid = int.Parse(Console.ReadLine());

                    Patient patient = patients.Find(p => p.GetId() == apPid);
                    Doctor doctor = doctors.Find(d => d.GetId() == apDid);

                    if (patient != null && doctor != null)
                    {
                        Appointment appt = new Appointment(patient, doctor, DateTime.Now);
                        appointments.Add(appt);
                        appt.DisplayAppointment();
                    }
                    else
                    {
                        Console.WriteLine("Invalid patient or doctor ID.");
                    }
                    break;

                case 4:
                    Console.Write("Patient ID: ");
                    int recPid = int.Parse(Console.ReadLine());
                    Patient recPatient = patients.Find(p => p.GetId() == recPid);

                    if (recPatient != null)
                    {
                        Console.Write("Diagnosis: ");
                        string diag = Console.ReadLine();
                        Console.Write("Treatment: ");
                        string treat = Console.ReadLine();

                        recPatient.AddMedicalRecord(new MedicalRecord(diag, treat));
                        Console.WriteLine("Medical record added.");
                    }
                    else
                    {
                        Console.WriteLine("Patient not found.");
                    }
                    break;

                case 5:
                    Console.Write("Patient ID: ");
                    int viewPid = int.Parse(Console.ReadLine());
                    Patient viewPatient = patients.Find(p => p.GetId() == viewPid);

                    if (viewPatient != null)
                        viewPatient.ViewMedicalHistory();
                    else
                        Console.WriteLine("Patient not found.");
                    break;

                case 6:
                    Console.WriteLine("Exiting system...");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

        } while (choice != 6);
    }
}
