using System;

public sealed class User	
{
    public int ID { get; }
	public string Name { get; }
    public int Age { get; set; }

    internal User(int id, string name, int age)
        => (ID, Name, Age) = (id, name, age);

    public override string ToString()
    {
        return $"User[ID={ID}, Name={Name}, Age={Age}]";
    }
}