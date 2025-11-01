using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public interface IHPRS
    {
        List<Patient> GetAllPatients();
        Patient GetPatientById(int id);
        bool AddPatient(Patient patient);
        bool UpdatePatient(Patient patient);
        bool DeletePatient(Patient patient);
        bool RegisterUser(Admin user);
        bool ValidateUser(string email,string password);
        bool UpdateAdmin(Admin user);
        Admin GetAdminByEmailId(string email);

        List<Patient> SearchPatientByDiagnosis(string diagnosis);



    }
}
