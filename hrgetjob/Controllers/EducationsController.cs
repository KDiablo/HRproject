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
    public class EducationsController : Controller
    {
        private HrGetJobModel db = new HrGetJobModel();

        // GET: Educations
        public ActionResult Index()
        {
            var educations = db.Educations.Include(e => e.Applicant);
            return View(educations.ToList());
        }

        // GET: Educations/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Education education = db.Educations.Find(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }

        // GET: Educations/Create
        public ActionResult Create()
        {
            ViewBag.ApplicantID = new SelectList(db.Applicants, "AppID", "FirstName");
            return View();
        }

        // POST: Educations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EdID,ApplicantID,SchoolName,City,StateName,Zip,StartDate,EndDate,Graduate,Degree,StartY,EndY")] Education education)
        {
            if (ModelState.IsValid)
            {
                db.Educations.Add(education);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicantID = new SelectList(db.Applicants, "AppID", "FirstName", education.ApplicantID);
            return View(education);
        }

        // GET: Educations/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Education education = db.Educations.Find(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicantID = new SelectList(db.Applicants, "AppID", "FirstName", education.ApplicantID);
            return View(education);
        }

        // POST: Educations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EdID,ApplicantID,SchoolName,City,StateName,Zip,StartDate,EndDate,Graduate,Degree,StartY,EndY")] Education education)
        {
            if (ModelState.IsValid)
            {
                db.Entry(education).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicantID = new SelectList(db.Applicants, "AppID", "FirstName", education.ApplicantID);
            return View(education);
        }

        // GET: Educations/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Education education = db.Educations.Find(id);
            if (education == null)
            {
                return HttpNotFound();
            }
            return View(education);
        }

        // POST: Educations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Education education = db.Educations.Find(id);
            db.Educations.Remove(education);
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
