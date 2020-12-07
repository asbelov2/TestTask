using System;
using System.Windows;

namespace TestTask
{
  /// <summary>
  /// Логика взаимодействия для EditWindow.xaml
  /// </summary>
  public partial class EditWindow : Window
  {
    private bool isNewAsset = true;
    private Asset asset;

    public EditWindow(Asset asset = null)
    {
      this.asset = asset;
      InitializeComponent();
      if (asset != null)
      {
        this.isNewAsset = false;
        this.NameTextBox.Text = asset.Name;
        if (asset is MoneyAsset)
        {
          var tmpAsset = asset as MoneyAsset;
          this.MoneyRadio.IsChecked = true;
          this.Non_monetary.Visibility = Visibility.Collapsed;
          this.Non_monetary.IsEnabled = false;
          this.BankInfo.Visibility = Visibility.Visible;
          this.BankInfo.IsEnabled = true;
          this.Money.Visibility = Visibility.Visible;
          this.Money.IsEnabled = true;
          this.Non_monetaryRadio.IsChecked = false;
          if (tmpAsset.Bank != string.Empty)
          {
            this.BankCheckbox.IsChecked = true;
            this.BankTextBox.Text = tmpAsset.Bank;
          }
          else
          {
            this.BankCheckbox.IsChecked = false;
          }
          if (tmpAsset.BankAccountNumber != string.Empty)
          {
            this.BankAccountNumberTextBox.Text = tmpAsset.BankAccountNumber;
          }
          this.ValueTextBox.Text = tmpAsset.Money.Value.ToString();
          this.CurrencyTextBox.Text = tmpAsset.Money.Currency;
        }
        else if (asset is NonMonetaryAsset)
        {
          var tmpAsset = asset as NonMonetaryAsset;
          this.Non_monetaryRadio.IsChecked = true;
          this.BankInfo.Visibility = Visibility.Collapsed;
          this.BankInfo.IsEnabled = false;
          this.Money.Visibility = Visibility.Collapsed;
          this.Money.IsEnabled = false;
          this.Non_monetary.Visibility = Visibility.Visible;
          this.Non_monetary.IsEnabled = true;
          this.MoneyRadio.IsChecked = false;
          if (tmpAsset.Amount != null)
          {
            this.AmountCheckbox.IsChecked = true;
            this.AmountTextBox.Text = tmpAsset.Amount.ToString();
          }
          else
          {
            this.AmountCheckbox.IsChecked = false;
          }
          if (tmpAsset.UnitName != string.Empty)
          {
            this.UnitNameTextBox.Text = tmpAsset.UnitName;
          }
          this.BookValueTextBox.Text = tmpAsset.BookValue.Value.ToString();
          this.BookCurrencyTextBox.Text = tmpAsset.BookValue.Currency;
          this.ResidualBookValueTextBox.Text = tmpAsset.ResidualBookValue.Value.ToString();
          this.ResidualBookCurrencyTextBox.Text = tmpAsset.ResidualBookValue.Currency;
          this.EstimatedValueTextBox.Text = tmpAsset.EstimatedCost.Value.ToString();
          this.EstimatedCurrencyTextBox.Text = tmpAsset.EstimatedCost.Currency;
          if (tmpAsset.ProductionDate != default(DateTime))
          {
            this.ProductionDateCheckbox.IsChecked = true;
            this.ProductionDate.SelectedDate = tmpAsset.ProductionDate;
          }
          else
          {
            this.ProductionDateCheckbox.IsChecked = false;
          }
          if (tmpAsset.InventoryNumber != null)
          {
            this.InventoryNumberCheckbox.IsChecked = true;
            this.InventoryNumberTextBox.Text = tmpAsset.InventoryNumber.ToString();
          }
          else
          {
            this.InventoryNumberCheckbox.IsChecked = false;
          }
          this.AdditionalInfoTextBox.Text = tmpAsset.AdditionalInfo;
        }
      }
      else
      {
        isNewAsset = true;
      }
    }

    private void MoneyRadio_Checked(object sender, RoutedEventArgs e)
    {
      this.Non_monetary.Visibility = Visibility.Collapsed;
      this.Non_monetary.IsEnabled = false;
      this.BankInfo.Visibility = Visibility.Visible;
      this.BankInfo.IsEnabled = true;
      this.Money.Visibility = Visibility.Visible;
      this.Money.IsEnabled = true;
      this.Non_monetaryRadio.IsChecked = false;
    }

    private void Non_monetaryRadio_Checked(object sender, RoutedEventArgs e)
    {
      this.BankInfo.Visibility = Visibility.Collapsed;
      this.BankInfo.IsEnabled = false;
      this.Money.Visibility = Visibility.Collapsed;
      this.Money.IsEnabled = false;
      this.Non_monetary.Visibility = Visibility.Visible;
      this.Non_monetary.IsEnabled = true;
      this.MoneyRadio.IsChecked = false;
    }

