namespace Organization_MVC.ViewModels
{
    public class UserViewModelByToken
    {
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? Role { get; set; }
        public string? Check { get; set; }
        public string Token { get; set; }
    }
}
