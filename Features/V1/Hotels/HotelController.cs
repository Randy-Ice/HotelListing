using HotelListing.Data;
using HotelListing.Features.V1.Hotels.DTO;
using Mapster;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.EntityFrameworkCore;

namespace HotelListing.Features.V1.Hotels
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class HotelController : ControllerBase
    {
        private readonly ProjectDB _db;
        public HotelController(ProjectDB db)
        {
            _db = db;
        }
        [HttpGet]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var hotel = await _db.Hotels.ToListAsync();
            return Ok(hotel.Adapt<IEnumerable<GetHotelSimpleDTO>>());
        }
    }
}
