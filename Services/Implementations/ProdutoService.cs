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

public class ProdutoService : IProdutoService
{
    private readonly TesteCamposDealerDbContext _context;
    private readonly IMapper _mapper;
    private readonly HttpClient _httpClient;

    public ProdutoService(TesteCamposDealerDbContext context, IMapper mapper, HttpClient httpClient)
    {
        _context = context;
        _mapper = mapper;
        _httpClient = httpClient;
    }

    public async Task<IEnumerable<ProdutoData>> GetAllProdutos()
    {
        var produtos = await _context.ProdutoData.ToListAsync();

        return produtos;
    }

    public async Task<ProdutoData> GetProdutoByDescription(string description)
    {
        var produto = await _context.ProdutoData.FirstOrDefaultAsync(p => p.dscProduto.Contains(description));

        if(produto is null)
            return null;
        
        return produto;
    }

    public async Task<ProdutoData> GetProdutoById(int id)
    {
        var produto = await _context.ProdutoData.FindAsync(id);

        if(produto is null)
            return null;
        
        return produto;
    }

    public async Task<ProdutoDTO> CreateProduto(ProdutoDTO produtoDTO)
    {
        var produtoData = _mapper.Map<ProdutoData>(produtoDTO);
        _context.ProdutoData.Add(produtoData);
        await _context.SaveChangesAsync();

        return _mapper.Map<ProdutoDTO>(produtoData);
    }

    public async Task<bool> UpdateProduto(ProdutoData produtoData)
    {
        var produtoExistente = await _context.ProdutoData.FindAsync(produtoData.IdProduto);
        if(produtoExistente is null)
            return false;

        _context.Entry(produtoExistente).CurrentValues.SetValues(produtoData);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> DeleteProduto(int id)
    {
        var produto = await _context.ProdutoData.FindAsync(id);
        if (produto is null)
            return false;
        
        _context.ProdutoData.Remove(produto);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> ImportarProdutosExternos()
    {
        try
        {
            string url = "https://camposdealer.dev/Sites/TesteAPI/produto";
            var response = await _httpClient.GetStringAsync(url);
            response = JsonSerializer.Deserialize<string>(response);
            System.Console.WriteLine(response);
            var produtos = JsonSerializer.Deserialize<List<ProdutoData>>(response);

            System.Console.WriteLine(produtos);

            if(produtos != null && produtos.Any())
            {
                _context.ProdutoData.AddRange(produtos);
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