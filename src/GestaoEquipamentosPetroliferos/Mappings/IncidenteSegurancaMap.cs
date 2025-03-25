namespace GestaoEquipamentosPetroliferos.Mappings;

public class IncidenteSegurancaMap : IEntityTypeConfiguration<IncidenteSeguranca>
{
    public void Configure(EntityTypeBuilder<IncidenteSeguranca> builder)
    {
        builder.HasKey(i => i.Id);

        // Propriedades
        builder.Property(i => i.Id)
               .IsRequired()
               .ValueGeneratedNever();  // Chave autoincrementável (não gerada automaticamente)


        // Relacionamentos com outras entidades
        builder.HasOne(i => i.Equipamento)
               .WithMany()
               .HasForeignKey(i => i.EquipamentoId);

        // .OnDelete(DeleteBehavior.Cascade);  // Apaga as entradas na tabela de junção ao deletar Missao
    }
}