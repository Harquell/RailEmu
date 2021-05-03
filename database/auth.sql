-- --------------------------------------------------------
-- Hôte :                        localhost
-- Version du serveur:           5.7.19 - MySQL Community Server (GPL)
-- SE du serveur:                Win64
-- HeidiSQL Version:             9.4.0.5125
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;

-- Export de la structure de la table auth. account
CREATE TABLE IF NOT EXISTS `account` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `Username` varchar(50) NOT NULL,
  `Password` varchar(50) NOT NULL,
  `Pseudo` varchar(50) DEFAULT NULL,
  `isAdmin` tinyint(1) NOT NULL DEFAULT '0',
  `Question` varchar(50) NOT NULL DEFAULT 'type "yes" :',
  `Answer` varchar(50) NOT NULL DEFAULT 'yes',
  `EndSub` datetime DEFAULT NULL,
  `Banned` tinyint(1) NOT NULL DEFAULT '0',
  `EndBan` datetime DEFAULT NULL,
  PRIMARY KEY (`Id`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=latin1;

-- Les données exportées n'étaient pas sélectionnées.
-- Export de la structure de la table auth. ipblock
CREATE TABLE IF NOT EXISTS `ipblock` (
  `IpAddress` varchar(50) NOT NULL,
  PRIMARY KEY (`IpAddress`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Les données exportées n'étaient pas sélectionnées.
-- Export de la structure de la table auth. serverlist
CREATE TABLE IF NOT EXISTS `serverlist` (
  `ServerId` int(11) NOT NULL,
  `Name` varchar(50) NOT NULL,
  `Status` int(11) NOT NULL,
  `Completion` int(11) NOT NULL,
  `Usable` int(11) NOT NULL,
  PRIMARY KEY (`ServerId`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Les données exportées n'étaient pas sélectionnées.
-- Export de la structure de la table auth. server_characters
CREATE TABLE IF NOT EXISTS `server_characters` (
  `ServerId` int(11) NOT NULL,
  `AccountId` int(11) NOT NULL,
  `NbChar` int(11) NOT NULL DEFAULT '0'
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- Les données exportées n'étaient pas sélectionnées.
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IF(@OLD_FOREIGN_KEY_CHECKS IS NULL, 1, @OLD_FOREIGN_KEY_CHECKS) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
