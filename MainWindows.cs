using System;
using Gtk;



public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
    }

    public String SelectedFilePath { get; set; }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnBtnSelectFileClicked(object sender, EventArgs e)
    {
        using (FileChooserDialog d =
        new FileChooserDialog(null,
                              "Выберите файл...",
                              null,
                              FileChooserAction.Open,
                              "Выбрать",
                              ResponseType.Accept,
                              "Закрыть",
                              ResponseType.Close))
        {
            if (d.Run() == (int)ResponseType.Accept)
            {
                SelectedFilePath = d.Filename;
                d.Destroy();
                btnCipher.Sensitive = true;
                btnDecipher.Sensitive = true;
            }
            else if (d.Run() == (int)ResponseType.Close)
            {
                d.Destroy();
            }
        }
        return;
    }
}
