using ActivacionProfetica.Module.BusinessObjects.Enums;
using ActivacionProfetica.Module.SharedKernel;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [Caption("Conversaciones whatsapp")]
    [DefaultProperty(nameof(WhatsappNumber))]
    [Persistent(Schema.Rjv + nameof(WhatsappConversation))]
    public class WhatsappConversation : BaseEntity
    {
        private string whatsappNumber;
        private bool automaticResponses = true;
        private MessageSource messageSource = MessageSource.Bot;


        public WhatsappConversation(Session session) : base(session)
        {
        }

        [ModelDefault("EditMask", "dd.MM.yyyy hh:mm:ss")]
        DateTime messageDate;
        [Caption("Último mensaje")]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [ModelDefault("AllowEdit", "False"), ModelDefault("DisplayFormat", "G")]
        public DateTime MessageDate
        {
            get { return messageDate; }
            set { SetPropertyValue(ref messageDate, value); }
        }

        [Caption("Último mensaje por")]
        [Nullable(false), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public MessageSource MessageSource
        {
            get => messageSource;
            set => SetPropertyValue(ref messageSource, value);
        }

        [RuleUniqueValue("ValidateUniqueWhatsappConversation", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, SkipNullOrEmptyValues = true)]
        [Caption("Whatsapp")]
        [Size(255), Nullable(false), RequiredField]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public string WhatsappNumber
        {
            get => whatsappNumber;
            set => SetPropertyValue(ref whatsappNumber, value);
        }

        [Caption("Respuestas automáticas")]
        [Nullable(false)]
        [RequiredField]
        [ImmediatePostData]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public bool AutomaticResponses
        {
            get => automaticResponses;
            set => SetPropertyValue(ref automaticResponses, value);
        }

        [Caption("Mensajes")]
        [Association("WhatsappConvesarion-Message"), Aggregated]
        public XPCollection<Message> Messages => GetCollection<Message>();
    }
}
