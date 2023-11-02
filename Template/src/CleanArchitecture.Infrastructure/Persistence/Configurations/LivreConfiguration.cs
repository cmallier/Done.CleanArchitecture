using CleanArchitecture.Domain.Repertoire;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.Persistence.Configurations;

public class LivreConfiguration : IEntityTypeConfiguration<Livre>
{
    public void Configure( EntityTypeBuilder<Livre> builder )
    {
        // Table
        builder.ToTable( "Livres" );


        // Key
        builder.HasKey( x => x.LivreId );


        // Properties
        builder.Property( x => x.LivreId )
               .HasColumnName( "LivreId" );

        builder.Property( x => x.Titre )
               .HasColumnType( "nvarchar(250)" );


        builder.HasData( new Livre { LivreId = 1, Titre = "Titre1" }, new Livre { LivreId = 2, Titre = "Titre2" } );
    }
}