using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WardAgendas
{
	public class AgendaServiceAgent
	{
		Dictionary<string, Member> _members;
		string _file = @"D:\Program Files\AgendaAssignments\member-data.txt";

		public AgendaServiceAgent()
		{
			_members = new Dictionary<string, Member>();
			if (!File.Exists(_file))
			{
				File.Create(_file);
			}
		}

		public void SaveMembers(Dictionary<string, Member> members)
		{
			using (StreamWriter file = new StreamWriter(_file))
			{
				foreach(var member in members)
				{
					file.WriteLine(String.Format("{0}|{1}|{2}", member.Key, member.Value.Name, member.Value.Email));
				}
			}
		}

		public Dictionary<string, Member> GetMembers()
		{
			string[] lines = File.ReadAllLines(Path.GetFullPath(_file));
			foreach (var line in lines)
			{
				if (!String.IsNullOrWhiteSpace(line))
				{
					string[] keyValue = line.Split('|');
					_members.Add(keyValue[0], new Member() { Name = keyValue[1], Email = keyValue[2] });
				}
			}
			return _members;
		}
	}
}
