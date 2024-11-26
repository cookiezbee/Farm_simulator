using System.Windows.Controls;

namespace Farm_simulator
{
    public interface IPlant
    {
        string Name { get; }
        string GrownIconPath { get; }
        int Cost { get; set; }
        int GrowthStage { get; set; }
        string TooltipText { get; }
        void Grow(TextBlock detailsTextBlock);
        void Harvest(TextBlock detailsTextBlock);
    }
}
