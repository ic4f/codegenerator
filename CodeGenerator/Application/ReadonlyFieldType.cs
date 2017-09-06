using System;

namespace CodeGenerator.Application
{
    public enum ReadonlyFieldType : int
    {
        Created = 0,
        Modified = 1,
        Timestamp = 2,
        Undefined = -1
    }
}
