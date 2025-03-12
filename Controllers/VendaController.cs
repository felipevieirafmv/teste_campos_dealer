using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using Model;
using Services;

namespace Controllers;

[ApiController]
[Route("venda")]
public class VendaController : ControllerBase
{
    private readonly IVendaService _vendaService;
    public VendaController(IVendaService vendaService)
    {
        _vendaService = vendaService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<VendaDTO>>> GetVendas()
    {
        var vendas = await _vendaService.GetAllVendas();
        return Ok(vendas);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<IEnumerable<VendaDTO>>> GetVendaById(int id)
    {
        var venda = await _vendaService.GetVendaById(id);
        if(venda is null)
            return NotFound();

        return Ok(venda);
    }

    [HttpGet("search")]
    public async Task<ActionResult<IEnumerable<VendaDTO>>> GetVendaByClienteOrProduto(
        [FromQuery] string? nmCliente,
        [FromQuery] string? dscProduto
    )
    {
        var vendas = await _vendaService.GetVendaByClienteOrProduto(nmCliente, dscProduto);
        if(vendas is null)
            return NotFound();

        return Ok(vendas);
    }

    [HttpPost]
    public async Task<ActionResult<VendaDTO>> CreateVenda(VendaDTO vendaDTO)
    {
        var vendaData = new VendaData
        {
            idCliente = vendaDTO.idCliente,
            idProduto = vendaDTO.idProduto,
            qtdVenda = vendaDTO.qtdVenda,
            dthVenda = DateTime.Now
        };

        var vendaCriada = await _vendaService.CreateVenda(vendaDTO);
        
        return Created("", vendaCriada);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduto(int id, ProdutoDTO produtoDTO)
    {
        var produtoExistente = await _produtoService.GetProdutoById(id);
        if(produtoExistente is null)
            return NotFound();

        var produtoData = new ProdutoData
        {
            IdProduto = id,
            dscProduto = produtoDTO.dscProduto,
            vlrUnitario = produtoDTO.vlrUnitario
        };

        var resultado = await _produtoService.UpdateProduto(produtoData);
        if(!resultado)
            return BadRequest();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduto(int id)
    {
        var resultado = await _produtoService.DeleteProduto(id);
        if(!resultado)
            return NotFound();
        
        return NoContent();
    }

    [HttpPost("importar")]
    public async Task<IActionResult> ImportarProdutos()
    {
        var response = await _produtoService.ImportarProdutosExternos();
        if(response)
            return Ok();
        
        return BadRequest();
    }
}
