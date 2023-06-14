using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using UI.Commands;
using UI.Models;

namespace UI.ViewModels
{
    public class AddTourViewModel : ViewModelBase
    {
        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        private readonly Tour _tour;
        private string _name;
        public string Name
        {
            get { return _name; }
            set 
            {
                _name = value; 
                OnPropertyChanged(nameof(Name));
            }
        }
        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }
        private string _from;
        public string From
        {
            get { return _from; }
            set
            {
                _from = value;
                OnPropertyChanged(nameof(From));
            }
        }
        private string _to;
        public string To
        {
            get { return _to; }
            set
            {
                _to = value;
                OnPropertyChanged(nameof(To));
            }
        }
        private string _transportType;
        public string TransportType
        {
            get { return _transportType; }
            set
            {
                _transportType = value;
                OnPropertyChanged(nameof(TransportType));
            }
        }
        /*private string Name => _tour.Name;
        private string Description => _tour.Description;
        private string From => _tour.From;
        private string To => _tour.To;
        private string TransportType => _tour.TransportType;*/

        public AddTourViewModel()
        {
            SubmitCommand = new AddTourCommand();
        }      
    }
}
