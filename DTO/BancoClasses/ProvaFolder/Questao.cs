namespace DTO.BancoClasses.ProvaFolder
{
    public class Questao
    {
        public int Id { get; set; }
        public string Nome { get; set; }    
        public string Descricao { get; set; }
        public QuestaoOpcao OpcaoSelecionada { get; set; }
        public QuestaoOpcao OpcaoCorreta { get; set; }
        public string DescricaoOpcao1 { get; set; } 
        public string DescricaoOpcao2 { get; set; }
        public string DescricaoOpcao3 { get; set; }
        public string DescricaoOpcao4 { get; set; }
        public string DescricaoOpcao5 { get; set; }
        public Prova Prova { get; set; }
    }
}
