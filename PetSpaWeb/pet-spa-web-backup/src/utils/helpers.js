import { REGEX } from './constants';

// Format currency VND
export const formatCurrency = (amount) => {
  if (!amount && amount !== 0) return '0đ';
  return new Intl.NumberFormat('vi-VN', {
    style: 'currency',
    currency: 'VND'
  }).format(amount);
};

// Format date
export const formatDate = (dateString) => {
  if (!dateString) return '';
  const date = new Date(dateString);
  return new Intl.DateTimeFormat('vi-VN', {
    year: 'numeric',
    month: '2-digit',
    day: '2-digit'
  }).format(date);
};

// Format datetime
export const formatDateTime = (dateString) => {
  if (!dateString) return '';
  const date = new Date(dateString);
  return new Intl.DateTimeFormat('vi-VN', {
    year: 'numeric',
    month: '2-digit',
    day: '2-digit',
    hour: '2-digit',
    minute: '2-digit'
  }).format(date);
};

// Validate email
export const validateEmail = (email) => {
  return REGEX.email.test(email);
};

// Validate phone
export const validatePhone = (phone) => {
  return REGEX.phone.test(phone);
};

// Validate password
export const validatePassword = (password) => {
  return REGEX.password.test(password);
};

// Get image URL with fallback
export const getImageUrl = (url, fallback = null) => {
  if (!url) return fallback || 'https://via.placeholder.com/400x300?text=No+Image';

  // If URL is already absolute, return it
  if (url.startsWith('http://') || url.startsWith('https://')) {
    return url;
  }

  // If URL starts with /, it's relative to API base
  const API_BASE = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5014';
  return url.startsWith('/') ? `${API_BASE}${url}` : `${API_BASE}/${url}`;
};

// Get avatar URL
export const getAvatarUrl = (filename) => {
  if (!filename) return 'https://via.placeholder.com/150?text=Avatar';
  const API_BASE = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5014';
  return `${API_BASE}/images/avatars/${filename}`;
};

// Get pet image URL
export const getPetImageUrl = (filename) => {
  if (!filename) return 'https://via.placeholder.com/300?text=Pet';
  const API_BASE = import.meta.env.VITE_API_BASE_URL || 'http://localhost:5014';
  return `${API_BASE}/images/pets/${filename}`;
};

// Truncate text
export const truncateText = (text, maxLength = 100) => {
  if (!text) return '';
  if (text.length <= maxLength) return text;
  return text.substring(0, maxLength) + '...';
};

// Debounce function
export const debounce = (func, delay = 300) => {
  let timeoutId;
  return (...args) => {
    clearTimeout(timeoutId);
    timeoutId = setTimeout(() => func(...args), delay);
  };
};

// Get stock status
export const getStockStatus = (stockQuantity) => {
  if (!stockQuantity || stockQuantity === 0) {
    return { label: 'Hết hàng', className: 'text-red-600 bg-red-50', available: false };
  }
  if (stockQuantity < 10) {
    return { label: 'Sắp hết', className: 'text-orange-600 bg-orange-50', available: true };
  }
  return { label: 'Còn hàng', className: 'text-green-600 bg-green-50', available: true };
};

// Get order status badge
export const getOrderStatusBadge = (status) => {
  const statusMap = {
    'Pending': { label: 'Chờ xác nhận', className: 'bg-yellow-100 text-yellow-800' },
    'Confirmed': { label: 'Đã xác nhận', className: 'bg-blue-100 text-blue-800' },
    'Shipping': { label: 'Đang giao', className: 'bg-purple-100 text-purple-800' },
    'Completed': { label: 'Đã hoàn thành', className: 'bg-green-100 text-green-800' },
    'Cancelled': { label: 'Đã hủy', className: 'bg-red-100 text-red-800' }
  };
  return statusMap[status] || { label: status, className: 'bg-gray-100 text-gray-800' };
};

// Get payment status badge
export const getPaymentStatusBadge = (status) => {
  const statusMap = {
    'Pending': { label: 'Chưa thanh toán', className: 'bg-yellow-100 text-yellow-800' },
    'Paid': { label: 'Đã thanh toán', className: 'bg-green-100 text-green-800' },
    'Failed': { label: 'Thất bại', className: 'bg-red-100 text-red-800' }
  };
  return statusMap[status] || { label: status, className: 'bg-gray-100 text-gray-800' };
};

// Get appointment status badge
export const getAppointmentStatusBadge = (status) => {
  const statusMap = {
    'Pending': { label: 'Chờ xác nhận', className: 'bg-yellow-100 text-yellow-800' },
    'Confirmed': { label: 'Đã xác nhận', className: 'bg-blue-100 text-blue-800' },
    'Completed': { label: 'Hoàn thành', className: 'bg-green-100 text-green-800' },
    'Cancelled': { label: 'Đã hủy', className: 'bg-red-100 text-red-800' }
  };
  return statusMap[status] || { label: status, className: 'bg-gray-100 text-gray-800' };
};

// Calculate discount amount
export const calculateDiscount = (subtotal, discountPercent) => {
  if (!discountPercent) return 0;
  return (subtotal * discountPercent) / 100;
};

// Scroll to top
export const scrollToTop = (smooth = true) => {
  window.scrollTo({
    top: 0,
    behavior: smooth ? 'smooth' : 'auto'
  });
};

// Get query params from URL
export const getQueryParams = (search) => {
  return new URLSearchParams(search);
};

// Build query string from object
export const buildQueryString = (params) => {
  const query = new URLSearchParams();
  Object.keys(params).forEach(key => {
    if (params[key] !== null && params[key] !== undefined && params[key] !== '') {
      query.append(key, params[key]);
    }
  });
  return query.toString();
};

// Handle API error
export const handleApiError = (error) => {
  if (error.response) {
    return error.response.data?.message || 'Có lỗi xảy ra từ server';
  } else if (error.request) {
    return 'Không thể kết nối đến server';
  } else {
    return error.message || 'Có lỗi xảy ra';
  }
};