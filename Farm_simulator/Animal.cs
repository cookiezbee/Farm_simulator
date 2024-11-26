using System;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Farm_simulator
{
    public abstract class Animal : IAnimal, INotifyPropertyChanged
    {
        public string Name { get; set; }
        public string HungryIconPath { get;}
        public string FullIconPath { get;}
        public int Cost { get; set; }
        public string PreferredFood { get; set; }
        public TimeSpan HungerInterval { get; set; }
        public DateTime LastFedTime { get; set; }
        public TimeSpan ProductionInterval { get; set; }
        public DateTime LastProductTime { get; set; }
        public bool IsHungry => DateTime.Now - LastFedTime > HungerInterval;
        public string CurrentIconPath => IsHungry ? HungryIconPath : FullIconPath;

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private DateTime? timeBecameHungry;
        public DateTime? TimeBecameHungry
        {
            get => timeBecameHungry;
            set
            {
                timeBecameHungry = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(TooltipText));
            }
        }

        public TimeSpan TimeToNextProduction
        {
            get
            {
                var timeElapsedSinceLastProduction = DateTime.Now - LastProductTime;
                return ProductionInterval - timeElapsedSinceLastProduction;
            }
        }

        public string TooltipText
        {
            get
            {
                var timeToDie = TimeBecameHungry.HasValue
                    ? $"{30 - (DateTime.Now - TimeBecameHungry.Value).Seconds} s."
                    : "Not hungry.";

                var timeToProduct = TimeToNextProduction.TotalSeconds > 0
                    ? $"{TimeToNextProduction.Seconds} s."
                    : "Ready to produce!";

                return $"Animal: {GetType().Name}\n" +
                       $"Preferred food: {PreferredFood}\n" +
                       $"Condition: {(IsHungry ? "Hungry" : "Full")}\n" +
                       $"Die in: {timeToDie}\n" +
                       $"Next product in: {timeToProduct}";
            }
        }

        public Animal(string name, string hungryIconPath, string fullIconPath, TimeSpan hungerInterval, string preferredFood, TimeSpan productionInterval)
        {
            Name = name;
            HungryIconPath = hungryIconPath;
            FullIconPath = fullIconPath;
            PreferredFood = preferredFood;
            LastFedTime = DateTime.Now;
            HungerInterval = hungerInterval;
            ProductionInterval = productionInterval;
            LastProductTime = DateTime.Now;
        }

        public void Feed()
        {
            LastFedTime = DateTime.Now;
            TimeBecameHungry = null;
            OnPropertyChanged(nameof(CurrentIconPath));
        }

        public void CheckHunger(TextBlock detailsTextBlock)
        {
            if (IsHungry)
            {
                if (TimeBecameHungry == null)
                {
                    TimeBecameHungry = DateTime.Now;
                    detailsTextBlock.Inlines.Add(new Run($"{Name} is hungry! It will be dead in 30 seconds!")
                    {
                        Foreground = Brushes.Orange
                    });
                    detailsTextBlock.Inlines.Add(new LineBreak());
                    OnPropertyChanged(nameof(CurrentIconPath));
                }
            }
            else
            {
                if (TimeBecameHungry != null)
                {
                    TimeBecameHungry = null;
                    OnPropertyChanged(nameof(CurrentIconPath));
                }
            }
        }

        public void Die(TextBlock detailsTextBlock)
        {
            detailsTextBlock.Inlines.Add(new Run($"{Name} has died from hunger!")
            {
                Foreground = Brushes.Red
            });
            detailsTextBlock.Inlines.Add(new LineBreak());
        }

        public override string ToString()
        {
            return $"{Name}.{GetType().Name}";
        }

        public abstract string MakeSound();
        public abstract string ProduceProduct();
    }
}
