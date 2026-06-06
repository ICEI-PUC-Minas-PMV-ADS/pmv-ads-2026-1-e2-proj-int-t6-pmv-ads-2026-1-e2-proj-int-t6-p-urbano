-- Execute no banco portal_urbano antes de usar recuperação de senha
ALTER TABLE `usuarios`
  ADD COLUMN `PasswordResetToken` VARCHAR(128) NULL DEFAULT NULL AFTER `CriadoEm`,
  ADD COLUMN `PasswordResetExpires` DATETIME(6) NULL DEFAULT NULL AFTER `PasswordResetToken`;
