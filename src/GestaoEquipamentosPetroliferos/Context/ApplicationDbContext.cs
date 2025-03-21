
namespace GestaoEquipamentosPetroliferos.Context;

public class ApplicationDbContext : DbContext
{
    private readonly IConfiguration _configuration;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IConfiguration configuration)
        : base(options)
    {
        _configuration = configuration;
    }

    public DbSet<Alerta> Alertas { get; set; } = null!;
    public DbSet<Equipamento> Equipamentos { get; set; } = null!;
    public DbSet<HistoricoFalha> HistoricoFalhas { get; set; } = null!;
    public DbSet<IncidenteSeguranca> IncidentesSeguranca { get; set; } = null!;
    public DbSet<Manutencao> Manutencoes { get; set; } = null!;
    public DbSet<Peca> Pecas { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Aplicar configurações de mapeamento
        modelBuilder.ApplyConfiguration(new AlertaMap());
        modelBuilder.ApplyConfiguration(new EquipamentoMap());
        modelBuilder.ApplyConfiguration(new HistoricoFalhaMap());
        modelBuilder.ApplyConfiguration(new IncidenteSegurancaMap());
        modelBuilder.ApplyConfiguration(new ManutencaoMap());
        modelBuilder.ApplyConfiguration(new PecaMap());

        // Filtro automático para só trazer registros ativos
        modelBuilder.Entity<Alerta>().HasQueryFilter(x => x.Ativo);
        modelBuilder.Entity<Equipamento>().HasQueryFilter(x => x.Ativo);
        modelBuilder.Entity<HistoricoFalha>().HasQueryFilter(x => x.Ativo);
        modelBuilder.Entity<IncidenteSeguranca>().HasQueryFilter(x => x.Ativo);
        modelBuilder.Entity<Manutencao>().HasQueryFilter(x => x.Ativo);
        modelBuilder.Entity<Peca>().HasQueryFilter(x => x.Ativo);

        // Para buscar inativos, use .IgnoreQueryFilters()
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("DefaultConnection"));
        }
    }

    public new async Task<bool> SaveChangesAsync(CancellationToken cancellationToken)
    {
        return await base.SaveChangesAsync(cancellationToken) > 0;
    }

    internal async Task SaveChangesAsync()
    {
        throw new NotImplementedException();
    }
}