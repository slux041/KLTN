-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Máy chủ: 127.0.0.1
-- Thời gian đã tạo: Th12 03, 2025 lúc 05:44 PM
-- Phiên bản máy phục vụ: 10.4.32-MariaDB
-- Phiên bản PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Cơ sở dữ liệu: `petspadb`
--

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `appointments`
--

CREATE TABLE `appointments` (
  `appointment_id` int(11) NOT NULL,
  `customer_id` int(11) DEFAULT NULL,
  `service_id` int(11) NOT NULL,
  `pet_info` text DEFAULT NULL,
  `appointment_date` datetime NOT NULL,
  `status` varchar(20) NOT NULL DEFAULT 'pending' COMMENT 'pending, confirmed, processing,  completed, canceled',
  `time_slot` varchar(20) NOT NULL DEFAULT '09:00' COMMENT 'Khung giờ: 09:00, 10:00, 11:00, 14:00, 15:00, 16:00, 17:00',
  `guest_name` varchar(100) DEFAULT NULL,
  `guest_phone` varchar(20) DEFAULT NULL,
  `guest_address` varchar(500) DEFAULT NULL,
  `pet_type` varchar(20) DEFAULT 'dog',
  `pet_breed` varchar(100) DEFAULT NULL,
  `Source` varchar(20) CHARACTER SET utf8 COLLATE utf8_general_ci NOT NULL DEFAULT 'web'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `appointments`
--

INSERT INTO `appointments` (`appointment_id`, `customer_id`, `service_id`, `pet_info`, `appointment_date`, `status`, `time_slot`, `guest_name`, `guest_phone`, `guest_address`, `pet_type`, `pet_breed`, `Source`) VALUES
(10, 5, 5, 'dfd', '2025-12-03 00:00:00', 'completed', '09:00', NULL, NULL, NULL, 'chó', 'dvdv', 'web'),
(11, 5, 6, 'hjhj', '2025-12-03 00:00:00', 'cancelled', '14:00', NULL, NULL, NULL, 'Mèo', 'hjhj', 'web'),
(17, NULL, 6, '', '2025-12-02 18:41:48', 'completed', '10:00', 'hj', '0977777777', 'Tại quầy', 'cat', NULL, 'store'),
(18, NULL, 6, '', '2025-12-03 18:50:46', 'completed', '09:00', 'hj', '0912837247', 'Tại quầy', 'dog', NULL, 'store'),
(19, NULL, 6, '', '2025-12-03 18:51:01', 'completed', '09:00', 'hjdf', '0976634636', 'Tại quầy', 'dog', NULL, 'store'),
(20, NULL, 7, '', '2025-12-03 19:19:19', 'completed', '10:00', 'hj', '0985554333', 'Tại quầy', 'dog', NULL, 'store'),
(21, 5, 5, 'golden', '2025-12-03 00:00:00', 'processing', '10:00', NULL, NULL, NULL, 'Chó', 'golden', 'web'),
(22, NULL, 6, '', '2025-12-02 19:24:51', 'completed', '09:00', 'fh', '0938345722', 'Tại quầy', 'cat', NULL, 'store'),
(23, NULL, 6, '', '2025-12-02 19:51:58', 'completed', '09:00', 'fhfh', '0967554433', 'Tại quầy', 'cat', NULL, 'store'),
(24, NULL, 5, '', '2025-12-02 19:58:57', 'completed', '09:00', 'rt', '0988888888', 'Tại quầy', 'cat', NULL, 'store'),
(25, NULL, 6, '', '2025-12-02 20:04:43', 'completed', '10:00', 'ss', '0896785456', 'Tại quầy', 'dog', NULL, 'store'),
(26, 5, 5, 'hj', '2025-12-04 00:00:00', 'completed', '15:00', NULL, NULL, NULL, 'Chó', 'hj', 'web'),
(27, 5, 5, 'hj', '2025-12-03 00:00:00', 'completed', '10:00', NULL, NULL, NULL, 'Chó', 'hj', 'web'),
(28, 5, 5, 'hj', '2025-12-05 00:00:00', 'completed', '09:00', NULL, NULL, NULL, 'Chó', 'hj', 'web'),
(29, NULL, 6, '', '2025-12-02 20:22:31', 'completed', '10:00', 'ss', '0918373744', 'Tại quầy', 'cat', NULL, 'store'),
(30, NULL, 6, '', '2025-12-02 20:29:37', 'completed', '11:00', 'ẻy', '0917273734', 'Tại quầy', 'cat', NULL, 'store'),
(31, 5, 5, 'x', '2025-12-06 00:00:00', 'completed', '10:00', NULL, NULL, NULL, 'Chó', 'x', 'web');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `audit_logs`
--

CREATE TABLE `audit_logs` (
  `id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `action` varchar(500) NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `cart_items`
--

CREATE TABLE `cart_items` (
  `cart_item_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `product_id` int(11) DEFAULT NULL,
  `service_id` int(11) DEFAULT NULL,
  `quantity` int(11) NOT NULL DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `cart_items`
--

INSERT INTO `cart_items` (`cart_item_id`, `user_id`, `product_id`, `service_id`, `quantity`) VALUES
(51, 6, 2, NULL, 2);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `categories`
--

CREATE TABLE `categories` (
  `category_id` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `type` varchar(20) NOT NULL COMMENT 'product, service',
  `description` text DEFAULT NULL,
  `is_active` tinyint(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `categories`
--

INSERT INTO `categories` (`category_id`, `name`, `type`, `description`, `is_active`) VALUES
(1, 'Thức ăn cho Chó', 'product', 'Các loại thức ăn hạt, pate, và xương gặm cho chó cưng.', 1),
(2, 'Thức ăn cho Mèo', 'product', 'Các loại thức ăn khô, ướt và thức ăn vặt cho mèo.', 1),
(3, 'Đồ chơi & Phụ kiện', 'product', 'Đồ chơi, vòng cổ, dây dắt, quần áo cho thú cưng.', 1),
(4, 'Vệ sinh & Chăm sóc', 'product', 'Sữa tắm, lược chải lông, cát vệ sinh, sản phẩm khử mùi.', 1),
(5, 'Dịch vụ Tắm & Cắt tỉa', 'service', 'Các gói dịch vụ tắm gội, cắt tỉa lông chuyên nghiệp.', 1),
(6, 'Dịch vụ Spa & Thư giãn', 'service', 'Dịch vụ massage, chăm sóc da và lông cao cấp.', 1),
(7, 'Dịch vụ trông giữ', 'service', 'Dịch vụ trông giữ thú cưng theo ngày hoặc dài hạn.', 1),
(8, 'Dịch vụ Nhuộm & Phụ thu', 'service', '', 1);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `customers`
--

CREATE TABLE `customers` (
  `customer_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `address` varchar(500) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `customers`
--

INSERT INTO `customers` (`customer_id`, `user_id`, `address`) VALUES
(1, 3, '123 Đường ABC, Phường 1, Quận 2, TP.HCM'),
(2, 4, 'hjhj'),
(3, 5, 'string'),
(4, 6, NULL),
(5, 7, NULL),
(6, 8, NULL),
(7, 10, 'Mua tại quầy');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `customer_addresses`
--

CREATE TABLE `customer_addresses` (
  `address_id` int(11) NOT NULL,
  `customer_id` int(11) NOT NULL,
  `full_name` varchar(100) NOT NULL COMMENT 'Tên người nhận',
  `phone` varchar(20) NOT NULL COMMENT 'SĐT người nhận',
  `address_line` varchar(500) NOT NULL COMMENT 'Địa chỉ chi tiết (số nhà, tên đường)',
  `province_id` varchar(10) NOT NULL COMMENT 'Mã tỉnh/thành phố từ API (1 trong 34 tỉnh thành)',
  `province_name` varchar(100) NOT NULL COMMENT 'Tên tỉnh/thành phố',
  `ward_id` varchar(10) NOT NULL COMMENT 'Mã xã/phường/thị trấn từ API',
  `ward_name` varchar(100) NOT NULL COMMENT 'Tên xã/phường/thị trấn',
  `is_default` tinyint(1) DEFAULT 0 COMMENT 'Địa chỉ mặc định',
  `created_at` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci COMMENT='Địa chỉ khách hàng - Cấu trúc 2 cấp: Tỉnh/Thành phố → Xã/Phường/Thị trấn';

--
-- Đang đổ dữ liệu cho bảng `customer_addresses`
--

INSERT INTO `customer_addresses` (`address_id`, `customer_id`, `full_name`, `phone`, `address_line`, `province_id`, `province_name`, `ward_id`, `ward_name`, `is_default`, `created_at`) VALUES
(1, 5, 'hjhj', '09127303021', 'gg', '01', 'Hà Nội', '273', 'Đan Phượng', 1, '2025-11-21 15:10:18'),
(2, 5, 'gdss', '09127303021', 'r', '35', 'Hà Nam', '352', 'Bình Lục', 0, '2025-11-21 15:20:38'),
(3, 5, 'bt', '09127303021', 'bt', '89', 'An Giang', '886', 'An Phú', 0, '2025-11-21 15:24:37'),
(5, 5, 'hj', '0917283833', '123', '04', 'Cao Bằng', '043', 'Bảo Lạc', 0, '2025-11-29 15:55:40'),
(6, 6, 'hjj', '0917293932', '123', '02', 'Hà Giang', '031', 'Bắc Mê', 1, '2025-11-29 21:04:18');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `orders`
--

CREATE TABLE `orders` (
  `order_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `total_amount` decimal(18,2) NOT NULL,
  `payment_method` varchar(20) NOT NULL DEFAULT 'cod' COMMENT 'cod, bank',
  `payment_status` varchar(20) NOT NULL DEFAULT 'pending' COMMENT 'pending, paid, failed',
  `order_status` varchar(20) NOT NULL DEFAULT 'pending' COMMENT 'pending, confirmed, shipping, completed, canceled',
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `shipping_address_id` int(11) DEFAULT NULL COMMENT 'ID địa chỉ giao hàng',
  `shipping_full_name` varchar(100) DEFAULT NULL COMMENT 'Tên người nhận',
  `shipping_phone` varchar(20) DEFAULT NULL COMMENT 'SĐT người nhận',
  `shipping_address_line` text DEFAULT NULL COMMENT 'Địa chỉ giao hàng đầy đủ',
  `shipping_fee` decimal(18,2) NOT NULL DEFAULT 25000.00 COMMENT 'Phí ship',
  `subtotal` decimal(18,2) NOT NULL DEFAULT 0.00 COMMENT 'Tổng tiền hàng trước giảm giá',
  `promotion_id` int(11) DEFAULT NULL COMMENT 'Mã giảm giá áp dụng',
  `discount_amount` decimal(18,2) NOT NULL DEFAULT 0.00 COMMENT 'Số tiền giảm',
  `note` text DEFAULT NULL COMMENT 'Ghi chú đơn hàng',
  `shipping_province_id` varchar(10) DEFAULT NULL COMMENT 'Mã tỉnh/thành phố',
  `shipping_province_name` varchar(100) DEFAULT NULL COMMENT 'Tên tỉnh/thành phố',
  `shipping_ward_id` varchar(10) DEFAULT NULL COMMENT 'Mã xã/phường',
  `shipping_ward_name` varchar(100) DEFAULT NULL COMMENT 'Tên xã/phường',
  `trans_id` varchar(50) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `orders`
--

INSERT INTO `orders` (`order_id`, `user_id`, `total_amount`, `payment_method`, `payment_status`, `order_status`, `created_at`, `shipping_address_id`, `shipping_full_name`, `shipping_phone`, `shipping_address_line`, `shipping_fee`, `subtotal`, `promotion_id`, `discount_amount`, `note`, `shipping_province_id`, `shipping_province_name`, `shipping_ward_id`, `shipping_ward_name`, `trans_id`) VALUES
(1, 6, 190000.00, 'bank', 'pending', 'pending', '2025-11-19 15:20:43', NULL, NULL, NULL, NULL, 25000.00, 0.00, NULL, 0.00, NULL, NULL, NULL, NULL, NULL, NULL),
(2, 6, 60000.00, 'COD', 'pending', 'pending', '2025-11-19 15:23:18', NULL, NULL, NULL, NULL, 25000.00, 0.00, NULL, 0.00, NULL, NULL, NULL, NULL, NULL, NULL),
(3, 6, 720000.00, 'COD', 'pending', 'pending', '2025-11-19 15:23:45', NULL, NULL, NULL, NULL, 25000.00, 0.00, NULL, 0.00, NULL, NULL, NULL, NULL, NULL, NULL),
(4, 7, 95000.00, 'COD', 'pending', 'Completed', '2025-11-19 15:38:09', NULL, NULL, NULL, NULL, 25000.00, 0.00, NULL, 0.00, NULL, NULL, NULL, NULL, NULL, NULL),
(5, 7, 105000.00, 'COD', 'pending', 'pending', '2025-11-19 15:47:44', NULL, NULL, NULL, NULL, 25000.00, 0.00, NULL, 0.00, NULL, NULL, NULL, NULL, NULL, NULL),
(6, 7, 180000.00, 'COD', 'pending', 'pending', '2025-11-19 15:48:02', NULL, NULL, NULL, NULL, 25000.00, 0.00, NULL, 0.00, NULL, NULL, NULL, NULL, NULL, NULL),
(7, 7, 180000.00, 'COD', 'pending', 'pending', '2025-11-19 15:49:55', NULL, NULL, NULL, NULL, 25000.00, 0.00, NULL, 0.00, NULL, NULL, NULL, NULL, NULL, NULL),
(8, 7, 95000.00, 'COD', 'pending', 'pending', '2025-11-19 15:55:06', NULL, NULL, NULL, NULL, 25000.00, 0.00, NULL, 0.00, NULL, NULL, NULL, NULL, NULL, NULL),
(9, 7, 180000.00, 'COD', 'pending', 'pending', '2025-11-19 15:58:33', NULL, NULL, NULL, NULL, 25000.00, 0.00, NULL, 0.00, NULL, NULL, NULL, NULL, NULL, NULL),
(10, 7, 240000.00, 'COD', 'pending', 'pending', '2025-11-19 15:58:53', NULL, NULL, NULL, NULL, 25000.00, 0.00, NULL, 0.00, NULL, NULL, NULL, NULL, NULL, NULL),
(11, 7, 240000.00, 'COD', 'pending', 'pending', '2025-11-19 16:00:58', NULL, NULL, NULL, NULL, 25000.00, 0.00, NULL, 0.00, NULL, NULL, NULL, NULL, NULL, NULL),
(12, 7, 50000.00, 'COD', 'pending', 'pending', '2025-11-19 16:01:27', NULL, NULL, NULL, NULL, 25000.00, 0.00, NULL, 0.00, NULL, NULL, NULL, NULL, NULL, NULL),
(13, 7, 105000.00, 'COD', 'pending', 'pending', '2025-11-19 16:04:27', NULL, NULL, NULL, NULL, 25000.00, 0.00, NULL, 0.00, NULL, NULL, NULL, NULL, NULL, NULL),
(14, 7, 240000.00, 'COD', 'pending', 'pending', '2025-11-19 16:06:18', NULL, NULL, NULL, NULL, 25000.00, 0.00, NULL, 0.00, NULL, NULL, NULL, NULL, NULL, NULL),
(15, 6, 241000.00, 'COD', 'pending', 'pending', '2025-11-20 15:14:58', 2, 'Rock Tuber', '0918181812', 'Dong Khoi, à, sdf, Tay Ninh', 25000.00, 240000.00, 1, 24000.00, '', NULL, NULL, NULL, NULL, NULL),
(16, 6, 119500.00, 'COD', 'pending', 'pending', '2025-11-20 15:29:17', 2, 'Rock Tuber', '0918181812', 'Dong Khoi, à, sdf, Tay Ninh', 25000.00, 105000.00, 1, 10500.00, '', NULL, NULL, NULL, NULL, NULL),
(17, 7, 270000.00, 'COD', 'pending', 'pending', '2025-11-21 15:06:04', NULL, 'hj', '0918283823', 'sdf', 25000.00, 245000.00, NULL, 0.00, 'sd', '02', 'Hà Giang', '031', 'Bắc Mê', NULL),
(18, 7, 355000.00, 'COD', 'pending', 'pending', '2025-11-21 15:24:43', NULL, 'bt', '09127303021', 'bt', 25000.00, 330000.00, NULL, 0.00, 'trb', '89', 'An Giang', '886', 'An Phú', NULL),
(19, 7, 119500.00, 'COD', 'pending', 'pending', '2025-11-21 15:51:11', NULL, 'hjhj', '09127303021', 'gg', 25000.00, 105000.00, 1, 10500.00, '', '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(20, 7, 130000.00, 'COD', 'pending', 'pending', '2025-11-21 15:52:25', NULL, 'gdss', '09127303021', 'r', 25000.00, 105000.00, NULL, 0.00, 'hj', '35', 'Hà Nam', '352', 'Bình Lục', NULL),
(21, 7, 241000.00, 'COD', 'pending', 'pending', '2025-11-21 15:52:43', NULL, 'hjhj', '09127303021', 'gg', 25000.00, 240000.00, 1, 24000.00, 'ty', '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(22, 7, 845000.00, 'COD', 'pending', 'pending', '2025-11-22 21:01:41', NULL, 'hjhj', '09127303021', 'gg', 25000.00, 820000.00, NULL, 0.00, '', '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(23, 7, 1006000.00, 'COD', 'pending', 'pending', '2025-11-22 22:37:52', NULL, 'hjhj', '09127303021', 'gg', 25000.00, 1090000.00, 1, 109000.00, '', '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(24, 7, 985000.00, 'COD', 'pending', 'pending', '2025-11-23 00:04:31', NULL, 'hjhj', '09127303021', 'gg', 25000.00, 960000.00, NULL, 0.00, '', '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(25, 7, 718000.00, 'COD', 'pending', 'pending', '2025-11-23 12:45:47', 1, 'hjhj', '09127303021', 'gg', 25000.00, 770000.00, 1, 77000.00, 'hj', '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(26, 7, 1177000.00, 'COD', 'pending', 'pending', '2025-11-24 16:07:27', 1, 'hjhj', '09127303021', 'gg', 25000.00, 1280000.00, 1, 128000.00, NULL, '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(27, 7, 277000.00, 'COD', 'pending', 'completed', '2025-11-24 20:13:39', 3, 'bt', '09127303021', 'bt', 25000.00, 280000.00, 1, 28000.00, NULL, '89', 'An Giang', '886', 'An Phú', NULL),
(28, 7, 439000.00, 'COD', 'pending', 'pending', '2025-11-24 23:17:03', 1, 'hjhj', '09127303021', 'gg', 25000.00, 460000.00, 1, 46000.00, NULL, '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(29, 7, 165000.00, 'COD', 'pending', 'pending', '2025-11-29 15:55:52', 1, 'hjhj', '09127303021', 'gg', 25000.00, 140000.00, NULL, 0.00, NULL, '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(30, 7, 1825000.00, 'COD', 'pending', 'pending', '2025-11-29 16:08:44', 1, 'hjhj', '09127303021', 'gg', 25000.00, 1800000.00, NULL, 0.00, NULL, '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(31, 7, 165000.00, 'COD', 'pending', 'pending', '2025-11-29 16:15:19', 1, 'hjhj', '09127303021', 'gg', 25000.00, 140000.00, NULL, 0.00, NULL, '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(32, 7, 165000.00, 'COD', 'pending', 'pending', '2025-11-29 16:15:27', 1, 'hjhj', '09127303021', 'gg', 25000.00, 140000.00, NULL, 0.00, NULL, '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(33, 7, 1825000.00, 'COD', 'pending', 'pending', '2025-11-29 16:15:49', 1, 'hjhj', '09127303021', 'gg', 25000.00, 1800000.00, NULL, 0.00, NULL, '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(34, 7, 165000.00, 'COD', 'pending', 'pending', '2025-11-29 16:17:09', 1, 'hjhj', '09127303021', 'gg', 25000.00, 140000.00, NULL, 0.00, NULL, '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(35, 7, 165000.00, 'COD', 'pending', 'pending', '2025-11-29 16:17:48', 1, 'hjhj', '09127303021', 'gg', 25000.00, 140000.00, NULL, 0.00, NULL, '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(36, 7, 165000.00, 'COD', 'pending', 'pending', '2025-11-29 16:38:32', 1, 'hjhj', '09127303021', 'gg', 25000.00, 140000.00, NULL, 0.00, NULL, '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(37, 7, 165000.00, 'COD', 'pending', 'confirmed', '2025-11-29 16:40:31', 1, 'hjhj', '09127303021', 'gg', 25000.00, 140000.00, NULL, 0.00, NULL, '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(38, 7, 165000.00, 'COD', 'pending', 'cancelled', '2025-11-29 16:42:51', 1, 'hjhj', '09127303021', 'gg', 25000.00, 140000.00, NULL, 0.00, NULL, '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(39, 8, 165000.00, 'COD', 'paid', 'completed', '2025-11-29 21:04:38', 6, 'hjj', '0917293932', '123', 25000.00, 140000.00, NULL, 0.00, NULL, '02', 'Hà Giang', '031', 'Bắc Mê', NULL),
(40, 7, 416500.00, 'COD', 'paid', 'completed', '2025-12-01 15:44:11', 5, 'hj', '0917283833', '123', 25000.00, 435000.00, 1, 43500.00, NULL, '04', 'Cao Bằng', '043', 'Bảo Lạc', NULL),
(41, 7, 997000.00, 'COD', 'paid', 'completed', '2025-12-02 12:26:12', 1, 'hjhj', '09127303021', 'gg', 25000.00, 1080000.00, 1, 108000.00, 'liền', '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(43, 5, 280000.00, 'Tiền mặt', 'paid', 'completed', '2025-12-02 12:55:39', NULL, 'Khách lẻ', '', 'Mua tại quầy', 25000.00, 280000.00, NULL, 0.00, NULL, '00', 'N/A', '0000', 'N/A', NULL),
(44, 5, 1800000.00, 'Tiền mặt', 'paid', 'completed', '2025-12-02 12:56:41', NULL, 'Khách lẻ', '', 'Mua tại quầy', 25000.00, 1800000.00, NULL, 0.00, NULL, '00', 'N/A', '0000', 'N/A', NULL),
(45, 10, 2080000.00, 'Tiền mặt', 'paid', 'completed', '2025-12-02 13:52:57', NULL, 'Khách lẻ', '0911111111', 'Mua tại quầy', 25000.00, 2080000.00, NULL, 0.00, NULL, '00', 'N/A', '0000', 'N/A', NULL),
(46, 10, 1720000.00, 'Tiền mặt', 'paid', 'completed', '2025-12-02 14:35:12', NULL, 'Khách lẻ', '0911111111', 'Mua tại quầy', 25000.00, 2220000.00, NULL, 500000.00, NULL, '00', 'N/A', '0000', 'N/A', NULL),
(47, 7, 263500.00, 'COD', 'pending', 'pending', '2025-12-02 17:35:17', 1, 'hjhj', '09127303021', 'gg', 25000.00, 265000.00, 1, 26500.00, NULL, '01', 'Hà Nội', '273', 'Đan Phượng', NULL),
(48, 10, 360000.00, 'Tiền mặt', 'paid', 'completed', '2025-12-02 17:40:29', NULL, 'Khách lẻ', '0911111111', 'Mua tại quầy', 25000.00, 360000.00, NULL, 0.00, NULL, '00', 'N/A', '0000', 'N/A', NULL),
(49, 8, 720000.00, 'Tiền mặt', 'paid', 'completed', '2025-12-02 17:41:33', NULL, 'Rock Tuber', '0923456789', 'Mua tại quầy', 25000.00, 720000.00, NULL, 0.00, NULL, '00', 'N/A', '0000', 'N/A', NULL),
(50, 10, 1850000.00, 'Tiền mặt', 'paid', 'completed', '2025-12-02 18:56:53', NULL, 'Khách lẻ', '0911111111', 'Mua tại quầy', 25000.00, 1850000.00, NULL, 0.00, NULL, '00', 'N/A', '0000', 'N/A', NULL),
(51, 10, 1800000.00, 'Tiền mặt', 'paid', 'completed', '2025-12-02 19:08:04', NULL, 'Khách lẻ', '0911111111', 'Mua tại quầy', 25000.00, 1800000.00, NULL, 0.00, NULL, '00', 'N/A', '0000', 'N/A', NULL),
(52, 10, 280000.00, 'Tiền mặt', 'paid', 'completed', '2025-12-02 19:20:53', NULL, 'Khách lẻ', '0911111111', 'Mua tại quầy', 25000.00, 280000.00, NULL, 0.00, NULL, '00', 'N/A', '0000', 'N/A', NULL),
(53, 10, 140000.00, 'Tiền mặt', 'paid', 'completed', '2025-12-02 19:44:08', NULL, 'Khách lẻ', '0911111111', 'Mua tại quầy', 25000.00, 140000.00, NULL, 0.00, NULL, '00', 'N/A', '0000', 'N/A', NULL),
(54, 10, 550000.00, 'Tiền mặt', 'paid', 'completed', '2025-12-02 19:52:46', NULL, 'Khách lẻ', '0911111111', 'Mua tại quầy', 25000.00, 550000.00, NULL, 0.00, NULL, '00', 'N/A', '0000', 'N/A', NULL),
(55, 10, 180000.00, 'Tiền mặt', 'paid', 'completed', '2025-12-02 20:00:08', NULL, 'Khách lẻ', '0911111111', 'Mua tại quầy', 25000.00, 180000.00, NULL, 0.00, NULL, '00', 'N/A', '0000', 'N/A', NULL),
(56, 10, 950000.00, 'Tiền mặt', 'paid', 'completed', '2025-12-02 20:05:40', NULL, 'Khách lẻ', '0911111111', 'Mua tại quầy', 25000.00, 950000.00, NULL, 0.00, NULL, '00', 'N/A', '0000', 'N/A', NULL),
(57, 5, 355000.00, 'Tiền mặt', 'paid', 'completed', '2025-12-02 20:11:29', NULL, 'adminn', '0917272727', 'Mua tại quầy', 25000.00, 355000.00, NULL, 0.00, NULL, '00', 'N/A', '0000', 'N/A', NULL),
(58, 5, 620000.00, 'Tiền mặt', 'paid', 'completed', '2025-12-02 20:18:48', NULL, 'adminn', '0917272727', 'Mua tại quầy', 25000.00, 620000.00, NULL, 0.00, NULL, '00', 'N/A', '0000', 'N/A', NULL),
(59, 10, 810000.00, 'Tiền mặt', 'paid', 'completed', '2025-12-02 20:28:54', NULL, 'Khách lẻ', '0911111111', 'Mua tại quầy', 25000.00, 810000.00, NULL, 0.00, NULL, '00', 'N/A', '0000', 'N/A', NULL),
(60, 5, 430000.00, 'Tiền mặt', 'paid', 'completed', '2025-12-02 20:41:47', NULL, 'adminn', '0917272727', 'Mua tại quầy', 25000.00, 430000.00, NULL, 0.00, NULL, '00', 'N/A', '0000', 'N/A', NULL),
(61, 10, 565000.00, 'Tiền mặt', 'paid', 'completed', '2025-12-02 20:43:21', NULL, 'Khách lẻ', '0911111111', 'Mua tại quầy', 25000.00, 565000.00, NULL, 0.00, NULL, '00', 'N/A', '0000', 'N/A', NULL),
(62, 5, 490000.00, 'Tiền mặt', 'paid', 'completed', '2025-12-02 20:44:40', NULL, 'adminn', '0917272727', 'Mua tại quầy', 25000.00, 490000.00, NULL, 0.00, NULL, '00', 'N/A', '0000', 'N/A', NULL);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `order_items`
--

CREATE TABLE `order_items` (
  `order_item_id` int(11) NOT NULL,
  `order_id` int(11) NOT NULL,
  `product_id` int(11) DEFAULT NULL,
  `service_id` int(11) DEFAULT NULL,
  `quantity` int(11) NOT NULL,
  `price` decimal(18,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `order_items`
--

INSERT INTO `order_items` (`order_item_id`, `order_id`, `product_id`, `service_id`, `quantity`, `price`) VALUES
(35, 22, 2, NULL, 2, 410000.00),
(36, 23, 3, NULL, 1, 550000.00),
(37, 23, 4, NULL, 1, 180000.00),
(38, 23, 1, NULL, 1, 360000.00),
(39, 24, 2, NULL, 1, 410000.00),
(40, 24, 3, NULL, 1, 550000.00),
(41, 25, 1, NULL, 1, 360000.00),
(42, 25, 2, NULL, 1, 410000.00),
(43, 26, 3, NULL, 2, 550000.00),
(44, 26, 4, NULL, 1, 180000.00),
(45, 27, 39, NULL, 2, 140000.00),
(46, 28, 22, NULL, 2, 230000.00),
(47, 29, 39, NULL, 1, 140000.00),
(48, 30, 48, NULL, 1, 1800000.00),
(49, 31, 39, NULL, 1, 140000.00),
(50, 32, 39, NULL, 1, 140000.00),
(51, 33, 48, NULL, 1, 1800000.00),
(52, 34, 39, NULL, 1, 140000.00),
(53, 35, 39, NULL, 1, 140000.00),
(54, 36, 39, NULL, 1, 140000.00),
(55, 37, 39, NULL, 1, 140000.00),
(56, 38, 39, NULL, 1, 140000.00),
(57, 39, 39, NULL, 1, 140000.00),
(58, 40, 8, NULL, 1, 45000.00),
(59, 40, 7, NULL, 1, 220000.00),
(60, 40, 20, NULL, 1, 150000.00),
(61, 40, 21, NULL, 1, 20000.00),
(62, 41, 4, NULL, 1, 180000.00),
(63, 41, 5, NULL, 2, 450000.00),
(65, 43, 39, NULL, 2, 140000.00),
(66, 44, 48, NULL, 1, 1800000.00),
(67, 45, 48, NULL, 1, 1800000.00),
(68, 45, 39, NULL, 2, 140000.00),
(69, 46, 48, NULL, 1, 1800000.00),
(70, 46, 39, NULL, 3, 140000.00),
(71, 47, 7, NULL, 1, 220000.00),
(72, 47, 8, NULL, 1, 45000.00),
(73, 48, 1, NULL, 1, 360000.00),
(74, 49, 1, NULL, 2, 360000.00),
(75, 50, NULL, 6, 1, 0.00),
(76, 50, NULL, 14, 1, 50000.00),
(77, 50, 48, NULL, 1, 1800000.00),
(78, 51, NULL, 6, 1, 0.00),
(79, 51, 48, NULL, 1, 1800000.00),
(80, 52, NULL, 7, 1, 0.00),
(81, 52, 39, NULL, 2, 140000.00),
(82, 53, NULL, 6, 1, 0.00),
(83, 53, 39, NULL, 1, 140000.00),
(84, 54, NULL, 6, 1, 0.00),
(85, 54, 3, NULL, 1, 550000.00),
(86, 55, NULL, 5, 1, 0.00),
(87, 55, 4, NULL, 1, 180000.00),
(88, 56, NULL, 6, 1, 400000.00),
(89, 56, 3, NULL, 1, 550000.00),
(90, 57, NULL, 5, 1, 210000.00),
(91, 57, 8, NULL, 1, 45000.00),
(92, 57, NULL, 10, 1, 100000.00),
(93, 58, NULL, 5, 1, 210000.00),
(94, 58, 2, NULL, 1, 410000.00),
(95, 59, NULL, 6, 1, 400000.00),
(96, 59, 2, NULL, 1, 410000.00),
(97, 60, NULL, 5, 1, 210000.00),
(98, 60, 7, NULL, 1, 220000.00),
(99, 61, NULL, 6, 1, 400000.00),
(100, 61, 11, NULL, 3, 55000.00),
(101, 62, NULL, 5, 1, 210000.00),
(102, 62, 39, NULL, 2, 140000.00);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `pets`
--

CREATE TABLE `pets` (
  `pet_id` int(11) NOT NULL,
  `customer_id` int(11) NOT NULL,
  `name` varchar(100) NOT NULL,
  `type` varchar(50) NOT NULL COMMENT 'dog, cat, etc.',
  `breed` varchar(100) DEFAULT NULL,
  `age` int(11) DEFAULT NULL,
  `image_url` varchar(500) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `pets`
--

INSERT INTO `pets` (`pet_id`, `customer_id`, `name`, `type`, `breed`, `age`, `image_url`) VALUES
(1, 1, 'Milo', 'Chó', 'Poodle', 2, 'https://i.imgur.com/pet1.jpg'),
(2, 1, 'Luna', 'Mèo', 'Anh Lông Ngắn', 3, 'https://i.imgur.com/pet2.jpg'),
(3, 4, 'hj', 'Chó', 'hj', 11, NULL),
(4, 4, 'hj', 'Chó', 'hj', 11, NULL),
(5, 4, 'hj', 'Chó', 'hj', 11, NULL),
(6, 4, '', '', '', NULL, NULL),
(7, 5, 'dfd', 'chó', 'dvdv', 1, NULL),
(8, 6, 'ss', 'Mèo', 'hj', 1, NULL);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `products`
--

CREATE TABLE `products` (
  `product_id` int(11) NOT NULL,
  `category_id` int(11) NOT NULL,
  `name` varchar(200) NOT NULL,
  `price` decimal(18,2) NOT NULL,
  `unit` varchar(50) DEFAULT NULL,
  `stock_quantity` int(11) NOT NULL DEFAULT 0,
  `description` text DEFAULT NULL,
  `image_url` varchar(500) DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp(),
  `is_active` tinyint(1) DEFAULT 1,
  `brand` varchar(200) CHARACTER SET utf8 COLLATE utf8_general_ci DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `products`
--

INSERT INTO `products` (`product_id`, `category_id`, `name`, `price`, `unit`, `stock_quantity`, `description`, `image_url`, `created_at`, `is_active`, `brand`) VALUES
(1, 1, 'Hạt Royal Canin Mini Adult', 360000.00, 'Túi 2kg', 45, 'Thức ăn cho chó trưởng thành giống nhỏ dưới 10kg.', 'rc-mini-adult.jpg', '2025-11-22 20:39:55', 1, 'Royal Canin'),
(2, 1, 'Hạt Royal Canin Puppy Mini', 410000.00, 'Túi 2kg', 34, 'Dinh dưỡng tối ưu cho chó con giống nhỏ.', 'rc-mini-puppy.jpg', '2025-11-22 20:39:55', 1, 'Royal Canin'),
(3, 1, 'Hạt SmartHeart Power Pack Adult', 550000.00, 'Túi 3kg', 24, 'Giàu năng lượng, giúp tăng cơ bắp cho chó vận động nhiều.', 'smartheart-power.jpg', '2025-11-22 20:39:55', 1, 'SmartHeart'),
(4, 1, 'Hạt SmartHeart Gold Puppy', 180000.00, 'Túi 1kg', 56, 'Dòng cao cấp hỗ trợ tiêu hóa cho chó con.', 'smartheart-gold.jpg', '2025-11-22 20:39:55', 1, 'SmartHeart'),
(5, 1, 'Hạt Ganador Premium Adult', 450000.00, 'Bao 10kg', 18, 'Thức ăn hỗn hợp vị Cừu và Gạo.', 'ganador-adult.jpg', '2025-11-22 20:39:55', 1, 'Ganador'),
(6, 1, 'Hạt Ganador Puppy Sữa & DHA', 120000.00, 'Túi 1.5kg', 50, 'Bổ sung DHA giúp phát triển trí não chó con.', 'ganador-puppy.jpg', '2025-11-22 20:39:55', 1, 'Ganador'),
(7, 1, 'Hạt Zenith Soft Kibble (Hạt mềm)', 220000.00, 'Túi 1.2kg', 32, 'Hạt mềm cho chó kén ăn và hệ tiêu hóa nhạy cảm.', 'zenith-dog.jpg', '2025-11-22 20:39:55', 1, 'Zenith'),
(8, 1, 'Pate Pedigree Vị Bò Kho', 45000.00, 'Lon 400g', 97, 'Pate dạng sốt hương vị bò thơm ngon.', 'pedigree-beef.jpg', '2025-11-22 20:39:55', 1, 'Pedigree'),
(9, 1, 'Pate Pedigree Vị Gà & Rau Củ', 45000.00, 'Lon 400g', 100, 'Cung cấp chất xơ và vitamin thiết yếu.', 'pedigree-chicken.jpg', '2025-11-22 20:39:55', 1, 'Pedigree'),
(10, 1, 'Xương gặm sạch răng Dentastix', 85000.00, 'Gói 7 thanh', 80, 'Giảm cao răng và mảng bám hiệu quả.', 'dentastix.jpg', '2025-11-22 20:39:55', 1, 'Pedigree'),
(11, 1, 'Bánh thưởng JerHigh vị Gà', 55000.00, 'Gói 70g', 147, 'Snack thưởng thơm ngon từ thịt gà thật.', 'jerhigh-chicken.jpg', '2025-11-22 20:39:55', 1, 'JerHigh'),
(12, 1, 'Hạt Nutrience Subzero Fraser Valley', 1350000.00, 'Túi 5kg', 15, 'Thức ăn cao cấp không ngũ cốc từ Canada.', 'nutrience-subzero.jpg', '2025-11-22 20:39:55', 1, 'Nutrience'),
(13, 1, 'Hạt ANF 6 Free Hữu Cơ', 480000.00, 'Túi 2kg', 25, 'Nguyên liệu hữu cơ sạch, an toàn tuyệt đối.', 'anf-6free.jpg', '2025-11-22 20:39:55', 1, 'ANF'),
(14, 1, 'Sữa bột Bio Milk For Pet', 120000.00, 'Hộp 100g', 60, 'Sữa thay thế sữa mẹ cho chó con sơ sinh.', 'bio-milk.jpg', '2025-11-22 20:39:55', 1, 'Bio Pharmachemie'),
(15, 1, 'Viên nhai NexGard trị ve rận (2-4kg)', 180000.00, 'Viên', 200, 'Đặc trị ve, rận, ghẻ, giun tim hiệu quả 1 tháng.', 'nexgard-xs.jpg', '2025-11-22 20:39:55', 1, 'Boehringer Ingelheim'),
(16, 2, 'Hạt Royal Canin Indoor 27', 390000.00, 'Túi 2kg', 45, 'Giảm mùi hôi phân cho mèo nuôi trong nhà.', 'rc-indoor.jpg', '2025-11-22 20:39:55', 1, 'Royal Canin'),
(17, 2, 'Hạt Royal Canin Mother & Babycat', 450000.00, 'Túi 2kg', 50, 'Dành cho mèo mẹ mang thai và mèo con tập ăn.', 'rc-babycat.jpg', '2025-11-22 20:39:55', 1, 'Royal Canin'),
(18, 2, 'Hạt Royal Canin Hairball Care', 420000.00, 'Túi 2kg', 30, 'Hỗ trợ tiêu búi lông trong dạ dày mèo.', 'rc-hairball.jpg', '2025-11-22 20:39:55', 1, 'Royal Canin'),
(19, 2, 'Hạt Whiskas Vị Cá Biển', 140000.00, 'Túi 1.2kg', 80, 'Hương vị cá biển kích thích vị giác.', 'whiskas-ocean.jpg', '2025-11-22 20:39:55', 1, 'Whiskas'),
(20, 2, 'Hạt Whiskas Junior (Mèo con)', 150000.00, 'Túi 1.1kg', 69, 'Giàu canxi giúp khung xương chắc khỏe.', 'whiskas-junior.jpg', '2025-11-22 20:39:55', 1, 'Whiskas'),
(21, 2, 'Pate Whiskas Vị Cá Ngừ', 20000.00, 'Gói 85g', 199, 'Sốt cá ngừ thơm ngon, tiện lợi.', 'whiskas-pouch.jpg', '2025-11-22 20:39:55', 1, 'Whiskas'),
(22, 2, 'Hạt Catsrang Adult', 230000.00, 'Túi 2kg', 58, 'Ngăn ngừa bệnh đường tiết niệu và búi lông.', 'catsrang-adult.jpg', '2025-11-22 20:39:55', 1, 'Catsrang'),
(23, 2, 'Hạt Catsrang Kitten', 240000.00, 'Túi 2kg', 55, 'Hạt nhỏ dễ nhai cho mèo con.', 'catsrang-kitten.jpg', '2025-11-22 20:39:55', 1, 'Catsrang'),
(24, 2, 'Súp thưởng Ciao Churu Cá ngừ', 65000.00, 'Gói 4 thanh', 150, 'Món ăn vặt thần thánh mà mèo nào cũng mê.', 'ciao-churu.jpg', '2025-11-22 20:39:55', 1, 'Ciao'),
(25, 2, 'Hạt Me-O Vị Hải Sản', 110000.00, 'Túi 1.2kg', 90, 'Giá thành hợp lý, hương vị đậm đà.', 'meo-seafood.jpg', '2025-11-22 20:39:55', 1, 'Me-O'),
(26, 2, 'Pate Me-O Delite', 22000.00, 'Gói 70g', 180, 'Dòng pate cao cấp làm từ thịt thật.', 'meo-delite.jpg', '2025-11-22 20:39:55', 1, 'Me-O'),
(27, 2, 'Hạt Reflex Plus Salmon', 320000.00, 'Túi 1.5kg', 40, 'Thức ăn cao cấp từ Thổ Nhĩ Kỳ vị cá hồi.', 'reflex-plus.jpg', '2025-11-22 20:39:55', 1, 'Reflex'),
(28, 2, 'Pate Snappy Tom Lon', 35000.00, 'Lon 400g', 120, 'Pate cá ngừ nguyên miếng.', 'snappy-tom.jpg', '2025-11-22 20:39:55', 1, 'Snappy Tom'),
(29, 2, 'Cỏ mèo tươi (Catnip)', 50000.00, 'Hũ', 50, 'Giúp mèo thư giãn, giảm stress.', 'catnip.jpg', '2025-11-22 20:39:55', 1, 'Hartz'),
(30, 2, 'Gel dinh dưỡng Virbac Nutri-plus', 210000.00, 'Tuýp 120g', 30, 'Bổ sung năng lượng cấp tốc cho mèo ốm.', 'virbac-gel.jpg', '2025-11-22 20:39:55', 1, 'Virbac'),
(31, 4, 'Sữa tắm Fay Puppy', 85000.00, 'Chai 300ml', 50, 'Sữa tắm dịu nhẹ cho chó mèo con.', 'fay-puppy.jpg', '2025-11-22 20:39:55', 1, 'Fay'),
(32, 4, 'Sữa tắm SOS Màu Xanh (Mềm mượt)', 110000.00, 'Chai 530ml', 80, 'Giúp lông mềm mượt và giữ hương lâu.', 'sos-blue.jpg', '2025-11-22 20:39:55', 1, 'SOS'),
(33, 4, 'Sữa tắm SOS Màu Trắng (Lông trắng)', 110000.00, 'Chai 530ml', 60, 'Dành riêng cho thú cưng lông trắng.', 'sos-white.jpg', '2025-11-22 20:39:55', 1, 'SOS'),
(34, 4, 'Sữa tắm Joyce & Dolls Bloom', 350000.00, 'Chai 500ml', 30, 'Hương nước hoa cao cấp sang trọng.', 'joyce-bloom.jpg', '2025-11-22 20:39:55', 1, 'Joyce & Dolls'),
(35, 4, 'Xịt khử mùi hôi miệng TropiClean', 320000.00, 'Chai 118ml', 40, 'Làm sạch mảng bám và thơm miệng không cần chải.', 'tropiclean-fresh.jpg', '2025-11-22 20:39:55', 1, 'TropiClean'),
(36, 4, 'Lược chải lông Furminator Size S', 350000.00, 'Cái', 25, 'Loại bỏ 90% lông rụng chết.', 'furminator-s.jpg', '2025-11-22 20:39:55', 1, 'Furminator'),
(37, 4, 'Cát vệ sinh Nhật (Đen)', 55000.00, 'Túi 8L', 200, 'Vón cục tốt, khử mùi cà phê.', 'cat-nhat.jpg', '2025-11-22 20:39:55', 1, 'Min'),
(38, 4, 'Cát vệ sinh Genki', 65000.00, 'Túi 5L', 150, 'Cát Bentonite ít bụi.', 'genki.jpg', '2025-11-22 20:39:55', 1, 'Genki'),
(39, 4, 'Cát đậu nành Cature', 140000.00, 'Túi 2.5kg', 36, 'An toàn, có thể xả bồn cầu.', 'cature-tofu.jpg', '2025-11-23 20:39:55', 1, 'Cature'),
(40, 4, 'Xịt diệt khuẩn Bioion', 180000.00, 'Chai 500ml', 45, 'Diệt 99.9% vi khuẩn và nấm mốc.', 'bioion.jpg', '2025-11-22 20:39:55', 1, 'Bioion'),
(41, 4, 'Tông đơ cắt lông Codos CP-6800', 450000.00, 'Bộ', 20, 'Máy êm, lưỡi sứ an toàn.', 'codos-6800.jpg', '2025-11-22 20:39:55', 1, 'Codos'),
(42, 4, 'Kìm cắt móng có đèn LED', 120000.00, 'Cái', 50, 'Giúp soi mạch máu móng chân dễ dàng.', 'dele-clipper.jpg', '2025-11-22 20:39:55', 1, 'Dele'),
(43, 4, 'Bỉm cho chó cái Dono (Bịch)', 110000.00, 'Bịch', 60, 'Thấm hút tốt cho chó đến kỳ.', 'dono-female.jpg', '2025-11-22 20:39:55', 1, 'Dono'),
(44, 4, 'Tấm lót vệ sinh Absorb Plus', 160000.00, 'Bịch 100 miếng', 40, 'Khử mùi than hoạt tính siêu thấm.', 'absorb-plus.jpg', '2025-11-22 20:39:55', 1, 'Absorb Plus'),
(45, 3, 'Đồ chơi Kong Classic Size M', 320000.00, 'Cái', 42, 'Đồ chơi cao su siêu bền cho chó.', 'kong-classic.jpg', '2025-11-22 20:39:55', 1, 'Kong'),
(46, 3, 'Dây dắt tự động Flexi New Classic', 450000.00, 'Cái', 10, 'Dây dắt dài 5m nhập khẩu Đức.', 'flexi-s.jpg', '2025-11-23 20:39:55', 1, 'Flexi'),
(47, 3, 'Bát ăn đôi chống gù lưng', 150000.00, 'Cái', 45, 'Thiết kế nghiêng bảo vệ cột sống.', 'super-design-bowl.jpg', '2025-11-22 20:39:55', 1, 'Super Design'),
(48, 3, 'Máy cho ăn tự động Petkit Mini', 1800000.00, 'Cái', 23, 'Điều khiển qua App điện thoại.', 'petkit-mini.jpg', '2025-11-23 20:39:55', 1, 'Petkit'),
(49, 3, 'Máy lọc nước Petkit Solo', 950000.00, 'Cái', 15, 'Nước sạch liên tục cho thú cưng.', 'petkit-solo.jpg', '2025-11-22 20:39:55', 1, 'Petkit'),
(50, 3, 'Túi vận chuyển phi hành gia', 250000.00, 'Cái', 50, 'Nhựa cứng trong suốt, thoáng khí.', 'm-pets-carrier.jpg', '2025-11-22 20:39:55', 1, 'M-Pets'),
(51, 3, 'Vòng cổ trị ve rận Bioline', 80000.00, 'Cái', 100, 'Xua đuổi ve rận trong 3 tháng.', 'bioline-collar.jpg', '2025-11-22 20:39:55', 1, 'Bioline'),
(52, 3, 'Chuồng sơn tĩnh điện (Size M)', 450000.00, 'Cái', 20, 'Kích thước 60x40cm có khay hứng.', 'vina-cage.jpg', '2025-11-22 20:39:55', 1, 'Vina'),
(53, 3, 'Nhà vệ sinh cho mèo nắp kín', 350000.00, 'Cái', 25, 'Ngăn mùi hiệu quả, kèm xẻng.', 'agile-litter-box.jpg', '2025-11-22 20:39:55', 1, 'Agile'),
(54, 3, 'Trụ cào móng xương rồng', 280000.00, 'Cái', 35, 'Giúp mèo mài móng, bảo vệ sofa.', 'cat-tree-cactus.jpg', '2025-11-22 20:39:55', 1, 'Vetreska'),
(55, 3, 'Áo thun mùa hè Hipipet', 95000.00, 'Cái', 100, 'Chất liệu Cotton thoáng mát.', 'hipipet-shirt.jpg', '2025-11-22 20:39:55', 1, 'Hipipet'),
(56, 3, 'Bóng cao su GiGwi có tiếng kêu', 120000.00, 'Cái', 60, 'Bóng nảy tốt, độ bền cao.', 'gigwi-ball.jpg', '2025-11-22 20:39:55', 1, 'GiGwi'),
(57, 3, 'Đồ chơi cần câu mèo lông vũ', 50000.00, 'Cái', 80, 'Kích thích bản năng săn mồi.', 'cat-wand.jpg', '2025-11-22 20:39:55', 1, 'M-Pets'),
(58, 3, 'Đệm nằm tròn lông xù', 180000.00, 'Cái', 40, 'Siêu êm ái cho giấc ngủ ngon.', 'fluffy-bed.jpg', '2025-11-22 20:39:55', 1, 'M-Pets'),
(59, 3, 'Vòng cổ da khắc tên', 120000.00, 'Cái', 70, 'Da PU cao cấp, kèm thẻ tên.', 'leather-collar.jpg', '2025-11-22 20:39:55', 1, 'Mon Ami'),
(60, 3, 'Rọ mõm mỏ vịt Silicon', 65000.00, 'Cái', 50, 'Ngăn chó cắn sủa nhưng vẫn dễ thở.', 'duck-muzzle.jpg', '2025-11-22 20:39:55', 1, 'Dono');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `promotions`
--

CREATE TABLE `promotions` (
  `promotion_id` int(11) NOT NULL,
  `code` varchar(50) NOT NULL,
  `description` text DEFAULT NULL,
  `discount_percent` decimal(5,2) NOT NULL,
  `start_date` datetime NOT NULL,
  `end_date` datetime NOT NULL,
  `is_active` tinyint(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `promotions`
--

INSERT INTO `promotions` (`promotion_id`, `code`, `description`, `discount_percent`, `start_date`, `end_date`, `is_active`) VALUES
(1, 'WELCOME10', 'Giảm 10% cho khách hàng mới lần đầu sử dụng dịch vụ.', 10.00, '2025-01-01 00:00:00', '2025-12-31 23:59:59', 1),
(2, 'SUMMER20', 'Giảm 20% cho tất cả dịch vụ Spa trong tháng 6.', 20.00, '2025-06-01 00:00:00', '2025-06-30 23:59:59', 1),
(3, 'HJHJHJ', 'Giảm sâu', 12.00, '2025-12-01 21:11:17', '2025-12-08 21:11:17', 1);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `purchase_orders`
--

CREATE TABLE `purchase_orders` (
  `purchase_order_id` int(11) NOT NULL,
  `supplier_id` int(11) NOT NULL,
  `staff_id` int(11) NOT NULL,
  `total_amount` decimal(18,2) NOT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `purchase_orders`
--

INSERT INTO `purchase_orders` (`purchase_order_id`, `supplier_id`, `staff_id`, `total_amount`, `created_at`) VALUES
(2, 1, 5, 3000000.00, '2025-12-02 22:47:52'),
(3, 1, 5, 22400000.00, '2025-12-02 22:58:32');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `purchase_order_items`
--

CREATE TABLE `purchase_order_items` (
  `id` int(11) NOT NULL,
  `purchase_order_id` int(11) NOT NULL,
  `product_id` int(11) NOT NULL,
  `quantity` int(11) NOT NULL,
  `price` decimal(18,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `purchase_order_items`
--

INSERT INTO `purchase_order_items` (`id`, `purchase_order_id`, `product_id`, `quantity`, `price`) VALUES
(2, 2, 46, 10, 300000.00),
(3, 3, 48, 20, 1000000.00),
(4, 3, 45, 12, 200000.00);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `services`
--

CREATE TABLE `services` (
  `service_id` int(11) NOT NULL,
  `category_id` int(11) NOT NULL,
  `name` varchar(200) NOT NULL,
  `duration_minutes` int(11) NOT NULL,
  `price` decimal(18,2) NOT NULL,
  `description` text DEFAULT NULL,
  `is_active` tinyint(1) DEFAULT 1,
  `pricing_method` varchar(20) DEFAULT 'fixed' COMMENT '''fixed: giá cố định, weight_based: theo cân nặng'';'
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `services`
--

INSERT INTO `services` (`service_id`, `category_id`, `name`, `duration_minutes`, `price`, `description`, `is_active`, `pricing_method`) VALUES
(1, 5, 'Tắm gội cơ bản (Cũ - Ngừng KD)', 60, 200000.00, 'Bao gồm: tắm bằng sữa tắm chuyên dụng, sấy khô, chải lông, cắt móng, vệ sinh tai.', 0, 'fixed'),
(2, 5, 'Cắt tỉa lông tạo kiểu (Cũ - Ngừng KD)', 120, 450000.00, 'Bao gồm gói tắm gội cơ bản và cắt tỉa lông theo yêu cầu hoặc theo kiểu phổ biến.', 0, 'fixed'),
(3, 6, 'Combo Spa Thư giãn (Cũ - Ngừng KD)', 90, 500000.00, 'Tắm thảo dược, massage thư giãn toàn thân, ủ dưỡng lông siêu mượt.', 0, 'fixed'),
(4, 7, 'Trông giữ theo ngày (dưới 10kg) (Cũ - Ngừng KD)', 1440, 250000.00, 'Bao gồm ăn uống 2 bữa, dắt đi dạo và khu vui chơi riêng biệt. Không bao gồm dịch vụ tắm rửa.', 0, 'fixed'),
(5, 5, 'Tắm vệ sinh', 60, 0.00, NULL, 1, 'weight_based'),
(6, 5, 'Tắm + Cạo lông', 90, 0.00, NULL, 1, 'weight_based'),
(7, 5, 'Tắm + Cắt tỉa', 120, 0.00, NULL, 1, 'weight_based'),
(8, 8, 'Nhuộm 2 tai', 30, 200000.00, NULL, 1, 'fixed'),
(9, 8, 'Nhuộm 4 chân', 60, 400000.00, NULL, 1, 'fixed'),
(10, 8, 'Nhuộm đuôi', 30, 100000.00, NULL, 1, 'fixed'),
(11, 8, 'Điều trị ve', 15, 50000.00, NULL, 1, 'fixed'),
(12, 8, 'Tắm trắng', 15, 50000.00, NULL, 1, 'fixed'),
(13, 8, 'Tắm trị bệnh da', 15, 50000.00, NULL, 1, 'fixed'),
(14, 8, 'Phụ thu gỡ rối (nhỏ)', 30, 50000.00, NULL, 1, 'fixed'),
(15, 8, 'Phụ thu gỡ rối (lớn)', 45, 100000.00, NULL, 1, 'fixed'),
(16, 8, 'Đánh răng', 15, 50000.00, NULL, 1, 'fixed');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `service_prices`
--

CREATE TABLE `service_prices` (
  `price_id` int(11) NOT NULL,
  `service_id` int(11) NOT NULL,
  `pet_type` varchar(10) NOT NULL COMMENT 'dog: chó, cat: mèo',
  `min_weight` decimal(5,2) NOT NULL DEFAULT 0.00 COMMENT 'Cân nặng tối thiểu (kg)',
  `max_weight` decimal(5,2) NOT NULL DEFAULT 999.00 COMMENT 'Cân nặng tối đa (kg)',
  `price` decimal(18,2) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `service_prices`
--

INSERT INTO `service_prices` (`price_id`, `service_id`, `pet_type`, `min_weight`, `max_weight`, `price`) VALUES
(1, 5, 'dog', 0.00, 3.00, 150000.00),
(2, 5, 'dog', 3.00, 5.00, 180000.00),
(3, 5, 'dog', 5.00, 10.00, 210000.00),
(4, 5, 'dog', 10.00, 20.00, 350000.00),
(5, 5, 'dog', 20.00, 30.00, 500000.00),
(6, 5, 'dog', 30.00, 40.00, 600000.00),
(7, 5, 'cat', 0.00, 2.00, 130000.00),
(8, 5, 'cat', 2.00, 5.00, 180000.00),
(9, 5, 'cat', 5.00, 7.00, 250000.00),
(10, 5, 'cat', 7.00, 20.00, 300000.00),
(16, 6, 'dog', 0.00, 3.00, 300000.00),
(17, 6, 'dog', 3.00, 5.00, 350000.00),
(18, 6, 'dog', 5.00, 10.00, 400000.00),
(19, 6, 'dog', 10.00, 20.00, 600000.00),
(20, 6, 'dog', 20.00, 30.00, 700000.00),
(21, 6, 'dog', 30.00, 40.00, 800000.00),
(22, 6, 'cat', 0.00, 2.00, 300000.00),
(23, 6, 'cat', 2.00, 5.00, 350000.00),
(24, 6, 'cat', 5.00, 7.00, 400000.00),
(25, 6, 'cat', 7.00, 20.00, 450000.00),
(31, 7, 'dog', 0.00, 3.00, 300000.00),
(32, 7, 'dog', 3.00, 5.00, 350000.00),
(33, 7, 'dog', 5.00, 10.00, 400000.00),
(34, 7, 'dog', 10.00, 20.00, 700000.00),
(35, 7, 'dog', 20.00, 30.00, 800000.00),
(36, 7, 'dog', 30.00, 40.00, 950000.00);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `suppliers`
--

CREATE TABLE `suppliers` (
  `supplier_id` int(11) NOT NULL,
  `name` varchar(200) NOT NULL,
  `address` varchar(500) DEFAULT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `email` varchar(100) DEFAULT NULL,
  `bank_account` varchar(100) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `suppliers`
--

INSERT INTO `suppliers` (`supplier_id`, `name`, `address`, `phone`, `email`, `bank_account`) VALUES
(1, 'Công ty TNHH Royal Canin Việt Nam', 'Lầu 10, Tòa nhà Bitexco, TP.HCM', '02838212121', 'contact.vn@royalcanin.com', '123456789'),
(2, 'Nhà phân phối Pet-Link', 'Khu Công nghiệp Tân Bình, TP.HCM', '0909090909', 'sales@petlink.vn', '987654321');

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `time_slot_config`
--

CREATE TABLE `time_slot_config` (
  `slot_id` int(11) NOT NULL,
  `time_slot` varchar(20) NOT NULL COMMENT 'VD: 09:00, 10:00',
  `max_bookings` int(11) NOT NULL DEFAULT 3 COMMENT 'Số lượng booking tối đa cho slot này',
  `is_active` tinyint(1) DEFAULT 1
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `time_slot_config`
--

INSERT INTO `time_slot_config` (`slot_id`, `time_slot`, `max_bookings`, `is_active`) VALUES
(1, '09:00', 3, 1),
(2, '10:00', 3, 1),
(3, '11:00', 3, 1),
(4, '14:00', 3, 1),
(5, '15:00', 3, 1),
(6, '16:00', 3, 1),
(7, '17:00', 3, 1);

-- --------------------------------------------------------

--
-- Cấu trúc bảng cho bảng `users`
--

CREATE TABLE `users` (
  `user_id` int(11) NOT NULL,
  `full_name` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `phone` varchar(20) DEFAULT NULL,
  `date_of_birth` date DEFAULT NULL,
  `gender` varchar(10) DEFAULT 'other' COMMENT 'male, female, other',
  `password_hash` varchar(255) NOT NULL,
  `role` varchar(20) NOT NULL DEFAULT 'customer' COMMENT 'admin, staff, customer',
  `status` varchar(20) NOT NULL DEFAULT 'active' COMMENT 'active, inactive',
  `image_url` varchar(500) DEFAULT NULL,
  `created_at` datetime NOT NULL DEFAULT current_timestamp()
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

--
-- Đang đổ dữ liệu cho bảng `users`
--

INSERT INTO `users` (`user_id`, `full_name`, `email`, `phone`, `date_of_birth`, `gender`, `password_hash`, `role`, `status`, `image_url`, `created_at`) VALUES
(1, 'Admin PetSpa', 'admin@petspa.com', '0901234567', NULL, 'male', '$2a$11$NkVdDxQrtG7hZnH0fWu9LOay89n4kzIkUeMdTyhiN5aoAsBm1SHFa', 'admin', 'active', NULL, '2025-11-19 15:01:27'),
(2, 'Nguyễn Văn Nhân Viên', 'staff@petspa.com', '0987654321', NULL, 'other', '$2a$11$g1.1i21i/5A5rFTa5A.2k.dY2I.7lZ.8lZ.7lZ.8lZ.8lZ.8lZ.8', 'staff', 'active', NULL, '2025-11-19 15:01:27'),
(3, 'Trần Thị Khách Hàng', 'customer1@example.com', '0123456789', NULL, 'other', '$2a$11$h1.1i21i/5A5rFTa5A.2k.dY2I.7lZ.8lZ.7lZ.8lZ.8lZ.8lZ.8', 'customer', 'active', NULL, '2025-11-19 15:01:27'),
(4, 'hj', 'hj@example.com', '0917203030', NULL, 'other', '$2a$11$E7kwsacr4ieWDDPeW5omBeP.mbGI5FYx.Gh2meE8N63Hgc3OLRLr.', 'customer', 'active', NULL, '2025-11-19 15:03:52'),
(5, 'adminn', 'admin@example.com', '0917272727', NULL, 'other', '$2a$11$74.Akb9vZ0ikoJZzub65T.f1G9VRxWjn./E0lE6ocJMAjfDfztLYq', 'admin', 'active', NULL, '2025-11-19 15:04:37'),
(6, 'hj', 'hn@gmail.com', NULL, '2003-05-14', 'other', '$2a$11$Vv6h43Gpv2Pi.EfNEBVFJe2MtJEGjVf9nro8W7Q.5I3xUOsUxjjna', 'customer', 'active', NULL, '2025-11-19 15:20:15'),
(7, 'hjjjjjjjjjjjjjjjj', 'hh@gmail.com', '0917272723', '2004-02-07', 'male', '$2a$11$eQJfDy.R3rtX/eQg4ETQp.yLHp71KQg2D2thJxKIA4VUUrNHqDiXG', 'customer', 'active', NULL, '2025-11-19 15:37:33'),
(8, 'Rock Tuber', 'hhh@gmail.com', '0923456789', '2025-11-28', 'male', '$2a$11$55S97gaKpq6OToN/xGpVEuO9khROndHGKkofMfCG3LGeNuquVzdDa', 'customer', 'active', NULL, '2025-11-29 20:38:00'),
(9, 'hjhj', 'hj@gmail.com', '0968555444', '2016-05-10', 'male', '$2a$11$oD1xFkzqk660/SPiLXypx.jP6kRDyK1B6LN05JdA4aT0B5tKziHUi', 'staff', 'inactive', NULL, '2025-12-02 09:56:12'),
(10, 'Khách lẻ', 'user@example.com', '0911111111', '2025-12-02', 'male', '$2a$11$TmhIn/tsCeRCHE0WTxPoqO0Kx2owHgHdlf3O1vK7MHmAafGvBY7VS', 'customer', 'active', NULL, '2025-12-02 10:36:23');

--
-- Chỉ mục cho các bảng đã đổ
--

--
-- Chỉ mục cho bảng `appointments`
--
ALTER TABLE `appointments`
  ADD PRIMARY KEY (`appointment_id`),
  ADD KEY `idx_customer` (`customer_id`),
  ADD KEY `idx_service` (`service_id`),
  ADD KEY `idx_date` (`appointment_date`),
  ADD KEY `idx_status` (`status`);

--
-- Chỉ mục cho bảng `audit_logs`
--
ALTER TABLE `audit_logs`
  ADD PRIMARY KEY (`id`),
  ADD KEY `idx_user` (`user_id`),
  ADD KEY `idx_created` (`created_at`);

--
-- Chỉ mục cho bảng `cart_items`
--
ALTER TABLE `cart_items`
  ADD PRIMARY KEY (`cart_item_id`),
  ADD KEY `product_id` (`product_id`),
  ADD KEY `service_id` (`service_id`),
  ADD KEY `idx_user` (`user_id`);

--
-- Chỉ mục cho bảng `categories`
--
ALTER TABLE `categories`
  ADD PRIMARY KEY (`category_id`),
  ADD KEY `idx_type` (`type`);

--
-- Chỉ mục cho bảng `customers`
--
ALTER TABLE `customers`
  ADD PRIMARY KEY (`customer_id`),
  ADD UNIQUE KEY `user_id` (`user_id`),
  ADD KEY `idx_user` (`user_id`);

--
-- Chỉ mục cho bảng `customer_addresses`
--
ALTER TABLE `customer_addresses`
  ADD PRIMARY KEY (`address_id`),
  ADD KEY `idx_customer` (`customer_id`),
  ADD KEY `idx_default` (`is_default`),
  ADD KEY `idx_province` (`province_id`);

--
-- Chỉ mục cho bảng `orders`
--
ALTER TABLE `orders`
  ADD PRIMARY KEY (`order_id`),
  ADD KEY `idx_user` (`user_id`),
  ADD KEY `idx_created` (`created_at`),
  ADD KEY `idx_status` (`order_status`),
  ADD KEY `promotion_id` (`promotion_id`);

--
-- Chỉ mục cho bảng `order_items`
--
ALTER TABLE `order_items`
  ADD PRIMARY KEY (`order_item_id`),
  ADD KEY `idx_order` (`order_id`),
  ADD KEY `idx_product` (`product_id`),
  ADD KEY `idx_service` (`service_id`);

--
-- Chỉ mục cho bảng `pets`
--
ALTER TABLE `pets`
  ADD PRIMARY KEY (`pet_id`),
  ADD KEY `idx_customer` (`customer_id`);

--
-- Chỉ mục cho bảng `products`
--
ALTER TABLE `products`
  ADD PRIMARY KEY (`product_id`),
  ADD KEY `idx_category` (`category_id`),
  ADD KEY `idx_active` (`is_active`);

--
-- Chỉ mục cho bảng `promotions`
--
ALTER TABLE `promotions`
  ADD PRIMARY KEY (`promotion_id`),
  ADD UNIQUE KEY `code` (`code`),
  ADD KEY `idx_code` (`code`),
  ADD KEY `idx_dates` (`start_date`,`end_date`);

--
-- Chỉ mục cho bảng `purchase_orders`
--
ALTER TABLE `purchase_orders`
  ADD PRIMARY KEY (`purchase_order_id`),
  ADD KEY `idx_supplier` (`supplier_id`),
  ADD KEY `idx_staff` (`staff_id`),
  ADD KEY `idx_created` (`created_at`);

--
-- Chỉ mục cho bảng `purchase_order_items`
--
ALTER TABLE `purchase_order_items`
  ADD PRIMARY KEY (`id`),
  ADD KEY `idx_order` (`purchase_order_id`),
  ADD KEY `idx_product` (`product_id`);

--
-- Chỉ mục cho bảng `services`
--
ALTER TABLE `services`
  ADD PRIMARY KEY (`service_id`),
  ADD KEY `idx_category` (`category_id`),
  ADD KEY `idx_active` (`is_active`);

--
-- Chỉ mục cho bảng `service_prices`
--
ALTER TABLE `service_prices`
  ADD PRIMARY KEY (`price_id`),
  ADD KEY `idx_service` (`service_id`);

--
-- Chỉ mục cho bảng `suppliers`
--
ALTER TABLE `suppliers`
  ADD PRIMARY KEY (`supplier_id`);

--
-- Chỉ mục cho bảng `time_slot_config`
--
ALTER TABLE `time_slot_config`
  ADD PRIMARY KEY (`slot_id`),
  ADD UNIQUE KEY `time_slot` (`time_slot`),
  ADD KEY `idx_active` (`is_active`);

--
-- Chỉ mục cho bảng `users`
--
ALTER TABLE `users`
  ADD PRIMARY KEY (`user_id`),
  ADD UNIQUE KEY `email` (`email`),
  ADD KEY `idx_email` (`email`),
  ADD KEY `idx_role` (`role`);

--
-- AUTO_INCREMENT cho các bảng đã đổ
--

--
-- AUTO_INCREMENT cho bảng `appointments`
--
ALTER TABLE `appointments`
  MODIFY `appointment_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=32;

--
-- AUTO_INCREMENT cho bảng `audit_logs`
--
ALTER TABLE `audit_logs`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT;

--
-- AUTO_INCREMENT cho bảng `cart_items`
--
ALTER TABLE `cart_items`
  MODIFY `cart_item_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=76;

--
-- AUTO_INCREMENT cho bảng `categories`
--
ALTER TABLE `categories`
  MODIFY `category_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- AUTO_INCREMENT cho bảng `customers`
--
ALTER TABLE `customers`
  MODIFY `customer_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT cho bảng `customer_addresses`
--
ALTER TABLE `customer_addresses`
  MODIFY `address_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=7;

--
-- AUTO_INCREMENT cho bảng `orders`
--
ALTER TABLE `orders`
  MODIFY `order_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=63;

--
-- AUTO_INCREMENT cho bảng `order_items`
--
ALTER TABLE `order_items`
  MODIFY `order_item_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=103;

--
-- AUTO_INCREMENT cho bảng `pets`
--
ALTER TABLE `pets`
  MODIFY `pet_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=9;

--
-- AUTO_INCREMENT cho bảng `products`
--
ALTER TABLE `products`
  MODIFY `product_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=65;

--
-- AUTO_INCREMENT cho bảng `promotions`
--
ALTER TABLE `promotions`
  MODIFY `promotion_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT cho bảng `purchase_orders`
--
ALTER TABLE `purchase_orders`
  MODIFY `purchase_order_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=4;

--
-- AUTO_INCREMENT cho bảng `purchase_order_items`
--
ALTER TABLE `purchase_order_items`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT cho bảng `services`
--
ALTER TABLE `services`
  MODIFY `service_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- AUTO_INCREMENT cho bảng `service_prices`
--
ALTER TABLE `service_prices`
  MODIFY `price_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=37;

--
-- AUTO_INCREMENT cho bảng `suppliers`
--
ALTER TABLE `suppliers`
  MODIFY `supplier_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT cho bảng `time_slot_config`
--
ALTER TABLE `time_slot_config`
  MODIFY `slot_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=8;

--
-- AUTO_INCREMENT cho bảng `users`
--
ALTER TABLE `users`
  MODIFY `user_id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=11;

--
-- Các ràng buộc cho các bảng đã đổ
--

--
-- Các ràng buộc cho bảng `appointments`
--
ALTER TABLE `appointments`
  ADD CONSTRAINT `appointments_ibfk_1` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`customer_id`),
  ADD CONSTRAINT `appointments_ibfk_2` FOREIGN KEY (`service_id`) REFERENCES `services` (`service_id`);

--
-- Các ràng buộc cho bảng `audit_logs`
--
ALTER TABLE `audit_logs`
  ADD CONSTRAINT `audit_logs_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`);

--
-- Các ràng buộc cho bảng `cart_items`
--
ALTER TABLE `cart_items`
  ADD CONSTRAINT `cart_items_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `cart_items_ibfk_2` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`) ON DELETE SET NULL,
  ADD CONSTRAINT `cart_items_ibfk_3` FOREIGN KEY (`service_id`) REFERENCES `services` (`service_id`) ON DELETE SET NULL;

--
-- Các ràng buộc cho bảng `customers`
--
ALTER TABLE `customers`
  ADD CONSTRAINT `customers_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`) ON DELETE CASCADE;

--
-- Các ràng buộc cho bảng `customer_addresses`
--
ALTER TABLE `customer_addresses`
  ADD CONSTRAINT `customer_addresses_ibfk_1` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`customer_id`) ON DELETE CASCADE;

--
-- Các ràng buộc cho bảng `orders`
--
ALTER TABLE `orders`
  ADD CONSTRAINT `orders_ibfk_1` FOREIGN KEY (`user_id`) REFERENCES `users` (`user_id`),
  ADD CONSTRAINT `orders_ibfk_2` FOREIGN KEY (`promotion_id`) REFERENCES `promotions` (`promotion_id`) ON DELETE SET NULL;

--
-- Các ràng buộc cho bảng `order_items`
--
ALTER TABLE `order_items`
  ADD CONSTRAINT `order_items_ibfk_1` FOREIGN KEY (`order_id`) REFERENCES `orders` (`order_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `order_items_ibfk_2` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`) ON DELETE SET NULL,
  ADD CONSTRAINT `order_items_ibfk_3` FOREIGN KEY (`service_id`) REFERENCES `services` (`service_id`) ON DELETE SET NULL;

--
-- Các ràng buộc cho bảng `pets`
--
ALTER TABLE `pets`
  ADD CONSTRAINT `pets_ibfk_1` FOREIGN KEY (`customer_id`) REFERENCES `customers` (`customer_id`) ON DELETE CASCADE;

--
-- Các ràng buộc cho bảng `products`
--
ALTER TABLE `products`
  ADD CONSTRAINT `products_ibfk_1` FOREIGN KEY (`category_id`) REFERENCES `categories` (`category_id`);

--
-- Các ràng buộc cho bảng `purchase_orders`
--
ALTER TABLE `purchase_orders`
  ADD CONSTRAINT `purchase_orders_ibfk_1` FOREIGN KEY (`supplier_id`) REFERENCES `suppliers` (`supplier_id`),
  ADD CONSTRAINT `purchase_orders_ibfk_2` FOREIGN KEY (`staff_id`) REFERENCES `users` (`user_id`);

--
-- Các ràng buộc cho bảng `purchase_order_items`
--
ALTER TABLE `purchase_order_items`
  ADD CONSTRAINT `purchase_order_items_ibfk_1` FOREIGN KEY (`purchase_order_id`) REFERENCES `purchase_orders` (`purchase_order_id`) ON DELETE CASCADE,
  ADD CONSTRAINT `purchase_order_items_ibfk_2` FOREIGN KEY (`product_id`) REFERENCES `products` (`product_id`);

--
-- Các ràng buộc cho bảng `services`
--
ALTER TABLE `services`
  ADD CONSTRAINT `services_ibfk_1` FOREIGN KEY (`category_id`) REFERENCES `categories` (`category_id`);

--
-- Các ràng buộc cho bảng `service_prices`
--
ALTER TABLE `service_prices`
  ADD CONSTRAINT `fk_service_prices_service` FOREIGN KEY (`service_id`) REFERENCES `services` (`service_id`) ON DELETE CASCADE;
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
