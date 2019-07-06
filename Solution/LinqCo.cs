using System.Collections.Generic;
using System.Linq;
using AutoMapper;

namespace ListProcessing
{
    class LinqCo : IHRSystem
    {
        public (long, JobCandidate, IList<Employee>) ProcessApplications(IEnumerable<JobCandidate> input, IMapper mapper)
        {
            var candidateList = input.ToList();

            var sum = candidateList.Select(r => r.Value).Sum();
            var lastItem = candidateList.LastOrDefault();

            var employeeList = candidateList.Select(item => {
                var newItem = mapper.Map<Employee>(item);
                newItem.ComputeFibonacci();
                return newItem;
            }).ToList();

            employeeList.Sort();
            return (sum, lastItem, employeeList);
        }
    }
}
