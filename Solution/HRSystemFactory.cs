using System.Collections.Generic;

namespace ListProcessing.Solution
{
    public static class HRSystemFactory
    {
        static Dictionary<HRSystemType, IHRSystem> Systems => new Dictionary<HRSystemType, IHRSystem> {
            {HRSystemType.FakeHR, new FakeHR()},
            {HRSystemType.OneLoop, new OneLoopSystem()},
            {HRSystemType.LinqCo, new LinqCo()},
            {HRSystemType.Parallel, new ParallelSystems()},
        };

        internal static IHRSystem Build(HRSystemType hrSystemType)
        {
            return Systems.GetValueOrDefault(hrSystemType);
        }
    }
}
