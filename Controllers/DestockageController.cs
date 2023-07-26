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
    public class DestockageController : ControllerBase
    {
        private readonly DataContext _context;
        public DestockageController(DataContext context)
        {
            _context = context;   
        }

        // [HttpPost]
        // public async Task<ActionResult<List<Destockage>>> AddInStock(Destockage destock){
            
        //     if(!ModelState.IsValid){
        //         return BadRequest(ModelState);
        //     }
            
        //     _context.Destockages.Add(destock);
        //     await _context.SaveChangesAsync();

        //     return Ok(await _context.Destockages.ToListAsync());
        // }

        [HttpPost]
        public async Task<ActionResult<Destockage>> PostDestockage(DestockageInputModel destockageInput)
        {
            try
            {
                // Create a new Destocker object based on the input model
                Destockage destockage = new Destockage
                {
                    num_produit = destockageInput.num_produit,
                    num_facture = destockageInput.num_facture,
                    quantite_sortie = destockageInput.quantite_sortie
                };

                // Add the new destocker object to the database context and save changes
                _context.Destockages.Add(destockage);
                await _context.SaveChangesAsync();

                // Return the created destocker object as a response
                return CreatedAtAction("GetDestockage", new { id = destockage.num_facture }, destockage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // [HttpGet]
        // public async Task<ActionResult<List<Destockage>>> GetDestockage(){
        //     return Ok(await _context.Destockages.ToListAsync());
        // }

        [HttpGet]
        public ActionResult<IEnumerable<DestockageGet>> GetDestockages()
        {
            try
            {
                var destockages = _context.Destockages
                    .Select(d => new DestockageGet
                    {
                        num_produit = d.num_produit,
                        num_facture = d.num_facture,
                        quantite_sortie = d.quantite_sortie
                    })
                    .ToList();

                return Ok(destockages);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DestockageGet>> GetDestockage(string id)
        {
            var destockage = await _context.Destockages
                .Where(d => d.num_facture == id)
                .Select(d => new DestockageGet
                {
                    num_produit = d.num_produit,
                    num_facture = d.num_facture,
                    quantite_sortie = d.quantite_sortie
                })
                .FirstOrDefaultAsync();

            if (destockage == null)
            {
                return NotFound();
            }

            return Ok(destockage);
        }
    }
}