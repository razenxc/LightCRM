using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Avalonia.Controls;
using CommunityToolkit.Mvvm.ComponentModel;
using LightCRM.Models;
using LightCRM.Views;

namespace LightCRM.ViewModels
{
    public partial class MainViewModel : ViewModelBase
    {
        // TEMP CONSTANTS
        public const string API_ADDRESS = "https://localhost:59050";
        public string AccessToken = "";
        // 

        // Data Grids
        [ObservableProperty]
        private ObservableCollection<Client> _apiClients = new();

        [ObservableProperty]
        private ObservableCollection<Order> _apiOrders = new();

        [ObservableProperty]
        private ObservableCollection<Product> _apiProducts = new();

        // ======
        // Clients
        [ObservableProperty]
        private int _selectedApiClient;

        // textboxes
        [ObservableProperty]
        private string? _apiClientId;

        [ObservableProperty]
        private string? _apiClientName;

        [ObservableProperty]
        private string? _apiClientEmail;

        [ObservableProperty]
        private string? _apiClientPhone;


        // Misc

        [ObservableProperty]
        public string _status;

        public MainViewModel()
        {
            _ = RunAsyncTasks();
        }


        public async Task RunAsyncTasks()
        {
            await FetchClients();
        }

        public async Task FetchClients()
        {
            List<Client>? clients = new();
            
            try
            {
                HttpClient client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
                clients = await client.GetFromJsonAsync<List<Client>>($"{API_ADDRESS}/api/clients");
            }
            catch (Exception ex)
            {
                Status = ex.Message;
            }
            finally
            {
                ApiClients = new ObservableCollection<Client>(clients);
            }
        }

        public void ApiClientRead()
        {
            Client selectedClient = ApiClients[SelectedApiClient];
            ApiClientId = selectedClient.Id.ToString();
            ApiClientEmail = selectedClient.Email;
            ApiClientName = selectedClient.Name;
            ApiClientPhone = selectedClient.Phone;
        }
    }
    
}
