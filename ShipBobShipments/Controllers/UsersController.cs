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
using Newtonsoft.Json;
using System.Web.Script.Serialization;

namespace ShipBobShipments.Controllers
{
    public class UsersController : Controller
    {
        private ShipmentDBContext db = new ShipmentDBContext();

        // GET: Users
        public ActionResult Index()
        {
            //possible repository 
            var userList = db.Users.ToList();
        
            var users = userList.Select(v => new {
                UserID = v.UserID,
                UserFirstName = v.UserFirstName,
                UserLastName = v.UserLastName
            });

            ViewBag.UserServerObject = users;
            return View(userList);
        }

        //Redirects to Order's index page with userId
        public ActionResult Orders(int? userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("Index", "Orders", new { userId });
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserID,UserFirstName,UserLastName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(user);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var chosenUser = db.Users.Find(userId);
            if (chosenUser == null)
            {
                return HttpNotFound();
            }

            ViewBag.ChosenUserObject = new {
                UserID = chosenUser.UserID,
                UserFirstName = chosenUser.UserFirstName,
                UserLastName = chosenUser.UserLastName
            };
            return View(chosenUser);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserID,UserFirstName,UserLastName")] User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? userId)
        {
            if (userId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(userId);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int userId)
        {
            User user = db.Users.Find(userId);
            db.Users.Remove(user);
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
