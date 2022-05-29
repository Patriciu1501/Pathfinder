using Pathfinder.Algorithms;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pathfinder {
    class Tutorial {

        const int notificationDelay = 500;

        private readonly byte widthTutorialLabel;
        private readonly byte heightTutorialLabel;
        public OOPLabel tutorialStartLabel;
        public OOPLabel tutorialEventNotifier;
        public static Thread runningNotifier;
        public static bool speedModified;
        public static bool algorithmLaunched;
        public static bool destinationFound;


        public Tutorial() {

            tutorialEventNotifier = new OOPLabel() { Visible = false, Size = new Size(Window.width / 5, Window.height / 27) };
            tutorialEventNotifier.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            tutorialEventNotifier.BackColor = Color.Black;
            tutorialEventNotifier.ForeColor = Color.FromArgb(0, 207, 255);
            tutorialEventNotifier.Font = new Font("Nirmala UI", 13, FontStyle.Bold);
            speedModified = false;
            algorithmLaunched = false;
            destinationFound = false;







        }

        public void runNotifier() {

            while (true) {

                int nowTime = DateTime.Now.Second;


                while (Map.sourceFlagAdded == false) {

                    if (tutorialEventNotifier.Visible == false) {

                        tutorialEventNotifier.Visible = true;
                        tutorialEventNotifier.Text = "Place the source node";
                    }

                    Thread.Sleep(notificationDelay);
                    tutorialEventNotifier.Visible = false;
                    Thread.Sleep(notificationDelay);

                }

                while (Map.destinationFlagAdded == false) {

                    if (tutorialEventNotifier.Visible == false) {

                        tutorialEventNotifier.Visible = true;
                        tutorialEventNotifier.Text = "Place the destination node";
                    }


                    Thread.Sleep(notificationDelay);
                    tutorialEventNotifier.Visible = false;
                    Thread.Sleep(notificationDelay);

                }


                while(Algorithm.algorithmState == Algorithm.AlgorithmState.NeverFinished) {

                    if (tutorialEventNotifier.Visible == false) {

                        tutorialEventNotifier.Visible = true;
                        tutorialEventNotifier.Text = "Run any algorithm";
                    }


                    Thread.Sleep(notificationDelay);
                    tutorialEventNotifier.Visible = false;
                    Thread.Sleep(notificationDelay);
                }


                while (Algorithm.algorithmState == Algorithm.AlgorithmState.Finished && speedModified == false) {

                    if (tutorialEventNotifier.Visible == false) {

                        if (!destinationFound) tutorialEventNotifier.Text = "Destination not found";

                        else tutorialEventNotifier.Text = "Destination found";

                        tutorialEventNotifier.Visible = true;
                    }
                }


                while (speedModified == true) {

                    tutorialEventNotifier.Text = "Speed set to " + Algorithm.algorithmSpeed.ToString();
                    tutorialEventNotifier.Visible = true;
                    Thread.Sleep(notificationDelay);
                    tutorialEventNotifier.Visible = false;
                    speedModified = false;
                }


                while (algorithmLaunched == true) {

                    tutorialEventNotifier.Text = Algorithm.algorithmName.ToString() + " launched";
                    tutorialEventNotifier.Visible = true;
                    Thread.Sleep(notificationDelay);
                    tutorialEventNotifier.Visible = false;
                    algorithmLaunched = false;
                }
            }
        }

    }
}
