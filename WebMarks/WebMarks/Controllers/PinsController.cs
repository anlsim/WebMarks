using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PinBoard.Data;
using PinBoard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace PinBoard.Controllers
{
   [Authorize]
    public class PinsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;


        public PinsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // GET: Pins
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            return View(await _context.Pin.Where(u => u.User.Id == user.Id).ToListAsync());
           // return View(await _context.Pin.ToListAsync());
        }

        // GET: Pins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pin = await _context.Pin
                .FirstOrDefaultAsync(m => m.Id == id);
            if (pin == null)
            {
                return NotFound();
            }

            return View(pin);
        }

        // GET: Pins/Create
        public async Task<IActionResult> CreateAsync()
        {

            CreatePinViewModel createViewModel = new CreatePinViewModel();
            var currentUser = await _userManager.GetUserAsync(User);
            createViewModel.Boards = _context.Board.Where(u => u.User.Id == currentUser.Id).ToList();
            if (createViewModel.Boards.Count != 0)
            {
                return View(createViewModel);
            }
            return RedirectToAction(nameof(Index), "Boards");
        }

        // POST: Pins/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Url,BoardId")] CreatePinViewModel createPinView)
        {
            
            if (ModelState.IsValid)
            {
                Pin newPin = new Pin(createPinView.Url);
                newPin.GetMeta(newPin.Url);
                newPin.User = await _userManager.GetUserAsync(User);
                newPin.Board = _context.Board.Find(createPinView.BoardId);
                _context.Add(newPin);
                await _context.SaveChangesAsync();
                
                return RedirectToAction(nameof(Details), "Boards", newPin.Board);
            }
            return View(createPinView);
        }

        // GET: Pins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
           // DetailsBoardView detailsModel = new DetailsBoardView();

            
            //var pin = new Pin (_context.Pins.Where(p => p.Id == id));

            if (id == null)
            {
                return NotFound();
            }

            Pin pin = await _context.Pin
              .FirstOrDefaultAsync(m => m.Id == id);
            pin.User = await _userManager.GetUserAsync(User);
            //pin.Board = _context.Board.Find(id.BoardId);

            // var currentUser = await _userManager.GetUserAsync(User);

            //pin.User = await _userManager.GetUserAsync(User);
            //var boardId = pin.getBoardId();
            pin.Board = await _context.Board
               .FirstOrDefaultAsync(m => m.Id == id);
            //detailsModel.PinId = (int)id;
            //var board = await _context.Board.FindAsync()
     

            if (pin == null)
            {
                return NotFound();
            }
            return View(pin);
        }

        // POST: Pins/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Url,Image,Title,Description,SiteName,Tags")] Pin pin)
        {
            if (id != pin.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                 
                    pin.Board = _context.Board.Find(pin.Board);
                    _context.Update(pin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PinExists(pin.Id))
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
            return View(pin);
        }

        // GET: Pins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
          
            var pin = await _context.Pin.FindAsync(id);
            if (pin == null)
            {
                return NotFound();
            }

            return View(pin);
        }

        // POST: Pins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var pin = await _context.Pin.FindAsync(id);
            _context.Pin.Remove(pin);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PinExists(int id)
        {
            return _context.Pin.Any(e => e.Id == id);
        }
    }
}
