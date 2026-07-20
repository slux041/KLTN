using GD.DTOs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace GD.Services
{
    public class AppointmentService
    {
        public async Task<List<AppointmentDto>> GetAppointments(string status = null, DateTime? date = null)
        {
            string url = "/api/Appointments";
            var queryParams = new List<string>();

            if (!string.IsNullOrEmpty(status))
                queryParams.Add($"status={status}");

            if (date.HasValue)
                queryParams.Add($"date={date.Value:yyyy-MM-ddTHH:mm:ss}");

            if (queryParams.Count > 0)
                url += "?" + string.Join("&", queryParams);

            var response = await ApiClient.Instance.GetAsync<ApiResponse<List<AppointmentDto>>>(url);
            return response.Data ?? new List<AppointmentDto>();
        }

        public async Task<AppointmentDto> GetAppointment(int id)
        {
            var response = await ApiClient.Instance.GetAsync<ApiResponse<AppointmentDto>>($"/api/Appointments/{id}");
            return response.Data;
        }

        public async Task<List<TimeSlotDto>> GetAvailableSlots(DateTime date)
        {
            string url = $"/api/Appointments/available-slots?date={date:yyyy-MM-ddTHH:mm:ss}";
            var response = await ApiClient.Instance.GetAsync<ApiResponse<List<TimeSlotDto>>>(url);
            return response.Data ?? new List<TimeSlotDto>();
        }

        public async Task CreateAppointment(CreateAppointmentDto appointment)
        {
            if (appointment.Source == "store")
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(ApiClient.Instance.BaseUrl);
                    var json = JsonConvert.SerializeObject(appointment);
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    var response = await client.PostAsync("/api/Appointments", content);

                    if (!response.IsSuccessStatusCode)
                    {
                        var error = await response.Content.ReadAsStringAsync();
                        throw new Exception("Lỗi tạo lịch: " + error);
                    }
                }
            }
            else
            {
                await ApiClient.Instance.PostAsync<ApiResponse<AppointmentDto>>("/api/Appointments", appointment);
            }
        }

        public async Task DeleteAppointment(int id)
        {
            await ApiClient.Instance.DeleteAsync<ApiResponse<string>>($"/api/Appointments/{id}");
        }

        public async Task UpdateStatus(int id, string status)
        {
            var dto = new UpdateAppointmentStatusDto { Status = status };
            await ApiClient.Instance.PostAsync<ApiResponse<AppointmentDto>>($"/api/Appointments/{id}/status", dto);
        }
        public async Task UpdateAppointment(int id, CreateAppointmentDto appointment)
        {
            await ApiClient.Instance.PutAsync<ApiResponse<AppointmentDto>>($"/api/Appointments/{id}", appointment);
        }
    }
}