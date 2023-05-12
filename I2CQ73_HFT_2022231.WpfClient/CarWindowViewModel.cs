using I2CQ73_HFT_2022231.Models;
using Microsoft.Toolkit.Mvvm.ComponentModel;
using Microsoft.Toolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;

namespace I2CQ73_HFT_2022231.WpfClient
{
	public class CarWindowViewModel : ObservableRecipient
	{
		public RestCollection<Car> Cars { get; set; }

		private Car selectedCar;

		public Car SelectedCar
		{
			get { return selectedCar; }
			set
			{
				if (value != null)
				{
					selectedCar = new Car()
					{
						CarId = value.CarId,
						EngineBrand = value.EngineBrand,
						Horsepower = value.Horsepower,
						MaxSpeed = value.MaxSpeed,
					};
					OnPropertyChanged();
					(DeleteCarCommand as RelayCommand).NotifyCanExecuteChanged();
				}
			}
		}


		public ICommand CreateCarCommand { get; set; }
		public ICommand DeleteCarCommand { get; set; }
		public ICommand UpdateCarCommand { get; set; }

		public static bool IsInDesignedMode
		{
			get
			{
				var prop = DesignerProperties.IsInDesignModeProperty;
				return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
			}
		}

		public CarWindowViewModel()
		{
			if (!IsInDesignedMode)
			{
				Cars = new RestCollection<Car>("http://localhost:4608/", "car", "hub");

				CreateCarCommand = new RelayCommand(() =>
				{
					Cars.Add(new Car()
					{
						EngineBrand = SelectedCar.EngineBrand,
						Horsepower = SelectedCar.Horsepower,
						MaxSpeed = SelectedCar.MaxSpeed,
					});
				});

				UpdateCarCommand = new RelayCommand(() =>
				{
					Cars.Update(SelectedCar);
				});

				DeleteCarCommand = new RelayCommand(() =>
				{
					Cars.Delete(SelectedCar.CarId);
				},
				() =>
				{
					return SelectedCar != null;
				});

				SelectedCar = new Car();
			}
		}
	}
}
