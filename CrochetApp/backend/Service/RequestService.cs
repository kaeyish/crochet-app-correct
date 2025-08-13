using CrochetApp.backend.Domain.Model;
using CrochetApp.backend.Domain.RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Service
{
    public  class RequestService
    {
        private IRequestRepository _requestRepository;

        public RequestService(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public List<Request> GetRequestsByCreator(int creatorId)
        {
            return _requestRepository.GetRequestsByCreator(creatorId);
        }

        public List<Request> GetRequestsByStatus(string status)
        {
            return _requestRepository.GetRequestsByStatus(status);
        }

        public List<Request> GetRequestsByDate(string date)
        {
            return _requestRepository.GetRequestsByDate(date);
        }

        public Request GetRequestById(int id)
        {
            return _requestRepository.GetRequestById(id);
        }

        public void AddRequest(DateTime date, string status, int creatorId)
        {
            _requestRepository.AddRequest(DateTimeFormatting.FormatSQL(date), status, creatorId);
        }

        public void UpdateRequest(int id, DateTime date, string status, int adminId)
        {
            _requestRepository.UpdateRequest(id, DateTimeFormatting.FormatSQL(date), status, adminId);
        }

        public void DeleteRequest(int id)
        {
            _requestRepository.DeleteRequest(id);
        }


    }
}
