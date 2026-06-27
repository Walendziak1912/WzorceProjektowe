using System;
using System.Linq;

namespace Strategia_Sortowanie
{
    public interface ISort
    {
        int[] Sort(int[] tab);
    }

    public class Bąbelkowe : ISort
    {
        public int[] Sort(int[] tab)
        {
            var n = tab.Length;
            do
            {
                for (int i = 0; i < n - 1; i++)
                {
                    if (tab[i].CompareTo(tab[i + 1]) > 0)
                    {
                        var tmp = tab[i];
                        tab[i] = tab[i + 1];
                        tab[i + 1] = tmp;
                    }
                }
                n = n - 1;
            } while (n > 1);
            return tab;
        }   
    }
    public class QuickSort : ISort
    {
        public int[] Sort(int[] tab)
        {
            return SortowanieSzybkie(tab, 0, tab.Length - 1);
        }
        private int[] SortowanieSzybkie(int[] arr, int start, int end)
        {
            int i;
            if (start < end)
            {
                i = Partition(arr, start, end);

                SortowanieSzybkie(arr, start, i - 1);
                SortowanieSzybkie(arr, i + 1, end);
            }
            return arr;
        }
        private int Partition(int[] arr, int start, int end)
        {
            int temp;
            int p = arr[end];
            int i = start - 1;

            for (int j = start; j <= end - 1; j++)
            {
                if (arr[j] <= p)
                {
                    i++;
                    temp = arr[i];
                    arr[i] = arr[j];
                    arr[j] = temp;
                }
            }

            temp = arr[i + 1];
            arr[i + 1] = arr[end];
            arr[end] = temp;
            return i + 1;
        }
    }

    public class Sortownia 
    {
        ISort sortObj;
        public Sortownia(ISort obj)
        {
            sortObj = obj;
        }

        public void ZmienMetodeSortujaca(ISort obj)
        {
            sortObj = obj;
        }

        public void SortujElementy(int[] tab)
        {
            sortObj.Sort(tab).ToList().ForEach(Console.WriteLine);
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            var tab = new int[] { 45, 2, 6, 12, 1, 4, 3, 9, 19 };
            Sortownia sortObj = new Sortownia(new QuickSort());
            sortObj.SortujElementy(tab);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Koniec");
            Console.ReadKey();
        }
    }
}
