using System;
using System.IO;


namespace Pathfinder {
    static class EntryPoint {

        public static StreamWriter fisier;

        static void Main() {

            fisier = new StreamWriter(@"C:\Users\bogat\source\repos\Pathfinder\ExceptionsLogs.txt", false);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Formular aplicatie = new Formular();

        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {

            using (fisier) {

                Exception ex = (Exception)e.ExceptionObject;
                fisier.WriteLine(ex.ToString() + "\n\n");
            }

        }
    }
}