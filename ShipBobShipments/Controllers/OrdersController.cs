using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ShipBobShipments.DAL;
using ShipBobShipments.Models;

namespace ShipBobShipments.Controllers
{
    public class OrdersController : Controller
    {
        private ShipmentDBContext db = new ShipmentDBContext();

        // GET: Orders
        public ActionResult Index(int? userId)
        {
            var orders = from o in db.Orders select o;
            if (userId == null)
            {
                orders = db.Orders.Include(o => o.User);
                return View(orders.ToList());
            }

            ViewBag.userId = userId;
            orders = orders.Where(o => o.UserID == userId);
            return View(orders);         
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            return View(orders);
        }

        // GET: Orders/Create
        public ActionResult Create(int? userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


            User users = db.Users.Find(userId);
            ViewBag.userName = users.UserFirstName + " " + users.UserLastName;
            ViewBag.userIdNum = userId;
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserFirstName");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderID,UserID,TrackingId,RecipientName,StreetAddress,City,State,Zipcode")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(orders);
                db.SaveChanges();
                return RedirectToAction("Index", "Orders", new { userId = orders.UserID });
            }

            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserFirstName", orders.UserID);
            return View(orders);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Orders orders = db.Orders.Find(id);
            User users = db.Users.Find(orders.UserID);
            if (orders == null || users == null)
            {
                return HttpNotFound();
            }
            ViewBag.userName = users.UserFirstName + " " + users.UserLastName;
            ViewBag.userIdNum = users.UserID;
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserFirstName", orders.UserID);
            return View(orders);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderID,UserID,TrackingId,RecipientName,StreetAddress,City,State,Zipcode")] Orders orders)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orders).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index", "Orders", new { userId = orders.UserID});
            }
            ViewBag.UserID = new SelectList(db.Users, "UserID", "UserFirstName", orders.UserID);
            return View(orders);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Orders orders = db.Orders.Find(id);
            if (orders == null)
            {
                return HttpNotFound();
            }
            ViewBag.userId = orders.UserID;
            return View(orders);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Orders orders = db.Orders.Find(id);
            int userId = orders.UserID;
            db.Orders.Remove(orders);
            db.SaveChanges();
            return RedirectToAction("Index", "Orders", new {userId});
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
