using EEIMS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EEIMS.Functionalities
{
    public interface IRequestRepository
    {
        void CreateNewRequest(CreateNewRequestViewModel requestViewModel);
        IEnumerable<ReviewRequestsViewModel> GetRequestedByEmployees();
        IEnumerable<ReviewRequestsViewModel> GetRequestedHistory();
        bool ApproveRequest(int requestId);
        bool DenyRequest(int requestId);


    }
}
