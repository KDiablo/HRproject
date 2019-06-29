using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using HrGetJob.Models;

namespace HrGetJob.Controllers
{
    public class ApplicantsController : Controller
    {
        private HrGetJobModel db = new HrGetJobModel();

        // GET: Applicants
        public ActionResult Index()
        {
            var applicants = db.Applicants.Include(a => a.JobPosting);
            return View(applicants.ToList());
        }

        // GET: Applicants/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant applicant = db.Applicants.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            return View(applicant);
        }

        // GET: Applicants/Create
        public ActionResult Create()
        {
            ViewBag.JobPostingID = new SelectList(db.JobPostings, "JobPostingID", "CreatedBy");
            return View();
        }

        // POST: Applicants/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AppID,JobPostingID,FirstName,MiddleName,LastName,Street,City,StateName,Zip,Phone1,Phone2,Email,bestWay2contact,LinkedIn,ResumeName,ResumePath,ASign,SubmittedDate,Archive,WhenArchived,Emailed,WhenEmailed,WhoEmailed,JobFairAttended")] Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                db.Applicants.Add(applicant);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.JobPostingID = new SelectList(db.JobPostings, "JobPostingID", "CreatedBy", applicant.JobPostingID);
            return View(applicant);
        }

        // GET: Applicants/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant applicant = db.Applicants.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            ViewBag.JobPostingID = new SelectList(db.JobPostings, "JobPostingID", "CreatedBy", applicant.JobPostingID);
            return View(applicant);
        }

        // POST: Applicants/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AppID,JobPostingID,FirstName,MiddleName,LastName,Street,City,StateName,Zip,Phone1,Phone2,Email,bestWay2contact,LinkedIn,ResumeName,ResumePath,ASign,SubmittedDate,Archive,WhenArchived,Emailed,WhenEmailed,WhoEmailed,JobFairAttended")] Applicant applicant)
        {
            if (ModelState.IsValid)
            {
                db.Entry(applicant).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.JobPostingID = new SelectList(db.JobPostings, "JobPostingID", "CreatedBy", applicant.JobPostingID);
            return View(applicant);
        }

        // GET: Applicants/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Applicant applicant = db.Applicants.Find(id);
            if (applicant == null)
            {
                return HttpNotFound();
            }
            return View(applicant);
        }

        // POST: Applicants/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Applicant applicant = db.Applicants.Find(id);
            db.Applicants.Remove(applicant);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
