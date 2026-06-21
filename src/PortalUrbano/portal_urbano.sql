-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Tempo de geração: 26/05/2026 às 02:56
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
  `Descricao` longtext NOT NULL,
  `Icone` text NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `categorias`
--

INSERT INTO `categorias` (`IdCategoria`, `Nome`, `Descricao`, `Icone`) VALUES
(1, 'Infraestrutura', 'Buracos, asfalto e problemas em vias', '<svg width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"><path d=\"M3 9l9-7 9 7v11a2 2 0 0 1-2 2H5a2 2 0 0 1-2-2z\"></path><polyline points=\"9 22 9 12 15 12 15 22\"></polyline></svg>'),
(2, 'Iluminação Pública', 'Postes sem luz, lâmpadas queimadas', '<svg width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"><circle cx=\"12\" cy=\"12\" r=\"5\"></circle><path d=\"M12 2v2M12 20v2M4.93 4.93l1.41 1.41M17.66 17.66l1.41 1.41M2 12h2M20 12h2M6.34 17.66l-1.41 1.41M19.07 4.93l-1.41 1.41\"></path></svg>'),
(3, 'Limpeza', 'Lixo acumulado, mato alto, entulho', '<svg width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"><path d=\"M3 6h18M19 6v14a2 2 0 0 1-2 2H7a2 2 0 0 1-2-2V6m3 0V4a2 2 0 0 1 2-2h4a2 2 0 0 1 2 2v2M10 11v6M14 11v6\"></path></svg>'),
(4, 'Segurança', 'Vandalismo, furtos, locais perigosos', '<svg width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"><rect x=\"3\" y=\"11\" width=\"18\" height=\"11\" rx=\"2\" ry=\"2\"></rect><path d=\"M7 11V7a5 5 0 0 1 10 0v4\"></path></svg>'),
(5, 'Saneamento', 'Vazamentos de água e esgoto entupido', '<svg width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\"><path d=\"M12 22a7 7 0 0 0 7-7c0-2-1-3.9-3-5.5s-3.5-4-4-6.5c-.5 2.5-2 4.9-4 6.5C6 11.1 5 13 5 15a7 7 0 0 0 7 7z\"></path></svg>');

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

--
-- Despejando dados para a tabela `comentarios`
--

INSERT INTO `comentarios` (`IdComentario`, `IdDenuncia`, `IdUsuario`, `Texto`, `CriadoEm`) VALUES
(2, 4, 1, 'teste', '2026-05-11 22:42:04.290264');

-- --------------------------------------------------------

--
-- Estrutura para tabela `denuncias`
--

