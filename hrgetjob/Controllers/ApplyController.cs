using HrGetJob.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace HrGetJob.Controllers
{
    public class ApplyController : Controller
    {
        //instantiate ado model
        private HrGetJobModel db = new HrGetJobModel();
        //view all open job postions
        public ActionResult ViewAllPosts()
        {
            return View(GetJobs());
        }
        //view a single job position by user selection
        public ActionResult ViewPost(int? id)
        {

            if (id == null)
            {
                return RedirectToAction("Error", "Shared");
            }
            JobPosting job = db.JobPostings.Find(id);
            if (job == null)
            {
                return RedirectToAction("Error", "Shared");
            }
            var jobTitle = job.Job.JobTitle;
            var branch = job.Branch.BranchName;
            ViewBag.JobTitle = jobTitle;
            ViewBag.BranchName = branch;
            return View(job);
        }

         public ActionResult Apply()
        {
            return View();
        }

        //helper method to to enumerate the open job positions   
        IEnumerable<JobPosting> GetJobs()
        {
            List<JobPosting> activePostList = new List<JobPosting>();
            Job job = new Job();
            foreach (var active in db.JobPostings)
            {
                if (active.IsActive)
                {
                    activePostList = db.JobPostings.ToList();
                }
            }
            return activePostList;
        }
    }
}