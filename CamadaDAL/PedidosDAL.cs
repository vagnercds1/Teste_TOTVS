using CamadaModel;
using Dapper;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CamadaDAL
{
    public class PedidosDAL
    {
        public int RetornaNumeroPedido()
        {
            int retorno = 1;
            try
            {
                using (IDbConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                {
                    
                    if (db.State == ConnectionState.Closed)
                    {
                        db.Open();
                    }

                    string sql = " select max(numeropedido) as numero from pedidos ";


                    string value = db.Query<string>(sql).Single();

                    if (value != null)
                        retorno = Convert.ToInt32(value) + 1;
                } 
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retorno;
        }

        public int GravarPedido(Pedidos  obj)
        {
            int retorno;
            string sqlQuery = "";
            if (obj.Id == 0)
            {
                #region INSERT
                sqlQuery = @"INSERT INTO [dbo].[Pedidos]
                                   ([ClientesId]
                                   ,[UsuariosId]
                                   ,[NumeroPedido]
                                   ,[DataEntrega])
                             VALUES
                                   (@ClientesId,
                                    @UsuariosId,
                                    @NumeroPedido,
                                    @DataEntrega) ";

                try
                {
                    using (SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString))
                    {
                        using (SqlCommand dataCommand = new SqlCommand(sqlQuery, db))
                        {
                            dataCommand.Parameters.AddWithValue("@ClientesId", obj.ClientesId);
                            dataCommand.Parameters.AddWithValue("@UsuariosId", obj.UsuariosId);
                            dataCommand.Parameters.AddWithValue("@NumeroPedido", obj.NumeroPedido);
                            dataCommand.Parameters.AddWithValue("@DataEntrega", obj.DataEntrega);
                             
                            db.Open();
                            retorno = dataCommand.ExecuteNonQuery();


                            string value = db.Query<string>("SELECT @@IDENTITY AS 'Identity'").Single();

                            retorno = Convert.ToInt32( value);
                            db.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                #endregion
            }
            else
            {
                #region UPDATE 
                sqlQuery = @" UPDATE [dbo].[Pedidos]
                                 SET [ClientesId] = @ClientesId,
                                    ,[UsuariosId] = @UsuariosId,
                                    ,[NumeroPedido] = @NumeroPedido,
                                    ,[DataEntrega] = @DataEntrega,
                               WHERE Id = @Id";
                try
                {
                    using (SqlConnection db = new SqlConnection(ConfigurationManager.ConnectionStrings["Context"].ConnectionString))
                    {
                        using (SqlCommand dataCommand = new SqlCommand(sqlQuery, db))
                        {
                            dataCommand.Parameters.AddWithValue("@ClientesId", obj.ClientesId);
                            dataCommand.Parameters.AddWithValue("@UsuariosId", obj.UsuariosId);
                            dataCommand.Parameters.AddWithValue("@NumeroPedido", obj.NumeroPedido);
                            dataCommand.Parameters.AddWithValue("@DataEntrega", obj.DataEntrega);
                            dataCommand.Parameters.AddWithValue("@Id", obj.Id);

                            db.Open();
                            retorno = dataCommand.ExecuteNonQuery();
                            db.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                #endregion
            }
            return retorno;
        }
    }
}
