using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppointment
{
	public class Patient
	{
		public int PatientNo;
		public string PatientName;
		public int PatientAge;
		public char[] Gender = new char[1];
		public bool IsPrisoner;
		public int DisabilityPoint;
		public string BleedCondition;
		public DateTime RegisterTime;
		public DateTime InspectionTime;
		public DateTime InspectionDuration;
		public int PriorityPoint;

		public Patient(string patientName, int patientAge, char[] gender, bool ısPrisoner, int disabiltyPoint, string bleedCondition, DateTime registerTime)
		{
			PatientName = patientName;
			PatientAge = patientAge;
			Gender = gender;
			IsPrisoner = ısPrisoner;
			DisabilityPoint = disabiltyPoint;
			BleedCondition = bleedCondition;
			RegisterTime = registerTime;
		}

		public static void OrganizePatients(LinkList<Patient> Patients)
		{
			string currentDirectory = Directory.GetCurrentDirectory();
			string dosyaYolu = Path.Combine(currentDirectory, "Hasta.txt");

			try
			{

				using (StreamReader sr = new StreamReader(dosyaYolu))
				{
					string line;

					while ((line = sr.ReadLine()) != null)
					{
						string[] PatientData = line.Split(',');
						string PatientName = PatientData[1];
						int PatiensAge = int.Parse(PatientData[2]);
						char[] Gender = { char.Parse(PatientData[3]) };
						bool IsPrisoner = bool.Parse(PatientData[4]);
						int Disabilty = int.Parse(PatientData[5]);
						string Bleeding = PatientData[6];
						DateTime RegisterTime = StringToTime(PatientData[7]);

						Patient patient = new Patient(PatientName, PatiensAge, Gender, IsPrisoner, Disabilty, Bleeding, RegisterTime);
						/*patient.PriorityPoint = PatientStatus.CalculatePriortyPoint(patient);
						patient.InspectionDuration = PatientInspection.CalculateInspectionDuration(patient);*/
						Patients.addToLast(patient);
					}
					SortForRegisterTime(Patients);

				}

				foreach (Patient patient in Patients)
				{
					Console.WriteLine(patient.toString());
				}

				Console.WriteLine("Hastalar Eklendi...");
			}
			catch (Exception e)
			{
				Console.WriteLine("Hata: " + e.Message);
			}
		}

		private static void SortForRegisterTime(LinkList<Patient> Patients)
		{
			bool swaped;
			do
			{
				swaped = false;
				Node<Patient> current = Patients.root;
				while (current != null && current.next != null)
				{
					if (current.Data.RegisterTime > current.next.Data.RegisterTime)
					{
						Patient temp = current.Data;
						current.Data = current.next.Data;
						current.next.Data = temp;
						swaped = true;
					}
					current = current.next;
				}
			} while (swaped);
			
			
			int i = 1;
			foreach (var patient in Patients)//Id verme
			{
				patient.PatientNo = i;
				i++;
			}
		}
		private static DateTime StringToTime(string str)
		{
			string[] time = str.Split('.');
			DateTime organizedTime = DateTime.Parse(time[0] + ":" + time[1]);
			return organizedTime;
		}
		
		public string toString()
		{
			return PatientNo + "," + PatientName + "," + PatientAge + "," +
				Gender[0] + "," + IsPrisoner + "," + DisabilityPoint + "," +
				BleedCondition + "," + RegisterTime.ToString()+","+ PriorityPoint + "," + InspectionDuration;
		}
	}
}
