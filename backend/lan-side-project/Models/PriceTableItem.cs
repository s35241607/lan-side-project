namespace lan_side_project.Models;

public class PriceTableItem
{
    public int Id { get; set; }
    public int PriceTableId { get; set; }

    public string Description { get; set; } = null!;
    public decimal Price { get; set; }

    public PriceTable PriceTable { get; set; } = null!;
}