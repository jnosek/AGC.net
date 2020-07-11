namespace Apollo.Virtual.AGC.Instructions
{
    interface IInstruction
    {
        ushort Code { get; }
        
        void Execute(ushort K);
    }
}
