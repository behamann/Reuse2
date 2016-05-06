using Reuse2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Reuse2.Validators
{
    [AttributeUsageAttribute(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class InstituicaoAttribute : ValidationAttribute
    {
        public InstituicaoAttribute() : base("Campo obrigatório") { }

        public override bool IsValid(object value)
        {
            ApplicationUser user = (ApplicationUser)value;
            if (user.role == "Instituicao")
            {
                if(user.cnpj == 0 || user.nomeDoResponsavel == null || user.descricaoDaCausa == null)
                    return false;
            }
            return true;
        }
    }
}