using System.Collections;

namespace WEZD.HtmlAgilityPack.HtmlAgilityPack
{
	public class HtmlAttributeCollection : IList<HtmlAttribute>, ICollection<HtmlAttribute>, IEnumerable<HtmlAttribute>, IEnumerable
	{
		internal Dictionary<string, HtmlAttribute> Hashitems = new Dictionary<string, HtmlAttribute>(StringComparer.OrdinalIgnoreCase);

		private HtmlNode _ownernode;

		internal List<HtmlAttribute> items = new List<HtmlAttribute>();

		public int Count => items.Count;

		public bool IsReadOnly => false;

		public HtmlAttribute this[int index]
		{
			get => items[index];
            set
			{
				HtmlAttribute htmlAttribute = items[index];
				items[index] = value;
				if (htmlAttribute.Name != value.Name)
				{
					Hashitems.Remove(htmlAttribute.Name);
				}
				Hashitems[value.Name] = value;
				value._ownernode = _ownernode;
				_ownernode.SetChanged();
			}
		}

		public HtmlAttribute this[string name]
		{
			get
			{
				if (name == null)
				{
					throw new ArgumentNullException("name");
				}
				if (!Hashitems.TryGetValue(name, out var value))
				{
					return null;
				}
				return value;
			}
			set
			{
				if (!Hashitems.TryGetValue(name, out var value2))
				{
					Append(value);
				}
				else
				{
					this[items.IndexOf(value2)] = value;
				}
			}
		}

		internal HtmlAttributeCollection(HtmlNode ownernode)
		{
			_ownernode = ownernode;
		}

		public void Add(string name, string value)
		{
			Append(name, value);
		}

		public void Add(HtmlAttribute item)
		{
			Append(item);
		}

		public void AddRange(IEnumerable<HtmlAttribute> items)
		{
			foreach (HtmlAttribute item in items)
			{
				Append(item);
			}
		}

		public void AddRange(Dictionary<string, string> items)
		{
			foreach (KeyValuePair<string, string> item in items)
			{
				Add(item.Key, item.Value);
			}
		}

		void ICollection<HtmlAttribute>.Clear()
		{
			Clear();
		}

		public bool Contains(HtmlAttribute item)
		{
			return items.Contains(item);
		}

		public void CopyTo(HtmlAttribute[] array, int arrayIndex)
		{
			items.CopyTo(array, arrayIndex);
		}

		IEnumerator<HtmlAttribute> IEnumerable<HtmlAttribute>.GetEnumerator()
		{
			return items.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return items.GetEnumerator();
		}

		public int IndexOf(HtmlAttribute item)
		{
			return items.IndexOf(item);
		}

		public void Insert(int index, HtmlAttribute item)
		{
			if (item == null)
			{
				throw new ArgumentNullException("item");
			}
			Hashitems[item.Name] = item;
			item._ownernode = _ownernode;
			items.Insert(index, item);
			_ownernode.SetChanged();
		}

		bool ICollection<HtmlAttribute>.Remove(HtmlAttribute item)
		{
			if (item == null)
			{
				return false;
			}
			int attributeIndex = GetAttributeIndex(item);
			if (attributeIndex == -1)
			{
				return false;
			}
			RemoveAt(attributeIndex);
			return true;
		}

		public void RemoveAt(int index)
		{
			HtmlAttribute htmlAttribute = items[index];
			Hashitems.Remove(htmlAttribute.Name);
			items.RemoveAt(index);
			_ownernode.SetChanged();
		}

		public HtmlAttribute Append(HtmlAttribute newAttribute)
		{
			if (_ownernode.NodeType == HtmlNodeType.Text || _ownernode.NodeType == HtmlNodeType.Comment)
			{
				throw new Exception("A Text or Comment node cannot have attributes.");
			}
			if (newAttribute == null)
			{
				throw new ArgumentNullException("newAttribute");
			}
			Hashitems[newAttribute.Name] = newAttribute;
			newAttribute._ownernode = _ownernode;
			items.Add(newAttribute);
			_ownernode.SetChanged();
			return newAttribute;
		}

		public HtmlAttribute Append(string name)
		{
			HtmlAttribute newAttribute = _ownernode._ownerdocument.CreateAttribute(name);
			return Append(newAttribute);
		}

		public HtmlAttribute Append(string name, string value)
		{
			HtmlAttribute newAttribute = _ownernode._ownerdocument.CreateAttribute(name, value);
			return Append(newAttribute);
		}

		public bool Contains(string name)
		{
			for (int i = 0; i < items.Count; i++)
			{
				if (string.Equals(items[i].Name, name, StringComparison.OrdinalIgnoreCase))
				{
					return true;
				}
			}
			return false;
		}

		public HtmlAttribute Prepend(HtmlAttribute newAttribute)
		{
			Insert(0, newAttribute);
			return newAttribute;
		}

		public void Remove(HtmlAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			int attributeIndex = GetAttributeIndex(attribute);
			if (attributeIndex == -1)
			{
				throw new IndexOutOfRangeException();
			}
			RemoveAt(attributeIndex);
		}

		public void Remove(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			for (int num = items.Count - 1; num >= 0; num--)
			{
				if (string.Equals(items[num].Name, name, StringComparison.OrdinalIgnoreCase))
				{
					RemoveAt(num);
				}
			}
		}

		public void RemoveAll()
		{
			Hashitems.Clear();
			items.Clear();
			_ownernode.SetChanged();
		}

		public IEnumerable<HtmlAttribute> AttributesWithName(string attributeName)
		{
			for (int i = 0; i < items.Count; i++)
			{
				if (string.Equals(items[i].Name, attributeName, StringComparison.OrdinalIgnoreCase))
				{
					yield return items[i];
				}
			}
		}

		public void Remove()
		{
			items.Clear();
		}

		internal void Clear()
		{
			Hashitems.Clear();
			items.Clear();
		}

		internal int GetAttributeIndex(HtmlAttribute attribute)
		{
			if (attribute == null)
			{
				throw new ArgumentNullException("attribute");
			}
			for (int i = 0; i < items.Count; i++)
			{
				if (items[i] == attribute)
				{
					return i;
				}
			}
			return -1;
		}

		internal int GetAttributeIndex(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			for (int i = 0; i < items.Count; i++)
			{
				if (string.Equals(items[i].Name, name, StringComparison.OrdinalIgnoreCase))
				{
					return i;
				}
			}
			return -1;
		}
	}
}
