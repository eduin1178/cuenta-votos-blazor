namespace CuentaVotos.Entities.Account
{
    public class UserRestorePassword
    {
        public int UserId { get; set; }
        public string NewPassword { get; set; } = null!;
    }
}
