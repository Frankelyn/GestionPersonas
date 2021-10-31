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
    /// Interaction logic for rAportes.xaml
    /// </summary>
    public partial class rAportes : Window
    {
        private Aportes Aporte = new Aportes();
        public rAportes()
        {
            InitializeComponent();
            this.DataContext = Aporte;

            PersonaComboBox.ItemsSource = PersonasBLL.GetPersonas();
            PersonaComboBox.SelectedValuePath = "PersonaId";
            PersonaComboBox.DisplayMemberPath = "Nombres";

            TipoDeAporteComboBox.ItemsSource = TipoDeAportesBLL.GetTipoDeAportes();
            TipoDeAporteComboBox.SelectedValuePath = "TipoDeAporteId";
            TipoDeAporteComboBox.DisplayMemberPath = "Descripcion";

            Limpiar();
            TotalAportesTextbox.Text = "0.00";
            AporteTextBox.Text = "0.00";
        }

        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = Aporte;
        }

        private void Limpiar()
        {
            this.Aporte = new Aportes();
            this.DataContext = Aporte;
        }

        

        private bool ExisteEnLaBaseDeDatos()
        {
            Aportes esValido = AportesBLL.Buscar(Aporte.AporteId);

            return (esValido != null);
        }

        private void BuscarAporteButton_Click(object sender, RoutedEventArgs e)
        {
            Aportes encontrado = AportesBLL.Buscar(Aporte.AporteId);

            if (encontrado != null)
            {
                Aporte = encontrado;
                Cargar();
            }
            else
            {
                Limpiar();
                MessageBox.Show("Aporte no existe en la base de datos", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            Aporte.AportesDetalle.Add(new AportesDetalle
            {
                TipoDeAporte = (TiposDeAportes)TipoDeAporteComboBox.SelectedItem,
                //Persona = (Personas)PersonaComboBox.SelectedItem,
                Aporte = Utilidades.ToFloat(AporteTextBox.Text)
            });

            Aporte.totalAportes += Utilidades.ToFloat(AporteTextBox.Text);
            Cargar();
           
            AporteTextBox.Text = "0.00";
            AporteTextBox.Focus();

        }

        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            if (AportesDetalleDataGrid.Items.Count >= 1 && AportesDetalleDataGrid.SelectedIndex <= AportesDetalleDataGrid.Items.Count - 1)
            {
                Aporte.AportesDetalle.RemoveAt(AportesDetalleDataGrid.SelectedIndex);
                Aporte.totalAportes -= Utilidades.ToFloat(AporteTextBox.Text);
                Cargar();
            }

        }

        private void NuevoAporteButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarAporteButton_Click(object sender, RoutedEventArgs e)
        {
            bool paso = false;

            if (Aporte.AporteId == 0)
            {
                paso = AportesBLL.Guardar(Aporte);
            }
            else
            {
                if (ExisteEnLaBaseDeDatos())
                {
                    paso = AportesBLL.Guardar(Aporte);
                }
                else
                {
                    MessageBox.Show("No existe en la base de datos", "ERROR");
                }
            }

            if (paso)
            {
                Limpiar();
                MessageBox.Show("Guardado!", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Fallo al guardar", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void EliminarAporteButton_Click(object sender, RoutedEventArgs e)
        {
            Aportes existe = AportesBLL.Buscar(Aporte.AporteId);

            if (existe == null)
            {
                MessageBox.Show("No existe el grupo en la base de datos", "Fallo", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            else
            {
                AportesBLL.Eliminar(Aporte.AporteId);
                MessageBox.Show("Eliminado", "Exito", MessageBoxButton.OK, MessageBoxImage.Information);
                Limpiar();
            }

        }
    }
}
