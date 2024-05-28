using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static HospitalAppointment.TreeList;

namespace HospitalAppointment
{
	class Appointment
	{
		public LinkList<Patient> AllPatients = new LinkList<Patient>();
		public TreeList treeList = new TreeList();
		public bool RoomIsFull = false;
		public DateTime StartTime = new DateTime(2024, 5,28,9,0,0);

		public void Start()
		{
			Patient.OrganizePatients(AllPatients);

		}
		public void GetRegister(DateTime RegisterTime)
		{
			if ( AllPatients.root != null  )
			{
				if(RegisterTime >= AllPatients.root.Data.RegisterTime)
				{
					AllPatients.root.Data.PriorityPoint = PatientStatus.CalculatePriortyPoint(AllPatients.root.Data);
					AllPatients.root.Data.InspectionDuration = PatientInspection.CalculateInspectionDuration(AllPatients.root.Data);
					treeList.AddElement(AllPatients.root.Data);
					AllPatients.ExtractToHead();
					PrintList();
				}
				
			}
        }
		public void SendInspection(DateTime Time)
		{
			Patient next = NextPatient();
			next.InspectionTime = Time;
			RoomIsFull = true;
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
			Console.Write(next.toString()+" ");
			Console.WriteLine("Muayneye hasta gönderildi");
			return next;
		}
		public Patient Next()
		{
			return treeList.root.Data;
		}

	}
}
