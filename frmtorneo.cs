using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TournamentApp.Controllers;
using TournamentApp.Entity_Framework;
using TournamentApp.Model;
using TournamentApp.Utils;
using TournamentApp.Models;
using static TournamentApp.Models.FormValidators;

namespace TournamentApp
{
    public partial class frmtorneo : UserControl
    {

        private static frmtorneo _instance;
        public static frmtorneo Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new frmtorneo();
                }
                return _instance;
            }
        }
        private static TournamentController tournamentController = new TournamentController();
        private List<tournament> tournaments = new List<tournament>(); //hacer una lista de la tabla torneo

        private tournament selectedTournament = null;//obtener los datos para el datagridview

        private int[] columnsToChange = { 0, 1 };
        private int[] columnsToHide = { 2 };
        private string[] titlesforColumns = { "ID", "Nombre de Torneo" };
        public frmtorneo()
        {
            InitializeComponent();
        }
        private void fillSelectedData(tournament currentTournament)
        {
            // Le damos el valor del torneo seleccionado accediendo a su propiedad
            txtnombretorneo.Text = currentTournament.tournament_name;
        }

        private void loadTable()
        {
            try
            {
                Operation<tournament> getTournamentOperation = tournamentController.getRecords();
                if (getTournamentOperation.State)
                {
                    tournaments = getTournamentOperation.Data;
                    dgvTournaments.DataSource = tournaments;
                    // Arreglo de titulos a cambiar, columnas a cambiar y el control c
                    FormUtils.changeTitlesForDgv(titlesforColumns, columnsToChange, dgvTournaments);
                    // Esconder las columnas y datagridview
                    FormUtils.hideColumnsForDgv(columnsToHide, dgvTournaments);
                    return;
                }
                MessageBox.Show("Error al cargar los datos de departamentos", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                FormUtils.defaultErrorMessage(ex);
            }
        }

        // Guardar registro
        private void saveData()
        {
            // Creamos un objeto temporal de tipo de la entidad (torneo)
            tournament tempTour = new tournament
            {
                tournament_name = txtnombretorneo.Text
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
            btnagregar.Text = "Guardar";
            selectedTournament = null;
            errorProvider.Clear();
        }
        private ToValidate[] getValidators() //metodo para validacion 
        {
            ToValidate[] validators =
            {
                new ToValidate(txtnombretorneo, new ControlValidator[] { FormValidators.hasText },
                new string[] { "Ingresa un nombre de torneo" })
            };
            return validators;
        }

        private Control[] textControls() // metodo validacion 
        {
            Control[] controls =
            {
                txtnombretorneo
            };
            return controls;
        }

        private void btnagregar_Click(object sender, EventArgs e)
        {
            try
            {
                List<ControlErrorProvider> errorProvider = FormValidators.validFormTest(getValidators());
                bool isValid = errorProvider == null;
                if (isValid)
                {
                    if (selectedTournament == null)
                    {
                        saveData();
                    }
                    else
                    {
                        selectedTournament.tournament_name = txtnombretorneo.Text;
                        updateData(selectedTournament);
                    }
                }
                else
                {
                    this.errorProvider.Clear();
                    MessageBox.Show("Algunos datos ingresados son inválidos.\n" +
                        "Pase el puntero sobre los íconos de error para ver los detalles de cada campo.", "Error al ingresar datos",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    foreach (ControlErrorProvider controlErrorProvider in errorProvider)
                    {
                        this.errorProvider.SetError(controlErrorProvider.ControlName,
                            controlErrorProvider.ErrorMessage);
                    }
                }
            }
            catch (Exception ex)
            {
                // FormUtils.defaultErrorMessage(ex);
            }
        }

        private void btnnuevo_Click(object sender, EventArgs e)
        {
            try
            {
                cleanForm();
            }
            catch (Exception ex)
            {
                //FormUtils.defaultErrorMessage(ex);
            }
        }

        private void btneliminar_Click(object sender, EventArgs e)
        {
            try
            {
                if (selectedTournament == null)
                {
                    MessageBox.Show("Seleccione un registro primero", "Error", MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
                else
                {
                    selectedTournament.tournament_name = txtnombretorneo.Text;
                    deleteData(selectedTournament);
                }
            }
            catch (Exception ex)
            {
                //FormUtils.defaultErrorMessage(ex);
            }
        }

        private void frmtorneo_Load(object sender, EventArgs e)
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

        private void dgvTournaments_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                int index = e.RowIndex;
                if (index >= 0)
                {
                    selectedTournament = tournaments[index];
                    btnagregar.Text = "Modificar";
                    fillSelectedData(selectedTournament);
                }
            }
            catch (Exception ex)
            {
                FormUtils.defaultErrorMessage(ex);
            }
        }
    }
}
