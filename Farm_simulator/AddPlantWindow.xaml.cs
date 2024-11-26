using System.Windows;
using System.Windows.Controls;

namespace Farm_simulator
{
    public partial class AddPlantWindow : Window
    {
        public IPlant NewPlant { get; private set; }

        public AddPlantWindow()
        {
            InitializeComponent();
        }

        private void AddPlantButton_Click(object sender, RoutedEventArgs e)
        {
            string selectedType = (PlantTypeComboBox.SelectedItem as ComboBoxItem)?.Tag?.ToString();
            string name = NameTextBox.Text;

            switch (selectedType)
            {
                case "Wheat":
                    NewPlant = new Wheat(name);
                    break;
                case "Corn":
                    NewPlant = new Corn(name);
                    break;
                case "Carrot":
                    NewPlant = new Carrot(name);
                    break;
                default:
                    MessageBox.Show("Select a valid plant type.");
                    return;
            }
            DialogResult = true;
        }
    }
}
