CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `migration_id` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `product_version` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `pk___ef_migrations_history` PRIMARY KEY (`migration_id`)
) CHARACTER SET=utf8mb4;

START TRANSACTION;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20250430065259_InitUsers') THEN

    ALTER DATABASE CHARACTER SET utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20250430065259_InitUsers') THEN

    CREATE TABLE `users` (
        `id` char(36) COLLATE ascii_general_ci NOT NULL,
        `first_name` longtext CHARACTER SET utf8mb4 NOT NULL,
        `last_name` longtext CHARACTER SET utf8mb4 NOT NULL,
        `email` longtext CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `pk_users` PRIMARY KEY (`id`)
    ) CHARACTER SET=utf8mb4;

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20250430065259_InitUsers') THEN

    INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
    VALUES ('20250430065259_InitUsers', '8.0.2');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

