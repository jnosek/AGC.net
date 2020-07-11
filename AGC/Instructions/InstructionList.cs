using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Apollo.Virtual.AGC.Instructions
{
    class InstructionList : IEnumerable
    {
        private readonly IInstruction[] array;

        /// <summary>
        /// Add a list of instructions to a list indexed based on code
        /// </summary>
        /// <param name="instructions"></param>
        public InstructionList(IEnumerable<IInstruction> instructions)
        {
            var maxInstructionCode = instructions.Max(s => s.Code) + 1;
            array = new IInstruction[maxInstructionCode];

            foreach(var instruction in instructions)
            {
                array[instruction.Code] = instruction;
            }
        }

        /// <summary>
        /// Add a list of quarter code instructions to a list indexed based on quarter code
        /// </summary>
        /// <param name="instructions"></param>
        public InstructionList(IEnumerable<IQuarterCodeInstruction> instructions)
        {
            var maxInstructionCode = instructions.Max(s => s.QuarterCode) + 1;
            array = new IInstruction[maxInstructionCode];

            foreach (var instruction in instructions)
            {
                array[instruction.QuarterCode] = instruction;
            }
        }

        public IInstruction this[ushort code]
        {
            get
            {
                Debug.Assert(code < array.Length);

                return array[code];
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => array.GetEnumerator();

        public int Count => array.Length;
    }
}
