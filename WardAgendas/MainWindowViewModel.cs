using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace WardAgendas
{
	public class MainWindowViewModel : INotifyPropertyChanged
	{
		private AgendaServiceAgent _agendaServiceAgent;
		private EmailServiceAgent _emailServiceAgent;
		private Dictionary<string, Member> _bishopricMembers;
		private Dictionary<string, Member> _pecMembers;
		private Dictionary<string, Member> _wardCouncilMembers;
		string _meeting;

		#region Properties

		public int CurrentTabIndex
		{
			get { return _CurrentTabIndex; }
			set
			{
				_CurrentTabIndex = value;
				InitializeAssignments();
				switch (value)
				{
					case 0:
						_meeting = "Bishopric";
						break;
					case 1:
						_meeting = "PEC";
						break;
					case 2:
						_meeting = "Ward Council";
						break;
					default:
						break;
				}	
				NotifyPropertyChanged("CurrentTabIndex");
			}
		}	private int _CurrentTabIndex;

		public string OpeningPrayer
		{
			get { return _OpeningPrayer; }
			set
			{
				_OpeningPrayer = value;
				NotifyPropertyChanged("OpeningPrayer");
			}
		}	private string _OpeningPrayer;
		public string Thought
		{
			get { return _Thought; }
			set
			{
				_Thought = value;
				NotifyPropertyChanged("Thought");
			}
		}	private string _Thought;
		public string ClosingPrayer
		{
			get { return _ClosingPrayer; }
			set
			{
				_ClosingPrayer = value;
				NotifyPropertyChanged("ClosingPrayer");
			}
		}	private string _ClosingPrayer;
		public int Offset
		{
			get { return _Offset; }
			set
			{
				_Offset = value;
				NotifyPropertyChanged("Offset");
			}
		}	private int _Offset;
		public DateTime UpdatedDate
		{
			get { return _UpdatedDate; }
			set
			{
				_UpdatedDate = value;
				NotifyPropertyChanged("UpdatedDate");
			}
		}	private DateTime _UpdatedDate;
		

		public Member Bishop
		{
			get { return _Bishop; }
			set
			{
				_Bishop = value;
				NotifyPropertyChanged("Bishop");
			}
		}	private Member _Bishop;
		public Member FirstCounselor
		{
			get { return _FirstCounselor; }
			set
			{
				_FirstCounselor = value;
				NotifyPropertyChanged("FirstCounselor");
			}
		}	private Member _FirstCounselor;
		public Member SecondCounselor
		{
			get { return _SecondCounselor; }
			set
			{
				_SecondCounselor = value;
				NotifyPropertyChanged("SecondCounselor");
			}
		}	private Member _SecondCounselor;
		public Member ExecutiveSecretary
		{
			get { return _ExecutiveSecretary; }
			set
			{
				_ExecutiveSecretary = value;
				NotifyPropertyChanged("ExecutiveSecretary");
			}
		}	private Member _ExecutiveSecretary;
		public Member AssistantExecutiveSecretary
		{
			get { return _AssistantExecutiveSecretary; }
			set
			{
				_AssistantExecutiveSecretary = value;
				NotifyPropertyChanged("AssistantExecutiveSecretary");
			}
		}	private Member _AssistantExecutiveSecretary;
		public Member AssistantExecutiveSecretary2
		{
			get { return _AssistantExecutiveSecretary2; }
			set
			{
				_AssistantExecutiveSecretary2 = value;
				NotifyPropertyChanged("AssistantExecutiveSecretary2");
			}
		}	private Member _AssistantExecutiveSecretary2;
		public Member WardClerk
		{
			get { return _WardClerk; }
			set
			{
				_WardClerk = value;
				NotifyPropertyChanged("WardClerk");
			}
		}	private Member _WardClerk;
		public Member SEldersQuorum
		{
			get { return _SEldersQuorum; }
			set
			{
				_SEldersQuorum = value;
				NotifyPropertyChanged("SEldersQuorum");
			}
		}	private Member _SEldersQuorum;
		public Member NEldersQuorum
		{
			get { return _NEldersQuorum; }
			set
			{
				_NEldersQuorum = value;
				NotifyPropertyChanged("NEldersQuorum");
			}
		}	private Member _NEldersQuorum;
		public Member WardMissionLeader
		{
			get { return _WardMissionLeader; }
			set
			{
				_WardMissionLeader = value;
				NotifyPropertyChanged("WardMissionLeader");
			}
		}	private Member _WardMissionLeader;
		public Member SundaySchoolPresident
		{
			get { return _SundaySchoolPresident; }
			set
			{
				_SundaySchoolPresident = value;
				NotifyPropertyChanged("SundaySchoolPresident");
			}
		}	private Member _SundaySchoolPresident;
		public Member ReliefSocietyPresident
		{
			get { return _ReliefSocietyPresident; }
			set
			{
				_ReliefSocietyPresident = value;
				NotifyPropertyChanged("ReliefSocietyPresident");
			}
		}	private Member _ReliefSocietyPresident;
		public Member PrimaryPresident
		{
			get { return _PrimaryPresident; }
			set
			{
				_PrimaryPresident = value;
				NotifyPropertyChanged("PrimaryPresident");
			}
		}	private Member _PrimaryPresident;

		#endregion

		#region Delegate Commands

		public DelegateCommand SaveSettingsCommand
		{
			get
			{
				if (_SaveSettingsCommand == null)
				{
					_SaveSettingsCommand = new DelegateCommand(this.OnSaveSettingsCommandExecute, this.OnSaveSettingsCommandCanExecute);
				}
				return _SaveSettingsCommand;
			}
		} private DelegateCommand _SaveSettingsCommand;
		private void OnSaveSettingsCommandExecute(object parameter)
		{
			var members = new Dictionary<string, Member>();
			members.Add("Bishop", Bishop);
			members.Add("FirstCounselor", FirstCounselor);
			members.Add("SecondCounselor", SecondCounselor);
			members.Add("ExecutiveSecretary", ExecutiveSecretary);
			members.Add("AssistantExecutiveSecretary", AssistantExecutiveSecretary);
			members.Add("AssistantExecutiveSecretary2", AssistantExecutiveSecretary2);
			members.Add("WardClerk", WardClerk);
			members.Add("SEldersQuorum", SEldersQuorum);
			members.Add("NEldersQuorum", NEldersQuorum);
			members.Add("WardMissionLeader", WardMissionLeader);
			members.Add("SundaySchoolPresident", SundaySchoolPresident);
			members.Add("ReliefSocietyPresident", ReliefSocietyPresident);
			members.Add("PrimaryPresident", PrimaryPresident);
			_agendaServiceAgent.SaveMembers(members);
		}
		private bool OnSaveSettingsCommandCanExecute(object parameter)
		{
			return true;
		}

		public DelegateCommand DecreaseOffsetCommand
		{
			get
			{
				if (_DecreaseOffsetCommand == null)
				{
					_DecreaseOffsetCommand = new DelegateCommand(this.OnDecreaseOffsetCommandExecute, this.OnDecreaseOffsetCommandCanExecute);
				}
				return _DecreaseOffsetCommand;
			}
		} private DelegateCommand _DecreaseOffsetCommand;
		private void OnDecreaseOffsetCommandExecute(object parameter)
		{
			Offset--;
			SetUpdateDate();
			InitializeAssignments();
		}
		private bool OnDecreaseOffsetCommandCanExecute(object parameter)
		{
			return Offset != 0;
		}

		public DelegateCommand IncreaseOffsetCommand
		{
			get
			{
				if (_IncreaseOffsetCommand == null)
				{
					_IncreaseOffsetCommand = new DelegateCommand(this.OnIncreaseOffsetCommandExecute, this.OnIncreaseOffsetCommandCanExecute);
				}
				return _IncreaseOffsetCommand;
			}
		} private DelegateCommand _IncreaseOffsetCommand;
		private void OnIncreaseOffsetCommandExecute(object parameter)
		{
			Offset++;
			SetUpdateDate();
			InitializeAssignments();
		}
		private bool OnIncreaseOffsetCommandCanExecute(object parameter)
		{
			return true;
		}

		public DelegateCommand ClearOffsetCommand
		{
			get
			{
				if (_ClearOffsetCommand == null)
				{
					_ClearOffsetCommand = new DelegateCommand(this.OnClearOffsetCommandExecute, this.OnClearOffsetCommandCanExecute);
				}
				return _ClearOffsetCommand;
			}
		} private DelegateCommand _ClearOffsetCommand;
		private void OnClearOffsetCommandExecute(object parameter)
		{
			Offset = 0;
			SetUpdateDate();
			InitializeAssignments();
		}
		private bool OnClearOffsetCommandCanExecute(object parameter)
		{
			return true;
		}

		public DelegateCommand SendMessageCommand
		{
			get
			{
				if (_SendMessageCommand == null)
				{
					_SendMessageCommand = new DelegateCommand(this.OnSendMessageCommandExecute, this.OnSendMessageCommandCanExecute);
				}
				return _SendMessageCommand;
			}
		} private DelegateCommand _SendMessageCommand;
		private void OnSendMessageCommandExecute(object parameter)
		{
			MessageResponse bishopMessageResponse = null;
			if (MessageBox.Show("Would you like to send this agenda to the Bishopric?", "Send Agenda", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
			{
				bishopMessageResponse = _emailServiceAgent.SendMessageToBishop(_meeting, OpeningPrayer, Thought, ClosingPrayer, new[] { Bishop, FirstCounselor, SecondCounselor });
			}
			var response = _emailServiceAgent.SendMessage(_meeting, _wardCouncilMembers.Where(m => m.Value.Name == Thought).FirstOrDefault().Value.Email, _bishopricMembers["Bishop"], _bishopricMembers["ExecutiveSecretary"]);
			//string message = null;
			string message = response.Success ? "Assignment successfully delivered" : "Encountered an error when delivering the assignment message";
			message += (bishopMessageResponse != null && bishopMessageResponse.Success) ? "\nAgenda successfully delivered" : (bishopMessageResponse != null) ? "\nEncountered an error when delivering the agenda" : "";
			MessageBox.Show(message, "Message Send Status", MessageBoxButton.OK);
		}
		private bool OnSendMessageCommandCanExecute(object parameter)
		{
			return Thought != null;
		}
		
		#endregion

		public MainWindowViewModel()
		{
			Initialize();
		}

		#region Methods

		private void Initialize()
		{
			_agendaServiceAgent = new AgendaServiceAgent();
			_emailServiceAgent = new EmailServiceAgent();
			Offset = Properties.Settings.Default.Offset;
			UpdatedDate = Properties.Settings.Default.Update;
			_meeting = "Bishopric";
			
			var members = _agendaServiceAgent.GetMembers();

			_bishopricMembers = InitalizeBishopric(members);
			_pecMembers = InitializePec(members);
			_wardCouncilMembers = InitalizeWardCouncil(members);
			InitalizeMembers(members);

			InitializeAssignments();
		}

		private void InitializeAssignments()
		{
			if (CurrentTabIndex == 0)
			{
				OpeningPrayer = _bishopricMembers.ToList()[(0 + Offset) % _bishopricMembers.Count].Value.Name;
				Thought = _bishopricMembers.ToList()[(2 + Offset) % _bishopricMembers.Count].Value.Name;
				ClosingPrayer = _bishopricMembers.ToList()[(4 + Offset) % _bishopricMembers.Count].Value.Name;
			}
			else if (CurrentTabIndex == 1)
			{
				OpeningPrayer = _pecMembers.ToList()[(3 + Offset) % _pecMembers.Count].Value.Name;
				Thought = _pecMembers.ToList()[(6 + Offset) % _pecMembers.Count].Value.Name;
				ClosingPrayer = _pecMembers.ToList()[(9 + Offset) % _pecMembers.Count].Value.Name;
			}
			else if (CurrentTabIndex == 2)
			{
				OpeningPrayer = _wardCouncilMembers.ToList()[(1 + Offset) % _wardCouncilMembers.Count].Value.Name;
				Thought = _wardCouncilMembers.ToList()[(5 + Offset) % _wardCouncilMembers.Count].Value.Name;
				ClosingPrayer = _wardCouncilMembers.ToList()[(8 + Offset) % _wardCouncilMembers.Count].Value.Name;
			}
		}

		private Dictionary<string, Member> InitalizeWardCouncil(Dictionary<string, Member> members)
		{
			var wardCouncilMembers = InitializePec(members);
			wardCouncilMembers.Add("SundaySchoolPresident", members["SundaySchoolPresident"]);
			wardCouncilMembers.Add("ReliefSocietyPresident", members["ReliefSocietyPresident"]);
			wardCouncilMembers.Add("PrimaryPresident", members["PrimaryPresident"]);
			return wardCouncilMembers;
		}

		private Dictionary<string, Member> InitializePec(Dictionary<string, Member> members)
		{
			var pecMembers = InitalizeBishopric(members);
			pecMembers.Add("SEldersQuorum", members["SEldersQuorum"]);
			pecMembers.Add("NEldersQuorum", members["NEldersQuorum"]);
			pecMembers.Add("WardMissionLeader", members["WardMissionLeader"]);
			return pecMembers;
		}

		private Dictionary<string, Member> InitalizeBishopric(Dictionary<string, Member> members)
		{
			var bishopricMembers = new Dictionary<string, Member>();
			bishopricMembers.Add("Bishop", members["Bishop"]);
			bishopricMembers.Add("FirstCounselor", members["FirstCounselor"]);
			bishopricMembers.Add("SecondCounselor", members["SecondCounselor"]);
			bishopricMembers.Add("ExecutiveSecretary", members["ExecutiveSecretary"]);
			bishopricMembers.Add("AssistantExecutiveSecretary", members["AssistantExecutiveSecretary"]);
			bishopricMembers.Add("AssistantExecutiveSecretary2", members["AssistantExecutiveSecretary2"]);
			bishopricMembers.Add("WardClerk", members["WardClerk"]);
			return bishopricMembers;
		}

		private void InitalizeMembers(Dictionary<string, Member> members)
		{
			Bishop = members["Bishop"];
			FirstCounselor = members["FirstCounselor"];
			SecondCounselor = members["SecondCounselor"];
			ExecutiveSecretary = members["ExecutiveSecretary"];
			AssistantExecutiveSecretary = members["AssistantExecutiveSecretary"];
			AssistantExecutiveSecretary2 = members["AssistantExecutiveSecretary2"];
			WardClerk = members["WardClerk"];
			SEldersQuorum = members["SEldersQuorum"];
			NEldersQuorum = members["NEldersQuorum"];
			WardMissionLeader = members["WardMissionLeader"];
			SundaySchoolPresident = members["SundaySchoolPresident"];
			ReliefSocietyPresident = members["ReliefSocietyPresident"];
			PrimaryPresident = members["PrimaryPresident"];
		}

		private void SetUpdateDate()
		{
			UpdatedDate = DateTime.Now;
		}

		#endregion

		#region INotifyPropertyChanged Implementation
		public event PropertyChangedEventHandler PropertyChanged;

		private void NotifyPropertyChanged(string propertyName)
		{
			var handler = PropertyChanged;
			if (handler != null)
			{
				handler(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		#endregion

	}

	#region Objects

	public class Member
	{
		public string Name { get; set; }
		public string Email { get; set; }
	}

	#endregion
}
