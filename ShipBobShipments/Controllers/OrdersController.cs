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

        // GET: Orders specific to the user
        public ActionResult Index(int? userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var chosenUser = db.Users.Find(userId);
            var userOrderList = db.Orders.Where(order => order.UserID == userId);
            if (chosenUser == null || userOrderList == null)
            {
                return HttpNotFound();
            }

            var orders = userOrderList.Select(order => new {
                OrderID = order.OrderID,
                TrackingId = order.TrackingId,
                RecipientName = order.RecipientName,
                StreetAddress = order.StreetAddress,
                City = order.City,
                State = order.State,
                Zipcode = order.Zipcode
            });

            ViewBag.ChosenUserOrdersListObject = orders;
            ViewBag.ChosenUserDetailsObject = new
            {
                UserID = chosenUser.UserID,
                UserFirstName = chosenUser.UserFirstName,
                UserLastName = chosenUser.UserLastName
            };
            return View(userOrderList);         
        }

        // GET: Orders/Details/5
        public ActionResult Details(int? orderId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var chosenOrder = db.Orders.Find(orderId);
            if (chosenOrder == null)
            {
                return HttpNotFound();
            }

            var chosenUser = db.Users.Find(chosenOrder.UserID);
            ViewBag.ChosenUserDetailsObject = new
            {
                UserID = chosenUser.UserID,
                UserFirstName = chosenUser.UserFirstName,
                UserLastName = chosenUser.UserLastName
            };
            ViewBag.ChosenOrdersDetailsObject = new
            {
                OrderID = chosenOrder.OrderID,
                TrackingId = chosenOrder.TrackingId,
                RecipientName = chosenOrder.RecipientName,
                StreetAddress = chosenOrder.StreetAddress,
                City = chosenOrder.City,
                State = chosenOrder.State,
                Zipcode = chosenOrder.Zipcode
            };
            return View(chosenOrder);
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
        public ActionResult Edit(int? orderId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Orders orders = db.Orders.Find(orderId);
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
        public ActionResult Delete(int? orderId)
        {
            if (orderId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            
            var chosenOrder = db.Orders.Find(orderId);
            if (chosenOrder == null)
            {
                return HttpNotFound();
            }

            var chosenUser = db.Users.Find(chosenOrder.UserID);
            ViewBag.ChosenUserDeleteObject = new {
                UserID = chosenUser.UserID,
                UserFirstName = chosenUser.UserFirstName,
                UserLastName = chosenUser.UserLastName
            };
            ViewBag.ChosenOrdersDeleteObject = new
            {
                TrackingId = chosenOrder.TrackingId,
                RecipientName = chosenOrder.RecipientName,
                StreetAddress = chosenOrder.StreetAddress,
                City = chosenOrder.City,
                State = chosenOrder.State,
                Zipcode = chosenOrder.Zipcode
            };
            return View(chosenOrder);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int orderId)
        {
            Orders orders = db.Orders.Find(orderId);
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
