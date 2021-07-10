using System;
using System.IO;
using System.Text;
using System.Linq;
using Gtk;



public partial class MainWindow : Gtk.Window
{
    public MainWindow() : base(Gtk.WindowType.Toplevel)
    {
        Build();
    }

    public string SelectedFilePath; //путь к выбранному файлу
    public string SelectedSavePath; //выбранный путь сохранения
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
        status.Text = " ";
        return;
    }
    public uint shift = 14;
    protected void OnBtnCipherClicked(object sender, EventArgs e)
    {
        Random rnd = new Random();
        string s = ""; // строка шифрования
        string result = ""; // строка результата
        //Если величина сдвига больше длины алфавита кирилицы
        if (shift > 32)
            shift = shift % 32;
        status.Text = "Строка считывается из файла...";
        s = File.ReadAllText(SelectedFilePath, Encoding.Default);
        //Выполение шифрования
        //Цикл по каждому символу строки
        for (int i = 0; i < s.Length; i++)
        {
            //Если не кириллица
            if (((int)(s[i]) < 1040) || ((int)(s[i]) > 1103))
                result += s[i];
            //Если буква является строчной
            if ((Convert.ToInt16(s[i]) >= 1072) && (Convert.ToInt16(s[i]) <= 1103))
            {
                //Если буква, после сдвига выходит за пределы алфавита
                if (Convert.ToInt16(s[i]) + shift > 1103)
                    //Добавление в строку результатов символ
                    result += Convert.ToChar(Convert.ToInt16(s[i]) + shift - 32);
                //Если буква может быть сдвинута в пределах алфавита
                else
                    //Добавление в строку результатов символ
                    result += Convert.ToChar(Convert.ToInt16(s[i]) + shift);
            }
            //Если буква является прописной
            if ((Convert.ToInt16(s[i]) >= 1040) && (Convert.ToInt16(s[i]) <= 1071))
            {
                //Если буква, после сдвига выходит за пределы алфавита
                if (Convert.ToInt16(s[i]) + shift > 1071)
                    //Добавление в строку результатов символ
                    result += Convert.ToChar(Convert.ToInt16(s[i]) + shift - 32);
                //Если буква может быть сдвинута в пределах алфавита
                else
                    //Добавление в строку результатов символ
                    result += Convert.ToChar(Convert.ToInt16(s[i]) + shift);
            }
        }
        status.Text = "Строка зашифрована успешно";
        using (FileChooserDialog d =
        new FileChooserDialog(null,
                              "Выберите путь сохранения...",
                              null,
                              FileChooserAction.Open,
                              "Выбрать",
                              ResponseType.Accept,
                              "Закрыть",
                              ResponseType.Close))
        {
            if (d.Run() == (int)ResponseType.Accept)
            {
                SelectedSavePath = d.Filename;
                d.Destroy();
            }
            else if (d.Run() == (int)ResponseType.Close)
            {
                d.Destroy();
            }
        }
        string path = SelectedSavePath + " Ciphered.txt";
        File.Create("Ciphered.txt");
        File.WriteAllText(path, result);
        btnCipher.Sensitive = false;
        btnDecipher.Sensitive = false;
    }

    protected void OnBtnDecipherClicked(object sender, EventArgs e)
    {
        status.Text = "Строка считывается из файла...";
        StreamReader sr = new StreamReader(SelectedFilePath);
        string s = sr.ReadToEnd();
        string result = "";
        sr.Close();
        //Цикл по каждому символу строки
        for (int i = 0; i < s.Length; i++)
        {
            if (Convert.ToInt16(s[i]) == 32)
                result += ' ';
            //Если буква является строчной
            if ((Convert.ToInt16(s[i]) >= 1072) && (Convert.ToInt16(s[i]) <= 1103))
            {
                //Если буква, после сдвига выходит за пределы алфавита
                if (Convert.ToInt16(s[i]) - shift < 1072)
                    //Добавление в строку результатов символ
                    result += Convert.ToChar(Convert.ToInt16(s[i]) - shift + 32);
                //Если буква может быть сдвинута в пределах алфавита
                else
                    //Добавление в строку результатов символ
                    result += Convert.ToChar(Convert.ToInt16(s[i]) - shift);
            }
            //Если буква является прописной
            if ((Convert.ToInt16(s[i]) >= 1040) && (Convert.ToInt16(s[i]) <= 1071))
            {
                //Если буква, после сдвига выходит за пределы алфавита
                if (Convert.ToInt16(s[i]) - shift < 1040)
                    //Добавление в строку результатов символ
                    result += Convert.ToChar(Convert.ToInt16(s[i]) - shift + 32);
                //Если буква может быть сдвинута в пределах алфавита
                else
                    //Добавление в строку результатов символ
                    result += Convert.ToChar(Convert.ToInt16(s[i]) - shift);
            }
        }
        status.Text = "Строка дешифрована успешно";
        using (FileChooserDialog d =
        new FileChooserDialog(null,
                              "Выберите путь сохранения...",
                              null,
                              FileChooserAction.Open,
                              "Выбрать",
                              ResponseType.Accept,
                              "Закрыть",
                              ResponseType.Close))
        {
            if (d.Run() == (int)ResponseType.Accept)
            {
                SelectedSavePath = d.Filename;
                d.Destroy();
            }
            else if (d.Run() == (int)ResponseType.Close)
            {
                d.Destroy();
            }
        }
        string path = SelectedSavePath + " Deciphered.txt";
        File.Create("Deciphered.txt");
        File.WriteAllText(path, result);
        btnCipher.Sensitive = false;
        btnDecipher.Sensitive = false;
    }
}