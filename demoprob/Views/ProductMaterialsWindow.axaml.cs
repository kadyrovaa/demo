using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Interactivity;
using Avalonia.Media;
using demoprob.Context;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System;

namespace demoprob.Views;

public partial class ProductMaterialsWindow : Window
{
    private readonly MyDbContext _context;

    public ProductMaterialsWindow()
    {
        InitializeComponent();
        
        try
        {
            _context = new MyDbContext();
            LoadProductMaterials();
        }
        catch (Exception ex)
        {
            ShowError($"Ошибка подключения к БД: {ex.Message}");
        }
    }

    private void InitializeComponent()
    {
        AvaloniaXamlLoader.Load(this);
    }

    private void LoadProductMaterials()
    {
        try
        {
            var productMaterials = _context.product_materials
                .Include(pm => pm.material)
                .Include(pm => pm.product_articleNavigation)
                .ToList();

            var dataStackPanel = this.FindControl<StackPanel>("DataStackPanel");
            dataStackPanel.Children.Clear();

            // Если нет данных, покажем сообщение
            TextBlock? statusText;
            if (productMaterials.Count == 0)
            {
                var noDataText = new TextBlock
                {
                    Text = "Нет данных в таблице product_materials",
                    FontSize = 14,
                    Foreground = new SolidColorBrush(Colors.Red),
                    HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                    Margin = new Thickness(0, 20, 0, 0)
                };
                dataStackPanel.Children.Add(noDataText);
                
                statusText = this.FindControl<TextBlock>("StatusText");
                statusText.Text = "В таблице нет данных";
                return;
            }

            for (int i = 0; i < productMaterials.Count; i++)
            {
                var pm = productMaterials[i];
                var backgroundColor = i % 2 == 0 ? Colors.White : Colors.LightBlue;

                var rowGrid = new Grid();
                // Создаем 7 колонок как в XAML
                rowGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(50) });
                rowGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
                rowGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(80) });
                rowGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(100) });
                rowGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(200) });
                rowGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });
                rowGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(120) });

                // ID (колонка 0)
                var idText = new TextBlock
                {
                    Text = pm.id.ToString(),
                    Background = new SolidColorBrush(backgroundColor),
                    Padding = new Thickness(5)
                };
                Grid.SetColumn(idText, 0);
                rowGrid.Children.Add(idText);

                // Артикул продукта (колонка 1)
                var articleText = new TextBlock
                {
                    Text = pm.product_article ?? "N/A",
                    Background = new SolidColorBrush(backgroundColor),
                    Padding = new Thickness(5)
                };
                Grid.SetColumn(articleText, 1);
                rowGrid.Children.Add(articleText);

                // ID материала (колонка 2)
                var materialIdText = new TextBlock
                {
                    Text = pm.material_id.ToString(),
                    Background = new SolidColorBrush(backgroundColor),
                    Padding = new Thickness(5)
                };
                Grid.SetColumn(materialIdText, 2);
                rowGrid.Children.Add(materialIdText);

                // Количество (колонка 3)
                var quantityText = new TextBlock
                {
                    Text = pm.material_quantity.ToString("N2"),
                    Background = new SolidColorBrush(backgroundColor),
                    Padding = new Thickness(5)
                };
                Grid.SetColumn(quantityText, 3);
                rowGrid.Children.Add(quantityText);

                // Название материала (колонка 4)
                var materialNameText = new TextBlock
                {
                    Text = pm.material?.material_name ?? "N/A",
                    Background = new SolidColorBrush(backgroundColor),
                    Padding = new Thickness(5),
                    TextWrapping = Avalonia.Media.TextWrapping.Wrap
                };
                Grid.SetColumn(materialNameText, 4);
                rowGrid.Children.Add(materialNameText);

                // Единица измерения (колонка 5)
                var unitText = new TextBlock
                {
                    Text = pm.material?.unit_of_measure ?? "N/A",
                    Background = new SolidColorBrush(backgroundColor),
                    Padding = new Thickness(5)
                };
                Grid.SetColumn(unitText, 5);
                rowGrid.Children.Add(unitText);

                // Цена (колонка 6)
                var costText = new TextBlock
                {
                    Text = (pm.material?.cost_per_unit ?? 0).ToString("N2") + " руб.",
                    Background = new SolidColorBrush(backgroundColor),
                    Padding = new Thickness(5)
                };
                Grid.SetColumn(costText, 6);
                rowGrid.Children.Add(costText);

                dataStackPanel.Children.Add(rowGrid);
            }

            statusText = this.FindControl<TextBlock>("StatusText");
            statusText.Text = $"Загружено записей: {productMaterials.Count}";
            
            this.Title = $"Материалы продукции ({productMaterials.Count} записей)";
        }
        catch (Exception ex)
        {
            ShowError($"Ошибка загрузки данных: {ex.Message}");
        }
    }

    private void ShowError(string message)
    {
        var statusText = this.FindControl<TextBlock>("StatusText");
        statusText.Text = message;
        statusText.Foreground = new SolidColorBrush(Colors.Red);
    }

    private void GoBack_Click(object sender, RoutedEventArgs e)
    {
        this.Close();
    }

    private void ShowCalculator_Click(object sender, RoutedEventArgs e)
    {
        var dialog = new Window()
        {
            Title = "Калькулятор",
            Width = 300,
            Height = 200,
            Content = new TextBlock { 
                Text = "Калькулятор будет здесь", 
                Margin = new Thickness(20),
                HorizontalAlignment = Avalonia.Layout.HorizontalAlignment.Center,
                VerticalAlignment = Avalonia.Layout.VerticalAlignment.Center
            }
        };
        dialog.ShowDialog(this);
    }

    private void Refresh_Click(object sender, RoutedEventArgs e)
    {
        LoadProductMaterials();
    }
}