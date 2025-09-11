using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Service
{
    public class HookService
    {
        IHookRepository _hookRepository;

        public HookService(IHookRepository hookRepository)
        {
            _hookRepository = hookRepository;
        }

        public int AddHook(double size)
        {
            return _hookRepository.AddHook(size);
        }

        public void DeleteHook(int id)
        {
            _hookRepository.DeleteHook(id);
        }

        public void UpdateHook(double size, int id)
        {
            _hookRepository.UpdateHook(size, id);
        }

        public List<Hook> GetAll()
        {
            return _hookRepository.GetAll();
        }

        public Hook GetHookById(int id)
        {
            return _hookRepository.GetById(id);
        }

        public List<Hook> GetAllBySize(double size)
        {
            return _hookRepository.GetAllBySize(size);
        }

        public void ConnectToPattern(int id, int patternId)
        {
            _hookRepository.ConnectToPattern(id, patternId);
        }
    }
}
