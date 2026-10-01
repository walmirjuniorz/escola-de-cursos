using EscolaDeCursos.WebApp.Modulos.ModuloTurma.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class TurmaConfiguration : IEntityTypeConfiguration<Turma>
{
    public void Configure(EntityTypeBuilder<Turma> builder)
    {
        builder.ToTable("TBTurmas");

        builder.HasKey(t => t.Id);
        builder.Property(t => t.Id).ValueGeneratedNever();

        builder.Property(t => t.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(t => t.NumeroMaximoAlunos)
            .IsRequired();

        builder.Property(t => t.DataInicio)
            .HasColumnType("date")
            .IsRequired();

        builder.Property(t => t.DataTermino)
            .HasColumnType("date")
            .IsRequired();

        builder.HasOne(t => t.Curso)
            .WithMany(c => c.Turmas)
            .HasForeignKey(t => t.CursoId)
            .HasConstraintName("FK_TBTurmas_TBCursos")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Instrutor)
            .WithMany(c => c.Turmas)
            .HasForeignKey(t => t.InstrutorId)
            .HasConstraintName("FK_TBTurmas_TBInstrutores")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(t => t.Matriculas)
            .WithOne(m => m.Turma)
            .HasForeignKey(m => m.TurmaId)
            .HasConstraintName("FK_TBMatriculas_TBTurmas")
            .OnDelete(DeleteBehavior.Restrict);
    }
}
