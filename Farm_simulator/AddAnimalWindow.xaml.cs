using System.Windows;
using System.Windows.Controls;

namespace Farm_simulator
{
    public partial class AddAnimalWindow : Window
    {
        public IAnimal NewAnimal { get; private set; }

        public AddAnimalWindow()
        {
            InitializeComponent();
        }

        private void AddAnimalButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedType = (AnimalTypeComboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString();
            string name = NameTextBox.Text;

            switch (selectedType)
            {
                case "Chicken":
                    NewAnimal = new Chicken(name);
                    break;
                case "Cow":
                    NewAnimal = new Cow(name);
                    break;
                case "Sheep":
                    NewAnimal = new Sheep(name);
                    break;
                default:
                    MessageBox.Show("Select a valid animal type.");
                    return;
            }
            DialogResult = true;
        }
    }
}
