using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

namespace demoprob.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }

        private void ShowProducts_Click(object sender, RoutedEventArgs e)
        {
            var productsWindow = new ProductsWindow();
            productsWindow.Show();
        }
        private void ShowProductMaterials_Click(object sender, RoutedEventArgs e)
        {
            var materialsWindow = new ProductMaterialsWindow();
            materialsWindow.Show();
        }
        private void ShowMaterials_Click(object sender, RoutedEventArgs e)
        {
            var materialsWindow = new ProductMaterialsWindow();
            materialsWindow.Show();
        }

        private void ShowCalculator_Click(object sender, RoutedEventArgs e)
        {
            var calculatorWindow = new MaterialCalculatorWindow();
            calculatorWindow.Show();
        }
    }
}