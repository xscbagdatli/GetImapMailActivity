# GetImapMailActivity


An activity for uipath that can receive and filter emails with imap and mark only filtered mails as read

-You may select the type of mail filter via this activity.
-You may select which type of the messages you would like to read.
-In the filter area, you may search the containing words via this activity.
-You may switch read or unread status of messages that are fit to your filter.

// !!! OutArguman returns with List<MimeMessage> type.

// Gmail, the server of your mail address you will read, There are a few steps you need to do, I leave the link here. (https://support.google.com/accounts/answer/185833)


//-----------------------------------------**-----------------------------------------------------

İmap ile e-postaları alıp filtreleyebilen ve yalnızca filtrelenmiş postaları okundu olarak işaretleyebilen uipath için bir etkinlik

-Bu aktivite ile Mail filtre tipi seçebilirsiniz (No,Subject,MailAddres,Body,MailId)
-Bu aktivite ile hangi türden mesajları okayacağınızı seçebilirsiniz ( All,UnRead,Read)
-Bu aktivite ile filtreleme yapacağınız yerde contains olarak kelime arayabilirsiniz 
-Bu aktivite ile sadece filtrenize uygun mesajların okundutipini isterseniz değiştirebilirsiniz

 // !!! OutArguman size List<MimeMessage> tipi ile döner bunu unutmayın.
 
 // Okuyacağınız mail adresinizin sunucusu Gmail ise yapmanız gereken bir kaç adım var buraya linkini bırakıyorum  (https://support.google.com/accounts/answer/185833)
