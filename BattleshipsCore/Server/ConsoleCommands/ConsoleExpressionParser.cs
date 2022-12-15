using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BattleshipsCore.Server.ConsoleCommands
{
    internal class ConsoleExpressionParser
    {
        private List<Token> Tokens { get; set; }
        private int Current { get; set; }

        private bool AtEnd => Tokens[Current].Type == TokenTypes.EOF;
        private Token Lookahead => Tokens[Current];
        private Token Previous => Tokens[Current - 1];

        public ConsoleExpressionParser()
        {
            Tokens = new List<Token>();
        }

        public Expression? ParseTokens(List<Token> tokens)
        {
            Tokens = tokens;
            Current = 0;

            try
            {
                return TopLevelExpression();
            }
            catch (Exception e)
            {
                ServerLogger.Instance.LogError(e.Message);

                return null;
            }
        }

        private Expression TopLevelExpression()
        {
            Expression? expr = null;

            if (Match(TokenTypes.PRINT))
            {
                Consume(TokenTypes.PARENTH_LEFT, "Left parenthesis missing from print expression");

                expr = new PrintStatement(Expression());

                Consume(TokenTypes.PARENTH_RIGHT, "Right parenthesis missing from print expression");
            }
            else if (Match(TokenTypes.DISCONNECT))
            {
                Consume(TokenTypes.PARENTH_LEFT, "Left parenthesis missing from disconnect expression");

                expr = new DisconnectStatement(Expression());

                Consume(TokenTypes.PARENTH_RIGHT, "Right parenthesis missing from disconnect expression");
            }

            if (expr == null) throw new ArgumentNullException("Failed to create a top level expression");

            return expr;
        }

        private Expression Expression()
        {
            return Where();
        }

        private Expression Where()
        {
            var expr = Equality();

            if (Match(TokenTypes.WHERE))
            {
                if (expr is IdentifierExpression)
                {
                    var cond = Equality();

                    return new WhereExpression(expr, cond);
                }
            }

            return expr;
        }

        private Expression Equality()
        {
            var expr = Additive();

            while (Match(TokenTypes.EQUALITY, TokenTypes.INEQUALITY))
            {
                var op = Previous;
                var right = Equality();

                return new EqualityExpression(expr, right, op.Type == TokenTypes.INEQUALITY);
            }

            return expr;
        }

        private Expression Additive()
        {
            var expr = Primary();

            while (Match(TokenTypes.PLUS))
            {
                var right = Additive();

                return new AdditionExpression(expr, right);
            }

            return expr;
        }

        private Expression Primary()
        {
            if (Match(TokenTypes.TRUE)) return new LiteralExpression(true);
            if (Match(TokenTypes.FALSE)) return new LiteralExpression(false);
            if (Match(TokenTypes.NUMBER)) return new LiteralExpression(Previous.Value!);
            if (Match(TokenTypes.STRING)) return new LiteralExpression(Previous.Value!);
            if (Match(TokenTypes.IDENTIFIER))
            {
                return new IdentifierExpression(Previous.Value!.ToString()!);
            }

            throw new ArgumentException("Failed to match anything");
        }

        private bool Match(params TokenTypes[] tokens)
        {
            foreach (var token in tokens)
            {
                if (NextIsType(token))
                {
                    Advance();
                    return true;
                }
            }

            return false;
        }

        private bool NextIsType(TokenTypes token)
        {
            if (AtEnd) return false;

            return Lookahead.Type == token;
        }

        private Token Advance()
        {
            if (!AtEnd) Current++;

            return Previous;
        }

        private Token Consume(TokenTypes token, string message)
        {
            if (NextIsType(token)) return Advance();

            throw new Exception();
        }
    }
}
