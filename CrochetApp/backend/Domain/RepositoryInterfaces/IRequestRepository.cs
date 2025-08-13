using CrochetApp.backend.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.RepositoryInterfaces
{
    public interface IRequestRepository
    {
        List<Request> GetRequestsByCreator(int creatorIduserId);

        List<Request> GetRequestsByStatus(string status);
        
        List<Request> GetRequestsByDate(string date);

        Request GetRequestById(int id);

        void AddRequest(string date, string status, int creatorId);
        void UpdateRequest(int id, string date, string status, int adminId);

        void DeleteRequest(int id);

    }
}
