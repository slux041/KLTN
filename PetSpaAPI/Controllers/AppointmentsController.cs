using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using PetSpaAPI.Data;
using PetSpaAPI.Models;
using PetSpaAPI.DTOs.Appointment;
using PetSpaAPI.DTOs.Common;

namespace PetSpaAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AppointmentsController : ControllerBase
    {
        private readonly PetSpaDbContext _context;

        public AppointmentsController(PetSpaDbContext context)
        {
            _context = context;
        }

        // GET: api/appointments
        [HttpGet]
        [Authorize]
        public async Task<ActionResult<ResponseDto<List<AppointmentDto>>>> GetAppointments(
            [FromQuery] string? status = null,
            [FromQuery] DateTime? date = null)
        {
            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            var userIdStr = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            IQueryable<Appointment> query = _context.Appointments
                .Include(a => a.Customer)
                .ThenInclude(c => c!.User)
                .Include(a => a.Service);

            if (userRole == "customer" && !string.IsNullOrEmpty(userIdStr))
            {
                var userId = int.Parse(userIdStr);
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
                if (customer == null)
                    return NotFound(ResponseDto<List<AppointmentDto>>.ErrorResponse("Thông tin khách hàng không tồn tại"));

                query = query.Where(a => a.CustomerId == customer.CustomerId);
            }

            if (!string.IsNullOrEmpty(status))
                query = query.Where(a => a.Status == status);

            if (date.HasValue)
                query = query.Where(a => a.AppointmentDate.Date == date.Value.Date);

            var appointments = await query
                .OrderByDescending(a => a.AppointmentDate)
                .Select(a => new AppointmentDto
                {
                    AppointmentId = a.AppointmentId,
                    CustomerId = a.CustomerId,
                    CustomerName = a.CustomerId.HasValue ? a.Customer!.User!.FullName : a.GuestName,
                    CustomerPhone = a.CustomerId.HasValue ? (a.Customer!.User!.Phone ?? "") : a.GuestPhone,
                    ServiceId = a.ServiceId,
                    ServiceName = a.Service!.Name,
                    ServicePrice = a.Service.Price,
                    PetInfo = a.PetInfo,
                    PetType = a.PetType,
                    PetBreed = a.PetBreed,
                    AppointmentDate = a.AppointmentDate,
                    TimeSlot = a.TimeSlot,
                    Status = a.Status,
                    Source = a.Source
                })
                .ToListAsync();

            return Ok(ResponseDto<List<AppointmentDto>>.SuccessResponse(appointments));
        }

        // GET: api/appointments/5
        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<AppointmentDto>>> GetAppointment(int id)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Customer)
                .ThenInclude(c => c!.User)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
                return NotFound(ResponseDto<AppointmentDto>.ErrorResponse("Lịch hẹn không tồn tại"));

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole == "customer")
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
                
                if (customer == null || appointment.CustomerId != customer.CustomerId)
                    return Forbid();
            }

            var appointmentDto = new AppointmentDto
            {
                AppointmentId = appointment.AppointmentId,
                CustomerId = appointment.CustomerId,
                CustomerName = appointment.CustomerId.HasValue ? appointment.Customer!.User!.FullName : appointment.GuestName,
                CustomerPhone = appointment.CustomerId.HasValue ? (appointment.Customer!.User!.Phone ?? "") : appointment.GuestPhone,
                ServiceId = appointment.ServiceId,
                ServiceName = appointment.Service!.Name,
                ServicePrice = appointment.Service.Price,
                PetInfo = appointment.PetInfo,
                PetType = appointment.PetType,
                PetBreed = appointment.PetBreed,
                AppointmentDate = appointment.AppointmentDate,
                TimeSlot = appointment.TimeSlot,
                Status = appointment.Status,
                Source = appointment.Source
            };

            return Ok(ResponseDto<AppointmentDto>.SuccessResponse(appointmentDto));
        }

        // GET: api/appointments/available-slots
        [HttpGet("available-slots")]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<List<TimeSlotDto>>>> GetAvailableTimeSlots([FromQuery] DateTime date)
        {
            var dateOnly = date.Date;

            var timeSlotConfigs = await _context.TimeSlotConfigs
                .Where(t => t.IsActive)
                .OrderBy(t => t.TimeSlot)
                .ToListAsync();

            var availableSlots = new List<TimeSlotDto>();

            foreach (var config in timeSlotConfigs)
            {
                var currentBookings = await _context.Appointments
                    .CountAsync(a => a.AppointmentDate.Date == dateOnly && 
                               a.TimeSlot == config.TimeSlot && 
                               a.Status != "canceled");

                availableSlots.Add(new TimeSlotDto
                {
                    TimeSlot = config.TimeSlot,
                    MaxBookings = config.MaxBookings,
                    CurrentBookings = currentBookings,
                    IsActive = config.IsActive
                });
            }

            return Ok(ResponseDto<List<TimeSlotDto>>.SuccessResponse(availableSlots));
        }

        // POST: api/appointments
        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<ResponseDto<AppointmentDto>>> CreateAppointment([FromBody] CreateAppointmentDto dto)
        {
            var service = await _context.Services.FindAsync(dto.ServiceId);
            if (service == null || !service.IsActive)
                return BadRequest(ResponseDto<AppointmentDto>.ErrorResponse("Dịch vụ không tồn tại hoặc không khả dụng"));

            var appointment = new Appointment
            {
                ServiceId = dto.ServiceId,
                PetInfo = dto.PetInfo,
                AppointmentDate = dto.AppointmentDate,
                TimeSlot = dto.TimeSlot,
                Status = "pending",
                PetType = dto.PetType,
                PetBreed = dto.PetBreed,
                Source = dto.Source
            };

            if (dto.CustomerId.HasValue && dto.CustomerId > 0)
            {
                var customerExists = await _context.Customers.AnyAsync(c => c.CustomerId == dto.CustomerId.Value);
                if (!customerExists)
                    return BadRequest(ResponseDto<AppointmentDto>.ErrorResponse("Mã khách hàng không hợp lệ"));
                
                appointment.CustomerId = dto.CustomerId.Value;
                appointment.GuestName = dto.CustomerName; 
                appointment.GuestPhone = dto.CustomerPhone;
            }
            else if (User.Identity != null && User.Identity.IsAuthenticated)
            {
                var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                if (!string.IsNullOrEmpty(userIdClaim))
                {
                    var userId = int.Parse(userIdClaim);
                    var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
                    if (customer != null)
                    {
                        appointment.CustomerId = customer.CustomerId;
                    }
                }
            }
            
            if (!appointment.CustomerId.HasValue)
            {
                if (string.IsNullOrEmpty(dto.CustomerName) || string.IsNullOrEmpty(dto.CustomerPhone))
                    return BadRequest(ResponseDto<AppointmentDto>.ErrorResponse("Vui lòng cung cấp tên và số điện thoại"));

                appointment.CustomerId = null;
                appointment.GuestName = dto.CustomerName;
                appointment.GuestPhone = dto.CustomerPhone;
                appointment.GuestAddress = dto.CustomerAddress;
            }

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            var createdApt = await _context.Appointments
                .Include(a => a.Customer).ThenInclude(c => c!.User)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.AppointmentId == appointment.AppointmentId);

            var appointmentDto = new AppointmentDto
            {
                AppointmentId = createdApt!.AppointmentId,
                CustomerId = createdApt.CustomerId,
                CustomerName = createdApt.CustomerId.HasValue ? createdApt.Customer!.User!.FullName : createdApt.GuestName,
                CustomerPhone = createdApt.CustomerId.HasValue ? (createdApt.Customer!.User!.Phone ?? "") : createdApt.GuestPhone,
                ServiceId = createdApt.ServiceId,
                ServiceName = createdApt.Service!.Name,
                ServicePrice = createdApt.Service.Price,
                PetInfo = createdApt.PetInfo,
                PetType = createdApt.PetType,
                PetBreed = createdApt.PetBreed,
                AppointmentDate = createdApt.AppointmentDate,
                TimeSlot = createdApt.TimeSlot,
                Status = createdApt.Status,
                Source = createdApt.Source
            };

            return CreatedAtAction(nameof(GetAppointment), new { id = appointment.AppointmentId },
                ResponseDto<AppointmentDto>.SuccessResponse(appointmentDto, "Đặt lịch thành công"));
        }

        // POST: api/appointments/5/status
        [HttpPost("{id}/status")]
        [Authorize]
        public async Task<ActionResult<ResponseDto<AppointmentDto>>> UpdateAppointmentStatus(
            int id, [FromBody] UpdateAppointmentStatusDto dto)
        {
            var appointment = await _context.Appointments
                .Include(a => a.Customer)
                .ThenInclude(c => c!.User)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.AppointmentId == id);

            if (appointment == null)
                return NotFound(ResponseDto<AppointmentDto>.ErrorResponse("Lịch hẹn không tồn tại"));

            var userRole = User.FindFirst(ClaimTypes.Role)?.Value;
            if (userRole == "customer")
            {
                var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
                var customer = await _context.Customers.FirstOrDefaultAsync(c => c.UserId == userId);
                
                if (customer == null || appointment.CustomerId != customer.CustomerId)
                    return Forbid();

                if (dto.Status != "canceled")
                    return BadRequest(ResponseDto<AppointmentDto>.ErrorResponse("Khách hàng chỉ có thể hủy lịch hẹn"));
            }

            appointment.Status = dto.Status;
            await _context.SaveChangesAsync();

            var appointmentDto = new AppointmentDto
            {
                AppointmentId = appointment.AppointmentId,
                CustomerId = appointment.CustomerId,
                CustomerName = appointment.CustomerId.HasValue ? appointment.Customer!.User!.FullName : appointment.GuestName,
                CustomerPhone = appointment.CustomerId.HasValue ? (appointment.Customer!.User!.Phone ?? "") : appointment.GuestPhone,
                ServiceId = appointment.ServiceId,
                ServiceName = appointment.Service!.Name,
                ServicePrice = appointment.Service.Price,
                PetInfo = appointment.PetInfo,
                PetType = appointment.PetType,
                PetBreed = appointment.PetBreed,
                AppointmentDate = appointment.AppointmentDate,
                TimeSlot = appointment.TimeSlot,
                Status = appointment.Status,
                Source = appointment.Source
            };

            return Ok(ResponseDto<AppointmentDto>.SuccessResponse(appointmentDto, "Cập nhật trạng thái thành công"));
        }

        // DELETE: api/appointments/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public async Task<ActionResult<ResponseDto<string>>> DeleteAppointment(int id)
        {
            var appointment = await _context.Appointments.FindAsync(id);

            if (appointment == null)
                return NotFound(ResponseDto<string>.ErrorResponse("Lịch hẹn không tồn tại"));

            _context.Appointments.Remove(appointment);
            await _context.SaveChangesAsync();

            return Ok(ResponseDto<string>.SuccessResponse("", "Xóa lịch hẹn thành công"));
        }
    }
}