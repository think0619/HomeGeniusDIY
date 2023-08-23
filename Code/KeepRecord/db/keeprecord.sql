/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 80030
 Source Host           : localhost:33306
 Source Schema         : keeprecord

 Target Server Type    : MySQL
 Target Server Version : 80030
 File Encoding         : 65001

 Date: 23/08/2023 17:35:57
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for tb_keeprecord
-- ----------------------------
DROP TABLE IF EXISTS `tb_keeprecord`;
CREATE TABLE `tb_keeprecord`  (
  `RecId` int NOT NULL AUTO_INCREMENT,
  `TypeId` int NULL DEFAULT NULL,
  `Count` decimal(10, 0) NULL DEFAULT NULL,
  `UnitsId` int NULL DEFAULT NULL,
  `SubCount` decimal(10, 0) NULL DEFAULT NULL,
  `SubUnitsId` int NULL DEFAULT NULL,
  `Status` tinyint NULL DEFAULT NULL,
  `RecordDatetime` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`RecId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of tb_keeprecord
-- ----------------------------

-- ----------------------------
-- Table structure for tb_keeptype
-- ----------------------------
DROP TABLE IF EXISTS `tb_keeptype`;
CREATE TABLE `tb_keeptype`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `TypeName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Status` tinyint NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of tb_keeptype
-- ----------------------------

-- ----------------------------
-- Table structure for tb_keepunits
-- ----------------------------
DROP TABLE IF EXISTS `tb_keepunits`;
CREATE TABLE `tb_keepunits`  (
  `Id` int NOT NULL AUTO_INCREMENT,
  `UnitsName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Status` tinyint NULL DEFAULT NULL,
  PRIMARY KEY (`Id`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of tb_keepunits
-- ----------------------------

-- ----------------------------
-- View structure for view_keeprecord
-- ----------------------------
DROP VIEW IF EXISTS `view_keeprecord`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `view_keeprecord` AS select `t`.`TypeName` AS `Type`,`u`.`UnitsName` AS `Units`,`r`.`RecId` AS `RecId`,`r`.`TypeId` AS `TypeId`,`r`.`Count` AS `Count`,`r`.`SubCount` AS `SubCount`,`r`.`RecordDatetime` AS `RecordDatetime` from ((`tb_keeprecord` `r` left join `tb_keeptype` `t` on(((`r`.`TypeId` = `t`.`Id`) and (`t`.`Status` = 1)))) left join `tb_keepunits` `u` on(((`r`.`UnitsId` = `u`.`Id`) and (`u`.`Status` = 1)))) where (`r`.`Status` = 1);

SET FOREIGN_KEY_CHECKS = 1;
