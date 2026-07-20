import { createContext, useContext, useState, useEffect } from 'react';
import { cartAPI } from '../services/api';
import { useAuth } from './AuthContext';
import { MESSAGES, SHIPPING_FEE } from '../utils/constants';

const CartContext = createContext();

export const useCart = () => {
  const context = useContext(CartContext);
  if (!context) {
    throw new Error('useCart must be used within CartProvider');
  }
  return context;
};

export const CartProvider = ({ children }) => {
  const { isAuthenticated } = useAuth();
  const [cart, setCart] = useState(null);
  const [loading, setLoading] = useState(false);
  const [cartCount, setCartCount] = useState(0);

  useEffect(() => {
    if (isAuthenticated) {
      fetchCart();
    } else {
      setCart(null);
      setCartCount(0);
    }
  }, [isAuthenticated]);

  const fetchCart = async () => {
    if (!isAuthenticated) return;
    
    try {
      setLoading(true);
      const response = await cartAPI.get();
      
      if (response.data.success) {
        const cartData = response.data.data;
        setCart(cartData);
        setCartCount(cartData.totalItems || 0);
      }
    } catch (error) {
      console.error('Fetch cart error:', error);
      setCart({ items: [], totalItems: 0, totalAmount: 0 });
      setCartCount(0);
    } finally {
      setLoading(false);
    }
  };

  const addToCart = async (productId = null, serviceId = null, quantity = 1) => {
    if (!isAuthenticated) {
      return { 
        success: false, 
        message: 'Vui lòng đăng nhập để thêm vào giỏ hàng!' 
      };
    }

    try {
      const response = await cartAPI.add({ productId, serviceId, quantity });
      
      if (response.data.success) {
        await fetchCart();
        return { success: true, message: MESSAGES.SUCCESS.ADD_TO_CART };
      } else {
        return { success: false, message: response.data.message || MESSAGES.ERROR.GENERIC };
      }
    } catch (error) {
      console.error('Add to cart error:', error);
      return { 
        success: false, 
        message: error.response?.data?.message || MESSAGES.ERROR.GENERIC 
      };
    }
  };

  const updateCartItem = async (cartItemId, quantity) => {
    if (!isAuthenticated) return { success: false, message: 'Chưa đăng nhập!' };

    if (quantity < 1) {
      return removeFromCart(cartItemId);
    }

    try {
      const response = await cartAPI.update(cartItemId, { quantity });
      
      if (response.data.success) {
        await fetchCart();
        return { success: true, message: MESSAGES.SUCCESS.UPDATE_CART };
      } else {
        return { success: false, message: response.data.message || MESSAGES.ERROR.GENERIC };
      }
    } catch (error) {
      console.error('Update cart error:', error);
      return { 
        success: false, 
        message: error.response?.data?.message || MESSAGES.ERROR.GENERIC 
      };
    }
  };

  const removeFromCart = async (cartItemId) => {
    if (!isAuthenticated) return { success: false, message: 'Chưa đăng nhập!' };

    try {
      const response = await cartAPI.remove(cartItemId);
      
      if (response.data.success) {
        await fetchCart();
        return { success: true, message: MESSAGES.SUCCESS.REMOVE_FROM_CART };
      } else {
        return { success: false, message: response.data.message || MESSAGES.ERROR.GENERIC };
      }
    } catch (error) {
      console.error('Remove from cart error:', error);
      return { 
        success: false, 
        message: error.response?.data?.message || MESSAGES.ERROR.GENERIC 
      };
    }
  };

  const clearCart = async () => {
    if (!isAuthenticated) return { success: false, message: 'Chưa đăng nhập!' };

    try {
      const response = await cartAPI.clear();
      
      if (response.data.success) {
        await fetchCart();
        return { success: true, message: 'Đã xóa toàn bộ giỏ hàng!' };
      } else {
        return { success: false, message: response.data.message || MESSAGES.ERROR.GENERIC };
      }
    } catch (error) {
      console.error('Clear cart error:', error);
      return { 
        success: false, 
        message: error.response?.data?.message || MESSAGES.ERROR.GENERIC 
      };
    }
  };

  const getCartTotals = (discountAmount = 0) => {
    if (!cart || !cart.items || cart.items.length === 0) {
      return {
        subtotal: 0,
        discount: 0,
        shippingFee: 0,
        total: 0
      };
    }

    const subtotal = cart.totalAmount || 0;
    const discount = discountAmount || 0;
    const shippingFee = SHIPPING_FEE;
    const total = subtotal - discount + shippingFee;

    return {
      subtotal,
      discount,
      shippingFee,
      total: Math.max(0, total)
    };
  };

  const value = {
    cart,
    loading,
    cartCount,
    fetchCart,
    addToCart,
    updateCartItem,
    removeFromCart,
    clearCart,
    getCartTotals
  };

  return <CartContext.Provider value={value}>{children}</CartContext.Provider>;
};