using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DevIO.Api.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        // especificar qual context em casos de mais de um por aplicacao
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }
    }
}
