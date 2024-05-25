using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppointment
{
	public class PatientStatus
	{
		protected static int CalculateAgePoint(int age)
		{
			if (age < 18)
			{
				return 20;
			}
			else if (age >= 18 && age <= 50)
			{
				return 10;
			}
			else { return 5; }
		}

		protected static int CalculateDisabiltyPoint(int disabiltyRate)
		{
			if (disabiltyRate < 40) { return 10; }
			else if (disabiltyRate >= 40 && disabiltyRate <= 70) { return 30; }
			else { return 50; }
		}

		protected static int CalculateBleedPoint(string bleedingStatus)
		{
			switch (bleedingStatus)
			{
				case "kanamaYok":
					return 0;
				case "kanama":
					return 20;
				case "ağırKanama":
					return 50;
				default:
					return 0;
			}
		}
		protected static int IsPrisoner(bool isPrisoner)
		{
			return (isPrisoner ? 50 : 0);
		}

		public static int CalculatePriortyPoint(Patient patient)
		{
			int AgePoint = CalculateAgePoint(patient.PatientAge);
			int DisabiltyPoint = CalculateDisabiltyPoint(patient.DisabilityPoint);
			int BleedCondition = CalculateBleedPoint(patient.BleedCondition);
			int PrsionStatus = IsPrisoner(patient.IsPrisoner);

			return AgePoint + DisabiltyPoint + BleedCondition + PrsionStatus;
		}
	}

}
