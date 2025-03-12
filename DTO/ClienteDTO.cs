using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace DTO;

public class ClienteDTO
{
    public string nmCliente { get; set; }
    public string Cidade { get; set; }
}