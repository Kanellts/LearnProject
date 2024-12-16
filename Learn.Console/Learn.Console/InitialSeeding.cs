using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Learn.Console.Migrations {
    public partial class InitialSeeding
    {    
        protected  void Seed(EntityTypeBuilder builder)
        {
            builder.HasData(new Student {Id = 1, Name = "Jon Smith", Age = 22},
                            new Student {Id = 2, Name = "Veronica Smith", Age = 19},
                            new Student {Id = 3, Name = "Jane Doe", Age = 21});
        }
    }
}
