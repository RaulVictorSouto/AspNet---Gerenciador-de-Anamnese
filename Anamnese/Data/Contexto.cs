using Anamnese.Models;
using Microsoft.EntityFrameworkCore;

namespace Anamnese.Data
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options) 
        {

        }

        public DbSet<PacienteModel> PacienteModel { get; set;}
        public DbSet<AnamneseModel> AnamneseModel { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<AnamneseModel>()
                .ToTable("tblanamneses")
                .HasKey(a => a.IdAnamnese);

            modelBuilder.Entity<AnamneseModel>()
                .Property(a => a.IdPaciente)
                .HasColumnName("IdPaciente");

            modelBuilder.Entity<AnamneseModel>()
               .HasOne(a => a.Paciente)
               .WithMany() 
               .HasForeignKey(a => a.IdPaciente);
        }
    }
}
