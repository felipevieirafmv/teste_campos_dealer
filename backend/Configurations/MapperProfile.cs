using AutoMapper;
using DTO;
using Model;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<ClienteData, ClienteDTO>();
        CreateMap<ClienteDTO, ClienteData>();
        CreateMap<ProdutoData, ProdutoDTO>();
        CreateMap<ProdutoDTO, ProdutoData>();
        CreateMap<VendaData, VendaDTO>();
        CreateMap<VendaDTO, VendaData>();
    }
}