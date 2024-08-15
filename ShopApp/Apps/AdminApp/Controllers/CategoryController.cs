<<<<<<< HEAD
﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
=======
﻿using Microsoft.AspNetCore.Mvc;
>>>>>>> 67bb883dc0e2fe3545e8e8a15568d41751ff03c7
using Microsoft.EntityFrameworkCore;
using ShopApp.Apps.AdminApp.Dtos.CategoryDto;
using ShopApp.Data;
using ShopApp.Entities;

namespace ShopApp.Apps.AdminApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {

        private readonly ShopAppContext _context;
<<<<<<< HEAD
        private readonly IMapper _mapper;

        public CategoryController(ShopAppContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
=======

        public CategoryController(ShopAppContext context)
        {
            _context = context;
>>>>>>> 67bb883dc0e2fe3545e8e8a15568d41751ff03c7
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int? id)
        {
            if (id is null) return BadRequest();

            var existCategory = await _context.Categories
                .Include(c => c.Products)
                .Where(p => !p.isDelete)
<<<<<<< HEAD
                .AsNoTracking()
                .FirstOrDefaultAsync(p => p.Id == id);

            if (existCategory is null) return BadRequest();


            return Ok(_mapper.Map<CategoryReturnDto>(existCategory));
=======

                .FirstOrDefaultAsync(p => p.Id == id);


            CategoryReturnDto category = new()
            {
                Id = existCategory.Id,
                Name = existCategory.Name,
                CreatedDate = existCategory.CreatedDate,
                UpdatedDate = existCategory.UpdateDate,
                ImageUrl = "http://localhost:5001/images/" + existCategory.Image
            };

            return Ok(category);
>>>>>>> 67bb883dc0e2fe3545e8e8a15568d41751ff03c7
        }

        [HttpGet]
        public async Task<IActionResult> Get(string search, int page = 1)
        {
            var query = _context.Categories
                .Where(p => !p.isDelete);

            if (!string.IsNullOrEmpty(search))
            {
                query = query.Where(c => c.Name.ToLower().Contains(search.ToLower()));
            }

            CategoryListDto categoryListDto = new()
            {
                Page = page,
                TotalCount = query.Count(),
                Items = await query.Skip((page - 1) * 2).Take(2)
              .Select(c => new CategoryListItemDto()
              {
                  Id = c.Id,
                  Name = c.Name,
                  CreatedDate = c.CreatedDate,
                  UpdateDate = c.UpdateDate,

              }).ToListAsync()
            };

            return Ok(categoryListDto);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateDto categoryCreateDto)
        {
            var isExist = await _context.Categories
                .AnyAsync(p => !p.isDelete && p.Name.ToLower() == categoryCreateDto.Name.ToLower());


            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(categoryCreateDto.Image.FileName);
            string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
            using FileStream fileStream = new(path, FileMode.Create);
            await categoryCreateDto.Image.CopyToAsync(fileStream);


            var file = categoryCreateDto.Image;
            Category category = new()
            {
                Name = categoryCreateDto.Name.Trim(),
                Image = fileName,
            };

            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();

            return StatusCode(201);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int? id, CategoryUpdateDto categoryUpdateDto)
        {
            if (id is null) return StatusCode(StatusCodes.Status400BadRequest);

            var existCategory = await _context.Categories
                .Where(c => !c.isDelete)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existCategory == null) return NotFound();

            if (categoryUpdateDto.Image != null)
            {
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(categoryUpdateDto.Image.FileName);
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);
                using FileStream fileStream = new(path, FileMode.Create);
                await categoryUpdateDto.Image.CopyToAsync(fileStream);

                existCategory.Image = fileName;
            }

            if (categoryUpdateDto.Name != null)
            {
                existCategory.Name = categoryUpdateDto.Name;
            }

            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Patch(int? id, bool status)
        {
            if (id is null) return BadRequest();

            var existCategory = await _context.Categories
                .Where(c => !c.isDelete)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (existCategory == null) return NotFound();

            existCategory.isDelete = status;
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status204NoContent);

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null) return BadRequest();

            var existCategory = await _context.Categories
                .Where(_c => !_c.isDelete)
                .FirstOrDefaultAsync(_c => _c.Id == id);

            if (existCategory == null) return NotFound();

            _context.Categories.Remove(existCategory);
            await _context.SaveChangesAsync();

            return Ok(StatusCodes.Status200OK);
<<<<<<< HEAD
        }
=======

        }

    
>>>>>>> 67bb883dc0e2fe3545e8e8a15568d41751ff03c7
    }
}
