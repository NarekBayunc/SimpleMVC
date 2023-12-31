﻿using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using SimpleMVC.Data.Base;
using SimpleMVC.Models;
using System.Security.Claims;
using System.Security.Cryptography;
using Image = SixLabors.ImageSharp.Image;
using Microsoft.Extensions.Caching.Memory;
using SimpleMVC.Models.ViewModels;
using SimpleMVC.Data.Services;

namespace SimpleMVC.Data
{
    internal static class Helper
    {
        public static string HashPassword(string password)
        {
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return $"{hashed}:{Convert.ToBase64String(salt)}";
        }
        public static bool VerifyPassword(string hashedPassword, string userInput)
        {
            string[] parts = hashedPassword.Split(':');
            if (parts.Length != 2)
            {
                return false;
            }
            byte[] salt = Convert.FromBase64String(parts[1]);
            string hashedInput = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: userInput,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 10000,
                numBytesRequested: 256 / 8));

            return parts[0] == hashedInput;
        }
        public static byte[] FromImgToBytes(IFormFile file)
        {
            byte[] imageData = null!;
            try
            {
                using (BinaryReader? binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)file.Length);
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Image Data is null");
            }
            return imageData;

        }
        public static byte[]? FromImgToBytes(IFormFile? file, string imageDataString)
        {
            byte[]? imageData = null!;
            if (file != null)
            {
                using (BinaryReader? binaryReader = new BinaryReader(file.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)file.Length);
                }
                return imageData;
            }
            else if (!string.IsNullOrEmpty(imageDataString))
            {
                imageData = Convert.FromBase64String(imageDataString);
                return imageData;
            }
            return imageData;

        }
        public static byte[]? FromImgToBytes(string imageDataString)
        {
            byte[]? imageData = null!;

            if (!string.IsNullOrEmpty(imageDataString))
            {
                using (FileStream fs = new FileStream(imageDataString, FileMode.Open))
                {
                    using (Image<Rgba32> image = Image.Load<Rgba32>(fs))
                    {
                        using (MemoryStream ms = new MemoryStream())
                        {
                            image.SaveAsJpeg(ms);
                            return ms.ToArray();
                        }
                    }
                }
            }
            return imageData;
        }
        public static byte[] DefaultImage()
        {
            string imagePath = "wwwroot/img/not-found.jpg";
            byte[] imageData;
            using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader binaryReader = new BinaryReader(fs))
                {
                    imageData = binaryReader.ReadBytes((int)fs.Length);
                }
            }
            return imageData;
        }
        public static void UserCookieConfig(User? user, out string authScheme,
                                                out ClaimsIdentity? identity,
                                                out AuthenticationProperties authProp) 
        {
            authScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            List<Claim>? claims = new List<Claim>()
                            { new Claim(ClaimTypes.Name, user?.Login!),
                              new Claim(ClaimTypes.Email, user?.Email!),
                              new Claim(ClaimsIdentity.DefaultRoleClaimType, user?.Role.ToString()!)
                            };
            identity = new ClaimsIdentity(claims, authScheme);
            authProp = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = true
            };
        }

        public static async Task<IndexViewModel<T>> GetPaginatedViewModel<T>(int page,
                                                            IEntityControllerService<T> service)
        {
            if (page <= 0) page = 1;
            int pageSize = 3;
            IEnumerable<T> allEntities = await service.GetAllAsync();
            int totalPages = (int)Math.Ceiling((decimal)allEntities.Count() / pageSize);
            if (page < 1 || page > totalPages)
            {
                return new IndexViewModel<T>
                {
                    PageInfo = new PageInfo { PageNumber = 1, PageSize = pageSize, TotalItems = allEntities.Count() },
                    Entities = service.GetForXPagesAsync(page, pageSize)
                };
            }
            IEnumerable<T> entitiesPerPage = service.GetForXPagesAsync(page, pageSize);
            PageInfo pageInfo = new PageInfo
            {
                PageNumber = page,
                PageSize = pageSize,
                TotalItems = allEntities.Count()
            };
            return new IndexViewModel<T> { PageInfo = pageInfo, Entities = entitiesPerPage };
        }
    }
}

