using System;
using System.Collections.Generic;

// Skill Class
class Skill
{
    private string name;
    private int power;

    public Skill(string name, int power)
    {
        this.name = name;
        this.power = power;
    }

    public int GetPower() => power;
    public string GetName() => name;

    public void Upgrade()
    {
        power += 5;
    }
}

// Inventory Class
class Inventory
{
    private List<string> items = new List<string>();

    public void AddItem(string item)
    {
        items.Add(item);
    }

    public void ShowInventory()
    {
        Console.WriteLine("Inventory:");
        if (items.Count == 0)
        {
            Console.WriteLine("Empty");
            return;
        }

        foreach (var item in items)
            Console.WriteLine("- " + item);
    }
}

// Base Character Class
abstract class Character
{
    private int id;
    private string name;
    protected int level = 1;
    protected int health = 100;
    protected int attackPower;
    protected Skill skill;
    protected Inventory inventory = new Inventory();

    protected Character(int id, string name)
    {
        this.id = id;
        this.name = name;
    }

    public int GetId() => id;
    public string GetName() => name;
    public bool IsAlive() => health > 0;

    public void LevelUp()
    {
        level++;
        health += 20;
        attackPower += 5;
        skill.Upgrade();
        Console.WriteLine(name + " leveled up to level " + level);
    }

    public abstract int Attack();

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health < 0) health = 0;
    }

    public virtual void Display()
    {
        Console.WriteLine("\nID: " + id);
        Console.WriteLine("Name: " + name);
        Console.WriteLine("Level: " + level);
        Console.WriteLine("Health: " + health);
        Console.WriteLine("Attack Power: " + attackPower);
        Console.WriteLine("Skill: " + skill.GetName() + " (Power: " + skill.GetPower() + ")");
    }
}

// Warrior Class
class Warrior : Character
{
    public Warrior(int id, string name) : base(id, name)
    {
        attackPower = 15;
        skill = new Skill("Sword Slash", 10);
    }

    public override int Attack()
    {
        return attackPower + skill.GetPower();
    }

    public override void Display()
    {
        base.Display();
        Console.WriteLine("Class: Warrior");
    }
}

// Mage Class
class Mage : Character
{
    public Mage(int id, string name) : base(id, name)
    {
        attackPower = 10;
        skill = new Skill("Fireball", 15);
    }

    public override int Attack()
    {
        return attackPower + skill.GetPower();
    }

    public override void Display()
    {
        base.Display();
        Console.WriteLine("Class: Mage");
    }
}

// Archer Class
class Archer : Character
{
    public Archer(int id, string name) : base(id, name)
    {
        attackPower = 12;
        skill = new Skill("Arrow Shot", 12);
    }

    public override int Attack()
    {
        return attackPower + skill.GetPower();
    }

    public override void Display()
    {
        base.Display();
        Console.WriteLine("Class: Archer");
    }
}

// Main System
class GameCharacterSystem
{
    static void Main()
    {
        List<Character> characters = new List<Character>();
        int choice;

        do
        {
            Console.WriteLine("\n--- Game Character System ---");
            Console.WriteLine("1. Create Character");
            Console.WriteLine("2. View Characters");
            Console.WriteLine("3. Level Up Character");
            Console.WriteLine("4. Battle Simulation");
            Console.WriteLine("5. Exit");
            Console.Write("Enter choice: ");
            choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    Console.Write("ID: ");
                    int id = int.Parse(Console.ReadLine());
                    Console.Write("Name: ");
                    string name = Console.ReadLine();

                    Console.WriteLine("Choose Class: 1.Warrior 2.Mage 3.Archer");
                    int type = int.Parse(Console.ReadLine());

                    if (type == 1)
                        characters.Add(new Warrior(id, name));
                    else if (type == 2)
                        characters.Add(new Mage(id, name));
                    else
                        characters.Add(new Archer(id, name));

                    Console.WriteLine("Character created successfully.");
                    break;

                case 2:
                    foreach (var c in characters)
                        c.Display();
                    break;

                case 3:
                    Console.Write("Enter Character ID: ");
                    int lvlId = int.Parse(Console.ReadLine());
                    Character levelChar = characters.Find(c => c.GetId() == lvlId);

                    if (levelChar != null)
                        levelChar.LevelUp();
                    else
                        Console.WriteLine("Character not found.");
                    break;

                case 4:
                    Console.Write("Attacker ID: ");
                    int atkId = int.Parse(Console.ReadLine());
                    Console.Write("Defender ID: ");
                    int defId = int.Parse(Console.ReadLine());

                    Character attacker = characters.Find(c => c.GetId() == atkId);
                    Character defender = characters.Find(c => c.GetId() == defId);

                    if (attacker != null && defender != null && attacker.IsAlive() && defender.IsAlive())
                    {
                        int damage = attacker.Attack();
                        defender.TakeDamage(damage);

                        Console.WriteLine(attacker.GetName() + " attacks " + defender.GetName());
                        Console.WriteLine("Damage: " + damage);
                        Console.WriteLine(defender.GetName() + " Health: " + (defender.IsAlive() ? "Remaining" : "Defeated"));
                    }
                    else
                    {
                        Console.WriteLine("Invalid characters or one is already defeated.");
                    }
                    break;

                case 5:
                    Console.WriteLine("Exiting game...");
                    break;

                default:
                    Console.WriteLine("Invalid choice.");
                    break;
            }

        } while (choice != 5);
    }
}
