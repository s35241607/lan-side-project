using lan_side_project.Data;
using lan_side_project.DTOs.Requests.PriceTable;
using lan_side_project.Models;
using lan_side_project.Utils;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lan_side_project.Controllers;

[ApiController]
[Route("api/v1/price-tables")]
public class PriceTableController(AppDbContext db) :ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<List<PriceTable>>> GetAllAsync()
    {
        var priceTables = await db.PriceTables.Include(o => o.PriceTableItems).ToListAsync();
        return Ok(priceTables);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<PriceTable>> GetByIdAsync(int id)
    {
        var priceTable = await db.PriceTables
            .Include(o => o.PriceTableItems)
            .FirstOrDefaultAsync(f => f.Id == id);

        if (priceTable == null)
        {
            return NotFound();
        }

        return Ok(priceTable);
    }


    [HttpPost]
    public async Task<ActionResult<PriceTable>> CreateAsync(CreatePriceTableRequest request)
    {
        var priceTable = MapperUtils.Mapper.Map<PriceTable>(request);
        await db.PriceTables.AddAsync(priceTable);
        await db.SaveChangesAsync();
        return StatusCode(201, priceTable);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<PriceTable>> UpdateAsync(int id, UpdatePriceTableRequest request)
    {
        var priceTable = await db.PriceTables.AsNoTracking().FirstOrDefaultAsync(f => f.Id == id);

        if (priceTable == null)
        {
            return NotFound();
        }

        MapperUtils.Mapper.Map(request, priceTable);
        
        db.PriceTables.Update(priceTable);
        await db.SaveChangesAsync();

        return Ok(priceTable);
    }
   
}
