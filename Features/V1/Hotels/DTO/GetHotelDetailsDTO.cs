namespace HotelListing.Features.V1.Hotels.DTO
{
    public class GetHotelDetailsDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public double Rating { get; set; }
    }
}
