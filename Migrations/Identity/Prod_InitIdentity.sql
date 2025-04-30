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
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20250430064218_InitIdentity') THEN

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
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20250430064218_InitIdentity') THEN

    CREATE TABLE `AspNetRoles` (
        `id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `name` varchar(256) CHARACTER SET utf8mb4 NULL,
        `normalized_name` varchar(256) CHARACTER SET utf8mb4 NULL,
        `concurrency_stamp` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `pk_asp_net_roles` PRIMARY KEY (`id`)
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
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20250430064218_InitIdentity') THEN

    CREATE TABLE `AspNetUsers` (
        `id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `user_name` varchar(256) CHARACTER SET utf8mb4 NULL,
        `normalized_user_name` varchar(256) CHARACTER SET utf8mb4 NULL,
        `email` varchar(256) CHARACTER SET utf8mb4 NULL,
        `normalized_email` varchar(256) CHARACTER SET utf8mb4 NULL,
        `email_confirmed` tinyint(1) NOT NULL,
        `password_hash` longtext CHARACTER SET utf8mb4 NULL,
        `security_stamp` longtext CHARACTER SET utf8mb4 NULL,
        `concurrency_stamp` longtext CHARACTER SET utf8mb4 NULL,
        `phone_number` longtext CHARACTER SET utf8mb4 NULL,
        `phone_number_confirmed` tinyint(1) NOT NULL,
        `two_factor_enabled` tinyint(1) NOT NULL,
        `lockout_end` datetime(6) NULL,
        `lockout_enabled` tinyint(1) NOT NULL,
        `access_failed_count` int NOT NULL,
        CONSTRAINT `pk_asp_net_users` PRIMARY KEY (`id`)
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
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20250430064218_InitIdentity') THEN

    CREATE TABLE `AspNetRoleClaims` (
        `id` int NOT NULL AUTO_INCREMENT,
        `role_id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `claim_type` longtext CHARACTER SET utf8mb4 NULL,
        `claim_value` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `pk_asp_net_role_claims` PRIMARY KEY (`id`),
        CONSTRAINT `fk_asp_net_role_claims_asp_net_roles_role_id` FOREIGN KEY (`role_id`) REFERENCES `AspNetRoles` (`id`) ON DELETE CASCADE
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
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20250430064218_InitIdentity') THEN

    CREATE TABLE `AspNetUserClaims` (
        `id` int NOT NULL AUTO_INCREMENT,
        `user_id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `claim_type` longtext CHARACTER SET utf8mb4 NULL,
        `claim_value` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `pk_asp_net_user_claims` PRIMARY KEY (`id`),
        CONSTRAINT `fk_asp_net_user_claims_asp_net_users_user_id` FOREIGN KEY (`user_id`) REFERENCES `AspNetUsers` (`id`) ON DELETE CASCADE
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
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20250430064218_InitIdentity') THEN

    CREATE TABLE `AspNetUserLogins` (
        `login_provider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `provider_key` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `provider_display_name` longtext CHARACTER SET utf8mb4 NULL,
        `user_id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `pk_asp_net_user_logins` PRIMARY KEY (`login_provider`, `provider_key`),
        CONSTRAINT `fk_asp_net_user_logins_asp_net_users_user_id` FOREIGN KEY (`user_id`) REFERENCES `AspNetUsers` (`id`) ON DELETE CASCADE
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
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20250430064218_InitIdentity') THEN

    CREATE TABLE `AspNetUserRoles` (
        `user_id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `role_id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        CONSTRAINT `pk_asp_net_user_roles` PRIMARY KEY (`user_id`, `role_id`),
        CONSTRAINT `fk_asp_net_user_roles_asp_net_roles_role_id` FOREIGN KEY (`role_id`) REFERENCES `AspNetRoles` (`id`) ON DELETE CASCADE,
        CONSTRAINT `fk_asp_net_user_roles_asp_net_users_user_id` FOREIGN KEY (`user_id`) REFERENCES `AspNetUsers` (`id`) ON DELETE CASCADE
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
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20250430064218_InitIdentity') THEN

    CREATE TABLE `AspNetUserTokens` (
        `user_id` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `login_provider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `name` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
        `value` longtext CHARACTER SET utf8mb4 NULL,
        CONSTRAINT `pk_asp_net_user_tokens` PRIMARY KEY (`user_id`, `login_provider`, `name`),
        CONSTRAINT `fk_asp_net_user_tokens_asp_net_users_user_id` FOREIGN KEY (`user_id`) REFERENCES `AspNetUsers` (`id`) ON DELETE CASCADE
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
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20250430064218_InitIdentity') THEN

    CREATE INDEX `ix_asp_net_role_claims_role_id` ON `AspNetRoleClaims` (`role_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20250430064218_InitIdentity') THEN

    CREATE UNIQUE INDEX `RoleNameIndex` ON `AspNetRoles` (`normalized_name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20250430064218_InitIdentity') THEN

    CREATE INDEX `ix_asp_net_user_claims_user_id` ON `AspNetUserClaims` (`user_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20250430064218_InitIdentity') THEN

    CREATE INDEX `ix_asp_net_user_logins_user_id` ON `AspNetUserLogins` (`user_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20250430064218_InitIdentity') THEN

    CREATE INDEX `ix_asp_net_user_roles_role_id` ON `AspNetUserRoles` (`role_id`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20250430064218_InitIdentity') THEN

    CREATE INDEX `EmailIndex` ON `AspNetUsers` (`normalized_email`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20250430064218_InitIdentity') THEN

    CREATE UNIQUE INDEX `UserNameIndex` ON `AspNetUsers` (`normalized_user_name`);

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

DROP PROCEDURE IF EXISTS MigrationsScript;
DELIMITER //
CREATE PROCEDURE MigrationsScript()
BEGIN
    IF NOT EXISTS(SELECT 1 FROM `__EFMigrationsHistory` WHERE `migration_id` = '20250430064218_InitIdentity') THEN

    INSERT INTO `__EFMigrationsHistory` (`migration_id`, `product_version`)
    VALUES ('20250430064218_InitIdentity', '8.0.2');

    END IF;
END //
DELIMITER ;
CALL MigrationsScript();
DROP PROCEDURE MigrationsScript;

COMMIT;

