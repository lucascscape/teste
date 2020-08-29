using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.AlunoAggregate.Models
{
   public class Aluno : AbstractDomain
    {
        public string NomeCompleto { get; set; }
        public string Email { get; set; }
        public string RMA { get; set; }
        public long TipoCursoId { get; set; }

         public Aluno(string nomeCompleto, string email, string rma, long tipoCursoId)
    {
        NomeCompleto = nomeCompleto;
        Email = email;
        RMA = rma;
        TipoCursoId = tipoCursoId;
    }

    }

   
}
