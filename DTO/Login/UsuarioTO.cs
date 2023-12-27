using DTO.BancoClasses.Login;
using System;

namespace DTO.Login
{
    public class UsuarioTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoginDate { get; set; }
        public DateTime? CreatedLoginDate{ get; set; }
        public ProvaTO ProvaTO { get; set; }

        public static explicit operator UsuarioTO(Usuario to)
        {
            return new UsuarioTO
            {
                Id = to.Id,
                Username = to.Username,
                Password = to.Password,
                LastLoginDate = to.LastLoginDate,
                CreatedLoginDate = to.CreatedAccountDate,
            };
        }
    }
}
