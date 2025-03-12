using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using DTO;
using Microsoft.EntityFrameworkCore;
using System;
using Model;
using System.Linq;
using System.Text.Json;

namespace Services;

public class ClienteService : IClienteService
{
    private readonly TesteCamposDealerDbContext _context;
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;

    public ClienteService(TesteCamposDealerDbContext context, IMapper mapper, HttpClient httpClient)
    {
        _context = context;
        _mapper = mapper;
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ClienteData>> GetAllClientes()
    {
        var clientes = await _context.ClienteData.ToListAsync();

        return clientes;
    }

    public async Task<ClienteData> GetClienteByName(string name)
    {
        var cliente = await _context.ClienteData.FirstOrDefaultAsync(c => c.nmCliente.Contains(name));

        if(cliente is null)
            return null;
        
        return cliente;
    }

    public async Task<ClienteData> GetClienteById(int id)
    {
        var cliente = await _context.ClienteData.FindAsync(id);

        if(cliente is null)
            return null;
        
        return cliente;
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

    public async Task<bool> ImportarClientesExternos()
    {
        try
        {
            string url = "https://camposdealer.dev/Sites/TesteAPI/cliente";
            var response = await _httpClient.GetStringAsync(url);
            response = JsonSerializer.Deserialize<string>(response);
            System.Console.WriteLine(response);
            var clientes = JsonSerializer.Deserialize<List<ClienteData>>(response);

            System.Console.WriteLine(clientes);

            if(clientes != null && clientes.Any())
            {
                _context.ClienteData.AddRange(clientes);
                await _context.SaveChangesAsync();
                return true;
            }

            return false;
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
            return false;
        }
    }
}