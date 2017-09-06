using System;

namespace CodeGenerator
{
    public class DuplicateNamespaceException : Exception
    {
        public DuplicateNamespaceException(string message) : base(message) { }
    }

    public class DuplicateClassException : Exception
    {
        public DuplicateClassException(string message) : base(message) { }
    }

    public class DuplicateTableException : Exception
    {
        public DuplicateTableException(string message) : base(message) { }
    }

    public class DuplicateFieldException : Exception
    {
        public DuplicateFieldException(string message) : base(message) { }
    }

    public class DuplicateAdditionalFieldException : Exception
    {
        public DuplicateAdditionalFieldException(string message) : base(message) { }
    }

    public class DuplicateFieldAdditionalFieldException : Exception
    {
        public DuplicateFieldAdditionalFieldException(string message) : base(message) { }
    }

    public class DuplicateSprocException : Exception
    {
        public DuplicateSprocException(string message) : base(message) { }
    }

    public class DuplicateParamException : Exception
    {
        public DuplicateParamException(string message) : base(message) { }
    }

    public class DuplicateReturnFieldException : Exception
    {
        public DuplicateReturnFieldException(string message) : base(message) { }
    }

    public class DuplicateCustomReturnFieldException : Exception
    {
        public DuplicateCustomReturnFieldException(string message) : base(message) { }
    }

    public class DuplicateReturnFieldCustomReturnFieldException : Exception
    {
        public DuplicateReturnFieldCustomReturnFieldException(string message) : base(message) { }
    }

    public class DuplicateConstraintException : Exception
    {
        public DuplicateConstraintException(string message) : base(message) { }
    }

    public class ConstraintFormatException : Exception
    {
        public ConstraintFormatException(string message) : base(message) { }
    }

    public class MultipleIdentityException : Exception
    {
        public MultipleIdentityException(string message) : base(message) { }
    }

    public class MultiplePrimaryKeysException : Exception
    {
        public MultiplePrimaryKeysException(string message) : base(message) { }
    }

    public class DuplicateDefaultSortFieldException : Exception
    {
        public DuplicateDefaultSortFieldException(string message) : base(message) { }
    }

    public class InvalidEncryptionFormatException : Exception
    {
        public InvalidEncryptionFormatException(string message) : base(message) { }
    }

    public class ExceededTableRowCapacityException : Exception
    {
        public ExceededTableRowCapacityException(string message) : base(message) { }
    }

    public class ForeignKeyInvalidRefTableException : Exception
    {
        public ForeignKeyInvalidRefTableException(string message) : base(message) { }
    }

    public class ForeignKeyInvalidRefFieldException : Exception
    {
        public ForeignKeyInvalidRefFieldException(string message) : base(message) { }
    }

    public class ForeignKeyInvalidRefFieldDatatypeException : Exception
    {
        public ForeignKeyInvalidRefFieldDatatypeException(string message) : base(message) { }
    }

    public class ForeignKeyRefFieldMissingConstraintException : Exception
    {
        public ForeignKeyRefFieldMissingConstraintException(string message) : base(message) { }
    }

    public class TableNotFoundException : Exception
    {
        public TableNotFoundException(string message) : base(message) { }
    }

    public class InvalidReturnFieldException : Exception
    {
        public InvalidReturnFieldException(string message) : base(message) { }
    }

    public class UnknownClassTypeException : Exception
    {
        public UnknownClassTypeException(string message) : base(message) { }
    }

    public class UnknownReadonlyFieldTypeException : Exception
    {
        public UnknownReadonlyFieldTypeException(string message) : base(message) { }
    }

    public class UnknownCsDatatypeException : Exception
    {
        public UnknownCsDatatypeException(string message) : base(message) { }
    }

    public class UnknownSqlDatatypeException : Exception
    {
        public UnknownSqlDatatypeException(string message) : base(message) { }
    }

    public class SqlDatatypeFormatException : Exception
    {
        public SqlDatatypeFormatException(string message) : base(message) { }
    }

    public class InvalidSprocReturnTypeException : Exception
    {
        public InvalidSprocReturnTypeException(string message) : base(message) { }
    }

    public class InvalidSprocFormatException : Exception
    {
        public InvalidSprocFormatException(string message) : base(message) { }
    }

    public class FieldCompatibilityException : Exception
    {
        public FieldCompatibilityException(string message) : base(message) { }
    }

    public class TableException : Exception
    {
        public TableException(string message) : base(message) { }
    }
}
