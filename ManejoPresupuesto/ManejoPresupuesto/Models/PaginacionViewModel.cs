namespace ManejoPresupuesto.Models
{
    public class PaginacionViewModel
    {
        public int Pagina { get; set; } = 1;
        public int recordsPorPagina { get; set; } = 10;

        public readonly int cantidadMaximaRecordsPorPagina = 50;

        public int RecordsPorPagina
        {
            get
            {
                return recordsPorPagina;
            }
            set
            {
                recordsPorPagina = (value > cantidadMaximaRecordsPorPagina) ?
                    cantidadMaximaRecordsPorPagina : value;
            }
        }

        public int RecordsAsaltar => recordsPorPagina * (Pagina - 1);
    }
}
