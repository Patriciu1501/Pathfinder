using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Pathfinder {

    class Map {

        public const int heightForm = 21;
        public const int widthForm = 37;

        public static readonly Color initialLabelColor = Color.FromArgb(179, 239, 255);
        public static readonly Color obstacleColor = Color.FromArgb(40, 50, 90);
        public static readonly Color searchColor = Color.FromArgb(103, 223, 255);
        public static readonly Color searchColorBorder = Color.FromArgb(128, 196, 255);
        public static readonly Color pathColor = Color.FromArgb(248, 255, 78);

        public static readonly Image sourceImage;
        public static readonly Image sourceSearchesImage;
        public static readonly Image sourcePath;
        public static readonly Image destinationImage;
        public static readonly Image destinationReachedImage;

        public static Label[,] labels;
        public const string labelCost = "0";
        public static Label source, destination;
        public static bool sourceFlagAdded;
        public static bool destinationFlagAdded;


        static Map() { 

            string projectPath = @"location";
            string fullPath;

            fullPath = Path.GetFullPath(projectPath);
            fullPath = fullPath.Remove(fullPath.Length - projectPath.Length - 10);
            fullPath += @"Images\";
            sourceImage = Image.FromFile(fullPath + "source.png");
            sourceSearchesImage = Image.FromFile(fullPath + "sourceSearches.png");
            sourcePath = Image.FromFile(fullPath + "sourcePath.png");
            destinationImage = Image.FromFile(fullPath + "destination.png");
            destinationReachedImage = Image.FromFile(fullPath + "destinationReached.png");

        }

        public Map(Form form) {

            
            labels = new Label[heightForm, widthForm];
            int height = Menu.topBar.Height;


            for (int i = 0; i < labels.GetLength(0); i++) {

                if (i > 0) height += 40;

                for (int j = 0; j < labels.GetLength(1); j++) {

                    labels[i, j] = new Label() { Visible = true, Width = 45, Height = 40, BackColor = initialLabelColor };
                    labels[i, j].Location = new Point(j > 0 ? labels[i, j - 1].Location.X + labels[i, j - 1].Width  - 3: 0, height);
                    labels[i, j].MouseMove += Events.labelMouseMove;
                    labels[i, j].Name = i + " " + j + " " + labelCost;
                    labels[i, j].MouseLeave += Events.labelMouseLeave;
                    labels[i, j].MouseDown += Events.labelMouseDown;
                    form.Controls.Add(labels[i, j]);
                }
            }
        }


        public static Tuple<int, int> GetPos(string name) {

            Tuple<int, int> result;
            string[] splits = name.Split(' ');
            result = new Tuple<int, int>(Convert.ToInt32(splits[0]), Convert.ToInt32(splits[1]));

            return result;
        }
    }
}

