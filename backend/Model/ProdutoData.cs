using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Model;

public class ProdutoData
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IdProduto { get; set; }
    public string dscProduto { get; set; }
    public float vlrUnitario { get; set; }
}
