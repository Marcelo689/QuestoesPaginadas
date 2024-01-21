using DTO;

namespace Web.Models
{
    public class EditarProvaViewModel
    {
        public ProvaTO Prova { get; set; }
        public bool IsTeacher { get; set; }
        public int QuantidadeQuestoesAtualizadas { get; set; } = 0;
    }
}
