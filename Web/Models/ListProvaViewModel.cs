using DTO;

namespace Web.Models
{
    public class ListProvaViewModel
    {
        public List<ProvaTO> ListProvaTO { get; internal set; }
        public bool IsTeacher { get; internal set; }
    }
}
