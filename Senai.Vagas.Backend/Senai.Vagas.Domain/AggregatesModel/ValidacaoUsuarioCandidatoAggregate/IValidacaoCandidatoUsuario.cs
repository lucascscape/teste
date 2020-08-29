using Senai.Vagas.Domain.AggregatesModel.ValidacaoUsuarioCandidatoAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.ValidacaoUsuarioCandidatoAggregate
{
    public interface IValidacaoCandidatoUsuario : IRepository<ValidacaoUsuarioCandidato>
    {
        void AtivoInativo(string Ativo);
        ValidacaoUsuarioCandidato GetAunobyId(long AlunoId);
    }
}
