namespace FlipKey
{
    internal class Program
    {
        public string CleanseAndInvert(string input)
        {
            if (string.IsNullOrEmpty(input) || input.Length < 6)
                return "";

            if (!input.All(char.IsLetter))
                return "";

            string lower = input.ToLower();
            string filtered = "";

            foreach (char c in lower)
            {
                if ((int)c % 2 != 0)
                    filtered += c;
            }

            char[] arr = filtered.Reverse().ToArray();

            for (int i = 0; i < arr.Length; i++)
            {
                if (i % 2 == 0)
                    arr[i] = char.ToUpper(arr[i]);
            }

            return new string(arr);
        }

        public static void Main(string[] args)
        {
            Console.WriteLine("Enter the word");
            string input = Console.ReadLine();

            Program p = new Program();
            string result = p.CleanseAndInvert(input);

            if (result == "")
                Console.WriteLine("Invalid Input");
            else
                Console.WriteLine("The generated key is - " + result);
        }
    }
}