using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MoviePro.Services.Interfaces;

namespace MoviePro.Services
{
    /// <summary>
    /// Implement async methods to help image storage in database
    /// Implement method to prepare image for display
    /// </summary>
    public class BasicImageService : IImageService
    {
        private readonly IHttpClientFactory _httpClient;

        public BasicImageService(IHttpClientFactory httpClient)
        {
            _httpClient = httpClient;
        }

        /// <summary>
        /// decode image from database
        /// </summary>
        /// <param name="poster"></param>
        /// <param name="contentType"></param>
        /// <returns></returns>
        public string DecodeImage(byte[] poster, string contentType)
        {
            if (poster == null) return null;
            var posterImage = Convert.ToBase64String(poster);
            return $"data:{contentType};base64,{posterImage}";
        }

        /// <summary>
        /// convert IFormFile to byte array
        /// used when creating or editing image or an image that has been imported to database
        /// </summary>
        /// <param name="poster"></param>
        /// <returns></returns>
        public async Task<byte[]> EncodeImageAsync(IFormFile poster)
        {
            if (poster == null) return null;

            using var ms = new MemoryStream();
            await poster.CopyToAsync(ms);
            return ms.ToArray();
        }

        /// <summary>
        /// convert url to byte array
        /// </summary>
        /// <param name="imageURL"></param>
        /// <returns></returns>
        public async Task<byte[]> EncodeImageURLAsync(string imageURL)
        {
            var client = _httpClient.CreateClient();
            var response = await client.GetAsync(imageURL);
            using Stream stream = await response.Content.ReadAsStreamAsync();

            var ms = new MemoryStream();
            await stream.CopyToAsync(ms);
            return ms.ToArray();
        }
    }
}
