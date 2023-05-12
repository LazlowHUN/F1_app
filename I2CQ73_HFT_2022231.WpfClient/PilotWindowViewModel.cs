using I2CQ73_HFT_2022231.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace I2CQ73_HFT_2022231.WpfClient
{
	public class PilotWindowViewModel : ObservableRecipient
	{
		public RestCollection<Pilot> Pilots { get; set; }

		private Pilot selectedPilot;

		public Pilot SelectedPilot
		{
			get { return selectedPilot; }
			set 
			{
				if (value != null)
				{
					selectedPilot = new Pilot()
					{
						PilotName = value.PilotName,
						PilotId = value.PilotId,
						PilotAge = value.PilotAge,
						TeamId = value.TeamId,
					};
					OnPropertyChanged();
					(DeletePilotCommand as RelayCommand).NotifyCanExecuteChanged();
				}
			}
		}


		public ICommand CreatePilotCommand { get; set; }
		public ICommand DeletePilotCommand { get; set; }
		public ICommand UpdatePilotCommand { get; set; }

		public static bool IsInDesignedMode
		{
			get
			{
				var prop = DesignerProperties.IsInDesignModeProperty;
				return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
			}
		}

		public PilotWindowViewModel() 
		{
			if (!IsInDesignedMode)
			{
				Pilots = new RestCollection<Pilot>("http://localhost:4608/", "pilot", "hub");

				CreatePilotCommand = new RelayCommand(() =>
				{
					Pilots.Add(new Pilot()
					{
						PilotName = SelectedPilot.PilotName,
						PilotAge = SelectedPilot.PilotAge,
						TeamId = SelectedPilot.TeamId,
					});
				});

				UpdatePilotCommand = new RelayCommand(() =>
				{
					Pilots.Update(SelectedPilot);
				});

				DeletePilotCommand = new RelayCommand(() =>
				{
					Pilots.Delete(SelectedPilot.PilotId);
				},
				() =>
				{
					return SelectedPilot != null;
				});

				SelectedPilot = new Pilot();
			}
		}
	}
}
