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

		public void Start()
		{
			Patient.OrganizePatients(AllPatients);
			foreach (var patient in AllPatients)
			{
				treeList.AddElement(patient);
			}
		}

		public void PrintList()
		{
			foreach (var Tnode in treeList)
			{
				Console.WriteLine(Tnode.Data.toString());
			}
		}
		public void Next()
		{
			Patient next = treeList.ExtractElement();
			Console.WriteLine(next.toString());
		}

	}
}
