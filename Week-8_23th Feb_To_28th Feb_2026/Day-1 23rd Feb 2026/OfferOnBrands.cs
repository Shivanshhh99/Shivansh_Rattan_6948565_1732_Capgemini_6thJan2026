using System;
using System.Collections.Generic;
using System.Linq;

namespace OfferOnBrands
{
    class Model
    {
        public string ModelName { get; set; }
        public int CarSpeed { get; set; }
    }

    class Implementation
    {
        public static List<string> GetModelName(List<Model> list)
        {
            return list.Select(x => x.ModelName).ToList();
        }

        public static Model GetModelInfo(List<Model> list, string name, int speed)
        {
            return list.FirstOrDefault(x => x.ModelName == name && x.CarSpeed == speed);
        }
    }

    class Program
    {
        static void Main()
        {
            List<Model> list = new List<Model>
            {
                new Model{ModelName="BMW", CarSpeed=200},
                new Model{ModelName="Audi", CarSpeed=180}
            };

            var names = Implementation.GetModelName(list);
            names.ForEach(Console.WriteLine);

            var model = Implementation.GetModelInfo(list, "BMW", 200);
            Console.WriteLine(model?.ModelName);
        }
    }
}