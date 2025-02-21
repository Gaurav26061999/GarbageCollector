using System;
using System.Collections.Generic;
using VirtualGarbageCollector.GC;

public class Program
{
    static void Main(string[] args)
    {
        // Create a heap and generational GC
        ManagedHeap heap = new ManagedHeap();
        GenerationalGC generationalGC = new GenerationalGC(heap);

        // Create root objects and temporary objects
        var root1 = heap.Allocate(1);
        var root2 = heap.Allocate(2);
        var temp1 = heap.Allocate(3); // temp1 will be promoted to Gen 1 from Gen 0
        var temp2 = heap.Allocate(4); // temp2 will be also promoted to Gen 1
        var temp3 = heap.Allocate(5);  // temp3 is not been reference by any root so it will be removed by GC

        // Add references for object temp1 and temp2
        root1.AddReference(temp1);
        root2.AddReference(temp2);
      

        // Allocate the objects into the generational GC system
        generationalGC.AllocateAndAddToGeneration(root1, 0);
        generationalGC.AllocateAndAddToGeneration(root2, 0);
        generationalGC.AllocateAndAddToGeneration(temp1, 0);
        generationalGC.AllocateAndAddToGeneration(temp2, 0);
        generationalGC.AllocateAndAddToGeneration(temp3, 0);

        // Print status before GC
        Console.WriteLine("Before GC:");
        Console.WriteLine($"Heap size: {heap.GetObjects().Count} objects");

        foreach (var obj in heap.GetObjects())
        {
            obj.PrintReferences();
        }
        Console.WriteLine("-----------------------");

        Console.WriteLine("Running first GC...");
        List<ManagedObject> roots = new List<ManagedObject> { root1, root2 };

        // we are deallocating root 1 and root2 object from heap now reference of root1(temp1) and root2(temp2) wil be also removed by the GC 
        heap.DeAllocate(root2);
        heap.DeAllocate(root1);
        generationalGC.CollectGarbage(roots);


        Console.WriteLine("After first GC:");
        Console.WriteLine($"Heap size: {heap.GetObjects().Count} objects");
        foreach (var obj in heap.GetObjects())
        {
            obj.PrintReferences();
        }

        Console.WriteLine("-----------------------");
        Console.WriteLine("Running second GC...");
        generationalGC.CollectGarbage(new List<ManagedObject>());

        Console.WriteLine("After second GC:");
        Console.WriteLine($"Heap size: {heap.GetObjects().Count} objects");
        foreach (var obj in heap.GetObjects())
        {
            obj.PrintReferences();
        }
        Console.WriteLine("-----------------------");
    }
}
