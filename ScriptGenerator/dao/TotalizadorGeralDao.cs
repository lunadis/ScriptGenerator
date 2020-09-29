using Dapper;
using ScriptGenerator.models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ScriptGenerator.dao
{
    public class TotalizadorGeralDao
    {
        private readonly string db_;
        public string ConnectionString { get; private set; }

        public TotalizadorGeralDao()
        {
            ConnectionFactory("Master");
        }



        public List<TotalizadorInfo> GetTotalizadorAfterthis(int totalizadorId)
        {
            var sqlComand = @$"select 
	                                id,
	                                ContadorReducaoZ,
	                                VendaBruta,
	                                TotalizadorGeral 
                              from  
                                     tb_pdv_total_geral 
                              WHERE 
                                     IdPdv = 2 
                                     and (id between {totalizadorId} and 
                              		     (Select 
                              		                MAX(id) 
                              		      From  
                              			           tb_pdv_total_geral 
                              		       WHERE 
                              			     	   IdPdv = 2 ))";

            using (var con = new SqlConnection(ConnectionString))
            {
                var entity = con.Query<TotalizadorInfo>(sqlComand);
                con.Close();
                return entity.ToList();
            }
        }

      

        public TotalizadorGeralDao(string db)
        {
            db_ = db;
            ConnectionFactory(db_);
        }


        private void ConnectionFactory(string db)
        {

            string connectionBase = @"Password=Morpheus123;Persist Security Info=True;User ID=sa;Initial Catalog=<dbname>;Data Source=.\SQL2017";
            ConnectionString = connectionBase.Replace("<dbname>", db_);
        }


    }
}
