using System.Data.SqlClient;

namespace Project1.models
{
    public class PagarPrestamos
    {
        public DateTime fechaActual { get; set; }
        public double tazaInteres { get; set; }
        public double tazaIva { get; }
        public int diasAnoComercial { get; set; }

        public PagarPrestamos(DateTime fecha, double iva, double interes, int dias)
        {
            fechaActual = fecha;
            tazaIva = iva;
            tazaInteres = interes;
            diasAnoComercial = dias;
        }

        public double MontoPago(CuentasDebito cuenta)
        {
            TimeSpan plazo = fechaActual - cuenta.fecha;
            double interes = Convert.ToDouble(cuenta.deuda) * plazo.Days * tazaInteres / diasAnoComercial;
            double iva = Math.Round(interes, 2) * tazaIva;
            double pago = Convert.ToDouble(cuenta.deuda) + Math.Round(interes, 2) + Math.Round(iva, 2);

            return pago;
        }

        public void CobrarCuenta(List<CuentasDebito> cuentas)
        {
            string connectionString = "Server=DESKTOP-PDGLL5M\\SQLEXPRESS; Database=EvaluacionPractica; Trusted_Connection=True;";
            SqlConnection connection = new(connectionString);
            connection.Open();
            var montoInicial = cuentas[0].monto;

            for (var i = 0; i < cuentas.Count; i++)
            {
                decimal montoTotal = Convert.ToDecimal(MontoPago(cuentas[i]));
                if (montoInicial > montoTotal)
                {
                    var updatePrestamo = $"Update prestamos set estado = 'Pagado' where id_prestamo = {cuentas[i].idPrestamo}";
                    SqlCommand command = new(updatePrestamo, connection);
                    command.ExecuteNonQuery();

                    montoInicial = montoInicial - montoTotal;
                    var updateMonto = $"Update cuentasDebito set monto = {montoInicial} where id = {cuentas[i].id}";

                    SqlCommand command2 = new(updateMonto, connection);
                    command2.ExecuteNonQuery();

                    Console.WriteLine($"Cliente: {cuentas[i].id}, Monto: {montoInicial}");
                }
            }
        }
    }
}
