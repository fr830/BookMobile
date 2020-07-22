using BookClient.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace BookClient.ViewModels
{
    // Simplifying Events with Commanding
    // https://blog.xamarin.com/simplifying-events-with-commanding/
    class SquareRootViewModel : ObservableObject
    {
        // ICommand implementations
        public double SquareRootWithParameterResult { get; private set; }
        public ICommand SquareRootWithParameterCommand { get; private set; }

        public SquareRootViewModel()
        {
            SquareRootWithParameterCommand = new Command<string>(CalculateSquareRoot);
        }

        private void CalculateSquareRoot(string value)
        {
            double num = Convert.ToDouble(value);
            SquareRootWithParameterResult = Math.Sqrt(num);
            OnPropertyChanged("SquareRootWithParameterResult");
        }
    }
}
