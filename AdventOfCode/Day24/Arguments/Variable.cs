namespace Day24.Arguments
{
    public class Variable : Argument
    {
        private int _value;

        public override int Value => _value;

        public override object Code => Reference;

        public char Reference { get; }

        public Variable(char reference) 
            => Reference = reference;

        public void Store(int value)
            => _value = value;

        public void Clear()
            => _value = default;

        public override int GetHashCode()
            => Reference.GetHashCode();

        public override string ToString()
            => $"{Reference}={Value}";
    }
}
