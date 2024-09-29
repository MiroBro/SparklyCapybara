using System;
using System.Collections.Generic;

namespace SparklyCapybara.AbstarctSyntaxTree
{
    public class AST
    {
        // Base class for AST nodes
        public abstract class ASTNode
        {
            public abstract void Accept(ASTEvaluator visitor);
        }

        // Add this to your AST class
        public class Parameter
        {
            public string Type { get; set; }
            public string Name { get; set; }

            public Parameter(string type, string name)
            {
                Type = type;
                Name = name;
            }
        }

        public class FunctionDeclaration : ASTNode
        {
            public List<Parameter> ReturnTypes { get; set; }  // Change from string to List<Parameter>
            public string Name { get; set; }
            public List<Parameter> Parameters { get; set; }
            public List<ASTNode> Body { get; set; }

            // Adjust the constructor to accept a list of return types
            public FunctionDeclaration(List<Parameter> returnTypes, string name, List<Parameter> parameters, List<ASTNode> body)
            {
                ReturnTypes = returnTypes;
                Name = name;
                Parameters = parameters;
                Body = body;
            }

            public override void Accept(ASTEvaluator visitor)
            {
                visitor.VisitFunctionDeclaration(this);
            }
        }


        // For variable declarations
        public class VariableDeclaration : ASTNode
        {
            public string Type { get; set; }
            public string Name { get; set; }
            public ASTNode InitialValue { get; set; }

            public VariableDeclaration(string type, string name, ASTNode initialValue)
            {
                Type = type;
                Name = name;
                InitialValue = initialValue;
            }

            public override void Accept(ASTEvaluator visitor)
            {
                visitor.VisitVariableDeclaration(this);
            }
        }

        // For binary expressions (e.g., addition, subtraction)
        public class BinaryExpression : ASTNode
        {
            public ASTNode Left { get; set; }
            public string Operator { get; set; }
            public ASTNode Right { get; set; }

            public BinaryExpression(ASTNode left, string op, ASTNode right)
            {
                Left = left;
                Operator = op;
                Right = right;
            }

            public override void Accept(ASTEvaluator visitor)
            {
                visitor.VisitBinaryExpression(this);
            }
        }

        // For conditionals (e.g., ternary or simple if-else)
        public class ConditionalExpression : ASTNode
        {
            public ASTNode Condition { get; set; }
            public ASTNode TrueExpression { get; set; }
            public ASTNode FalseExpression { get; set; }

            public ConditionalExpression(ASTNode condition, ASTNode trueExpr, ASTNode falseExpr)
            {
                Condition = condition;
                TrueExpression = trueExpr;
                FalseExpression = falseExpr;
            }

            public override void Accept(ASTEvaluator visitor)
            {
                visitor.VisitConditionalExpression(this);
            }
        }

        // Literal expressions (e.g., numbers, strings)
        public class LiteralExpression : ASTNode
        {
            public string Value { get; set; }

            public LiteralExpression(string value)
            {
                Value = value;
            }

            public override void Accept(ASTEvaluator visitor)
            {
                visitor.VisitLiteralExpression(this);
            }
        }

        // Variable expressions (e.g., references to variables)
        public class VariableExpression : ASTNode
        {
            public string Name { get; set; }

            public VariableExpression(string name)
            {
                Name = name;
            }

            public override void Accept(ASTEvaluator visitor)
            {
                visitor.VisitVariableExpression(this);
            }
        }

        // Function Call expressions
        public class FunctionCallExpression : ASTNode
        {
            public string FunctionName { get; set; }
            public List<ASTNode> Arguments { get; set; }

            public FunctionCallExpression(string functionName, List<ASTNode> arguments)
            {
                FunctionName = functionName;
                Arguments = arguments;
            }

            public override void Accept(ASTEvaluator visitor)
            {
                visitor.VisitFunctionCallExpression(this);
            }
        }

        // For while loops
        public class WhileLoop : ASTNode
        {
            public ASTNode Condition { get; set; }
            public List<ASTNode> Body { get; set; }

            public WhileLoop(ASTNode condition, List<ASTNode> body)
            {
                Condition = condition;
                Body = body;
            }

            public override void Accept(ASTEvaluator visitor)
            {
                visitor.VisitWhileLoop(this);
            }
        }

        // For for loops
        public class ForLoop : ASTNode
        {
            public ASTNode Initializer { get; set; }
            public ASTNode Condition { get; set; }
            public ASTNode Increment { get; set; }
            public List<ASTNode> Body { get; set; }

            public ForLoop(ASTNode initializer, ASTNode condition, ASTNode increment, List<ASTNode> body)
            {
                Initializer = initializer;
                Condition = condition;
                Increment = increment;
                Body = body;
            }

            public override void Accept(ASTEvaluator visitor)
            {
                visitor.VisitForLoop(this);
            }
        }

        // Array Declaration (e.g., int[] arr = new int[10])
        public class ArrayDeclaration : ASTNode
        {
            public string ElementType { get; set; }
            public string ArrayName { get; set; }
            public int Size { get; set; }

            public ArrayDeclaration(string elementType, string arrayName, int size)
            {
                ElementType = elementType;
                ArrayName = arrayName;
                Size = size;
            }

            public override void Accept(ASTEvaluator visitor)
            {
                visitor.VisitArrayDeclaration(this);
            }
        }

        // Array Access (e.g., arr[0])
        public class ArrayAccess : ASTNode
        {
            public string ArrayName { get; set; }
            public ASTNode Index { get; set; }

            public ArrayAccess(string arrayName, ASTNode index)
            {
                ArrayName = arrayName;
                Index = index;
            }

            public override void Accept(ASTEvaluator visitor)
            {
                visitor.VisitArrayAccess(this);
            }
        }

        // Unary expressions (e.g., -x, ++x, --x)
        public class UnaryExpression : ASTNode
        {
            public string Operator { get; set; }
            public ASTNode Operand { get; set; }

            public UnaryExpression(string op, ASTNode operand)
            {
                Operator = op;
                Operand = operand;
            }

            public override void Accept(ASTEvaluator visitor)
            {
                visitor.VisitUnaryExpression(this);
            }
        }
    }
}
