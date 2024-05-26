using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalAppointment
{
	public class TreeList
	{

		public class TNode
		{
			public Patient Data;
			public TNode RightC;
			public TNode LeftC;

			public TNode(Patient data)
			{
				Data = data;
				RightC = null;
				LeftC = null;
			}
		}
		public TNode root;
		public TreeList() { root = null; }

		public void AddElement(Patient data)
		{

			TNode newNode = new TNode(data);
			if (root == null)
			{
				root = newNode;
			}
			else
			{
				LinkList<TNode> listFillTree = new LinkList<TNode>();
				listFillTree.addToLast(root);


				while (listFillTree.size > 0)
				{
					TNode current = listFillTree.root.Data;
					listFillTree.ExtractToHead();

					if (current.LeftC == null)
					{
						current.LeftC = newNode;
						HeapifyUp(current.LeftC);
						return;
					}
					else if (current.RightC == null)
					{
						current.RightC = newNode;
						HeapifyUp(current.RightC);
						return;
					}
					else
					{
						listFillTree.addToLast(current.LeftC);
						listFillTree.addToLast(current.RightC);

					}

				}
			}



		}
		private void HeapifyUp(TNode node)
		{
			TNode current = node;
			while (current != root)
			{
				TNode parent = GetParent(current); // Ebeveyn düğümü alır
				if (parent != null && current.Data.PriorityPoint > parent.Data.PriorityPoint)
				{
					Swap(current, parent);
					current = parent; // Ebeveyn düğümün konumuna güncellenir
				}
				else
				{
					break; // Max heap özellikleri sağlandığı için döngüden çıkılır
				}
			}
		}
		public Patient ExtractElement()
		{
			if (root == null)
			{
				return null;
			}
			Patient next = root.Data;
			HeapifyDown(root);
			return next;
		}
		private void HeapifyDown(TNode root)
		{
			TNode current = root;
			while (current.LeftC != null)
			{
				TNode largerChild = current.LeftC;
				if (current.RightC != null)
				{
					if (current.LeftC.Data.PriorityPoint > current.RightC.Data.PriorityPoint)
					{
						Swap(largerChild, current);
						current = largerChild;
					}
					else if (current.RightC.Data.PriorityPoint >= current.LeftC.Data.PriorityPoint)
					{
						largerChild = current.RightC;
						Swap(largerChild, current);
						current = largerChild;
					}
				}
				else
				{
					Swap(largerChild, current);
					current = largerChild;
				}
			}
			if (current.LeftC == null && current.RightC == null)
			{
				current = null;
			}
		}

		private TNode GetParent(TNode node)
		{
			LinkList<TNode> listFindParent = new LinkList<TNode>();
			listFindParent.addToLast(root);

			while (listFindParent.size > 0)
			{
				TNode current = listFindParent.root.Data;
				listFindParent.ExtractToHead();

				if (current.LeftC == node)
				{
					return current;
				}
				else if (current.RightC == node)
				{
					return current;
				}
				else
				{
					listFindParent.addToLast(current.LeftC);
					listFindParent.addToLast(current.RightC);
				}
			}
			return null;
		}

		private void Swap(TNode child, TNode parent)
		{
			Patient temp = child.Data;
			child.Data = parent.Data;
			parent.Data = temp;
		}



		public IEnumerator<TNode> GetEnumerator()
		{
			if (root == null)
				yield break;

			Queue<TNode> queue = new Queue<TNode>();
			queue.Enqueue(root);

			while (queue.Count > 0)
			{
				TNode current = queue.Dequeue();
				yield return current;

				if (current.LeftC != null)
					queue.Enqueue(current.LeftC);

				if (current.RightC != null)
					queue.Enqueue(current.RightC);
			}
		}
	}

}
