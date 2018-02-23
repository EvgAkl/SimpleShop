using System.Net;
using System.Net.Mail;
using System.Text;
using SimpleShop.Domain.Abstract;
using SimpleShop.Domain.Entities;

namespace SimpleShop.Domain.Concrete
{
    public class EmailSetting
    {
        public string MailToAddress = "arders@example.com";
        public string MailFromAddress = "gamestore@example";
        public bool UseSl = true;
        public string UserName = "MySmtpUserName";
        public string Password = "MySmtpPassword";
        public string ServerName = "smtp.example.com";
        public int ServerPort = 587;
        public bool WriteAsFile = true;
        public string FileLocation = @"c:\game_store_emails";
    } // end class EmailOrderProcessor

    public class EmailOrderProcessor : IOrderProcessor
    {
        // Declaring variables
        private EmailSetting emailSettings;
        // Declaring constructors
        public EmailOrderProcessor(EmailSetting settings)
        {
            emailSettings = settings;
        }
        // Declaring methods
        public void ProcessOrder(Cart cart, ShipingDetails shippingInfo)
        {
            using (var smtpClient = new SmtpClient())
            {
                smtpClient.EnableSsl = emailSettings.UseSl;
                smtpClient.Host = emailSettings.ServerName;
                smtpClient.Port = emailSettings.ServerPort;
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential(emailSettings.UserName, emailSettings.Password);

                if (emailSettings.WriteAsFile)
                {
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.SpecifiedPickupDirectory;
                    smtpClient.PickupDirectoryLocation = emailSettings.FileLocation;
                    smtpClient.EnableSsl = false;
                }

                StringBuilder body = new StringBuilder().AppendLine("Новый заказ обработан").AppendLine("---").AppendLine("Товары:");

                foreach (var line in cart.Lines)
                {
                    var subtotal = line.Game.Price * line.Quantity;
                    body.AppendFormat("{0} x {1} (итого: {2:c})", line.Quantity, line.Game.Name, subtotal);
                }

                body.AppendFormat("общая стогмость: {0:c}", cart.ComputeTotalValue())
                    .AppendLine("---")
                    .AppendLine("Доставка")
                    .AppendLine(shippingInfo.Name)
                    .AppendLine(shippingInfo.Line1)
                    .AppendLine(shippingInfo.Line2 ?? "")
                    .AppendLine(shippingInfo.Line3 ?? "")
                    .AppendLine(shippingInfo.City)
                    .AppendLine(shippingInfo.Country)
                    .AppendLine("---")
                    .AppendFormat("Подарочная упаковка: {0}", shippingInfo.GiftWrap ? "Да" : "Нет");

                MailMessage mailMessage = new MailMessage(emailSettings.MailFromAddress, // От кого
                                                          emailSettings.MailToAddress,   // Кому
                                                          "Новый заказ отправлен!",      // Тема
                                                          body.ToString());              // Тело письма

                if (emailSettings.WriteAsFile)
                {
                    mailMessage.BodyEncoding = Encoding.UTF8;
                }

                smtpClient.Send(mailMessage);

            } // end using

        } // end ProcessorOrder()

    } // end class EmailOrderProcessor

} // end namespace
