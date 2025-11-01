using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Entities
{
    public class Patient
    {
        public int PatientId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public string Gender { get; set; }
        public string Diagnosis { get; set; }
        public DateTime AddmissionDate { get; set; }
        public bool isDischarged { get; set; } = false;

    }
}
