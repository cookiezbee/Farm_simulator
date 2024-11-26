using System;

namespace Farm_simulator
{
    public class Wheat : Plant
    {
        public Wheat(string name) : base(name, "Image/wheat_icon.png", TimeSpan.FromSeconds(10)) 
        {
            Cost = 5;
        }
    }

    public class Corn : Plant
    {
        public Corn(string name) : base(name, "Image/corn_icon.png", TimeSpan.FromSeconds(20)) 
        {
            Cost = 15;
        }
    }

    public class Carrot : Plant
    {
        public Carrot(string name) : base(name, "Image/carrot_icon.png", TimeSpan.FromSeconds(30))
        {
            Cost = 25;
        }
    }
}
