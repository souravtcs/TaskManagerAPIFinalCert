using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManagerAPI.Controllers;

namespace NBench
{
    public class CounterPerfSpecs
    {
        private TaskController task = new TaskController();

        [PerfBenchmark(NumberOfIterations = 1,
           RunMode = RunMode.Throughput,
           TestMode = TestMode.Test,
           SkipWarmups = true)]
        [ElapsedTimeAssertion(MaxTimeMilliseconds = 2000)]
        public void Add_Benchmark_Performance()
        {
            for (var i = 0; i < 100000; i++)
            {
                task.GetTasks();
            }
        }


    }
}
