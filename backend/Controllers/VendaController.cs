using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<IActionResult> UpdateVenda(int id, VendaDTO vendaDTO)
    {
        var resultado = await _vendaService.UpdateVenda(id, vendaDTO);
        if(!resultado)
            return NotFound();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteVenda(int id)
    {
        var resultado = await _vendaService.DeleteVenda(id);
        if(!resultado)
            return NotFound();
        
        return NoContent();
    }

    [HttpPost("importar")]
    public async Task<IActionResult> ImportarVendas()
    {
        var response = await _vendaService.ImportarVendasExternas();
        if(response)
            return Ok();
        
        return BadRequest();
    }
}
