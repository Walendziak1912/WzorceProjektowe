using System;

namespace Strategia_Kaczki
{
    /// <summary>
    /// Interfejsy odpowiedzialne za kwakanie i latanie
    /// </summary>
    public interface ILatanie
    {
        void Leć();
    }
    public interface IKwakanie
    {
        void Kwacz();
    }
    //klasy implementujące interfejsy latania  i kwakania
    public class OpcjaLatania : ILatanie
    {
        public void Leć()
        {
            Console.WriteLine("Latam bo mam skrzydła!");
        }
    }
    public class BrakOpcjiLatania : ILatanie
    {
        public void Leć()
        {
            Console.WriteLine("Nie potrafię latać");
        }
    }
    public class OpcjaKwakanie : IKwakanie
    {
        public void Kwacz()
        {
            Console.WriteLine("Kwa Kwa Kwa");
        }
    }
    public class BrakOpcjKwakania : IKwakanie
    {
        public void Kwacz()
        {
            Console.WriteLine("...");
        }
    }
    public class OpcjaSyczenie : IKwakanie
    {
        public void Kwacz()
        {
            Console.WriteLine("ssss sss ss s");
        }
    }
    //klasa abstrakcyjna kaczki
    public abstract class Kaczka
    {
        internal ILatanie zachowanieLatanie;
        internal IKwakanie zachowanieKwakanie;

        public abstract void Wyświetl();

        public void SetZachowanieLatanie(ILatanie fb)
        {
            zachowanieLatanie = fb;
        }

        public void SetZachowanieKwakanie(IKwakanie qb)
        {
            zachowanieKwakanie = qb;
        }

        public void WywołajLatanie()
        {
            zachowanieLatanie.Leć();
        }

        public void WywołajKwakanie()
        {
            zachowanieKwakanie.Kwacz();
        }

        public void Płyń()
        {
            Console.WriteLine("Wszystkie kaczki potrafią pływać!");
        }
    }

    public class DzikaKaczka : Kaczka
    {
        public DzikaKaczka()
        {
            zachowanieLatanie = new OpcjaLatania();
            zachowanieKwakanie = new OpcjaKwakanie();
        }

        public override void Wyświetl()
        {
            Console.WriteLine("Jestem dzika kaczka!");
        }
    }
    public class GumowaKaczka : Kaczka
    {
        public GumowaKaczka()
        {
            zachowanieLatanie = new BrakOpcjiLatania();
            zachowanieKwakanie = new OpcjaSyczenie();
        }

        public override void Wyświetl()
        {
            Console.WriteLine("Jestem gumowa kaczka");
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            Kaczka dzika = new DzikaKaczka();
            dzika.Wyświetl();
            dzika.WywołajKwakanie();
            dzika.WywołajLatanie();
            dzika.SetZachowanieLatanie(new BrakOpcjiLatania());
            dzika.WywołajLatanie();

            Console.WriteLine("Koniec");
            Console.ReadKey();
        }
    }
}
