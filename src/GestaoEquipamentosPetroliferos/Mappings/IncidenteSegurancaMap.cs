namespace GestaoEquipamentosPetroliferos.Mappings;

public class IncidenteSegurancaMap : IEntityTypeConfiguration<IncidenteSeguranca>
{
    public void Configure(EntityTypeBuilder<IncidenteSeguranca> builder)
    {
        builder.HasKey(is => is.Id);

        // Propriedades
        builder.Property(is => is.Id)
               .IsRequired()
               .ValueGeneratedNever();  // Chave autoincrementável (não gerada automaticamente)


        // Relacionamentos com outras entidades
        builder.HasOne(is => is.Equipamento)
               .WithMany()
               .HasForeignKey(is => is.EquipamentoId)
               .IsRequired();

        // .OnDelete(DeleteBehavior.Cascade);  // Apaga as entradas na tabela de junção ao deletar Missao
    }
}