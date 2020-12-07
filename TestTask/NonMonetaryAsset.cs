using System;

namespace TestTask
{
  internal class NonMonetaryAsset : Asset
  {
    public NonMonetaryAsset(string name,
                             Money bookValue,
                             Money residualBookValue,
                             Money estimatedCost,
                             DateTime productionDate = default(DateTime),
                             int? inventoryNumber = null,
                             int? amount = null,
                             string unitName = "",
                             string additionalInfo = "") : base(name)
    {
      this.BookValue = bookValue;
      this.ResidualBookValue = residualBookValue;
      this.EstimatedCost = estimatedCost;
      this.ProductionDate = productionDate;
      this.InventoryNumber = inventoryNumber;
      this.Amount = amount;
      this.UnitName = unitName;
      this.AdditionalInfo = additionalInfo;
    }

    public override string ToString()
    {
      return string.Concat(
        this.Amount.HasValue ? (this.Amount + " " + this.UnitName + " ") : "",
        this.Name,
        (ProductionDate != default(DateTime)) ? (", Дата производства: " + ProductionDate) : "",
        ", Начальная стоимость:", BookValue, "" +
        ", Остаточная стоимость:", ResidualBookValue,
        ", Оценочная стоимость:", EstimatedCost,
        this.InventoryNumber.HasValue ? (", Инвентарный номер:" + this.InventoryNumber) : "",
        this.AdditionalInfo != string.Empty ? ", Дополнительная информация:" : "",
        this.AdditionalInfo);
    }

    public Money BookValue { get; set; }

    public Money ResidualBookValue { get; set; }

    public Money EstimatedCost { get; set; }

    public DateTime ProductionDate { get; set; }

    public int? InventoryNumber { get; set; }

    public int? Amount { get; set; }

    public string UnitName { get; set; }

    public string AdditionalInfo { get; set; }
  }
}