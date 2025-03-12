using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;

namespace Controllers;

[ApiController]
[Route("produto")]
public class ProdutoController : ControllerBase
{
    private readonly IProdutoService _produtoService;
    public ProdutoController(IProdutoService produtoService)
    {
        _produtoService = produtoService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProdutoDTO>>> GetProdutos()
    {
        var produtos = await _produtoService.GetAllProdutos();
        return Ok(produtos);
    }

    [HttpGet("description/{description}")]
    public async Task<ActionResult<ProdutoData>> GetProdutoByDescription(string description)
    {
        var produto = await _produtoService.GetProdutoByDescription(description);
        if(produto is null)
            return NotFound();

        return Ok(produto);
    }

    [HttpPost]
    public async Task<ActionResult<ProdutoDTO>> CreateProduto(ProdutoDTO produtoDTO)
    {
        var produtoData = new ProdutoData
        {
            dscProduto = produtoDTO.dscProduto,
            vlrUnitario = produtoDTO.vlrUnitario
        };
        var produtoCriado = await _produtoService.CreateProduto(produtoDTO);
        return CreatedAtAction(nameof(GetProdutoByDescription), new { description = produtoCriado.dscProduto }, produtoCriado);
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
