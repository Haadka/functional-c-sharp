using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using FizzWare.NBuilder;
using ListProcessing.Solution;

namespace ListProcessing
{
    partial class Program
    {
        static void Main(string[] args)
        {
            // Random data generator
            var inputSize = 70000;
            var randomData = Enumerable.Range(0, inputSize)
                .Select(i => Builder<JobCandidate>.CreateNew()
                .With(a => a.Value = i)
                .With(a => a.Age = 100)
                .With(a => a.Index = i)
                .With(a => a.References = Builder<Reference>.CreateListOfSize(30).Build().ToList())
                .Build());

            IHRSystem hrSystem = HRSystemFactory.Build(HRSystemType.LinqCo);

            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<JobCandidate, Employee>()).CreateMapper();

            (long, JobCandidate, IEnumerable<Employee>) result = hrSystem.ProcessApplications(randomData, mapper);

            if (ValidateResult(inputSize, result))
            {
                Console.WriteLine($"Approach passed  👍");
            }
        }

        private static bool ValidateResult(int inputSize, (long, JobCandidate, IEnumerable<Employee>) result)
        {
            return result.Item1 == 2449965000 && result.Item2.Index == 69999 &&
                result.Item3.Count() == inputSize && result.Item3.First().Index == 0 &&
                result.Item3.First().Value == -980107325;
        }
    }
}
