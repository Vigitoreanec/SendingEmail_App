using System.Net;
using System.Net.Mail;



namespace SendingEmail_App.Models
{
    public class Service
    {
        private readonly ILogger<Service> _logger;
        public Service(ILogger<Service> logger)
        {
            _logger = logger;
        }

        public void SendEmailAnswer(string recipientEmail, string code)
        {
            try
            {
                // пример отправки
                MailMessage message = new MailMessage();
                message.IsBodyHtml = false;
                message.From = new MailAddress("chirik39@mail.ru", "Ответ Авторизации");
                message.To.Add($"{recipientEmail}");
                message.Subject = "Сообщение Авторизации";
                message.Body = $"Код авторизации {code}";
                //message.Attachments.Add(new Attachment(" .. путь к файлу .. "));

                using (SmtpClient client = new SmtpClient("smtp.mail.ru", 587))
                {
                    // подготовка клиента
                    client.EnableSsl = true;
                    client.Credentials = new NetworkCredential("chirik39@mail.ru", "E446Q0zVCiQhP4tDhGPL");
                    // отправка сообщения
                    client.Send(message);
                }
                _logger.LogInformation($"Сообщение с кодом подтверждения отправлено на { recipientEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.GetBaseException().Message);
            }
        }
    }
}
