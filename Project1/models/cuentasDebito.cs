namespace Project1.models
{
    public class CuentasDebito
    {
        public int id { get; set; }
        public decimal monto { get; set; }
        public int idPrestamo { get; set; }
        public DateTime fecha { get; set; }
        public decimal deuda { get; set; }
        public string estado { get; set; }
    }
}
