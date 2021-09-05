using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory.ValueGeneration.Internal;
using Microsoft.EntityFrameworkCore.Metadata;
//using Microsoft.EntityFrameworkCore.Relational;
using ShipCURDOperations.Data.Models;
using System.Linq;

namespace ShipCURDOperations.Data.Repository

{
    public class MemoryDBContext : DbContext
    {
        public MemoryDBContext(DbContextOptions<MemoryDBContext> options) : base(options)
        {
        }
        public DbSet<Ship> Ships { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            // builder.Entity<Ship>().Property(x => x.Id).HasValueGenerator()
            //       //  .HasColumnName(@"ContactCategoryID")
            //         .HasColumnType("int") //Weirdly this was upsetting SQLite
            //         .IsRequired()
            //         .ValueGeneratedOnAdd().Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore)
            //         ;

            //  builder.Entity<Ship>().HasKey(s => s.Id);
            builder.Entity<Ship>().Property(p => p.Code);
    //         builder.Entity<Ship>()
    // .Property(e => e.Id)
    // .ValueGeneratedOnAdd()
    // .Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Throw);


            // //.Metadata.SetAfterSaveBehavior(PropertySaveBehavior.Ignore);
            // builder.<int>("ProjectId").StartsAt(100).IncrementsBy(1);

            builder.Entity<Ship>(ship =>
            {
                // var shipId = ship.Property(s => s.Id).ValueGeneratedOnAdd().;

                // shipId.ValueGeneratedOnAdd();
                // if (Database.IsInMemory())
                //     shipId.HasValueGenerator<InMemoryIntegerValueGenerator<int>>();


                // if (Database.IsInMemory())
                // {
                //     var autoGenDecimalProperies = builder.Model.GetEntityTypes()
                //         .Select(t => t.FindPrimaryKey())
                //         .Where(pk => pk != null)
                //         .SelectMany(pk => pk.Properties)
                //         .Where(p => p.ClrType == typeof(int) && p.ValueGenerated != ValueGenerated.Never);

                //     foreach (var property in autoGenDecimalProperies)
                // property.SetValueGeneratorFactory((p, t) => new InMemoryIntegerValueGenerator<int>(p.ValueGenerated));
                // }
            });
            base.OnModelCreating(builder);

        }
    }



}