using System;
using System.Collections.Generic;
using AutoMapper;

namespace ListProcessing
{
    class OneLoopSystem : IHRSystem
    {
        public (long, JobCandidate, IList<Employee>) ProcessApplications(IEnumerable<JobCandidate> candidates, IMapper mapper)
        {
            long sum = 0;
            var employeeList = new SortedList<int, Employee>();
            JobCandidate lastItem = null;
            foreach (var item in candidates)
            {
                sum += item.Value;
                var newItem = mapper.Map<Employee>(item);
                newItem.ComputeFibonacci();
                employeeList.Add(item.Index, newItem);
                lastItem = item;
            };
            return (sum, lastItem, employeeList.Values);
        }
    }
}
