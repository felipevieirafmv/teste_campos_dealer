using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using Model;

namespace Services;

public interface IVendaService
{
    Task<IEnumerable<VendaData>> GetAllVendas();
    Task<VendaData> GetVendaById(int id);
    Task<IEnumerable<VendaData>> GetVendaByClienteOrProduto(string nmCliente = null, string dscProduto = null);
    Task<VendaDTO> CreateVenda(VendaDTO vendaDTO);
    Task<bool> UpdateVenda(int id, VendaDTO vendaDTO);
    Task<bool> DeleteVenda(int id);
    Task<bool> ImportarVendasExternas();
}
