import { STORAGE_KEYS } from '../utils/constants';

export const getToken = () => {
  return localStorage.getItem(STORAGE_KEYS.token);
};

export const setToken = (token) => {
  localStorage.setItem(STORAGE_KEYS.token, token);
};

export const removeToken = () => {
  localStorage.removeItem(STORAGE_KEYS.token);
};

export const getUser = () => {
  const userStr = localStorage.getItem(STORAGE_KEYS.user);
  return userStr ? JSON.parse(userStr) : null;
};

export const setUser = (user) => {
  localStorage.setItem(STORAGE_KEYS.user, JSON.stringify(user));
};

export const removeUser = () => {
  localStorage.removeItem(STORAGE_KEYS.user);
};

export const isLoggedIn = () => {
  return !!getToken();
};

export const logout = () => {
  removeToken();
  removeUser();
  window.location.href = '/login';
};

export const getUserRole = () => {
  const user = getUser();
  return user?.role || null;
};

export const isCustomer = () => {
  const role = getUserRole();
  return role === 'Customer';
};