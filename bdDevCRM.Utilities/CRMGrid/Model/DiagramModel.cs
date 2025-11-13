using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utilities.Model
{
    public class DiagramModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string image { get; set; }
        public string title { get; set; }
        public string colorScheme { get; set; }
        public string RefId { get; set; }
        public int StateId { get; set; }
        public int Gender { get; set; }
        public string EmployeeId { get; set; }
        public string ShortName { get; set; }
        public int level { get; set; }
        public int CompanyId { get; set; }


        public List<DiagramModel> items { get; set; }
    }
}