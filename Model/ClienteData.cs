using System.ComponentModel.DataAnnotations;

namespace Model;

public class ClienteData
{
    [Key]
    public int IdCliente { get; set; }
    public string NmCliente { get; set; }
    public string Cidade { get; set; }
}
