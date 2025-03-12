using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Model;

namespace Services;

public class ClienteService : IClienteService
{
    private readonly TesteCamposDealerDbContext _context;
    private readonly IMapper _mapper;

    public ClienteService(TesteCamposDealerDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ClienteDTO>> GetAllClientes()
    {
        var clientes = await _context.ClienteData.ToListAsync();

        return _mapper.Map<IEnumerable<ClienteDTO>>(clientes);
    }

    public async Task<ClienteDTO> GetClienteById(int id)
    {
        var cliente = await _context.ClienteData.FindAsync(id);

        if(cliente is null)
            return null;
        
        return _mapper.Map<ClienteDTO>(cliente);
    }

    public async Task<ClienteDTO> CreateCliente(ClienteData cliente)
    {
        _context.ClienteData.Add(cliente);
        await _context.SaveChangesAsync();

        return _mapper.Map<ClienteDTO>(cliente);
    }

    public async Task<bool> UpdateCliente(ClienteData cliente)
    {
        var clienteExistente = await _context.ClienteData.FindAsync(cliente.IdCliente);
        if(clienteExistente is null)
            return false;

        _context.Entry(clienteExistente).CurrentValues.SetValues(cliente);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteCliente(int id)
    {
        var cliente = await _context.ClienteData.FindAsync(id);
        if (cliente is null)
            return false;
        
        _context.ClienteData.Remove(cliente);
        await _context.SaveChangesAsync();
        return true;
    }
}