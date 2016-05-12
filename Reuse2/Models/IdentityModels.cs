using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace Reuse2.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser, IValidatableObject
    {
        [DisplayName("Endereço")]
        [Required(ErrorMessage = "Este campo é obrigatório")]
        public String endereco { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DisplayName("CEP")]
        public String cep { get; set; }
        [DisplayName("Bairro")]
        public String bairro { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DisplayName("Cidade")]
        public String cidade { get; set; }
        [Required(ErrorMessage = "Este campo é obrigatório")]
        [DisplayName("Estado")]
        public String estado { get; set; }
        [DisplayName("Telefone")]
        public String telefone { get; set; }
        [DisplayName("Itens Doados")]
        public int itensDoados { get; set; }
        [DisplayName("Itens Pedidos")]
        public int itensPedidos { get; set; }
        [DisplayName("Avatar")]
        public string avatar { get; set; }

        public string role { get; set; }
        [DisplayName("CNPJ")]
        public string cnpj { get; set; }
        [DisplayName("Nome do Responsável")]
        public string nomeDoResponsavel { get; set; }
        [DisplayName("Tipo da instituição")]
        public int tipoID { get; set; }
        public virtual TipoDeInstituicao tipo { get; set; }
        [DisplayName("Descrição da causa")]
        public string descricaoDaCausa { get; set; }
        [DisplayName("Itens necessitados")]
        public string itensNecessitados { get; set; }
        [DisplayName("Método de coleta de itens")]
        public string metodoDeColeta { get; set; }
        [DisplayName("Área de cobertura")]
        public string areaDeCobertura { get; set; }
        [DisplayName("Restrições para coleta de itens")]
        public string restricoesDeColeta { get; set; }

        public virtual ICollection<Interesse> interesses { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (role != "User")
            {
                if (cnpj == null)
                    yield return new ValidationResult("Este campo é obrigatório", new[] { nameof(cnpj) });

                if (tipoID == 0)
                    yield return new ValidationResult("Este campo é obrigatório", new[] { nameof(tipo) });

                if (descricaoDaCausa == null)
                    yield return new ValidationResult("Este campo é obrigatório", new[] { nameof(descricaoDaCausa) });
            }
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

        public System.Data.Entity.DbSet<Reuse2.Models.Imagem> Imagens { get; set; }

        public System.Data.Entity.DbSet<Reuse2.Models.TipoDeInstituicao> Tipos { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().Property(x => x.cep).IsRequired();
            modelBuilder.Entity<ApplicationUser>().Property(x => x.cidade).IsRequired();
            modelBuilder.Entity<ApplicationUser>().Property(x => x.estado).IsRequired();
            modelBuilder.Entity<ApplicationUser>().Property(x => x.endereco).IsRequired();
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