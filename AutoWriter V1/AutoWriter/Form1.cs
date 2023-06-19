using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



using Microsoft.Speech.Recognition;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace AutoWriter
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Accuracy.accuracy = 40;
        }


        //AutoWriter

        private void AutoWriter_Shown(object sender, EventArgs e)
        {
            AutoWriter();
        }

        private void AutoWriter()
        {
            l = label1;

            System.Globalization.CultureInfo ci = new System.Globalization.CultureInfo("ru-ru");
            SpeechRecognitionEngine sre = new SpeechRecognitionEngine(ci);
            sre.SetInputToDefaultAudioDevice();
            //sre.SetInputToAudioStream();
            sre.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(sre_SpeechRecognized);


            Choices numbers = new Choices();
            numbers.Add(new string[]
            { "пробел","удалить","очистить всё",
              "вопросительный знак","восклицательный знак","запятая","точка",
              "1","2","3","4","5","6","7","8","9","0",
              "+","-","=",
              "минус","равно",
              "а", "б", "в", "г",
              "д", "е", "ё", "ж", "з", "и",
              "й", "к", "л", "м", "н", "о",
              "п", "р", "с", "т", "у", "ф",
              "х", "ц", "ч", "щ", "ъ", "ы",
              "ь", "э", "ю", "я", });


            GrammarBuilder gb = new GrammarBuilder();
            gb.Culture = ci;
            gb.Append(numbers);


            Grammar g = new Grammar(gb);
            sre.LoadGrammar(g);

            sre.RecognizeAsync(RecognizeMode.Multiple);
        }

        static Label l;

        static void sre_SpeechRecognized(object sender, SpeechRecognizedEventArgs e)
        {
            if (e.Result.Confidence > 0.01 *Accuracy.accuracy)
            {

                switch (e.Result.Text)
                {
                    case "пробел":
                        l.Text += " ";
                        return;

                    case "удалить":
                        l.Text = l.Text.Remove(l.Text.Length - 1);
                        return;

                    case "очистить всё":
                        l.Text = "";
                        return;

                    case "вопросительный знак":
                        l.Text += "?";
                        return;

                    case "восклицательный знак":
                        l.Text += "!";
                        return;

                    case "запятая":
                        l.Text += ",";
                        return;

                    case "точка":
                        l.Text += ".";
                        return;

                    case "минус":
                        l.Text += "-";
                        return;

                    case "равно":
                        l.Text += "=";
                        return;

                    //case "":
                    //    return;

                }

                l.Text += e.Result.Text;


            }
        }

        //Точнность 
        private void Sensitivity_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 f = new Form2();
            f.ShowDialog();
        }

        // открытие файла
        private void Open_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|All files (*.*)|*.*";

            // Вывести диалог открытия фала
            openFileDialog1.ShowDialog();
            if (openFileDialog1.FileName == String.Empty) return;

            // Чтение текстового файла:
            try
            {
                // Создание экземпляра StreamReader для чтения из файла
                var Read = new System.IO.StreamReader(
                openFileDialog1.FileName,
                Encoding.GetEncoding(1251));
                // - здесь заказ кодовой страницы Win1251 для русских букв
                label1.Text = Read.ReadToEnd();
                Read.Close();
            }
            catch (System.IO.FileNotFoundException Ситуация)
            {
                MessageBox.Show(Ситуация.Message + "\nНет такого файла",
                "Ошибка", MessageBoxButtons.OK,
                MessageBoxIcon.Exclamation);
            }
            catch (Exception Ситуация)
            { // Отчет о других ошибках
                MessageBox.Show(Ситуация.Message, "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        // Сохранение файла
        private void Save_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Filter = "Текстовые файлы (*.txt)|*.txt|All files (*.*)|*.*";

            if (saveFileDialog1.ShowDialog() != DialogResult.OK)
            {
                return;
            }
                
            try
            {
                // Создание экземпляра StreamWriter для записи в файл:
                var weiter = new System.IO.StreamWriter(
                saveFileDialog1.FileName, false,
                System.Text.Encoding.GetEncoding(1251));
                // - здесь заказ кодовой страницы Win1251 для русских букв
                weiter.Write(label1.Text);
                weiter.Close();
            }
            catch (Exception Ситуация)
            {
                // Отчет обо всех возможных ошибках
                MessageBox.Show(Ситуация.Message, "Ошибка",
                MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void Restart_ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AutoWriter();
        }
    }

    // Точность в числе
    static class Accuracy
    {
        public static int accuracy { get; set; }
    }

}
