# C# : Functional Paradigm vs One Loop.

Does a modern C# functional paradigm perform as fast as code that has a single-iteration (via foreach/for)?

To answer that, I conducted the following experiment: an input of 70,000 IEnumerable objects will be processed in 3 different ways (implementations):

- With Single for-each loop
- With C# Linq to objects
- With PLINQ and concurrent collections (using tasks/ threads underneath)

To compare the performance of these, I used Visual studio profiler. The processing steps that each approach has to implement are as follows:

- Calculate the sum of a property in each list item
- Transform (map) all items from one type to another
- Compute Fibonacci of 100 for each item
- Fetch the last item in the input list
- Sort the input list based on one property

To give the experiment a context, the program input is regarded as a list of job candidates. The 3 approaches are seen as three HR systems. Each system has to give the same output which is a list of employee (having been processed)

## Approach #1 
## One-Loop System:
```
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
```

The argument for one loop is that data will be iterated once only. To avoid sorting in the end, a `SortedList` is used to perform online sorting.

The other way would be using Linq

## Approach #2
## Linq-Co:

```
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
```

The rationale behind this approach is that code readability is important and each statement should have clear intent. This style is therefore more maintainable. The `ToList` ensures that an enumerations is not executed many times. And since the collection fits into memory, it is hard to reason how a for-loop of a high-level language affects CPU/memory interaction.

## Approach #3
## ParallelSystems:

Since we have some calculations and many data points, using parallel task library could possibly speed the overall process.

```
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
```

# Results 

Using the free Visual Studio Profiler, we can compare the three approaches and also a fake Implementation which does nothing but iteration over the input data. This can be our baseline.  

For each approach, I have executed the profiler three time and took the average. The memory usage (heap) has been recorded as well:

| Approach | Average Total Time (s) | CPU time (ms) for the implementation  | Memory (heap)|
| ------------- | ------------- |----------|--------------|
| Fake HR| 8.98 | 5431 | 285 KB|
| One Loop | 10.06 | 6740 | 217 MB |
| Linq | 9.84 | 6685 | 216 MB 
| Parallel | 10.50 | 6725 | 217 MB

# Second Experiment Results:

The fist experiment shows that Parallel did not perform well. This is due to the processing logic being mostly IO bound operations. To test the case for CPU bound operation, I have run the experiment again but with a higher number in the Fibonacci calculations (1,000,000 instead of 100)

The results are as follows:

| Approach | Average Total Time (s) |
| ------------- | ------------- |
| Fake HR| 8.56 |
| One Loop | 85.49 |
| Linq | 54.02 |
| Parallel | 26.33 |

# Summary

As you can see above, the result speak for itself. There is no single advantage in the one-loop style. It reduces the readability without any added performance benefits in IO bound operations. In CPU bound operations, one-loop style performs very badly.

This repo contains all the code and results for the experiment. Please feel free to contribute and share your take on this issue.

