using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PhoneBookDemo.DAL;
using PhoneBookDemo.Models;

namespace PhoneBookDemo.Controllers
{
    public class PersonController : Controller
    {
        private UnitOfWork unitOfWork;

        public PersonController(PhoneBookLiteDBContext context)
        {
            this.unitOfWork = new UnitOfWork(context);
        }

        // GET: Person
        public IActionResult Index()
        {
            return View(unitOfWork.PersonRepository.SelectAll()); 
        }

        // GET: Person/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            var person =  unitOfWork.PersonRepository.SelectAll().FirstOrDefault(m => m.Id == id);

            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // GET: Person/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Person/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,FirstName,LastName")] Person person)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.PersonRepository.Create(person);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Person/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = unitOfWork.PersonRepository.SelectAll().FirstOrDefault(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        // POST: Person/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,FirstName,LastName")] Person person)
        {
            if (id != person.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.PersonRepository.Update(person);
                    unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PersonExists(person.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(person);
        }

        // GET: Person/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var person = unitOfWork.PersonRepository.SelectAll()
                .FirstOrDefault(m => m.Id == id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        // POST: Person/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var person =  unitOfWork.PersonRepository.SelectAll().FirstOrDefault(m => m.Id == id);
            unitOfWork.PersonRepository.Delete(id);
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool PersonExists(int id)
        {
            if (unitOfWork.PersonRepository.SelectByID(id) == null) return false;
            return true;
        }
    }
}
