using Apollo.Virtual.AGC.Math;
using System.Diagnostics;

namespace Apollo.Virtual.AGC.Instructions
{
    /// <summary>
    /// BZF - EX 0001 01
    ///       EX 0001 10
    ///       EX 0001 11
    /// 
    /// Jumps to a fixed memory location if the accumulator is 0
    /// The memory location must assemble to a 12-bit memory address in fixed memory.  (In other words, the two most significant bits of address K cannot be 00.)
    /// </summary>
    public class BranchZeroToFixed : IQuarterCodeInstruction
    {
        private const ushort _code = 0x1;
        private const ushort _instruction = _code << 12;

        public static ushort Encode(ushort address)
        {
            // cannot encode an instruction not in fixed memory (this would then be a Divide instruction)
            // check mask for any 1 bits
            Debug.Assert((0x0C00 | address) == 0);

            return (ushort)(_instruction | address);
        }

        public BranchZeroToFixed(Processor cpu)
        {
            this.cpu = cpu;
        }

        private readonly Processor cpu;

        ushort IInstruction.Code => _code;

        ushort IQuarterCodeInstruction.QuarterCode => 0x01;

        void IInstruction.Execute(ushort K)
        {
            // if in overflow, no jump
            if(cpu.A.IsOverflow)
                return;
            
            var value = cpu.A.Read();

            // if +0 or -0, then jump
            if (value == 0 || value == OnesCompliment.NegativeZero)
                cpu.Z.Write(K);
        }
    }

    /// <summary>
    /// Stand in quartercode "like" instruction for additional Fixed memory addresses
    /// </summary>
    public class BranchZeroToFixed10 : BranchZeroToFixed, IQuarterCodeInstruction
    {
        public BranchZeroToFixed10(Processor cpu) : base(cpu)
        {
        }

        ushort IQuarterCodeInstruction.QuarterCode => 0x10;
    }

    /// <summary>
    /// Stand in quartercode "like" instruction for additional Fixed memory addresses
    /// </summary>
    public class BranchZeroToFixed11: BranchZeroToFixed, IQuarterCodeInstruction
    {
        public BranchZeroToFixed11(Processor cpu) : base(cpu)
        {
        }

        ushort IQuarterCodeInstruction.QuarterCode => 0x11;
    }
}
