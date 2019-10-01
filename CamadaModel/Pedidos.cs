using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamadaModel
{

    public class Pedidos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [DisplayName("Clientes")]

        public int ClientesId { get; set; }

        [Required]
        [DisplayName("Usuários")]
        public int UsuariosId { get; set; }

        [DisplayName("Nº pedido")]
        public int NumeroPedido { get; set; }

        [DisplayName("Data de Entrega")]
        public DateTime DataEntrega { get; set; }
         
        public virtual Clientes Clientes { get; set; }
         
        public virtual Usuarios Usuarios { get; set; }
        
        public virtual ICollection<ItensPedidos> ItensPedidos { get; set; }
    }
}