using BookClient.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookClient.ViewModels
{
    // Calling an Asynchronous Method
    // The following code example shows a sample application command implementation 
    // that simulates a file downloading through an asynchronous method
    // https://blog.xamarin.com/simplifying-events-with-commanding/
    class DownloadViewModel : ObservableObject
    {
        bool canDownload = true;
        string simulatedDownloadResult;

        public ICommand SimulateDownloadCommand { get; private set; }

        public DownloadViewModel()
        {
            SimulateDownloadCommand = new Command(async () => await SimulateDownloadAsync(), () => canDownload);
        }
        
        public string SimulatedDownloadResult
        {
            get => simulatedDownloadResult;
            private set
            {
                if (simulatedDownloadResult == value) return;
                simulatedDownloadResult = value;
                OnPropertyChanged("SimulatedDownloadResult");
            }
        }

       private async Task SimulateDownloadAsync()
        {
            CanInitiateNewDownload(false);
            SimulatedDownloadResult = string.Empty;
            await Task.Run(SimulateDownload);
            SimulatedDownloadResult = "Simulated download complete";
            CanInitiateNewDownload(true);
        }

       private void CanInitiateNewDownload(bool value)
        {
            canDownload = value;
            ((Command)SimulateDownloadCommand).ChangeCanExecute();
        }

       private static void SimulateDownload()
        {
            // Simulate a 5 second pause
            var endTime = DateTime.Now.AddSeconds(5);
            while (true)
            {
                if (DateTime.Now >= endTime)  
                    break;
            }
        }
    }
}
