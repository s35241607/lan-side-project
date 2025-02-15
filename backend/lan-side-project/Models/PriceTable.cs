namespace lan_side_project.Models;

public class PriceTable : AuditBase
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<PriceTableItem> PriceTableItems { get; set; } = [];
}
