using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web.api.demarcacao.gestao.empreendimento.Domain.Entities;

namespace web.api.demarcacao.gestao.empreendimento.Data.Mapping
{
    public class EmpreendimentoConfiguration : IEntityTypeConfiguration<Empreendimento>
    {
        public void Configure(EntityTypeBuilder<Empreendimento> builder)
        {
            builder.ToTable("EMPREENDIMENTO");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(o => o.Descricao)
                .HasColumnName("DESCRICAO")
                .HasMaxLength(60)
                .IsRequired();

            builder.Property(o => o.GrupoEmpresa)
                .HasColumnName("GRUPO_EMPRESA")
                .HasMaxLength(60)
                .IsRequired();

            builder.OwnsOne(emp => emp.Endereco,
            emp =>
            {
                emp.Property(o => o.Logradouro)
                .HasColumnName("ENDERECO_LOGRADOURO")
                .IsRequired();

                emp.Property(o => o.Bairro)
                .HasColumnName("ENDERECO_BAIRRO")
                .IsRequired();

                emp.Property(o => o.Cidade)
                .HasColumnName("ENDERECO_CIDADE")
                .IsRequired();

                emp.Property(o => o.Estado)
                .HasColumnName("ENDERECO_ESTADO")
                .IsRequired();

                emp.Property(o => o.Complemento)
                .HasColumnName("ENDERECO_COMPLEMENTO")
                .IsRequired(false);

                emp.Property(o => o.Numero)
                .HasColumnName("ENDERECO_NUMERO")
                .IsRequired();

                emp.Property(o => o.Cep)
                .HasColumnName("ENDERECO_CEP")
                .IsRequired();

                emp.Property(o => o.Referencia)
               .HasColumnName("ENDERECO_REFERENCIA")
               .IsRequired(false);
            });
        }
    }
}
