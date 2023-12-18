namespace DTO.BancoClasses.Prova
{
    public class Questao
    {
        public int Id { get; set; }
        public string Nome { get; set; }    
        public string Descricao { get; set; }
        public QuestaoOpcao OpcaoCorreta { get; set; }
        public Prova Prova { get; set; }
    }
}
