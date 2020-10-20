using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using VehicleGarage.Data;
using VehicleGarage.Models;
using VehicleGarage.Models.ViewModels;

namespace VehicleGarage.Controllers
{
    public class VehiclesController : Controller
    {
        private readonly VehicleGarageContext _context;

        public VehiclesController(VehicleGarageContext context)
        {
            _context = context;
        }

        // GET: Vehicles
        public async Task<IActionResult> Index()
        {
            return View(await _context.Vehicle.ToListAsync());
        }

        // GET: Vehicles
        public async Task<IActionResult> Vehicles()
        {
            var model = await _context.Vehicle.Select(v => new VehicleViewModel
            {
                Id = v.Id,
                VehicleType = v.VehicleType,
                RegNum = v.RegNum,
                ArrivalTime = v.ArrivalTime,
            }).ToListAsync();

            return View(model);
        }

        // GET: Vehicles/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Vehicles/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,VehicleType,RegNum,Color,Brand,Model,NumWheels,ArrivalTime")] Vehicle vehicle)
        {
            if (ModelState.IsValid)
            {
                vehicle.ArrivalTime = DateTime.Now;
                vehicle.RegNum.ToUpper();
                _context.Add(vehicle);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(vehicle);
        }

        // UniqueRegNum
        [AcceptVerbs("GET", "POST")]
        public IActionResult UniqueRegNum(string regNum)
        {
            if(_context.Vehicle.Any(v => v.RegNum == regNum))
            {
                return Json("That registration number already is among the parked vehicles.");
            }
            return Json(true);
        }

        // GET: Vehicles/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .Select(e => new EditViewModel
                {
                    Id = e.Id,
                    VehicleType = e.VehicleType,
                    Color = e.Color,
                    Brand = e.Brand,
                    Model = e.Model,
                    NumWheels = e.NumWheels
                }).FirstOrDefaultAsync(v => v.Id == id);

            if (vehicle == null)
            {
                return NotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditViewModel editViewModel)
        {
            if (id != editViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var vehicle = new Vehicle
                {
                    Id = editViewModel.Id,
                    VehicleType = editViewModel.VehicleType,
                    Color = editViewModel.Color,
                    Brand = editViewModel.Brand,
                    Model = editViewModel.Model,
                    NumWheels = editViewModel.NumWheels
                };
                try
                {
                    _context.Entry(vehicle).State = EntityState.Modified;
                    _context.Entry(vehicle).Property(v => v.ArrivalTime).IsModified = false;
                    _context.Entry(vehicle).Property(v => v.RegNum).IsModified = false;

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VehicleExists(vehicle.Id))
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
            return View(editViewModel);
        }

        // GET: Vehicles/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle
                .FirstOrDefaultAsync(m => m.Id == id);
            if (vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var vehicle = await _context.Vehicle.FindAsync(id);
            _context.Vehicle.Remove(vehicle);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Receipt
        public async Task<IActionResult> Receipt(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }

            var vehicle = await _context.Vehicle.FirstOrDefaultAsync(m => m.Id == id);

            if(vehicle == null)
            {
                return NotFound();
            }

            return View(vehicle);
        }

        private bool VehicleExists(int id)
        {
            return _context.Vehicle.Any(e => e.Id == id);
        }
    }
}
