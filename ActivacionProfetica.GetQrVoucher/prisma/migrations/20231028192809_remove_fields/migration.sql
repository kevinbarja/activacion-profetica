/*
  Warnings:

  - You are about to drop the column `GenerationDate` on the `QrVaucher` table. All the data in the column will be lost.
  - You are about to drop the column `TransactionDate` on the `QrVaucher` table. All the data in the column will be lost.

*/
BEGIN TRY

BEGIN TRAN;

-- AlterTable
ALTER TABLE [rjv].[QrVaucher] DROP COLUMN [GenerationDate],
[TransactionDate];

COMMIT TRAN;

END TRY
BEGIN CATCH

IF @@TRANCOUNT > 0
BEGIN
    ROLLBACK TRAN;
END;
THROW

END CATCH
