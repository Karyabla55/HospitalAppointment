using System;
using System.Drawing;
using System.Windows.Forms;

namespace HospitalAppointment
{
	public partial class frmAppointment : Form
	{
		Appointment system;
		private System.Windows.Forms.Timer timer;
		private DateTime simulatedTime;
		Patient SendedPatient;

		public frmAppointment()
		{
			InitializeComponent();
			system = new Appointment();
			system.Start();

			simulatedTime = new DateTime(2024, 5, 28, 8, 0, 0);
			// Timer'ı oluştur ve ayarla
			timer = new System.Windows.Forms.Timer();
			timer.Interval = 1000; // 1000 milisaniye (1 saniye)
			timer.Tick += Timer_Tick; // Tick olayına işleyici ekle
			timer.Start(); // Timer'ı başlat
		}
		private void Timer_Tick(object sender, EventArgs e)
		{

			simulatedTime = simulatedTime.AddSeconds(120);
			system.GetRegister(simulatedTime);
			if (simulatedTime >= system.StartTime && system.treeList != null)
			{
				if (system.treeList.root != null && !system.RoomIsFull)
				{
					SendedPatient = system.Next();
					system.SendInspection(simulatedTime);
				}
				else if (AddTime(SendedPatient.InspectionTime,SendedPatient.InspectionDuration) <= simulatedTime)
				{
					system.RoomIsFull = false;
				}
				lblPatient.Text = "Hasta: " + SendedPatient.toString();
			}

			lblTime.Text = simulatedTime.ToString("HH:mm:ss");
			Invalidate();
		}

		public DateTime AddTime(DateTime FirstTime, DateTime SecondTime)
		{
			// İki DateTime nesnesinin saat ve dakika bilgilerini al
			TimeSpan firstTimeSpan = FirstTime.TimeOfDay;
			TimeSpan secondTimeSpan = SecondTime.TimeOfDay;

			// İki TimeSpan'i topla
			TimeSpan resultTimeSpan = firstTimeSpan + secondTimeSpan;

			// Toplam sonucunu yeni bir DateTime nesnesi olarak geri döndür
			DateTime resultDateTime = DateTime.Today.Add(resultTimeSpan);

			return resultDateTime;
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (system.treeList.root != null)
			{
				DrawNode(e.Graphics, system.treeList.root, this.ClientSize.Width / 2, 50, this.ClientSize.Width / 4);
			}
		}

		private void DrawNode(Graphics g, TreeList.TNode node, int x, int y, int xOffset)
		{
			if (node == null)
				return;

			// Düğümü çiz
			int rectWidth = 150;
			int rectHeight = 100;
			Rectangle rect = new Rectangle(x - rectWidth / 2, y - rectHeight / 2, rectWidth, rectHeight);
			g.FillRectangle(Brushes.LightBlue, rect);
			g.DrawRectangle(Pens.Black, rect);

			// Düğüm bilgilerini yaz
			string nodeInfo = $"Kayıt Sırası: {node.Data.PatientNo}\nİsim:{node.Data.PatientName}\nYaş: {node.Data.PatientAge}\nÖncelik Puanı: {node.Data.PriorityPoint}";
			using (Font font = new Font("Arial", 12))
			{
				g.DrawString(nodeInfo, font, Brushes.Black, rect);
			}

			// Sol çocuk düğümü çiz ve bağlantıyı çiz
			if (node.LeftC != null)
			{
				g.DrawLine(Pens.Black, x - 75, y + 20, x - xOffset, y + 50);
				DrawNode(g, node.LeftC, x - xOffset, y + 50, xOffset / 2);
			}

			// Sağ çocuk düğümü çiz ve bağlantıyı çiz
			if (node.RightC != null)
			{
				g.DrawLine(Pens.Black, x + 75, y + 20, x + xOffset, y + 50);
				DrawNode(g, node.RightC, x + xOffset, y + 50, xOffset / 2);
			}
		}


	}
}
