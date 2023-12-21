using BancoProject.Login;
using DTO;
using DTO.BancoClasses.ProvaFolder;

namespace BancoProject.ProvaFolder
{
    public static class ProvaDB
    {
        public static ProvaTO GetProvaDB(int id) { 
            ProvaTO provaTO = PreencheProvaTO(id);
            return provaTO;
        }

        private static ProvaTO PreencheProvaTO(int id)
        {
            var provaTO = new ProvaTO();    
            IQueryable<Questao> listaQuestoesProva = DBInstance.DB.Questao.Where(e => e.Prova.Id == id);
            //AddOpcaoCorretaToProvaTO(listaQuestoesProva);
            List<Questao> questaoTO = listaQuestoesProva.ToList();
            provaTO.Questoes = questaoTO.Select(questao => (QuestaoTO) questao).ToArray();
            return provaTO;
        }

        private static List<Questao> AddOpcaoCorretaToProvaTO(IQueryable<Questao> listaQuestoesProva)
        {
            return listaQuestoesProva.ToList();
            //foreach (var questao in listaQuestoesProva)
            //{
            //    questao.OpcaoCorreta = DBInstance.DB.QuestaoOpcao.FirstOrDefault(e => e.Id == questao.Id);
            //}
        }
    }
}
