namespace lan_side_project.DTOs.Requests.PriceTable;

public class CreatePriceTableItemRequest
{
    public string Description { get; set; } = null!;
    public decimal? Price { get; set; }
}