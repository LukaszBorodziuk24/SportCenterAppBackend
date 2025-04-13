using SportCenterApi.Entities;
using SportCenterApi.Models.Enums;
using SportCenterApi;
using System.Net;
using Faker;
using Microsoft.AspNetCore.Identity;

public static class SeedAppUser
{
    private static readonly string DefaultPassword = "P@ssw0rd123";
    public static async Task SeedAsync(UserManager<AppUser> userManager, int recordsCount)
    {
        if (!userManager.Users.Any())
        {
            var users = new List<AppUser>();

            for (int i = 0; i < recordsCount; i++)
            {
                var email = Internet.Email();
                var user = new AppUser
                {
                    UserName = email,
                    Email = email,
                    Name = Name.First(),
                    LastName = Name.Last(),
                    City = Address.City(),
                    Country = Address.Country(),
                    Rating = RandomNumber.Next(1, 6),   
                };

                var result = await userManager.CreateAsync(user, DefaultPassword);

                if (!result.Succeeded)
                {
                    
                    Console.WriteLine($"Błąd dodawania użytkownika {email}: {string.Join(", ", result.Errors.Select(e => e.Description))}");
                }

            }
        }
    }
}