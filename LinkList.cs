using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppointment
{
	public class LinkList<T>
	{
		public Node<T> root;
		public int size;

		public LinkList()
		{
			this.root = null;
			size = 0;
		}

		public void addToLast(T Data)
		{
			Node<T> newNode = new Node<T>(Data);
			if (root == null)
			{
				root = newNode;
			}
			else
			{
				Node<T> ither = root;
				while (ither.next != null)
				{
					ither = ither.next;
				}

				ither.next = newNode;
			}
			size++;
		}
		public void ExtractToHead()
		{
			if (root == null )
			{
				return;
			}
			else
			{
				root = root.next;
				size--;
			}
			
		}

		public IEnumerator<T> GetEnumerator()
		{
			Node<T> current = root;
			while (current != null)
			{
				yield return current.Data;
				current = current.next;
			}
		}
	}


}
