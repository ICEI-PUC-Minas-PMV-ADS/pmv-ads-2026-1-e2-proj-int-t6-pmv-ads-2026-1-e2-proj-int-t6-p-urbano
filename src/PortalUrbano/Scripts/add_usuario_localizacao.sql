-- Execute no banco portal_urbano
ALTER TABLE `usuarios`
  ADD COLUMN `Cidade` VARCHAR(100) NULL DEFAULT NULL AFTER `SenhaHash`,
  ADD COLUMN `Bairro` VARCHAR(100) NULL DEFAULT NULL AFTER `Cidade`,
  ADD COLUMN `Uf` VARCHAR(2) NULL DEFAULT NULL AFTER `Bairro`;

-- Opcional: remover coluna antiga de telefone
-- ALTER TABLE `usuarios` DROP COLUMN `Telefone`;
