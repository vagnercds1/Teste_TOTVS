using CamadaModel;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace WebApplicationTeste
{
    public class ContextoEF : DbContext
    {

        public ContextoEF() : base("ConnectionString"){

            Database.SetInitializer<ContextoEF>(new UniDBInitializer<ContextoEF>());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            base.OnModelCreating(modelBuilder);
        }

        public DbSet<Usuarios> Usuarios { get; set; }

        public DbSet<Produtos> Produtos { get; set; }

        public DbSet<Clientes> Clientes { get; set; }

        public DbSet<Pedidos> Pedidos { get; set; }

        public DbSet<ItensPedidos> ItensPedidos { get; set; } 

    }

    public  class UniDBInitializer<T> : DropCreateDatabaseAlways<ContextoEF>
    {

        protected override void Seed(ContextoEF context)
        {

            #region Seed usuários
            IList<Usuarios> usuarios = new List<Usuarios>();

            usuarios.Add(new Usuarios()
            {
                Nome = "Manual Gomes"
            });

            usuarios.Add(new Usuarios()
            {
               Nome = "maria Emilia"
            });

            usuarios.Add(new Usuarios()
            {
               Nome="Vagner Correa"
            });

            foreach (Usuarios usuario in usuarios)
                context.Usuarios.Add(usuario);
            base.Seed(context);

            #endregion


            #region Seed Produtos
            IList<Produtos> produtos = new List<Produtos>();

            produtos.Add(new Produtos()
            {
                Descricao = "Amaciante concentrado",
                Valor = 10.56
            });

            produtos.Add(new Produtos()
            {
                Descricao = "Sabonete liquido",
                Valor = 5.40
            });

            produtos.Add(new Produtos()
            {
                Descricao = "Detergente Limpol",
                Valor = 2.34
            });

            foreach (Produtos produto in produtos)
                context.Produtos.Add(produto);
            base.Seed(context);

            #endregion



            #region Seed Clientes
            IList<Clientes> clientes = new List<Clientes>();

            clientes.Add(new Clientes()
            {
                CPF= "076.444.240-67",
                Nome = "Batman"
            });

            clientes.Add(new Clientes()
            {
                CPF = "097.685.520-85",
                Nome = "Inri Cristo"
            });

            clientes.Add(new Clientes()
            {
                CPF = "097.685.520-85",
                Nome = "Hommer Simpson"
            });

            foreach (Clientes cliente in clientes)
                context.Clientes.Add(cliente);
            base.Seed(context);

            #endregion 

        }
    }
}
