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
	public class TeamWindowViewModel : ObservableRecipient
	{
		public RestCollection<Team> Teams { get; set; }

		private Team selectedTeam;

		public Team SelectedTeam
		{
			get { return selectedTeam; }
			set
			{
				if (value != null)
				{
					selectedTeam = new Team()
					{
						TeamId = value.TeamId,
						TeamName = value.TeamName,
						TeamPoints = value.TeamPoints,
						Budget = value.Budget,
						CarId = value.CarId,
					};
					OnPropertyChanged();
					(DeleteTeamCommand as RelayCommand).NotifyCanExecuteChanged();
				}
			}
		}


		public ICommand CreateTeamCommand { get; set; }
		public ICommand DeleteTeamCommand { get; set; }
		public ICommand UpdateTeamCommand { get; set; }

		public static bool IsInDesignedMode
		{
			get
			{
				var prop = DesignerProperties.IsInDesignModeProperty;
				return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
			}
		}

		public TeamWindowViewModel()
		{
			if (!IsInDesignedMode)
			{
				Teams = new RestCollection<Team>("http://localhost:4608/", "team", "hub");

				CreateTeamCommand = new RelayCommand(() =>
				{
					Teams.Add(new Team()
					{
						TeamName = SelectedTeam.TeamName,
						CarId = SelectedTeam.CarId,
						Budget = SelectedTeam.Budget,
						TeamPoints = SelectedTeam.TeamPoints,
					});
				});

				UpdateTeamCommand = new RelayCommand(() =>
				{
					Teams.Update(SelectedTeam);
				});

				DeleteTeamCommand = new RelayCommand(() =>
				{
					Teams.Delete(SelectedTeam.TeamId);
				},
				() =>
				{
					return SelectedTeam != null;
				});

				SelectedTeam = new Team();
			}
		}
	}
}
