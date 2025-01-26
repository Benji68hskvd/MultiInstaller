namespace WEZD.HtmlAgilityPack.HtmlAgilityPack
{
	public class HtmlCommentNode : HtmlNode
	{
		private string _comment;

		public string Comment
		{
			get
			{
				if (_comment == null)
				{
					return base.InnerHtml;
				}
				return _comment;
			}
			set => _comment = value;
        }

		public override string InnerHtml
		{
			get
			{
				if (_comment == null)
				{
					return base.InnerHtml;
				}
				return _comment;
			}
			set => _comment = value;
        }

		public override string OuterHtml
		{
			get
			{
				if (_comment == null)
				{
					return base.OuterHtml;
				}
				if (_comment.StartsWith("<!--") && _comment.EndsWith("-->"))
				{
					return _comment;
				}
				return "<!--" + _comment + "-->";
			}
		}

        public HtmlCommentNode(HtmlDocument ownerdocument, int index)
			: base(HtmlNodeType.Comment, ownerdocument, index)
		{
		}
	}
}
