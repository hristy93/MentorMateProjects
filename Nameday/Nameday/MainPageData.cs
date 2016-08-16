using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Contacts;
using Windows.ApplicationModel.Email;

namespace Nameday
{
    public class MainPageData : INotifyPropertyChanged
    {
        private string _greeting;
        private string _filter;
        private NamedayModel _selectedNameDay;
        private List<NamedayModel> _allNamedays = new List<NamedayModel>();

        public MainPageData()
        {
            Namedays = new ObservableCollection<NamedayModel>();

            if (DesignMode.DesignModeEnabled)
            {
                Contacts = new ObservableCollection<ContactEx>
                {
                    new ContactEx("Contact", "1"),
                    new ContactEx("Contact", "2"),
                    new ContactEx("Contact", "3"),
                    new ContactEx("Contact", "4")
                };

                for (int month = 1; month <= 12; month++)
                {
                    _allNamedays.Add(new NamedayModel(month, 1,
                        new string[] { "Adam" }));
                    _allNamedays.Add(new NamedayModel(month, 24,
                        new string[] { "Eve", "Andrew" }));
                }

                PerformFiltering();
            }
            else
            {
                LoadData();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public string Greeting
        {
            get { return _greeting; }
            set
            {
                if (_greeting == value)
                {
                    return;
                }

                _greeting = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Greeting)));
            }
        }

        public string Filter
        {
            get { return _filter; }
            set
            {
                if (_filter == value)
                {
                    return;
                }

                _filter = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Filter)));

                PerformFiltering();
            }
        }

        public ObservableCollection<NamedayModel> Namedays { get; set; }
        public ObservableCollection<ContactEx> Contacts { get; } = new ObservableCollection<ContactEx>();

        public NamedayModel SelectedNameday
        {
            get { return _selectedNameDay; }
            set
            {
                if (value == null)
                {
                    Greeting = "Hello World";
                }
                else
                {
                    Greeting = "Hello" + value.NamesAsString;
                }

                UpdateContacts();
            }
        }

        public async Task SendEmailAsync(Contact contact)
        {
            if (contact == null || contact.Emails.Count == 0)
                return;

            var msg = new EmailMessage();
            msg.To.Add(new EmailRecipient(contact.Emails[0].Address));
            msg.Subject = "Happy Nameday!";

            await EmailManager.ShowComposeNewEmailAsync(msg);
        }

        private async void UpdateContacts()
        {
            Contacts.Clear();

            if (SelectedNameday != null)
            {
                var contactStore =
                    await ContactManager.RequestStoreAsync(ContactStoreAccessType.AllContactsReadOnly);

                foreach (var name in SelectedNameday.Names)
                    foreach (var contact in await contactStore.FindContactsAsync(name))
                        Contacts.Add(new ContactEx(contact));
            }
        }

        private void PerformFiltering()
        {
            if (_filter == null)
                _filter = "";

            var lowerCaseFilter = Filter.ToLowerInvariant().Trim();

            var result =
                _allNamedays.Where(d => d.NamesAsString.ToLowerInvariant()
                .Contains(lowerCaseFilter))
                .ToList();

            var toRemove = Namedays.Except(result).ToList();

            foreach (var x in toRemove)
                Namedays.Remove(x);

            var resultCount = result.Count;
            for (int i = 0; i < resultCount; i++)
            {
                var resultItem = result[i];
                if (i + 1 > Namedays.Count || !Namedays[i].Equals(resultItem))
                    Namedays.Insert(i, resultItem);
            }
        }

        private async void LoadData()
        {
            _allNamedays = await NamedayRepository.GetAllNamedaysAsync();
            PerformFiltering();
        }
    }
}
