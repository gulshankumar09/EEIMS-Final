using EEIMS.Models;
using EEIMS.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace EEIMS.Controllers
{
    public class RequestController : Controller
    {
        RequestRepository _requestRepository = new RequestRepository();


        public ActionResult Index()
        {
            return View();
        }

        // GET: Request
        [HttpGet]
        public ActionResult AddNewRequest()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddNewRequest(CreateNewRequestViewModel requestViewModel)
        {
            if (ModelState.IsValid)
            {
                _requestRepository.CreateNewRequest(requestViewModel);
                return RedirectToAction("Index", "Request");
            }
            else
            {
                return View(requestViewModel);
            }
        }

        //
        // Get: pending requests from the database
        [HttpGet]
        public ActionResult ReviewRequests()
        {
            var requests = _requestRepository.GetRequestedByEmployees();
            return View(requests);
        }

        //
        // Post: approve

        [HttpPost]
        public ActionResult ApproveRequest(int requestId)
        {
            _requestRepository.ApproveRequest(requestId);
            return RedirectToAction("ReviewRequests");
        }

        //
        // Post: deny
        [HttpPost]
        public ActionResult DenyRequest(int requestId)
        {
            _requestRepository.DenyRequest(requestId);
            return RedirectToAction("ReviewRequests");
        }




    }
}