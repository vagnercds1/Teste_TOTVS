using CamadaModel;
using Dapper;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CamadaDAL
{
    public class ItensPedidosDAL
    {
        public string RetornaTotalPorPedido(int pedidoId)
        {
      
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {

                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                    }

                    string sql = "select 'Valor Total: ' + CONVERT(VARCHAR(10), sum(ValorTotal)) + ' | Qtd. Total:' + CONVERT(VARCHAR(10), sum(Quantidade))  from ItensPedidos where PedidosId =" + pedidoId;
                     
                    return db.Query<string>(sql).Single();
                     
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
         
        }
    }
}