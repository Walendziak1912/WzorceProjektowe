using System;
using System.Collections.Generic;
using System.Threading;

namespace Obserwator_YouTube
{
    public interface IMailObserver
    {
        void Update();
    }
    public class User : IMailObserver
    {
        private String name;
        public User(String name)
        {
            this.name = name;
        }

        public void Update()
        {
            Console.WriteLine("\'"+name+"\'" + " got mail! ");
        }
    }

    public class Blog
    {
        public List<IMailObserver> mailObserwers;

        public Blog()
        {
            mailObserwers = new List<IMailObserver>();
        }

        public void Subscribe(IMailObserver obserwer)
        {
            mailObserwers.Add(obserwer);
        }

        public void StartWork() 
        {
            Console.WriteLine("Dodano nowy film!");
            foreach (var obs in mailObserwers)
            {
                obs.Update();
            }
            
        }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            Blog blog = new Blog();
            blog.Subscribe(new User("Darek"));
            blog.Subscribe(new User("Heniek"));
            blog.StartWork();
            Console.ReadKey();        
        }
    }
}
