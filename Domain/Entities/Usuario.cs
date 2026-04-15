namespace ApiDotNet.Domain.Entities
{
    public class Usuario
    {
        public int Id { get; set; }

        public string Senha { get; set; } = string.Empty;
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public string Role { get; set; } = "User";

        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }
    }
}