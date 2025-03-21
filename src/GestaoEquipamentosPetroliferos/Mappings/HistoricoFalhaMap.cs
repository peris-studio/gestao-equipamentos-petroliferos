namespace GestaoEquipamentosPetroliferos.Mappings;

public class HistoricoFalhaMap : IEntityTypeConfiguration<HistoricoFalha>
{
       public void Configure(EntityTypeBuilder<HistoricoFalha> builder)
       {
              builder.HasKey(hf => hf.Id);

              // Propriedades
              builder.Property(hf => hf.Id)
                     .IsRequired()
                     .ValueGeneratedNever();  // Chave autoincrementável (não gerada automaticamente)


              // Relacionamentos com outras entidades
              builder.HasOne(hf => hf.Equipamento)
                     .WithMany()
                     .HasForeignKey(hf => hf.EquipamentoId);
              // .OnDelete(DeleteBehavior.Cascade);  // Apaga as entradas na tabela de junção ao deletar Missao
       }
}