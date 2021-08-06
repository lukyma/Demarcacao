using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web.api.demarcacao.gestao.usuarios.Domain.Entities;

namespace web.api.demarcacao.gestao.usuarios.Data.Mapping
{
    public class InterfaceConfiguration : IEntityTypeConfiguration<Interface>
    {
        public void Configure(EntityTypeBuilder<Interface> builder)
        {
            builder.ToTable("INTERFACE");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(o => o.Descricao)
                .HasColumnName("DESCRICAO")
                .IsRequired();

            builder.Property(o => o.Tag)
                .HasColumnName("TAG")
                .IsRequired();

            builder.HasData(new Interface(1, "Cadastro de Empreendimento", "CAD_EMP"),
                            new Interface(2, "Atualização de Empreendimento", "ATUAL_EMP"),
                            new Interface(3, "Exclusão de Empreendimento", "EXC_EMP"),
                            new Interface(4, "Listagem de Empreendimento", "LIST_EMP"),
                            new Interface(5, "Cadastro de Terreno", "CAD_TERR"),
                            new Interface(6, "Atualização de Terreno", "ATUAL_TERR"),
                            new Interface(7, "Exclusão de Terreno", "EXC_TERR"),
                            new Interface(8, "Listagem de Terreno", "LIST_TERR"));
        }
    }
}
