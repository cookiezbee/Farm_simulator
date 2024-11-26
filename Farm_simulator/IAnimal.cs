using System;
using System.Windows.Controls;

namespace Farm_simulator
{
    public interface IAnimal
    {
        string Name { get; }
        string HungryIconPath { get; }
        string FullIconPath { get; }
        int Cost { get; set; }
        string PreferredFood { get; set; }
        DateTime LastFedTime { get; set; }
        TimeSpan HungerInterval { get; set; }
        DateTime? TimeBecameHungry { get; set; }
        TimeSpan ProductionInterval { get; set; }
        DateTime LastProductTime { get; set; }
        bool IsHungry { get; }
        string TooltipText { get; }
        void Feed();
        void CheckHunger(TextBlock detailsTextBlock);
        void Die(TextBlock detailsTextBlock);
        string MakeSound();
        string ProduceProduct();
    }
}
