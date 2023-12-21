namespace DTO.BancoClasses.ProvaFolder
{
    public enum EnumOpcao
    {
        Vazio = 0,
        A = 1,
        B = 2,
        C = 3,
        D = 4,
        E = 5,
    }
    public class QuestaoOpcao
    {
        public int Id { get; set; }
        public EnumOpcao Opcao { get; set; }
    }
}