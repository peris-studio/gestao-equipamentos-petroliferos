namespace GestaoEquipamentosPetroliferos.Mappings;

public class EquipamentoMap : IEntityTypeConfiguration<Equipamento>
{
    public void Configure(EntityTypeBuilder<Equipamento> builder)
    {
        builder.HasKey(e => e.Id);

        // Propriedades
        builder.Property(e => e.Id)
               .IsRequired()
               .ValueGeneratedNever();  // Chave autoincrementável (não gerada automaticamente)
    }
}