CREATE TABLE `denuncias` (
  `IdDenuncia` int(11) NOT NULL,
  `IdUsuario` int(11) NOT NULL,
  `IdCategoria` int(11) NOT NULL,
  `Titulo` longtext NOT NULL,
  `Descricao` longtext DEFAULT NULL,
  `ImagemUrl` longtext DEFAULT NULL,
  `Rua` varchar(255) NOT NULL,
  `Bairro` varchar(255) NOT NULL,
  `Cidade` varchar(255) NOT NULL,
  `Uf` varchar(255) NOT NULL,
  `Cep` varchar(255) NOT NULL,
  `Complemento` varchar(255) DEFAULT NULL,
  `Latitude` decimal(10,6) NOT NULL,
  `Longitude` decimal(10,6) NOT NULL,
  `StatusAnonimo` int(11) NOT NULL DEFAULT 0,
  `Status` longtext NOT NULL,
  `CriadoEm` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `denuncias`
--

INSERT INTO `denuncias` (`IdDenuncia`, `IdUsuario`, `IdCategoria`, `Titulo`, `Descricao`, `ImagemUrl`, `Rua`, `Bairro`, `Cidade`, `Uf`, `Cep`, `Complemento`, `Latitude`, `Longitude`, `StatusAnonimo`, `Status`, `CriadoEm`) VALUES
(1, 1, 1, 'Buraco na Via', 'Estava transitando pela rodovia e vi que está com este buraco enorme atrapalhando a passagem. FIQUEM ATENTOS!', 'https://jsyjgvatzaxogazcmqaf.supabase.co/storage/v1/object/public/portal_urbano_img/denuncia_1_0.png', 'Jose Freitas de Oliveira', 'Parque Durval de Barros', 'Ibirité', 'MG', '32440000', 'Proximo ao posto de saúde', -19.981144, -44.062178, 1, 'Aberta', '2026-05-08 11:11:21.378512'),
(2, 1, 3, 'Árvore caída', 'Gente, cuidado! ⚠️\r\n\r\nAcabou de cair uma árvore aqui e a passagem ficou perigosa 😥\r\nEvitem passar pelo local até resolverem a situação, porque está interditado e pode oferecer risco.\r\n\r\nCompartilhem pra mais pessoas ficarem sabendo', 'https://jsyjgvatzaxogazcmqaf.supabase.co/storage/v1/object/public/portal_urbano_img/denuncia_2_0.png', 'Rua Doutor Benjamin Rabello', 'Conjunto Santa Maria', 'Belo Horizonte', 'MG', '30380460', 'Próximo a padaria Paganinni', -20.032582, -44.058584, 0, 'Aberta', '2026-05-08 11:20:43.395443'),
(3, 1, 2, 'Falta de iluminação na rua', '⚠️ Pessoal, atenção! ⚠️\r\n\r\nA iluminação aqui da região está apagada e o local ficou bem escuro 😕\r\nQuem puder, evite passar por aqui à noite até resolverem a situação, principalmente por questão de segurança.', 'https://jsyjgvatzaxogazcmqaf.supabase.co/storage/v1/object/public/portal_urbano_img/denuncia_3_0.png', 'Avenida do Contorno', 'Funcionários', 'Belo Horizonte', 'MG', '30110031', 'Próximo ao supermercado BH', -19.934295, -43.924915, 0, 'Aberta', '2026-05-08 11:24:56.456468'),
(4, 1, 3, 'Teste', 'Teste21', 'https://jsyjgvatzaxogazcmqaf.supabase.co/storage/v1/object/public/portal_urbano_img/denuncia_4_0.jpg', 'Rua Bahia', 'Vila Rica', 'Sabará', 'MG', '34585180', 'pe de banana', -19.889344, -43.852426, 1, 'Aberta', '2026-05-11 22:41:50.239137');

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

--
-- Despejando dados para a tabela `gostei`
--

INSERT INTO `gostei` (`LikeId`, `UsuarioId`, `DenunciaId`, `CriadoEm`) VALUES
(1, 1, 3, '2026-05-08 11:26:16'),
(2, 1, 4, '2026-05-11 22:41:56');

-- --------------------------------------------------------

--
-- Estrutura para tabela `reportes`
--

CREATE TABLE `reportes` (
  `IdReporte` int(11) NOT NULL,
  `IdDenuncia` int(11) NOT NULL,
  `IdUsuario` int(11) NOT NULL,
  `Motivo` longtext NOT NULL,
  `detalhes` longtext DEFAULT NULL,
  `CriadoEm` datetime(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `reportes`
--

INSERT INTO `reportes` (`IdReporte`, `IdDenuncia`, `IdUsuario`, `Motivo`, `detalhes`, `CriadoEm`) VALUES
(1, 4, 1, 'Denuncia reportada pelo feed', NULL, '2026-05-25 22:35:32.749074');

-- --------------------------------------------------------

--
-- Estrutura para tabela `usuarios`
--

CREATE TABLE `usuarios` (
  `IdUsuario` int(11) NOT NULL,
  `Nome` longtext NOT NULL,
  `Email` longtext NOT NULL,
  `SenhaHash` longtext NOT NULL,
  `Cidade` varchar(100) DEFAULT NULL,
  `Bairro` varchar(100) DEFAULT NULL,
  `Uf` varchar(2) DEFAULT NULL,
  `Avisos` int(11) NOT NULL DEFAULT 0,
  `Banido` tinyint(4) NOT NULL DEFAULT 0,
  `CriadoEm` datetime(6) DEFAULT current_timestamp(6),
  `PasswordResetToken` varchar(128) DEFAULT NULL,
  `PasswordResetExpires` datetime(6) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Despejando dados para a tabela `usuarios`
--

INSERT INTO `usuarios` (`IdUsuario`, `Nome`, `Email`, `SenhaHash`, `Cidade`, `Bairro`, `Uf`, `Avisos`, `Banido`, `CriadoEm`) VALUES
(1, 'Cidadão Exemplo', 'cidadao@portalurbano.com.br', 'senha_hash_123', 'Belo Horizonte', 'Centro', 'MG', 2, 0, '2026-05-05 20:12:15.855812'),
(2, 'Usuário Real', 'real@portal.com', '123', NULL, NULL, NULL, 0, 0, '2026-05-06 00:14:02.042699');

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
  MODIFY `IdComentario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de tabela `denuncias`
--
ALTER TABLE `denuncias`
  MODIFY `IdDenuncia` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de tabela `gostei`
--
ALTER TABLE `gostei`
  MODIFY `LikeId` bigint(20) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de tabela `reportes`
--
ALTER TABLE `reportes`
  MODIFY `IdReporte` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=2;

--
-- AUTO_INCREMENT de tabela `usuarios`
--
ALTER TABLE `usuarios`
  MODIFY `IdUsuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

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
