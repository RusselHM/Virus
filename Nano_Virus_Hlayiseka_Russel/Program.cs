using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Nano_Virus_Hlayiseka_Russel
{
    class Program
    {
        static void Main(string[] args)
        { 

            CellContainer container = CellContainer.ContainerInstance;
         
            bool complete = false;
            while (!complete)
            {
                container.CycleNo++;
                container.DestroyCellsOrNot();

                if (container.CycleNo % 5 == 0)
                    container.MoveToCells();

                if (container.TumourousCells.Count == 0 || (container.RedBloodCells.Count + container.WhiteBloodCells.Count) == 0)
                    complete = true;

                Console.WriteLine(container.ToString());

                SaveToFile.WriteToFile(container.CycleNo);

            }
            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();


        }
    }
}
