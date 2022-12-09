using BattleshipsCore.Communication;
using BattleshipsCore.Game;
using Newtonsoft.Json;
using System.Collections;

namespace BattleshipsCore.Server.ConsoleCommands
{
    internal abstract class Expression
    {
        public abstract object Interpret(ServerContext context);
    }

    internal class ErrorExpression : Expression
    {
        public Exception Exception { get; set; }

        public ErrorExpression(Exception exception)
        {
            Exception = exception;
        }

        public override object Interpret(ServerContext context)
        {
            return this;
        }
    }

    internal class PrintStatement : Expression
    {
        private Expression ValueExpression { get; set; }

        public PrintStatement(Expression expr)
        {
            ValueExpression = expr;
        }

        public override object Interpret(ServerContext context)
        {
            var value = ValueExpression.Interpret(context);
            
            if (value is IEnumerable list)
            {
                foreach (var item in list)
                {
                    CheckedPrint(item);
                }
            }
            else
            {
                CheckedPrint(value);
            }

            return value;
        }

        private static void CheckedPrint(object value)
        {
            if (value is PlayerData pd)
            {
                string output;
                if (pd.JoinedSession == null)
                {
                    output = $"Player {pd.Name}";
                }
                else
                {
                    output = $"Player: {pd.Name} | Joined session: {pd.JoinedSession.SessionName}";
                }

                ServerLogger.Instance.LogInfo(output);
            }
            else
            {
                var json = JsonConvert.SerializeObject(value);

                ServerLogger.Instance.LogInfo(json);
            }
        }
    }

    internal class DisconnectStatement : Expression
    {
        private Expression PlayerExpression { get; set; }

        public DisconnectStatement(Expression playerExpression)
        {
            PlayerExpression = playerExpression;
        }

        public override object Interpret(ServerContext context)
        {
            var playerValue = PlayerExpression.Interpret(context);

            if (playerValue is IEnumerable<PlayerData> players)
            {
                context.Disconnect(players);

                return playerValue;
            }
            else if (playerValue is PlayerData player)
            {
                context.Disconnect(new[] { player });

                return playerValue;
            }
            else
            {
                throw new ArgumentException("Disconnect statement requires a player or array of players");
            }
        }
    }

    internal class EqualityExpression : Expression
    {
        private Expression Left { get; set; }
        private Expression Right { get; set; }
        private bool Invert { get; set; }

        public EqualityExpression(Expression left, Expression right, bool invert)
        {
            Left = left;
            Right = right;
            Invert = invert;
        }

        public override object Interpret(ServerContext context)
        {
            var leftValue = Left.Interpret(context);
            var rightValue = Right.Interpret(context);

            if (leftValue is long ll && rightValue is long rl)
            {
                if (Invert)
                {
                    return ll != rl;
                }

                return ll == rl;
            }
            else if (leftValue is string && rightValue is string)
            {
                if (Invert)
                {
                    return !leftValue.Equals(rightValue);
                }

                return leftValue.Equals(rightValue);
            }
            else if (leftValue is bool lb && rightValue is bool rb)
            {
                if (Invert)
                {
                    return lb != rb;
                }

                return lb == rb;
            }

            return false;
        }
    }

    internal class AdditionExpression : Expression
    {
        private Expression Left { get; set; }
        private Expression Right { get; set; }

        public AdditionExpression(Expression left, Expression right)
        {
            Left = left;
            Right = right;
        }

        public override object Interpret(ServerContext context)
        {
            var leftValue = Left.Interpret(context);
            var rightValue = Right.Interpret(context);

            if (leftValue is string ls && rightValue is string rs)
            {
                return ls + rs;
            }
            else if (leftValue is long li && rightValue is long ri)
            {
                return li + ri;
            }
            else
            {
                throw new ArgumentException("Cannot add types");
            }
        }
    }

    internal class IdentifierExpression : Expression
    {
        private string Name { get; set; }

        public IdentifierExpression(string name)
        {
            Name = name;
        }

        public override object Interpret(ServerContext context)
        {
            var variable = context.ReadVariable(Name);

            if (variable == null)
            {
                throw new ArgumentException("Unknown variable");
            }

            return variable;
        }
    }

    internal class LiteralExpression : Expression
    {
        private object Value { get; set; }

        public LiteralExpression(object value)
        {
            Value = value;
        }

        public override object Interpret(ServerContext context)
        {
            return Value;
        }
    }

    internal class WhereExpression : Expression
    {
        private Expression ArrayExpression { get; set; }
        private Expression Condition { get; set; }

        public WhereExpression(Expression array, Expression condition)
        {
            ArrayExpression = array;
            Condition = condition;
        }

        public override object Interpret(ServerContext context)
        {
            var array = ArrayExpression.Interpret(context);

            if (array is IEnumerable arr)
            {
                var output = new List<object>();
                long i = 0;

                foreach (var item in arr)
                {
                    if (item is null)
                    {
                        i++;
                        continue;
                    }

                    context.SetVariable("index", i);
                    var tempVariables = context.SetVariablesFromProperties(item);

                    var evaluatedValue = Condition.Interpret(context);

                    if (evaluatedValue is bool b)
                    {
                        if (b)
                        {
                            output.Add(item);
                        }
                    }
                    else
                    {
                        throw new ArgumentException("Logic expression does not return boolean");
                    }

                    context.RemoveVariable("index");
                    foreach (var tempVar in tempVariables)
                    {
                        context.RemoveVariable(tempVar);
                    }

                    i++;
                }

                return output;
            }
            else
            {
                throw new ArgumentException("Where expression is used on arrays");
            }
        }
    }
}
