using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CamadaModel
{
    public class ItensPedidos
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; } 

        [Required]
        [DisplayName("Quantidade")]
        public int Quantidade { get; set; }

        [Required]
        [DisplayName("Valor Unit.")]
        public double ValorUnit { get; set; }
         
        [Required]
        [DisplayName("Valor Total.")]
        public double ValorTotal { get; set; }
         
        [Required]
        [DisplayName("Pedidos")]
        public int PedidosId { get; set; }

        [Required]
        [DisplayName("Produtos")]
        public int ProdutosId { get; set; }
         
        public virtual Pedidos Pedidos { get; set; }

        public virtual Produtos Produtos { get; set; }
         
    }
}