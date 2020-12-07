using System;
using System.Windows;

namespace TestTask
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      MoneyAsset asset1 = new MoneyAsset("Счёт в банке", new Money(1000, "Рубелй"), "ЕвроВорБанк", "5");
      MoneyAsset asset3 = new MoneyAsset("Счёт в банке", new Money(5, "Долларов"), "Внешторгабке", "3");
      MoneyAsset asset4 = new MoneyAsset("В кассе", new Money(100, "Рубелй"));
      MoneyAsset asset5 = new MoneyAsset("В кассе", new Money(3000, "Рублей(в талонах на бензин от Аспека)"));
      NonMonetaryAsset asset2 = new NonMonetaryAsset("Торговое здание по адресу Бассейная-6", new Money(30000, "Рублей"), new Money(5000, "Рублей"), new Money(1000000, "Рублей"), new DateTime(1970, 1, 1), 7);
      NonMonetaryAsset asset6 = new NonMonetaryAsset("Гвозди", new Money(1000, "Рублей"), new Money(100, "Рублей"), new Money(2000, "Рублей"), new DateTime(2000, 1, 1), amount: 100, unitName: "килограммов");
      this.Assets.Items.Add(asset1);
      this.Assets.Items.Add(asset2);
      this.Assets.Items.Add(asset3);
      this.Assets.Items.Add(asset4);
      this.Assets.Items.Add(asset5);
      this.Assets.Items.Add(asset6);
    }

    private void AddMenuItem_Click(object sender, RoutedEventArgs e)
    {
      EditWindow addAsset = new EditWindow();
      addAsset.Owner = this;
      addAsset.ShowDialog();
    }

    private void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
    {
      this.Assets.Items.Remove(Assets.SelectedItem);
    }

    private void EditMenuItem_Click(object sender, RoutedEventArgs e)
    {
      EditWindow editAsset = new EditWindow(this.Assets.SelectedItem as Asset);
      editAsset.Owner = this;
      editAsset.ShowDialog();
    }
  }
}