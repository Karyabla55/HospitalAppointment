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
			if (age < 5)
			{
				return 20;
			}
			else if ( age < 45)
			{
				return 0;
			}
			else if (age < 65)
			{
				return 15;
			}
			else { return 25; }
		}

		protected static int CalculateDisabiltyPoint(int disabiltyRate)
		{
			return disabiltyRate / 4;
		}

		protected static int CalculateBleedPoint(string bleedingStatus)
		{
			switch (bleedingStatus)
			{
				case "kanamaYok ":
					return 0;
				case "kanama    ":
					return 20;
				case "agirKanama":
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
