using System.Collections.Generic;

namespace SparklyCapybara.AbstarctSyntaxTree
{
    public class ProgramNode : AST.ASTNode
    {
        public List<AST.ASTNode> Statements { get; private set; }

        public ProgramNode()
        {
            Statements = new List<AST.ASTNode>();
        }

        public void AddStatement(AST.ASTNode statement)
        {
            Statements.Add(statement);
        }

        public override void Accept(ASTEvaluator visitor)
        {
            visitor.VisitProgramNode(this);
        }
    }
}
