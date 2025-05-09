namespace GordumYedim.API.Models.Dtos
{
    public class RegisterDto
    {
        public int UserId { get; set; }

        public string? Username { get; set; }

        public string? Password { get; set; }

        public string? UserCity { get; set; }

        public string? Email { get; set; }

    }
}
