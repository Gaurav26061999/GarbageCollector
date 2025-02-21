//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace VirtualGarbageCollector.GC
//{
//    public class CustomBackgroundGC
//    {
//        private ManagedHeap _heap;
//        private List<ManagedObject> _roots;
//        private CancellationTokenSource _cancellationTokenSource;
//        private Task _gcTask;

//        public CustomBackgroundGC(ManagedHeap heap, List<ManagedObject> roots)
//        {
//            _heap = heap;
//            _roots = roots;
//            _cancellationTokenSource = new CancellationTokenSource();
//        }

//        public void StartGC()
//        {
//            // Start GC in a background task/thread
//            _gcTask = Task.Run(() => BackgroundGarbageCollection(_cancellationTokenSource.Token));
//        }

//        private void BackgroundGarbageCollection(CancellationToken cancellationToken)
//        {
//            while (!cancellationToken.IsCancellationRequested)
//            {
//                // Perform garbage collection periodically (e.g., every 5 seconds)
//                Console.WriteLine("Performing background garbage collection...");
//                _heap.CollectGarbage(_roots);

//                
//                Thread.Sleep(5000); 
//            }
//        }

//        public void StopGC()
//        {
//            // Cancel the background GC thread/task
//            _cancellationTokenSource.Cancel();
//            _gcTask.Wait(); // Ensure the GC task completes before exiting
//        }
//    }
//}
