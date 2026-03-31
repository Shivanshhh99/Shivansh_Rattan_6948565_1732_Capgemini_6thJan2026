// using System;

// abstract class Computer
// {
//     protected string type;
//     protected string model;
//     protected int gpu;
//     protected bool isTurnedOn = false;
//     public Computer(string type, string model, int gpu)
//     {
//         this.type = type;
//         this.model = model;
//         this.gpu = gpu;
//     }

//     public abstract string GetComputerType();
//     public abstract string GetComputerModel();
//     public abstract int GetComputerGPU();
//     public abstract bool GetComputerStatus();
//     public abstract void TurnOn();
// }

// class PersonalComputer : Computer
// {
//     public PersonalComputer(string type, string model, int gpu)
//         : base(type, model, gpu)
//     {
//     }

//     public override string GetComputerType()
//     {
//         return type;
//     }

//     public override string GetComputerModel()
//     {
//         return model;
//     }

//     public override int GetComputerGPU()
//     {
//         return gpu;
//     }

//     public override bool GetComputerStatus()
//     {
//         return isTurnedOn;
//     }

//     public override void TurnOn()
//     {
//         isTurnedOn = !isTurnedOn;
//     }
// }

// class Program
// {
//     static void Main()
//     {
//         Console.WriteLine("Enter Computer Type, Model and GPU (separated by space):");
//         string input = Console.ReadLine();
//         string[] arr = input.Split(' ');

//         PersonalComputer pc = new PersonalComputer(arr[0], arr[1], Convert.ToInt32(arr[2]));

//         Console.WriteLine($"Computer info: Type - {pc.GetComputerType()}, Model - {pc.GetComputerModel()}, GPU - {pc.GetComputerGPU()}");

//         string status = pc.GetComputerStatus() ? "on" : "off";
//         Console.WriteLine($"Computer is {status}");

//         Console.WriteLine("Switching");
//         pc.TurnOn();

//         status = pc.GetComputerStatus() ? "on" : "off";
//         Console.WriteLine($"Computer is {status}");

//         Console.WriteLine("Switching");
//         pc.TurnOn();

//         status = pc.GetComputerStatus() ? "on" : "off";
//         Console.WriteLine($"Computer is {status}");
//     }
// }