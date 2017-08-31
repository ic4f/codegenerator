using System;

namespace CodeGenerator.Application
{
	public class ClassTypeFactory
	{
		public static ClassType GetClassType(string type)
		{
			switch(type)
			{
				case "record" :
					return ClassType.Record;
				case "link" :
					return ClassType.Link;
				case "readonly" :
					return ClassType.Readonly;
				case "final" :
					return ClassType.Final;
				default :
					throw new UnknownClassTypeException("Unknown classtype: " + type);
			}				 
		}

		private ClassTypeFactory() {}
	}
}
