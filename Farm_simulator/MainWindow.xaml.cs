using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using System.Windows.Threading;

namespace Farm_simulator
{
    public partial class MainWindow : Window
    {
        private Farm farm;
        private DispatcherTimer hungerTimer;
        private DispatcherTimer growTimer;
        private DispatcherTimer productionTimer;
        private DispatcherTimer tooltipUpdateTimer;

        private int money;

        public int Money
        {
            get { return money; }
            set
            {
                money = value;
                MoneyCountTextBlock.Text = money.ToString();
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            farm = new Farm();
            Money = 100;
            UpdateUI();

            hungerTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            hungerTimer.Tick += HungerTimer_Tick;
            hungerTimer.Start();

            growTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            growTimer.Tick += GrowPlants;
            growTimer.Start();

            productionTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            productionTimer.Tick += ProductionTimer_Tick;
            productionTimer.Start();

            tooltipUpdateTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            tooltipUpdateTimer.Tick += (s, e) =>
            {
                foreach (var animal in farm.Animals)
                {
                    if (animal is Animal concreteAnimal)
                    {
                        concreteAnimal.OnPropertyChanged(nameof(concreteAnimal.TooltipText));
                    }
                }

                foreach (var plant in farm.Plants)
                {
                    if (plant is Plant concretePlant)
                    {
                        concretePlant.OnPropertyChanged(nameof(concretePlant.TooltipText));
                    }
                }
            };
            tooltipUpdateTimer.Start();
        }

        private void HungerTimer_Tick(object sender, EventArgs e)
        {
            for (int i = farm.Animals.Count - 1; i >= 0; i--)
            {
                var animal = farm.Animals[i];

                animal.CheckHunger(DetailsTextBlock);

                if (animal.TimeBecameHungry.HasValue && (DateTime.Now - animal.TimeBecameHungry.Value) > TimeSpan.FromSeconds(30))
                {
                    animal.Die(DetailsTextBlock);
                    farm.Animals.RemoveAt(i);
                }
            }
        }

        private void GrowPlants(object sender, EventArgs e)
        {
            foreach (var item in PlantsListBox.Items)
            {
                if (item is Plant plant)
                {
                    plant.Grow(DetailsTextBlock);
                }
            }
        }

        private void ProductionTimer_Tick(object sender, EventArgs e)
        {
            foreach (var animal in farm.Animals)
            {
                if ((DateTime.Now - animal.LastProductTime).TotalSeconds >= animal.ProductionInterval.TotalSeconds)
                {
                    string product = animal.ProduceProduct();
                    AddProductToUI(product);
                    animal.LastProductTime = DateTime.Now;
                }
            }
        }

        private void BuyAnimal(IAnimal animal)
        {
            if (Money >= animal.Cost)
            {
                Money -= animal.Cost;
                farm.AddAnimal(animal);
                UpdateUI();
                AddMessageToDetails($"You have bought a {animal.Name}!", Brushes.Black);
            }
            else
            {
                AddMessageToDetails("Not enough money to buy this animal.", Brushes.Red);
            }
        }

        private void BuyPlant(IPlant plant)
        {
            if (Money >= plant.Cost)
            {
                Money -= plant.Cost;
                farm.AddPlant(plant);
                UpdateUI();
                AddMessageToDetails($"You have bought a {plant.Name}!", Brushes.Black);
            }
            else
            {
                AddMessageToDetails("Not enough money to buy this plant.", Brushes.Red);
            }
        }

        public void AddMessageToDetails(string message, Brush color)
        {
            DetailsTextBlock.Inlines.Add(new Run(message)
            {
                Foreground = color
            });
            DetailsTextBlock.Inlines.Add(new LineBreak());
        }

        private void UpdateUI()
        {
            AnimalsListBox.ItemsSource = null;
            AnimalsListBox.ItemsSource = farm.Animals;

            PlantsListBox.ItemsSource = null;
            PlantsListBox.ItemsSource = farm.Plants;
        }

        private void FeedAnimal_Click(object sender, RoutedEventArgs e)
        {
            if (AnimalsListBox.SelectedItem is Animal selectedAnimal)
            {
                int availableCount = 0;

                if (selectedAnimal.PreferredFood == "Wheat")
                {
                    availableCount = int.TryParse(WheatCountTextBlock.Text, out var wheatCount) ? wheatCount : 0;
                }
                else if (selectedAnimal.PreferredFood == "Corn")
                {
                    availableCount = int.TryParse(CornCountTextBlock.Text, out var cornCount) ? cornCount : 0;
                }
                else if (selectedAnimal.PreferredFood == "Carrot")
                {
                    availableCount = int.TryParse(CarrotCountTextBlock.Text, out var carrotCount) ? carrotCount : 0;
                }

                if (availableCount > 0)
                {
                    selectedAnimal.Feed();

                    if (selectedAnimal.PreferredFood == "Wheat")
                    {
                        WheatCountTextBlock.Text = (availableCount - 1).ToString();
                    }

                    else if (selectedAnimal.PreferredFood == "Corn")
                    {
                        CornCountTextBlock.Text = (availableCount - 1).ToString();
                    }

                    else if (selectedAnimal.PreferredFood == "Carrot")
                    {
                        CarrotCountTextBlock.Text = (availableCount - 1).ToString();
                    }

                    AddMessageToDetails($"{selectedAnimal.Name} has been fed with {selectedAnimal.PreferredFood}.", Brushes.Black);
                }
                else
                {
                    AddMessageToDetails($"Not enough {selectedAnimal.PreferredFood} to feed {selectedAnimal.Name}.", Brushes.Red);
                }
            }
            else
            {
                AddMessageToDetails("No animal selected.", Brushes.Red);
            }
        }

        private void HarvestPlant_Click(object sender, RoutedEventArgs e)
        {
            if (PlantsListBox.SelectedItem is Plant selectedPlant)
            {
                if (selectedPlant.GrowthStage == 2)
                {
                    selectedPlant.Harvest(DetailsTextBlock);
                    UpdatePlantCounts(selectedPlant.GetType().Name);
                    farm.Plants.Remove(selectedPlant);
                    UpdateUI();
                }
                else
                {
                    AddMessageToDetails($"{selectedPlant.Name} is not ready for harvest yet.", Brushes.Red);
                }
            }
            else
            {
                AddMessageToDetails("No plant selected to harvest.", Brushes.Red);
            }
        }

        private void UpdatePlantCounts(string plantType)
        {
            switch (plantType)
            {
                case "Wheat":
                    WheatCountTextBlock.Text = (int.Parse(WheatCountTextBlock.Text) + 1).ToString();
                    break;
                case "Corn":
                    CornCountTextBlock.Text = (int.Parse(CornCountTextBlock.Text) + 1).ToString();
                    break;
                case "Carrot":
                    CarrotCountTextBlock.Text = (int.Parse(CarrotCountTextBlock.Text) + 1).ToString();
                    break;
            }
        }

        private void AddProductToUI(string product)
        {
            if (product.Contains("egg"))
            {
                EggCountTextBlock.Text = (int.Parse(EggCountTextBlock.Text) + 1).ToString();
            }
            else if (product.Contains("milk"))
            {
                MilkCountTextBlock.Text = (int.Parse(MilkCountTextBlock.Text) + 1).ToString();
            }
            else if (product.Contains("wool"))
            {
                WoolCountTextBlock.Text = (int.Parse(WoolCountTextBlock.Text) + 1).ToString();
            }
            AddMessageToDetails(product, Brushes.Black);
        }

        private void AddAnimal_Click(object sender, RoutedEventArgs e)
        {
            var addAnimalWindow = new AddAnimalWindow();
            if (addAnimalWindow.ShowDialog() == true)
            {
                BuyAnimal(addAnimalWindow.NewAnimal);
            }
        }

        private void AddPlant_Click(object sender, RoutedEventArgs e)
        {
            var addPlantWindow = new AddPlantWindow();
            if (addPlantWindow.ShowDialog() == true)
            {
                BuyPlant(addPlantWindow.NewPlant);
            }
        }

        private void Shop_Click(object sender, RoutedEventArgs e)
        {
            ShopWindow shopWindow = new ShopWindow
            {
                Owner = this
            };
            shopWindow.ShowDialog();
        }

        private void AnimalsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedAnimal = AnimalsListBox.SelectedItem as Animal;

            if (selectedAnimal != null)
            {
                AddMessageToDetails(selectedAnimal.MakeSound(), Brushes.DimGray);
            }
        }
    }
}
