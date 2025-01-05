using SMK_ServicePoint.Interface;
using SMK_ServicePoint.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Services 
{
    public class PanCardService : IPancard
    {
        private readonly HttpClient _httpClient;

        public PanCardService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }

        public async Task<PanCard> AddAsync(PanCard mod)
        {
            var data = await _httpClient.PostAsJsonAsync("api/PanCards/Add-Pancard", mod);
            var response = await data.Content.ReadFromJsonAsync<PanCard>();
            return response;
        }

        public async Task<PanCard> DeleteAsync(int id)
        {
            var data = await _httpClient.DeleteAsync($"api/PanCards/Delete-Pancard/{id}");
            var response = await data.Content.ReadFromJsonAsync<PanCard>();
            return response;
        }

        public async Task<List<PanCard>> GetAllAsync()
        {
            var allpancard = await _httpClient.GetAsync("api/PanCards/All-Pancard");
            var response = await allpancard.Content.ReadFromJsonAsync<List<PanCard>>();
            return response;
        }

        public async Task<PanCard> GetByIdAsync(int id)
        {
            var singlepancard = await _httpClient.GetAsync($"api/PanCards/Single-Pancard/{id}");
            var response = await singlepancard.Content.ReadFromJsonAsync<PanCard>();
            return response;
        }
            
        public async Task<PanCard> UpdateAsync(PanCard mod)
        {
            var newpancard = await _httpClient.PostAsJsonAsync("api/PanCards/Update-Pancard", mod);
            var response = await newpancard.Content.ReadFromJsonAsync<PanCard>();
            return response;
        }
    }
}
