using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FishingNet
{
    public class Net
    {
        private readonly ICollection<Fish> fish;
        public Net(string material, int capacity)
        {
            Material = material;
            Capacity = capacity;
            this.fish = new List<Fish>();
        }

        public string Material {  get; set; }
        public int Capacity { get; set; }
        public IReadOnlyCollection<Fish> Fish => (IReadOnlyCollection<Fish>)this.fish;
        public int Count => this.fish.Count;

        public string AddFish(Fish fish)
        { 
           
            if (!string.IsNullOrWhiteSpace(fish.FishType) && fish.Length > 0 && fish.Weight > 0)
            {
                if (Capacity > Count)
                {
                    this.fish.Add(fish);
                    return $"Successfully added {fish.FishType} to the fishing net.";
                }
                else
                {
                    return "Fishing net is full.";
                }

            }
            else
            {
                return "Invalid fish.";
            }
           
            
        }
        public bool ReleaseFish(double weight) => this.fish.Remove(this.fish.FirstOrDefault(x => x.Weight == weight));
        
        public Fish GetFish(string fishType)
        {
            Fish fish = this.fish.FirstOrDefault(x => x.FishType == fishType);
            return fish;
        }
        public Fish GetBiggestFish()
        {
            double max = this.fish.Max(x => x.Length);
            Fish fish = this.fish.FirstOrDefault(x=>x.Length == max);
            return fish;
        }
        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Into the {this.Material}:");
            foreach (Fish fish in Fish.OrderByDescending(x => x.Length))
            {
                sb.AppendLine(fish.ToString());
            }
            return sb.ToString().TrimEnd();
        }
    }
}
