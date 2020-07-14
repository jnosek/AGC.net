using Apollo.Virtual.AGC.Instructions;
using Apollo.Virtual.AGC.Math;
using Apollo.Virtual.AGC.Memory;
using Apollo.Virtual.AGC.Registers;

namespace Apollo.Virtual.AGC
{
    // http://www.ibiblio.org/apollo/assembly_language_manual.html

    /// <summary>
    /// No parity bits are used in this implementation
    /// </summary>
    public class Processor
    {
        internal IMemoryBus Memory;

        private readonly InstructionList standardInstructions;
        private readonly InstructionList extraCodeInstructions;

        /// <summary>
        /// used to allow more than 8 instruction codes
        /// </summary>
        internal bool ExtraCodeFlag = false;

        #region Register Definitions

        /// <summary>
        /// Accumulator
        /// </summary>
        internal AccumulatorRegister A;

        /// <summary>
        /// The lower product after MP instructions
        /// also known as L
        /// </summary>
        internal IWord L;

        /// <summary>
        ///  Remainder from the DV instruction, 
        ///  and the return address after TC instructions
        /// </summary>
        internal IWord Q;

        /// <summary>
        /// Erasable Bank Register
        /// </summary>
        internal IWord EB;

        /// <summary>
        /// Fixed Bank Register
        /// </summary>
        internal IWord FB;

        /// <summary>
        /// Program Counter
        /// </summary>
        internal ProgramCounter Z;

        /// <summary>
        /// Both Banks Register
        /// </summary>
        internal IWord BB;

        /// <summary>
        /// Holds A value during interrupts, must be set by handler
        /// </summary>
        internal IWord ARUPT;

        /// <summary>
        /// Holds L value during interrupts, must be set by handler
        /// </summary>
        internal IWord LRUPT;

        /// <summary>
        /// Holds Q value during interrupts, must be set by handler
        /// </summary>
        internal IWord QRUPT;

        /// <summary>
        /// Holds Z value during interrupts, must be set by handler
        /// </summary>
        internal IWord ZRUPT;

        /// <summary>
        /// Holds BB value during interrupts, must be set by handler
        /// </summary>
        internal IWord BBRUPT;

        /// <summary>
        /// Stores the value at the return address of an interrupt service routine.
        /// It is the instruction tobe executed when the interrupt service routine returns.
        /// The CPU automatically loads this register before running an interrupt service routine.
        /// TODO: this might not be used, may have issues?
        /// </summary>
        internal IWord BRUPT;

        /// <summary>
        /// Cycle Right Register, value written is automatically cycled right
        /// least significant bit (1), wrapping into bit 15
        /// </summary>
        internal CycleRightRegister CYR;

        /// <summary>
        /// Shift Right Register, value writtin is automatically shifted right
        /// most significant bit (15) is duplicated into bit 14, least significant bit 1 is discarded
        /// </summary>
        internal IWord SR;

        /// <summary>
        /// Cycle Left Register, value is read back is automatically cycled left
        /// most significant bit (15), wrapping into bit 1
        /// </summary>
        internal IWord CYL;

        /// <summary>
        /// Edit Polish Opcode Register, used mainly for decoding interpreter instructions
        /// Value written is automatically shifted right 7 positions and the upper 8 bites are zeroed
        /// </summary>
        internal IWord EDOP;

        /// <summary>
        /// Paired with TIME1, to make 28 bit time value
        /// </summary>
        internal IWord TIME2;

        /// <summary>
        /// 15-bit 1's compliment counter, incremented every 10ms.
        /// Overflows ever 2^14 * 10 ms, or 163.84 seconds
        /// Upon overflow, TIME2 is automatically incremented.
        /// This forms a 28-bit value capable of tracking time for 2^28 * 10 ms, or about 31 days
        /// Master clock for the AGC
        /// </summary>
        internal IWord TIME1;

        /// <summary>
        /// 15-bit 1's compliment counter, incremented every 10ms.
        /// Upon overflow, requests an interrupt (T3RUPT) which vectors to ISR @4014 (0x80C)
        /// Used by the wait-list for scheduing multi-tasking
        /// 5ms out of phase with TIME4. if a routine is less than 4ms, they will not conflict
        /// Software typically loads this with a value chosen to insure a desired interrupt rate
        /// </summary>
        internal IWord TIME3;

        /// <summary>
        /// See TIME3
        /// Upon overflow, requests an interrupt (T4RUPT) which vectors to ISR @4020 (0x810)
        /// Services the DSKY's display
        /// </summary>
        internal IWord TIME4;

        /// <summary>
        /// See TIME3
        /// Upon overflow, requests an interrupt (T5RUPT) which vectors to ISR @4010 (0x808)
        /// Used by the DAP (Digital Auto Pilot)
        /// </summary>
        internal IWord TIME5;

        /// <summary>
        /// 15-bit 1's compliment counter, incremented every 1/1600 seconds by means of a DINC unprogrammed sequence.
        /// Can be enabled or disabled through bit-15 of i/o channel @13
        /// Upon overflow, requests an interrupt (T6RUPT) which vectors to ISR @4004 (0x804)
        /// then turns off the T6RUPT counter-enable bit
        /// Used by the DAP of the LM to control RCS
        /// </summary>
        internal IWord TIME6;

