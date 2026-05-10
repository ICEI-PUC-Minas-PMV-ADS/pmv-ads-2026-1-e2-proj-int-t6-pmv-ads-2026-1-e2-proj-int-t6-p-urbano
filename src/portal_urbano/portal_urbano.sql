-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 06/05/2026 às 02:02
-- Versão do servidor: 10.4.32-MariaDB
-- Versão do PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Banco de dados: `portal_urbano`
--

-- --------------------------------------------------------

--
-- Estrutura para tabela `categorias`
--

CREATE TABLE `categorias` (
  `IdCategoria` int(11) NOT NULL,
  `Nome` longtext NOT NULL,
  `Descricao` longtext NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `categorias`
--

INSERT INTO `categorias` (`IdCategoria`, `Nome`, `Descricao`) VALUES
(1, 'Infraestrutura', 'Buracos, asfalto e problemas em vias'),
(2, 'Iluminação Pública', 'Postes sem luz, lâmpadas queimadas'),
(3, 'Limpeza', 'Lixo acumulado, mato alto, entulho'),
(4, 'Segurança', 'Vandalismo, furtos, locais perigosos'),
(5, 'Saneamento', 'Vazamentos de água e esgoto entupido');

-- --------------------------------------------------------

--
-- Estrutura para tabela `comentarios`
--

CREATE TABLE `comentarios` (
  `IdComentario` int(11) NOT NULL,
  `IdDenuncia` int(11) NOT NULL,
  `IdUsuario` int(11) NOT NULL,
  `Texto` longtext NOT NULL,
  `CriadoEm` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura para tabela `denuncias`
--

CREATE TABLE `denuncias` (
  `IdDenuncia` int(11) NOT NULL,
  `IdUsuario` int(11) NOT NULL,
  `IdCategoria` int(11) NOT NULL,
  `Titulo` longtext NOT NULL,
  `Descricao` longtext NOT NULL,
  `ImagemUrl` longtext DEFAULT NULL,
  `Rua` varchar(255) NOT NULL,
  `Bairro` varchar(255) NOT NULL,
  `Cidade` varchar(255) NOT NULL,
  `Uf` varchar(255) NOT NULL,
  `Cep` varchar(255) NOT NULL,
  `Complemento` varchar(255) NOT NULL,
  `Latitude` decimal(18,6) NOT NULL,
  `Longitude` decimal(18,6) NOT NULL,
  `Status` longtext NOT NULL,
  `CriadoEm` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura para tabela `gostei`
--

CREATE TABLE `gostei` (
  `LikeId` bigint(20) NOT NULL,
  `UsuarioId` bigint(20) NOT NULL,
  `DenunciaId` bigint(20) NOT NULL,
  `CriadoEm` datetime DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura para tabela `reportes`
--

CREATE TABLE `reportes` (
  `IdReporte` int(11) NOT NULL,
  `IdDenuncia` int(11) NOT NULL,
  `IdUsuario` int(11) NOT NULL,
  `Motivo` longtext NOT NULL,
  `CriadoEm` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- --------------------------------------------------------

--
-- Estrutura para tabela `usuarios`
--

CREATE TABLE `usuarios` (
  `IdUsuario` int(11) NOT NULL,
  `Nome` longtext NOT NULL,
  `Email` longtext NOT NULL,
  `SenhaHash` longtext NOT NULL,
  `Telefone` longtext DEFAULT NULL,
  `CriadoEm` datetime(6) DEFAULT current_timestamp(6)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `usuarios`
--

INSERT INTO `usuarios` (`IdUsuario`, `Nome`, `Email`, `SenhaHash`, `Telefone`, `CriadoEm`) VALUES
(1, 'Cidadão Exemplo', 'cidadao@portalurbano.com.br', 'senha_hash_123', '(11) 99999-9999', '2026-05-05 20:12:15.855812');

-- --------------------------------------------------------

--
-- Estrutura para tabela `__efmigrationshistory`
--

CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `__efmigrationshistory`
--

INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
('20260504232808_InitialCreate', '9.0.0');

--
-- Índices para tabelas despejadas
--

--
-- Índices de tabela `categorias`
--
ALTER TABLE `categorias`
  ADD PRIMARY KEY (`IdCategoria`);

--
-- Índices de tabela `comentarios`
--
ALTER TABLE `comentarios`
  ADD PRIMARY KEY (`IdComentario`),
  ADD KEY `IX_Comentarios_IdDenuncia` (`IdDenuncia`),
  ADD KEY `IX_Comentarios_IdUsuario` (`IdUsuario`);

--
-- Índices de tabela `denuncias`
--
ALTER TABLE `denuncias`
  ADD PRIMARY KEY (`IdDenuncia`),
  ADD KEY `IX_Denuncias_IdCategoria` (`IdCategoria`),
  ADD KEY `IX_Denuncias_IdUsuario` (`IdUsuario`);

--
-- Índices de tabela `gostei`
--
ALTER TABLE `gostei`
  ADD PRIMARY KEY (`LikeId`);

--
-- Índices de tabela `reportes`
--
ALTER TABLE `reportes`
  ADD PRIMARY KEY (`IdReporte`),
  ADD KEY `IX_Reportes_IdDenuncia` (`IdDenuncia`),
  ADD KEY `IX_Reportes_IdUsuario` (`IdUsuario`);

--
-- Índices de tabela `usuarios`
--
ALTER TABLE `usuarios`
  ADD PRIMARY KEY (`IdUsuario`);

--
-- Índices de tabela `__efmigrationshistory`
--
ALTER TABLE `__efmigrationshistory`
  ADD PRIMARY KEY (`MigrationId`);

--
-- AUTO_INCREMENT para tabelas despejadas
--

--
-- AUTO_INCREMENT de tabela `categorias`
--
ALTER TABLE `categorias`
  MODIFY `IdCategoria` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de tabela `comentarios`
--
ALTER TABLE `comentarios`
  MODIFY `IdComentario` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `denuncias`
--
ALTER TABLE `denuncias`
  MODIFY `IdDenuncia` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `gostei`
--
ALTER TABLE `gostei`
  MODIFY `LikeId` bigint(20) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `reportes`
--
ALTER TABLE `reportes`
  MODIFY `IdReporte` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT de tabela `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `IdUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- Restrições para tabelas despejadas
--

--
-- Restrições para tabelas `comentarios`
--
ALTER TABLE `comentarios`
  ADD CONSTRAINT `FK_Comentarios_Denuncias_IdDenuncia` FOREIGN KEY (`IdDenuncia`) REFERENCES `denuncias` (`IdDenuncia`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Comentarios_Usuarios_IdUsuario` FOREIGN KEY (`IdUsuario`) REFERENCES `usuarios` (`IdUsuario`);

--
-- Restrições para tabelas `denuncias`
--
ALTER TABLE `denuncias`
  ADD CONSTRAINT `FK_Denuncias_Categorias_IdCategoria` FOREIGN KEY (`IdCategoria`) REFERENCES `categorias` (`IdCategoria`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Denuncias_Usuarios_IdUsuario` FOREIGN KEY (`IdUsuario`) REFERENCES `usuarios` (`IdUsuario`) ON DELETE CASCADE;

--
-- Restrições para tabelas `reportes`
--
ALTER TABLE `reportes`
  ADD CONSTRAINT `FK_Reportes_Denuncias_IdDenuncia` FOREIGN KEY (`IdDenuncia`) REFERENCES `denuncias` (`IdDenuncia`) ON DELETE CASCADE,
  ADD CONSTRAINT `FK_Reportes_Usuarios_IdUsuario` FOREIGN KEY (`IdUsuario`) REFERENCES `usuarios` (`IdUsuario`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
