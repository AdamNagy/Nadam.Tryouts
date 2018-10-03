using System;
using System.Collections.Generic;
using System.Text;

namespace Shapes
{
	public abstract class Shape : IDrawable
	{
		Color color;
		PointF location;

		public Color Color
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public PointF Location
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		protected Shape()
		{
			throw new NotImplementedException();
		}

		public abstract void Draw(Graphics g);
	}
}
