using Gtk;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
public partial class MainWindow : Gtk.Window
{
    public class Cipher
    {
        public String SelectedFilePath { get; set; }
    }
    Cipher pc_cphr = new Cipher();
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
    }

    protected void OnDeleteEvent(object sender, DeleteEventArgs a)
    {
        Application.Quit();
        a.RetVal = true;
    }

    protected void OnBtnSelectFilelicked(object sender, EventArgs e)
    {
        status.Text = "";
        btnDecipher.Sensitive = false;
        btnCipher.Sensitive = false;
        FileChooserDialog d = new FileChooserDialog("Выберите файл", null, FileChooserAction.SelectFolder, "Выход", ResponseType.Cancel, "Выбрать", ResponseType.Accept);
        if (d.Run() == (int)ResponseType.Accept)
        {
            pc_cphr.SelectedFilePath = d.Filename;
        }
        d.Destroy();
    }
}

