namespace Advanced.OperatorOverloading.Basics
{
    struct SQLServerBoolean
    {
        internal static readonly SQLServerBoolean Null = new SQLServerBoolean(0);
        internal static readonly SQLServerBoolean False = new SQLServerBoolean(1);
        internal static readonly SQLServerBoolean True = new SQLServerBoolean(2);

        byte _value;

        SQLServerBoolean(byte value)
        {
            _value = value;
        }

        public override string ToString()
        {
            if (this._value == Null._value)
                return "Null";
            if (this._value == False._value)
                return "False";
            return "True";
        }

        public static bool operator true(SQLServerBoolean x)
            => x._value == True._value;

        public static bool operator false(SQLServerBoolean x)
            => x._value == False._value;

        public static SQLServerBoolean operator !(SQLServerBoolean x)
        {
            if (x._value == Null._value)
                return Null;
            if (x._value == False._value)
                return False;
            return True;
        }
    }
}
