using System.Collections.ObjectModel;

namespace Farm_simulator
{
    public class Farm
    {
        public ObservableCollection<IAnimal> Animals { get; set; }
        public ObservableCollection<IPlant> Plants { get; set; }

        public Farm()
        {
            Animals = new ObservableCollection<IAnimal>();
            Plants = new ObservableCollection<IPlant>();
        }

        public void AddAnimal(IAnimal animal)
        {
            Animals.Add(animal);
        }

        public void AddPlant(IPlant plant)
        {
            Plants.Add(plant);
        }
    }
}
