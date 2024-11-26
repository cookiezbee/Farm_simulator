using System;
using System.Windows.Controls;

namespace Farm_simulator
{
    public class Chicken : Animal
    {
        public Chicken(string name) : base(name, "Image/chicken_hungry.png", "Image/chicken_icon.png", TimeSpan.FromSeconds(10), "Wheat", TimeSpan.FromSeconds(30))
        {
            Cost = 30;
        }

        public override string MakeSound()
        {
            return $"{Name} says Cluck!";
        }

        public override string ProduceProduct()
        {
            return $"{Name} produced an egg!";
        }
    }

    public class Cow : Animal
    {
        public Cow(string name) : base(name, "Image/cow_hungry.png", "Image/cow_icon.png", TimeSpan.FromSeconds(20), "Corn", TimeSpan.FromSeconds(60)) 
        {
            Cost = 60;
        }

        public override string MakeSound()
        {
            return $"{Name} says Moo!";
        }

        public override string ProduceProduct()
        {
            return $"{Name} produced milk!";
        }
    }

    public class Sheep : Animal
    {
        public Sheep(string name) : base(name, "Image/sheep_hungry.png", "Image/sheep_icon.png", TimeSpan.FromSeconds(30), "Carrot", TimeSpan.FromSeconds(90))
        {
            Cost = 90;
        }

        public override string MakeSound()
        {
            return $"{Name} says Baa!";
        }

        public override string ProduceProduct()
        {
            return $"{Name} produced wool!";
        }
    }
}
