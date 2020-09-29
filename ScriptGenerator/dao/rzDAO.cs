using Dapper;
using ScriptGenerator.models;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace ScriptGenerator.dao
{
    public class rzDAO
    {
        private readonly string db_;
        public string ConnectionString { get; private set; }

        public rzDAO()
        {
            ConnectionFactory("Master");
        }

        public rzDAO(string db)
        {
            db_ = db;
            ConnectionFactory(db_);
        }

        public IEnumerable<rzinfo> GetRZ(string[] rzIds)
        {
            using (var con = new SqlConnection(ConnectionString))
            {
                var query = @"select ptg.Id, ptg.ContadorReducaoZ, afr.*
                              from tb_pdv_total_geral as ptg
                              LEFT JOIN tb_comercio_Arquivos_Fisco_ReducaoZ as frz on frz.ReducaoID = ptg.Id
                              LEFT JOIN(Select MAX(fr.id) as id_Resposta, MAX(fr.Data) as dataresposta, fr.IdReferencia,
                              				  MAX(fr.SituacaoProcessamentoCodigo) as SituacaoProcessamentoCodigo, fr.Recibo
                                         from tb_comercio_Arquivos_Fisco_Resposta fr
                                         group by fr.IdReferencia, fr.Recibo
                              		   ) as afr on afr.IdReferencia = frz.ID and afr.Recibo = frz.Protocolo
                              where ptg.DataEmissao >= '20200301'
                              AND ptg.isECF = 1
                              AND ptg.NumeroEquipamento = 1
                              and ptg.Id in @Ids
                              ORDER BY
                              ptg.DataEmissao";

                var entity = con.Query<rzinfo>(query, new { Ids = rzIds });
                con.Close();
                return entity;
            }
        }

        public void ConnectionFactory(string db_)
        {
            string connectionBase = @"Password=Morpheus123;Persist Security Info=True;User ID=sa;Initial Catalog=<dbname>;Data Source=.\SQL2017";
            ConnectionString = connectionBase.Replace("<dbname>", db_);
        }
    }
}