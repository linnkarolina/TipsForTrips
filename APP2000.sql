-- MySQL Script generated by MySQL Workbench
-- Mon Jan 20 13:24:00 2020
-- Model: New Model    Version: 1.0
-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`User`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`User` (
  `username` VARCHAR(45) NOT NULL,
  `password` VARCHAR(45) NULL,
  `location` VARCHAR(45) NULL,
  `email` VARCHAR(45) NULL,
  `full_name` VARCHAR(45) NULL,
  `phone_NR` VARCHAR(45) NULL,
  PRIMARY KEY (`username`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Admin`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Admin` (
  `ID_admin` INT NOT NULL,
  `username` VARCHAR(45) NULL,
  `password` VARCHAR(45) NULL,
  PRIMARY KEY (`ID_admin`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Area`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Area` (
  `ID_area` VARCHAR(45) NOT NULL,
  `admin_ID_admin` INT NOT NULL,
  `User_username` VARCHAR(45) NOT NULL,
  
  PRIMARY KEY (`ID_area`),

  CONSTRAINT `fk_area_admin1`
    FOREIGN KEY (`admin_ID_admin`)
    REFERENCES `mydb`.`Admin` (`ID_admin`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_area_User1`
    FOREIGN KEY (`User_username`)
    REFERENCES `mydb`.`User` (`username`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Trip`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Trip` (
  `area` INT NOT NULL,
  `area_ID_area` VARCHAR(45) NOT NULL,
  `type_of_trip` VARCHAR(45) NULL,
  `length_of_trip` VARCHAR(45) NULL,
  `difficulty` VARCHAR(45) NULL,
  `description` VARCHAR(45) NULL,
  `location` VARCHAR(45) NULL,
  `attraction_website` VARCHAR(45) NULL,
  `image` VARCHAR(45) NULL,
  PRIMARY KEY (`area`, `area_ID_area`),
  CONSTRAINT `fk_trip_area1`
    FOREIGN KEY (`area_ID_area`)
    REFERENCES `mydb`.`Area` (`ID_area`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Map_route`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Map_route` (
  `ID_map route` INT NOT NULL,
  `trip_ID_area` INT NOT NULL,
  `route_name` VARCHAR(45) NULL,
  `route_distance` VARCHAR(45) NULL,
  `longditude-latitude` VARCHAR(45) NULL,
  PRIMARY KEY (`ID_map route`, `trip_ID_area`),
  CONSTRAINT `fk_Map routes_trip1`
    FOREIGN KEY (`trip_ID_area`)
    REFERENCES `mydb`.`Trip` (`area`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Feedback`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Feedback` (
  `ID_feedback` INT NOT NULL,
  `feedback_description` VARCHAR(500) NULL,
  `user_username` VARCHAR(45) NOT NULL,
  `admin_ID_admin` INT NOT NULL,
  PRIMARY KEY (`ID_feedback`),
  CONSTRAINT `fk_feedback_User1`
    FOREIGN KEY (`user_username`)
    REFERENCES `mydb`.`User` (`username`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_feedback_admin1`
    FOREIGN KEY (`admin_ID_admin`)
    REFERENCES `mydb`.`Admin` (`ID_admin`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Recomended`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Recomended` (
  `trip_ID_area` INT NOT NULL,
  `user_username` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`trip_ID_area`, `user_username`),
  CONSTRAINT `fk_recomended_trip1`
    FOREIGN KEY (`trip_ID_area`)
    REFERENCES `mydb`.`Trip` (`area`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_recomended_User1`
    FOREIGN KEY (`user_username`)
    REFERENCES `mydb`.`User` (`username`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Image`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Image` (
  `ID_image` INT NOT NULL,
  `trip_area` INT NOT NULL,
  `trip_area_ID_area` VARCHAR(45) NOT NULL,
  `picture` BLOB NULL,
  PRIMARY KEY (`ID_image`, `trip_area`, `trip_area_ID_area`),
  CONSTRAINT `fk_image_trip1`
    FOREIGN KEY (`trip_area` , `trip_area_ID_area`)
    REFERENCES `mydb`.`Trip` (`area` , `area_ID_area`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Stored_tag`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Stored_tag` (
  `ID_tag` INT NOT NULL,
  `name` VARCHAR(45) NULL,
  PRIMARY KEY (`ID_tag`))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Stored_tag_has_User`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Stored_tag_has_User` (
  `Stored_tag_ID_tag` INT NOT NULL,
  `User_username` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`Stored_tag_ID_tag`, `User_username`),
  CONSTRAINT `fk_Stored_tag_has_User_Stored_tag1`
    FOREIGN KEY (`Stored_tag_ID_tag`)
    REFERENCES `mydb`.`Stored_tag` (`ID_tag`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Stored_tag_has_User_User1`
    FOREIGN KEY (`User_username`)
    REFERENCES `mydb`.`User` (`username`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Trip_has_Stored_tag`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Trip_has_Stored_tag` (
  `Trip_area` INT NOT NULL,
  `Trip_area_ID_area` VARCHAR(45) NOT NULL,
  `Stored_tag_ID_tag` INT NOT NULL,
  PRIMARY KEY (`Trip_area`, `Trip_area_ID_area`, `Stored_tag_ID_tag`),
  CONSTRAINT `fk_Trip_has_Stored_tag_Trip1`
    FOREIGN KEY (`Trip_area` , `Trip_area_ID_area`)
    REFERENCES `mydb`.`Trip` (`area` , `area_ID_area`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Trip_has_Stored_tag_Stored_tag1`
    FOREIGN KEY (`Stored_tag_ID_tag`)
    REFERENCES `mydb`.`Stored_tag` (`ID_tag`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`Review`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `mydb`.`Review` (
  `ID_review` INT NOT NULL,
  `review_text` VARCHAR(500) NULL,
  `User_username` VARCHAR(45) NOT NULL,
  `Trip_area` INT NOT NULL,
  `Trip_area_ID_area` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`ID_review`, `User_username`, `Trip_area`, `Trip_area_ID_area`),
  CONSTRAINT `fk_Review_User1`
    FOREIGN KEY (`User_username`)
    REFERENCES `mydb`.`User` (`username`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION,
  CONSTRAINT `fk_Review_Trip1`
    FOREIGN KEY (`Trip_area` , `Trip_area_ID_area`)
    REFERENCES `mydb`.`Trip` (`area` , `area_ID_area`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
