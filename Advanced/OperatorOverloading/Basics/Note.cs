using System;
namespace Advanced.OperatorOverloading.Basics
{
    /// <summary>
    /// Represents a musical note.
    /// </summary>
    struct Note
    {
        int value;

        internal Note(int semitonesFromA)
        {
            value = semitonesFromA;
        }

        /// <summary>
        /// Overload the + operator to allow adding semitones to a <see cref="Note"/>.
        /// </summary>
        /// <param name="x">A given <see cref="Note"/>.</param>
        /// <param name="semitones">Given semitones.</param>
        /// <returns>A <see cref="Note"/> by adding <paramref name="semitones"/> to <paramref name="x"/></returns>
        public static Note operator + (Note x, int semitones)
            => new Note(x.value + semitones);

        /// <summary>
        /// Convert a given <see cref="Note"/> to frequency (in hertz).
        /// </summary>
        /// <param name="x">A given note.</param>
        public static implicit operator double(Note x)
            => 440 * Math.Pow(2, (double)x.value / 12);

        /// <summary>
        /// Convert a given frequency (in hertz) (<paramref name="x"/>)
        /// to a <see cref="Note"/> (accurate to the nearest semitone).
        /// </summary>
        /// <param name="x">A given frequency (in hertz).</param>
        public static explicit operator Note(double x)
            => new Note((int)(0.5 + 12 * (Math.Log(x / 440) / Math.Log(2))));

        /// <summary>
        /// Overrides <see cref="ValueType.ToString"/>
        /// </summary>
        public override string ToString()
        {
            return value.ToString();
        }

    }
}
