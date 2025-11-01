using DataAccessLayer.Entities;
using DataAccessLayer.Repository;
using HPRSApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

namespace HPRSApp.Controllers
{
    public class PatientController : Controller
    {
        IHPRS repo;
        public PatientController(IHPRS repo)
        {
            this.repo = repo;
        }
        [HttpGet]
        public IActionResult ViewAllPatientRecords()
        {
            var patientList = repo.GetAllPatients();
            var pvmList = new List<PatientViewModel>();
            if (patientList != null)
            {
                foreach (var p in patientList)
                {
                    if (!p.isDischarged)
                    {
                        var pvm = new PatientViewModel
                        {
                            PatientId = p.PatientId,
                            Name = p.Name,
                            Age = p.Age,
                            Gender = p.Gender,
                            Diagnosis = p.Diagnosis,
                            AddmissionDate = p.AddmissionDate,
                            isDischarged = p.isDischarged
                        };
                        pvmList.Add(pvm);
                    }
                }
                return View(pvmList);
            }
            return View();
        }
        [HttpPost]
        public IActionResult ViewAllPatientRecords(string diagnosis)
        {
            Console.WriteLine("POST method triggered");
            var patientList = repo.SearchPatientByDiagnosis(diagnosis);
            var pvmList = new List<PatientViewModel>();
            if (patientList != null)
            {
                foreach (var pa in patientList)
                {
                    if (!pa.isDischarged)
                    {
                        var pvm = new PatientViewModel
                        {
                            PatientId = pa.PatientId,
                            Name = pa.Name,
                            Age = pa.Age,
                            Gender = pa.Gender,
                            Diagnosis = pa.Diagnosis,
                            AddmissionDate = pa.AddmissionDate,
                            isDischarged = pa.isDischarged

                        };
                        pvmList.Add(pvm);

                    }
                }
                return View(pvmList);
            }
            ViewBag.msg = "There is no Patients with the searched Disgnosis!";
            return View();
        }

        [HttpGet]
        public IActionResult ViewAllDischargedPatientRecords()
        {
            var patientList = repo.GetAllPatients();
            var pvmList = new List<PatientViewModel>();
            if (patientList != null)
            {
                foreach (var p in patientList)
                {
                    if (p.isDischarged)
                    {
                        var pvm = new PatientViewModel
                        {
                            PatientId = p.PatientId,
                            Name = p.Name,
                            Age = p.Age,
                            Gender = p.Gender,
                            Diagnosis = p.Diagnosis,
                            AddmissionDate = p.AddmissionDate,
                            isDischarged = p.isDischarged
                        };
                        pvmList.Add(pvm);
                    }
                }
                return View(pvmList);
            }
            return View();
        }
        [HttpPost]
        public IActionResult ViewAllDischargedPatientRecords(string diagnosis)
        {
            var patientList = repo.SearchPatientByDiagnosis(diagnosis);
            var pvmList = new List<PatientViewModel>();
            if (patientList != null)
            {
                foreach (var pa in patientList)
                {
                    if (pa.isDischarged)
                    {
                        var pvm = new PatientViewModel
                        {
                            PatientId = pa.PatientId,
                            Name = pa.Name,
                            Age = pa.Age,
                            Gender = pa.Gender,
                            Diagnosis = pa.Diagnosis,
                            AddmissionDate = pa.AddmissionDate,
                            isDischarged = pa.isDischarged

                        };
                        pvmList.Add(pvm);

                    }
                }
                return View(pvmList);
            }
            ViewBag.msg = "There is no Discharged Patients with the searched Disgnosis!";
            return View();
        }

        [HttpGet]
        public IActionResult AddNewPatient()
        {
            ViewBag.Gender = new List<SelectListItem> {
               new SelectListItem { Value = "Male", Text="Male"},
               new SelectListItem { Value = "Female", Text="Female"},
               new SelectListItem { Value = "Others", Text="Others"},
            };
            return View();
        }
        [HttpPost]
        public IActionResult AddNewPatient(PatientViewModel p)
        {
            if (ModelState.IsValid)
            {
                var patient = new Patient
                {
                    Name = p.Name,
                    Age = p.Age,
                    Gender = p.Gender,
                    Diagnosis = p.Diagnosis,
                    AddmissionDate = p.AddmissionDate,
                    isDischarged = p.isDischarged
                };
                var result = repo.AddPatient(patient);
                if (result)
                {
                    return RedirectToAction("ViewAllPatientRecords");
                }
            }
            ViewBag.msg = "Adding failed";
            return View(p);
        }
        [HttpGet]
        public IActionResult UpdatePatientRecord(int id)
        {
            var patient = repo.GetPatientById(id);
            if (patient != null)
            {
                var pvm = new PatientViewModel
                {
                    PatientId = patient.PatientId,
                    Name = patient.Name,
                    Age = patient.Age,
                    Gender = patient.Gender,
                    Diagnosis = patient.Diagnosis,
                    AddmissionDate = patient.AddmissionDate,
                    isDischarged = patient.isDischarged

                };
                ViewBag.Gender = new List<SelectListItem> {
               new SelectListItem { Value = "Male", Text="Male" , Selected=patient.Gender=="Male"},
               new SelectListItem { Value = "Female", Text="Female", Selected=patient.Gender=="Female"},
               new SelectListItem { Value = "Others", Text="Others", Selected=patient.Gender=="Others"},
               };
                return View(pvm);

            }
            return NotFound();
        }

        [HttpPost]
        public IActionResult UpdatePatientRecord(Patient p)
        {
            var patient = repo.GetPatientById(p.PatientId);
            if (ModelState.IsValid)
            {
                if (patient != null)
                {
                    patient.PatientId = p.PatientId;
                    patient.Name = p.Name;
                    patient.Age = p.Age;
                    patient.Gender = p.Gender;
                    patient.Diagnosis = p.Diagnosis;
                    patient.AddmissionDate = p.AddmissionDate;
                    patient.isDischarged = p.isDischarged;

                    var result = repo.UpdatePatient(patient);
                    if (result)
                    {
                        return RedirectToAction("ViewAllPatientRecords");

                    }
                }
            }
            ViewBag.msg = "Update failed";
            return View(p);
        }
        [HttpGet]
        public IActionResult DeletePatient(int id)
        {
            var currentpatient = repo.GetPatientById(id);
            if (currentpatient != null)
            {
                var pvm = new PatientViewModel
                {
                    PatientId = currentpatient.PatientId,
                    Name = currentpatient.Name,
                    Age = currentpatient.Age,
                    Gender = currentpatient.Gender,
                    Diagnosis = currentpatient.Diagnosis,
                    AddmissionDate = currentpatient.AddmissionDate,
                    isDischarged = currentpatient.isDischarged,
                };
                return View(pvm);
            }
            return NotFound();
        }
        [HttpPost]
        [ActionName("DeletePatient")]
        public IActionResult DeletePatientConfirm(int id)
        {
            var currentpatient = repo.GetPatientById(id);
            if (currentpatient != null)
            {
                var result = repo.DeletePatient(currentpatient);
                if (result)
                {
                    return RedirectToAction("ViewAllPatientRecords");
                }
            }
            return NotFound();
        }
    }
}
