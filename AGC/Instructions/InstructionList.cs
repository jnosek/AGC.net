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
            var maxInstructionCode = instructions.Max(s => s.Code);
            array = new IInstruction[maxInstructionCode];

            foreach(var instruction in instructions)
            {
                array[instruction.Code] = instruction;
            }
        }

        /// <summary>
        /// Add a dictionary of instructions to a list indexed based on dictionary key code
        /// </summary>
        /// <param name="instructions"></param>
        public InstructionList(IDictionary<ushort, IInstruction> instructions)
        {
            var maxInstructionCode = instructions.Keys.Max();
            array = new IInstruction[maxInstructionCode];

            foreach (var keyValuePair in instructions)
            {
                array[keyValuePair.Key] = keyValuePair.Value;
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
