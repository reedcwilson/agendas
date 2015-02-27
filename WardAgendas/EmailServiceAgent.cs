using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace WardAgendas
{
	public class EmailServiceAgent
	{
		public MessageResponse SendMessage(string meeting, string toEmail, Member bishop, Member exec_sec)
		{
			MailMessage mail = new MailMessage();
			//var html = GenerateHtmlMessageBody(meeting, bishop, exec_sec);
			var text = GeneratePlainTextMessageBody(meeting, bishop, exec_sec);
			ConfigureMessage(mail, new[] { toEmail }, null, text);
			var response = SendMessage(mail);
			mail.Dispose();
			return response;
		}

		public MessageResponse SendMessageToBishop(string meeting, string openingPrayer, string thought, string closingPrayer, Member[] bishopric)
		{
			MailMessage mail = new MailMessage();
			//var html = GenerateHtmlMessageBody(meeting, bishop, exec_sec);
			var text = GeneratePlainTextMessageBodyForBishop(meeting, openingPrayer, thought, closingPrayer);
			ConfigureMessage(mail, bishopric.Select(b => b.Email).ToArray(), null, text);
			var response = SendMessage(mail);
			mail.Dispose();
			return response;
		}

		private MessageResponse SendMessage(MailMessage mail)
		{
			var response = new MessageResponse();

			SmtpClient SmtpServer = new SmtpClient()
			{
				Host = "smtp.gmail.com",
				Port = 587,
				EnableSsl = true,
				DeliveryMethod = SmtpDeliveryMethod.Network,
				UseDefaultCredentials = false,
        // replace network credentials with yours
				Credentials = new NetworkCredential("username@gmail.com", "*******")
			};

			try
			{
				using (mail)
				{
					SmtpServer.Send(mail);
				}
				response.Success = true;
				Console.WriteLine("eMail Sent");
			}
			catch (Exception ex)
			{
				response.Success = false;
				response.Exception = ex;
				response.Message = ex.Message;
				Console.WriteLine("eMail not sent: " + ex.ToString());
			}
			return response;
		}

		private string GenerateHtmlMessageBody(string meeting, Member bishop, Member exec_sec)
		{
			var html = new StringBuilder();
			html.Append(_htmlHead);

			html.Append("<body>");
			html.Append(String.Format("<h1>Spiritual Thought</h1>"));
			html.Append(String.Format(@"<p>{0} is wondering if you would be willing to share a spiritual thought in this upcoming {1} meeting. If you will be unable to attend this meeting or unable to share a thought please respond to {2} ({3}) as soon as possible.</p>", bishop.Name, meeting, exec_sec.Name, exec_sec.Email));
			html.Append("</body></html>");

			return html.ToString();
		}

		private string GeneratePlainTextMessageBody(string meeting, Member bishop, Member exec_sec)
		{
			var text = new StringBuilder();
			//text.Append(_htmlHead);

			text.Append(String.Format("Spiritual Thought\n\n"));
			text.Append(String.Format("{0} is wondering if you would be willing to share a spiritual thought in this upcoming {1} meeting. If you will be unable to attend this meeting or unable to share a thought please respond to {2} ({3}) as soon as possible.", bishop.Name, meeting, exec_sec.Name, exec_sec.Email));

			return text.ToString();
		}
		
		private string GeneratePlainTextMessageBodyForBishop(string meeting, string openingPrayer, string thought, string closingPrayer)
		{
			var text = new StringBuilder();

			text.Append(String.Format("{0} Agenda\n\n", meeting));
			text.Append(String.Format("Opening Prayer: {0}\nSpiritual Thought: {1}\nClosing Prayer: {2}", openingPrayer, thought, closingPrayer));

			return text.ToString();
		}

		private static void ConfigureMessage(MailMessage mail, string[] toEmails, string html, string text)
		{
			mail.From = new MailAddress("reedcwilson@gmail.com");
			foreach (var email in toEmails)
			{
				mail.To.Add(email);
			}
			mail.Subject = "Spiritual Thought";

			mail.IsBodyHtml = false;
			mail.Body = text;

			//var htmlView = AlternateView.CreateAlternateViewFromString(html, new ContentType("text/html"));

			//mail.AlternateViews.Add(htmlView);
			//mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(text, new ContentType("text/plain")));
		}

		#region Html Strings

		private static string _htmlHead = @"<html>
									<head></head>
									<style type='text/css' rel='stylesheet'>
									body { color:#363636; font-family:'Helvetica Neue',Arial,sans-serif; }
									table { border-collapse:collapse; border:0; }
									h1,h2,th { font-family:Georgia,Serif; font-weight:normal; }
									h2 { font-size:28px; margin:30px 0 15px; }
									th { border-bottom:2px solid #b8ac92; padding:10px 8px; color:#445708; font-size:16px; }
									td { border-bottom:1px solid #ccc; padding:6px 8px; font-size:14px; }
									.number { text-align:right; }
									</style>";

		#endregion

	}

	public class MessageResponse
	{
		public bool Success { get; set; }
		public Exception Exception { get; set; }
		public string Message { get; set; }
	}
}
