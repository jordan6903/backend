namespace MyApi2.Dtos
{
    public class AuthenticationDto
    {
        public bool IsAuthSuccessful { get; set; }

        public string? ErrorMessage { get; set; }

        public string? Token { get; set; }

        public string? User { get; set; }
    }
}
