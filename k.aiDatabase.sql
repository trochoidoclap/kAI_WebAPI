-- --------------------------------------------------------
-- Host:                         127.9.6.9
-- Server version:               8.0.40 - MySQL Community Server - GPL
-- Server OS:                    Win64
-- HeidiSQL Version:             12.10.0.7000
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;


-- Dumping database structure for format
CREATE DATABASE IF NOT EXISTS `format` /*!40100 DEFAULT CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci */ /*!80016 DEFAULT ENCRYPTION='N' */;
USE `format`;

-- Dumping structure for table format.questions
CREATE TABLE IF NOT EXISTS `questions` (
  `id_question` int NOT NULL AUTO_INCREMENT,
  `type` int NOT NULL,
  `content` text,
  `score` int NOT NULL DEFAULT '0',
  PRIMARY KEY (`id_question`),
  KEY `FK_questions_questions_type` (`type`),
  CONSTRAINT `FK_questions_questions_type` FOREIGN KEY (`type`) REFERENCES `questions_type` (`id_questype`)
) ENGINE=InnoDB AUTO_INCREMENT=26 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='id_question : ko cần bàn :))\r\ntype (link với idtype trong bảng questions_type) : dạng câu hỏi tương ứng với mật mã Holland của nó\r\ncontent : câu hỏi';

-- Dumping data for table format.questions: ~25 rows (approximately)
DELETE FROM `questions`;
INSERT INTO `questions` (`id_question`, `type`, `content`, `score`) VALUES
	(1, 2, 'Tôi thích giải các bài toán logic và câu đố toán học trừu tượng.', 0),
	(2, 2, 'Tôi tò mò về cách hoạt động của vạn vật xung quanh tôi.', 0),
	(3, 3, 'Tôi thích viết truyện, thơ hoặc tạo các nhân vật hư cấu.', 0),
	(4, 4, 'Tôi thích đọc về lịch sử và cách xã hội và thế giới đã thay đổi.', 0),
	(5, 6, 'Tôi cảm thấy hài lòng khi tổ chức dữ liệu trực quan hơn.', 0),
	(6, 3, 'Tôi thích khám phá bức tranh toàn cảnh hoặc tưởng tượng về những khả năng trong tương lai.', 0),
	(7, 1, 'Tôi thích làm những việc với sự trợ giúp của công cụ hoặc nghiên cứu các vật dụng khác nhau.', 0),
	(8, 4, 'Tôi thích thảo luận về các vấn đề xã hội thực tế.', 0),
	(9, 5, 'Tôi thích lãnh đạo nhóm và có những ý kiến mang tính quyết định.', 0),
	(10, 6, 'Tôi thích tạo ra những quy tắc riêng để quản lý công việc.', 0),
	(11, 3, 'Tôi thường suy ngẫm kỹ trước khi nói hoặc viết.', 0),
	(12, 2, 'Tôi thích quan sát thiên nhiên quanh tôi hay cách cơ thể hoạt động.', 0),
	(13, 3, 'Tôi thường suy nghĩ bằng hình ảnh như sơ đồ, bản đồ hoặc bản phác thảo.', 0),
	(14, 4, 'Tôi thích nói trước công chúng hoặc giải thích ý tưởng cho người khác.', 0),
	(15, 2, 'Tôi bị thu hút bởi các thí nghiệm khoa học, dù đó là vật lí, hoá học, hay sinh học.', 0),
	(16, 4, 'Tôi thích tìm hiểu về các nền văn hóa và vấn đề toàn cầu khác nhau.', 0),
	(17, 1, 'Tôi quan trọng việc học thực hành hơn chỉ đơn thuần trên lý thuyết.', 0),
	(18, 3, 'Tôi thích khám phá các ý nghĩa đằng sau các loại ngôn ngữ khác nhau.', 0),
	(19, 4, 'Tôi cảm thấy được tiếp thêm năng lượng khi làm việc nhóm và phát huy ý tưởng.', 0),
	(20, 2, 'Tôi thích diễn giải các sự kiện lịch sử và ảnh hưởng lên đời sống hiện nay của chúng.', 0),
	(21, 6, 'Tôi thích áp dụng công thức, định luật để giải quyết vấn đề thực tế.', 0),
	(22, 2, 'Tôi hứng thú với cách mọi thứ trong tự nhiên sinh hay diệt vào từng thời kì riêng biệt.', 0),
	(23, 6, 'Tôi thích các hướng dẫn rõ ràng, từng bước trong các bài tập.', 0),
	(24, 3, 'Tôi thích khám phá các giá trị cá nhân và ý nghĩa cuộc sống.', 0),
	(25, 2, 'Tôi thích vận dụng mọi thứ một cách linh hoạt thay vì chỉ đơn thuần là máy móc theo những gì đã học.', 0);

