using DataAccessLayer.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class HPRS : IHPRS
    {
        private HPRSDbContext context;

        public HPRS()
        {
            context = new HPRSDbContext();
        }
        //today
        public bool AddPatient(Patient patient)
        {
            try
            {
                context.Patients.Add(patient);
                context.SaveChanges();
                return true;


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public bool DeletePatient(Patient patient)
        {
            try
            {
                var p = context.Patients.Find(patient.PatientId);
                if (p != null)
                {
                    context.Patients.Remove(p);
                    context.SaveChanges();
                    return true;
                }
                return false;

            }
            catch (Exception ex)
            {
                return false;
            }


        }

        public Admin GetAdminByEmailId(string email)
        {
            var admin = context.Admins.FirstOrDefault(a => a.EmailId == email);
            if (admin == null)
            {
                return null;
            }
            return admin;
        }

        //today
        public List<Patient> GetAllPatients()
        {
            return context.Patients.ToList();
        }
        //today
        public Patient GetPatientById(int id)
        {
            var patient = context.Patients.Find(id);
            try
            {
                if (id > 0)
                {
                    return patient;
                }
                return null;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public bool RegisterUser(Admin user)
        {
            try
            {
                if (user != null)
                {
                    context.Admins.Add(user);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return false;
            }
        }

        public List<Patient> SearchPatientByDiagnosis(string diagnosis)
        {
            try
            {
                if (diagnosis != null)
                {
                    return context.Patients.Where(d => d.Diagnosis.ToLower() == diagnosis.ToLower()).ToList();
                }
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        //today
        public bool UpdateAdmin(Admin user)
        {
            var admin = context.Admins.Find(user.AdminId);
            try
            {
                if (admin != null)
                {
                    context.Entry(admin).CurrentValues.SetValues(user);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        // today
        public bool UpdatePatient(Patient patient)
        {
            var oldPatient = context.Patients.Find(patient.PatientId);
            try
            {
                if (oldPatient != null)
                {
                    context.Entry(oldPatient).CurrentValues.SetValues(patient);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        //today
        public bool ValidateUser(string email, string password)
        {
            var adminUser = context.Admins.FirstOrDefault(a => a.EmailId == email && a.Password == password);
            try
            {
                if (adminUser == null)
                {
                    return false;
                }
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }


        }
    }
}
