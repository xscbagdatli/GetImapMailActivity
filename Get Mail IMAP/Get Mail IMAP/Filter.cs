using MailKit;
using MailKit.Net.Imap;
using MailKit.Search;
using MimeKit;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Get_Mail_IMAP
{
       public class Filter
    {
        public static List<MimeMessage> NoFilter(ImapClient client,string ReadType , bool MarkAsRead)
        {
            List<MimeMessage> MessageList = new List<MimeMessage>();
            IList<UniqueId> uids = client.Inbox.Search(SearchQuery.All);

            
            if (ReadType == "UnRead")
            {
                uids = client.Inbox.Search(SearchQuery.And(SearchQuery.NotSeen, SearchQuery.All));
                if (MarkAsRead)
                    client.Inbox.AddFlags(uids, MessageFlags.Seen, true);
                return MessageList = uids.AsEnumerable().Select(x => client.Inbox.GetMessage(x)).ToList();
            }
            else if (ReadType == "Read")
            {
                uids = client.Inbox.Search(SearchQuery.And(SearchQuery.Seen, SearchQuery.All));
                if (MarkAsRead)
                    client.Inbox.AddFlags(uids, MessageFlags.Seen, true);
                return MessageList = uids.AsEnumerable().Select(x => client.Inbox.GetMessage(x)).ToList();
            }
            else
            {
                uids = client.Inbox.Search(SearchQuery.All);
                if (MarkAsRead)
                    client.Inbox.AddFlags(uids, MessageFlags.Seen, true);
                return MessageList = uids.AsEnumerable().Select(x => client.Inbox.GetMessage(x)).ToList();
            }

                
        }

        public static List<MimeMessage> SubjectFilter(ImapClient client, string ReadType,CodeActivityContext context,InArgument<string> FilterText,bool MarkAsRead)
        {

            List<MimeMessage> MessageList = new List<MimeMessage>();
            IList<UniqueId> uids = client.Inbox.Search(SearchQuery.All);

            if (ReadType == "UnRead")
            {
                uids = client.Inbox.Search(SearchQuery.And(SearchQuery.NotSeen, SearchQuery.SubjectContains(FilterText.Get(context).ToString())));
                if (MarkAsRead)
                    client.Inbox.AddFlags(uids, MessageFlags.Seen, true);
                return MessageList = uids.AsEnumerable().Select(x => client.Inbox.GetMessage(x)).ToList();
            }
            else if (ReadType == "Read")
            {
                uids = client.Inbox.Search(SearchQuery.And(SearchQuery.Seen, SearchQuery.SubjectContains(FilterText.Get(context).ToString())));
                if (MarkAsRead)
                    client.Inbox.AddFlags(uids, MessageFlags.Seen, true);
                return MessageList = uids.AsEnumerable().Select(x => client.Inbox.GetMessage(x)).ToList();
            }
            else
            {
                uids = client.Inbox.Search(SearchQuery.SubjectContains(FilterText.Get(context).ToString()));
                if (MarkAsRead)
                    client.Inbox.AddFlags(uids, MessageFlags.Seen, true);
                return MessageList = uids.AsEnumerable().Select(x => client.Inbox.GetMessage(x)).ToList();
            }
        }

        public static List<MimeMessage> MailAddresFilter(ImapClient client, string ReadType, CodeActivityContext context, InArgument<string> FilterText,bool MarkAsRead)
        {
            List<MimeMessage> MessageList = new List<MimeMessage>();
            IList<UniqueId> uids = client.Inbox.Search(SearchQuery.All);

            if (ReadType == "UnRead")
            {
                uids = client.Inbox.Search(SearchQuery.And(SearchQuery.NotSeen, SearchQuery.FromContains(FilterText.Get(context).ToString())));
                if (MarkAsRead)
                    client.Inbox.AddFlags(uids, MessageFlags.Seen, true);
                return MessageList = uids.AsEnumerable().Select(x => client.Inbox.GetMessage(x)).ToList();
            }
            else if (ReadType == "Read")
            {
                uids = client.Inbox.Search(SearchQuery.And(SearchQuery.Seen, SearchQuery.FromContains(FilterText.Get(context).ToString())));
                if (MarkAsRead)
                    client.Inbox.AddFlags(uids, MessageFlags.Seen, true);
                return MessageList = uids.AsEnumerable().Select(x => client.Inbox.GetMessage(x)).ToList();
            }
            else
            {
                uids = client.Inbox.Search(SearchQuery.FromContains(FilterText.Get(context).ToString()));
                if (MarkAsRead)
                    client.Inbox.AddFlags(uids, MessageFlags.Seen, true);
                return MessageList = uids.AsEnumerable().Select(x => client.Inbox.GetMessage(x)).ToList();
            }

        }

        public static List<MimeMessage> BoddyFilter(ImapClient client, string ReadType, CodeActivityContext context, InArgument<string> FilterText,bool MarkAsRead)
        {
            List<MimeMessage> MessageList = new List<MimeMessage>();
            IList<UniqueId> uids = client.Inbox.Search(SearchQuery.All);


            if (ReadType == "UnRead")
            {
                uids = client.Inbox.Search(SearchQuery.NotSeen);
                var uids2 = uids.Where(x => client.Inbox.GetMessage(x).Body.ToString().Contains(FilterText.Get(context).ToString())).ToList();
                if (MarkAsRead)
                    client.Inbox.AddFlags(uids2, MessageFlags.Seen, true);             
                return MessageList = uids2.AsEnumerable().Select(x => client.Inbox.GetMessage(x)).ToList(); //  I fucked For each :)              
            }
            else if (ReadType == "Read")
            {
                uids = client.Inbox.Search(SearchQuery.Seen);
                var uids2 = uids.Where(x => client.Inbox.GetMessage(x).Body.ToString().Contains(FilterText.Get(context).ToString())).ToList();
                if (MarkAsRead)
                    client.Inbox.AddFlags(uids2, MessageFlags.Seen, true);                        
                return MessageList = uids2.AsEnumerable().Select(x => client.Inbox.GetMessage(x)).ToList(); //  I fucked For each :)
            }
            else
            {
                uids = client.Inbox.Search(SearchQuery.All);
                var uids2 = uids.Where(x => client.Inbox.GetMessage(x).Body.ToString().Contains(FilterText.Get(context).ToString())).ToList();
                if (MarkAsRead)
                    client.Inbox.AddFlags(uids2, MessageFlags.Seen, true);
                return MessageList = uids2.AsEnumerable().Select(x => client.Inbox.GetMessage(x)).ToList(); //  I fucked For each :)
            }

        }

        public static List<MimeMessage> MailIdFilter(ImapClient client, string ReadType, CodeActivityContext context, InArgument<string> FilterText, bool MarkAsRead)
        {
            List<MimeMessage> MessageList = new List<MimeMessage>();
            IList<UniqueId> uids = client.Inbox.Search(SearchQuery.All);

            if (ReadType == "UnRead")
            {
                uids = client.Inbox.Search(SearchQuery.NotSeen);
                var uids2 = uids.Where(x => x.Id == Convert.ToUInt32(FilterText.Get(context).ToString())).ToList();
                if (MarkAsRead)
                    client.Inbox.AddFlags(uids2, MessageFlags.Seen, true);
               return MessageList = uids2.AsEnumerable().Select(x=> client.Inbox.GetMessage(x)).ToList();             
            }

            else if(ReadType == "Read")
            {
                uids = client.Inbox.Search(SearchQuery.Seen);
                var uids2 = uids.Where(x => x.Id == Convert.ToUInt32(FilterText.Get(context).ToString())).ToList();
                if (MarkAsRead)
                    client.Inbox.AddFlags(uids2, MessageFlags.Seen, true);
                return MessageList = uids2.AsEnumerable().Select(x => client.Inbox.GetMessage(x)).ToList();
            }
            else
            {             
                var uids2 = uids.Where(x => x.Id == Convert.ToUInt32(FilterText.Get(context).ToString())).ToList();
                if (MarkAsRead)
                    client.Inbox.AddFlags(uids2, MessageFlags.Seen, true);
                return MessageList = MessageList = uids2.AsEnumerable().Select(x => client.Inbox.GetMessage(x)).ToList();
            }
      
        }
    }
}
