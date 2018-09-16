using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;
using System.IO;
using System.Web.UI.WebControls;
using System.Globalization;
using Sprint1AppDev3A.Models;

namespace Sprint1AppDev3A.Controllers
{
    public class Bookings1Controller : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Bookings1
        public ActionResult Index()
        {
            string email = User.Identity.Name;
            var s = from a in db.Bookings select a;
            s = s.Where(x => x.email.Equals(email));
            return View(s);
            //return View(db.Bookings.ToList());
        }

        public ActionResult AgentIndex()
        {
            return View(db.Bookings.ToList());
        }
        public ActionResult NewBooking2()
        {
            return View();
        }


        // GET: Bookings1/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookings bookings = db.Bookings.Find(id);
            if (bookings == null)
            {
                return HttpNotFound();
            }
            var gv = new GridView();
            gv.DataSource = db.Bookings.ToList();
            gv.DataBind();
            Response.ClearContent();
            Response.Buffer = true;
            Response.AddHeader("content-disposition", "attachment; filename=DemoExcel.xls");
            Response.ContentType = "application/ms-excel";
            Response.Charset = "";
            StringWriter objStringWriter = new StringWriter();
            HtmlTextWriter objHtmlTextWriter = new HtmlTextWriter(objStringWriter);
            gv.RenderControl(objHtmlTextWriter);
            Response.Output.Write(objStringWriter.ToString());
            Response.Flush();
            Response.End();
            return View(bookings);
        }

        // GET: Bookings1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Bookings1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "refNumber,BookingIds,ColStreetNum,ColStreetName,ColArea,ColCity,ColCode,pickupdate,DelStreetNum,DelStreetName,DelArea,DelCity,DelCode,dropofdate,clientname,cellnum,email,ConNum,ConType,specInstruct,Assigned,delcon,colcon,testtext,two,finna,Collection,Dropoff")] Bookings bookings)
        {
            if (ModelState.IsValid)
            {
                NewContainer obj = new NewContainer();
                obj.ContainerID = bookings.BookingIds;
                obj.ContainerNumber = bookings.ConNum;
                obj.ContainerSize = bookings.ConType;
                obj.Priority = bookings.priorty;
                obj.PickUp = bookings.pickupdate;
                obj.Location = bookings.Collection;
                obj.Destination = bookings.Dropoff;
                obj.Status = "UnAssigned";


             
                
                

               
                var s = db.CalAmts.Where(x => x.id == 1);
                var s1 = db.Vats.Where(x => x.ID == 1);

                double f = Convert.ToDouble(s.Sum(y => y.fuel));
                double v = Convert.ToDouble(s1.Sum(y => y.vat));
                double rate = Convert.ToDouble(s.Sum(m => m.Rate));
                string neww = bookings.testtext;

                neww = neww.Replace("km", "");
                neww = neww.Replace(",", "");
                neww = neww.Trim();

                string dist = neww;
                bookings.two = Convert.ToDouble(neww, CultureInfo.InvariantCulture);
                bookings.two = Math.Round(bookings.two);

                

                db.NewContainers.Add(obj);
                db.SaveChanges();
                db.Bookings.Add(bookings);
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(bookings);
        }

        // GET: Bookings1/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookings bookings = db.Bookings.Find(id);
            if (bookings == null)
            {
                return HttpNotFound();
            }
            return View(bookings);
        }

        // POST: Bookings1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "BookingIds,ColStreetNum,ColStreetName,ColArea,ColCity,ColCode,pickupdate,DelStreetNum,DelStreetName,DelArea,DelCity,DelCode,dropofdate,clientname,cellnum,email,ConNum,ConType,specInstruct,Assigned,delcon,colcon,testtext,two,finna,Collection,Dropoff")] Bookings bookings)
        {
            if (ModelState.IsValid)
            {
                db.Entry(bookings).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(bookings);
        }

        // GET: Bookings1/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bookings bookings = db.Bookings.Find(id);
            if (bookings == null)
            {
                return HttpNotFound();
            }
            return View(bookings);
        }

        // POST: Bookings1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Bookings bookings = db.Bookings.Find(id);
            db.Bookings.Remove(bookings);
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
