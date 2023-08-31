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

 Date: 31/08/2023 17:35:21
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
  `RecordDate` datetime NULL DEFAULT NULL,
  PRIMARY KEY (`RecId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 39 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of tb_keeprecord
-- ----------------------------
INSERT INTO `tb_keeprecord` VALUES (2, 1, 222, 1, 333, 2, 1, '2023-08-25 19:52:28', '2023-08-25 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (3, 1, 222, 1, 333, 2, 1, '2023-08-25 20:01:49', '2023-08-25 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (4, 1, 222, 1, 333, 2, 1, '2023-08-25 20:01:51', '2023-08-25 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (5, 1, 222, 1, 333, 2, 1, '2023-08-25 20:01:51', '2023-08-25 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (6, 1, 222, 1, 333, 2, 1, '2023-08-25 20:01:52', '2023-08-25 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (7, 1, 222, 1, 333, 2, 1, '2023-08-25 20:01:52', '2023-08-25 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (8, 1, 222, 1, 333, 2, 1, '2023-08-25 20:01:52', '2023-08-25 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (9, 1, 222, 1, 333, 2, 1, '2023-08-25 20:01:53', '2023-08-25 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (10, 1, 222, 1, 333, 2, 1, '2023-08-25 20:01:53', '2023-08-25 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (11, 1, 2222, 4, 3334, 3, 1, '2023-08-25 20:02:07', '2023-08-25 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (12, 1, 1, 1, 333, 1, 0, '2023-08-26 10:23:17', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (13, 1, 1, 1, 333, 1, 0, '2023-08-26 10:23:38', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (14, 1, 1, 1, 333, 1, 0, '2023-08-26 10:24:35', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (15, 1, 1, 1, 333, 1, 0, '2023-08-26 10:25:11', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (16, 1, 1, 1, 333, 1, 0, '2023-08-26 10:25:20', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (17, 1, 1, 1, 333, 1, 0, '2023-08-26 10:25:24', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (18, 1, 1, 1, 333, 1, 0, '2023-08-26 10:26:34', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (19, 1, 1, 1, 333, 1, 0, '2023-08-26 10:27:38', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (20, 1, 1, 1, 333, 1, 0, '2023-08-26 10:29:57', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (21, 1, 1, 1, 333, 1, 0, '2023-08-26 10:32:20', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (22, 1, 1, 1, 333, 1, 0, '2023-08-26 10:32:45', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (23, 1, 1, 1, 333, 1, 0, '2023-08-26 10:33:15', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (24, 1, 1, 1, 333, 1, 0, '2023-08-26 10:34:01', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (25, 1, 1, 1, 333, 1, 0, '2023-08-26 10:34:01', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (26, 1, 1, 1, 333, 1, 0, '2023-08-26 10:34:01', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (27, 1, 1, 1, 333, 1, 0, '2023-08-26 10:34:22', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (28, 1, 1, 1, 333, 1, 0, '2023-08-26 10:37:49', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (29, 1, 1, 1, 333, 1, 0, '2023-08-26 10:37:49', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (30, 1, 1, 1, 333, 1, 0, '2023-08-26 10:38:26', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (31, 1, 1, 1, 333, 1, 0, '2023-08-26 10:38:26', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (32, 1, 1, 1, 333, 1, 0, '2023-08-26 11:10:07', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (33, 1, 1, 1, 333, 1, 0, '2023-08-26 11:12:35', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (34, 1, 2, 3, 6, 4, 0, '2023-08-26 11:15:41', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (35, 1, 2, 3, 6, 4, 0, '2023-08-26 11:15:47', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (36, 1, 2, 3, 6, 4, 0, '2023-08-26 11:15:51', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (37, 1, 2, 3, 6, 4, 0, '2023-08-26 11:15:59', '2023-08-26 00:00:00');
INSERT INTO `tb_keeprecord` VALUES (38, 1, 2, 3, 6, 4, 0, '2023-08-26 11:16:27', '2023-08-26 00:00:00');

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
) ENGINE = InnoDB AUTO_INCREMENT = 6 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

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
CREATE ALGORITHM = UNDEFINED SQL SECURITY DEFINER VIEW `view_keeprecord` AS select `r`.`RecId` AS `RecId`,`t`.`TypeName` AS `TypeName`,`r`.`Count` AS `Count`,`u1`.`UnitsName` AS `Units`,`r`.`SubCount` AS `SubCount`,`u2`.`UnitsName` AS `SubUnits`,`r`.`RecordDatetime` AS `RecordDatetime`,`r`.`RecordDate` AS `RecordDate`,`r`.`UnitsId` AS `UnitsId`,`r`.`SubUnitsId` AS `SubUnitsId` from (((`tb_keeprecord` `r` left join `tb_keeptype` `t` on(((`r`.`TypeId` = `t`.`Id`) and (`t`.`Status` = 1)))) left join `tb_keepunits` `u1` on(((`r`.`UnitsId` = `u1`.`Id`) and (`u1`.`Status` = 1)))) left join `tb_keepunits` `u2` on(((`r`.`UnitsId` = `u2`.`Id`) and (`u2`.`Status` = 1)))) where (`r`.`Status` = 1);

SET FOREIGN_KEY_CHECKS = 1;
