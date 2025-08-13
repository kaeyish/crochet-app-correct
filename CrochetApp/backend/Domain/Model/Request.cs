using System;
using System.Collections.Generic;
using System.Globalization;
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

        public int? AdminId { get; set; }
        public int CreatorId { get; set; }

        public Request() { }

        public Request(int id, DateTime date, string status, int? adminId, int creatorId)
        {
            Id = id;
            Date = date;
            Status = (Status)Enum.Parse(typeof(Status), status, true);
            CreatorId = creatorId;

            if (adminId != null)
            {
                AdminId = adminId;
            }
            else
            {
                AdminId = null;
            }
        }
    }
}