-- Dumping structure for table format.questions_type
CREATE TABLE IF NOT EXISTS `questions_type` (
  `id_questype` int NOT NULL AUTO_INCREMENT,
  `questype` char(50) NOT NULL DEFAULT '0',
  `comment` tinytext,
  `commentVN` tinytext,
  PRIMARY KEY (`id_questype`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='id_questype : ko cần bàn\r\nquestype : dạng câu hỏi (một trong 6 mật mã Holland)\r\ncomment vs commentVN : tên của cái mật mã Holland đó thôi';

-- Dumping data for table format.questions_type: ~6 rows (approximately)
DELETE FROM `questions_type`;
INSERT INTO `questions_type` (`id_questype`, `questype`, `comment`, `commentVN`) VALUES
	(1, 'R', 'Realistic', 'Thực tế'),
	(2, 'I', 'Investigate', 'Tìm tòi'),
	(3, 'A', 'Artistic', 'Nghệ thuật'),
	(4, 'S', 'Social', 'Xã giao'),
	(5, 'E', 'Enterprising', 'Lãnh đạo'),
	(6, 'C', 'Conventional', 'Kỉ luật');

-- Dumping structure for table format.subjects
CREATE TABLE IF NOT EXISTS `subjects` (
  `id_subjects` int NOT NULL AUTO_INCREMENT,
  `subject` varchar(50) NOT NULL DEFAULT '0',
  `type` varchar(50) NOT NULL DEFAULT '',
  PRIMARY KEY (`id_subjects`)
) ENGINE=InnoDB AUTO_INCREMENT=16 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='id_subjects : ko bàn\r\nsubject : tên môn\r\ntype : bắt buộc, hoặc tự chọn (TN/XH), hoặc cả hai <- đừng hỏi t cái này là thg Gia nó warp bảng t tlà victim pls donnt kill me pls dont kill me AH';

-- Dumping data for table format.subjects: ~10 rows (approximately)
DELETE FROM `subjects`;
INSERT INTO `subjects` (`id_subjects`, `subject`, `type`) VALUES
	(1, 'Toán', 'Bắt buộc'),
	(2, 'Ngữ văn', 'Bắt buộc'),
	(3, 'Tiếng Anh', 'Bắt buộc'),
	(4, 'Vật lí', 'Tự chọn (TN)'),
	(5, 'Hoá học', 'Tự chọn (TN)'),
	(6, 'Sinh học', 'Tự chọn (TN)'),
	(7, 'Công nghệ', 'Bắt buộc/Tự chọn'),
	(8, 'Lịch sử', 'Bắt buộc'),
	(9, 'Địa lí', 'Tự chọn (XH)'),
	(10, 'Tin học', 'Bắt buộc/Tự chọn');

-- Dumping structure for table format.subjects_group
CREATE TABLE IF NOT EXISTS `subjects_group` (
  `id_subgroup` int NOT NULL AUTO_INCREMENT,
  `subject1` int NOT NULL,
  `subject2` int NOT NULL,
  `subject3` int NOT NULL,
  `subject4` int DEFAULT NULL,
  `subject5` int DEFAULT NULL,
  PRIMARY KEY (`id_subgroup`),
  KEY `FK_subjects_group_subjects` (`subject1`),
  KEY `FK_subjects_group_subjects_2` (`subject2`),
  KEY `FK_subjects_group_subjects_3` (`subject3`),
  KEY `FK_subjects_group_subjects_4` (`subject4`),
  KEY `FK_subjects_group_subjects_5` (`subject5`),
  CONSTRAINT `FK_subjects_group_subjects` FOREIGN KEY (`subject1`) REFERENCES `subjects` (`id_subjects`),
  CONSTRAINT `FK_subjects_group_subjects_2` FOREIGN KEY (`subject2`) REFERENCES `subjects` (`id_subjects`),
  CONSTRAINT `FK_subjects_group_subjects_3` FOREIGN KEY (`subject3`) REFERENCES `subjects` (`id_subjects`),
  CONSTRAINT `FK_subjects_group_subjects_4` FOREIGN KEY (`subject4`) REFERENCES `subjects` (`id_subjects`),
  CONSTRAINT `FK_subjects_group_subjects_5` FOREIGN KEY (`subject5`) REFERENCES `subjects` (`id_subjects`)
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='id_subgroup : mã tổ hợp môn\r\nsubject1 -> subject5 (tất cả link đến bảng subjects) : các môn thành phần trong tổ hợp môn';

-- Dumping data for table format.subjects_group: ~2 rows (approximately)
DELETE FROM `subjects_group`;
INSERT INTO `subjects_group` (`id_subgroup`, `subject1`, `subject2`, `subject3`, `subject4`, `subject5`) VALUES
	(1, 1, 3, 4, NULL, NULL),
	(2, 1, 3, 5, NULL, NULL);

-- Dumping structure for table format.transcript
CREATE TABLE IF NOT EXISTS `transcript` (
  `id_transcript` int(8) unsigned zerofill NOT NULL AUTO_INCREMENT,
  `id_user` int NOT NULL DEFAULT '0',
  `date` date NOT NULL,
  `content` text NOT NULL,
  `rating` char(4) NOT NULL DEFAULT '',
  PRIMARY KEY (`id_transcript`),
  KEY `FK__users` (`id_user`),
  CONSTRAINT `FK__users` FOREIGN KEY (`id_user`) REFERENCES `users` (`id_users`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='id_trascript : mã bài làm\r\nid_user (link đến id_user trong bàng users) : người dùng làm bài tnghiệm ấy\r\ndate : ngày làm\r\ncontent : nội dung làm (độ dài xâu cố định với format 01X02X03X...23X24X25X với X là số chỉ mức độ hài lòng từ 1->5, xem ví dụ hiểu rõ thêm :)))\r\nrating : nội dung con API trả (aka. kết quả chung/cuối cùng) theo mã Holland/MBTI?';

-- Dumping data for table format.transcript: ~0 rows (approximately)
DELETE FROM `transcript`;
INSERT INTO `transcript` (`id_transcript`, `id_user`, `date`, `content`, `rating`) VALUES
	(00000001, 1, '2025-05-30', '012024035042053063071085092102113125134142152161174185192203214224235245251', 'A');

-- Dumping structure for table format.users
CREATE TABLE IF NOT EXISTS `users` (
  `id_users` int NOT NULL AUTO_INCREMENT,
  `username` varchar(50) NOT NULL DEFAULT '0',
  `password` varchar(50) NOT NULL DEFAULT '0',
  `fullname` text NOT NULL,
  `email` varchar(50) NOT NULL DEFAULT '0',
  `phone` varchar(10) NOT NULL DEFAULT '',
  `address` text NOT NULL,
  PRIMARY KEY (`id_users`)
) ENGINE=InnoDB AUTO_INCREMENT=2 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci COMMENT='cả cái bảng này ko có j phải bàn nữa cả :)))';

-- Dumping data for table format.users: ~1 rows (approximately)
DELETE FROM `users`;
INSERT INTO `users` (`id_users`, `username`, `password`, `fullname`, `email`, `phone`, `address`) VALUES
	(1, 'test', 'test', 'Nguyễn Văn A', 'test@email.com', '0900000000', 'add');

/*!40103 SET TIME_ZONE=IFNULL(@OLD_TIME_ZONE, 'system') */;
/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
