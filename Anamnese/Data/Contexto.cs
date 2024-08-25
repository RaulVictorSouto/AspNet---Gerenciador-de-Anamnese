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
    }
}
