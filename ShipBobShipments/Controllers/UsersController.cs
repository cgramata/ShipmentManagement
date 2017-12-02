using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using ShipBobShipments.DAL;
using ShipBobShipments.Models;

namespace ShipBobShipments.Controllers
{
    public class UsersController : Controller
    {
        private ShipmentDBContext db = new ShipmentDBContext();

        // Retrieves users for the user index 
        // Users/Index
        public ActionResult Index()
        {
            //possible repository 
            var userList = db.Users.ToList();
            if (userList == null)
            {
                return HttpNotFound();
            }

            var users = userList.Select(v => new {
                UserID = v.UserID,
                UserFirstName = v.UserFirstName,
                UserLastName = v.UserLastName
            });

            ViewBag.UserServerObject = users;
            return View(userList);
        }

        //Redirects to Order's index page with userId
        // Orders/Index
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

        // Retrieves user info before being the edit
        // GET: Users/Edit/
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

            ViewBag.ChosenUserEditObject = new
            {
                UserID = chosenUser.UserID,
                UserFirstName = chosenUser.UserFirstName,
                UserLastName = chosenUser.UserLastName
            };
            return View(chosenUser);
        }

        // POST: Users/Edit/
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

        // Retrieves user information before deletion
        // GET: Users/Delete/
        public ActionResult Delete(int? userId)
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

            ViewBag.ChosenUserDeleteObject = new
            {
                UserID = chosenUser.UserID,
                UserFirstName = chosenUser.UserFirstName,
                UserLastName = chosenUser.UserLastName
            };
            return View(chosenUser);
        }

        // Saves the deletion to the table in the DB
        // POST: Users/Delete/
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
