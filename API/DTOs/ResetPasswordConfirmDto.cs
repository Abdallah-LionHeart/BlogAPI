namespace API.DTOs
{
    public class ResetPasswordConfirmDto
    {
        public string Email { get; set; }
        public string Code { get; set; }
        public string NewPassword { get; set; }
    }
}
