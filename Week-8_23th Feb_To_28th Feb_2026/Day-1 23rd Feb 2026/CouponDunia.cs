namespace CouponDunia
{
    class InvalidCouponException : Exception
    {
        public InvalidCouponException(string msg) : base(msg) { }
    }

    class Product
    {
        public string name;
        public double price;
        public string coupon;

        public Product(string name, double price, string coupon)
        {
            this.name = name;
            this.price = price;
            this.coupon = coupon;
        }
    }

    class Validator
    {
        public static string ValidateCoupon(Product p)
        {
            var parts = p.coupon.Split('-');

            if (parts.Length != 2)
                throw new InvalidCouponException("Invalid Format");

            if (!parts[0].Equals(p.name, StringComparison.OrdinalIgnoreCase))
                throw new InvalidCouponException("Product Name Mismatch");

            int discount = int.Parse(parts[1]);

            if (discount < 10 || discount > 25)
                throw new InvalidCouponException("Invalid Discount");

            return "Coupon Valid";
        }

        public static double NetPrice(Product p)
        {
            int discount = int.Parse(p.coupon.Split('-')[1]);
            return p.price - (p.price * discount / 100);
        }
    }

    class Program
    {
        static void Main()
        {
            Product p = new Product("Phone", 10000, "Phone-20");

            try
            {
                Console.WriteLine(Validator.ValidateCoupon(p));
                Console.WriteLine("Net Price: " + Validator.NetPrice(p));
            }
            catch (InvalidCouponException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}