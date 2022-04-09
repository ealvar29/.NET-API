﻿using Newtonsoft.Json;
using ParkyWeb.Repository.IRepository;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ParkyWeb.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IHttpClientFactory _clientFactory;

        public Repository(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }
        public async Task<bool> CreateAsync(string url, T objToCreate)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            if (objToCreate != null)
            {
                request.Content = new StringContent(
                    JsonConvert.SerializeObject(objToCreate), Encoding.UTF8, "application/json");
            } else
            {
                return false;
            }
            var client = _clientFactory.CreateClient();
            HttpResponseMessage response = await client.SendAsync(request);
            if(response.StatusCode == System.Net.HttpStatusCode.Created)
            {
                return true;
            } else
            {
                return false;
            }
        }

        public Task<bool> DeleteAsync(string url, int Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<System.Collections.Generic.IEnumerable<T>> GetAllAsync(string url)
        {
            throw new System.NotImplementedException();
        }

        public Task<T> GetAsync(string url, int Id)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool> UpdateAsync(string url, T objToUpdate)
        {
            throw new System.NotImplementedException();
        }
    }
}
