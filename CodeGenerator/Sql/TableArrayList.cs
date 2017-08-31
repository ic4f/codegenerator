using System;
using System.Collections;

namespace CodeGenerator.Sql
{
	public class TableArrayList : ArrayList
	{
		public new Table this[int index]
		{
			get
			{
				if (index < 0)				
					return null;				
				else				
					return (Table)base[index];				
			}
			set
			{		
				if (index >= 0)	
					base[index] = value;				
			}
		}

		public override int Add(object value)
		{
			return base.Add ((Table)value);
		}
	}
}
