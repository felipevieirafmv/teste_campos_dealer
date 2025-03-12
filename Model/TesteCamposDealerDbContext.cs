using Microsoft.EntityFrameworkCore;

namespace Model;

public class TesteCamposDealerDbContext : DbContext
{
    public TesteCamposDealerDbContext(DbContextOptions<TesteCamposDealerDbContext> options)
        : base(options) { }
    public DbSet<ClienteData> ClienteData { get; set; }
    public DbSet<ProdutoData> ProdutoData { get; set; }
    public DbSet<VendaData> VendaData { get; set; }
}