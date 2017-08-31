using System;

namespace CodeGenerator.Application
{
	public enum ClassType : int
	{
		Record = 0,		//standard
		Link = 1,		//like UserRoles (contains only 2 fields?
		Readonly = 2,	//no create/update/delete (built-in tables like permissions, or external tables/views)
		Final = 3		//no updates (like logs or emails)
	}
}
