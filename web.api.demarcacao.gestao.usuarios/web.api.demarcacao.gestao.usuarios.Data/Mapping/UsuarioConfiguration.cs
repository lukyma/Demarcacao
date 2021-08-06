using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web.api.demarcacao.gestao.usuarios.Domain.Entities;

namespace web.api.demarcacao.gestao.usuarios.Data.Mapping
{
    public class UsuarioConfiguration : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("USUARIO");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd()
                .IsRequired();

            builder.Property(o => o.Nome)
                .HasColumnName("NOME")
                .IsRequired();

            builder.Property(o => o.Login)
                .HasColumnName("LOGIN")
                .IsRequired();

            builder.Property(o => o.Password)
                .HasColumnName("PASSWORD")
                .IsRequired();

            builder.HasData(new Usuario(1, "Admin", "admin", "admin123"),
                            new Usuario(2, "Campo", "campo", "campo123"),
                            new Usuario(3, "Cliente", "cliente", "cliente123"));
        }
    }
}
