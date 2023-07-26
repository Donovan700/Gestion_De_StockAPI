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
    public class ProductController : ControllerBase
    {
        private readonly DataContext _context;
        public ProductController(DataContext context)
        {
            _context = context;   
        }

        // [HttpPost]
        // public async Task<ActionResult<List<Product>>> AddProduct(Product product){
        //     _context.Products.Add(product);
        //     await _context.SaveChangesAsync();

        //     return Ok(await _context.Products.ToListAsync());
        // }

        [HttpPost]
        public async Task<ActionResult<Product>> PostProduct(ProductInputModel ProductInput)
        {
            try
            {
                // Create a new Destocker object based on the input model
                Product product = new Product
                {
                    num_produit = ProductInput.num_produit,
                    design = ProductInput.design,
                    description = ProductInput.description,
                    image = ProductInput.image
                };

                // Add the new destocker object to the database context and save changes
                _context.Products.Add(product);
                await _context.SaveChangesAsync();

                // Return the created destocker object as a response
                return CreatedAtAction("GetProduct", new { id = product.num_produit }, product);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Product>>> GetAllProducts(){
            return Ok(await _context.Products.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Product>>> GetProductById(string id){
            var product = await _context.Products.FindAsync(id);
            if(product == null){
                return BadRequest("Product not found.");
            }
            return Ok(product);
        } 

        [HttpPut]
        public async Task<ActionResult<List<Product>>> PutAsync(string id,Product product){
            _context.Products.Update(product);
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Product>>> DeleteProductById(string id){
            var product = await _context.Products.FindAsync(id);
            if(product == null){
                return BadRequest("Product whith number: "+id+" doesn't exist.");
            }
            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return Ok("Product removed successfully");    
        }
    }
}