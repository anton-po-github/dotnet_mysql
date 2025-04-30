using System.ComponentModel.DataAnnotations;

public class Product
{
    [Key]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string ProductName { get; set; }
    public string ProductDescription { get; set; }
    public string Discount { get; set; }
    public int Price { get; set; }
}

