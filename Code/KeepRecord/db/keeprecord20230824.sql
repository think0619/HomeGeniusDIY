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

 Date: 24/08/2023 17:27:20
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
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of tb_keeprecord
-- ----------------------------
INSERT INTO `tb_keeprecord` VALUES (1, 1, 10, 1, 22, 2, 1, '2023-08-24 15:51:54');

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
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of tb_keeptype
-- ----------------------------
INSERT INTO `tb_keeptype` VALUES (1, '跑步', NULL, 1);
INSERT INTO `tb_keeptype` VALUES (2, '跳绳', NULL, 1);
INSERT INTO `tb_keeptype` VALUES (3, '拳击', NULL, 1);
INSERT INTO `tb_keeptype` VALUES (4, '俯卧撑', NULL, 1);
INSERT INTO `tb_keeptype` VALUES (5, '骑行', NULL, 1);

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
) ENGINE = InnoDB AUTO_INCREMENT = 5 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of tb_keepunits
-- ----------------------------
INSERT INTO `tb_keepunits` VALUES (1, '分钟', NULL, 1);
INSERT INTO `tb_keepunits` VALUES (2, '次', NULL, 1);
INSERT INTO `tb_keepunits` VALUES (3, '个', NULL, 1);
INSERT INTO `tb_keepunits` VALUES (4, '公里', NULL, 1);

-- ----------------------------
-- View structure for view_keeprecord
-- ----------------------------
DROP VIEW IF EXISTS `view_keeprecord`;
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `view_keeprecord` AS select `r`.`RecId` AS `RecId`,`t`.`TypeName` AS `Type`,`r`.`Count` AS `Count`,`u1`.`UnitsName` AS `Units`,`r`.`SubCount` AS `SubCount`,`u2`.`UnitsName` AS `SubUnits`,`r`.`RecordDatetime` AS `RecordDatetime`,`r`.`UnitsId` AS `UnitsId`,`r`.`SubUnitsId` AS `SubUnitsId` from (((`tb_keeprecord` `r` left join `tb_keeptype` `t` on(((`r`.`TypeId` = `t`.`Id`) and (`t`.`Status` = 1)))) left join `tb_keepunits` `u1` on(((`r`.`UnitsId` = `u1`.`Id`) and (`u1`.`Status` = 1)))) left join `tb_keepunits` `u2` on(((`r`.`UnitsId` = `u2`.`Id`) and (`u2`.`Status` = 1)))) where (`r`.`Status` = 1);

SET FOREIGN_KEY_CHECKS = 1;
