import { createContext, useContext, useState, useEffect } from 'react';
import { authAPI, customerAPI } from '../services/api';
import { setToken, removeToken, setUser, removeUser, getToken, getUser } from '../services/auth';
import { MESSAGES } from '../utils/constants';

const AuthContext = createContext();

export const useAuth = () => {
  const context = useContext(AuthContext);
  if (!context) {
    throw new Error('useAuth must be used within AuthProvider');
  }
  return context;
};

export const AuthProvider = ({ children }) => {
  const [user, setUserState] = useState(null);
  const [loading, setLoading] = useState(true);
  const [isAuthenticated, setIsAuthenticated] = useState(false);

  // Initialize - Check if user is logged in
  useEffect(() => {
    const initAuth = async () => {
      const token = getToken();
      const savedUser = getUser();
      
      if (token && savedUser) {
        setUserState(savedUser);
        setIsAuthenticated(true);
        
        // Optionally verify token with server
        try {
          const response = await authAPI.getProfile();
          if (response.data.success) {
            const userData = response.data.data;
            setUserState(userData);
            setUser(userData);
          }
        } catch (error) {
          // Token invalid, clear auth
          handleLogout();
        }
      }
      setLoading(false);
    };

    initAuth();
  }, []);

  // Login
  const login = async (email, password) => {
    try {
      const response = await authAPI.login({ email, password });
      
      if (response.data.success) {
        const { token, ...userData } = response.data.data;
        
        // Save to localStorage
        setToken(token);
        setUser(userData);
        
        // Update state
        setUserState(userData);
        setIsAuthenticated(true);
        
        return { success: true, message: MESSAGES.SUCCESS.LOGIN };
      } else {
        return { success: false, message: response.data.message || MESSAGES.ERROR.LOGIN_FAILED };
      }
    } catch (error) {
      console.error('Login error:', error);
      return { 
        success: false, 
        message: error.response?.data?.message || MESSAGES.ERROR.LOGIN_FAILED 
      };
    }
  };

  // Register
  const register = async (data) => {
    try {
      const response = await authAPI.register(data);
      
      if (response.data.success) {
        const { token, ...userData } = response.data.data;
        
        // Save to localStorage
        setToken(token);
        setUser(userData);
        
        // Update state
        setUserState(userData);
        setIsAuthenticated(true);
        
        return { success: true, message: MESSAGES.SUCCESS.REGISTER };
      } else {
        return { success: false, message: response.data.message || MESSAGES.ERROR.REGISTER_FAILED };
      }
    } catch (error) {
      console.error('Register error:', error);
      return { 
        success: false, 
        message: error.response?.data?.message || MESSAGES.ERROR.REGISTER_FAILED 
      };
    }
  };

  // Logout
  const handleLogout = () => {
    removeToken();
    removeUser();
    setUserState(null);
    setIsAuthenticated(false);
  };

  // Update user profile
  const updateUserProfile = async (data) => {
    try {
      const response = await customerAPI.updateProfile(data);
      
      if (response.data.success) {
        const updatedUser = { ...user, ...response.data.data };
        setUserState(updatedUser);
        setUser(updatedUser);
        
        return { success: true, message: MESSAGES.SUCCESS.UPDATE_PROFILE };
      } else {
        return { success: false, message: response.data.message || MESSAGES.ERROR.GENERIC };
      }
    } catch (error) {
      console.error('Update profile error:', error);
      return { 
        success: false, 
        message: error.response?.data?.message || MESSAGES.ERROR.GENERIC 
      };
    }
  };

  // Refresh user data
  const refreshUser = async () => {
    try {
      const response = await customerAPI.getMyProfile();
      if (response.data.success) {
        const userData = response.data.data;
        setUserState(userData);
        setUser(userData);
      }
    } catch (error) {
      console.error('Refresh user error:', error);
    }
  };

  const value = {
    user,
    loading,
    isAuthenticated,
    login,
    register,
    logout: handleLogout,
    updateUserProfile,
    refreshUser
  };

  return <AuthContext.Provider value={value}>{children}</AuthContext.Provider>;
};