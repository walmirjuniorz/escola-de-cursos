using EscolaDeCursos.WebApp.Modulos.ModuloCurso.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaDeCursos.WebApp.Modulos.ModuloCurso.Infraestrutura;

public sealed class AulaConfiguration : IEntityTypeConfiguration<Aula>
{
    public void Configure(EntityTypeBuilder<Aula> builder)
    {
        builder.ToTable("TBAulas");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedNever();

        builder.Property(c => c.Nome)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(a => a.DuracaoEmMinutos)
            .IsRequired();

        builder.Property(a => a.Ordem)
            .IsRequired();

        builder.Property(a => a.CursoId)
            .IsRequired();

        builder.HasIndex(a => new { a.CursoId, a.Nome })
            .IsUnique();

        builder.HasIndex(a => new { a.CursoId, a.Ordem })
            .IsUnique();
    }
}
