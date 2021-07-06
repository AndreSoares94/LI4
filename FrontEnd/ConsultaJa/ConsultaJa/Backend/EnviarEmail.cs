using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace ConsultaJa.Backend
{
    class EnviarEmail
    {
        public static int sendEmail(string email)
        {
            Random random = new Random();
            int codigo = random.Next(999999);

            try
            {
                // Instancia da classe de mensagem
                MailMessage mailMessage = new MailMessage();

                // Remetente
                mailMessage.From = new MailAddress("consultaja4@gmail.com");

                // Destinatário seta no metodo abaixo

                // Constrói o MailMessage
                mailMessage.CC.Add(email);
                mailMessage.Subject = "Email de Verificação de ConsultaJa";
                mailMessage.Body = "Bem vindo à plataforma Consultaja!!!\n\n\nCódigo de Verificação: " + codigo + "\n\nObrigado pela preferência!\nEquipa de suporte Consultaja";

                // Configuração com porta
                SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", Convert.ToInt32("587"));

                // Configuração sem porta
                // SmtpClient smtpClient = new SmtpClient(UtilRsource.ConfigSmtp);

                // Credencial para envio por SMTP Seguro (Quando o servidor exige autenticação)
                smtpClient.UseDefaultCredentials = false;
                smtpClient.Credentials = new NetworkCredential("consultaja4@gmail.com", "consulta!4ja");

                smtpClient.EnableSsl = true;

                smtpClient.Send(mailMessage);

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            return codigo;
        }
    }
}
