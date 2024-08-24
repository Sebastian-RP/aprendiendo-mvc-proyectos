using ManejoPresupuesto.Validaciones;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace ManejoPresupuesto.Models
{
    public class TipoCuenta //: IValidatableObject
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [PrimeraLetraMayuscula]
        [Remote(action: "VericarExisteTipoCuenta", controller: "TiposCuentas",
            AdditionalFields = nameof(Id))]
        public string Nombre { get; set; }
        public int UsuarioId { get; set; }
        public int Orden { get; set; }

        //Validacion a nivel del modelo
        //public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        //{
        //    if (Nombre != null && Nombre.Length > 0) 
        //    {
        //        var primaraLetra = Nombre[0].ToString();

        //        if (primaraLetra != primaraLetra.ToUpper())
        //        {
        //            yield return new ValidationResult("La primera letra debe ser mayuscula",
        //                new[] { nameof(Nombre) });
        //        }
        //    }
        //}
    }
}
