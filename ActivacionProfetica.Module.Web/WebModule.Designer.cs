﻿namespace ActivacionProfetica.Module.Web {
    partial class ActivacionProfeticaAspNetModule {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing) {
            if(disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            // 
            // ActivacionProfeticaAspNetModule
            // 
            this.RequiredModuleTypes.Add(typeof(ActivacionProfetica.Module.ActivacionProfeticaModule));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Web.SystemModule.SystemAspNetModule));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ReportsV2.Web.ReportsAspNetModuleV2));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.TreeListEditors.Web.TreeListEditorsAspNetModule));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.Validation.Web.ValidationAspNetModule));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.ReportsV2.ReportsModuleV2));
            this.RequiredModuleTypes.Add(typeof(DevExpress.ExpressApp.StateMachine.StateMachineModule));

        }

        #endregion
    }
}