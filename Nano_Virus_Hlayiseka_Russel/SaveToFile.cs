using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Nano_Virus_Hlayiseka_Russel
{
    class SaveToFile
    {
        //Path to the MyDocuments folder of the current user
        private static string directory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\Nano Virus";
        private static string fileName = string.Format("{0}\\NanoVirusSimulation {1}.txt", directory, DateTime.Now.ToString("yyyy.MM.dd HH.mm.ss"));

        public static void CreateDirectory()
        {
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
        }

        static SaveToFile()
        {
            CreateDirectory();
        }

        public static void WriteToFile(int cycleNumber)
        {

            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None))
                {
                    CellContainer container = CellContainer.Container;

                    StreamWriter writer = new StreamWriter(fs);

                    writer.WriteLine(container.ToString());

                    writer.Close();
                    fs.Close();
                }
                Console.Write("File has been created and the Path is D:\\Nano.txt");
                Console.ReadKey();
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

    }
}
