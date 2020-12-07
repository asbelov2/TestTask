namespace TestTask
{
  public abstract class Asset
  {
    public Asset(string name)
    {
      this.Name = name;
    }

    public string Name { get; set; }
  }
}