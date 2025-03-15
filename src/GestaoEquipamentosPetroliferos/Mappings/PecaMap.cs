namespace GestaoEquipamentosPetroliferos.Mappings;

public class PecaMap : IEntityTypeConfiguration<Peca>
{
    public void Configure(EntityTypeBuilder<Peca> builder)
    {
        builder.HasKey(p => p.Id);

        // Propriedades
        builder.Property(p => p.Id)
               .IsRequired()
               .ValueGeneratedNever();  // Chave autoincrementável (não gerada automaticamente)
    }
}