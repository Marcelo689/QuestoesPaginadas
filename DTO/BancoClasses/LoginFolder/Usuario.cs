using System;

namespace DTO.BancoClasses.Login
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime CreatedAccountDate { get; set; }

        public void AtualizaDataLogin()
        {
            LastLoginDate = DateTime.Now;
        }
    }
}
