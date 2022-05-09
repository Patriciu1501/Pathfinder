using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Pathfinder {

    class Map {

        public const int heightForm = 21;
        public const int widthForm = 31;

        public static readonly Color initialLabelColor = Color.FromArgb(179, 239, 255);
        public static readonly Color obstacleColor = Color.FromArgb(40, 50, 90);
        public static readonly Color searchColor = Color.FromArgb(103, 223, 255);
        public static readonly Color searchColorBorder = Color.FromArgb(128, 196, 255);

        public static readonly Image sourceImage;
        public static readonly Image sourceSearchesImage;
        public static readonly Image destinationImage;
        public static readonly Image destinationReachedImage;

        public static Label[,] labeluri;
        public static Label source, destination;
        public static bool startFlagAdded;
        public static bool destinationFlagAdded;

        


        static Map() { // se preia locatia proiectului apoi se seteaza imaginile pentru sursa si destinatie

            string projectPath = @"location";
            string fullPath;

            fullPath = Path.GetFullPath(projectPath);
            fullPath = fullPath.Remove(fullPath.Length - projectPath.Length - 10);
            fullPath += @"Images\";
            sourceImage = Image.FromFile(fullPath + "start.png");
            sourceSearchesImage = Image.FromFile(fullPath + "startSearches.png");
            destinationImage = Image.FromFile(fullPath + "destination.png");
            destinationReachedImage = Image.FromFile(fullPath + "destinationReached.png");

        }

        public Map(Form formular) {

            
            labeluri = new Label[heightForm, widthForm];
            int height = 0;

            for (int i = 0; i < labeluri.GetLength(0); i++) {

                if (i > 0) height += 41;

                for (int j = 0; j < labeluri.GetLength(1); j++) {

                    labeluri[i, j] = new Label() { Visible = true, Width = 50, Height = 50, BackColor = initialLabelColor };
                    labeluri[i, j].Location = new Point(j > 0 ? labeluri[i, j - 1].Location.X + labeluri[i, j - 1].Width  : 0, height);
                    labeluri[i, j].MouseMove += Events.labelMouseMove;
                    labeluri[i, j].Name = i + " " + j;
                    labeluri[i, j].MouseLeave += Events.labelMouseLeave;
                    labeluri[i, j].MouseDown += Events.labelMouseDown;
                    formular.Controls.Add(labeluri[i, j]);
                }
            }
        }


        public static Tuple<int, int> getPos(string name) {

            Tuple<int, int> result;
            string[] splits = name.Split(' ');
            result = new Tuple<int, int>(Convert.ToInt32(splits[0]), Convert.ToInt32(splits[1]));

            return result;
        }
    }
}

