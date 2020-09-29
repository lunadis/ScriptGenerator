using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptGenerator.models
{
    public class TotalizadorInfo
    {
        //id, ContadorReducaoZ, VendaBruta, TotalizadorGeral

        public int id { get; set; }
        public int ContadorReducaoZ { get; set; }
        public decimal VendaBruta { get; set; }
        public decimal TotalizadorGeral { get; set; }
    }
}
