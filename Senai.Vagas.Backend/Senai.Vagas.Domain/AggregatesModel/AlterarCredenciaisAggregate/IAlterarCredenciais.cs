using Senai.Vagas.Domain.AggregatesModel.AlterarCredenciaisAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.AlterarCredenciaisAggregate
{
    public interface IAlterarCredenciais : IRepository<AlterarCredenciais>
    {
        void AtivoInativo(string Ativo);
        AlterarCredenciais GetAunobyId(long AlunoId);
        void NovoEmail(int AlunoId, AlterarCredenciais novoEmail);

    }
}


