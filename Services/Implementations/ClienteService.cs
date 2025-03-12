using System.Collections;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
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

    public async Task<ClienteDTO> GetClienteByName(string name)
    {
        var cliente = await _context.ClienteData.FirstOrDefaultAsync(c => c.NmCliente.Contains(name));

        if(cliente is null)
            return null;
        
        return _mapper.Map<ClienteDTO>(cliente);
    }

    public async Task<ClienteDTO> GetClienteById(int id)
    {
        var cliente = await _context.ClienteData.FindAsync(id);

        if(cliente is null)
            return null;
        
        return _mapper.Map<ClienteDTO>(cliente);
    }

    public async Task<ClienteDTO> CreateCliente(ClienteDTO clienteDTO)
    {
        var clienteData = _mapper.Map<ClienteData>(clienteDTO);
        _context.ClienteData.Add(clienteData);
        await _context.SaveChangesAsync();

        return _mapper.Map<ClienteDTO>(clienteData);
    }

    public async Task<bool> UpdateCliente(ClienteData clienteData)
    {
        var clienteExistente = await _context.ClienteData.FindAsync(clienteData.IdCliente);
        if(clienteExistente is null)
            return false;

        _context.Entry(clienteExistente).CurrentValues.SetValues(clienteData);
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