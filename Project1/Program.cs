using Project1.models;
using System.Data.SqlClient;

namespace prueba
{
    class Program
    {
        static void Main(string[] args)
        {
            string connectionString = "Server=DESKTOP-PDGLL5M\\SQLEXPRESS; Database=EvaluacionPractica; Trusted_Connection=True;";
            SqlConnection connection = new(connectionString);
            connection.Open();

            var consult = "SELECT cd.id, cd.monto, p.id_prestamo, p.fecha, p.monto as deuda, p.estado from cuentasDebito cd join prestamos p on cd.id = p.cliente where cd.estado = 'Activa' and p.estado = 'Pendiente' order by p.fecha desc";
            SqlCommand command = new(consult, connection);

            SqlDataReader reader = command.ExecuteReader();
            List<CuentasDebito> listaCuentasDebito = new List<CuentasDebito>();

            while (reader.Read())
            {
                CuentasDebito cuenta = new CuentasDebito();
                cuenta.id = (int)reader["id"];
                cuenta.monto = (decimal)reader["monto"];
                cuenta.idPrestamo = (int)reader["id_prestamo"];
                cuenta.fecha = (DateTime)reader["fecha"];
                cuenta.deuda = (decimal)reader["deuda"];
                cuenta.estado = (string)reader["estado"];

                listaCuentasDebito.Add(cuenta);
            }

            var fechaActual = new DateTime(2021, 02, 15);
            var tasaInteres = 0.075;
            var tasaIVA = 0.16;
            int diasAnoComercial = 360;

            PagarPrestamos prestamos = new PagarPrestamos(fechaActual, tasaIVA, tasaInteres, diasAnoComercial);
            prestamos.CobrarCuenta(listaCuentasDebito);
            connection.Close();
        }
    }
}