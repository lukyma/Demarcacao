using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web.api.demarcacao.gestao.usuarios.Domain.Entities;

namespace web.api.demarcacao.gestao.usuarios.Data.Mapping
{
    public class UsuarioInterfaceConfiguration : IEntityTypeConfiguration<UsuarioInterface>
    {
        public void Configure(EntityTypeBuilder<UsuarioInterface> builder)
        {
            builder.ToTable("USUARIO_INTERFACE");

            builder.HasKey(o => new { o.IdUsuario, o.IdInterface });

            builder.Property(o => o.IdInterface)
                .HasColumnName("ID_INTERFACE")
                .IsRequired();

            builder.Property(o => o.IdUsuario)
                .HasColumnName("ID_USUARIO")
                .IsRequired();

            builder.HasOne(o => o.Interface)
                .WithMany(o => o.UsuarioInterfaces)
                .HasForeignKey(o => o.IdInterface);

            builder.HasOne(o => o.Usuario)
                .WithMany(o => o.UsuarioInterfaces)
                .HasForeignKey(o => o.IdUsuario);

            builder.HasOne(o => o.Interface)
                .WithMany(o => o.UsuarioInterfaces)
                .HasForeignKey(o => o.IdInterface);

            builder.HasData(new UsuarioInterface(1, 1),
                            new UsuarioInterface(1, 2),
                            new UsuarioInterface(1, 3),
                            new UsuarioInterface(1, 4),
                            new UsuarioInterface(1, 5),
                            new UsuarioInterface(1, 6),
                            new UsuarioInterface(1, 7),
                            new UsuarioInterface(1, 8),
                            new UsuarioInterface(2, 4),
                            new UsuarioInterface(2, 5),
                            new UsuarioInterface(2, 6),
                            new UsuarioInterface(2, 7),
                            new UsuarioInterface(2, 8),
                            new UsuarioInterface(3, 4),
                            new UsuarioInterface(3, 8));
        }
    }
}
