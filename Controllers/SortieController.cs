using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockAPI.Data;
using StockAPI.Models;

namespace StockAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SortieController : ControllerBase
    {

        private readonly DataContext _context;
        public SortieController(DataContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<ActionResult<List<Sortie>>> AddSortie(Sortie sortie){
            _context.Sorties.Add(sortie);
            await _context.SaveChangesAsync();

            return Ok(await _context.Sorties.ToListAsync());
        }

        [HttpGet]
        public async Task<ActionResult<List<Sortie>>> GetSorties(){
            return Ok(await _context.Sorties.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Sortie>>> GetSortie(string id){
            var sortie = await _context.Sorties.FindAsync(id);
            if(sortie == null){
                return BadRequest("Sortie not found");
            }
            return Ok(sortie);
        }
    }
}