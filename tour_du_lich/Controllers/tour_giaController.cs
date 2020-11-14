using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DAO.Model;

namespace tour_du_lich.Controllers
{
    public class tour_giaController : Controller
    {
        private db db = new db();

        // GET: tour_gia
        public ActionResult Index([Bind(Include = "tour_id")] tour_gia tour_gia)
        {
            // Tạo cbx
            // Lấy toàn bộ thể loại:
            List<tour> cate = db.tours.ToList();

            // Tạo SelectList
            SelectList List = new SelectList(cate, "tour_id", "tour_ten",1);

            // Set vào ViewBag
            ViewBag.tourList = List;

           if(tour_gia.tour_id == null)
            {
                var data = db.tour_gia.Where(m => m.tour_id == 1).ToList();
                return View(data);
            }
            else
            {
                var data = db.tour_gia.Where(m => m.tour_id == tour_gia.tour_id).ToList();
                return View(data);
            }
           
           
        }

        // GET: tour_gia/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_gia tour_gia = db.tour_gia.Find(id);
            if (tour_gia == null)
            {
                return HttpNotFound();
            }
            return View(tour_gia);
        }

        // GET: tour_gia/Create
        public ActionResult Create()
        {
            // Lấy data
            // Lấy toàn bộ thể loại:
            List<tour> cate = db.tours.ToList();

            // Tạo SelectList
            SelectList List = new SelectList(cate, "tour_id", "tour_ten");

            // Set vào ViewBag
            ViewBag.tourList = List;
            return View();
        }

        // POST: tour_gia/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "gia_id,gia_sotien,tour_id,gia_tungay,gia_denngay")] tour_gia tour_gia)
        {
            if (ModelState.IsValid)
            {
                db.tour_gia.Add(tour_gia);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(tour_gia);
        }

        // GET: tour_gia/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_gia tour_gia = db.tour_gia.Find(id);
            if (tour_gia == null)
            {
                return HttpNotFound();
            }
            return View(tour_gia);
        }

        // POST: tour_gia/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "gia_id,gia_sotien,tour_id,gia_tungay,gia_denngay")] tour_gia tour_gia)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tour_gia).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(tour_gia);
        }

        // GET: tour_gia/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            tour_gia tour_gia = db.tour_gia.Find(id);
            if (tour_gia == null)
            {
                return HttpNotFound();
            }
            return View(tour_gia);
        }

        // POST: tour_gia/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            tour_gia tour_gia = db.tour_gia.Find(id);
            db.tour_gia.Remove(tour_gia);
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
