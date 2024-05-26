using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppointment
{
	class Appointment
	{
		public LinkList<Patient> AllPatients = new LinkList<Patient>();
		public TreeList treeList = new TreeList();
		public TimeSpan StartTime = new TimeSpan(9,0,0);
		public TimeSpan EndTime = new TimeSpan(17, 0, 0);

		public void Start()
		{
			Patient.OrganizePatients(AllPatients);
			/*
			treeList.AddElement(AllPatients.root.Data);
			AllPatients.ExtractToHead();*/
		}
		public void GetRegister(TimeSpan RegisterTime)
		{
			if (RegisterTime == AllPatients.root.Data.RegisterTime)
			{
				AllPatients.root.Data.PriorityPoint = PatientStatus.CalculatePriortyPoint(AllPatients.root.Data);
				AllPatients.root.Data.InspectionDuration = PatientStatus.CalculatePriortyPoint(AllPatients.root.Data);
				treeList.AddElement(AllPatients.root.Data);
				AllPatients.ExtractToHead();
				PrintList();
			}

		}
		public void SendInspection(TimeSpan Time)
		{
			if(Time > StartTime && Time< EndTime) 
			{
				Patient next = NextPatient();
				next.InspectionTime = Time;
				TimeSpan inspection = new TimeSpan(0,0,next.InspectionDuration);
				if(next.InspectionTime + inspection == Time)
				{
					return;
				}
			}
		}

		public void PrintList()
		{
			foreach (var Tnode in treeList)
			{
				Console.WriteLine(Tnode.Data.toString());
			}
			Console.WriteLine("Yeni hasta sisteme eklendi");
		}
		public Patient NextPatient()
		{
			Patient next = treeList.ExtractElement();
			PrintList();
			return next;
		}

	}
}
