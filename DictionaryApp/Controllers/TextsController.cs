using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DictionaryApp.Data;
using DictionaryApp.Models;

namespace DictionaryApp.Controllers
{
    public class TextsController : Controller
    {
        private readonly DictionaryAppContext _context;

        public TextsController(DictionaryAppContext context)
        {
            _context = context;
        }

        // GET: Texts
        public async Task<IActionResult> Index(string searchString)
        {
            if (_context.Text == null)
            {
                return Problem("Entity set 'MvcMovieContext.Movie'  is null.");
            }

            var texts = from m in _context.Text
                        select m;

            if (!String.IsNullOrEmpty(searchString))
            {
                if (CharacterChecker.ContainsNepaliCharacters(searchString))
                    {
                    texts = texts.Where(s => s.Word!.Contains(searchString));
                }
                else
                {
                    texts = texts.Where(s => s.Definition!.Contains(searchString));
                }
    
            }

            return View(await texts.ToListAsync());
        }

        // GET: Texts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Text == null)
            {
                return NotFound();
            }

            var text = await _context.Text
                .FirstOrDefaultAsync(m => m.Id == id);
            if (text == null)
            {
                return NotFound();
            }

            return View(text);
        }

        // GET: Texts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Texts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Word,Pronunciation,Definition")] Text text)
        {
            if (ModelState.IsValid)
            {
                _context.Add(text);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(text);
        }

        // GET: Texts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Text == null)
            {
                return NotFound();
            }

            var text = await _context.Text.FindAsync(id);
            if (text == null)
            {
                return NotFound();
            }
            return View(text);
        }

        // POST: Texts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Word,Definition")] Text text)
        {
            if (id != text.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(text);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TextExists(text.Id))
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
            return View(text);
        }

        // GET: Texts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Text == null)
            {
                return NotFound();
            }

            var text = await _context.Text
                .FirstOrDefaultAsync(m => m.Id == id);
            if (text == null)
            {
                return NotFound();
            }

            return View(text);
        }

        // POST: Texts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Text == null)
            {
                return Problem("Entity set 'DictionaryAppContext.Text'  is null.");
            }
            var text = await _context.Text.FindAsync(id);
            if (text != null)
            {
                _context.Text.Remove(text);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TextExists(int id)
        {
            return (_context.Text?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public class CharacterChecker
        {
            public static bool ContainsNepaliCharacters(string text)
            {
                foreach (char c in text)
                {
                    int codePoint = Char.ConvertToUtf32(c.ToString(), 0);
                    if (0x0900 <= codePoint && codePoint <= 0x097F)
                    {
                        return true;
                    }
                }
                return false;
            }

            public static bool ContainsEnglishCharacters(string text)
            {
                foreach (char c in text)
                {
                    int codePoint = Char.ConvertToUtf32(c.ToString(), 0);
                    if ((0x0000 <= codePoint && codePoint <= 0x007F) ||
                        (0x0080 <= codePoint && codePoint <= 0x00FF) ||
                        (0x0100 <= codePoint && codePoint <= 0x017F) ||
                        (0x0180 <= codePoint && codePoint <= 0x024F))
                    {
                        return true;
                    }
                }
                return false;
            }

        }
    }
    }
