using ScriptGenerator.dao;
using ScriptGenerator.models;
using ScriptGenerator.services.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ScriptGenerator.services
{
    public class RzInfoFileServices
    {
        private readonly rzDAO _rzDAO;

        public RzInfoFileServices(string db)
        {
            _rzDAO = new rzDAO(db);
        }

        internal Handleresp Handle(string[] idsRz)
        {
            var rzInfo = _rzDAO.GetRZ(idsRz);

            var resp = new Handleresp("", false);
            try
            {
                resp.FilesPath = FileFactory(rzInfo);
                resp.IsSucess = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"cls \n {ex.ToString()}");
                resp.FilesPath = string.Empty;
                resp.IsSucess = false;
            }

            return resp;
        }

        private string FileFactory(IEnumerable<rzinfo> rzInfo)
        {
            StringBuilder fileText = new StringBuilder();
            fileText.Append("/* \n Insert de respostas de sucesso no banco do cliente \n cnpj: <CNPJ> \n nome: <CLIENTE> \n*/ \n");
            foreach (var rz in rzInfo)
            {
                try
                {
                    var valueText = InputSQLFactory(rz);
                    fileText.Append(valueText);
                }
                catch (Exception)
                {
                    fileText.Append("\n");
                }


            }

            string path = @"D:\";

            SaveFile(path, fileText);

            return path;
        }

        private void SaveFile(string path, StringBuilder fileText)
        {
            string FilePath = path + $"InsertSucesso_{DateTime.Now.ToString("dd-MM-yyyy")}.sql";
            if (!File.Exists(FilePath))
            {
                var x = File.CreateText(FilePath);

                x.Write(fileText.ToString());
                x.Close();
            };
        }

        private string InputSQLFactory(rzinfo rz)
        {
            var input = Resource1.ResourceManager.GetString("InputSQL");
            return input.ReplaceInput(FieldsRz.CRZ, rz.ContadorReducaoZ)
                        .ReplaceInput(FieldsRz.Data, rz.dataresposta)
                        .ReplaceInput(FieldsRz.IdRef, rz.IdReferencia)
                        .ReplaceInput(FieldsRz.ProcCod, 1)
                        .ReplaceInput(FieldsRz.ProcDesc, "Sucesso")
                        .ReplaceInput(FieldsRz.Recibo, rz.Recibo)
                        .ReplaceInput(FieldsRz.rzID, rz.Id);
        }
    }
}