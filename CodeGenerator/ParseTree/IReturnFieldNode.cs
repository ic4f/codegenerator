using System;

namespace CodeGenerator.ParseTree
{
    public interface ISprocReturnFieldNode : IParseTreeNode
    {
        string Name { get; }
    }
}
