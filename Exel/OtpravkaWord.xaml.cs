using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Net;
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
using System.Xml.Linq;

namespace Exel
{
    
    public partial class OtpravkaWord : Window
    {
        private string selectedFilePath;
        public OtpravkaWord()
        {
            InitializeComponent();
        }

        

        private void otpravka(object sender, RoutedEventArgs e)
        {
            TextRange range = new TextRange(MessageRtb.Document.ContentStart, MessageRtb.Document.ContentEnd);
            MailMessage message = new MailMessage(From.Text, To.Text, Subject.Text, range.Text)
            {
                IsBodyHtml = true
            };

            if (!string.IsNullOrEmpty(selectedFilePath))
            {
                FileInfo fileInfo = new FileInfo(selectedFilePath);
                const long maxAttachmentSize = 25 * 1024 * 1024;
                if (fileInfo.Length > maxAttachmentSize)
                {
                    MessageBox.Show("Выбранный файл слишком большой. Пожалуйста, выберите файл меньшего размера.");
                    return;
                }

                Attachment attachment = new Attachment(selectedFilePath);
                message.Attachments.Add(attachment);
            }



            SmtpClient client = null;


            if (From.Text.EndsWith("@mail.ru"))
            {
                client = new SmtpClient("smtp.mail.ru", 587)
                {
                    Credentials = new NetworkCredential(From.Text, Pass.Password),
                    EnableSsl = true
                };
            }
            else if (From.Text.EndsWith("@gmail.com"))
            {
                client = new SmtpClient("smtp.gmail.com", 587)
                {
                    Credentials = new NetworkCredential(From.Text, Pass.Password),
                    EnableSsl = true
                };
            }
            else if (From.Text.EndsWith("@rambler.ru"))
            {
                client = new SmtpClient("smtp.rambler.ru", 587)
                {
                    Credentials = new NetworkCredential(From.Text, Pass.Password),
                    EnableSsl = true
                };
            }
            else if (From.Text.EndsWith("@yandex.ru"))
            {
                client = new SmtpClient("smtp.yandex.ru", 587)
                {
                    Credentials = new NetworkCredential(From.Text, Pass.Password),
                    EnableSsl = true
                };
            }
            else
            {
                MessageBox.Show("Данная почта не поддэрживается.");
                return;
            }

            try
            {
                client.Send(message);
                MessageBox.Show("Письмо успешно улетело!");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Откисай чушпан: " + ex.Message);
            }


        }


        private void vibor(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                selectedFilePath = openFileDialog.FileName;

            }
        }



    }
}
