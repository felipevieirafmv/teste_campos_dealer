using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using AutoMapper;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using Model;
using System.Linq;
using System.Text.Json;

namespace Services;

public class VendaService : IVendaService
{
    private readonly TesteCamposDealerDbContext _context;
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;

    public VendaService(TesteCamposDealerDbContext context, IMapper mapper, HttpClient httpClient)
    {
        _context = context;
        _mapper = mapper;
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<VendaData>> GetAllVendas()
    {
        var vendas = await _context.VendaData.ToListAsync();

        return vendas;
    }

    public async Task<IEnumerable<VendaData>> GetVendaByClienteOrProduto(string nmCliente = null, string dscProduto = null)
    {
        var query = _context.VendaData.AsQueryable();

        if(!string.IsNullOrEmpty(nmCliente))
        {
            var cliente = await _context.ClienteData
                .Where(c => c.nmCliente.Contains(nmCliente))
                .FirstOrDefaultAsync();

            if(cliente is null)
                query = query.Where(v => v.idCliente == cliente.IdCliente);
        }

        if(!string.IsNullOrEmpty(dscProduto))
        {
            var produto = await _context.ProdutoData
                .Where(p => p.dscProduto.Contains(dscProduto))
                .FirstOrDefaultAsync();
            
            if(produto is null)
                query = query.Where(v => v.idProduto == produto.IdProduto);
        }

        return await query.ToListAsync();
    }

    public async Task<VendaData> GetVendaById(int id)
    {
        var venda = await _context.VendaData.FindAsync(id);

        if(venda is null)
            return null;
        
        return venda;
    }

    public async Task<VendaDTO> CreateVenda(VendaDTO vendaDTO)
    {
        var produtoData = await _context.ProdutoData
            .Where(p => p.IdProduto == vendaDTO.idProduto)
            .FirstOrDefaultAsync();
        
        if(produtoData is null)
            return null;
        
        var vlrTotalVenda = vendaDTO.qtdVenda * produtoData.vlrUnitario;

        var vendaData = new VendaData
        {
            idCliente = vendaDTO.idCliente,
            idProduto = vendaDTO.idProduto,
            qtdVenda = vendaDTO.qtdVenda,
            vlrUnitarioVenda = produtoData.vlrUnitario,
            dthVenda = vendaDTO.dthVenda,
            vlrTotalVenda = vlrTotalVenda
        };

        _context.VendaData.Add(vendaData);
        await _context.SaveChangesAsync();

        return _mapper.Map<VendaDTO>(vendaData);
    }

    public async Task<bool> UpdateVenda(int id, VendaDTO vendaDTO)
    {
        var vendaExistente = await _context.VendaData
            .Where(v => v.IdVenda == id)
            .FirstOrDefaultAsync();

        if(vendaExistente is null)
            return false;
        
        var produtoData = await _context.ProdutoData
            .Where(p => p.IdProduto == vendaDTO.idProduto)
            .FirstOrDefaultAsync();
        
        if(produtoData is null)
            return false;
        
        vendaExistente.idCliente = vendaDTO.idCliente;
        vendaExistente.idProduto = vendaDTO.idProduto;
        vendaExistente.qtdVenda = vendaDTO.qtdVenda;
        vendaExistente.dthVenda = vendaDTO.dthVenda;
        vendaExistente.vlrUnitarioVenda = produtoData.vlrUnitario;
        vendaExistente.vlrTotalVenda = vendaDTO.qtdVenda * produtoData.vlrUnitario;

        var registrosAfetados = await _context.SaveChangesAsync();

        return registrosAfetados > 0;
    }

    public async Task<bool> DeleteVenda(int id)
    {
        var venda = await _context.VendaData.FindAsync(id);
        if (venda is null)
            return false;
        
        _context.VendaData.Remove(venda);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ImportarVendasExternas()
    {
        try
        {
            string url = "https://camposdealer.dev/Sites/TesteAPI/venda";
            var response = await _httpClient.GetStringAsync(url);
            response = JsonSerializer.Deserialize<string>(response);
            System.Console.WriteLine(response);
            var vendasExternas = JsonSerializer.Deserialize<List<VendaData>>(response);

            System.Console.WriteLine(vendasExternas);

            if(vendasExternas is null || !vendasExternas.Any())
                return false;

            var vendas = vendasExternas.Select(v => new VendaData
            {
                idCliente = v.idCliente,
                idProduto = v.idProduto,
                qtdVenda = v.qtdVenda,
                vlrUnitarioVenda = v.vlrUnitarioVenda,
                vlrTotalVenda = v.vlrUnitarioVenda * v.qtdVenda,
                dthVenda = v.dthVenda
            }).ToList();

            _context.VendaData.AddRange(vendas);
            await _context.SaveChangesAsync();
            return true;
        }
        catch(Exception ex)
        {
            Console.WriteLine($"Erro: {ex.Message}");
            return false;
        }
    }
}