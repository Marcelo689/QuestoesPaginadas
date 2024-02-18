using DTO;

namespace Web.Models
{
    public class EditarProvaViewModel
    {
        public int Id { get; set; }
        public ProvaTO Prova { get; set; }
        public bool IsTeacher { get; set; }
        public int QuantidadeQuestoesAtualizadas { get; set; } = 0;
        public int QuestaoDeleteId { get; set; }
    }
}
