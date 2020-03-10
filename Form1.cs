using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TournamentApp.Controllers;
using TournamentApp.Entity_Framework;
using TournamentApp.Model;
using TournamentApp.Utils;

namespace TournamentApp
{
    public partial class Form1 : Form
    {

        private static TournamentController tournamentController = new TournamentController();
        private List<tournament> tournaments = new List<tournament>();

        private tournament selectedTournament = null;

        private int[] columnsToChange = { 0, 1};
        private int[] columnsToHide = { 2 };
        private string[] titlesforColumns = { "ID", "Nombre de Torneo"};

        public Form1()
        {
            InitializeComponent();
        }

        // Llenar los datos seleccionados
        private void fillSelectedData(tournament currentTournament)
        {
            // Le damos el valor del torneo seleccionado accediendo a su propiedad
            txtName.Text = currentTournament.tournament_name;
        }

        // Cargar Tabla
        private void loadTable()
        {
            Operation<tournament> getTournamentOperation = tournamentController.getRecords();
            if (getTournamentOperation.State)
            {
                tournaments = getTournamentOperation.Data;
                dgvTournaments.DataSource = tournaments;
                // Arreglo de titulos a cambiar, columnas a cambiar y el contrl datagridview
                FormUtils.changeTitlesForDgv(titlesforColumns, columnsToChange, dgvTournaments);
                // Esconder las columnas y datagridview
                FormUtils.hideColumnsForDgv(columnsToHide, dgvTournaments);
                return;
            }
            MessageBox.Show("Error al cargar los datos de departamentos", "Error",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        // Guardar registro
        private void saveData()
        {
            // Creamos un objeto temporal de tipo de la entidad (torneo)
            tournament tempTour = new tournament
            {
                tournament_name = txtName.Text
            };
            // Realizamos la operación addRecord
            Operation<tournament> operation = tournamentController.addRecord(tempTour);
            if (operation.State)
            {
                MessageBox.Show("Torneo agregado con éxito", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadTable();
                cleanForm();
            }
        }

        // Actualizar registro
        private void updateData(tournament currentTournament)
        {
            Operation<tournament> operation = tournamentController.updateRecord(currentTournament);
            if (operation.State)
            {
                MessageBox.Show("Torneo actualizado con éxito", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadTable();
                cleanForm();
            }
            else
            {
                MessageBox.Show(operation.Error, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Eliminar un registro
        private void deleteData(tournament currentTournament)
        {
            Operation<tournament> operation = tournamentController.deleteRecord(currentTournament);
            if (operation.State)
            {
                MessageBox.Show("Torneo elimnado con éxito", "Éxito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadTable();
                cleanForm();
            }
            else
            {
                MessageBox.Show(operation.Error, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Limpiar formulario
        private void cleanForm()
        {
            FormUtils.clearTextbox(textControls());
            btnAgregar.Text = "Guardar";
        }

        private Control[] textControls()
        {
            Control[] controls =
            {
                txtName
            };
            return controls;
        }

        private void dgvTournaments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            try
            {
                //List<ControlErrorProvider> errorProvider = FormValidators.validFormTest(getValidators());
                //bool isValid = errorProvider == null;
                //if (isValid)
                //{
                    if (selectedTournament == null)
                    {
                        saveData();
                    }
                    else
                    {
                        selectedTournament.tournament_name = txtName.Text;
                        updateData(selectedTournament);
                    }
                //}
                //else
                //{
                //    this.errorProvider.Clear();
                //    MessageBox.Show("Algunos datos ingresados son inválidos.\n" +
                //        "Pase el puntero sobre los íconos de error para ver los detalles de cada campo.", "Error al ingresar datos",
                //        MessageBoxButtons.OK, MessageBoxIcon.Error);
                //    foreach (ControlErrorProvider controlErrorProvider in errorProvider)
                //    {
                //        this.errorProvider.SetError(controlErrorProvider.ControlName,
                //            controlErrorProvider.ErrorMessage);
                //    }
                //}
            }
            catch (Exception ex)
            {
                FormUtils.defaultErrorMessage(ex);
            }
        }

        private void btnNuevo_Click(object sender, EventArgs e)
        {
            try
            {
                cleanForm();
            }
            catch (Exception ex)
            {
                FormUtils.defaultErrorMessage(ex);
            }
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (selectedTournament == null)
            {
                MessageBox.Show("Seleccione un registro primero", "Error", MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
            else
            {
                selectedTournament.tournament_name = txtName.Text;
                deleteData(selectedTournament);
            }
        }

        private void dgvTournaments_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                if (index >= 0)
                {
                    selectedTournament = tournaments[index];
                    btnAgregar.Text = "Modificar";
                    fillSelectedData(selectedTournament);
                }
            }
            catch (Exception ex)
            {
                FormUtils.defaultErrorMessage(ex);
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                loadTable();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error al cargar",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
