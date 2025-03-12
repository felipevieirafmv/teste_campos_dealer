using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using DTO;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;

namespace Controllers;

[ApiController]
[Route("cliente")]
public class ClienteController : ControllerBase
{
    private readonly IClienteService _clienteService;
    public ClienteController(IClienteService clienteService)
    {
        _clienteService = clienteService;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ClienteDTO>>> GetClientes()
    {
        var clientes = await _clienteService.GetAllClientes();
        return Ok(clientes);
    }

    [HttpGet("name/{name}")]
    public async Task<ActionResult<ClienteDTO>> GetClienteByName(string name)
    {
        var cliente = await _clienteService.GetClienteByName(name);
        if(cliente is null)
            return NotFound();

        return Ok(cliente);
    }

    [HttpPost]
    public async Task<ActionResult<ClienteDTO>> CreateCliente(ClienteDTO clienteDTO)
    {
        var clienteData = new ClienteData
        {
            NmCliente = clienteDTO.NmCliente,
            Cidade = clienteDTO.Cidade
        };
        var clienteCriado = await _clienteService.CreateCliente(clienteDTO);
        return CreatedAtAction(nameof(GetClienteByName), new { name = clienteCriado.NmCliente }, clienteCriado);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCliente(int id, ClienteDTO clienteDTO)
    {
        var clienteExistente = await _clienteService.GetClienteById(id);
        if(clienteExistente is null)
            return NotFound();

        var clienteData = new ClienteData
        {
            IdCliente = id,
            NmCliente = clienteDTO.NmCliente,
            Cidade = clienteDTO.Cidade
        };

        var resultado = await _clienteService.UpdateCliente(clienteData);
        if(!resultado)
            return BadRequest();
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCliente(int id)
    {
        var resultado = await _clienteService.DeleteCliente(id);
        if(!resultado)
            return NotFound();
        
        return NoContent();
    }
}
