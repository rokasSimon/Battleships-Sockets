namespace BattleshipsCore.Server.ConsoleCommands
{
    internal interface ITokenParser
    {
        List<Token> ReadTokens();
    }

    internal enum TokenTypes
    {
        BAD_TOKEN, EOF,

        PRINT, BROADCAST, DISCONNECT,

        WHERE,

        PARENTH_LEFT, PARENTH_RIGHT, SEMICOLON, COMMA,

        PLUS, DOT, EQUALITY, INEQUALITY,

        STRING, NUMBER, IDENTIFIER, UNDERSCORE, TRUE, FALSE
    }

    internal class Token
    {
        public TokenTypes Type { get; set; }
        public object? Value { get; set; }

        public Token(TokenTypes type, object? value)
        {
            Type = type;
            Value = value;
        }
    }

    internal class ConsoleCommandTokenParser : ITokenParser
    {
        private readonly Dictionary<string, TokenTypes> _reservedKeywords = new Dictionary<string, TokenTypes>
        {
            {"print", TokenTypes.PRINT},
            {"broadcast", TokenTypes.BROADCAST},
            {"where", TokenTypes.WHERE},
            {"disconnect", TokenTypes.DISCONNECT},
            {"true", TokenTypes.TRUE},
            {"false", TokenTypes.FALSE},
        };

        private int _start;
        private int _current;
        private string? _source;
        private List<Token> _tokens;

        public ConsoleCommandTokenParser()
        {
            _start = _current = 0;
            _tokens = new List<Token>();
            _source = null;
        }

        public List<Token> ReadTokens()
        {
            Console.Write(">>> ");

            _source = Console.ReadLine();
            if (_source == null) return new List<Token> { new Token(TokenTypes.BAD_TOKEN, null) };
            _tokens = new List<Token>();
            _current = 0;

            while (!EOF())
            {
                _start = _current;
                ScanToken();
            }

            _tokens.Add(new Token(TokenTypes.EOF, ""));

            return _tokens;
        }

        private bool EOF() => _source == null || _current >= _source.Length;

        private void ScanToken()
        {
            var c = Advance();

            switch (c)
            {
                case ' ':
                case '\r':
                case '\t': break;
                case '\n': break;

                case '_': AddToken(TokenTypes.UNDERSCORE); break;
                case '(': AddToken(TokenTypes.PARENTH_LEFT); break;
                case ')': AddToken(TokenTypes.PARENTH_RIGHT); break;
                case '+': AddToken(TokenTypes.PLUS); break;
                case '.': AddToken(TokenTypes.DOT); break;
                case ',': AddToken(TokenTypes.COMMA); break;
                case ';': AddToken(TokenTypes.SEMICOLON); break;
                case '!': AddToken(Match('=') ? TokenTypes.INEQUALITY : TokenTypes.BAD_TOKEN); break;
                case '=': AddToken(Match('=') ? TokenTypes.EQUALITY : TokenTypes.BAD_TOKEN); break;
                case '"': HandleString(); break;
                default:
                    {
                        if (IsDigit(c))
                        {
                            HandleNumbers();
                        }
                        else if (IsAlpha(c))
                        {
                            HandleIdentifier();
                        }
                        else
                        {
                            AddToken(TokenTypes.BAD_TOKEN);
                            return;
                        }

                        break;
                    }
            }
        }

        private void AddToken(TokenTypes type, object? value = null)
        {
            _tokens.Add(new Token(type, value));
        }

        private char Advance()
        {
            _current++;

            return _source![_current - 1];
        }

        private bool Match(char expected)
        {
            if (EOF() || _source![_current] != expected)
            {
                return false;
            }

            _current++;
            return true;
        }

        private char LookAhead1()
        {
            if (EOF())
            {
                return '\0';
            }

            return _source![_current];
        }

        private void HandleString()
        {
            while (LookAhead1() != '"' && !EOF())
            {
                Advance();
            }

            if (EOF())
            {
                Console.WriteLine($"Unterminated string.");
                AddToken(TokenTypes.BAD_TOKEN);
                return;
            }

            Advance(); // Skip ending quote
            AddToken(TokenTypes.STRING, _source![(_start + 1)..(_current - 1)]);
        }

        private void HandleNumbers()
        {
            while (IsDigit(LookAhead1()))
            {
                Advance();
            }

            var intToParse = _source![_start.._current];

            AddToken(TokenTypes.NUMBER, long.Parse(intToParse));
        }

        private void HandleIdentifier()
        {
            while (IsAlphaNumeric(LookAhead1()))
            {
                Advance();
            }

            var text = _source![_start.._current];
            var type = _reservedKeywords.ContainsKey(text) ? _reservedKeywords[text] : TokenTypes.IDENTIFIER;

            AddToken(type, text);
        }

        private bool IsDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private bool IsAlpha(char c)
        {
            return (c >= 'a' && c <= 'z') ||
                   (c >= 'A' && c <= 'Z') ||
                   c == '_';
        }

        private bool IsAlphaNumeric(char c)
        {
            return IsDigit(c) ||IsAlpha(c);
        }
    }
}
