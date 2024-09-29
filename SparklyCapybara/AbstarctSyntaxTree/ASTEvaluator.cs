using System;
using System.Collections.Generic;
using static SparklyCapybara.AbstarctSyntaxTree.AST;

namespace SparklyCapybara.AbstarctSyntaxTree
{
    public interface IVisitor
    {
        void VisitFunctionDeclaration(AST.FunctionDeclaration function);
        void VisitVariableDeclaration(AST.VariableDeclaration variable);
        void VisitBinaryExpression(AST.BinaryExpression binary);
        void VisitConditionalExpression(AST.ConditionalExpression conditional);
        void VisitLiteralExpression(AST.LiteralExpression literal);
        void VisitVariableExpression(AST.VariableExpression variable);
        void VisitFunctionCallExpression(AST.FunctionCallExpression functionCall);
        void VisitWhileLoop(AST.WhileLoop whileLoop);
        void VisitForLoop(AST.ForLoop forLoop);
        void VisitArrayDeclaration(AST.ArrayDeclaration arrayDeclaration);
        void VisitArrayAccess(AST.ArrayAccess arrayAccess);
        void VisitUnaryExpression(AST.UnaryExpression unaryExpression);
    }

    public class ASTEvaluator : IVisitor
    {
        private Dictionary<string, object> _variables = new Dictionary<string, object>();

        public void VisitProgramNode(ProgramNode program)
        {
            foreach (var statement in program.Statements)
            {
                statement.Accept(this);
            }
        }

        public void VisitFunctionDeclaration(AST.FunctionDeclaration function)
        {
            Console.WriteLine($"Function {function.Name} declared with return types: {string.Join(", ", function.ReturnTypes)}");
            foreach (var param in function.Parameters)
            {
                Console.WriteLine($"Parameter: {param.Type} {param.Name}");
            }
        }

        public void VisitVariableDeclaration(AST.VariableDeclaration variable)
        {
            var value = Evaluate(variable.InitialValue);
            _variables[variable.Name] = value;
            Console.WriteLine($"Variable '{variable.Name}' of type '{variable.Type}' initialized to {value}");
        }

        public void VisitBinaryExpression(AST.BinaryExpression binary)
        {
            var left = Evaluate(binary.Left);
            var right = Evaluate(binary.Right);
            var result = PerformBinaryOperation(left, right, binary.Operator);
            Console.WriteLine($"Binary expression {left} {binary.Operator} {right} = {result}");
        }

        public void VisitConditionalExpression(AST.ConditionalExpression conditional)
        {
            var condition = (bool)Evaluate(conditional.Condition);
            if (condition)
            {
                Evaluate(conditional.TrueExpression);
            }
            else
            {
                Evaluate(conditional.FalseExpression);
            }
        }

        public void VisitLiteralExpression(AST.LiteralExpression literalExpression)
        {
            Console.WriteLine($"Literal value: {literalExpression.Value}");
        }

        public void VisitVariableExpression(AST.VariableExpression variableExpression)
        {
            if (_variables.ContainsKey(variableExpression.Name))
            {
                Console.WriteLine($"Variable {variableExpression.Name} value: {_variables[variableExpression.Name]}");
            }
            else
            {
                throw new Exception($"Variable '{variableExpression.Name}' not defined");
            }
        }

        public void VisitFunctionCallExpression(AST.FunctionCallExpression functionCallExpression)
        {
            Console.WriteLine($"Function {functionCallExpression.FunctionName} called with arguments: {string.Join(", ", functionCallExpression.Arguments)}");
            foreach (var arg in functionCallExpression.Arguments)
            {
                Evaluate(arg);
            }
        }

        public void VisitWhileLoop(AST.WhileLoop whileLoop)
        {
            while ((bool)Evaluate(whileLoop.Condition))
            {
                foreach (var stmt in whileLoop.Body)
                {
                    Evaluate(stmt);
                }
            }
        }

        public void VisitForLoop(AST.ForLoop forLoop)
        {
            Evaluate(forLoop.Initializer);
            while ((bool)Evaluate(forLoop.Condition))
            {
                foreach (var stmt in forLoop.Body)
                {
                    Evaluate(stmt);
                }
                Evaluate(forLoop.Increment);
            }
        }

        public void VisitArrayDeclaration(AST.ArrayDeclaration arrayDeclaration)
        {
            var array = new object[arrayDeclaration.Size];
            _variables[arrayDeclaration.ArrayName] = array;
            Console.WriteLine($"Array '{arrayDeclaration.ArrayName}' of type '{arrayDeclaration.ElementType}' with size {arrayDeclaration.Size} declared.");
        }

        public void VisitArrayAccess(AST.ArrayAccess arrayAccess)
        {
            if (_variables.ContainsKey(arrayAccess.ArrayName) && _variables[arrayAccess.ArrayName] is object[] array)
            {
                var index = (int)Evaluate(arrayAccess.Index);
                Console.WriteLine($"Accessing array '{arrayAccess.ArrayName}' at index {index}, value: {array[index]}");
            }
            else
            {
                throw new Exception($"Array '{arrayAccess.ArrayName}' not defined or not an array");
            }
        }

        public void VisitUnaryExpression(AST.UnaryExpression unaryExpression)
        {
            var operand = Evaluate(unaryExpression.Operand);
            switch (unaryExpression.Operator)
            {
                case "-":
                    Console.WriteLine($"Unary negation: -{operand}");
                    break;
                case "++":
                    Console.WriteLine($"Incrementing: {operand}++");
                    break;
                case "--":
                    Console.WriteLine($"Decrementing: {operand}--");
                    break;
                default:
                    throw new Exception($"Unknown unary operator {unaryExpression.Operator}");
            }
        }

        // Helper method to evaluate expressions
        private object Evaluate(ASTNode node)
        {
            if (node is AST.LiteralExpression literal)
            {
                return literal.Value;
            }
            if (node is AST.VariableExpression variable)
            {
                return _variables[variable.Name];
            }
            node.Accept(this);
            return null;
        }

        // Helper method to perform binary operations
        private object PerformBinaryOperation(object left, object right, string op)
        {
            switch (op)
            {
                case "+":
                    return Convert.ToInt32(left) + Convert.ToInt32(right);
                case "-":
                    return Convert.ToInt32(left) - Convert.ToInt32(right);
                case "*":
                    return Convert.ToInt32(left) * Convert.ToInt32(right);
                case "/":
                    return Convert.ToInt32(left) / Convert.ToInt32(right);
                default:
                    throw new Exception($"Unknown binary operator {op}");
            }
        }
    }
}
