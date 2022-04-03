using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using AndreAirlaines_API.Model;

namespace AndreAirlaines_API.Data
{
    public class AndreAirlaines_APIContext : DbContext
    {
        public AndreAirlaines_APIContext (DbContextOptions<AndreAirlaines_APIContext> options)
            : base(options)
        {
        }

        public DbSet<AndreAirlaines_API.Model.Aeronave> Aeronave { get; set; }

        public DbSet<AndreAirlaines_API.Model.Aeroporto> Aeroporto { get; set; }

        public DbSet<AndreAirlaines_API.Model.Endereco> Endereco { get; set; }

        public DbSet<AndreAirlaines_API.Model.Passageiro> Passageiro { get; set; }

        public DbSet<AndreAirlaines_API.Model.Voo> Voo { get; set; }

        public DbSet<AndreAirlaines_API.Model.Classe> Classe { get; set; }

        public DbSet<AndreAirlaines_API.Model.Passagem> Passagem { get; set; }

        public DbSet<AndreAirlaines_API.Model.PrecoBase> PrecoBase { get; set; }
    }
}
