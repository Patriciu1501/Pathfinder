namespace Pathfinder {

    using System.Windows.Forms;

    class Formular {

        public Form formular;
        public Menu menu;
        public Map map;

        public Formular() {


            formular = new Form();
            menu = new Menu();
            formular.Controls.Add(menu.topBar);
            map = new Map(formular);
            formular.KeyPreview = true;
            formular.FormBorderStyle = FormBorderStyle.None;
            formular.WindowState = FormWindowState.Maximized;
            formular.Text = "Proiect POO - Pathfinder";
            formular.FormClosing += Events.formClose;

            Application.Run(formular);

        }
    }
}
