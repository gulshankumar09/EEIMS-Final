using EEIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EEIMS.Repositories
{
    public class AssignmentRepository
    {
        ApplicationDbContext _context;

        public AssignmentRepository()
        {
            _context = new ApplicationDbContext();
        }

        public bool CreateAssignemt(createAssignmentViewModel assignment)
        {
            try
            {
                var newAssign = new Assignment
                {
                    EmployeeId = assignment.EmployeeId,
                    EquipmentId = assignment.EquipmentId,
                    AssignDate = DateTime.Now,
                    ExpectedReturnDate = assignment.ExpectedReturnDate,
                    ActualReturnDate = assignment.ActualReturnDate,
                    assignStatus = AssignStatus.assigned
                };

                _context.Assignments.Add(newAssign);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return false;
        }
    }
}