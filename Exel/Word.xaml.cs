using Microsoft.Win32;
using Spire.Doc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;


namespace Exel
{
    /// <summary>
    /// Логика взаимодействия для Word.xaml
    /// </summary>
    public partial class Word : Window
    {
        public Word()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Word documents (.docx)|*.docx";
            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;

                Document doc = new Document();
                doc.LoadFromFile(filePath);

                rtb.Document.Blocks.Clear();
                rtb.Document.Blocks.Add(new Paragraph(new Run(doc.GetText())));
            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "Word documents (.docx)|*.docx";
            if (saveFileDialog.ShowDialog() == true)
            {
                string filePath = saveFileDialog.FileName;

                Document doc = new Document();
                doc.AddSection().AddParagraph().AppendText(new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd).Text);
                doc.SaveToFile(filePath, FileFormat.Docx);
            }
        }

        private void Export()
        {
            TextRange range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            FileStream fs = new FileStream("", FileMode.Create);
            range.Save(fs, DataFormats.Rtf);
            fs.Close();
        }

        private void Import()
        {
            TextRange range = new TextRange(rtb.Document.ContentStart, rtb.Document.ContentEnd);
            FileStream fs = new FileStream("", FileMode.OpenOrCreate);
            range.Load(fs, DataFormats.Rtf);
            fs.Close();
        }


    }
}
