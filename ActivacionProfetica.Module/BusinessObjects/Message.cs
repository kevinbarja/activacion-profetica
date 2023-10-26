using ActivacionProfetica.Module.BusinessObjects.Enums;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [Caption("Mensaje")]
    [DefaultProperty(nameof(Content))]
    [Persistent(Schema.Rjv + nameof(Message))]

    [Appearance("BotColorMessage", Enabled = true, TargetItems = "*",
        Criteria = "[MessageSource] = ##Enum#ActivacionProfetica.Module.BusinessObjects.Enums.MessageSource,Bot#",
        Context = "WhatsappConversation_Messages_ListView", BackColor = "136, 242, 161")]
    public class Message : BaseEntity
    {
        private WhatsappConversation whatsappConversation;
        private string whatsappMessage;
        private MessageSource messageSource = MessageSource.Bot;

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            messageDate = DateTime.Now;
        }

        [ModelDefault("EditMask", "dd.MM.yyyy hh:mm:ss")]
        DateTime messageDate;
        [Caption("Fecha")]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [ModelDefault("AllowEdit", "False"), ModelDefault("DisplayFormat", "G")]
        public DateTime MessageDate
        {
            get { return messageDate; }
            set { SetPropertyValue(ref messageDate, value); }
        }

        [Caption("Whatsapp")]
        [Association("WhatsappConvesarion-Message")]
        [Persistent("WhatsappConvesarion_Message")]
        public WhatsappConversation WhatsappConversation
        {
            get => whatsappConversation;
            set => SetPropertyValue(ref whatsappConversation, value);
        }

        [Caption("Mensaje")]
        [FieldSize(-1)]
        [Nullable(false), RequiredField, Size(Constants.StringSize.LargeSringSize)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Content
        {
            get => whatsappMessage;
            set => SetPropertyValue(ref whatsappMessage, value);
        }

        [Caption("Remitente")]
        [Nullable(false), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public MessageSource MessageSource
        {
            get => messageSource;
            set => SetPropertyValue(ref messageSource, value);
        }

        public Message(Session session) : base(session)
        {

        }
    }
}
