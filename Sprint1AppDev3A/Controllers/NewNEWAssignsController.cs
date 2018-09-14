using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Sprint1AppDev3A.Models;

namespace Sprint1AppDev3A.Controllers
{
    public class NewNEWAssignsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: NewNEWAssigns
        public ActionResult Index()
        {
            var newNEWAssigns = db.NewNEWAssigns.Include(n => n.ContainerID);
            return View(newNEWAssigns.ToList());
        }

        // GET: NewNEWAssigns/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewNEWAssign newNEWAssign = db.NewNEWAssigns.Find(id);
            if (newNEWAssign == null)
            {
                return HttpNotFound();
            }
            return View(newNEWAssign);
        }

        // GET: NewNEWAssigns/Create
        public ActionResult Create()
        {
            ViewBag.ContID = new SelectList(db.NewContainers, "ContainerID", "ContainerNumber");
            return View();
        }

        // POST: NewNEWAssigns/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "AssignID,ContID,SelectedDate,ContainerNumber,ContainerSize,Driver,Truck,Trailer,Location,Destination,Status,PickUpTime,PickUpDate,EstDate,EstTime,AssignCode")] NewNEWAssign newNEWAssign)
        {
            if (ModelState.IsValid)
            {
                do
                {
                   
                        var containers = db.NewContainers.Where(x=>x.Status == "UnAssigned" && x.PickUp == newNEWAssign.SelectedDate).ToList();

                        var Priority1 = containers.Where(x => x.Priority == "High").ToList();
                        var Priority2 = containers.Where(x => x.Priority == "Medium").ToList();
                        var Priority3 = containers.Where(x => x.Priority == "Low").ToList();

                        var FreeDri = db.DatesBookedDrivers.Where(x=>x.PickUpDate != newNEWAssign.SelectedDate).ToList();
                        var FreeTru = db.DatesBookedTrucks.Where(x => x.PickUpDate != newNEWAssign.SelectedDate).ToList();
                        var FreeTra = db.DatesBookedTrailers.Where(x => x.PickUpDate != newNEWAssign.SelectedDate).ToList();
                        
                       

                        foreach (var item in Priority1)
                        {
                                try
                                        {
                                            var Pri1 = Priority1.Where(x => x.Status == "UnAssigned").First();
                                            var Dri = FreeDri.Where(x=>x.PickUpDate != newNEWAssign.SelectedDate).First();
                                            var DriverYeah = db.NewDrivers.Find(Dri.DriveID);                                            

                                            NewNEWAssign obj = new NewNEWAssign();
                                            obj.AssignID = Pri1.ContainerID;
                                            obj.PickUpDate = Pri1.PickUp;
                                            obj.PickUpTime = Pri1.PickUpTime;
                                            obj.ContainerNumber = Pri1.ContainerNumber;
                                            obj.ContainerSize = Pri1.ContainerSize;
                                            obj.Driver = DriverYeah.Email;

                                            Pri1.Status = "Pending";

                                        }
                                catch (Exception)
                                        {

                            
                                        }
                            


                      
                        }

                       

                       


                } while (true);









                db.NewNEWAssigns.Add(newNEWAssign);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ContID = new SelectList(db.NewContainers, "ContainerID", "ContainerNumber", newNEWAssign.ContID);
            return View(newNEWAssign);
        }

        // GET: NewNEWAssigns/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewNEWAssign newNEWAssign = db.NewNEWAssigns.Find(id);
            if (newNEWAssign == null)
            {
                return HttpNotFound();
            }
            ViewBag.ContID = new SelectList(db.NewContainers, "ContainerID", "ContainerNumber", newNEWAssign.ContID);
            return View(newNEWAssign);
        }

        // POST: NewNEWAssigns/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "AssignID,ContID,SelectedDate,ContainerNumber,ContainerSize,Driver,Truck,Trailer,Location,Destination,Status,PickUpTime,PickUpDate,EstDate,EstTime,AssignCode")] NewNEWAssign newNEWAssign)
        {
            if (ModelState.IsValid)
            {
                db.Entry(newNEWAssign).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ContID = new SelectList(db.NewContainers, "ContainerID", "ContainerNumber", newNEWAssign.ContID);
            return View(newNEWAssign);
        }

        // GET: NewNEWAssigns/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            NewNEWAssign newNEWAssign = db.NewNEWAssigns.Find(id);
            if (newNEWAssign == null)
            {
                return HttpNotFound();
            }
            return View(newNEWAssign);
        }

        // POST: NewNEWAssigns/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            NewNEWAssign newNEWAssign = db.NewNEWAssigns.Find(id);
            db.NewNEWAssigns.Remove(newNEWAssign);
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
