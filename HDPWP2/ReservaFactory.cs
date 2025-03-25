using System;

namespace HotelReservas
{
    public static class ReservaFactory
    {
        public static Reserva CrearReserva(string tipo, string nombreCliente, int numeroHabitacion, DateTime fechaReserva, int duracionEstadia, decimal tarifa)
        {
            if (tipo == "Estandar")
                return new HabitacionEstandar(nombreCliente, numeroHabitacion, fechaReserva, duracionEstadia, tarifa);
            else if (tipo == "VIP")
                return new HabitacionVIP(nombreCliente, numeroHabitacion, fechaReserva, duracionEstadia, tarifa);
            else
                throw new ArgumentException("Tipo de habitación no válido.");
        }
    }
}