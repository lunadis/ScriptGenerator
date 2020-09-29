using System;

namespace ScriptGenerator.models
{
    public class rzinfo
    {
        public int Id { get; set; }
        public int ContadorReducaoZ { get; set; }
        public int id_Resposta { get; set; }
        public DateTime dataresposta { get; set; }
        public int IdReferencia { get; set; }
        public int SituacaoProcessamentoCodigo { get; set; }
        public string Recibo { get; set; }
    }
}