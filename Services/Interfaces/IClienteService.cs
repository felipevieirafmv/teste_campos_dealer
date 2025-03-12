using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using Model;

namespace Services;

public interface IClienteService
{
    Task<IEnumerable<ClienteDTO>> GetAllClientes();
    Task<ClienteDTO> GetClienteByName(string name);
    Task<ClienteDTO> GetClienteById(int id);
    Task<ClienteDTO> CreateCliente(ClienteDTO clienteDTO);
    Task<bool> UpdateCliente(ClienteData clienteData);
    Task<bool> DeleteCliente(int id);
}
