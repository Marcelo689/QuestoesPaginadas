using System;
using System.Collections.Generic;

namespace ClassesModel
{
    public enum Options
    {
        A = 0,
        B = 1,
        C = 2,
        D = 3,
        E = 4,
    }
    public class QuestaoTO
    {
        public QuestaoTO(List<string> descricoes)
        {
            InsertDescriptions(descricoes);
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public Options? SelectedOption { get; set; }
        public Dictionary<Options, string> OptionDescriptions { get; set; }

        private void InsertDescriptions(List<string> optionsDescriptions)
        {
            bool amountOfDescriptionsDifferThen5 = OptionDescriptions.Count != 5;

            if (amountOfDescriptionsDifferThen5)
                ErrorNecessary5Descriptions();
            else
                AddDescriptions(optionsDescriptions);
        }
        private void AddDescriptions(List<string> optionsDescriptions)
        {
            OptionDescriptions.Add(Options.A, optionsDescriptions[0]);
            OptionDescriptions.Add(Options.B, optionsDescriptions[1]);
            OptionDescriptions.Add(Options.C, optionsDescriptions[2]);
            OptionDescriptions.Add(Options.D, optionsDescriptions[3]);
            OptionDescriptions.Add(Options.E, optionsDescriptions[4]);
        }
        private static void ErrorNecessary5Descriptions()
        {
            throw new ArgumentException("Necessary exacly 5 descriptions");
        }
    }
}