        /// <summary>
        /// Inner Gimbal Angle
        /// </summary>
        internal IWord CDUX;

        /// <summary>
        /// Middle Gimbal Angle
        /// </summary>
        internal IWord CDUY;

        /// <summary>
        /// Outer Gimbal Angle
        /// </summary>
        internal IWord CDUZ;

        /// <summary>
        /// Orientation of the optics subsystem
        /// Trunnion Angle
        /// </summary>
        internal IWord OPTY;

        /// <summary>
        /// Orientation of the optics subsystem
        /// Shaft Angle
        /// </summary>
        internal IWord OPTX;

        /// <summary>
        /// Pulsed Integrating Pendulous Accelerometer
        /// Monitors vehicle velocity
        /// </summary>
        internal IWord PIPAX;

        /// <summary>
        /// Pulsed Integrating Pendulous Accelerometer
        /// Monitors vehicle velocity
        /// </summary>
        internal IWord PIPAY;

        /// <summary>
        /// Pulsed Integrating Pendulous Accelerometer
        /// Monitors vehicle velocity
        /// </summary>
        internal IWord PIPAZ;

        /// <summary>
        /// Received uplink data from a ground stating
        /// After data is received the UPRUPT interupt is set
        /// </summary>
        internal IWord INLINK;

        #endregion

        public Processor(IMemoryBus memory)
        {
            this.Memory = memory;
            standardInstructions = StandardInstructions.Build(this);
            extraCodeInstructions = ExtraInstructions.Build(this);

            // configure registers

            // main registers?
            A = memory.MapRegister<AccumulatorRegister>(0x00);
            L = memory.MapRegister<ErasableMemory>(0x01);
            Q = memory.MapRegister<Register16>(0x02);
            EB = memory.MapRegister<ErasableBankRegister>(0x03);
            FB = memory.MapRegister<FixedBankRegister>(0x4);
            Z = memory.MapRegister<ProgramCounter>(0x05);
            BB = memory.MapRegister<BothBanksRegister>(0x06);

            //memory[0x7] = 0; // this is always set to 0, TODO: need to hard code?

            // interrupt helper registers
            ARUPT = memory.MapRegister<ErasableMemory>(0x08);
            LRUPT = memory.MapRegister<ErasableMemory>(0x09);
            QRUPT = memory.MapRegister<ErasableMemory>(0x0A);
            // 0XB, 0XC are spares. not used?
            ZRUPT = memory.MapRegister<ErasableMemory>(0x0D);
            BBRUPT = memory.MapRegister<ErasableMemory>(0x0E);
            BRUPT = memory.MapRegister<ErasableMemory>(0x0F);

            // editing registers
            CYR = memory.MapRegister<CycleRightRegister>(0x10);
            SR = memory.MapRegister<ShiftRightRegister>(0x11);
            CYL = memory.MapRegister<ErasableMemory>(0x12); // temp
            EDOP = memory.MapRegister<ErasableMemory>(0x13); // temp

            // time registers
            TIME2 = memory.MapRegister<ErasableMemory>(0x14);  // temp
            TIME1 = memory.MapRegister<ErasableMemory>(0x15);  // temp
            TIME3 = memory.MapRegister<ErasableMemory>(0x16);  // temp
            TIME4 = memory.MapRegister<ErasableMemory>(0x17);  // temp
            TIME5 = memory.MapRegister<ErasableMemory>(0x18);  // temp
            TIME6 = memory.MapRegister<ErasableMemory>(0x19);  // temp

            // orientation registers
            CDUX = memory.MapRegister<ErasableMemory>(0x1A);
            CDUY = memory.MapRegister<ErasableMemory>(0x1B);
            CDUZ = memory.MapRegister<ErasableMemory>(0x1C);
            OPTY = memory.MapRegister<ErasableMemory>(0x1D);
            OPTX = memory.MapRegister<ErasableMemory>(0x1E);
            PIPAX = memory.MapRegister<ErasableMemory>(0x1F);
            PIPAY = memory.MapRegister<ErasableMemory>(0x20);
            PIPAZ = memory.MapRegister<ErasableMemory>(0x21);
            // LM Only Pitch, Yaw, and Roll registers

            INLINK = memory.MapRegister<ErasableMemory>(0x25);

            // prime Z to start at the boot interrupt
            Z.Write(0x800);
        }

        public void Execute()
        {
            // get address of instruction to run
            ushort address = Z.Read();

            // update Z
            Z.Increment();

            // get instruction
            ushort instruction = Memory[address];

            // we only care about 15-bit instructions
            instruction = MaskInstruction(instruction);

            // decode instruction
            var code = DecodeCode(instruction);
            var K = DecodeOperand(instruction);

            // determine if this is an extra code instruction
            if (ExtraCodeFlag)
            {
                // reset CPU for instruction to execute
                extraCodeInstructions[code].Execute(K);

                // clear the extra code flag
                ExtraCodeFlag = false;
            }
            else
            {
                // reset CPU for instruction to execute
                standardInstructions[code].Execute(K);
            }
        }

        private static ushort MaskInstruction(ushort instruction) =>
            (ushort)(instruction & 0x7FFF);

        private static ushort DecodeCode(ushort instruction) =>
            (ushort)(instruction >> 12);

        private static ushort DecodeOperand(ushort instruction) =>
            (ushort)(instruction & 0xFFF);
    }
}

