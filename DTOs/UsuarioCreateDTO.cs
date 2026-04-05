namespace ApiDotNet.DTOs
{
    public class UsuarioCreateDTO
    {
        public string Nome { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Role { get; set; } = "User";
        public string Senha { get; set; }
    }
}