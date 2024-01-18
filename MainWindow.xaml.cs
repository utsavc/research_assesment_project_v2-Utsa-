using RAPLite.Controller;
using RAPLite.DataSource;
using RAPLite.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using RAPLitev2.DataSource;
using RAPLitev2.Entity;
using RAPLitev2.Controller;

namespace RAPLitev2
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            DataContext = this;  //setting the data context to MainWindow Object
                                 //In case of multiple Windows or Views or custom controls
                                 //you need to set the DataContext to relevant Objects
            InitializeComponent();
            
        }

        public ObservableCollection<Researcher> Researchers
        {
            get { return new ObservableCollection<Researcher>(new DataSource.ResearcherDataSource().ShowResearcher()); }
        }

        public ObservableCollection<Publication> Publications
        {
            get
            {
                if (ResearchersListView.SelectedItem != null)
                {
                    List<Publication> publications = (List<Publication>)new PublicationController().SearchByResearcher(ResearchersListView.SelectedItem as Researcher);
                    ObservableCollection<Publication> publist = new ObservableCollection<Publication>(publications);
                    return publist;
                }
                else
                {
                    return null;
                }
            }
        }


        private void EmploymentLevelComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Console.WriteLine(e.AddedItems[0].ToString());

            if (e.AddedItems[0] != null)
            {
                string selectedType = e.AddedItems[0].ToString();
                Position selectedLevel = Position.All;

                // Determine the selected employment level
                if (selectedType.EndsWith("Student"))
                {
                    selectedLevel = Position.Student;
                }
                else if (selectedType.EndsWith("Staff"))
                {
                    selectedLevel = Position.Staff;
                }
                else if (selectedType.EndsWith("A"))
                {
                    selectedLevel = Position.A;
                }
                else if (selectedType.EndsWith("B"))
                {
                    selectedLevel = Position.B;
                }
                else if (selectedType.EndsWith("C"))
                {
                    selectedLevel = Position.C;
                }
                else if (selectedType.EndsWith("D"))
                {
                    selectedLevel = Position.D;
                }
                else if (selectedType.EndsWith("E"))
                {
                    selectedLevel = Position.E;
                }

                ResearchersListView.ItemsSource = ResearcherController.FilterByTypeAndLevel(selectedLevel, selectedType, Researchers);
            }
        }

        private void PublicationsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            PublicationDetailsStackPanel.DataContext = PublicationsListView.SelectedItem as Publication;
        }

        private void ResearchersListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            string firstPart=null;
            string secondPart=null;
            if (e.AddedItems.Count > 0)
            {
                Researcher selectedResearcher = e.AddedItems[0] as Researcher;
                
                string[] parts = selectedResearcher.Name.Split(' ');

                if (parts.Length >= 2)
                {
                     firstPart = parts[0];
                    secondPart = parts[1];
                }

                selectedResearcher.Name = secondPart + " " + firstPart;

                    if (selectedResearcher != null)
                {
                    // Assuming you have a method to get publications based on a researcher
                    List<Publication> publications = new PublicationController().SearchByResearcher(selectedResearcher);

                    // Set the filtered publications as the ItemsSource for PublicationsListView
                    PublicationsListView.ItemsSource = publications;

                    // Rest of your code to update UI elements (e.g., researcher photo)
                    var photo = new Image();    

                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.UriSource = selectedResearcher.Photo;
                    bitmap.EndInit();
                    ImageSource imageSource = bitmap;
                    ResercherPhoto.Source = imageSource;

                    Name.Content = selectedResearcher.Name;
                    JobTitle.Content = selectedResearcher.Title;
                    Campus.Content = selectedResearcher.CampusName;
                    School.Content = selectedResearcher.Unit;
                    Email.Content = selectedResearcher.Email;
                    Degree.Content = selectedResearcher.Degree;
                    CommencedInstitution.Content = selectedResearcher.UtasStart;
                    CommencedPosition.Content = selectedResearcher.CurrentStart;
                    Tenure.Content = selectedResearcher.Tenure;
                    CurrentJob.Content = selectedResearcher.CurrentJob;
                    PublicationField.Content = new PublicationController().CountPublicationsByResearcher(selectedResearcher);
                    Supervisor.Content = ResearcherController.GetResearcherNameBySupervisorId(selectedResearcher.SupervisorId, Researchers);
                    SupervisionField.Content = ResearcherController.CountSupervisions(selectedResearcher, Researchers);

                    List<Funding> fundings=FundingDataSource.LoadAll();

                    int selectedResearcherId = selectedResearcher.Id;
                    
                    List<Funding> researcherFundings = FundingsController.GetResearcherFundings(selectedResearcherId, fundings);
                    double totalFunding = FundingsController.CalculateTotalFunding(researcherFundings);
                    fundingReceived.Content = totalFunding;
                    string d=selectedResearcher.UtasStart;

                    DateTime date = DateTime.Parse(d);
                    DateTime currentDate = DateTime.Now;
                    int yearDifference = currentDate.Year - date.Year;

                    fundingPerformance.Content = totalFunding/yearDifference;

                    List<Publication> dpublications = new PublicationController().LoadAllPublications();
                    
                    int currentYear = DateTime.Now.Year;
                    
                    double threeYearAverage = selectedResearcher.CalculateThreeYearAverage();

                    average.Content = threeYearAverage;


                }
            }
        }






    

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = textBox.Text;
            List<Researcher> filteredResearchers = ResearcherController.FilterResearchersByLastName(searchText, Researchers);
            ResearchersListView.ItemsSource = filteredResearchers;
        }

    }
}
