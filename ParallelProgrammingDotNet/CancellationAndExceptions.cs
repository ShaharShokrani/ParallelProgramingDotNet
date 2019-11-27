using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelLinq
{
    public static class CancellationAndExceptions
    {
        public static void Do()
        {
            var cts = new CancellationTokenSource();
            var items = Enumerable.Range(1, 20);

            var results = items.AsParallel().WithCancellation(cts.Token).Select(i =>
            {
                double result = Math.Log10(i);                
                return result;
            });
            
            try
            {
                foreach (var c in results)
                {
                    if (c > 1)
                        cts.Cancel();
                    Console.WriteLine($"result = {c}");
                }
            }
            catch (OperationCanceledException e)
            {
                if (cts.IsCancellationRequested)
                    Console.WriteLine($"Canceled");
            }
            catch (AggregateException ae)
            {
                ae.Handle(e =>
                {
                    Console.WriteLine($"{e.GetType().Name}: {e.Message}");
                    return true;
                });
            }
        }
    }
}