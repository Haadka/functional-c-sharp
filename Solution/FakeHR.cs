using System;
using System.Collections.Generic;
using AutoMapper;

namespace ListProcessing
{
    class FakeHR : IHRSystem
    {
        public (long, JobCandidate, IList<Employee>) ProcessApplications(IEnumerable<JobCandidate> input, IMapper mapper)
        {
            foreach (var item in input)
            {

            }
            return (0, null, new List<Employee>());
        }
    }
}
