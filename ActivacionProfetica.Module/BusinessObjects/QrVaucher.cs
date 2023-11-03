using ActivacionProfetica.Module.SharedKernel;
using DevExpress.Xpo;
using DevExpress.XtraRichEdit.Import.Html;
using System.ComponentModel;
using static ActivacionProfetica.Module.SharedKernel.Constants;
using Caption = System.ComponentModel.DisplayNameAttribute;

namespace ActivacionProfetica.Module.BusinessObjects
{
    [Caption("Comprobantes QR")]
    [DefaultProperty(nameof(Content))]
    [Persistent(Schema.Rjv + nameof(QrVaucher))]
    public class QrVaucher : BaseEntity
    {
        private string bankId;

        public string BankId
        {
            get { return bankId; }
            set { SetPropertyValue(ref bankId, value); }
        }

        private int amount;

        public int Amount
        {
            get { return amount; }
            set { SetPropertyValue(ref amount, value); }
        }

        private string currency;

        public string Currency
        {
            get { return currency; }
            set { SetPropertyValue(ref currency, value); }
        }

        private string generationDate;

        public string GenerationDate
        {
            get { return generationDate; }
            set { SetPropertyValue(ref generationDate, value); }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { SetPropertyValue(ref status, value); }
        }

        private string transactionDate;

        public string TransactionDate
        {
            get { return transactionDate; }
            set { SetPropertyValue(ref transactionDate, value); }
        }

        private string originName;

        public string OriginName
        {
            get { return originName; }
            set { SetPropertyValue(ref originName, value); }
        }

        private string sourceBank;

        public string SourceBank
        {
            get { return sourceBank; }
            set { SetPropertyValue(ref sourceBank, value); }
        }

        private string sourceAccountNumber;

        public string SourceAccountNumber
        {
            get { return sourceAccountNumber; }
            set { SetPropertyValue(ref sourceAccountNumber, value); }
        }

        private string gloss;

        public string Gloss
        {
            get { return gloss; }
            set { SetPropertyValue(ref gloss, value); }
        }

        private string destinationAccountNumber;

        public string DestinationAccountNumber
        {
            get { return destinationAccountNumber; }
            set { SetPropertyValue(ref destinationAccountNumber, value); }
        }




        public QrVaucher(Session session) : base(session)
        {
        }

    }
}
