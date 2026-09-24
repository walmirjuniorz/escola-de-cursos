using EscolaDeCursos.WebApp.Modulos.ModuloAluno.Dominio;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EscolaDeCursos.WebApp.Modulos.ModuloAluno.Infraestrutura;

public sealed class AlunoConfiguration : IEntityTypeConfiguration<Aluno>
{
    public void Configure(EntityTypeBuilder<Aluno> builder)
    {
        builder.ToTable("TBAlunos");

        builder.HasKey(a => a.Id);
        builder.Property(a => a.Id).ValueGeneratedNever();

        builder.Property(a => a.Nome).HasMaxLength(100).IsRequired();

        builder.Property(a => a.Email).HasMaxLength(255).IsRequired();

        builder.Property(a => a.NumeroMatricula).HasMaxLength(20).IsRequired();

        builder.HasIndex(a => a.NumeroMatricula).IsUnique();
    }
}
