using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model;

public class ClienteData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdCliente { get; set; }
    public string nmCliente { get; set; }
    public string Cidade { get; set; }
}
