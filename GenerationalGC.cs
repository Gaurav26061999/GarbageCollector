using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualGarbageCollector.GC
{
    public class GenerationalGC
    {
        private ManagedHeap _heap;
        private Dictionary<int, int> _objectGeneration; 
        private const int Gen0 = 0;
        private const int Gen1 = 1;
        private const int Gen2 = 2;

        public GenerationalGC(ManagedHeap heap)
        {
            _heap = heap;
            _objectGeneration = new Dictionary<int, int>();
        }

        public void AllocateAndAddToGeneration(ManagedObject obj, int generation)
        {
            
            _objectGeneration[obj.ID] = generation;
        }

        public void CollectGarbage(List<ManagedObject> roots)
        {
            Console.WriteLine("Collecting Garbage...");
            _heap.CollectGarbage(roots);

            PromoteObjects();
        }

        private void PromoteObjects()
        {
            List<ManagedObject> objectsToPromote = new List<ManagedObject>();
            foreach (var obj in _heap.GetObjects())
            {
                if (_objectGeneration.ContainsKey(obj.ID) && _objectGeneration[obj.ID] == Gen0)
                {
                    // moved object from Gen0 to Gen1
                    _objectGeneration[obj.ID] = Gen1;
                    objectsToPromote.Add(obj);
                }
                else if (_objectGeneration.ContainsKey(obj.ID) && _objectGeneration[obj.ID] == Gen1)
                {
                    // movec object from Gen1 to Gen2
                    _objectGeneration[obj.ID] = Gen2;
                    objectsToPromote.Add(obj);
                }
            }

            Console.WriteLine($"Objects promoted to Gen1: {objectsToPromote.Count}");
        }
    }
}

