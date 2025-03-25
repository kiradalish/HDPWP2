using System;
using System.Collections.Generic;

namespace HotelReservas
{
    public abstract class Reserva
    {
        public string NombreCliente { get; set; }
        public int NumeroHabitacion { get; set; }
        public DateTime FechaReserva { get; set; }
        public int DuracionEstadia { get; set; }

        public Reserva(string nombreCliente, int numeroHabitacion, DateTime fechaReserva, int duracionEstadia)
        {
            if (string.IsNullOrEmpty(nombreCliente))
                throw new ArgumentException("El nombre del cliente es obligatorio.");
            if (duracionEstadia < 1)
                throw new ArgumentException("La estadía debe ser mayor a 1 noche.");

            NombreCliente = nombreCliente;
            NumeroHabitacion = numeroHabitacion;
            FechaReserva = fechaReserva;
            DuracionEstadia = duracionEstadia;
        }

        public abstract decimal CalcularCostoTotal();
    }

    public class HabitacionEstandar : Reserva
    {
        public decimal TarifaFija { get; set; }

        public HabitacionEstandar(string nombreCliente, int numeroHabitacion, DateTime fechaReserva, int duracionEstadia, decimal tarifaFija)
            : base(nombreCliente, numeroHabitacion, fechaReserva, duracionEstadia)
        {
            if (tarifaFija <= 0)
                throw new ArgumentException("La tarifa fija debe ser mayor a cero.");

            TarifaFija = tarifaFija;
        }

        public override decimal CalcularCostoTotal()
        {
            return TarifaFija * DuracionEstadia;
        }
    }

    public class HabitacionVIP : Reserva
    {
        public decimal TarifaBase { get; set; }
        private const decimal Descuento = 0.20m;

        public HabitacionVIP(string nombreCliente, int numeroHabitacion, DateTime fechaReserva, int duracionEstadia, decimal tarifaBase)
            : base(nombreCliente, numeroHabitacion, fechaReserva, duracionEstadia)
        {
            if (tarifaBase <= 0)
                throw new ArgumentException("La tarifa base debe ser mayor a cero.");

            TarifaBase = tarifaBase;
        }

        public override decimal CalcularCostoTotal()
        {
            decimal costoTotal = TarifaBase * DuracionEstadia;
            if (DuracionEstadia > 5)
            {
                costoTotal -= costoTotal * Descuento;
            }
            return costoTotal;
        }
    }

    public class GestorReservas
    {
        private static GestorReservas instancia;
        private List<Reserva> reservas;

        private GestorReservas()
        {
            reservas = new List<Reserva>();
        }

        public static GestorReservas Instancia
        {
            get
            {
                if (instancia == null)
                {
                    instancia = new GestorReservas();
                }
                return instancia;
            }
        }

        public void AgregarReserva(Reserva reserva)
        {
            if (reservas.Exists(r => r.NumeroHabitacion == reserva.NumeroHabitacion && r.FechaReserva == reserva.FechaReserva))
            {
                throw new InvalidOperationException("Ya existe una reserva para esta habitación en la misma fecha.");
            }
            reservas.Add(reserva);
        }

        public void EliminarReserva(Reserva reserva)
        {
            if (!reservas.Contains(reserva))
            {
                throw new InvalidOperationException("La reserva no existe.");
            }
            reservas.Remove(reserva);
        }

        public List<Reserva> ListarReservas()
        {
            return new List<Reserva>(reservas);
        }
    }
}
