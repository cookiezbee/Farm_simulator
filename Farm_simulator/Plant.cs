using System;
using System.ComponentModel;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Runtime.CompilerServices;

namespace Farm_simulator
{
    public abstract class Plant : IPlant, INotifyPropertyChanged
    {
        public string Name { get; private set; }
        public string GrownIconPath { get; }
        public int Cost { get; set; }
        public int GrowthStage { get; set; } // 0 - не посажено, 1 - растет, 2 - созрело
        private DateTime plantedTime;
        private TimeSpan growthTime;
        private TimeSpan remainingTime;

        public string IconPath
        {
            get
            {
                if (GrowthStage == 1)
                    return "Image/plant_growing.png";
                else if (GrowthStage == 2)
                    return GrownIconPath;
                else
                    return "Image/plant_growing.png";
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string TooltipText
        {
            get
            {
                string growthStatus;

                if (GrowthStage == 0)
                {
                    growthStatus = "Not planted.";
                }

                else if (GrowthStage == 1)
                {
                    growthStatus = "Growing...";
                }

                else if (GrowthStage == 2)
                {
                    growthStatus = "Grown and ready for harvest!";
                }

                else
                {
                    growthStatus = "Unknown";
                }

                return $"Plant: {GetType().Name}\n" +
                       $"Status: {growthStatus}\n" +
                       $"Time to harvest: {(remainingTime > TimeSpan.Zero ? $"{remainingTime.Seconds} s" : "Ready for harvest")}";
            }
        }

        public Plant(string name, string grownIconPath, TimeSpan growthTime)
        {
            Name = name;
            GrownIconPath = grownIconPath;
            this.growthTime = growthTime;
            GrowthStage = 0;
            remainingTime = growthTime;
        }

        public void Grow(TextBlock detailsTextBlock)
        {
            if (GrowthStage == 0)
            {
                plantedTime = DateTime.Now;
                GrowthStage = 1;
                remainingTime = growthTime;
                OnPropertyChanged(nameof(GrowthStage));
                OnPropertyChanged(nameof(IconPath));
            }

            else if (GrowthStage == 1)
            {
                var elapsedTime = DateTime.Now - plantedTime;
                remainingTime = growthTime - elapsedTime;

                if (remainingTime <= TimeSpan.Zero)
                {
                    GrowthStage = 2;
                    remainingTime = TimeSpan.Zero;
                    OnPropertyChanged(nameof(GrowthStage));
                    OnPropertyChanged(nameof(IconPath));

                    detailsTextBlock.Inlines.Add(new Run($"{Name} is ready to be harvested!")
                    {
                        Foreground = Brushes.Green
                    });
                    detailsTextBlock.Inlines.Add(new LineBreak());
                }
            }
        }

        public void Harvest(TextBlock detailsTextBlock)
        {
            if (GrowthStage == 2)
            {
                detailsTextBlock.Inlines.Add(new Run($"{Name} has been harvested!")
                {
                    Foreground = Brushes.Green
                });
                detailsTextBlock.Inlines.Add(new LineBreak());

                GrowthStage = 0;
                remainingTime = growthTime;
                OnPropertyChanged(nameof(GrowthStage));
                OnPropertyChanged(nameof(IconPath));
            }
        }

        public override string ToString()
        {
            return $"{Name}.{GetType().Name}";
        }
    }
}
