using CrochetApp.backend.Domain.Model;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.RepositoryInterfaces
{
    public interface IHookRepository
    {

        public Hook GetById(int id);

        public List<Hook> GetAll();

        public void AddHook(double size);

        public void UpdateHook(double size, int id);

        public void DeleteHook(int id);

        public List<Hook> GetAllBySize(double size);

    }
}
