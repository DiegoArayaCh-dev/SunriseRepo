﻿using Infraestructure.Models;
using Infraestructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Services
{
   public class ServiceInformes
    {
        public IEnumerable<HISTORICO> GetEntradas()
        {
            RepositoryInforme repository = new RepositoryInforme();
            return repository.GetEntradas();
        }

        public IEnumerable<HISTORICO> GetSalidas()
        {
            RepositoryInforme repository = new RepositoryInforme();
            return repository.GetSalidas();
        }
        public HISTORICO GetInformeById(int id)
        {
            RepositoryInforme repository = new RepositoryInforme();
            return repository.GetInformeByID(id);
        }
    }
}
