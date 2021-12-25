namespace Day24.Arguments
{
    public abstract class Argument
    {
        public abstract object Code { get; }

        public abstract int Value { get; }

        public static implicit operator Argument(int value)
            => (Number)value;

        public static implicit operator int(Argument arg)
            => arg.Value;

        public override bool Equals(object? obj)
        {
            if (obj is null)
            {
                return false;
            }

            if (GetType() == obj.GetType())
            {
                return Value == ((Argument)obj).Value;
            }

            return false;
        }

        public override int GetHashCode()
            => Value.GetHashCode();
    }
}
