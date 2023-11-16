using EEIMS.Functionalities;
using EEIMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EEIMS.Repositories
{
    public class AssignmentRepository: IAssignmentRepository, IDisposable
    {
        private ApplicationDbContext _context;

        public AssignmentRepository(ApplicationDbContext applicationDbContext)
        {
            _context = applicationDbContext;
        }

        public ApplicationDbContext Context
        {
            get
            {
                return _context ?? new ApplicationDbContext();
            }
            private set
            {
                _context = value;
            }
        }

        bool IAssignmentRepository.CreateAssignemt(CreateAssignmentViewModel assignment)
        {
            foreach(var equipmentId in assignment.EquipmentIds)
            {
                var newAssign = new Assignment
                {
                    EmployeeId = assignment.EmployeeId,
                    EquipmentId = equipmentId,
                    AssignDate = DateTime.Now,
                    ExpectedReturnDate = assignment.ExpectedReturnDate,
                    assignStatus = AssignStatus.assigned
                };

                // Updating the assign equipment status
                var equipment = Context.Equipments.Find(equipmentId);
                if (equipment != null)
                {
                    equipment.IsAssigned = true;
                    Context.Entry(equipment).State = EntityState.Modified;
                }
                Context.Assignments.Add(newAssign);
                Context.SaveChanges();
            }
            return true;
        }

        UpdateAssignmentsViewModel IAssignmentRepository.GetAssignmentById(int id)
        {
            var temp = Context.Assignments.Where(a => a.AssignId == id).FirstOrDefault();
            var newTemp = new UpdateAssignmentsViewModel
            {
                EmployeeId = temp.EmployeeId,
                EquipmentId = temp.EquipmentId,
                ExpectedReturnDate = temp.ExpectedReturnDate
            };
            return newTemp;
        }

        bool IAssignmentRepository.UpdateAssignment(UpdateAssignmentsViewModel assignment)
        {
            var temp = Context.Assignments.Where(a => a.AssignId == assignment.AssignId).FirstOrDefault();
            if (temp != null)
            {
                temp.EmployeeId = assignment.EmployeeId;
                temp.EquipmentId = assignment.EquipmentId;
                temp.ExpectedReturnDate = assignment.ExpectedReturnDate;
                temp.AssignDate = DateTime.Now;
                temp.assignStatus = AssignStatus.assigned;
                Context.Entry(temp).State = EntityState.Modified;
                Context.SaveChanges();
                return true;
            }
            return false;
        }

        IEnumerable<Assignment> IAssignmentRepository.GetAllAssignments()
        {
            var temp = Context.Assignments.ToList();
            return temp;
        }


        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_context != null)
                {
                    _context.Dispose();
                    _context = null;
                }
            }
        }

        void IDisposable.Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

    }
}