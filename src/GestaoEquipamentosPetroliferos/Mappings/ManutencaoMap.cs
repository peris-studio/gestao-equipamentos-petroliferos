namespace GestaoEquipamentosPetroliferos.Mappings;

public class ManutencaoMap : IEntityTypeConfiguration<Manutencao>
{
    public void Configure(EntityTypeBuilder<Manutencao> builder)
    {
        builder.HasKey(m => m.Id);

        // Propriedades
        builder.Property(m => m.Id)
               .IsRequired()
               .ValueGeneratedNever();  // Chave autoincrementável (não gerada automaticamente)


        // Relacionamentos com outras entidades
        builder.HasOne(m => m.Equipamento)
               .WithMany()
               .HasForeignKey(m => m.EquipamentoId)
               .IsRequired();

        // .OnDelete(DeleteBehavior.Cascade);  // Apaga as entradas na tabela de junção ao deletar Missao
    }
}