using System;

namespace Strategia_Kaczki
{
    /// <summary>
    /// Interfejsy odpowiedzialne za kwakanie i latanie
    /// </summary>
    public interface IFlyBehavior
    {
        void Fly();
    }
    public interface IQuackBehavior
    {
        void Quack();
    }
    //klasy implementujące interfejsy latania  i kwakania
    public class FlyWithWings : IFlyBehavior
    {
        public void Fly()
        {
            Console.WriteLine("I'm flying with my wings!");
        }
    }
    public class FlyNoWay : IFlyBehavior
    {
        public void Fly()
        {
            Console.WriteLine("I can't fly...");
        }
    }
    public class QuackReal : IQuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine("Quack");
        }
    }
    public class QuackMute : IQuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine("...");
        }
    }
    public class QuackSqueak : IQuackBehavior
    {
        public void Quack()
        {
            Console.WriteLine("Squeak");
        }
    }
    //klasa abstrakcyjna kaczki
    public abstract class Duck
    {
        internal IFlyBehavior flyBehavior;
        internal IQuackBehavior quackBehavior;

        public abstract void Display();

        public void SetFlyBehavior(IFlyBehavior fb)
        {
            flyBehavior = fb;
        }

        public void SetQuackBehavior(IQuackBehavior qb)
        {
            quackBehavior = qb;
        }

        public void PerformFly()
        {
            flyBehavior.Fly();
        }

        public void PerformQuack()
        {
            quackBehavior.Quack();
        }

        public void Swim()
        {
            Console.WriteLine("All ducks float!");
        }
    }

    public class MallardDuck : Duck
    {
        public MallardDuck()
        {
            flyBehavior = new FlyWithWings();
            quackBehavior = new QuackReal();
        }

        public override void Display()
        {
            Console.WriteLine("I'm a mallard!");
        }
    }
    public class RubberDuck : Duck
    {
        public RubberDuck()
        {
            flyBehavior = new FlyNoWay();
            quackBehavior = new QuackSqueak();
        }

        public override void Display()
        {
            Console.WriteLine("I am a rubber duckie.");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Duck mallard = new MallardDuck();
            mallard.PerformQuack();
            mallard.PerformFly();
            mallard.SetFlyBehavior(new FlyNoWay());
            mallard.PerformFly();

            Console.WriteLine("Koniec");
            Console.ReadKey();
        }
    }
}
