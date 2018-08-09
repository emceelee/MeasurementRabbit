using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class BalanceGroup
    {
        private string _id;
        public BalanceGroup(string id)
        {
            _id = id;
        }
        public string Id { get { return _id; } }

        public List<Location> LocationAssignments { get; set; } = new List<Location>();
    }
}
