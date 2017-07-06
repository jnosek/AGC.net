using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Apollo.Virtual.AGC.Core
{
    class InstructionList
    {
        private IInstruction[] array;

        public Processor CPU { get; set; }

        public InstructionList(int count)
        {
            array = new IInstruction[0];
        }

        public InstructionList()
            : this(0)
        {
        }

        protected void Add(IInstruction instruction)
        {
            instruction.CPU = CPU;
            this[instruction.Code] = instruction;
        }

        protected void Add(ExtraInstructionList list)
        {
            this[list.Code] = list;
        }

        public IInstruction this[ushort code]
        {
            get
            {
                if (code > array.Length - 1)
                    throw new IndexOutOfRangeException();

                return array[code];
            }
            set
            {
                if(code > array.Length - 1)
                Array.Resize(ref array, code + 1);

                array[code] = value;
            }
        }

        public void Clear()
        {
            array = new IInstruction[0];
        }

        public int Count
        {
            get { return array.Length; }
        }
    }
}
