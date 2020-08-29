using Microsoft.EntityFrameworkCore;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Senai.Vagas.Infrastructure.Contexts
{
    public class SenaiVagasContext : DbContext, IUnitOfWork
    {
        public SenaiVagasContext(DbContextOptions<SenaiVagasContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Classes mapeadoras
            //modelBuilder.ApplyConfiguration(new NomeClasse());


            //Caso houver uma referencia de 2 Foreign Key's para uma mesma tabela, o loop altera o comportamento de Cascata para Restrito
            //[...] automaticamente ao fazer a migration
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                entityType.GetForeignKeys()
                    .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade)
                    .ToList()
                    .ForEach(fk => fk.DeleteBehavior = DeleteBehavior.Restrict);
            }

        }

        //Padrão de repositorios usando UnitOfWork, salva alterações na DB independentemente de quais tabelas foram alteradas.
        public async Task SaveDbChanges(CancellationToken cancellationToken = default)
        {
            await base.SaveChangesAsync();
        }
    }
}
