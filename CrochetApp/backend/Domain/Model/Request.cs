using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrochetApp.backend.Domain.Model
{
    public class Request
    {

        public int Id { get; set; }

        public DateTime Date{ get; set; }

        public Status Status { get; set; }

        public int AdminId { get; set; }
        public int CreatorId { get; set; }

        public Request() { }

        public Request(int id, DateTime date, Status status, int adminId, int creatorId)
        {
            Id = id;
            Date = date;
            Status = status;
            AdminId = adminId;
            CreatorId = creatorId;
        }
    }
}
