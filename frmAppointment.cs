using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;
using Timer = System.Timers.Timer;

namespace HospitalAppointment
{
	public partial class frmAppointment : Form
	{
		Appointment system;

		public frmAppointment()
		{
			InitializeComponent();
			system = new Appointment();
		}
	}
}
