using System;
using System.Collections.Generic;
using System.Linq;

class User
{
    public int Id { get; set; }
    public string IdentityNumber { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public int Age { get; set; }
    public DateTime? BirthDate { get; set; }
    public string Email { get; set; }
    public string Gender { get; set; }
    public string Country { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string ZipCode { get; set; }
    public string PhoneNumber { get; set; }
    public string Department { get; set; }
    public string Roles { get; set; }
    public DateTime? JoinDate { get; set; }
    public decimal Credit { get; set; }
    public string Status { get; set; }
}

static class UserManager
{
    public static (List<User>, List<User>) CompareUsers(List<User> usersListInDB, List<User> newUsersList)
    {
        List<User> updated = new List<User>();
        List<User> inserted = new List<User>();

        foreach (var newUser in newUsersList)
        {
            if (newUser.Id == 0)
            {
                inserted.Add(newUser);
            }
            else
            {
                var existingUser = usersListInDB.FirstOrDefault(x => x.Id == newUser.Id);

                if (existingUser != null)
                {
                    if (existingUser.FirstName != newUser.FirstName ||
                        existingUser.LastName != newUser.LastName ||
                        existingUser.Email != newUser.Email ||
                        existingUser.Status != newUser.Status)
                    {
                        updated.Add(newUser);
                    }
                }
            }
        }

        return (updated, inserted);
    }
}

class Program
{
    static void Main()
    {
        List<User> usersListInDB = new List<User>();
        List<User> newUsersList = new List<User>();

        Console.WriteLine("Enter number of users in DB:");
        int userInDbCount = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter user details in DB (Id,IdentityNumber,FirstName,LastName,Age,BirthDate,Email,Gender,Country,City,Address,ZipCode,PhoneNumber,Department,Roles,JoinDate,Credit,Status):");
        for (int i = 0; i < userInDbCount; i++)
        {
            var u = Console.ReadLine().Split(',');

            usersListInDB.Add(new User
            {
                Id = Convert.ToInt32(u[0]),
                IdentityNumber = u[1],
                FirstName = u[2],
                LastName = u[3],
                Age = string.IsNullOrEmpty(u[4]) ? 0 : Convert.ToInt32(u[4]),
                BirthDate = string.IsNullOrEmpty(u[5]) ? (DateTime?)null : Convert.ToDateTime(u[5]),
                Email = u[6],
                Gender = u[7],
                Country = u[8],
                City = u[9],
                Address = u[10],
                ZipCode = u[11],
                PhoneNumber = u[12],
                Department = u[13],
                Roles = u[14],
                JoinDate = string.IsNullOrEmpty(u[15]) ? (DateTime?)null : Convert.ToDateTime(u[15]),
                Credit = string.IsNullOrEmpty(u[16]) ? 0 : Convert.ToDecimal(u[16]),
                Status = u[17]
            });
        }

        Console.WriteLine("Enter number of new users:");
        int newUsersCount = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Enter new user details (same format as above):");
        for (int i = 0; i < newUsersCount; i++)
        {
            var u = Console.ReadLine().Split(',');

            newUsersList.Add(new User
            {
                Id = Convert.ToInt32(u[0]),
                IdentityNumber = u[1],
                FirstName = u[2],
                LastName = u[3],
                Age = string.IsNullOrEmpty(u[4]) ? 0 : Convert.ToInt32(u[4]),
                BirthDate = string.IsNullOrEmpty(u[5]) ? (DateTime?)null : Convert.ToDateTime(u[5]),
                Email = u[6],
                Gender = u[7],
                Country = u[8],
                City = u[9],
                Address = u[10],
                ZipCode = u[11],
                PhoneNumber = u[12],
                Department = u[13],
                Roles = u[14],
                JoinDate = string.IsNullOrEmpty(u[15]) ? (DateTime?)null : Convert.ToDateTime(u[15]),
                Credit = string.IsNullOrEmpty(u[16]) ? 0 : Convert.ToDecimal(u[16]),
                Status = u[17]
            });
        }

        var result = UserManager.CompareUsers(usersListInDB, newUsersList);

        Console.WriteLine($"\nUpdated Users: {result.Item1.Count}");
        Console.WriteLine($"Inserted Users: {result.Item2.Count}");
    }
}