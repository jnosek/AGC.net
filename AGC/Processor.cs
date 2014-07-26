using Apollo.Virtual.AGC.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC
{
    // http://www.ibiblio.org/apollo/assembly_language_manual.html

    public class Processor
    {
        internal MemoryMap Memory;

        private InstructionSet instructions;

        #region Register Definitions

        /// <summary>
        /// Accumulator
        /// </summary>
        public Register A;

        /// <summary>
        /// The lower product after MP instructions
        /// also known as L
        /// </summary>
        public Register LP;

        /// <summary>
        ///  Remainder from the DV instruction, 
        ///  and the return address after TC instructions
        /// </summary>
        public Register Q;

        /// <summary>
        /// Erasable Bank Register
        /// </summary>
        public Register EB;

        /// <summary>
        /// Fixed Bank Register
        /// </summary>
        public Register FB;

        /// <summary>
        /// Program Counter
        /// </summary>
        public Register Z;

        /// <summary>
        /// Both Banks Register
        /// </summary>
        public Register BB;

        /// <summary>
        /// Holds A value during interrupts, must be set by handler
        /// </summary>
        public Register ARUPT;

        /// <summary>
        /// Holds L value during interrupts, must be set by handler
        /// </summary>
        public Register LRUPT;

        /// <summary>
        /// Holds Q value during interrupts, must be set by handler
        /// </summary>
        public Register QRUPT;

        /// <summary>
        /// Holds Z value during interrupts, must be set by handler
        /// </summary>
        public Register ZRUPT;

        /// <summary>
        /// Holds BB value during interrupts, must be set by handler
        /// </summary>
        public Register BBRUPT;

        /// <summary>
        /// Stores the value at the return address of an interrupt service routine.
        /// It is the instruction tobe executed when the interrupt service routine returns.
        /// The CPU automatically loads this register before running an interrupt service routine.
        /// TODO: this might not be used, may have issues?
        /// </summary>
        public Register BRUPT;

        /// <summary>
        /// Cycle Right Register, value written is automatically cycled right
        /// least significant bit (1), wrapping into bit 15
        /// </summary>
        public Register CYR;

        /// <summary>
        /// Shift Right Register, value writtin is automatically shifted right
        /// most significant bit (15) is duplicated into bit 14, least significant bit 1 is discarded
        /// </summary>
        public Register SR;

        /// <summary>
        /// Cycle Left Register, value is read back is automatically cycled left
        /// most significant bit (15), wrapping into bit 1
        /// </summary>
        public Register CYL;

        /// <summary>
        /// Edit Polish Opcode Register, used mainly for decoding interpreter instructions
        /// Value written is automatically shifted right 7 positions and the upper 8 bites are zeroed
        /// </summary>
        public Register EDOP;

        /// <summary>
        /// Paired with TIME1, to make 28 bit time value
        /// </summary>
        public Register TIME2;

        /// <summary>
        /// 15-bit 1's compliment counter, incremented every 10ms.
        /// Overflows ever 2^14 * 10 ms, or 163.84 seconds
        /// Upon overflow, TIME2 is automatically incremented.
        /// This forms a 28-bit value capable of tracking time for 2^28 * 10 ms, or about 31 days
        /// Master clock for the AGC
        /// </summary>
        public Register TIME1;

        /// <summary>
        /// 15-bit 1's compliment counter, incremented every 10ms.
        /// Upon overflow, requests an interrupt (T3RUPT) which vectors to ISR @4014 (0x80C)
        /// Used by the wait-list for scheduing multi-tasking
        /// 5ms out of phase with TIME4. if a routine is less than 4ms, they will not conflict
        /// Software typically loads this with a value chosen to insure a desired interrupt rate
        /// </summary>
        public Register TIME3;

        /// <summary>
        /// See TIME3
        /// Upon overflow, requests an interrupt (T4RUPT) which vectors to ISR @4020 (0x810)
        /// Services the DSKY's display
        /// </summary>
        public Register TIME4;

        /// <summary>
        /// See TIME3
        /// Upon overflow, requests an interrupt (T5RUPT) which vectors to ISR @4010 (0x808)
        /// Used by the DAP (Digital Auto Pilot)
        /// </summary>
        public Register TIME5;

        /// <summary>
        /// 15-bit 1's compliment counter, incremented every 1/1600 seconds by means of a DINC unprogrammed sequence.
        /// Can be enabled or disabled through bit-15 of i/o channel @13
        /// Upon overflow, requests an interrupt (T6RUPT) which vectors to ISR @4004 (0x804)
        /// then turns off the T6RUPT counter-enable bit
        /// Used by the DAP of the LM to control RCS
        /// </summary>
        public Register TIME6;

        /// <summary>
        /// Inner Gimbal Angle
        /// </summary>
        public Register CDUX;

        /// <summary>
        /// Middle Gimbal Angle
        /// </summary>
        public Register CDUY;

        /// <summary>
        /// Outer Gimbal Angle
        /// </summary>
        public Register CDUZ;

        /// <summary>
        /// Orientation of the optics subsystem
        /// Trunnion Angle
        /// </summary>
        public Register OPTY;

        /// <summary>
        /// Orientation of the optics subsystem
        /// Shaft Angle
        /// </summary>
        public Register OPTX;

        /// <summary>
        /// Pulsed Integrating Pendulous Accelerometer
        /// Monitors vehicle velocity
        /// </summary>
        public Register PIPAX;

        /// <summary>
        /// Pulsed Integrating Pendulous Accelerometer
        /// Monitors vehicle velocity
        /// </summary>
        public Register PIPAY;

        /// <summary>
        /// Pulsed Integrating Pendulous Accelerometer
        /// Monitors vehicle velocity
        /// </summary>
        public Register PIPAZ;

        /// <summary>
        /// Received uplink data from a ground stating
        /// After data is received the UPRUPT interupt is set
        /// </summary>
        public Register INLINK;

        #endregion

        public Processor(MemoryMap memory)
        {
            this.Memory = memory;
            instructions = new InstructionSet(this);

            // configure registers

            // main registers?
            A = new Register(memory.GetAddress(0x0));
            LP = new Register(memory.GetAddress(0x1));
            Q = new Register(memory.GetAddress(0x2));
            EB = new Register(memory.GetAddress(0x3));
            FB = new Register(memory.GetAddress(0x4));
            Z = new Register(memory.GetAddress(0x5));
            BB = new Register(memory.GetAddress(0x6));

            memory[0x7] = 0; // this is always set to 0, TODO: need to hard code?

            // interrupt helper registers
            ARUPT = new Register(memory.GetAddress(0x8));
            LRUPT = new Register(memory.GetAddress(0x9));
            QRUPT = new Register(memory.GetAddress(0xA));
            // 0XB, 0XC are spares. not used?
            ZRUPT = new Register(memory.GetAddress(0xD));
            BBRUPT = new Register(memory.GetAddress(0xE));
            BRUPT = new Register(memory.GetAddress(0xF));

            // editing registers
            CYR = new Register(memory.GetAddress(0x10));
            SR = new Register(memory.GetAddress(0x11));
            CYL = new Register(memory.GetAddress(0x12));
            EDOP = new Register(memory.GetAddress(0x13));

            // time registers
            TIME2 = new Register(memory.GetAddress(0x14));
            TIME1 = new Register(memory.GetAddress(0x15));
            TIME3 = new Register(memory.GetAddress(0x16));
            TIME4 = new Register(memory.GetAddress(0x17));
            TIME5 = new Register(memory.GetAddress(0x18));
            TIME6 = new Register(memory.GetAddress(0x19));

            // orientation registers
            CDUX = new Register(memory.GetAddress(0x1A));
            CDUY = new Register(memory.GetAddress(0x1B));
            CDUZ = new Register(memory.GetAddress(0x1C));
            OPTY = new Register(memory.GetAddress(0x1D));
            OPTX = new Register(memory.GetAddress(0x1E));
            PIPAX = new Register(memory.GetAddress(0x1F));
            PIPAY = new Register(memory.GetAddress(0x20));
            PIPAZ = new Register(memory.GetAddress(0x21));
            // LM Only Pitch, Yaw, and Roll registers

            INLINK = new Register(memory.GetAddress(0x25));

        }


        public void Execute()
        {
            // get address of instruction to run
            var address = Memory.GetAddress(Z.Read());

            // update Z
            Z.Write((ushort)(Z.Read() + 1));

            var code = (ushort)(address.Read() >> 12);
            var K = (ushort)(address.Read() & 0xFFF);

            instructions[code].Execute(K);
        }
    }
}

