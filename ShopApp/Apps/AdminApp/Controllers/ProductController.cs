<<<<<<< HEAD
﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
=======
﻿using Microsoft.AspNetCore.Mvc;
>>>>>>> 67bb883dc0e2fe3545e8e8a15568d41751ff03c7
using Microsoft.EntityFrameworkCore;
using ShopApp.Apps.AdminApp.Dtos.ProductDto;
using ShopApp.Data;
using ShopApp.Entities;

<<<<<<< HEAD
=======


>>>>>>> 67bb883dc0e2fe3545e8e8a15568d41751ff03c7
namespace ShopApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ShopAppContext _context;
<<<<<<< HEAD
        private readonly IMapper _mapper;

        public ProductController(ShopAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
=======

        public ProductController(ShopAppContext context)
        {
            _context = context;
>>>>>>> 67bb883dc0e2fe3545e8e8a15568d41751ff03c7
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id == null) return BadRequest();
            var existProduct = await _context.Products
                .Include(p => p.Category)
                .ThenInclude(p => p.Products)
                .Where(p => !p.isDelete)
                .FirstOrDefaultAsync(x => x.Id == id);
<<<<<<< HEAD

            if (existProduct == null) return NotFound();

            return Ok(_mapper.Map<ProductReturnDto>(existProduct));
        }

        [HttpGet]
        [Authorize(Roles = "member")]
=======
            if (existProduct == null) return NotFound();

            ProductReturnDto returnDto = new()
            {
                Id = existProduct.Id,
                Name = existProduct.Name,
                CostPrice = existProduct.CostPrice,
                SalePrice = existProduct.SalePrice,
                CreatedDate = existProduct.CreatedDate,
                UpdatedDate = existProduct.UpdateDate,
                Category = new()
                {
                    Name = existProduct.Category.Name,
                    ProductCount = existProduct.Category.Products.Count()
                }
            };

            return Ok(returnDto);
        }

        [HttpGet]
>>>>>>> 67bb883dc0e2fe3545e8e8a15568d41751ff03c7
        public async Task<IActionResult> Get(string search, int page = 1)
        {
            var query = _context.Products
                .Where(p => !p.isDelete);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(p => p.Name.ToLower().Contains(search.ToLower()));
            }

            ProductListDto listDto = new()
            {

                Page = page,
                TotalCount = query.Count(),
                Items = await query.Skip((page - 1) * 2).Take(2)
<<<<<<< HEAD
                .Select(c => new ProductListItemDto()
                {
                    Id = c.Id,
                    Name = c.Name,
                    CostPrice = c.CostPrice,
                    SalePrice = c.SalePrice,
                    CreatedDate = c.CreatedDate,
                    UpdatedDate = c.UpdateDate,
                    Category = new()
                    {
                        Name = c.Category.Name,
                        ProductCount = c.Category.Products.Count()
=======
                .Select(p => new ProductListItemDto()
                {
                    Id = p.Id,
                    Name = p.Name,
                    CostPrice = p.CostPrice,
                    SalePrice = p.SalePrice,
                    CreatedDate = p.CreatedDate,
                    UpdatedDate = p.UpdateDate,
                    Category = new()
                    {
                        Name = p.Category.Name,
                        ProductCount = p.Category.Products.Count()
>>>>>>> 67bb883dc0e2fe3545e8e8a15568d41751ff03c7
                    }
                })
                .ToListAsync()
            };
            return Ok(listDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateDto productCreateDto)
        {
            if (!await _context.Categories.AnyAsync(c => !c.isDelete && c.Id == productCreateDto.CategoryId))
                return StatusCode(409);

            Product product = new();

            product.Name = productCreateDto.Name;
            product.SalePrice = productCreateDto.SalePrice;
            product.CostPrice = productCreateDto.CostPrice;
            product.CategoryId = productCreateDto.CategoryId;

            await _context.Products.AddAsync(product);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, ProductUpdateDto productUpdateDto)
        {
            if (id is null) return StatusCode(StatusCodes.Status400BadRequest);

            var existProduct = await _context.Products
                .Where(p => !p.isDelete)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existProduct == null) return NotFound();

            if (!await _context.Categories.AnyAsync(c => !c.isDelete && c.Id == productUpdateDto.CategoryId))
                return StatusCode(409);

            existProduct.Name = productUpdateDto.Name;
            existProduct.SalePrice = productUpdateDto.SalePrice;
            existProduct.CostPrice = productUpdateDto.CostPrice;
            existProduct.CategoryId = productUpdateDto.CategoryId;
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int id, bool status)
        {
            var existProduct = await _context.Products
                .Where(p => !p.isDelete)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (existProduct == null) return NotFound();
            existProduct.isDelete = status;
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpDelete("{id}")]
<<<<<<< HEAD
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null) return NotFound();
=======
        public async Task<IActionResult> Delete(bool status, int id)
        {
>>>>>>> 67bb883dc0e2fe3545e8e8a15568d41751ff03c7
            var existProduct = await _context.Products
                .Where(p => !p.isDelete)
                .FirstOrDefaultAsync(x => x.Id == id);


            if (existProduct == null) return NotFound();
            _context.Products.Remove(existProduct);

            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
