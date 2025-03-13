using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using Model;

namespace Services;

public interface IClienteService
{
    Task<IEnumerable<ClienteData>> GetAllClientes();
    Task<ClienteData> GetClienteByName(string name);
    Task<ClienteData> GetClienteById(int id);
    Task<ClienteDTO> CreateCliente(ClienteDTO clienteDTO);
    Task<bool> UpdateCliente(ClienteData clienteData);
    Task<bool> DeleteCliente(int id);
    Task<bool> ImportarClientesExternos();
}
