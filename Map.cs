﻿using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;


namespace Pathfinder {

    static class Map {

        private const byte labelsRowNumber = 25;
        private const byte labelsColumnNumber = 31;

        private static readonly int labelHeight;
        private static readonly int labelWidth;

        public static readonly Color initialLabelColor;
        public static readonly Color obstacleColor;
        public static readonly Color searchColor;
        public static readonly Color searchColorBorder;
        public static readonly Color pathColor;
                                                      
        public static readonly Image sourceImage;
        public static readonly Image sourceSearchesImage;
        public static readonly Image sourcePath;
        public static readonly Image destinationImage;
        public static readonly Image destinationReachedImage;
        public static readonly Image weightInitialImage;
        public static readonly Image weightSearchedImage;
        public static readonly Image weightSearchedBorderImage;
        public static readonly Image weightPathImage;

        public static OOPLabel[,] labels;
        public static OOPLabel source, destination;
        public static bool sourceFlagAdded;
        public static bool destinationFlagAdded;
        public static bool weightedGraph;
        public static Cursor weightedCursor;

        public static bool buildMap;


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
            weightInitialImage = Image.FromFile(fullPath + "weightInitial.png");
            weightSearchedImage = Image.FromFile(fullPath + "weightSearched.png");
            weightSearchedBorderImage = Image.FromFile(fullPath + "weightSearchedBorder.png");
            weightPathImage = Image.FromFile(fullPath + "weightPath.png");

            labelHeight = (Window.height - Menu.topBar.Height) / labelsRowNumber;
            labelWidth = Window.width / labelsColumnNumber + 1;

            initialLabelColor = Color.FromArgb(179, 239, 255);
            obstacleColor = Color.FromArgb(40, 50, 90);
            searchColor = Color.FromArgb(103, 223, 255);
            searchColorBorder = Color.FromArgb(128, 196, 255);
            pathColor = Color.FromArgb(248, 255, 78);
            weightedCursor = new Cursor(fullPath + "weightedCursor.cur");

            labels = new OOPLabel[labelsRowNumber, labelsColumnNumber];
            int height = Menu.topBar.Height;

            for (int i = 0; i < labels.GetLength(0); i++) {

                if (i > 0) height += labelHeight;

                for (int j = 0; j < labels.GetLength(1); j++) {

                    labels[i, j] = new OOPLabel() { Visible = true, Width = labelWidth, Height = labelHeight, BackColor = initialLabelColor };
                    labels[i, j].Location = new Point(j > 0 ? labels[i, j - 1].Location.X + labels[i, j - 1].Width : 0, height);
                    labels[i, j].MouseMove += Events.labelMouseMove;
                    labels[i, j].Name = i + " " + j;
                    labels[i, j].MouseLeave += Events.labelMouseLeave;
                    labels[i, j].MouseDown += Events.labelMouseDown;
                    if(Window.gridOn) labels[i, j].Paint += Events.PaintGrid;
                    Window.form.Controls.Add(labels[i, j]);
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

