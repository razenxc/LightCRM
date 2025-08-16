using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using LightCRM.Models;
using LightCRM.Views;

namespace LightCRM.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        public ObservableCollection<Client> ApiClients { get; }

        public MainViewModel()
        {
            var people = new List<Client>
            {
                new Client(Guid.NewGuid(), "Walter White", "walterhwhite@yahoo.com", "+12323242423"),
                new Client(Guid.NewGuid(), "Hank", "hank@aq.dea.gov", "+123424232322"),
            };
            ApiClients = new ObservableCollection<Client>(people);


            var auth = new AuthenticationView();
            auth.Show();
        }
    }
    
}
