using System;
using System.Activities;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using MailKit.Net.Imap;
using MailKit;
using MailKit.Security;
using MailKit.Search;
using System.Net.Mail;
using MimeKit;
using Get_Mail_IMAP;

namespace SCB.IMAP.GetMail
{

    //"imap.yandex.com.tr"
    //"selim@selimcanbagdatli.com"

    public class GetIMAPMessage : CodeActivity
    {
        public enum SecureSocketEncryption
        {
            None,
            Auto,
            SslOnConnect,
            StartTls,
            StartTlsWhenAvailable,
        }

        public enum FilterTypes
        {
            No,
            Subject,
            MailAddres,
            Body,
            MailId                     
        }

        public enum ReadTypes
        {
            All,
            UnRead,
            Read
        }

        //-----------------  FilterType  --------------------
        [Category("Filters")]
        [DisplayName("FilterTypes")]
        public FilterTypes FilterType { get; set; }

        [Category("Filters")]
        [DisplayName("FilterText")]
        public InArgument<string> FilterText { get; set; }

        //-----------------  OPTİONS  --------------------
        [Category("Options")]
        [DisplayName("SecureConnectionName")]
        public SecureSocketEncryption SecureConnection { get; set; }

        [Category("Options")]
        [DisplayName("ReadTypes")]
        public ReadTypes ReadType { get; set; }

        [Category("Options")]
        [DisplayName("MarkAsRead")]
        public bool MarkAsRead { get; set; }

        //-----------------  HOST  --------------------

        [Category("Host")]
        [DisplayName("PortDisplayName")]
        public InArgument<int> Port { get; set; }

        [Category("Host")]
        [DisplayName("MailBoxName")]
        public InArgument<int> MailBoxName { get; set; }
      
        [Category("Host")]
        [DisplayName("ServerDisplayName")]
        [RequiredArgument]
        public InArgument<string> Server { get; set; }

        [Category("Host")]
        [DisplayName("MailFolderDisplayName")]
        public InArgument<string> MailFolder { get; set; }

        //-----------------  LOGON  --------------------
        [RequiredArgument]
        [Category("Logon")]
        [DisplayName("EmailDisplayName")]
        public InArgument<string> Email { get; set; }

       
        [RequiredArgument]
        [Category("Logon")]
        [DisplayName("PasswordDisplayName")]
        public InArgument<string> Password { get; set; }

        //-----------------  OUTPUT  --------------------
        [Category("Output")]
        [DisplayName("OutputMessage")]
        public OutArgument<List<MimeMessage>> MailMessage { get; set; }

        protected override void Execute(CodeActivityContext context)  
        {

            using (var client = new ImapClient(new ProtocolLogger("imap.log")))

            {

               
                if (this.SecureConnection== SecureSocketEncryption.Auto)
                {
                    client.Connect(Server.Get(context), Port.Get(context), SecureSocketOptions.Auto);
                }
                else if(this.SecureConnection == SecureSocketEncryption.None)
                {
                    client.Connect(Server.Get(context), Port.Get(context), SecureSocketOptions.None);
                }
                else if (this.SecureConnection == SecureSocketEncryption.SslOnConnect)
                {
                    client.Connect(Server.Get(context), Port.Get(context), SecureSocketOptions.SslOnConnect);
                }
                else if (this.SecureConnection == SecureSocketEncryption.StartTls)
                {
                    client.Connect(Server.Get(context), Port.Get(context), SecureSocketOptions.StartTls);
                }
                else if (this.SecureConnection == SecureSocketEncryption.StartTlsWhenAvailable)
                {
                    client.Connect(Server.Get(context), Port.Get(context), SecureSocketOptions.StartTlsWhenAvailable);
                }


                client.Authenticate(Email.Get(context), Password.Get(context));

                //client.GetFolder(MailBoxName.Get(context).ToString()).Open(FolderAccess.ReadWrite);

                client.Inbox.Open(FolderAccess.ReadWrite);

                List<MimeMessage> MessageList = new List<MimeMessage>();
                bool MarkAsRead = true;

                if (this.MarkAsRead)
                    MarkAsRead = true;

                //No Filter      
                if (this.FilterType == FilterTypes.No)
                {
                    Console.WriteLine("GetImap WorkFlow => NoFilter : Mesajlar okunuyor");
                    MessageList =Filter.NoFilter(client,this.ReadType.ToString(),MarkAsRead);
                    Console.WriteLine("GetImap WorkFlow => NoFilter : Mesajlar okunuyor bulunan mesaj sayısı =" + MessageList.Count.ToString());                 
                }

                // Subject Filter
                if (this.FilterType==FilterTypes.Subject)
                {
                    Console.WriteLine("GetImap WorkFlow => SubjectFilter : Mesajlar okunuyor");
                    MessageList = Filter.SubjectFilter(client, this.ReadType.ToString(), context, FilterText,MarkAsRead);
                    Console.WriteLine("GetImap WorkFlow => SubjectFilter : Mesajlar okunuyor bulunan mesaj sayısı =" + MessageList.Count.ToString());
                }
               
                //MailAddres Filter              
                if (this.FilterType == FilterTypes.MailAddres)
                {
                    Console.WriteLine("GetImap WorkFlow => MailAddresFilter : Mesajlar okunuyor");
                    MessageList = Filter.MailAddresFilter(client, this.ReadType.ToString(), context, FilterText,MarkAsRead);
                    Console.WriteLine("GetImap WorkFlow => MailAddresFilter : Mesajlar okunuyor bulunan mesaj sayısı =" + MessageList.Count.ToString());
                }

                //Boddey Filter
                else if (this.FilterType == FilterTypes.Body)
                {
                    Console.WriteLine("GetImap WorkFlow => BoddeyFilter : Mesajlar okunuyor");
                    MessageList = Filter.BoddyFilter(client, this.ReadType.ToString(), context,FilterText,MarkAsRead);
                    Console.WriteLine("GetImap WorkFlow => BoddeyFilter : Mesajlar okunuyor bulunan mesaj sayısı =" + MessageList.Count.ToString());
                }

                //MailId Filter 
                 else if (this.FilterType == FilterTypes.MailId)
                {
                    Console.WriteLine("GetImap WorkFlow => MailIdFilter : Mesajlar okunuyor");
                    MessageList = Filter.MailIdFilter(client, this.ReadType.ToString(), context, FilterText,MarkAsRead);
                    Console.WriteLine("GetImap WorkFlow => MailIdFilter : Mesajlar okunuyor bulunan mesaj sayısı =" + MessageList.Count.ToString());
                }

                client.Disconnect(true);

                MailMessage.Set(context, MessageList);
             
            }
               
        }        
    }
}
