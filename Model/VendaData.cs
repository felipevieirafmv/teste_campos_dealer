using System;
using System.ComponentModel.DataAnnotations;

namespace Model;

public class VendaData
{
    [Key]
    public int IdVenda { get; set; }
    public int IdCliente { get; set; }
    public int IdProduto { get; set; }
    public int QtdVenda { get; set; }
    public float VlrUnitarioVenda { get; set; }
    public DateTime DthVenda { get; set; }
    public float VlrTotalVenda { get; set; }
}
