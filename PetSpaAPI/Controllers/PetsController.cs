using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using PetSpaAPI.Data;
using PetSpaAPI.Models;
using PetSpaAPI.DTOs.Pet;
using PetSpaAPI.DTOs.Common;

namespace PetSpaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class PetsController : ControllerBase
    {
        private readonly PetSpaDbContext _context;

        public PetsController(PetSpaDbContext context)
        {
            _context = context;
        }

        // GET: api/pets
        [HttpGet]
        public async Task<ActionResult<ResponseDto<List<PetDto>>>> GetPets([FromQuery] int? customerId = null)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);

            IQueryable<Pet> query = _context.Pets;

            if (userRole == "customer")
            {
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
                if (customer == null)
                    return NotFound(ResponseDto<List<PetDto>>.ErrorResponse("Thông tin khách hàng không tồn tại"));

                query = query.Where(p => p.CustomerId == customer.CustomerId);
            }
            else if (customerId.HasValue)
            {
                query = query.Where(p => p.CustomerId == customerId.Value);
            }

            var pets = await query
                .Select(p => new PetDto
                {
                    PetId = p.PetId,
                    CustomerId = p.CustomerId,
                    Name = p.Name,
                    Type = p.Type,
                    Breed = p.Breed,
                    Age = p.Age,
                    ImageUrl = p.ImageUrl
                })
                .ToListAsync();

            return Ok(ResponseDto<List<PetDto>>.SuccessResponse(pets));
        }

        // GET: api/pets/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ResponseDto<PetDto>>> GetPet(int id)
        {
            var pet = await _context.Pets.FindAsync(id);

            if (pet == null)
                return NotFound(ResponseDto<PetDto>.ErrorResponse("Thú cưng không tồn tại"));

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole == "customer")
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
                
                if (customer == null || pet.CustomerId != customer.CustomerId)
                    return Forbid();
            }

            var petDto = new PetDto
            {
                PetId = pet.PetId,
                CustomerId = pet.CustomerId,
                Name = pet.Name,
                Type = pet.Type,
                Breed = pet.Breed,
                Age = pet.Age,
                ImageUrl = pet.ImageUrl
            };

            return Ok(ResponseDto<PetDto>.SuccessResponse(petDto));
        }

        // POST: api/pets
        [HttpPost]
        [Authorize(Roles = "customer")]
        public async Task<ActionResult<ResponseDto<PetDto>>> CreatePet([FromBody] CreatePetDto dto)
        {
            var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
            var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);

            if (customer == null)
                return NotFound(ResponseDto<PetDto>.ErrorResponse("Thông tin khách hàng không tồn tại"));

            var pet = new Pet
            {
                CustomerId = customer.CustomerId,
                Name = dto.Name,
                Type = dto.Type,
                Breed = dto.Breed,
                Age = dto.Age,
                ImageUrl = dto.ImageUrl
            };

            _context.Pets.Add(pet);
            await _context.SaveChangesAsync();

            var petDto = new PetDto
            {
                PetId = pet.PetId,
                CustomerId = pet.CustomerId,
                Name = pet.Name,
                Type = pet.Type,
                Breed = pet.Breed,
                Age = pet.Age,
                ImageUrl = pet.ImageUrl
            };

            return CreatedAtAction(nameof(GetPet), new { id = pet.PetId },
                ResponseDto<PetDto>.SuccessResponse(petDto, "Thêm thú cưng thành công"));
        }

        // POST: api/pets/5/update
        [HttpPost("{id}/update")]
        public async Task<ActionResult<ResponseDto<PetDto>>> UpdatePet(int id, [FromBody] UpdatePetDto dto)
        {
            var pet = await _context.Pets.FindAsync(id);

            if (pet == null)
                return NotFound(ResponseDto<PetDto>.ErrorResponse("Thú cưng không tồn tại"));

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole == "customer")
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
                
                if (customer == null || pet.CustomerId != customer.CustomerId)
                    return Forbid();
            }

            if (!string.IsNullOrEmpty(dto.Name))
                pet.Name = dto.Name;

            if (!string.IsNullOrEmpty(dto.Type))
                pet.Type = dto.Type;

            if (dto.Breed != null)
                pet.Breed = dto.Breed;

            if (dto.Age.HasValue)
                pet.Age = dto.Age;

            if (dto.ImageUrl != null)
                pet.ImageUrl = dto.ImageUrl;

            await _context.SaveChangesAsync();

            var petDto = new PetDto
            {
                PetId = pet.PetId,
                CustomerId = pet.CustomerId,
                Name = pet.Name,
                Type = pet.Type,
                Breed = pet.Breed,
                Age = pet.Age,
                ImageUrl = pet.ImageUrl
            };

            return Ok(ResponseDto<PetDto>.SuccessResponse(petDto, "Cập nhật thú cưng thành công"));
        }

        // DELETE: api/pets/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<ResponseDto<string>>> DeletePet(int id)
        {
            var pet = await _context.Pets.FindAsync(id);

            if (pet == null)
                return NotFound(ResponseDto<string>.ErrorResponse("Thú cưng không tồn tại"));

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole == "customer")
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
                
                if (customer == null || pet.CustomerId != customer.CustomerId)
                    return Forbid();
            }

            _context.Pets.Remove(pet);
            await _context.SaveChangesAsync();

            return Ok(ResponseDto<string>.SuccessResponse("", "Xóa thú cưng thành công"));
        }
    }
}