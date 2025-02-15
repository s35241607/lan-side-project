namespace lan_side_project.DTOs.Requests.PriceTable;

public class UpdatePriceTableItemRequest
{
    public int Id { get; set; }
    public string Description { get; set; } = null!;
    public decimal? Price { get; set; }
}