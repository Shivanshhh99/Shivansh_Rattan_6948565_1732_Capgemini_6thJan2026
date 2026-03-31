namespace ValidateParenthesisByOneSwap
{
    internal class Program
    {
        public bool CanBeBalanced(string s)
        {
            int open = 0, close = 0;
            foreach (char c in s)
            {
                if (c == '(') open++;
                else close++;
            }

            if (open != close) return false;
            if (IsValid(s)) return true;

            int balance = 0, negativeCount = 0;
            foreach (char c in s)
            {
                balance += (c == '(') ? 1 : -1;
                if (balance < 0) negativeCount++;
            }

            return negativeCount <= 1;
        }

        private bool IsValid(string s)
        {
            int balance = 0;
            foreach (char c in s)
            {
                balance += (c == '(') ? 1 : -1;
                if (balance < 0) return false;
            }
            return balance == 0;
        }
    }
}