using EscolaDeCursos.WebApp.Modulos.ModuloMatricula.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

public sealed class MatriculaConfiguration : IEntityTypeConfiguration<Matricula>
{
    public void Configure(EntityTypeBuilder<Matricula> builder)
    {
        builder.ToTable("TBMatriculas");

        builder.HasKey(m => m.Id);
        builder.Property(m => m.Id).ValueGeneratedNever();

        builder.HasOne(m => m.Aluno)
            .WithMany()
            .HasForeignKey(m => m.AlunoId)
            .HasConstraintName("FK_TBMatriculas_TBAlunos")
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(m => new { m.TurmaId, m.AlunoId })
            .IsUnique();
    }
}
