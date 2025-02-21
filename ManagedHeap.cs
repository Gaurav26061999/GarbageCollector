using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualGarbageCollector.GC
{
    public class ManagedHeap
    {
        private List<ManagedObject> _objects = new List<ManagedObject>();

        public ManagedObject Allocate(int id)
        {
            var obj = new ManagedObject(id);
            _objects.Add(obj);
            return obj;
        }

        public List<ManagedObject> GetObjects() => _objects;

        public void DeAllocate(ManagedObject obj)
        {
            _objects.Remove(obj);
        }

        public void CollectGarbage(List<ManagedObject> roots)
        {
           
            // Mark phase: Mark reachable objects from roots as this shouldn't be removed
            HashSet<ManagedObject> markedObjects = new HashSet<ManagedObject>();
            MarkRoots(roots, markedObjects);

            // Sweep phase: Remove unreachable objects from the heap
            Sweep(markedObjects);
        }

        private void MarkRoots(List<ManagedObject> roots, HashSet<ManagedObject> markedObjects)
        {
            foreach (var root in roots)
            {
                Mark(root, markedObjects);
            }
        }

        private void Mark(ManagedObject obj, HashSet<ManagedObject> markedObjects)
        {
            if (markedObjects.Contains(obj))
                return;

            markedObjects.Add(obj);

            foreach (var refObj in obj.References)
            {
                Mark(refObj, markedObjects);
            }
        }

        private void Sweep(HashSet<ManagedObject> markedObjects)
        {
            _objects.RemoveAll(obj => !markedObjects.Contains(obj));
        }
    }
}
