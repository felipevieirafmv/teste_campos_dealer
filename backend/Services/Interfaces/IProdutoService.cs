using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using Model;

namespace Services;

public interface IProdutoService
{
    Task<IEnumerable<ProdutoData>> GetAllProdutos();
    Task<ProdutoData> GetProdutoByDescription(string description);
    Task<ProdutoData> GetProdutoById(int id);
    Task<ProdutoDTO> CreateProduto(ProdutoDTO produtoDTO);
    Task<bool> UpdateProduto(ProdutoData produtoData);
    Task<bool> DeleteProduto(int id);
    Task<bool> ImportarProdutosExternos();
}
