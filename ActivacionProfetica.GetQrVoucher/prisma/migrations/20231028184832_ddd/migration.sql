BEGIN TRY

BEGIN TRAN;

-- CreateIndex
CREATE NONCLUSTERED INDEX [iAuditedObject_AuditDataItemPersistent] ON [dbo].[AuditDataItemPersistent]([AuditedObject]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iGCRecord_AuditDataItemPersistent] ON [dbo].[AuditDataItemPersistent]([GCRecord]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iModifiedOn_AuditDataItemPersistent] ON [dbo].[AuditDataItemPersistent]([ModifiedOn]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iNewObject_AuditDataItemPersistent] ON [dbo].[AuditDataItemPersistent]([NewObject]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iObjectType_AuditDataItemPersistent] ON [dbo].[AuditDataItemPersistent]([ObjectType]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iOldObject_AuditDataItemPersistent] ON [dbo].[AuditDataItemPersistent]([OldObject]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iOperationType_AuditDataItemPersistent] ON [dbo].[AuditDataItemPersistent]([OperationType]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iUserName_AuditDataItemPersistent] ON [dbo].[AuditDataItemPersistent]([UserName]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iCreatedBy_rjv_Customer_72F81A64] ON [rjv].[Customer]([CreatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iUpdatedBy_rjv_Customer_72483A44] ON [rjv].[Customer]([UpdatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iGCRecord_HCategory] ON [dbo].[HCategory]([GCRecord]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iParent_HCategory] ON [dbo].[HCategory]([Parent]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iCreatedBy_rjv_Message_2C064DB4] ON [rjv].[Message]([CreatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iUpdatedBy_rjv_Message_6C072DF4] ON [rjv].[Message]([UpdatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iWhatsappConvesarion_Message_rjv_Message_85CA84B6] ON [rjv].[Message]([WhatsappConvesarion_Message]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iGCRecord_ModelDifference] ON [dbo].[ModelDifference]([GCRecord]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iGCRecord_ModelDifferenceAspect] ON [dbo].[ModelDifferenceAspect]([GCRecord]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iOwner_ModelDifferenceAspect] ON [dbo].[ModelDifferenceAspect]([Owner]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iCreatedBy_rjv_Operation_932464E7] ON [rjv].[Operation]([CreatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iCustomer_Operations_rjv_Operation_0B2081BD] ON [rjv].[Operation]([Customer_Operations]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iPaymentPlan_Operations_rjv_Operation_9042CA65] ON [rjv].[Operation]([PaymentPlan_Operations]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iPlaceStatus_Operations_rjv_Operation_95BB0578] ON [rjv].[Operation]([PlaceStatus_Operations]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iSector_Operations_rjv_Operation_11203DBD] ON [rjv].[Operation]([Sector_Operations]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iUpdatedBy_rjv_Operation_CB3474E7] ON [rjv].[Operation]([UpdatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iCreatedBy_rjv_Payment_8FC73D05] ON [rjv].[Payment]([CreatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iOperation_Payments_rjv_Payment_325172B2] ON [rjv].[Payment]([Operation_Payments]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iPaymentPlanDetail_Payments_rjv_Payment_B01CBFDB] ON [rjv].[Payment]([PaymentPlanDetail_Payments]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iUpdatedBy_rjv_Payment_CFC65D45] ON [rjv].[Payment]([UpdatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iCreatedBy_rjv_PaymentPlan_52E7433E] ON [rjv].[PaymentPlan]([CreatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iSector_PaymentPlans_rjv_PaymentPlan_D4844B28] ON [rjv].[PaymentPlan]([Sector_PaymentPlans]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iUpdatedBy_rjv_PaymentPlan_56E7553A] ON [rjv].[PaymentPlan]([UpdatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iCreatedBy_rjv_PaymentPlanDetail_C3948F81] ON [rjv].[PaymentPlanDetail]([CreatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iPaymentPlan_PaymentPlanDetails_rjv_PaymentPlanDetail_3BE29487] ON [rjv].[PaymentPlanDetail]([PaymentPlan_PaymentPlanDetails]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iUpdatedBy_rjv_PaymentPlanDetail_C3CC9F91] ON [rjv].[PaymentPlanDetail]([UpdatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iGCRecord_PermissionPolicyActionPermissionObject] ON [dbo].[PermissionPolicyActionPermissionObject]([GCRecord]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iRole_PermissionPolicyActionPermissionObject] ON [dbo].[PermissionPolicyActionPermissionObject]([Role]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iGCRecord_PermissionPolicyMemberPermissionsObject] ON [dbo].[PermissionPolicyMemberPermissionsObject]([GCRecord]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iTypePermissionObject_PermissionPolicyMemberPermissionsObject] ON [dbo].[PermissionPolicyMemberPermissionsObject]([TypePermissionObject]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iGCRecord_PermissionPolicyNavigationPermissionsObject] ON [dbo].[PermissionPolicyNavigationPermissionsObject]([GCRecord]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iRole_PermissionPolicyNavigationPermissionsObject] ON [dbo].[PermissionPolicyNavigationPermissionsObject]([Role]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iGCRecord_PermissionPolicyObjectPermissionsObject] ON [dbo].[PermissionPolicyObjectPermissionsObject]([GCRecord]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iTypePermissionObject_PermissionPolicyObjectPermissionsObject] ON [dbo].[PermissionPolicyObjectPermissionsObject]([TypePermissionObject]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iGCRecord_PermissionPolicyRole] ON [dbo].[PermissionPolicyRole]([GCRecord]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iObjectType_PermissionPolicyRole] ON [dbo].[PermissionPolicyRole]([ObjectType]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iGCRecord_PermissionPolicyTypePermissionsObject] ON [dbo].[PermissionPolicyTypePermissionsObject]([GCRecord]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iRole_PermissionPolicyTypePermissionsObject] ON [dbo].[PermissionPolicyTypePermissionsObject]([Role]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iGCRecord_PermissionPolicyUser] ON [dbo].[PermissionPolicyUser]([GCRecord]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iObjectType_PermissionPolicyUser] ON [dbo].[PermissionPolicyUser]([ObjectType]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iSector_Users_PermissionPolicyUser] ON [dbo].[PermissionPolicyUser]([Sector_Users]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iUser_PermissionPolicyUserLoginInfo] ON [dbo].[PermissionPolicyUserLoginInfo]([User]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iRoles_PermissionPolicyUserUsers_PermissionPolicyRoleRoles] ON [dbo].[PermissionPolicyUserUsers_PermissionPolicyRoleRoles]([Roles]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iUsers_PermissionPolicyUserUsers_PermissionPolicyRoleRoles] ON [dbo].[PermissionPolicyUserUsers_PermissionPolicyRoleRoles]([Users]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iCreatedBy_rjv_Place_2860381C] ON [rjv].[Place]([CreatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iOperation_Places_rjv_Place_D746083C] ON [rjv].[Place]([Operation_Places]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iParentPlace_Place_rjv_Place_672191E2] ON [rjv].[Place]([ParentPlace_Place]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iPlaceStatus_Places_rjv_Place_DC384E00] ON [rjv].[Place]([PlaceStatus_Places]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iSector_Places_rjv_Place_47008B84] ON [rjv].[Place]([Sector_Places]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iUpdatedBy_rjv_Place_A9613819] ON [rjv].[Place]([UpdatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iCreatedBy_rjv_PlaceStatus_CCDD48CD] ON [rjv].[PlaceStatus]([CreatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iUpdatedBy_rjv_PlaceStatus_C8DD5EC9] ON [rjv].[PlaceStatus]([UpdatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iCreatedBy_rjv_QrVaucher_CFC0ED62] ON [rjv].[QrVaucher]([CreatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iUpdatedBy_rjv_QrVaucher_97D0FD62] ON [rjv].[QrVaucher]([UpdatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iGCRecord_ReportDataV2] ON [dbo].[ReportDataV2]([GCRecord]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iCreatedBy_rjv_Sector_A059CB7E] ON [rjv].[Sector]([CreatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iUpdatedBy_rjv_Sector_20D9C9BE] ON [rjv].[Sector]([UpdatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iCreatedBy_rjv_WhatsappConversation_2F3ED2B1] ON [rjv].[WhatsappConversation]([CreatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iUpdatedBy_rjv_WhatsappConversation_2D3ED9B3] ON [rjv].[WhatsappConversation]([UpdatedBy]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iGCRecord_XpoState] ON [dbo].[XpoState]([GCRecord]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iStateMachine_XpoState] ON [dbo].[XpoState]([StateMachine]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iGCRecord_XpoStateAppearance] ON [dbo].[XpoStateAppearance]([GCRecord]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iState_XpoStateAppearance] ON [dbo].[XpoStateAppearance]([State]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iGCRecord_XpoStateMachine] ON [dbo].[XpoStateMachine]([GCRecord]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iStartState_XpoStateMachine] ON [dbo].[XpoStateMachine]([StartState]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iGCRecord_XpoTransition] ON [dbo].[XpoTransition]([GCRecord]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iSourceState_XpoTransition] ON [dbo].[XpoTransition]([SourceState]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iTargetState_XpoTransition] ON [dbo].[XpoTransition]([TargetState]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iGCRecord_XPWeakReference] ON [dbo].[XPWeakReference]([GCRecord]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iObjectType_XPWeakReference] ON [dbo].[XPWeakReference]([ObjectType]);

-- CreateIndex
CREATE NONCLUSTERED INDEX [iTargetType_XPWeakReference] ON [dbo].[XPWeakReference]([TargetType]);

-- AddForeignKey
ALTER TABLE [dbo].[AuditDataItemPersistent] ADD CONSTRAINT [FK_AuditDataItemPersistent_AuditedObject] FOREIGN KEY ([AuditedObject]) REFERENCES [dbo].[AuditedObjectWeakReference]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[AuditDataItemPersistent] ADD CONSTRAINT [FK_AuditDataItemPersistent_NewObject] FOREIGN KEY ([NewObject]) REFERENCES [dbo].[XPWeakReference]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[AuditDataItemPersistent] ADD CONSTRAINT [FK_AuditDataItemPersistent_ObjectType] FOREIGN KEY ([ObjectType]) REFERENCES [dbo].[XPObjectType]([OID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[AuditDataItemPersistent] ADD CONSTRAINT [FK_AuditDataItemPersistent_OldObject] FOREIGN KEY ([OldObject]) REFERENCES [dbo].[XPWeakReference]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[AuditedObjectWeakReference] ADD CONSTRAINT [FK_AuditedObjectWeakReference_Oid] FOREIGN KEY ([Oid]) REFERENCES [dbo].[XPWeakReference]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[BaseAuditDataItemPersistent] ADD CONSTRAINT [FK_BaseAuditDataItemPersistent_Oid] FOREIGN KEY ([Oid]) REFERENCES [dbo].[AuditDataItemPersistent]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Customer] ADD CONSTRAINT [FK_rjv_Customer_CreatedBy_2FE30556] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Customer] ADD CONSTRAINT [FK_rjv_Customer_UpdatedBy_39E70156] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[HCategory] ADD CONSTRAINT [FK_HCategory_Parent] FOREIGN KEY ([Parent]) REFERENCES [dbo].[HCategory]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Message] ADD CONSTRAINT [FK_rjv_Message_CreatedBy_A8F2A906] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Message] ADD CONSTRAINT [FK_rjv_Message_UpdatedBy_BEF6AD06] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Message] ADD CONSTRAINT [FK_rjv_Message_WhatsappConvesarion_Message_4A271B0F] FOREIGN KEY ([WhatsappConvesarion_Message]) REFERENCES [rjv].[WhatsappConversation]([InternalId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[ModelDifferenceAspect] ADD CONSTRAINT [FK_ModelDifferenceAspect_Owner] FOREIGN KEY ([Owner]) REFERENCES [dbo].[ModelDifference]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Operation] ADD CONSTRAINT [FK_rjv_Operation_CreatedBy_1AD08632] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Operation] ADD CONSTRAINT [FK_rjv_Operation_Customer_Operations_4A519331] FOREIGN KEY ([Customer_Operations]) REFERENCES [rjv].[Customer]([InternalId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Operation] ADD CONSTRAINT [FK_rjv_Operation_PaymentPlan_Operations_8B297C61] FOREIGN KEY ([PaymentPlan_Operations]) REFERENCES [rjv].[PaymentPlan]([InternalId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Operation] ADD CONSTRAINT [FK_rjv_Operation_PlaceStatus_Operations_CA570FA6] FOREIGN KEY ([PlaceStatus_Operations]) REFERENCES [rjv].[PlaceStatus]([InternalId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Operation] ADD CONSTRAINT [FK_rjv_Operation_Sector_Operations_5F482C24] FOREIGN KEY ([Sector_Operations]) REFERENCES [rjv].[Sector]([InternalId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Operation] ADD CONSTRAINT [FK_rjv_Operation_UpdatedBy_0CD48232] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Payment] ADD CONSTRAINT [FK_rjv_Payment_CreatedBy_58AE856E] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Payment] ADD CONSTRAINT [FK_rjv_Payment_Operation_Payments_805ED40D] FOREIGN KEY ([Operation_Payments]) REFERENCES [rjv].[Operation]([InternalId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Payment] ADD CONSTRAINT [FK_rjv_Payment_PaymentPlanDetail_Payments_A11AACA6] FOREIGN KEY ([PaymentPlanDetail_Payments]) REFERENCES [rjv].[PaymentPlanDetail]([InternalId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Payment] ADD CONSTRAINT [FK_rjv_Payment_UpdatedBy_4EAA816E] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[PaymentPlan] ADD CONSTRAINT [FK_rjv_PaymentPlan_CreatedBy_74EFAEE8] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[PaymentPlan] ADD CONSTRAINT [FK_rjv_PaymentPlan_Sector_PaymentPlans_6B8C1A9F] FOREIGN KEY ([Sector_PaymentPlans]) REFERENCES [rjv].[Sector]([InternalId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[PaymentPlan] ADD CONSTRAINT [FK_rjv_PaymentPlan_UpdatedBy_62EBAAE8] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[PaymentPlanDetail] ADD CONSTRAINT [FK_rjv_PaymentPlanDetail_CreatedBy_E523C320] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[PaymentPlanDetail] ADD CONSTRAINT [FK_rjv_PaymentPlanDetail_PaymentPlan_PaymentPlanDetails_39C92407] FOREIGN KEY ([PaymentPlan_PaymentPlanDetails]) REFERENCES [rjv].[PaymentPlan]([InternalId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[PaymentPlanDetail] ADD CONSTRAINT [FK_rjv_PaymentPlanDetail_UpdatedBy_F327C720] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[PermissionPolicyActionPermissionObject] ADD CONSTRAINT [FK_PermissionPolicyActionPermissionObject_Role] FOREIGN KEY ([Role]) REFERENCES [dbo].[PermissionPolicyRole]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[PermissionPolicyMemberPermissionsObject] ADD CONSTRAINT [FK_PermissionPolicyMemberPermissionsObject_TypePermissionObject] FOREIGN KEY ([TypePermissionObject]) REFERENCES [dbo].[PermissionPolicyTypePermissionsObject]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[PermissionPolicyNavigationPermissionsObject] ADD CONSTRAINT [FK_PermissionPolicyNavigationPermissionsObject_Role] FOREIGN KEY ([Role]) REFERENCES [dbo].[PermissionPolicyRole]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[PermissionPolicyObjectPermissionsObject] ADD CONSTRAINT [FK_PermissionPolicyObjectPermissionsObject_TypePermissionObject] FOREIGN KEY ([TypePermissionObject]) REFERENCES [dbo].[PermissionPolicyTypePermissionsObject]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[PermissionPolicyRole] ADD CONSTRAINT [FK_PermissionPolicyRole_ObjectType] FOREIGN KEY ([ObjectType]) REFERENCES [dbo].[XPObjectType]([OID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[PermissionPolicyTypePermissionsObject] ADD CONSTRAINT [FK_PermissionPolicyTypePermissionsObject_Role] FOREIGN KEY ([Role]) REFERENCES [dbo].[PermissionPolicyRole]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[PermissionPolicyUser] ADD CONSTRAINT [FK_PermissionPolicyUser_ObjectType] FOREIGN KEY ([ObjectType]) REFERENCES [dbo].[XPObjectType]([OID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[PermissionPolicyUser] ADD CONSTRAINT [FK_PermissionPolicyUser_Sector_Users] FOREIGN KEY ([Sector_Users]) REFERENCES [rjv].[Sector]([InternalId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[PermissionPolicyUserLoginInfo] ADD CONSTRAINT [FK_PermissionPolicyUserLoginInfo_User] FOREIGN KEY ([User]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[PermissionPolicyUserUsers_PermissionPolicyRoleRoles] ADD CONSTRAINT [FK_PermissionPolicyUserUsers_PermissionPolicyRoleRoles_Roles] FOREIGN KEY ([Roles]) REFERENCES [dbo].[PermissionPolicyRole]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[PermissionPolicyUserUsers_PermissionPolicyRoleRoles] ADD CONSTRAINT [FK_PermissionPolicyUserUsers_PermissionPolicyRoleRoles_Users] FOREIGN KEY ([Users]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Place] ADD CONSTRAINT [FK_rjv_Place_CreatedBy_5E0229A6] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Place] ADD CONSTRAINT [FK_rjv_Place_Operation_Places_33698C3F] FOREIGN KEY ([Operation_Places]) REFERENCES [rjv].[Operation]([InternalId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Place] ADD CONSTRAINT [FK_rjv_Place_ParentPlace_Place_92B0EE7A] FOREIGN KEY ([ParentPlace_Place]) REFERENCES [rjv].[Place]([InternalId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Place] ADD CONSTRAINT [FK_rjv_Place_PlaceStatus_Places_94229868] FOREIGN KEY ([PlaceStatus_Places]) REFERENCES [rjv].[PlaceStatus]([InternalId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Place] ADD CONSTRAINT [FK_rjv_Place_Sector_Places_A3F4DD72] FOREIGN KEY ([Sector_Places]) REFERENCES [rjv].[Sector]([InternalId]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Place] ADD CONSTRAINT [FK_rjv_Place_UpdatedBy_48062DA6] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[PlaceStatus] ADD CONSTRAINT [FK_rjv_PlaceStatus_CreatedBy_FA6D520F] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[PlaceStatus] ADD CONSTRAINT [FK_rjv_PlaceStatus_UpdatedBy_EC69560F] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[QrVaucher] ADD CONSTRAINT [FK_rjv_QrVaucher_CreatedBy_23F2E765] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[QrVaucher] ADD CONSTRAINT [FK_rjv_QrVaucher_UpdatedBy_35F6E365] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Sector] ADD CONSTRAINT [FK_rjv_Sector_CreatedBy_31621315] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[Sector] ADD CONSTRAINT [FK_rjv_Sector_UpdatedBy_27661715] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[WhatsappConversation] ADD CONSTRAINT [FK_rjv_WhatsappConversation_CreatedBy_E1D9F180] FOREIGN KEY ([CreatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [rjv].[WhatsappConversation] ADD CONSTRAINT [FK_rjv_WhatsappConversation_UpdatedBy_F7DDF580] FOREIGN KEY ([UpdatedBy]) REFERENCES [dbo].[PermissionPolicyUser]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[XpoState] ADD CONSTRAINT [FK_XpoState_StateMachine] FOREIGN KEY ([StateMachine]) REFERENCES [dbo].[XpoStateMachine]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[XpoStateAppearance] ADD CONSTRAINT [FK_XpoStateAppearance_State] FOREIGN KEY ([State]) REFERENCES [dbo].[XpoState]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[XpoStateMachine] ADD CONSTRAINT [FK_XpoStateMachine_StartState] FOREIGN KEY ([StartState]) REFERENCES [dbo].[XpoState]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[XpoTransition] ADD CONSTRAINT [FK_XpoTransition_SourceState] FOREIGN KEY ([SourceState]) REFERENCES [dbo].[XpoState]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[XpoTransition] ADD CONSTRAINT [FK_XpoTransition_TargetState] FOREIGN KEY ([TargetState]) REFERENCES [dbo].[XpoState]([Oid]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[XPWeakReference] ADD CONSTRAINT [FK_XPWeakReference_ObjectType] FOREIGN KEY ([ObjectType]) REFERENCES [dbo].[XPObjectType]([OID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

-- AddForeignKey
ALTER TABLE [dbo].[XPWeakReference] ADD CONSTRAINT [FK_XPWeakReference_TargetType] FOREIGN KEY ([TargetType]) REFERENCES [dbo].[XPObjectType]([OID]) ON DELETE NO ACTION ON UPDATE NO ACTION;

COMMIT TRAN;

END TRY
BEGIN CATCH

IF @@TRANCOUNT > 0
BEGIN
    ROLLBACK TRAN;
END;
THROW

END CATCH
