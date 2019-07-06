using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace ListProcessing
{
    class ParallelSystems : IHRSystem
    {
        public (long, JobCandidate, IList<Employee>) ProcessApplications(IEnumerable<JobCandidate> input, IMapper mapper)
        {
            var candidateList = input.ToList();
            var employeeSet = new ConcurrentBag<Employee>();

            var sum = candidateList.Select(r => r.Value).Sum();
            var lastItem = candidateList.LastOrDefault();

            candidateList.AsParallel().ForAll(item => {
                var newItem = mapper.Map<Employee>(item);
                newItem.ComputeFibonacci();
                employeeSet.Add(newItem);
            });

            var employeeList = employeeSet.ToList();
            employeeList.Sort();
            return (sum, lastItem, employeeList);
        }
    }
}
