export const API_BASE_URL = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5014';

export const IMAGE_PATHS = {
  logo: `${API_BASE_URL}/images/service/logo.png`,
  servicePrice: `${API_BASE_URL}/images/service/service.jpg`,
  avatars: `${API_BASE_URL}/images/avatars`,
  pets: `${API_BASE_URL}/images/pets`,
  products: `${API_BASE_URL}/images/products`,
  placeholder: 'https://via.placeholder.com/400x300?text=No+Image'
};

export const STORAGE_KEYS = {
  token: 'pet_spa_token',
  user: 'pet_spa_user',
  cart: 'pet_spa_cart'
};

export const ORDER_STATUS = {
  PENDING: 'Pending',
  CONFIRMED: 'Confirmed',
  SHIPPING: 'Shipping',
  COMPLETED: 'Completed',
  CANCELLED: 'Cancelled'
};

export const PAYMENT_METHODS = {
  COD: 'cod',
  MOMO: 'momo'
};

export const PAYMENT_STATUS = {
  PENDING: 'Pending',
  PAID: 'Paid',
  FAILED: 'Failed'
};

export const APPOINTMENT_STATUS = {
  PENDING: 'Pending',
  CONFIRMED: 'Confirmed',
  COMPLETED: 'Completed',
  CANCELLED: 'Cancelled'
};

export const SERVICE_IDS = {
  BATH: 5,  
  BATH_SHAVE: 6,
  BATH_TRIM: 7 
};

export const PET_TYPES = {
  DOG: 'Chó',
  CAT: 'Mèo'
};

export const SHIPPING_FEE = 25000;

export const HOME_CATEGORIES = {
  BEST_SELLING: 'best-selling',
  DOG_PRODUCTS: 'dog-products',
  CAT_PRODUCTS: 'cat-products',
  NEW_ARRIVALS: 'new-arrivals'
};

export const PAGINATION = {
  DEFAULT_PAGE: 1,
  DEFAULT_PAGE_SIZE: 20,
  PRODUCTS_PER_PAGE: 12
};

export const PRICE_RANGES = [
  { label: 'Tất cả', min: 0, max: null },
  { label: 'Dưới 100,000đ', min: 0, max: 100000 },
  { label: '100,000đ - 300,000đ', min: 100000, max: 300000 },
  { label: '300,000đ - 500,000đ', min: 300000, max: 500000 },
  { label: '500,000đ - 1,000,000đ', min: 500000, max: 1000000 },
  { label: 'Trên 1,000,000đ', min: 1000000, max: null }
];

export const TIME_SLOTS = [
  '08:00 - 09:00',
  '09:00 - 10:00',
  '10:00 - 11:00',
  '11:00 - 12:00',
  '13:00 - 14:00',
  '14:00 - 15:00',
  '15:00 - 16:00',
  '16:00 - 17:00',
  '17:00 - 18:00'
];

export const REGEX = {
  email: /^[^\s@]+@[^\s@]+\.[^\s@]+$/,
  phone: /(84|0[3|5|7|8|9])+([0-9]{8})\b/,
  password: /^.{6,}$/
};

export const MESSAGES = {
  SUCCESS: {
    LOGIN: 'Đăng nhập thành công!',
    REGISTER: 'Đăng ký thành công!',
    ADD_TO_CART: 'Đã thêm vào giỏ hàng!',
    UPDATE_CART: 'Đã cập nhật giỏ hàng!',
    REMOVE_FROM_CART: 'Đã xóa khỏi giỏ hàng!',
    ORDER_SUCCESS: 'Đặt hàng thành công!',
    APPOINTMENT_SUCCESS: 'Đặt lịch thành công!',
    UPDATE_PROFILE: 'Cập nhật thông tin thành công!',
    ADD_PET: 'Thêm thú cưng thành công!',
    UPDATE_PET: 'Cập nhật thú cưng thành công!',
    DELETE_PET: 'Xóa thú cưng thành công!',
    ADD_ADDRESS: 'Thêm địa chỉ thành công!',
    UPDATE_ADDRESS: 'Cập nhật địa chỉ thành công!',
    DELETE_ADDRESS: 'Xóa địa chỉ thành công!',
    SET_DEFAULT_ADDRESS: 'Đã đặt làm địa chỉ mặc định!'
  },
  ERROR: {
    GENERIC: 'Có lỗi xảy ra. Vui lòng thử lại!',
    NETWORK: 'Lỗi kết nối. Vui lòng kiểm tra mạng!',
    LOGIN_FAILED: 'Đăng nhập thất bại. Vui lòng kiểm tra lại thông tin!',
    REGISTER_FAILED: 'Đăng ký thất bại. Email có thể đã tồn tại!',
    INVALID_EMAIL: 'Email không hợp lệ!',
    INVALID_PHONE: 'Số điện thoại không hợp lệ!',
    INVALID_PASSWORD: 'Mật khẩu phải có ít nhất 6 ký tự!',
    REQUIRED_FIELD: 'Vui lòng điền đầy đủ thông tin!',
    OUT_OF_STOCK: 'Sản phẩm đã hết hàng!',
    INVALID_QUANTITY: 'Số lượng không hợp lệ!',
    CART_EMPTY: 'Giỏ hàng trống!',
    ADDRESS_REQUIRED: 'Vui lòng chọn địa chỉ giao hàng!'
  }
};