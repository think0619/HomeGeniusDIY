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

 Date: 29/11/2023 19:07:18
*/

SET NAMES utf8mb4;
SET FOREIGN_KEY_CHECKS = 0;

-- ----------------------------
-- Table structure for tb_vlcsrc
-- ----------------------------
DROP TABLE IF EXISTS `tb_vlcsrc`;
CREATE TABLE `tb_vlcsrc`  (
  `RecId` bigint NOT NULL AUTO_INCREMENT,
  `Name` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Remark` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL DEFAULT NULL,
  `Value` text CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NULL,
  `Status` tinyint NULL DEFAULT NULL,
  PRIMARY KEY (`RecId`) USING BTREE
) ENGINE = InnoDB CHARACTER SET = utf8mb4 COLLATE = utf8mb4_0900_ai_ci ROW_FORMAT = Dynamic;

-- ----------------------------
-- Records of tb_vlcsrc
-- ----------------------------
INSERT INTO `tb_vlcsrc` VALUES (1, '中国之声', NULL, 'http://ngcdn001.cnr.cn/live/zgzs/index.m3u8', 1);
INSERT INTO `tb_vlcsrc` VALUES (2, '江苏音乐台', NULL, 'http://satellitepull.cnr.cn/live/wx32jsjdlxyy/playlist.m3u8', 1);
INSERT INTO `tb_vlcsrc` VALUES (3, '天籁之音 Hi-Fi Radio', NULL, 'http://play-radio-stream3.hndt.com/now/WUBA5hW2/playlist.m3u8', 1);
INSERT INTO `tb_vlcsrc` VALUES (4, '亚洲音乐', NULL, 'http://asiafm.hk:8000/asiafm', 1);
INSERT INTO `tb_vlcsrc` VALUES (5, '亚洲经典', NULL, 'http://goldfm.cn:8000/goldfm', 1);
INSERT INTO `tb_vlcsrc` VALUES (6, '江苏交通广播', NULL, 'http://satellitepull.cnr.cn/live/wx32jsjtgb/playlist.m3u8', 1);
INSERT INTO `tb_vlcsrc` VALUES (7, 'CNR经典音乐广播', NULL, 'http://liveop.cctv.cn/hls/jdyygb192/playlist.m3u8', 1);
INSERT INTO `tb_vlcsrc` VALUES (8, '南京音乐广播', NULL, 'http://live.njgb.com:10588/show.mp3', 1);

SET FOREIGN_KEY_CHECKS = 1;
