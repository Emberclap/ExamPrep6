using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;

namespace _01._Bakery_Shop
{
    public class Program
    {
        
        static void Main()
        {

            CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;

            double[] waterList = Console.ReadLine()
                .Split()
                .Select(double.Parse)
                .ToArray();

            Queue<double> water = new Queue<double>(waterList);

            Stack<double> flour = new Stack<double>(Console.ReadLine()
                .Split()
                .Select(double.Parse));

            Dictionary<string, int> bakery = new Dictionary<string, int>
            {
                { "Croissant", 0 },
                { "Muffin", 0 },
                { "Baguette", 0},
                { "Bagel", 0 }
            };

            while (water.Any() && flour.Any())
            {
                double mix = flour.Peek() + water.Peek();
                double waterPercent = (water.Peek() * 100) / mix;
                double flourPercent = (flour.Peek() * 100) / mix;

                if (waterPercent == 50 && flourPercent == 50)
                {
                    bakery["Croissant"]++;
                    flour.Pop();
                    water.Dequeue();
                }
                else if (waterPercent == 40 && flourPercent == 60)
                {
                    bakery["Muffin"]++;
                    flour.Pop();
                    water.Dequeue();
                }
                else if (waterPercent == 30 && flourPercent == 70)
                {
                    bakery["Baguette"]++;
                    flour.Pop();
                    water.Dequeue();
                }
                else if (waterPercent == 20 && flourPercent == 80)
                {
                    bakery["Bagel"]++;
                    flour.Pop();
                    water.Dequeue();
                }
                else
                {
                    double leftOverFlour = flour.Peek() - water.Peek();
                    bakery["Croissant"]++;
                    flour.Pop();
                    water.Dequeue();
                    flour.Push(leftOverFlour);

                }
            }
            foreach (var product in bakery.OrderByDescending(x => x.Value).ThenBy(x => x.Key))
            {
                if (product.Value > 0)
                {
                    Console.WriteLine($"{product.Key}: {product.Value}");

                }
            }
            if (water.Any())
            {
                Console.WriteLine($"Water left: {string.Join(", ", water)}");
            }
            else
            {
                Console.WriteLine("Water left: None");
            }
            if (flour.Any())
            {
                Console.WriteLine($"Flour left: {string.Join(", ", flour)} ");
            }
            else
            {
                Console.WriteLine("Flour left: None");
            }
        }

    }
}
