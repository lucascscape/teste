﻿using Senai.Vagas.Domain.SeedWork;
using System;
using System.Collections.Generic;
using System.Text;

namespace Senai.Vagas.Domain.AggregatesModel.HistoricoStatusEstagioAggregate.Models
{
    public class HistoricoStatusEstagio : AbstractDomain
    {
	 public string Explicacao { get; set; } 
	 public bool Atual  { get; set; }
     public long StatusEstagioId  { get; set; }
	 public long EstagioId  { get; set; }

    public HistoricoStatusEstagio(string explicacao, long statusStagioId, long estagioId)
        {
            StatusEstagioId = statusStagioId;
            Explicacao = explicacao;
            EstagioId = estagioId;
            Atual = true;

        }

        public void AlterarParaAntigo()
        {
            Atual = false;
        }
    }
}

