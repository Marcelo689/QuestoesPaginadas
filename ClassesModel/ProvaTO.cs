using System.Collections.Generic;

namespace ClassesModel
{
    public class ProvaTO
    {
        public int Id { get; set; }
        public string Name { get; set; }    
        public string Description { get; set; }
        public List<QuestaoTO> Questoes { get; set; }
    }
}
