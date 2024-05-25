﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppointment
{
	internal class PatientInspection
	{
		protected static int CalculateAgePoint(int age)
		{
			if (age < 65)
			{
				return 0;
			}
			else { return 15; }
		}
		protected static int CalculateDisabiltyPoint(int disabiltyRate)
		{
			return disabiltyRate / 5;
		}
		protected static int CalculateBleedPoint(string bleedingStatus)
		{
			switch (bleedingStatus)
			{
				case "kanamaYok":
					return 0;
				case "kanama":
					return 10;
				case "ağırKanama":
					return 20;
				default:
					return 0;

			}
		}

		public static int CalculateInspectionDuration(Patient patient)
		{
			int AgePoint = CalculateAgePoint(patient.PatientAge);
			int DisabiltyPoint = CalculateDisabiltyPoint(patient.DisabilityPoint);
			int BleedConditon = CalculateBleedPoint(patient.BleedCondition);

			return AgePoint + DisabiltyPoint + BleedConditon;
		}
	}
}
