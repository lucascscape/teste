using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.AlterarCredenciaisAggregate.Models
{
    public class AlterarCredenciais : AbstractDomain
    {
        public string Token { get; set; }
        public DateTime DataValida { get; set; }
        public bool Ativo { get; set; }
        public long AlunoId { get; set; }
        public string NovoEmail { get; set; }


        public AlterarCredenciais(string token, long alunoId, string novoEmail)
        {

            Token = token;
            AlunoId = alunoId;
            Ativo = true;
            DataValida = DateTime.Now;
            NovoEmail = novoEmail;
        }

        public void AlterarParaInativo()
        {
            Ativo = false;
        }
    }
}
