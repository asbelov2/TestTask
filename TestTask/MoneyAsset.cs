namespace TestTask
{
  public class MoneyAsset : Asset
  {
    public MoneyAsset(string name, Money money, string bank = "", string bankAccountNumber = "") : base(name)
    {
      this.Bank = bank;
      this.BankAccountNumber = bankAccountNumber;
      this.Money = money;
    }

    public override string ToString()
    {
      return string.Concat(this.Name, ", ", Money,
        (Bank != string.Empty) ? ", Банк: " + Bank : ",",
        (Bank != string.Empty && BankAccountNumber != string.Empty) ? " Номер счета: " + BankAccountNumber : "");
    }

    public string Bank { get; set; }

    public string BankAccountNumber { get; set; }

    public Money Money { get; set; }
  }
}