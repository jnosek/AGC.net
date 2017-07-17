namespace Apollo.Virtual.AGC.Memory
{
    /// <summary>
    /// Default 15-bit ones compliment memory word
    /// </summary>
    public class MemoryWord : IWord
    {
        private MemoryBank bank;

        public ushort Address { get; private set; }

        public MemoryWord(ushort address, MemoryBank bank)
        {
            this.bank = bank;
            Address = address;
        }

        public virtual OnesCompliment Read()
        {
            return new OnesCompliment(bank[Address]);
        }

        protected ushort ReadRaw()
        {
            return bank[Address];
        }

        /// <summary>
        /// read a value at a specific address within the same memory bank
        /// </summary>
        /// <param name="address"></param>
        /// <returns></returns>
        protected ushort ReadRaw(ushort address)
        {
            return bank[address];
        }

        /// <summary>
        /// 15-bit writes are overflow corrected from 16-bit values
        /// </summary>
        /// <param name="value"></param>
        public virtual void Write(OnesCompliment value)
        {
            // in case there is an overflow, correct it
            var correctedValue = OverflowCorrect(value.NativeValue);

            // sign extend, since we store all values as 16-bit
            correctedValue = SignExtend(correctedValue);

            WriteRaw(correctedValue);
        }

        protected void WriteRaw(ushort value)
        {
            bank[Address] = value;
        }

        protected void WriteRaw(int value)
        {
            bank[Address] = (ushort)value;
        }

        /// <summary>
        /// write a value at a specific address within the same memory bank
        /// </summary>
        /// <param name="value"></param>
        /// <param name="address"></param>
        protected void WriteRaw(int value, ushort address)
        {
            bank[address] = (ushort)value;
        }

        protected static ushort OverflowCorrect(ushort value)
        {
            uint newValue = value;

            // get lower 14 bits
            uint lowerBits = newValue & 0x3FFF;

            // move 16-th bit, into 15th position, isolate it, and set it in above value;
            newValue = (newValue >> 1 & 0x4000) | lowerBits;

            return (ushort)newValue;
        }

        /// <summary>
        /// Performs sign extending on a 15bit value, converting it to a 16 bit value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        protected static ushort SignExtend(ushort value)
        {
            uint newValue = value;

            // take lower 15-bits
            newValue = newValue & 0x7FFF;

            // shift left 1 and take 16th bit, combine with lower 15 bits
            newValue = ((newValue << 1) & 0x8000) | newValue;

            return (ushort)newValue;
        }
    }
}
