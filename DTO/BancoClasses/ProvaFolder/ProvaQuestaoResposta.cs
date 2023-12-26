using DTO.BancoClasses.Login.Entidades.EstudanteFolder;
using System;

namespace DTO.BancoClasses.ProvaFolder
{
    public class ProvaQuestaoResposta
    {
        public int Id { get; set; } 
        public Prova Prova { get; set; }
        public Estudante Estudante { get; set; }
        public DateTime DataRespondida { get; set; }    
        public QuestaoOpcao OpcaoSelecionada { get; set; }  
        public Questao Questao { get; set; }   
    }
}
