using EEIMS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace EEIMS.Repositories
{
    public class RequestRepository
    {
        

        private ApplicationDbContext _context;

        public RequestRepository()
        {
            _context = new ApplicationDbContext();
        }

        public void CreateNewRequest(CreateNewRequestViewModel requestViewModel)
        {
            try
            {
                var newRequest = new Request
                {
                    EmployeeId = requestViewModel.EmployeeId,
                    CategoryId = requestViewModel.CategoryId,
                    Comments = requestViewModel.Comments,
                    RequestDate = DateTime.Now, 
                    Status = RequestStatus.Pending,
                };

                _context.Requests.Add(newRequest);
                _context.SaveChanges();

            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
        }

        public IEnumerable<ReviewRequestsViewModel> GetRequestedByEmployees()
        {
            try
            {
                var requests = _context.Requests.Where(r => r.Status == RequestStatus.Pending).ToList();
                return requests.Select(r => new ReviewRequestsViewModel
                {
                    RequestId = r.RequestId,
                    RequestDate = r.RequestDate,
                    Status = r.Status,
                    Comments = r.Comments,
                    RequestedBy = r.RequestedBy.FirstName + " " + r.RequestedBy.LastName,
                    Category = r.Category.CategoryName
                }).ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                return null;
            }
        }

        public bool ApproveRequest(int requestId)
        {
            try
            {
                var request = _context.Requests.Find(requestId);
                if (request != null)
                {
                    request.Status = RequestStatus.Approved;
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
            return false;
        }
        
        public bool DenyRequest(int requestId)
        {
            try
            {
                var request = _context.Requests.Find(requestId);
                if (request != null)
                {
                    request.Status = RequestStatus.Rejected;
                    _context.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
            }
                return false;
        }
    }
}