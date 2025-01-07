Create database faisestockdemo_db;
use faisestockdemo_db;
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
    PRIMARY KEY(`deposit_id`),
    CONSTRAINT `deposit_history_user_id_foreign` FOREIGN KEY (`user_id`) REFERENCES `user`(`user_id`)
);

CREATE TABLE `top_user` (
    `user_id` VARCHAR(255) NOT NULL,
    `create_at` DATE NOT NULL,
    `rank` INT NOT NULL,
    `increased_amount` DOUBLE NOT NULL,
    `ROIC` DOUBLE NOT NULL,
    PRIMARY KEY(`user_id`, `create_at`),
    CONSTRAINT `top_user_user_id_foreign` FOREIGN KEY (`user_id`) REFERENCES `user`(`user_id`)
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
INSERT INTO `deposit_history` (`deposit_id`, `user_id`, `amount`) VALUES 
('deposit1', 'user1', 500.00),
('deposit3', 'user1', 150.00),
('deposit2', 'user2', 350.00),
('deposit4', 'user2', 110.00);

-- Insert data into top_user table
INSERT INTO `top_user` (`user_id`, `create_at`, `rank`, `increased_amount`, `ROIC`) VALUES 
('user1', '2025-01-05', 1, 500.00, 50.00),
('user2', '2025-01-05', 2, 700.00, 35.00);
