using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApi;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChecklistController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        public ChecklistController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/CheckListItems
        [HttpGet]
        public async Task<IEnumerable<CheckListItem>> Get()
        {
            return await _context.CheckListItems.ToListAsync();
        }

        // GET: api/CheckListItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<CheckListItem>> Get (int id)
        {
            var checkListItem = await _context.CheckListItems.FindAsync(id);

            if (checkListItem == null)
            {
                return NotFound();
            }

            return checkListItem;
        }

        // PUT: api/CheckListItems/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<bool> Update (int id, CheckListItem checkListItem)
        {
            if (id != checkListItem.Id)
            {
                return false;
            }

            _context.Entry(checkListItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CheckListItemExists(id))
                {
                    return false;
                }
                else
                {
                    throw;
                }
            }

            return true;
        }

        // POST: api/CheckListItems
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<int> Create (CheckListItem checkListItem)
        {
            _context.CheckListItems.Add(checkListItem);
            await _context.SaveChangesAsync();

            return checkListItem.Id;
        }

        // DELETE: api/CheckListItems/5
        [HttpDelete("{id}")]
        public async Task<bool> Delete (int id)
        {
            var checkListItem = await _context.CheckListItems.FindAsync(id);
            if (checkListItem == null)
            {
                return false;
            }

            _context.CheckListItems.Remove(checkListItem);
            var result = await _context.SaveChangesAsync();

            return result > 0;
        }

        private bool CheckListItemExists(int id)
        {
            return _context.CheckListItems.Any(e => e.Id == id);
        }
    }
}
