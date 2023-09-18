using ECommerce.DAL.Helpers;
using ECommerce.DAL.Reposatory.Repo;
using MailKit.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using MimeKit;

namespace ECommerce.DAL.Reposatory.RepoServices
{
    public class MailingService : IEmailService
    {
        private readonly MailSettings _mailSettings;
        public MailingService( IOptions<MailSettings> mailSettings )
        {
            this._mailSettings = mailSettings.Value;
        }
        public async Task SendEmailAsync( string mailTo , string body , string subject , IList<IFormFile> attachments = null )
        {
            var message = new MimeMessage( );
            /*            {
                            Sender = ( MailAddress ) MailboxAddress.Parse( _mailSettings.Email ) ,
                            Subject = subject ,
                        };*/
            message.From.Add( new MailboxAddress( _mailSettings.DisplayedName , _mailSettings.Email ) );
            message.To.Add( new MailboxAddress( mailTo.Split( '@' )[ 0 ] , mailTo ) );
            message.Subject = subject;
            var builder = new BodyBuilder( );
            if ( attachments != null )
            {
                byte[ ] fileBytes;
                foreach ( var attachment in attachments )
                {
                    if ( attachment.Length > 0 )
                    {
                        using var ms = new MemoryStream( );
                        attachment.CopyTo( ms );
                        fileBytes = ms.ToArray( );
                        // We may also want to attach a calendar event for Monica's party...
                        builder.Attachments.Add( attachment.FileName , fileBytes , ContentType.Parse( attachment.ContentType ) );
                    }
                }

            }

            builder.HtmlBody = body;
            // Now we just need to set the message body and we're done

            message.Body = builder.ToMessageBody( );
            using ( var client = new MailKit.Net.Smtp.SmtpClient( ) )
            {
                client.Connect( _mailSettings.Host , _mailSettings.Port , SecureSocketOptions.StartTls );
                client.Authenticate( _mailSettings.Email , _mailSettings.Password );

                await client.SendAsync( message );

                client.Disconnect( true );
            }
        }
    }
}
