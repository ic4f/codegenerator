using System;

namespace CodeGenerator.Application
{
    public class ReadonlyFieldTypeFactory
    {
        public static ReadonlyFieldType GetReadonlyTypeClassType(string type)
        {
            switch (type)
            {
                case "created":
                    return ReadonlyFieldType.Created;
                case "modified":
                    return ReadonlyFieldType.Modified;
                case "timestamp":
                    return ReadonlyFieldType.Timestamp;
                case "":
                    return ReadonlyFieldType.Undefined;
                case null:
                    return ReadonlyFieldType.Undefined;
                default:
                    throw new UnknownReadonlyFieldTypeException("Unknown classtype: " + type);
            }
        }

        private ReadonlyFieldTypeFactory() { }
    }
}

