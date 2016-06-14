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
        public int tipoDeInstituicaoID { get; set; }
        public virtual TipoDeInstituicao tipoDeInstituicao { get; set; }
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
            if (this.IsDigitsOnly(PhoneNumber) == false)
                yield return new ValidationResult("Apenas números são aceitos", new[] { nameof(PhoneNumber) });

            if (telefone != null && this.IsDigitsOnly(telefone) == false)
                yield return new ValidationResult("Apenas números são aceitos", new[] { nameof(telefone) });

            if (this.IsDigitsOnly(cep) == false)
                yield return new ValidationResult("Apenas números são aceitos", new[] { nameof(cep) });

            if (role != "User")
            {
                if (cnpj == null)
                    yield return new ValidationResult("Este campo é obrigatório", new[] { nameof(cnpj) });

                if (tipoDeInstituicaoID == 0)
                    yield return new ValidationResult("Este campo é obrigatório", new[] { nameof(tipoDeInstituicao) });

                if (descricaoDaCausa == null)
                    yield return new ValidationResult("Este campo é obrigatório", new[] { nameof(descricaoDaCausa) });

                if (nomeDoResponsavel == null)
                    yield return new ValidationResult("Este campo é obrigatório", new[] { nameof(nomeDoResponsavel) });

                if (areaDeCobertura == null)
                    yield return new ValidationResult("Este campo é obrigatório", new[] { nameof(areaDeCobertura) });

                if (cnpj != null && this.IsDigitsOnly(cnpj) == false)
                    yield return new ValidationResult("Apenas números são aceitos", new[] { nameof(cnpj) });

                if (cnpj != null && this.ValidaCNPJ(cnpj) == false)
                    yield return new ValidationResult("CNPJ inválido", new[] { nameof(cnpj) });
            }
        }


        bool IsDigitsOnly(string str)
        {
            foreach (char c in str)
            {
                if (c < '0' || c > '9')
                    return false;
            }

            return true;
        }

        public bool ValidaCNPJ(string vrCNPJ)

        {

            string CNPJ = vrCNPJ.Replace(".", "");

            CNPJ = CNPJ.Replace("/", "");

            CNPJ = CNPJ.Replace("-", "");



            int[] digitos, soma, resultado;

            int nrDig;

            string ftmt;

            bool[] CNPJOk;



            ftmt = "6543298765432";

            digitos = new int[14];

            soma = new int[2];

            soma[0] = 0;

            soma[1] = 0;

            resultado = new int[2];

            resultado[0] = 0;

            resultado[1] = 0;

            CNPJOk = new bool[2];

            CNPJOk[0] = false;

            CNPJOk[1] = false;



            try

            {

                for (nrDig = 0; nrDig < 14; nrDig++)

                {

                    digitos[nrDig] = int.Parse(

                        CNPJ.Substring(nrDig, 1));

                    if (nrDig <= 11)

                        soma[0] += (digitos[nrDig] *

                          int.Parse(ftmt.Substring(

                          nrDig + 1, 1)));

                    if (nrDig <= 12)

                        soma[1] += (digitos[nrDig] *

                          int.Parse(ftmt.Substring(

                          nrDig, 1)));

                }



                for (nrDig = 0; nrDig < 2; nrDig++)

                {

                    resultado[nrDig] = (soma[nrDig] % 11);

                    if ((resultado[nrDig] == 0) || (

                         resultado[nrDig] == 1))

                        CNPJOk[nrDig] = (

                        digitos[12 + nrDig] == 0);

                    else

                        CNPJOk[nrDig] = (

                        digitos[12 + nrDig] == (

                        11 - resultado[nrDig]));

                }

                return (CNPJOk[0] && CNPJOk[1]);

            }

            catch

            {

                return false;

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