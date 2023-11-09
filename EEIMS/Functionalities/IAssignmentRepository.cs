using EEIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEIMS.Functionalities
{
    public interface IAssignmentRepository
    {
        bool CreateAssignemt(CreateAssignmentViewModel assignment);

        UpdateAssignmentsViewModel GetAssignmentById(int id);

        bool UpdateAssignment(UpdateAssignmentsViewModel assignment);

        IEnumerable<Assignment> GetAllAssignments();

    }
}
