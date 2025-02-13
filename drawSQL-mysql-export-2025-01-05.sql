Create database faisestockdemo_db;
use faisestockdemo_db;

CREATE TABLE `contest` (
    `contest_id` VARCHAR(255) NOT NULL,
    `start_date_time` DATETIME NOT NULL,
    `contest_name` VARCHAR(255) NOT NULL,
    `end_date_time` DATETIME NOT NULL,
    PRIMARY KEY(`contest_id`)
);

CREATE TABLE `wallet` (
    `wallet_id` VARCHAR(255) NOT NULL,
    `balance` DOUBLE NOT NULL,
    `user_id` VARCHAR(255) NOT NULL,
    PRIMARY KEY(`wallet_id`),
    CONSTRAINT `wallet_user_id_foreign` FOREIGN KEY (`user_id`) REFERENCES `user`(`user_id`)
);

CREATE TABLE `user` (
    `user_id` VARCHAR(255) NOT NULL,
    `name` VARCHAR(255) NOT NULL,
    PRIMARY KEY(`user_id`)
);

CREATE TABLE `deposit_history` (
    `deposit_id` VARCHAR(255) NOT NULL,
    `user_id` VARCHAR(255) NOT NULL,
    `amount` DOUBLE NOT NULL,
    `create_at` DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,
    PRIMARY KEY(`deposit_id`),
    CONSTRAINT `deposit_history_user_id_foreign` FOREIGN KEY (`user_id`) REFERENCES `user`(`user_id`)
);

CREATE TABLE `top_user` (
    `user_id` VARCHAR(255) NOT NULL,
     `contest_id` VARCHAR(255) NOT NULL,
    `create_at` datetime NOT NULL,
    `rank` INT NOT NULL,
    `increased_amount` DOUBLE NOT NULL,
    `ROIC` DOUBLE NOT NULL,
    PRIMARY KEY(`user_id`, `create_at`),
    CONSTRAINT `top_user_user_id_foreign` FOREIGN KEY (`user_id`) REFERENCES `user`(`user_id`),
     CONSTRAINT `top_user_contest_id_foreign` FOREIGN KEY (`contest_id`) REFERENCES `contest`(`contest_id`)
);

-- Insert data into user table
INSERT INTO `user` (`user_id`, `name`) VALUES 
('user1', 'Alice'),
('user2', 'Bob');

-- Insert data into wallet table
INSERT INTO `wallet` (`wallet_id`, `balance`,`user_id`) VALUES 
('wallet1', 1000.00,'user1'),
('wallet2', 1000.00,'user2');

-- Insert data into deposit_history table
INSERT INTO `deposit_history` (`deposit_id`, `user_id`, `amount`, `create_at`) VALUES 
('deposit1', 'user1', 500.00, '2025-02-11 10:30:00'),
('deposit3', 'user1', 150.00, '2025-02-11 11:00:00'),
('deposit2', 'user2', 350.00, '2025-02-11 12:15:00'),
('deposit4', 'user2', 110.00, '2025-02-11 13:45:00');

-- Insert additional users
INSERT INTO `user` (`user_id`, `name`) VALUES 
('user3', 'Charlie'),
('user4', 'David'),
('user5', 'Eve'),
('user6', 'Frank'),
('user7', 'Grace'),
('user8', 'Hank'),
('user9', 'Ivy'),
('user10', 'Jack'),
('user11', 'Karen'),
('user12', 'Leo');

-- Insert corresponding wallets
INSERT INTO `wallet` (`wallet_id`, `balance`, `user_id`) VALUES 
('wallet3', 1000.00, 'user3'),
('wallet4', 1000.00, 'user4'),
('wallet5', 1000.00, 'user5'),
('wallet6', 1000.00, 'user6'),
('wallet7', 1000.00, 'user7'),
('wallet8', 1000.00, 'user8'),
('wallet9', 1000.00, 'user9'),
('wallet10', 1000.00, 'user10'),
('wallet11', 1000.00, 'user11'),
('wallet12', 1000.00, 'user12');

-- Insert deposits into deposit_history
INSERT INTO `deposit_history` (`deposit_id`, `user_id`, `amount`, `create_at`) VALUES 
-- Deposits for user3
('deposit5', 'user3', 300.00, '2025-02-11 08:00:00'),
('deposit6', 'user3', 200.00, '2025-02-11 09:15:00'),
('deposit7', 'user3', 500.00, '2025-02-11 10:30:00'),
-- Deposits for user4
('deposit8', 'user4', 400.00, '2025-02-11 11:00:00'),
('deposit9', 'user4', 100.00, '2025-02-11 12:10:00'),
('deposit10', 'user4', 250.00, '2025-02-11 13:45:00'),
-- Deposits for user5
('deposit11', 'user5', 350.00, '2025-02-11 14:30:00'),
('deposit12', 'user5', 150.00, '2025-02-11 15:20:00'),
('deposit13', 'user5', 300.00, '2025-02-11 16:45:00'),
-- Deposits for user6
('deposit14', 'user6', 500.00, '2025-02-11 17:10:00'),
('deposit15', 'user6', 400.00, '2025-02-11 18:25:00'),
('deposit16', 'user6', 100.00, '2025-02-11 19:50:00'),
-- Deposits for user7
('deposit17', 'user7', 600.00, '2025-02-11 20:15:00'),
('deposit18', 'user7', 200.00, '2025-02-11 21:30:00'),
('deposit19', 'user7', 400.00, '2025-02-11 22:45:00'),
-- Deposits for user8
('deposit20', 'user8', 300.00, '2025-02-11 23:00:00'),
('deposit21', 'user8', 150.00, '2025-02-12 00:15:00'),
('deposit22', 'user8', 450.00, '2025-02-12 01:30:00'),
-- Deposits for user9
('deposit23', 'user9', 500.00, '2025-02-12 02:45:00'),
('deposit24', 'user9', 300.00, '2025-02-12 03:50:00'),
('deposit25', 'user9', 200.00, '2025-02-12 04:15:00'),
-- Deposits for user10
('deposit26', 'user10', 250.00, '2025-02-12 05:30:00'),
('deposit27', 'user10', 100.00, '2025-02-12 06:45:00'),
('deposit28', 'user10', 300.00, '2025-02-12 07:50:00'),
-- Deposits for user11
('deposit29', 'user11', 450.00, '2025-02-12 08:15:00'),
('deposit30', 'user11', 200.00, '2025-02-12 09:30:00'),
('deposit31', 'user11', 350.00, '2025-02-12 10:45:00'),
-- Deposits for user12
('deposit32', 'user12', 400.00, '2025-02-12 11:15:00'),
('deposit33', 'user12', 300.00, '2025-02-12 12:30:00'),
('deposit34', 'user12', 200.00, '2025-02-12 13:45:00');





