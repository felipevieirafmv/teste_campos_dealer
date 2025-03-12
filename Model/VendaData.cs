using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model;

public class VendaData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdVenda { get; set; }
    public int idCliente { get; set; }
    public int idProduto { get; set; }
    public int qtdVenda { get; set; }
    public float vlrUnitarioVenda { get; set; }
    public DateTime dthVenda { get; set; }
    public float vlrTotalVenda { get; set; }
}
