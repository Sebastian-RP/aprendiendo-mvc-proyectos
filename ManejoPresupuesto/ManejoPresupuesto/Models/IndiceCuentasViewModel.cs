namespace ManejoPresupuesto.Models
{
    public class IndiceCuentasViewModel
    {
        public string Tipocuenta { get; set; }
        public IEnumerable<Cuenta> Cuentas { get; set; }
        public decimal Balance => Cuentas.Sum(x => x.Balance);
    }
}
