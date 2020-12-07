namespace TestTask
{
  public class Money
  {
    public Money(double value, string currency)
    {
      this.Value = value;
      this.Currency = currency;
    }

    public double Value { get; }

    public string Currency { get; }

    public override string ToString()
    {
      return string.Concat(Value, " ", Currency);
    }
  }
}