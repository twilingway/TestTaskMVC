using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TestTaskMVC.Data;
using TestTaskMVC.Models;

namespace TestTaskMVC.Controllers
{
    public class UsersController : Controller
    {
        private readonly TestTaskMVCContext _context;

        private Log _log;
        private DateTime _birthdays;
        private int _id;
        private bool _isMale;

        public UsersController(TestTaskMVCContext context, Log log)
        {
            _context = context;
            _log = log;
        }
        public async Task<IActionResult> Index(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var userSearchVM = new UserSearchViewModel
            {
                CurrentSort = sortOrder,
                IDSort = sortOrder == "ID" ? "id_desc" : "ID",
                NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "",
                DateSort = sortOrder == "Date" ? "date_desc" : "Date",
                GenderSort = sortOrder == "IsMale" ? "gender_desc" : "IsMale",
                RequestSort = sortOrder == "Request" ? "request_desc" : "Request",
            };
            
            if (searchString != currentFilter)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            userSearchVM.CurrentFilter = searchString;

            var users = from u in _context.User
                        select u;

            if (!String.IsNullOrEmpty(searchString))
            {
                if (DateTime.TryParse(searchString, out _birthdays))
                {
                    string.Format("{0:d.MM.yyyy}", _birthdays);
                    users = users.Where(s => s.Birthday == _birthdays);
                }
                else if (int.TryParse(searchString, out _id))
                {
                    users = users.Where(s => s.ID == _id || s.Request == _id);
                }
                else if (bool.TryParse(searchString, out _isMale))
                {
                    users = users.Where(s => s.IsMale == _isMale);
                }
                else
                {
                    users = users.Where(s => s.Name.Contains(searchString));
                }
            }

            switch (sortOrder)
            {
                case "ID":
                    users = users.OrderBy(s => s.ID);
                    break;
                case "id_desc":
                    users = users.OrderByDescending(s => s.ID);
                    break;
                case "name_desc":
                    users = users.OrderByDescending(s => s.Name);
                    break;
                case "Date":
                    users = users.OrderBy(s => s.Birthday);
                    break;
                case "date_desc":
                    users = users.OrderByDescending(s => s.Birthday);
                    break;
                case "IsMale":
                    users = users.OrderBy(s => s.IsMale);
                    break;
                case "gender_desc":
                    users = users.OrderByDescending(s => s.IsMale);
                    break;
                case "Request":
                    users = users.OrderBy(s => s.Request);
                    break;
                case "request_desc":
                    users = users.OrderByDescending(s => s.Request);
                    break;
                default:
                    users = users.OrderBy(s => s.Name);
                    break;
            }
            int pageSize = 10;
            userSearchVM.Users = await PaginatedList<User>.CreateAsync(users.AsNoTracking(), pageNumber ?? 1, pageSize);

            if (!String.IsNullOrEmpty(searchString) && !String.IsNullOrEmpty(sortOrder))
            {
                _log.WriteInfo($"Администратор произвел фильтрацию по {searchString} и сортировку по {sortOrder}");
            }
            else if (!String.IsNullOrEmpty(sortOrder))
            {
                _log.WriteInfo($"Администратор произвел сортировку по {sortOrder}");
            }
            else if (!String.IsNullOrEmpty(searchString))
            {
                _log.WriteInfo($"Администратор произвел фильтрацию по {searchString}");
            }

            return View(userSearchVM);
        }


        // GET: Users/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Birthday,IsMale,Request")] User user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(user);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
            return View(user);
        }

        // GET: Users/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Name,Birthday,IsMale,Request")] User user)
        {
            if (id != user.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.ID))
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
            return View(user);
        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.User
                .FirstOrDefaultAsync(m => m.ID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.User.FindAsync(id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // POST: Log/Delete/5
        [HttpPost, ActionName("DeleteAll")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteAll()
        {
            if (_context.User == null)
            {
                return RedirectToAction(nameof(Index));
            }
            try
            {
                _context.User.RemoveRange(_context.User);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.)
                return RedirectToAction(nameof(DeleteAll));
            }
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(e => e.ID == id);
        }
    }
}
