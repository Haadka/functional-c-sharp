#functional-c-sharp
## Using Functional Paradigm in C#, or just foreach?

The goal here is to show how functional programming paradigm in modern C# can outperforme a coding style that uses single-iteration (via foreach/for) to get a better optimization.

To show that we conduct the following experiment: I will process an input of `IEnumerable` items of objects in two different ways and using Visual studio profiler.

I choose a complicated input but a simple processing. This fits the nature of many Line of Business applications. So, the processing is: 

- Calculate the sum of a property in each time
- Store the a copy of the list in a new list
- Fetch the first item in the list

One way is to write a single for-each:
```
            var newRandomData = new List<Poco>();
            long sum = 0; 
            Poco firstItem = null;

            foreach (var item in randomData)
            {
                sum = item.Value;
                newRandomData.Add(item);
                if(firstItem == null)
                {
                    firstItem = item;
                }
            };
```

The argument here is that we have only a single iteration over the IEnumerable set.

The other way would be

```
            var randomDataList = randomData.ToList();
            var newRandomData = randomDataList.Select(r => r).ToList();
            var sum = randomDataList.Select(r => r.Value).Sum();
            var firstItem = randomDataList.FirstOrDefault();
```

That is 50% less code and it does the same thing.

It is a simple use case but imagine putting such limitation (single foreach loop) on every list in your application. It will give the program a low-level-code feel. It will lead developers to sometime couple pieces of logic that do not belong together. 

But is is worth it?

Using the free Visual Studio Profiler, we can compare the two approaches with sizable data.  

Here is the input to our program:

```
            var randomData = Enumerable.Range(0, inputSize)
                .Select(i => new Poco(Guid.NewGuid(), ComputeFibonacci(1000  i)))
                .OrderBy(r => r.Id).ThenByDescending(r => r.Value).AsEnumerable();
```

The reason for this value is to test if a method handles lazy loading properly: without multiple executions.



## Experiment Results:
|


To take this further, since our code is made of non I/O bound operations, we can use PLINQ to speed things up.


# Profiler Screen analysis
