using System.ComponentModel.DataAnnotations;

namespace Model;

public class ProdutoData
{
    [Key]
    public int IdProduto { get; set; }
    public string DscProduto { get; set; }
    public float VlrUnitario { get; set; }
}