    private void OK_Click(object sender, RoutedEventArgs e)
    {
      var name = this.NameTextBox.Text;
      if (this.MoneyRadio.IsChecked ?? false)
      {
        double value;
        if (!double.TryParse(this.ValueTextBox.Text, out value))
        {
          MessageBox.Show("Некорректное значение суммы денежного актива");
          return;
        }
        var money = new Money(value, this.CurrencyTextBox.Text);

        if (isNewAsset)
        {
          if (this.BankCheckbox.IsChecked ?? false)
          {
            var bank = this.BankTextBox.Text;
            var bankNumber = this.BankAccountNumberTextBox.Text;
            (this.Owner as MainWindow).Assets.Items.Add(new MoneyAsset(name, money, bank, bankNumber));
          }
          else
          {
            (this.Owner as MainWindow).Assets.Items.Add(new MoneyAsset(name, money));
          }
        }
        else
        {
          (this.Owner as MainWindow).Assets.Items.Remove(asset);
          if (this.BankCheckbox.IsChecked ?? false)
          {
            var bank = this.BankTextBox.Text;
            var bankNumber = this.BankAccountNumberTextBox.Text;
            (this.Owner as MainWindow).Assets.Items.Add(new MoneyAsset(name, money, bank, bankNumber));
          }
          else
          {
            (this.Owner as MainWindow).Assets.Items.Add(new MoneyAsset(name, money));
          }
        }
      }
      else if (this.Non_monetaryRadio.IsChecked ?? false)
      {
        double value;
        if (!double.TryParse(this.BookValueTextBox.Text, out value))
        {
          MessageBox.Show("Некорректное значение начальной балансовой стоимости");
          return;
        }
        var bookValue = new Money(value, this.CurrencyTextBox.Text);
        if (!double.TryParse(this.ResidualBookValueTextBox.Text, out value))
        {
          MessageBox.Show("Некорректное значение остаточной балансовой стоимости");
          return;
        }
        var residualBookValue = new Money(value, this.ResidualBookCurrencyTextBox.Text);
        if (!double.TryParse(this.EstimatedValueTextBox.Text, out value))
        {
          MessageBox.Show("Некорректное значение оценочной стоимости");
          return;
        }
        var estimatedCost = new Money(double.Parse(this.EstimatedValueTextBox.Text), this.EstimatedCurrencyTextBox.Text);
        DateTime productionDate;
        if (this.ProductionDateCheckbox.IsChecked ?? false)
        {
          productionDate = this.ProductionDate.SelectedDate.Value;
        }
        else
        {
          productionDate = default(DateTime);
        }
        int? inventoryNumber;
        if (this.InventoryNumberCheckbox.IsChecked ?? false)
        {
          int intValue;
          if (!int.TryParse(this.InventoryNumberTextBox.Text, out intValue))
          {
            MessageBox.Show("Некорректное значение инвентарного номера");
            return;
          }
          inventoryNumber = intValue;
        }
        else
        {
          inventoryNumber = null;
        }

        int? amount;
        string unitName;
        if (this.AmountCheckbox.IsChecked ?? false)
        {
          int intValue;
          if (!int.TryParse(this.AmountTextBox.Text, out intValue))
          {
            MessageBox.Show("Некорректное значение инвентарного номера");
            return;
          }
          amount = intValue;
          unitName = this.UnitNameTextBox.Text;
        }
        else
        {
          amount = null;
          unitName = "";
        }
        if (isNewAsset)
        {
          (this.Owner as MainWindow).Assets.Items.Add(new NonMonetaryAsset(this.NameTextBox.Text,
                                                    bookValue,
                                                    residualBookValue,
                                                    estimatedCost,
                                                    productionDate,
                                                    inventoryNumber,
                                                    amount,
                                                    unitName,
                                                    this.AdditionalInfoTextBox.Text
                                                    ));
        }
        else
        {
          (this.Owner as MainWindow).Assets.Items.Remove(asset);
          (this.Owner as MainWindow).Assets.Items.Add(new NonMonetaryAsset(this.NameTextBox.Text,
                                                    bookValue,
                                                    residualBookValue,
                                                    estimatedCost,
                                                    productionDate,
                                                    inventoryNumber,
                                                    amount,
                                                    unitName,
                                                    this.AdditionalInfoTextBox.Text
                                                    ));
        }
      }
      this.Close();
    }

    private void Cancel_Click(object sender, RoutedEventArgs e)
    {
      this.Close();
    }

    private void BankCheckbox_Checked(object sender, RoutedEventArgs e)
    {
      this.BankInfoArea.IsEnabled = true;
    }

    private void BankCheckbox_Unchecked(object sender, RoutedEventArgs e)
    {
      this.BankInfoArea.IsEnabled = false;
    }

    private void AmountCheckbox_Checked(object sender, RoutedEventArgs e)
    {
      this.AmountArea.IsEnabled = true;
    }

    private void AmountCheckbox_Unchecked(object sender, RoutedEventArgs e)
    {
      this.AmountArea.IsEnabled = false;
    }

    private void ProductionDateCheckbox_Checked(object sender, RoutedEventArgs e)
    {
      this.ProductionDateArea.IsEnabled = true;
    }

    private void ProductionDateCheckbox_Unchecked(object sender, RoutedEventArgs e)
    {
      this.ProductionDateArea.IsEnabled = false;
    }

    private void InventoryNumberCheckbox_Checked(object sender, RoutedEventArgs e)
    {
      this.InventoryNumberArea.IsEnabled = true;
    }

    private void InventoryNumberCheckbox_Unchecked(object sender, RoutedEventArgs e)
    {
      this.InventoryNumberArea.IsEnabled = false;
    }
  }
}