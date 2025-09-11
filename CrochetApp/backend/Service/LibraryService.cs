using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Service
{
    public class LibraryService
    {
        private readonly ILibraryRepository _libraryRepository;
        public LibraryService(ILibraryRepository libraryRepository) {
            _libraryRepository = libraryRepository;
        }

        public List<Library> GetAllLibraries() {
            return _libraryRepository.GetAllLibraries();
        }
        public List<Library> GetLibrariesByUser(int id) {
            return _libraryRepository.GetLibraryByUser(id);
        }
        public Library GetLibraryById(int id) {
            return _libraryRepository.GetLibraryById(id);
        }
        public Library GetLibraryByName(string name) {
            return _libraryRepository.GetLibraryByName(name);
        }

        public int AddLibrary(string name, string desc, int user) {

            return _libraryRepository.AddLibrary(name, desc, DateTimeFormatting.FormatSQL(DateTime.Now), user);
        }

        public void ConnectPatternToLibrary(int patternId, int libraryId, int userId) {
            _libraryRepository.ConnectPatternToLibrary(patternId, libraryId, userId);
        }

        public void UpdateLibrary(int id, string name, string desc, DateTime date) { 
            _libraryRepository.UpdateLibrary(id, name, desc, DateTimeFormatting.FormatSQL(date));
        }

        public void DeleteLibrary(int id) {
            _libraryRepository.DeleteLibrary(id);
        }

    }
}
