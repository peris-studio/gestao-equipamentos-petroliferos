namespace GestaoEquipamentosPetroliferos.Mappings;

public class AlertaMap : IEntityTypeConfiguration<Alerta>
{
    public void Configure(EntityTypeBuilder<Alerta> builder)
    {
        builder.HasKey(a => a.Id);

        // Propriedades
        builder.Property(a => a.Id)
               .IsRequired()
               .ValueGeneratedNever();  // Chave autoincrementável (não gerada automaticamente)


        // Relacionamentos com outras entidades
        builder.HasOne(a => a.Equipamento)
               .WithMany()
               .HasForeignKey(a => a.EquipamentoId)
               .IsRequired();

        builder.HasOne(a => a.Peca)
               .WithMany()
               .HasForeignKey(a => a.PecaId)
               .IsRequired();

        // builder.Ignore(m => m.MissaoLicencas);
        // .OnDelete(DeleteBehavior.Cascade);  // Apaga as entradas na tabela de junção ao deletar Missao
    }
}