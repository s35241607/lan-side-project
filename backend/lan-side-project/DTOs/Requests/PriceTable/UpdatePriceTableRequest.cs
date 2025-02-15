namespace lan_side_project.DTOs.Requests.PriceTable;

public class UpdatePriceTableRequest
{
    public string Name { get; set; } = null!;
    public List<UpdatePriceTableItemRequest> PriceTableItems { get; set; } = [];
}
