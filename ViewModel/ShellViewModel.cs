using Revisao_Planejamento.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using VMS.TPS.Common.Model.API;

namespace Prametros_SBRTeSRS.ViewModel
{
    public class ShellViewModel : INotifyPropertyChanged
    {
        public ScriptContext context;
        public ShellViewModel(Patient p)
        {
            CoursesList = GetListOfCourse(p);           
            MeuComando = new RelayCommand(AllFuncs);
        }

        private List<Course> _coursesList;
        private Course _selectedCourse;
        private List<PlanSetup> _planinglist;
        private List<Structure> _structureList;
        private PlanSetup _selectedPlan;
        private ICommand _meuComando;
        private Structure _selectedPTV;
        private Structure _selectedD100;
        private Structure _selectedD50;
        private Structure _selected2CM;
        private int _numberOfFraction;
        private double _totalDose;
        private int _numberOfBeams;
        private string _volumePTV;
        private string _volumeD100;
        private string _volumeD50;
        private string _doseMax;

        private string _doseMax2CM;

        












        public List<PlanSetup> PlaningList
        {
            get { return _planinglist; }
            set { _planinglist = value; OnPropertyChanged(); }
        }
        public List<Course> CoursesList
        {
            get { return _coursesList; }
            set { _coursesList = value; OnPropertyChanged(); }
        }
        public List<Structure> StructureList
        {
            get { return _structureList; }
            set { _structureList = value; OnPropertyChanged(); }
        }
        public PlanSetup SelectedPlan
        {
            get { return _selectedPlan; }
            set 
            { 
                _selectedPlan = value;
                GetListOfStructures(SelectedPlan);
                OnPropertyChanged(); 
            }
        }
        
        public ICommand MeuComando
        {
            get { return _meuComando; }
            set
            {
                _meuComando = value;
                OnPropertyChanged();
            }
        }
        public Course SelectedCourse
        {
            get { return _selectedCourse; }
            set
            {
                _selectedCourse = value;
                GetListOfPlans(SelectedCourse);
                OnPropertyChanged();
            }
        }


        public List<Course> GetListOfCourse(Patient p)
        {
            List<Course> courses = new List<Course>();
            foreach (Course c in p.Courses)
            {
                courses.Add(c);
            }
            return courses;
        }
        public Structure SelectedPTV
        {
            get { return _selectedPTV; }
            set 
            { _selectedPTV = value; OnPropertyChanged(); }
        }
        public Structure SelectedD100
        {
            get { return _selectedD100; }
            set { _selectedD100 = value; OnPropertyChanged(); }
        }
        public Structure SelectedD50
        {
            get { return _selectedD50; }
            set { _selectedD50 = value; OnPropertyChanged(); }
        }
        public Structure Selected2CM
        {
            get { return _selected2CM; }
            set { _selected2CM = value; OnPropertyChanged(); }
        }

        public int NumberOfFraction
        {
            get { return _numberOfFraction; }
            set 
            { 
                _numberOfFraction = value;
                OnPropertyChanged();
            }
        }
        public double TotalDose
        {
            get { return _totalDose; }
            set { _totalDose = value; OnPropertyChanged(); }
        }

        public int NumberOfBeams
        {
            get { return _numberOfBeams; }
            set { _numberOfBeams = value; OnPropertyChanged(); }
        }

        public string VolumePTV
        {
            get { return _volumePTV; }
            set { _volumePTV = value; OnPropertyChanged(); }
        }

        public string VolumeD100
        {
            get { return _volumeD100; }
            set { _volumeD100 = value; OnPropertyChanged(); }
        }

        public string VolumeD50
        {
            get { return _volumeD50; }
            set { _volumeD50 = value; OnPropertyChanged(); }
        }
        public string DoseMax
        {
            get { return _doseMax; }
            set { _doseMax = value; OnPropertyChanged(); }
        }
        public string DoseMax2CM
        {
            get { return _doseMax2CM; }
            set { _doseMax2CM = value; OnPropertyChanged(); }
        }
        public void GetListOfStructures(PlanSetup plan)
        {
            if (SelectedPlan != null)
            {
                try
                {
                    List<Structure> structures = new List<Structure>();
                    foreach(Structure structure in plan.StructureSet.Structures)
                    {
                        if (structure.IsEmpty)
                        {
                            continue;
                        }
                        else
                        {
                            structures.Add(structure);
                        }
                                              
                    }
                    StructureList = structures;
                }
                catch(Exception e)
                {
                    MessageBox.Show("Erro ao obter a lista de estruturas: ");
                }
            }
            else
            {
                StructureList = new List<Structure>();
            }
        }
        public void GetListOfPlans(Course c)
        {
            if (SelectedCourse != null)
            {
                try
                {
                    List<PlanSetup> plans = new List<PlanSetup>();
                    foreach (PlanSetup plan in c.PlanSetups)
                    {
                        plans.Add(plan);
                    }

                    PlaningList = plans;
                }
                catch (Exception e)
                {
                    MessageBox.Show("Erro ao obter a lista de planos: " + e.Message);
                }
            }
            else
            {
                PlaningList = new List<PlanSetup>();
            }
        }

        public void GetNumberOfFraction()
        {
            NumberOfFraction = SelectedPlan.UniqueFractionation.NumberOfFractions.Value;
        }
        public void GetTotalDose()
        {
            TotalDose = SelectedPlan.TotalPrescribedDose.Dose;
        }

        public void GetNumberOfBeams()
        {
            List<Beam> listOfBeams = new List<Beam>();
            foreach (Beam b in SelectedPlan.Beams)
            {
                if(b.IsSetupField == false)
                {
                    listOfBeams.Add(b);
                }
            }
            NumberOfBeams = listOfBeams.Count;
        }

        public void GetVolumeOfPTV()
        {
            VolumePTV = SelectedPTV.Volume.ToString("F2");
        }
        public void GetVolumeOfD100()
        {
            VolumeD100 = SelectedD100.Volume.ToString("F2");
        }
        public void GetVolumeOfD50()
        {
            VolumeD50 = SelectedD50.Volume.ToString("F2");
        }

        public void GetDoseMax()
        {
            DoseMax = SelectedPlan.Dose.DoseMax3D.Dose.ToString("F2");
        }

        public void GetDoseMax2CM()
        {
            DoseMax2CM = (SelectedPlan.GetDoseAtVolume(Selected2CM, 0.0, 0, 0)).ToString();
        }
        private void AllFuncs()
        {
            GetNumberOfFraction();
            GetTotalDose();
            GetNumberOfBeams();
            GetVolumeOfPTV();
            GetVolumeOfD100();
            GetVolumeOfD50();
            GetDoseMax();
            GetDoseMax2CM();

            MessageBox.Show("Analisado!");
        }

        #region Property Changed
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
        #endregion
    }
}
