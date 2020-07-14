namespace Apollo.Virtual.AGC.Math
{
    /// <summary>
    /// https://github.com/rburkey2005/virtualagc/blob/master/yaAGC/agc_engine.c 
    /// </summary>
    public class DoublePrecision
    {
        public ushort MostSignificantWord { get; }
        public ushort LeastSignificantWord { get; }

        public DoublePrecision(ushort most, ushort least)
        {
            MostSignificantWord = most;
            LeastSignificantWord = least;
        }

        /// <summary>
        /// Double Precision Addition
        /// </summary>
        /// <param name="left">left operand</param>
        /// <param name="right">right operand</param>
        /// <returns></returns>
        public static DoublePrecision operator +(DoublePrecision left, DoublePrecision right)
        {
            // single preceision add the least significant word and most significant word
            ushort lsw = OnesCompliment.Add(left.LeastSignificantWord, right.LeastSignificantWord);
            ushort msw = OnesCompliment.Add(left.MostSignificantWord, right.MostSignificantWord);

            // check for overflow and adjust
            if (OnesCompliment.IsPositiveOverflow(lsw))
                msw = OnesCompliment.AddPositiveOne(msw);
            else if (OnesCompliment.IsNegativeOverflow(lsw))
                msw = OnesCompliment.AddNegativeOne(msw);

            // in case lsw is in overflow, correct it and extend it back to 16 bits
            
            // TODO: shoudl this be done with DP value is stored in memory?
            //lsw.OverflowCorrect();
            //lsw.SignExtend();

            return new DoublePrecision(msw, lsw);
        }
    }
}
