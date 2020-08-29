using Senai.Vagas.Domain.AggregatesModel.AlunoAggregate.Models;
using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.AlunoAggregate
{
    public interface IAlunoRepository : IRepository<Aluno>
    {
        Aluno CreateAluno(Aluno aluno);
        Aluno GetTipoCursobyId(long tipocursoid);
        Aluno BuscarPorEmail(string email);
        Aluno BuscarPorRMA(string rma);
        Aluno GetAluno(string nomeCompleto);


    }
    
}
