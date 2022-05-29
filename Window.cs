namespace Pathfinder {
    
    using System.Windows.Forms;


    static class Window {

        public static readonly int width = Screen.PrimaryScreen.Bounds.Width;
        public static readonly int height = Screen.PrimaryScreen.Bounds.Height;
        public static bool gridOn = false;

        public static Form form;

        public static bool buildWindow;
      
        static Window() {

            form = new Form();
            form.Controls.Add(Menu.topBar);
            DialogResult gridResult = MessageBox.Show("Turn grid on?", "Grid activator", MessageBoxButtons.YesNo);
            if (gridResult == DialogResult.Yes) gridOn = true;

            Map.buildMap = true; 
            
            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Maximized;
            form.Text = "Pathfinder by Bogatu Patriciu";
            form.FormClosing += Events.formClose;
            
            Tutorial.runningNotifier.Start();
            Application.Run(form);

        }
    }
}
