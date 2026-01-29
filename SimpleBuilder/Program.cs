namespace SimpleBuilder
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var user = new UserBuilder()
                .SetID(1)
                .SetName("Jan")
                .SetAge(30)
                .Build();

            Console.WriteLine(user.ToString());
        }
    }
}
