List<User> users = new List<User>();
List<Pet> pets = new List<Pet>{new Pet("1","1", "coco", "bulldog", 9)};

users.Add(new User("1", "juan", 12, "3124121", "1"));
users.Add(new User("2", "maria", 28, "3001112222", "1"));
users.Add(new User("3", "carlos", 35, "3013334444", "1"));
users.Add(new User("4", "ana", 22, "3025556666", "1"));
users.Add(new User("5", "sofia", 30, "3037778888", "1"));
users.Add(new User("6", "diego", 41, "3049990000", "1"));
users.Add(new User("7", "juan", 19, "3051234567", "1"));
users.Add(new User("8", "andres", 27, "3062345678", "1"));
users.Add(new User("9", "valentina", 33, "3073456789", "1"));
users.Add(new User("10", "sebastian", 24, "3084567890","3"));

var found = Queries.QueryNames(users);

foreach (var f in found)
{
    Console.WriteLine(f.Name);
    Console.WriteLine(found.Count());
}

class User
{
    public string Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; } = 0;
    public string Cel { get; set; } = string.Empty;
    
    public string PuppyId { get; set; }
    public Pet? Pet { get; set; } = null!;

    public User(string id, string name, int age, string cel, string puppyId)
    {
        Id = id;
        Name = name;
        Age = age;
        Cel = cel;
        PuppyId = puppyId;
    }
}

class Pet
{
    public string Id { get; set; }
    public string IdOwner { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Race { get; set; } = string.Empty;
    public int Age { get; set; } = 0;

    public Pet(string id, string idOwner, string name, string race, int age)
    {
        Id = id;
        IdOwner = idOwner;
        Name = name;
        Race = race;
        Age = age;
    }
}

class Queries
{
    
    public static IEnumerable<User> QueryNames(List<User> arr)
    {
        return arr.Where(user => user.Name == "juan");
    }
    
    public static IEnumerable<User> QueryAge(List<User> arr)
    {
        return arr.Where(user => user.Name == "juan");
    }
}