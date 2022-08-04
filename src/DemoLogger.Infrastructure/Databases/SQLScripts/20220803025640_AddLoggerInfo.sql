CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `logger` (
    `Id` bigint NOT NULL AUTO_INCREMENT,
    `Content` varchar(30) CHARACTER SET utf8mb4 NOT NULL,
    `Title` varchar(20) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK_logger` PRIMARY KEY (`Id`)
) CHARACTER SET=utf8mb4;

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20220803032007_AddLoggerInfo', '6.0.7');

COMMIT;

