using CrochetApp.backend.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.RepositoryInterfaces
{
    public interface ILibraryRepository
    {
        public List<Library> GetAllLibraries();
        public Library GetLibraryById(int id);
        public Library GetLibraryByName(string name);

        public void AddLibrary(string name, string desc, string date, int user);

        public void UpdateLibrary(int id, string name, string desc, string date);

        public void DeleteLibrary(int id);

    }
}
