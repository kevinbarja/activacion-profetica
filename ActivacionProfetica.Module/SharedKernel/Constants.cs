namespace ActivacionProfetica.Module.SharedKernel
{
    public static class Constants
    {
        public static class Schema
        {
            public const string Ap = "ap.";
        }

        public static class Menu
        {
            public const string ActivacionProfetica = "Application/NavigationItems/Items/ActivacionProfetica";
            public const string Customers = "Application/NavigationItems/Items/ActivacionProfetica/Items/Customers";
        }

        public static class View
        {
            public const string AuditListView = "Audit_ListView";
            public const string OperationPlacesLookupListView = "Operation_Places_LookupListView";
            public const string OperationDetailView = "Operation_DetailView";
            public const string PaymentListView = "ReportPayment_ListView";

        }

        public static class Role
        {
            public const string Public = "Acceso público";
            public const string Admin = "Acceso completo";
            public const string OperationEjecutive = "Ejecutivo de operaciones";
            public const string OperationSupervisor = "Supervisor de operaciones";
        }

        public static class StringSize
        {
            public const int ShortSringSize = 255;
            public const int LargeSringSize = 500;
            public const int SmallSringSize = 10;
        }

        #region Colors
        public static string DisabledColor = "240, 240, 240";
        public const string BlackFontColor = "0, 0, 0";
        #endregion
    }
}
