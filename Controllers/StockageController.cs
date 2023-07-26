using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StockAPI.Data;
using StockAPI.Models;
using StockAPI.StructureJSON;

namespace StockAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StockageController : ControllerBase
    {
        private readonly DataContext _context;
        public StockageController(DataContext context)
        {
            _context = context;   
        }

        [HttpPost]
        public async Task<ActionResult<Stockage>> PostStockage(StockageInputModel destockageInput)
        {
            try
            {
                // Create a new Destocker object based on the input model
                Stockage stockage = new Stockage
                {
                    num_produit = destockageInput.num_produit,
                    num_bon_liv = destockageInput.num_bon_liv,
                    quantite_entree = destockageInput.quantite_entree
                };

                // Add the new destocker object to the database context and save changes
                _context.Stockages.Add(stockage);
                await _context.SaveChangesAsync();

                // Return the created destocker object as a response
                return CreatedAtAction("GetStockage", new { id = stockage.num_bon_liv }, stockage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // [HttpGet]
        // public async Task<ActionResult<List<Stockage>>> GetStockage(){
        //     return Ok(await _context.Stockages.ToListAsync());
        // }

        [HttpGet]
        public ActionResult<IEnumerable<StockageGet>> GetStockages()
        {
            try
            {
                var stockages = _context.Stockages
                    .Select(s => new StockageGet
                    {
                        num_produit = s.num_produit,
                        num_bon_liv = s.num_bon_liv,
                        quantite_entree = s.quantite_entree
                    })
                    .ToList();

                return Ok(stockages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // [HttpGet("{id}")]
        // public async Task<ActionResult<List<Stockage>>> GetStockageById(string id){
        //     var stockage = await _context.Stockages.FindAsync(id);
        //     if(stockage == null){
        //         return BadRequest("Stockage not found");
        //     }
        //     return Ok(stockage);
        // }

        [HttpGet("{id}")]
        public async Task<ActionResult<StockageGet>> GetStockage(string id)
        {
            var stockage = await _context.Stockages
                .Where(d => d.num_bon_liv == id)
                .Select(d => new StockageGet
                {
                    num_produit = d.num_produit,
                    num_bon_liv = d.num_bon_liv,
                    quantite_entree = d.quantite_entree
                })
                .FirstOrDefaultAsync();

            if (stockage == null)
            {
                return NotFound();
            }

            return Ok(stockage);
        }
    }
}