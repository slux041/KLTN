import { useEffect, useState } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { useCart } from '../contexts/CartContext';
import { useAuth } from '../contexts/AuthContext';
import CartItem from '../components/cart/CartItem';
import CartSummary from '../components/cart/CartSummary';
import LoadingSpinner from '../components/common/LoadingSpinner';

const CartPage = () => {
  const navigate = useNavigate();
  const { isAuthenticated } = useAuth();
  const { cart, loading, updateCartItem, removeFromCart, clearCart, getCartTotals } = useCart();
  const [clearingCart, setClearingCart] = useState(false);

  useEffect(() => {
    if (!isAuthenticated) {
      navigate('/login', { state: { from: { pathname: '/cart' } } });
    }
  }, [isAuthenticated, navigate]);

  const handleUpdateQuantity = async (cartItemId, quantity) => {
    await updateCartItem(cartItemId, quantity);
  };

  const handleRemove = async (cartItemId) => {
    await removeFromCart(cartItemId);
  };

  const handleClearCart = async () => {
    if (window.confirm('Bạn có chắc muốn xóa toàn bộ giỏ hàng?')) {
      setClearingCart(true);
      await clearCart();
      setClearingCart(false);
    }
  };

  if (loading) {
    return <LoadingSpinner fullScreen />;
  }

  // Empty cart
  if (!cart || !cart.items || cart.items.length === 0) {
    return (
      <div className="bg-gray-50 py-12">
        <div className="container-custom">
          <div className="bg-white rounded-lg shadow-lg p-12 text-center">
            <svg className="w-24 h-24 text-gray-400 mx-auto mb-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M3 3h2l.4 2M7 13h10l4-8H5.4M7 13L5.4 5M7 13l-2.293 2.293c-.63.63-.184 1.707.707 1.707H17m0 0a2 2 0 100 4 2 2 0 000-4zm-8 2a2 2 0 11-4 0 2 2 0 014 0z" />
            </svg>
            <h2 className="text-2xl font-bold text-gray-900 mb-4">Giỏ hàng trống</h2>
            <p className="text-gray-600 mb-8">Bạn chưa có sản phẩm nào trong giỏ hàng</p>
            <Link to="/products" className="btn-primary inline-block">
              Tiếp tục mua sắm
            </Link>
          </div>
        </div>
      </div>
    );
  }

  const totals = getCartTotals();

  return (
    <div className="bg-gray-50 py-8">
      <div className="container-custom">
        {/* Breadcrumb */}
        <nav className="flex mb-6" aria-label="Breadcrumb">
          <ol className="inline-flex items-center space-x-1 md:space-x-3">
            <li className="inline-flex items-center">
              <Link to="/" className="text-gray-700 hover:text-primary-500 inline-flex items-center">
                <svg className="w-4 h-4 mr-2" fill="currentColor" viewBox="0 0 20 20">
                  <path d="M10.707 2.293a1 1 0 00-1.414 0l-7 7a1 1 0 001.414 1.414L4 10.414V17a1 1 0 001 1h2a1 1 0 001-1v-2a1 1 0 011-1h2a1 1 0 011 1v2a1 1 0 001 1h2a1 1 0 001-1v-6.586l.293.293a1 1 0 001.414-1.414l-7-7z" />
                </svg>
                Trang chủ
              </Link>
            </li>
            <li>
              <div className="flex items-center">
                <svg className="w-6 h-6 text-gray-400" fill="currentColor" viewBox="0 0 20 20">
                  <path fillRule="evenodd" d="M7.293 14.707a1 1 0 010-1.414L10.586 10 7.293 6.707a1 1 0 011.414-1.414l4 4a1 1 0 010 1.414l-4 4a1 1 0 01-1.414 0z" clipRule="evenodd" />
                </svg>
                <span className="ml-1 text-gray-500 md:ml-2">Giỏ hàng</span>
              </div>
            </li>
          </ol>
        </nav>

        {/* Header */}
        <div className="flex items-center justify-between mb-6">
          <h1 className="text-3xl font-bold text-gray-900">
            Giỏ hàng ({cart.totalItems} sản phẩm)
          </h1>
          <button
            onClick={handleClearCart}
            disabled={clearingCart}
            className="text-red-500 hover:text-red-700 font-medium text-sm disabled:opacity-50"
          >
            {clearingCart ? 'Đang xóa...' : 'Xóa tất cả'}
          </button>
        </div>

        {/* Main Content */}
        <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
          {/* Cart Items */}
          <div className="lg:col-span-2 space-y-4">
            {cart.items.map((item) => (
              <CartItem
                key={item.cartItemId}
                item={item}
                onUpdateQuantity={handleUpdateQuantity}
                onRemove={handleRemove}
              />
            ))}
          </div>

          {/* Cart Summary */}
          <div className="lg:col-span-1">
            <CartSummary
              subtotal={totals.subtotal}
              discount={totals.discount}
              shippingFee={totals.shippingFee}
              total={totals.total}
              showCheckoutButton={true}
            />
          </div>
        </div>

        {/* Shopping Tips */}
        <div className="mt-12 bg-blue-50 border border-blue-200 rounded-lg p-6">
          <div className="flex items-start gap-3">
            <svg className="w-6 h-6 text-blue-500 flex-shrink-0 mt-0.5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
              <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M13 16h-1v-4h-1m1-4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
            </svg>
            <div>
              <h3 className="font-semibold text-gray-900 mb-2">Lưu ý khi mua hàng</h3>
              <ul className="text-sm text-gray-600 space-y-1">
                <li>• Vui lòng kiểm tra kỹ thông tin sản phẩm trước khi thanh toán</li>
                <li>• Phí vận chuyển được tính dựa trên địa chỉ giao hàng</li>
                <li>• Đơn hàng sẽ được xử lý trong vòng 24h</li>
                <li>• Liên hệ hotline 1900-xxxx nếu cần hỗ trợ</li>
              </ul>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default CartPage;