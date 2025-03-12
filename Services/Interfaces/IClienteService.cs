using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Storage;
using Model;

namespace Services;

public interface IClienteService
{
    Task<IEnumerable<ClienteData>> GettAllClientes();
    Task<ClienteData> GetClienteById(int id);
    Task<ClienteData> CreateCliente(ClienteData cliente);
    Task<bool> UpdateCliente(ClienteData cliente);
    Task<bool> DeleteCliente(int id);
}
