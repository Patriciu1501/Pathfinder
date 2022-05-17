namespace Pathfinder {

    using System.Windows.Forms;

    class Window {

        public Form form;
        public Menu menu;
        public Map map;

        public Window() {


            form = new Form();
            menu = new Menu();
            form.Controls.Add(Menu.topBar);
            map = new Map(form);
            form.KeyPreview = true;
            form.FormBorderStyle = FormBorderStyle.None;
            form.WindowState = FormWindowState.Maximized;
            form.Text = "Proiect POO - Pathfinder";
            form.FormClosing += Events.formClose;

            Application.Run(form);

        }
    }
}
