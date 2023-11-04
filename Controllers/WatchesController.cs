using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MyWatchListWebApp.Data;
using MyWatchListWebApp.Models;

namespace MyWatchListWebApp.Controllers
{
    public class WatchesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public WatchesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Watches
        public async Task<IActionResult> Index()
        {
              return _context.Watch != null ? 
                          View(await _context.Watch.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Watch'  is null.");
        }

        // GET: Watches/ShowSearchForm
        public async Task<IActionResult> ShowSearchForm()
        {
            return _context.Watch != null ?
                        View() :
                        Problem("ShowSearchForm is null.");
        }

        // POST: Watches/ShowSearchResults
        public async Task<IActionResult> ShowSearchResults(string SearchReferenceNumber, string SearchBrand, string SearchModel, string SearchMovement, string SearchCaseMaterial, string SearchBandMaterial, string SearchDialColor, string SearchBraceletColor, double SearchPowerReserve, double SearchCaseDiameter, double SearchLugToLugWidth, double SearchThickness)
        //public async Task<IActionResult> ShowSearchResults()
        {
            //var filteredList = GetFilteredWatchList(await _context.Watch.ToListAsync(), SearchReferenceNumber, SearchBrand, SearchModel, SearchMovement, SearchCaseMaterial, SearchBandMaterial, SearchDialColor, SearchBraceletColor, SearchPowerReserve, SearchCaseDiameter, SearchLugToLugWidth, SearchThickness);

            return _context.Watch != null ?
                        View("Index", await _context.Watch.Where(
                            w => 
                                (string.IsNullOrEmpty(SearchReferenceNumber) || w.ReferenceNumber.Contains(SearchReferenceNumber)) &&
                                (string.IsNullOrEmpty(SearchBrand) || w.Brand.Contains(SearchBrand)) &&
                                (string.IsNullOrEmpty(SearchModel) || w.Model.Contains(SearchModel)) &&
                                (string.IsNullOrEmpty(SearchMovement) || w.Movement.Contains(SearchMovement)) &&
                                (string.IsNullOrEmpty(SearchCaseMaterial) || w.CaseMaterial.Contains(SearchCaseMaterial)) &&
                                (string.IsNullOrEmpty(SearchBandMaterial) || w.BandMaterial.Contains(SearchBandMaterial)) &&
                                (string.IsNullOrEmpty(SearchDialColor) || w.DialColor.Contains(SearchDialColor)) &&
                                (string.IsNullOrEmpty(SearchBraceletColor) || w.BraceletColor.Contains(SearchBraceletColor))
                            ).ToListAsync()) :
                        Problem("ShowSearchResults is null.");
        }

        // GET: Watches/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null || _context.Watch == null)
            {
                return NotFound();
            }

            var watch = await _context.Watch
                .FirstOrDefaultAsync(m => m.ReferenceNumber == id);
            if (watch == null)
            {
                return NotFound();
            }

            return View(watch);
        }

        // GET: Watches/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Watches/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReferenceNumber,Brand,Model,Movement,CaseMaterial,BandMaterial,DialColor,BraceletColor,ImagePath,PowerReserve,CaseDiameter,LugToLugWidth,Thickness")] Watch watch)
        {
            if (ModelState.IsValid)
            {
                _context.Add(watch);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(watch);
        }

        // GET: Watches/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null || _context.Watch == null)
            {
                return NotFound();
            }

            var watch = await _context.Watch.FindAsync(id);
            if (watch == null)
            {
                return NotFound();
            }
            return View(watch);
        }

        // POST: Watches/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ReferenceNumber,Brand,Model,Movement,CaseMaterial,BandMaterial,DialColor,BraceletColor,ImagePath,PowerReserve,CaseDiameter,LugToLugWidth,Thickness")] Watch watch)
        {
            if (id != watch.ReferenceNumber)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(watch);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!WatchExists(watch.ReferenceNumber))
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
            return View(watch);
        }

        // GET: Watches/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null || _context.Watch == null)
            {
                return NotFound();
            }

            var watch = await _context.Watch
                .FirstOrDefaultAsync(m => m.ReferenceNumber == id);
            if (watch == null)
            {
                return NotFound();
            }

            return View(watch);
        }

        // POST: Watches/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            if (_context.Watch == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Watch'  is null.");
            }
            var watch = await _context.Watch.FindAsync(id);
            if (watch != null)
            {
                _context.Watch.Remove(watch);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool WatchExists(string id)
        {
          return (_context.Watch?.Any(e => e.ReferenceNumber == id)).GetValueOrDefault();
        }

        private List<Watch> GetFilteredWatchList(List<Watch> results, string SearchReferenceNumber, string SearchBrand, string SearchModel, string SearchMovement, string SearchCaseMaterial, string SearchBandMaterial, string SearchDialColor, string SearchBraceletColor, double SearchPowerReserve, double SearchCaseDiameter, double SearchLugToLugWidth, double SearchThickness)
        {
            List<Watch> query = results.ToList();

            if (!string.IsNullOrWhiteSpace(SearchReferenceNumber))
            {
                query = (List<Watch>)query.Where(w => w.ReferenceNumber != null && w.ReferenceNumber.Contains(SearchReferenceNumber));
            }
            if (!string.IsNullOrWhiteSpace(SearchBrand))
            {
                query = (List<Watch>)query.Where(w => w.Brand != null && w.Brand.Contains(SearchBrand));
            }
            if (!string.IsNullOrWhiteSpace(SearchModel))
            {
                query = (List<Watch>)query.Where(w => w.Model != null && w.Model.Contains(SearchModel));
            }
            if (!string.IsNullOrWhiteSpace(SearchMovement))
            {
                query = (List<Watch>)query.Where(w => w.Movement != null && w.Movement.Contains(SearchMovement));
            }
            if (!string.IsNullOrWhiteSpace(SearchCaseMaterial))
            {
                query = (List<Watch>)query.Where(w => w.CaseMaterial != null && w.CaseMaterial.Contains(SearchCaseMaterial));
            }
            if (!string.IsNullOrWhiteSpace(SearchBandMaterial))
            {
                query = (List<Watch>)query.Where(w => w.BandMaterial != null && w.BandMaterial.Contains(SearchBandMaterial));
            }
            if (!string.IsNullOrWhiteSpace(SearchDialColor))
            {
                query = (List<Watch>)query.Where(w => w.DialColor != null && w.DialColor.Contains(SearchDialColor));
            }
            if (!string.IsNullOrWhiteSpace(SearchBraceletColor))
            {
                query = (List<Watch>)query.Where(w => w.BraceletColor != null && w.BraceletColor.Contains(SearchBraceletColor));
            }

            return query;
        }
    }
}
