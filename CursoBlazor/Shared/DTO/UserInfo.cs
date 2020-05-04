using System.ComponentModel.DataAnnotations;

namespace CursoBlazor.Shared.DTO
{
    public class UserInfo
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required] 
        public string Senha { get; set; }
    }
}
