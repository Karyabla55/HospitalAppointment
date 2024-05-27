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

			Patient extractedData = root.Data;
			TNode lastNode = GetLastNode();
			if (lastNode != root)
			{
				root.Data = lastNode.Data;

				TNode parent = GetParent(lastNode);
				if (parent.LeftC == lastNode)
				{
					parent.LeftC = null;
				}
				else if (parent.RightC == lastNode)
				{
					parent.RightC = null;
				}

				HeapifyDown(root);
			}
			else
			{
				root = null;
			}

			return extractedData;
		}


		private void HeapifyDown(TNode node)
		{
			TNode largest = node;
			while (true)
			{
				TNode left = node.LeftC;
				TNode right = node.RightC;

				if (left != null && left.Data.PriorityPoint > largest.Data.PriorityPoint)
				{
					largest = left;
				}

				if (right != null && right.Data.PriorityPoint > largest.Data.PriorityPoint)
				{
					largest = right;
				}

				if (largest != node)
				{
					Swap(node, largest);
					node = largest;
				}
				else
				{
					break;
				}
			}
		}


		private TNode GetLastNode()
		{
			LinkList<TNode> ListLastNode = new LinkList<TNode>();
			ListLastNode.addToLast(root);
			TNode lastNode = null;

			while (ListLastNode.size > 0)
			{
				lastNode = ListLastNode.root.Data;
				ListLastNode.ExtractToHead();

				if (lastNode.LeftC != null)
				{
					ListLastNode.addToLast(lastNode.LeftC);
				}

				if (lastNode.RightC != null)
				{
					ListLastNode.addToLast(lastNode.RightC);
				}
			}

			return lastNode;
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
