
using AdvanceCRM.Web.Helpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace AdvanceCRM.Web.EF.Models
{
    public static class DbInitializer
    {
        public static void Initialize(AdvanceCRMContext context)
        {
            try
            {
                context.Database.EnsureCreated();
                SeedUsers(context);
                SeedLanguages(context);
            }catch(Exception ex)
            {
                ex.Log();
                throw ex;
            }
        }

        private static void SeedUsers(AdvanceCRMContext context)
        {
            if (!context.Users.Any())
            {

                var users = new User[]
                {
            new User
            {
                Username = "admin",
                DisplayName = "admin",
                Email = "admin@dummy.com",
                Source = "site",
                Company=1,
                PasswordHash = "rfqpSPYs0ekFlPyvIRTXsdhE/qrTHFF+kKsAUla7pFkXL4BgLGlTe89GDX5DBysenMDj8AqbIZPybqvusyCjwQ",
                PasswordSalt = "hJf_F",
                InsertDate = new DateTime(2014, 1, 1),
                InsertUserId = 1,
                IsActive = 1
            },
                    // Add other users here
                };

                context.Users.AddRange(users);
                context.SaveChanges();
            }
        }



        private static void SeedLanguages(AdvanceCRMContext context)
        {
            if (!context.Languages.Any())
            {
                var languages = new Language[]
                {
                    new Language {   LanguageId = "en", LanguageName = "English" },
                    new Language {  LanguageId = "ru", LanguageName = "Russian" },
                    new Language { LanguageId = "es", LanguageName = "Spanish" },
                    new Language {  LanguageId = "tr", LanguageName = "Turkish" },
                    new Language {   LanguageId = "de", LanguageName = "German" },
                    new Language {   LanguageId = "zh-CN", LanguageName = "Chinese (Simplified)" },
                    new Language {  LanguageId = "it", LanguageName = "Italian" },
                    new Language {  LanguageId = "pt", LanguageName = "Portuguese" },
                    new Language {   LanguageId = "pt-BR", LanguageName = "Portuguese (Brazil)" },
                    new Language {  LanguageId = "fa", LanguageName = "Farsi" },
                    new Language {  LanguageId = "vi-VN", LanguageName = "Vietnamese (Vietnam)" },
                    // Add other languages here
                };

                context.Languages.AddRange(languages);
                context.SaveChanges();
            }
        }
              
    }
}
