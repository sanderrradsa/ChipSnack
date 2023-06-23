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

create table budget
(
    id        int not null,
    budgetMax int not null
);

create table categorie
(
    id   int auto_increment
        primary key,
    naam varchar(64) not null
);

create table gebruiker
(
    id         int auto_increment
        primary key,
    naam       varchar(32)  not null,
    email      varchar(64)  not null,
    wachtwoord varchar(255) not null,
    rol        smallint     not null
);

create table snackbar
(
    id   int auto_increment
        primary key,
    naam varchar(64) not null
);

create table snack
(
    id           int auto_increment
        primary key,
    naam         varchar(64)  not null,
    prijs        int          not null,
    beschrijving varchar(255) null,
    snackbarId   int          null,
    categorieId  int          null,
    constraint snack_ibfk_1
        foreign key (categorieId) references categorie (id)
            on update cascade on delete set null,
    constraint snack_ibfk_2
        foreign key (snackbarId) references snackbar (id)
            on update cascade on delete set null
);

create table bestelling
(
    id          int auto_increment
        primary key,
    gebruikerId int               not null,
    aantal      int               not null,
    opmerking   varchar(255)      null,
    weeknr      int               not null,
    jaar        int               not null,
    herhalen    tinyint default 0 null,
    snackId     int               null,
    bevestigd   tinyint default 0 null,
    constraint bestelling_ibfk_2
        foreign key (gebruikerId) references gebruiker (id)
            on update cascade,
    constraint bestelling_ibfk_3
        foreign key (snackId) references snack (id)
            on delete cascade
);

create index gebruikerId
    on bestelling (gebruikerId);

create index snackId
    on bestelling (snackId);

create index categorieId
    on snack (categorieId);

create index snackbarId
    on snack (snackbarId);


COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
