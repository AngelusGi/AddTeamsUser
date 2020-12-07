using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Microsoft.Toolkit.Graph.Providers;
using Microsoft.Toolkit.Uwp.UI.Controls;
using Microsoft.Graph;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using Windows.UI.Popups;

// Il modello di elemento Pagina vuota è documentato all'indirizzo https://go.microsoft.com/fwlink/?LinkId=234238

namespace UwpClient
{
    /// <summary>
    /// Pagina vuota che può essere usata autonomamente oppure per l'esplorazione all'interno di un frame.
    /// </summary>
    public sealed partial class CalendarPage : Page
    {
        public CalendarPage()
        {
            this.InitializeComponent();
        }

        private void ShowNotification(string message)
        {
            // Get the main page that contains the InAppNotification
            var mainPage = (Window.Current.Content as Frame).Content as MainPage;

            // Get the notification control
            var notification = mainPage.FindName("Notification") as InAppNotification;

            notification.Show(message);
        }


        protected async void SearchButton_Click(object sender, RoutedEventArgs e)
        {

            var dateToSearch = GetInsertDate();
            //Debug.WriteLine($"{nameof(dateToSearch)} -> {dateToSearch}");

            LoadStatus.IsActive = true;

            // Get the Graph client from the provider
            var graphClient = ProviderManager.Instance.GlobalProvider.Graph;

            try
            {

                // Get all the events
                var events = await graphClient.Me.Events.Request()
                    .Header("Prefer", "outlook.timezone=\"W. Europe Standard Time\"")
                    //.Select(value: "subject,attendees,start,end")
                    .OrderBy("createdDateTime DESC")
                    .GetAsync();

                var today = $"{DateTime.Now.Date:s}";

                var aboutMe = await graphClient.Me.Request().GetAsync();

                var eventsByMe = from result in events.CurrentPage.ToList()
                                 let organizedByMe = ((result.Organizer.EmailAddress.Address == aboutMe.UserPrincipalName) || (result.Organizer.EmailAddress.Address == aboutMe.Mail))
                                 let noPastEvents = Convert.ToDateTime(result.Start.DateTime).Date.CompareTo(Convert.ToDateTime(today)) >= 0 // Today or future events
                                 where organizedByMe && noPastEvents
                                 select result;

                if (dateToSearch.HasValue)
                {
                    var userDate = $"{dateToSearch:s}";

                    Debug.WriteLine(userDate);

                    var foundElements = from result in eventsByMe.ToList()
                                        let searchedDay = Convert.ToDateTime(result.Start.DateTime).Date.CompareTo(Convert.ToDateTime(userDate)) == 0 // Same day
                                        where searchedDay
                                        select result;

                    EventList.ItemsSource = foundElements.ToList();

                }
                else
                {
                    EventList.ItemsSource = eventsByMe.ToList();
                }

                // TEMPORARY: Show the results as JSON
                //Events.Text = JsonConvert.SerializeObject(events.CurrentPage);

                //Debug.WriteLine(EventList.ToString());
            }
            catch (Microsoft.Graph.ServiceException ex)
            {
                ShowNotification($"Exception getting events: {ex.Message}");
            }
            finally
            {
                LoadStatus.IsActive = false;
            }

        }

        private DateTime? GetInsertDate()
        {

            //return DayToSearch.Date.HasValue ? DayToSearch.Date.Value.Date : null;
            if (DayToSearch.Date.HasValue)
            {
                return DayToSearch.Date.Value.Date;
            }
            else
            {
                return null;
            }

        }

    }
}
