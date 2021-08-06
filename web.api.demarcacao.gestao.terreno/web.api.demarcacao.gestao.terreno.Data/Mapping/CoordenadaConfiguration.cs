using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using web.api.demarcacao.gestao.terreno.Domain.Entities;

namespace web.api.demarcacao.gestao.terreno.Data.Mapping
{
    public class CoordenadaConfiguration : IEntityTypeConfiguration<Coordenada>
    {
        public void Configure(EntityTypeBuilder<Coordenada> builder)
        {
            builder.ToTable("COORDENADA");

            builder.HasKey(o => o.Id);

            builder.Property(o => o.Id)
                .HasColumnName("ID")
                .ValueGeneratedOnAdd();

            builder.Property(o => o.IdTerreno)
                .HasColumnName("ID_TERRENO")
                .IsRequired();

            builder.Property(o => o.Ordem)
                .HasColumnName("ORDEM")
                .IsRequired();

            builder.Property(o => o.Latitude)
                .HasColumnName("LATITUDE")
                .IsRequired();

            builder.Property(o => o.Longitude)
                .HasColumnName("LONGITUDE")
                .IsRequired();

            builder.HasOne(o => o.Terreno)
                .WithMany(o => o.Coordenadas)
                .HasForeignKey(o => o.IdTerreno)
                .IsRequired();
        }
    }
}
