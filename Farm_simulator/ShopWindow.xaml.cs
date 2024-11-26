using System.Windows;

namespace Farm_simulator
{
    public partial class ShopWindow : Window
    {
        public ShopWindow()
        {
            InitializeComponent();
        }

        private void SellEggs_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Owner;

            if (int.TryParse(EggCountTextBox.Text, out int eggsToSell) && eggsToSell > 0)
            {
                if (int.TryParse(mainWindow.EggCountTextBlock.Text, out int eggCount) && eggCount >= eggsToSell)
                {
                    int eggPrice = 10;
                    eggCount -= eggsToSell;
                    mainWindow.Money += eggsToSell * eggPrice;

                    mainWindow.EggCountTextBlock.Text = eggCount.ToString();
                    mainWindow.MoneyCountTextBlock.Text = mainWindow.Money.ToString();

                    MessageBox.Show($"Sold {eggsToSell} eggs for {eggsToSell * eggPrice}$!");
                }
                else
                {
                    MessageBox.Show("Not enough eggs to sell!");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number of eggs to sell!");
            }
        }

        private void SellMilk_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Owner;

            if (int.TryParse(MilkCountTextBox.Text, out int milkToSell) && milkToSell > 0)
            {
                if (int.TryParse(mainWindow.MilkCountTextBlock.Text, out int milkCount) && milkCount >= milkToSell)
                {
                    int milkPrice = 20;
                    milkCount -= milkToSell;
                    mainWindow.Money += milkToSell * milkPrice;

                    mainWindow.MilkCountTextBlock.Text = milkCount.ToString();
                    mainWindow.MoneyCountTextBlock.Text = mainWindow.Money.ToString();

                    MessageBox.Show($"Sold {milkToSell} milk for {milkToSell * milkPrice}$!");
                }
                else
                {
                    MessageBox.Show("Not enough milk to sell!");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number of milk to sell!");
            }
        }

        private void SellWool_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Owner;

            if (int.TryParse(WoolCountTextBox.Text, out int woolToSell) && woolToSell > 0)
            {
                if (int.TryParse(mainWindow.WoolCountTextBlock.Text, out int woolCount) && woolCount >= woolToSell)
                {
                    int woolPrice = 30;
                    woolCount -= woolToSell;
                    mainWindow.Money += woolToSell * woolPrice;

                    mainWindow.WoolCountTextBlock.Text = woolCount.ToString();
                    mainWindow.MoneyCountTextBlock.Text = mainWindow.Money.ToString();

                    MessageBox.Show($"Sold {woolToSell} wool for {woolToSell * woolPrice}$!");
                }
                else
                {
                    MessageBox.Show("Not enough wool to sell!");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number of wool to sell!");
            }
        }

        private void BuyWheat_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Owner;

            if (int.TryParse(WheatBuyTextBox.Text, out int wheatToBuy) && wheatToBuy > 0)
            {
                int wheatPrice = 10;
                int totalCost = wheatToBuy * wheatPrice;

                if (int.TryParse(mainWindow.MoneyCountTextBlock.Text, out int currentMoney) && currentMoney >= totalCost)
                {
                    currentMoney -= totalCost;
                    mainWindow.MoneyCountTextBlock.Text = currentMoney.ToString();

                    if (int.TryParse(mainWindow.WheatCountTextBlock.Text, out int currentWheat))
                    {
                        currentWheat += wheatToBuy;
                        mainWindow.WheatCountTextBlock.Text = currentWheat.ToString();
                    }

                    MessageBox.Show($"Bought {wheatToBuy} wheat for {totalCost}$!");
                }
                else
                {
                    MessageBox.Show("Not enough money to buy wheat!");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number of wheat to buy!");
            }
        }

        private void BuyCorn_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Owner;

            if (int.TryParse(CornBuyTextBox.Text, out int cornToBuy) && cornToBuy > 0)
            {
                int cornPrice = 20;
                int totalCost = cornToBuy * cornPrice;

                if (int.TryParse(mainWindow.MoneyCountTextBlock.Text, out int currentMoney) && currentMoney >= totalCost)
                {
                    currentMoney -= totalCost;
                    mainWindow.MoneyCountTextBlock.Text = currentMoney.ToString();

                    if (int.TryParse(mainWindow.CornCountTextBlock.Text, out int currentCorn))
                    {
                        currentCorn += cornToBuy;
                        mainWindow.CornCountTextBlock.Text = currentCorn.ToString();
                    }

                    MessageBox.Show($"Bought {cornToBuy} corn for {totalCost}$!");
                }
                else
                {
                    MessageBox.Show("Not enough money to buy corn!");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number of corn to buy!");
            }
        }

        private void BuyCarrot_Click(object sender, RoutedEventArgs e)
        {
            var mainWindow = (MainWindow)Owner;

            if (int.TryParse(CarrotBuyTextBox.Text, out int carrotToBuy) && carrotToBuy > 0)
            {
                int carrotPrice = 30;
                int totalCost = carrotToBuy * carrotPrice;

                if (int.TryParse(mainWindow.MoneyCountTextBlock.Text, out int currentMoney) && currentMoney >= totalCost)
                {
                    currentMoney -= totalCost;
                    mainWindow.MoneyCountTextBlock.Text = currentMoney.ToString();

                    if (int.TryParse(mainWindow.CarrotCountTextBlock.Text, out int currentCarrot))
                    {
                        currentCarrot += carrotToBuy;
                        mainWindow.CarrotCountTextBlock.Text = currentCarrot.ToString();
                    }

                    MessageBox.Show($"Bought {carrotToBuy} carrot for {totalCost}$!");
                }
                else
                {
                    MessageBox.Show("Not enough money to buy carrot!");
                }
            }
            else
            {
                MessageBox.Show("Please enter a valid number of carrot to buy!");
            }
        }
    }
}
