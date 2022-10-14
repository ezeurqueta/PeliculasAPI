using Microsoft.AspNetCore.Identity;
using PeliculasAPI;
using PeliculasAPI.Entidades;
using System.Security.Claims;

namespace PeliculasApi
{
    public static class ContextSeeding
    {
        public static async Task SeedTestData(this ApplicationDbContext context)
        {

            var usuarioAdminId = "5673b8cf-12de-44f6-92ad-fae4a77932ad";
            var usuarioUserId = "8e19fb2b-c13a-4549-a4fa-9325bd387815";

            var passwordHasher = new PasswordHasher<IdentityUser>();

            var username = "eze@gmail.com";
            var usernameUser = "user@gmail.com";

            var usuarioAdmin = new IdentityUser()
            {
                Id = usuarioAdminId,
                UserName = username,
                NormalizedUserName = username,
                Email = username,
                NormalizedEmail = username,
                PasswordHash = passwordHasher.HashPassword(null, "Aa5075!")
            };
            var actor = new Actor()
            {
                Id = 500,
                Nombre = "Ezequiel",
                
            };
            var genero = new Genero()
            {
                Id = 100,
                Nombre = "Comedia"
            };
            var pelicula = new Pelicula()
            {
                Titulo = "HarryPotter",
                Id = 300
            };
            var review = new Review()
            {
                Id = 400,
                Comentario = "Excelente"
            };
            var salaDeCine = new SalaDeCine()
            {
                Nombre = "Hoytz",
                Id = 200
            };


            var usuarioUser = new IdentityUser()
            {
                Id = usuarioUserId,
                UserName = usernameUser,
                NormalizedUserName = username,
                Email = username,
                NormalizedEmail = username,
                PasswordHash = passwordHasher.HashPassword(null, "Aa5075!")
            };



            var adminClaim = new IdentityUserClaim<string>()
            {
                Id = 10,
                ClaimType = ClaimTypes.Role,
                UserId = usuarioAdminId,
                ClaimValue = "Admin"
            };
            var userClaim = new IdentityUserClaim<string>()
            {
                Id = 20,
                ClaimType = ClaimTypes.Role,
                UserId = usuarioUserId,
                ClaimValue = "User"
            };

            await context.UserClaims.AddRangeAsync(new IdentityUserClaim<string>[] { userClaim, adminClaim });
            await context.Users.AddRangeAsync(new IdentityUser[] { usuarioAdmin, usuarioUser });

            await context.Actores.AddAsync(actor);
            await context.Generos.AddAsync(genero);
            await context.Peliculas.AddAsync(pelicula);
            await context.Reviews.AddAsync(review);
            await context.SalasDeCine.AddAsync(salaDeCine);

            await context.SaveChangesAsync();

        }
    }
}