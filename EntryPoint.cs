
using System;
using System.IO;

namespace Pathfinder {
    static class EntryPoint {

        public static StreamWriter fisier;

        static void Main() {

            fisier = new StreamWriter(@"D:\repozitoriu\Patriciu1501\Pathfinder\Exceptions.txt", false);
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
            Window.buildWindow = true;

        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {

            using (fisier) {

                Exception ex = (Exception)e.ExceptionObject;
                fisier.WriteLine(ex.ToString() + "\n\n");
            }

        }
    }
}