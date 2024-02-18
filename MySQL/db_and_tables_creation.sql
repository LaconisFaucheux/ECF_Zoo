CREATE DATABASE IF NOT EXISTS ZOO;

use ZOO;

DROP TABLE IF EXISTS animal_image;
DROP TABLE IF EXISTS habitat_image;
DROP TABLE IF EXISTS opening_hours;
DROP TABLE IF EXISTS service;
DROP TABLE IF EXISTS review;
DROP TABLE IF EXISTS animal;
DROP TABLE IF EXISTS species;
DROP TABLE IF EXISTS diet;
DROP TABLE IF EXISTS habitat;

CREATE TABLE opening_hours (
	id TINYINT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    day_of_week VARCHAR(10),
	morning_opening TIME,
	morning_closing TIME,
	afternoon_opening TIME,
	afternoon_closing TIME
);

CREATE TABLE review (
	id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    pseudo VARCHAR(100),
    content TEXT,
    is_validated BOOL,
    note TINYINT UNSIGNED
);

CREATE TABLE zoo_service (
	id TINYINT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    service_name VARCHAR(150),
    service_description TEXT,
    service_full_price TINYINT UNSIGNED,
    service_child_price TINYINT UNSIGNED,
    service_special_price TINYINT UNSIGNED
);

CREATE TABLE diet (
	id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    diet_name VARCHAR(50)
);

CREATE TABLE habitat (
	id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    habitat_name VARCHAR(100),
    habitat_description TEXT    
);

CREATE TABLE species (
	id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    species_name VARCHAR(150),
    scientific_name VARCHAR(150),
    species_description TEXT,
    size_infos VARCHAR(150),
    weight_infos VARCHAR(150),  
    lifespan TINYINT,
    species_diet_id INT,
    species_habitat_id INT,
    FOREIGN KEY (species_diet_id) REFERENCES diet(id),
    FOREIGN KEY (species_habitat_id) REFERENCES habitat(id)
);

CREATE TABLE species_habitat (
    species_id INT, 
    habitat_id INT,
    FOREIGN KEY (species_id) REFERENCES species(id),
    FOREIGN KEY (habitat_id) REFERENCES habitat(id),
    PRIMARY KEY (species_id, habitat_id),
    UNIQUE (species_id, habitat_id)
);

CREATE TABLE habitat_image (
	id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    slug VARCHAR(150) UNIQUE,
    habitat_id INT,
    FOREIGN KEY (habitat_id) REFERENCES habitat(id) ON DELETE CASCADE
);

CREATE TABLE animal (
	id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    animal_name VARCHAR(50) UNIQUE,
    is_male BOOLEAN,
    animal_species INT,
    FOREIGN KEY (animal_species) REFERENCES species(id)
);

CREATE TABLE animal_image (
	id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
    slug VARCHAR(150) UNIQUE NOT NULL,
    animal_id INT NOT NULL,    
    FOREIGN KEY (animal_id) REFERENCES animal(id) ON DELETE CASCADE
);
