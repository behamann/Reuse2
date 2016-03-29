using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Reuse2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        /*[Required]*/
        [DisplayName("Endereço")]
        public String endereco { get; set; }
        /*[Required]*/
        [DisplayName("CEP")]
        public String cep { get; set; }
        [DisplayName("Bairro")]
        public String bairro { get; set; }
        /*[Required]*/
        [DisplayName("Cidade")]
        public String cidade { get; set; }
        /*[Required]*/
        [DisplayName("Estado")]
        public String estado { get; set; }
        [DisplayName("Telefone")]
        public String telefone { get; set; }
        [DisplayName("Itens Doados")]
        public int itensDoados { get; set; }
        [DisplayName("Itens Pedidos")]
        public int itensPedidos { get; set; }

        public virtual ICollection<Interesse> interesses { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Reuse2.Models.Anuncio> Anuncios { get; set; }

        public System.Data.Entity.DbSet<Reuse2.Models.Categoria> Categorias { get; set; }

        public System.Data.Entity.DbSet<Reuse2.Models.Subcategoria> Subcategorias { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Interesse>()
                .HasKey(c => new { c.anuncioID, c.userID });

            modelBuilder.Entity<Anuncio>()
                .HasMany(c => c.interessados)
                .WithRequired()
                .HasForeignKey(c => c.anuncioID);

            modelBuilder.Entity<ApplicationUser>()
                .HasMany(c => c.interesses)
                .WithRequired()
                .HasForeignKey(c => c.userID);
        }

        public System.Data.Entity.DbSet<Reuse2.Models.Interesse> Interesses { get; set; }
    }
}