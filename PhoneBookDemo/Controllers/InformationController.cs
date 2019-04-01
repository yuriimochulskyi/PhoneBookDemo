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
    public class InformationController : Controller
    {
        private readonly PhoneBookLiteDBContext _context;

        private UnitOfWork unitOfWork;

        public InformationController(PhoneBookLiteDBContext context)
        {
            this.unitOfWork = new UnitOfWork(context);
            _context = context;
        }

        // GET: Information
        public IActionResult Index()
        {
            return View(unitOfWork.InformationRepository.SelectAll());
        }

        // GET: Information/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var information = unitOfWork.InformationRepository.SelectAll()
                                .FirstOrDefault(m => m.Id == id);
            if (information == null)
            {
                return NotFound();
            }

            return View(information);
        }

        // GET: Information/Create
        public IActionResult Create()
        {
            ViewData["PersonId"] = new SelectList(unitOfWork.PersonRepository.SelectAll(), "Id", "FirstName");
            return View();
        }

        // POST: Information/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("Id,PersonId,PhoneNumber")] Information information)
        {
            if (ModelState.IsValid)
            {
                unitOfWork.InformationRepository.Create(information);
                unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PersonId"] = new SelectList(unitOfWork.PersonRepository.SelectAll(), "Id", "FirstName", information.PersonId);
            return View(information);
        }

        // GET: Information/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var information = unitOfWork.InformationRepository.SelectAll().FirstOrDefault(m => m.Id == id);

            if (information == null)
            {
                return NotFound();
            }
            ViewData["PersonId"] = new SelectList(unitOfWork.PersonRepository.SelectAll(), "Id", "FirstName", information.PersonId);
            return View(information);
        }

        // POST: Information/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,PersonId,PhoneNumber")] Information information)
        {
            if (id != information.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    unitOfWork.InformationRepository.Update(information);
                    unitOfWork.Save();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!InformationExists(information.Id))
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
            ViewData["PersonId"] = new SelectList(unitOfWork.PersonRepository.SelectAll(), "Id", "FirstName", information.PersonId);
            return View(information);
        }

        // GET: Information/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var information = unitOfWork.InformationRepository.SelectAll().FirstOrDefault(m => m.Id == id);
            if (information == null)
            {
                return NotFound();
            }

            return View(information);
        }

        // POST: Information/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var information = unitOfWork.InformationRepository.SelectAll().FirstOrDefault(m => m.Id == id);
            unitOfWork.InformationRepository.Delete(information);
            unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        private bool InformationExists(int id)
        {
            if (unitOfWork.InformationRepository.SelectByID(id) == null) return false;
            return true;
        }
    }
}
