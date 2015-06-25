﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Backend.Models;
using Microsoft.AspNet.Identity;

namespace Backend.Controllers
{
    public class MealPlansController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        protected UserManager<ApplicationUser> UserManger { get; set; }
        // GET: api/MealPlans
        public IQueryable<MealPlan> GetMealPlans()
        {
            return db.MealPlans;
        }

        // GET: api/MealPlans
        //public MealPlan GetMealPlansForLoggedInUser()
        //{
        //    var currentUser = UserManger.FindById(User.Identity.GetUserId());
        //    return db.MealPlans.Where(w => w.User == currentUser).FirstOrDefault();
            
        //}

        // GET: api/MealPlans/5
        [ResponseType(typeof(MealPlan))]
        public IHttpActionResult GetMealPlan(int id)
        {
            MealPlan mealPlan = db.MealPlans.Find(id);
            if (mealPlan == null)
            {
                return NotFound();
            }

            return Ok(mealPlan);
        }

        // PUT: api/MealPlans/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutMealPlan(int id, MealPlan mealPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mealPlan.Id)
            {
                return BadRequest();
            }

            db.Entry(mealPlan).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MealPlanExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/MealPlans
        [ResponseType(typeof(MealPlan))]
        public IHttpActionResult PostMealPlan(MealPlan mealPlan)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.MealPlans.Add(mealPlan);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mealPlan.Id }, mealPlan);
        }

        // DELETE: api/MealPlans/5
        [ResponseType(typeof(MealPlan))]
        public IHttpActionResult DeleteMealPlan(int id)
        {
            MealPlan mealPlan = db.MealPlans.Find(id);
            if (mealPlan == null)
            {
                return NotFound();
            }

            db.MealPlans.Remove(mealPlan);
            db.SaveChanges();

            return Ok(mealPlan);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MealPlanExists(int id)
        {
            return db.MealPlans.Count(e => e.Id == id) > 0;
        }
    }
}