import axios from 'axios';
import { STORAGE_KEYS } from '../utils/constants';

const api = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL || 'http://localhost:5014',
  headers: {
    'Content-Type': 'application/json',
  },
  timeout: 30000
});

api.interceptors.request.use(
  (config) => {
    const token = localStorage.getItem(STORAGE_KEYS.token);
    if (token) {
      config.headers.Authorization = `Bearer ${token}`;
    }
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

api.interceptors.response.use(
  (response) => {
    return response;
  },
  (error) => {
    if (error.response?.status === 401) {
      localStorage.removeItem(STORAGE_KEYS.token);
      localStorage.removeItem(STORAGE_KEYS.user);
      window.location.href = '/login';
    }
    return Promise.reject(error);
  }
);

// ========== AUTH APIs ==========
export const authAPI = {
  login: (data) => api.post('/api/Auth/login', data),
  register: (data) => api.post('/api/Auth/register', data),
  getProfile: () => api.get('/api/Auth/profile')
};

// ========== ADDRESS APIs ==========
export const addressAPI = {
  getProvinces: () => api.get('/api/AddressApi/provinces'),
  getWards: (provinceId) => api.get(`/api/AddressApi/wards/${provinceId}`),
  getFullAddress: (provinceId, wardId) => 
    api.get(`/api/AddressApi/full/${provinceId}/${wardId}`)
};

// ========== CUSTOMER APIs ==========
export const customerAPI = {
  getMyProfile: () => api.get('/api/Customers/my-profile'),
  updateProfile: (data) => api.post('/api/Customers/update-profile', data)
};

// ========== CUSTOMER ADDRESS APIs ==========
export const customerAddressAPI = {
  getAll: () => api.get('/api/CustomerAddresses'),
  getById: (id) => api.get(`/api/CustomerAddresses/${id}`),
  create: (data) => api.post('/api/CustomerAddresses', data),
  update: (id, data) => api.post(`/api/CustomerAddresses/${id}/update`, data),
  delete: (id) => api.delete(`/api/CustomerAddresses/${id}`),
  setDefault: (id) => api.post(`/api/CustomerAddresses/${id}/set-default`)
};

// ========== PET APIs ==========
export const petAPI = {
  getAll: (customerId) => api.get('/api/Pets', { params: { customerId } }),
  getById: (id) => api.get(`/api/Pets/${id}`),
  create: (data) => api.post('/api/Pets', data),
  update: (id, data) => api.post(`/api/Pets/${id}/update`, data),
  delete: (id) => api.delete(`/api/Pets/${id}`)
};

// ========== CATEGORY APIs ==========
export const categoryAPI = {
  getAll: (params) => api.get('/api/Categories', { params }),
  getById: (id) => api.get(`/api/Categories/${id}`)
};

// ========== PRODUCT APIs ==========
export const productAPI = {
  getAll: (params) => api.get('/api/Products', { params }),
  getById: (id) => api.get(`/api/Products/${id}`),
  getBrands: () => api.get('/api/Products/brands'),
  getLowStock: (threshold = 10) => 
    api.get('/api/Products/low-stock', { params: { threshold } })
};

// ========== SERVICE APIs ==========
export const serviceAPI = {
  getAll: (params) => api.get('/api/Services', { params }),
  getById: (id) => api.get(`/api/Services/${id}`)
};

// ========== CART APIs ==========
export const cartAPI = {
  get: () => api.get('/api/Cart'),
  add: (data) => api.post('/api/Cart/add', data),
  update: (id, data) => api.post(`/api/Cart/${id}/update`, data),
  remove: (id) => api.delete(`/api/Cart/${id}`),
  clear: () => api.delete('/api/Cart/clear')
};

// ========== PROMOTION APIs ==========
export const promotionAPI = {
  getAll: (params) => api.get('/api/Promotions', { params }),
  getById: (id) => api.get(`/api/Promotions/${id}`),
  validate: (code) => api.post('/api/Promotions/validate', { code })
};

// ========== ORDER APIs ==========
export const orderAPI = {
  getAll: (params) => api.get('/api/Orders', { params }),
  getById: (id) => api.get(`/api/Orders/${id}`),
  create: (data) => api.post('/api/Orders', data)
};

// ========== APPOINTMENT APIs ==========
export const appointmentAPI = {
  getAll: (params) => api.get('/api/Appointments', { params }),
  getById: (id) => api.get(`/api/Appointments/${id}`),
  getAvailableSlots: (date) => 
    api.get('/api/Appointments/available-slots', { params: { date } }),
  create: (data) => api.post('/api/Appointments', data),
  delete: (id) => api.delete(`/api/Appointments/${id}`)
};

export default api;