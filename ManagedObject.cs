using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VirtualGarbageCollector.GC
{
    public class ManagedObject
    {
        public int ID { get; set; }
        public List<ManagedObject> References { get; set; }

        public ManagedObject(int id)
        {
            ID = id;
            References = new List<ManagedObject>();
        }

        public void AddReference(ManagedObject obj)
        {
            References.Add(obj);
        }
        public void DeReference(ManagedObject obj)
        {
            References.Remove(obj);
        }
        public void PrintReferences()
        {
            Console.WriteLine($"Object {ID} has {References.Count} references.");
        }
    }
}

