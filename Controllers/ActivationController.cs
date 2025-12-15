using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;

namespace AdvanceCRM.Controllers
{
    [Route("Activation/[action]")]
    public class ActivationController : Controller
    {
        private readonly IWebHostEnvironment _env;

        public ActivationController(IWebHostEnvironment env)
        {
            _env = env;
        }

        [HttpPost]
        public IActionResult GenerateCrypto(string requestKey)
        {
            var hash = ComputeSha512(requestKey + "Developer@2020#$");
            hash = InsertDashes(hash, 58);
            return Json(hash);
        }

        private static string ComputeSha512(string input)
        {
            using var sha = SHA512.Create();
            var bytes = sha.ComputeHash(Encoding.UTF8.GetBytes(input));
            var hex = BitConverter.ToString(bytes).Replace("-", "");
            return Regex.Replace(hex, @"[^0-9A-Za-z]+", "").ToUpper();
        }

        private static string InsertDashes(string hash, int startRemove)
        {
            hash = hash.Remove(0, startRemove);
            for (int i = 4; i <= hash.Length; i += 6)
            {
                hash = hash.Insert(i, "-");
            }
            return hash;
        }
    }
}
