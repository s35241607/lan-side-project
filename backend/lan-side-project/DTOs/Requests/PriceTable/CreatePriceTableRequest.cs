namespace lan_side_project.DTOs.Requests.PriceTable;

public class CreatePriceTableRequest
{
    public string Name { get; set; } = null!;
    public List<CreatePriceTableItemRequest> PriceTableItems { get; set; } = [];
}
