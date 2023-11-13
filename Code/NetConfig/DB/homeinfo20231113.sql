/*
 Navicat Premium Data Transfer

 Source Server         : localhost
 Source Server Type    : MySQL
 Source Server Version : 80030
 Source Host           : localhost:33306
 Source Schema         : homeinfo

 Target Server Type    : MySQL
 Target Server Version : 80030
 File Encoding         : 65001

 Date: 13/11/2023 17:11:56
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for tb_config
-- ----------------------------
DROP TABLE IF EXISTS `tb_config`;
CREATE TABLE `tb_config`  (
  `RecId` int NOT NULL AUTO_INCREMENT,
  `MachineId` int NULL DEFAULT NULL,
  `ServerName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `InnerIP` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `OuterIP` varchar(20) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Username` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Userpassword` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Token` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Remark2` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Remark3` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Status` tinyint NULL DEFAULT 1,
  `MachineName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `TextRecord` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  PRIMARY KEY (`RecId`) USING BTREE
) ENGINE = InnoDB AUTO_INCREMENT = 1 CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of tb_config
-- ----------------------------
INSERT INTO `tb_config` VALUES (1, 0, 'xServerName', '', 'xOuterIP', 'xUsername', 'xUserpassword', 'xToken', 'xRemark', 'xRemark2', 'xRemark3', 0, 'xMachineName', 'xTextRecord');
INSERT INTO `tb_config` VALUES (2, 0, 'xServerName', 'xInnerIP', 'xOuterIP', 'xUsername', 'xUserpassword', 'xToken', 'xRemark', 'xRemark2', 'xRemark3', 1, 'xMachineName', 'xTextRecord');
INSERT INTO `tb_config` VALUES (3, 0, 'xServerName', 'xInnerIP', 'xOuterIP', 'xUsername', 'xUserpassword', 'xToken', 'xRemark', 'xRemark2', 'xRemark3', 1, 'xMachineName', 'xTextRecord');
INSERT INTO `tb_config` VALUES (4, 0, 'xServerName', 'xInnerIP', 'xOuterIP', 'xUsername', 'xUserpassword', 'xToken', 'xRemark', 'xRemark2', 'xRemark3', 1, 'xMachineName', 'xTextRecord');
INSERT INTO `tb_config` VALUES (5, 0, 'xServerName', 'xInnerIP', 'xOuterIP', 'xUsername', 'xUserpassword', 'xToken', 'xRemark', 'xRemark2', 'xRemark3', 1, 'xMachineName', 'xTextRecord');
INSERT INTO `tb_config` VALUES (6, 0, 'xServerName', 'xInnerIP', 'xOuterIP', 'xUsername', 'xUserpassword', 'xToken', 'xRemark', 'xRemark2', 'xRemark3', 1, 'xMachineName', 'xTextRecord');

-- ----------------------------
-- Table structure for tb_machine
-- ----------------------------
DROP TABLE IF EXISTS `tb_machine`;
CREATE TABLE `tb_machine`  (
  `RecId` int NOT NULL,
  `MachineName` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Status` tinyint NULL DEFAULT 1,
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  PRIMARY KEY (`RecId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of tb_machine
-- ----------------------------

SET FOREIGN_KEY_CHECKS = 1;
