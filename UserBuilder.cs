using System;

public sealed class UserBuilder
{
    private int _id;
    private string _name = string.Empty;
    private int _age;

    public UserBuilder SetID(int id)
    {
        _id = id;
        return this;
    }
    public UserBuilder SetName(string name)
    {
        _name = name;
        return this;
    }
    public UserBuilder SetAge(int age)
    {
        _age = age;
        return this;
    }
    public User Build()
    {
        return new User(_id, _name, _age);
    }
}
