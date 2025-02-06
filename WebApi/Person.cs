namespace WebApi;

//record Person(Guid Id, string Name);

public class Person
{
    public Person()
    {            
    }
    public Person(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; set; }
    public string? Name { get; set; }
}