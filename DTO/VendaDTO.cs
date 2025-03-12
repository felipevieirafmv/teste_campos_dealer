using System;

namespace DTO;

public class VendaDTO
{
    public int idCliente { get; set; }
    public int idProduto { get; set; }
    public int qtdVenda { get; set; }
    public DateTime dthVenda { get; set; }
}