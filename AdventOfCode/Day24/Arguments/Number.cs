namespace Day24.Arguments
{
    public class Number : Argument
    {
        private readonly int _value;

        public override int Value => _value;

        public override object Code => _value;

        public Number(int value)
            => _value = value;

        public static implicit operator Number(int value)
            => new (value);

        public static Number operator +(Number left, Number right) 
            => left.Value + right.Value;
    }
}
