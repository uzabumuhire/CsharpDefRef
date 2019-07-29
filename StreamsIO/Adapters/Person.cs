using System.IO;

namespace StreamsIO.Adapters
{
    class Person
    {
        /// <summary>
        /// The name of this person.
        /// </summary>
        internal string Name;

        /// <summary>
        /// The age of this person.
        /// </summary>
        internal int Age;

        /// <summary>
        /// The height of this person.
        /// </summary>
        internal double Height;

        internal Person() {}

        /// <summary>
        /// Create and initializes a new person from a stream.
        /// </summary>
        /// <param name="s">A stream where to load from.</param>
        internal Person(Stream s)
        {
            LoadData(s);
        }

        /// <summary>
        /// <see cref="object.ToString"/>
        /// </summary>
        /// <returns>A string representation of this person.</returns>
        public override string ToString()
        {
            if (Age < 2)
                return Name + " is " + Age + " year old and his height is " + Height;
            return Name + " is " + Age + " years old and his height is " + Height;
        }

        /// <summary>
        /// Saves the person's data to a stream using
        /// a binary adapter.
        /// </summary>
        /// <param name="s">A stream to write to.</param>
        internal void SaveData(Stream s)
        {
            var bw = new BinaryWriter(s);
            
            bw.Write(Name);
            bw.Write(Age);
            bw.Write(Height);

            // Ensure the `BinaryWriter` buffer
            // is cleared. We won't dispose/close it,
            // so more data can be written to the stream.
            bw.Flush();
        }

        /// <summary>
        /// Loads the person's data from a stream using
        /// a binary adapter.
        /// </summary>
        /// <param name="s">A stream to read from.</param>
        internal void LoadData(Stream s)
        {
            var br = new BinaryReader(s);

            Name = br.ReadString();
            Age = br.ReadInt32();
            Height = br.ReadDouble();
        }
    }
}
