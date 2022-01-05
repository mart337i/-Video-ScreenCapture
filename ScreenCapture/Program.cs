using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using SharpAvi;

namespace ScreenCapture
{
    internal class Program
    {
        public static List<Options> options;
        public static void Main(string[] args)
        {
            options = new List<Options>
            {
                new Options("Manuel Rec",  ManuelRec),
                new Options("AutoRec", AutoRec),
                new Options("Exit", () => Environment.Exit(0)),
            };
            // Set the default index of the selected item to be the first
            int index = 0;
            
            // Write the menu out
            Writemenu(options, options[index]);
 
            ConsoleKeyInfo keyInfo;
            do
            {
                keyInfo = Console.ReadKey();

                try
                {
                    if (keyInfo.Key == ConsoleKey.DownArrow)
                    {
                        if (index + 1 >= 0)
                        {
                            index++;
                            Writemenu(options, options[index]);
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.UpArrow)
                    {
                        if (index - 1 >= 0)
                        {
                            index--;
                            Writemenu(options, options[index]);
                        }
                    }
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        options[index].Selected.Invoke();
                        index = 0;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (keyInfo.Key != ConsoleKey.X);

            Console.ReadKey();
        }

        static void ManuelRec()
        {
            var Record = new Recorder(new Capture("out.avi", 60, SharpAvi.KnownFourCCs.Codecs.MotionJpeg, 70));
            Console.WriteLine("Recording...");
            Console.ReadKey();
            Record.Dispose();
            Console.WriteLine("Recordding stoped");
            Thread.Sleep(200);
            Console.Clear();
        }

        static void AutoRec()
        {
            var moniter = new Moniter();

            foreach (var windowProcess in moniter.WindowProcesses())
            {
                if (windowProcess.ProcessName == "CSGO.exe")
                {
                    var Record = new Recorder(new Capture("out.avi", 60, KnownFourCCs.Codecs.MotionJpeg, 70));
                    while (Process.GetProcessesByName(windowProcess.ProcessName).ToString() == "CSGO.exe")
                    {
                       Console.WriteLine("recording");
                    }
                    Record.Dispose();
                    Console.WriteLine(Record);
     
                }
                else
                {
                    Console.WriteLine("no supported game is open");
                }
            }
        }

        static void Writemenu(List<Options> optionsList, Options selectedOption)
        {
            Console.Clear();

            foreach (Options option in optionsList)
            {
                if (option == selectedOption)
                {
                    Console.Write("> ");
                }
                else
                {
                    Console.Write(" ");
                }

                Console.WriteLine(option.Name);
            }
            
        }
        
    }

    public class Options
    {
        public string Name { get; }
        public Action Selected { get; }

        public Options(string name, Action selected)
        {
            Name = name;
            Selected = selected;
        }
    }
}