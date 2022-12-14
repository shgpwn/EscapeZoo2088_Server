CREATE TABLE `users` (
	`userid` INT(11) NOT NULL AUTO_INCREMENT,
	`nickname` VARCHAR(50) NULL DEFAULT NULL COLLATE 'utf8_general_ci',
	`win` INT(11) NULL DEFAULT NULL,
	`loss` INT(11) NULL DEFAULT NULL,
	`draw` INT(11) NULL DEFAULT NULL,
	`point` INT(11) NULL DEFAULT NULL,
	PRIMARY KEY (`userid`) USING BTREE,
	UNIQUE INDEX `UX_users_nickname` (`nickname`) USING BTREE
)
COLLATE='utf8_general_ci'
ENGINE=InnoDB
;
