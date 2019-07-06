using System.Collections.Generic;
using AutoMapper;

namespace ListProcessing
{
    internal interface IHRSystem
    {
        (long, JobCandidate, IList<Employee>) ProcessApplications(IEnumerable<JobCandidate> input, IMapper mapper);
    }

    public enum HRSystemType {
        FakeHR,
        OneLoop,
        LinqCo,
        Parallel
    }
}
