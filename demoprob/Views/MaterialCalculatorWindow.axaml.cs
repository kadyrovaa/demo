using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using System;

namespace demoprob.Views
{
    public partial class MaterialCalculatorWindow : Window
    {
        private TextBox productTypeIdTextBox;
        private TextBox materialTypeIdTextBox;
        private TextBox productQuantityTextBox;
        private TextBox parameter1TextBox;
        private TextBox parameter2TextBox;
        private TextBox materialStockTextBox;
        private TextBlock resultText;

        public MaterialCalculatorWindow()
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

        protected override void OnOpened(System.EventArgs e)
        {
            base.OnOpened(e);
            
            productTypeIdTextBox = this.Find<TextBox>("ProductTypeId");
            materialTypeIdTextBox = this.Find<TextBox>("MaterialTypeId");
            productQuantityTextBox = this.Find<TextBox>("ProductQuantity");
            parameter1TextBox = this.Find<TextBox>("Parameter1");
            parameter2TextBox = this.Find<TextBox>("Parameter2");
            materialStockTextBox = this.Find<TextBox>("MaterialStock");
            resultText = this.Find<TextBlock>("ResultText");
        }

        private int CalculateRequiredMaterial(
            int productTypeId,
            int materialTypeId,
            int productQuantity,
            double parameter1,
            double parameter2,
            double materialStock)
        {
            if (productTypeId <= 0 || materialTypeId <= 0)
                return -1;

            if (productQuantity <= 0 || parameter1 <= 0 || parameter2 <= 0 || materialStock < 0)
                return -1;

            double productionCoefficient = 1.2;
            double defectPercentage = 5.0;

            double materialPerUnit = parameter1 * parameter2 * productionCoefficient;
            double defectFactor = 1.0 + defectPercentage / 100.0;
            double totalRequiredMaterial = materialPerUnit * productQuantity * defectFactor;
            double additionalMaterialNeeded = totalRequiredMaterial - materialStock;

            if (additionalMaterialNeeded <= 0)
                return 0;

            return (int)Math.Ceiling(additionalMaterialNeeded);
        }

        private void Calculate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int productTypeId = int.Parse(productTypeIdTextBox.Text);
                int materialTypeId = int.Parse(materialTypeIdTextBox.Text);
                int productQuantity = int.Parse(productQuantityTextBox.Text);
                double parameter1 = double.Parse(parameter1TextBox.Text);
                double parameter2 = double.Parse(parameter2TextBox.Text);
                double materialStock = double.Parse(materialStockTextBox.Text);

                int result = CalculateRequiredMaterial(
                    productTypeId,
                    materialTypeId,
                    productQuantity,
                    parameter1,
                    parameter2,
                    materialStock
                );

                if (result == -1)
                {
                    resultText.Text = "Ошибка в данных";
                }
                else
                {
                    resultText.Text = $"Необходимо: {result} единиц";
                }
            }
            catch (Exception)
            {
                resultText.Text = "Ошибка в формате данных";
            }
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}