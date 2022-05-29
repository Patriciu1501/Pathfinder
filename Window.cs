namespace Pathfinder {
    
    using System.Windows.Forms;



    class Window {

        public static readonly int width = Screen.PrimaryScreen.Bounds.Width;
        public static readonly int height = Screen.PrimaryScreen.Bounds.Height;


        Form form;
        Menu menu;
        Map map;
      

        public Window() {

            form = new Form();
            menu = new Menu();
            form.Controls.Add(menu.topBar);
            new Map(form);
            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Maximized;
            form.Text = "Pathfinder by Bogatu Patriciu";
            form.FormClosing += Events.formClose;

            Tutorial.runningNotifier.Start();
            Application.Run(form);

        }
    }
}
