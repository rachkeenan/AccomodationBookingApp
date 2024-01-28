using BookingApplication;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BookingAppWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public BookingAppContext db = new BookingAppContext();

        readonly List<string> accTypes = new List<string>() { "All", "Apartment", "House", "Villa", "Cabin" };

        public MainWindow()
        {
            InitializeComponent();
            AccomodationComboBox.ItemsSource = accTypes;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ShowAllAccomodation();
            ShowAllBookings();

            // Pre populate
            AccomodationComboBox.SelectedItem = "All";
            DpStartDate.SelectedDate = DateTime.Now;
            DpEndDate.SelectedDate = DateTime.Now.AddDays(2);
        }

        // Query 1 - Available Accomodations
        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            // Clear the textbox before a search
            TbxSelectedItem.Text = null;

            FilterAvailableAccomodations();
        }

        public void FilterAvailableAccomodations()
        {
            // Variables
            string selectedAccomodation = AccomodationComboBox.SelectedItem.ToString();
            DateTime startDate = (DateTime)DpStartDate.SelectedDate;
            DateTime endDate = (DateTime)DpEndDate.SelectedDate;

            if (selectedAccomodation == "All")
            {
                // https://stackoverflow.com/questions/15179539/linq-not-exists-multiple-tables
                // https://stackoverflow.com/questions/9031008/how-to-select-where-not-exist-using-linq

                // Queries the accomodation database where there is not a booking in the bookings database
                //    with an end date that is less than or equal to the selected start date 
                //    and a start date that is greater than or equal to the selected end date
                var availableAccomodations = (
                    from a in db.Accomodations
                    where
                    // A statement that returns true or false
                    !(
                    from b in db.Bookings
                    where
                        b.EndDate >= startDate
                        && b.StartDate <= endDate
                    select b.AccomodationId
                    )
                    .Contains(a.Id)
                    select a
                    )
                    .ToList();

                LbxAvailableAccomodations.ItemsSource = availableAccomodations;
            }

            else if (selectedAccomodation == "Apartment")
            {
                var availableApartments = (
                    from a in db.Accomodations
                    where
                    !(
                    from b in db.Bookings
                    where
                        b.EndDate >= startDate
                        && b.StartDate <= endDate
                    select b.AccomodationId
                    )
                    .Contains(a.Id)
                    // Gets the accomodation types that are apartments
                    where a.AccomodationType == "Apartment"
                    select a
                    )
                    .ToList();

                LbxAvailableAccomodations.ItemsSource = availableApartments;
            }

            else if (selectedAccomodation == "House")
            {
                var availableHouses = (
                    from a in db.Accomodations
                    where
                    !(
                    from b in db.Bookings
                    where
                        b.EndDate >= startDate
                        && b.StartDate <= endDate
                    select b.AccomodationId
                    )
                    .Contains(a.Id)
                    // Gets the accomodation types that are houses
                    where a.AccomodationType == "House"
                    select a
                    )
                    .ToList();

                LbxAvailableAccomodations.ItemsSource = availableHouses;
            }

            else if (selectedAccomodation == "Villa")
            {
                var availableVillas = (
                    from a in db.Accomodations
                    where
                    // A statement that returns true or false
                    !(
                    from b in db.Bookings
                    where
                        b.EndDate >= startDate
                        && b.StartDate <= endDate
                    select b.AccomodationId
                    )
                    .Contains(a.Id)
                    // Gets the accomodation types that are villas
                    where a.AccomodationType == "Villa"
                    select a
                    )
                    .ToList();

                LbxAvailableAccomodations.ItemsSource = availableVillas;
            }
            
            else if (selectedAccomodation == "Cabin")
            {
                var availableCabins = (
                    from a in db.Accomodations
                    where
                    // A statement that returns true or false
                    !(
                    from b in db.Bookings
                    where
                        b.EndDate >= startDate
                        && b.StartDate <= endDate
                    select b.AccomodationId
                    )
                    .Contains(a.Id)
                    // Gets the accomodation types that are villas
                    where a.AccomodationType == "Cabin"
                    select a
                    )
                    .ToList();

                LbxAvailableAccomodations.ItemsSource = availableCabins;
            }
            else
            {
                return;
            }

        }

        public void LbxAvailableAccomodations_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Variables
            Accomodation selectedAccomodation = LbxAvailableAccomodations.SelectedItem as Accomodation;
            DateTime selectedStartDate = (DateTime)DpStartDate.SelectedDate;
            DateTime selectedEndDate = (DateTime)DpEndDate.SelectedDate;

            if (selectedAccomodation != null)
            {
                TbxSelectedItem.Text = selectedAccomodation.ToString() + $"\nCheck In: {selectedStartDate}\nCheck Out: {selectedEndDate}";
            }
        }

        // Insert a booking
        private void BtnBook_Click(object sender, RoutedEventArgs e)
        {
            // Variables
            DateTime selectedStartDate = (DateTime)DpStartDate.SelectedDate;
            DateTime selectedEndDate = (DateTime)DpEndDate.SelectedDate;
            Accomodation selectedAccomodation = LbxAvailableAccomodations.SelectedItem as Accomodation;

            // Create new booking
            Booking newBooking = new Booking()
            {
                AccomodationId = selectedAccomodation.Id,
                StartDate = selectedStartDate,
                EndDate = selectedEndDate,
            };

            db.Bookings.Add(newBooking);
            db.SaveChanges();

            // Displays a message box to the user with the booked accomodation information
            MessageBox.Show(selectedAccomodation.ToString() + $"\nCheck In: {selectedStartDate}\nCheck Out: {selectedEndDate}");
        }

        // Display all bookings
        private void ShowAllBookings()
        {
            var queryBookings = from b in db.Bookings
                                select b;

            GridBookings.ItemsSource = queryBookings.ToList();
        }

        private void ShowAllAccomodation()
        {
            var queryAccomodation = from a in db.Accomodations
                                    select new
                                    {
                                        a.Id,
                                        a.AccomodationType,
                                        a.Name,
                                        a.NoOfRooms,
                                    };

            GridAllAccomodations.ItemsSource = queryAccomodation.ToList();
        }

        // Delete a booking
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Get selected booking Id
            Booking booking = GridBookings.SelectedItem as Booking;

            // Exit clause
            if (booking == null)
            {
                return;
            }

            int bookingId = booking.Id;

            // Delete selected item from the database
            var deleteBooking = from b in db.Bookings
                                where b.Id == bookingId
                                select b;


            db.Bookings.RemoveRange(deleteBooking);
            db.SaveChanges();
            ShowAllBookings();
        }
        }
}