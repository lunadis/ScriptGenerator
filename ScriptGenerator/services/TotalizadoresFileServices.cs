using ScriptGenerator.dao;
using ScriptGenerator.models;
using ScriptGenerator.services.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptGenerator.services
{
    public class TotalizadoresFileServices
    {
        private readonly TotalizadorGeralDao _DAO;

        public TotalizadoresFileServices(string _db)
        {
            _DAO = new TotalizadorGeralDao(_db);
        }


        public Handleresp Handle(int idTg)
        {

            var lstTotalizadores = _DAO.GetTotalizadorAfterthis(idTg);

            var primeiraTg = lstTotalizadores.FirstOrDefault(x => x.id == idTg);
            lstTotalizadores.Remove(primeiraTg);

            StringBuilder FileText = new StringBuilder();
            StringBuilder ArrayIds = new StringBuilder();


            FileText.Append("Corretor de CRZ \n\n");
            int newCRZ = primeiraTg.ContadorReducaoZ + 1;

            foreach (var item in lstTotalizadores)
            {
                item.ContadorReducaoZ = newCRZ;
                newCRZ++;

                FileText.Append(InputUpdateText(item));
                ArrayIds.Append($",{item.id}");
            }


            try
            {
                FileServices.SaveFile(@"D:\UpdateTG", FileText);
                return new Handleresp { FilesPath = @"D:\", IsSucess = true };

            }
            catch (Exception ex)
            {
                return new Handleresp { FilesPath = ex.Message, IsSucess = false };
            }



        }

        private string InputUpdateText(TotalizadorInfo item)
        {
            var input = Resource1.ResourceManager.GetString("ReviewCRZ");
            return input.ReplaceInput(FieldsTG.CRZ, item.ContadorReducaoZ)
                        .ReplaceInput(FieldsTG.tgID, item.id) + "\n\n";
        }
    }
}
