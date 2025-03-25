using HotelReservas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HDPWP2
{
    public partial class Form1: Form
    {
        public Form1()
        {
            InitializeComponent();
            cmbTipoHabitacion.Items.Add("Estandar");
            cmbTipoHabitacion.Items.Add("VIP");
            cmbTipoHabitacion.SelectedIndex = 0;
            ActualizarListaReservas();
        }

       

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void btnAgregarReserva_Click(object sender, EventArgs e)
        {
            try
            {
                string tipo = cmbTipoHabitacion.SelectedItem.ToString();
                string nombre = txtNombreCliente.Text;
                int numeroHabitacion = int.Parse(txtNumeroHabitacion.Text);
                DateTime fecha = dtpFechaReserva.Value;
                int duracion = int.Parse(txtDuracion.Text);
                decimal tarifa = decimal.Parse(txtTarifa.Text);

                Reserva nuevaReserva = ReservaFactory.CrearReserva(tipo, nombre, numeroHabitacion, fecha, duracion, tarifa);
                GestorReservas.Instancia.AgregarReserva(nuevaReserva);

                MessageBox.Show("Reserva agregada con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ActualizarListaReservas();

                txtNombreCliente.Clear();
                txtNumeroHabitacion.Clear();
                txtDuracion.Clear();
                txtTarifa.Clear();
                cmbTipoHabitacion.SelectedIndex = 0;
                dtpFechaReserva.Value = DateTime.Now;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnEliminarReserva_Click(object sender, EventArgs e)
        {
            {
                if (dgvReservas.SelectedRows.Count > 0)
                {
                    try
                    {
                        int index = dgvReservas.SelectedRows[0].Index;
                        Reserva reservaAEliminar = GestorReservas.Instancia.ListarReservas()[index];
                        GestorReservas.Instancia.EliminarReserva(reservaAEliminar);

                        MessageBox.Show("Reserva eliminada con éxito", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        ActualizarListaReservas();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Seleccione una reserva para eliminar.", "Advertencia", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

        }

        private void ActualizarListaReservas()
        {
            dgvReservas.Rows.Clear();
            foreach (var reserva in GestorReservas.Instancia.ListarReservas())
            {
                dgvReservas.Rows.Add(reserva.NombreCliente, reserva.NumeroHabitacion, reserva.FechaReserva.ToShortDateString(), reserva.DuracionEstadia, reserva.CalcularCostoTotal());
            }
        }

    }
}
