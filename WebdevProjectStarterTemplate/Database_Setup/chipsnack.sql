-- phpMyAdmin SQL Dump
-- version 5.1.1
-- https://www.phpmyadmin.net/
--
-- Host: 127.0.0.1
-- Gegenereerd op: 10 mei 2023 om 12:42
-- Serverversie: 10.4.20-MariaDB
-- PHP-versie: 8.0.9

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Database: `chipsnack`
--

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `bestelling`
--

CREATE TABLE `bestelling` (
  `id` int(11) NOT NULL,
  `gebruikerId` int(11) NOT NULL,
  `aantal` int(11) NOT NULL,
  `opmerking` varchar(255) NOT NULL,
  `weeknr` int(11) NOT NULL,
  `jaar` int(11) NOT NULL,
  `herhalen` smallint(6) NOT NULL,
  `snackId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `categorie`
--

CREATE TABLE `categorie` (
  `id` int(11) NOT NULL,
  `naam` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `gebruiker`
--

CREATE TABLE `gebruiker` (
  `id` int(11) NOT NULL,
  `naam` varchar(32) NOT NULL,
  `email` varchar(64) NOT NULL,
  `microsoftId` int(11) NOT NULL,
  `wachtwoord` varchar(255) NOT NULL,
  `rol` smallint(6) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `snack`
--

CREATE TABLE `snack` (
  `id` int(11) NOT NULL,
  `naam` varchar(64) NOT NULL,
  `prijs` int(11) NOT NULL,
  `beschrijving` varchar(255) NOT NULL,
  `snackbarId` int(11) DEFAULT NULL,
  `categorieId` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

-- --------------------------------------------------------

--
-- Tabelstructuur voor tabel `snackbar`
--

CREATE TABLE `snackbar` (
  `id` int(11) NOT NULL,
  `naam` varchar(64) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4;

--
-- Indexen voor geëxporteerde tabellen
--

--
-- Indexen voor tabel `bestelling`
--
ALTER TABLE `bestelling`
  ADD PRIMARY KEY (`id`),
  ADD KEY `gebruikerId` (`gebruikerId`),
  ADD KEY `snackId` (`snackId`);

--
-- Indexen voor tabel `categorie`
--
ALTER TABLE `categorie`
  ADD PRIMARY KEY (`id`);

--
-- Indexen voor tabel `gebruiker`
--
ALTER TABLE `gebruiker`
  ADD PRIMARY KEY (`id`);

--
-- Indexen voor tabel `snack`
--
ALTER TABLE `snack`
  ADD PRIMARY KEY (`id`),
  ADD KEY `snackbarId` (`snackbarId`),
  ADD KEY `categorieId` (`categorieId`);

--
-- Indexen voor tabel `snackbar`
--
ALTER TABLE `snackbar`
  ADD PRIMARY KEY (`id`);

--
-- AUTO_INCREMENT voor geëxporteerde tabellen
--

--
-- AUTO_INCREMENT voor een tabel `bestelling`
--
ALTER TABLE `bestelling`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `categorie`
--
ALTER TABLE `categorie`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `gebruiker`
--
ALTER TABLE `gebruiker`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `snack`
--
ALTER TABLE `snack`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT voor een tabel `snackbar`
--
ALTER TABLE `snackbar`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- Beperkingen voor geëxporteerde tabellen
--

--
-- Beperkingen voor tabel `bestelling`
--
ALTER TABLE `bestelling`
  ADD CONSTRAINT `bestelling_ibfk_1` FOREIGN KEY (`snackId`) REFERENCES `snack` (`id`) ON DELETE SET NULL ON UPDATE CASCADE,
  ADD CONSTRAINT `bestelling_ibfk_2` FOREIGN KEY (`gebruikerId`) REFERENCES `gebruiker` (`id`) ON UPDATE CASCADE;

--
-- Beperkingen voor tabel `snack`
--
ALTER TABLE `snack`
  ADD CONSTRAINT `snack_ibfk_1` FOREIGN KEY (`categorieId`) REFERENCES `categorie` (`id`) ON DELETE SET NULL ON UPDATE CASCADE,
  ADD CONSTRAINT `snack_ibfk_2` FOREIGN KEY (`snackbarId`) REFERENCES `snackbar` (`id`) ON DELETE SET NULL ON UPDATE CASCADE;


CREATE TABLE `budget` (
    budget int PRIMARY KEY NOT NULL
);

SET sql_mode=(SELECT REPLACE(@@sql_mode,'ONLY_FULL_GROUP_BY',''));


COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
