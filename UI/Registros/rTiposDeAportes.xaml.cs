using GestionPersonas.BLL;
using GestionPersonas.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestionPersonas.UI.Registros
{
    /// <summary>
    /// Interaction logic for rTiposDeAportes.xaml
    /// </summary>
    public partial class rTiposDeAportes : Window
    {
        public rTiposDeAportes()
        {
            InitializeComponent();
            this.DataContext = tAporte;

        }

        private TiposDeAportes tAporte = new();

        private void Limpiar()
        {
            this.tAporte = new TiposDeAportes();
            this.DataContext = tAporte;
        }

        private bool Validar()
        {
            bool esValido = true;

            if(DescripcionTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Falta la descripcion", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if(MetaTextBox.Text.Length == 0)
            {
                esValido = false;
                MessageBox.Show("Falta la Meta", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return esValido;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            var tipoAporte = TipoDeAportesBLL.Buscar(Utilidades.ToInt(TipoAporteIdTextBox.Text));

            if(tipoAporte == null)
            {
                MessageBox.Show("Registro no encontrado!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if(tipoAporte != null)
            {
                this.tAporte = tipoAporte;
            }
            else
            {
                this.tAporte = new TiposDeAportes();
            }

            this.DataContext = this.tAporte;
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
            {
                return;
            }

            var paso = TipoDeAportesBLL.Guardar(tAporte);

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Trasaccion exitosa!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Trasaccion fallida!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if (TipoDeAportesBLL.Eliminar(Utilidades.ToInt(TipoAporteIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro eliminado!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("No se pudo eliminar!", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
