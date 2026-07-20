import { Link } from 'react-router-dom';
import { formatCurrency } from '../../utils/helpers';

const CartSummary = ({ subtotal, discount = 0, shippingFee = 0, total, showCheckoutButton = true }) => {
  return (
    <div className="bg-white rounded-lg shadow-lg p-6 sticky top-24">
      <h2 className="text-xl font-bold text-gray-900 mb-6">Tóm tắt đơn hàng</h2>

      <div className="space-y-3 mb-6">
        {/* Subtotal */}
        <div className="flex justify-between text-gray-600">
          <span>Tạm tính</span>
          <span>{formatCurrency(subtotal)}</span>
        </div>

        {/* Discount */}
        {discount > 0 && (
          <div className="flex justify-between text-green-600">
            <span>Giảm giá</span>
            <span>-{formatCurrency(discount)}</span>
          </div>
        )}

        {/* Shipping Fee */}
        <div className="flex justify-between text-gray-600">
          <span>Phí vận chuyển</span>
          <span>{formatCurrency(shippingFee)}</span>
        </div>

        <hr />

        {/* Total */}
        <div className="flex justify-between text-lg font-bold text-gray-900">
          <span>Tổng cộng</span>
          <span className="text-primary-500">{formatCurrency(total)}</span>
        </div>
      </div>

      {/* Checkout Button */}
      {showCheckoutButton && (
        <Link to="/checkout" className="block w-full btn-primary text-center">
          Thanh toán
        </Link>
      )}

      {/* Continue Shopping */}
      <Link
        to="/products"
        className="block w-full text-center py-2 text-sm text-gray-600 hover:text-primary-500 mt-3"
      >
        Tiếp tục mua sắm
      </Link>

      {/* Benefits */}
      <div className="mt-6 pt-6 border-t space-y-3">
        <div className="flex items-start gap-2 text-sm text-gray-600">
          <svg className="w-5 h-5 text-green-500 flex-shrink-0 mt-0.5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
          </svg>
          <span>Miễn phí đổi trả trong 7 ngày</span>
        </div>
        <div className="flex items-start gap-2 text-sm text-gray-600">
          <svg className="w-5 h-5 text-green-500 flex-shrink-0 mt-0.5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
          </svg>
          <span>Cam kết 100% hàng chính hãng</span>
        </div>
        <div className="flex items-start gap-2 text-sm text-gray-600">
          <svg className="w-5 h-5 text-green-500 flex-shrink-0 mt-0.5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
          </svg>
          <span>Giao hàng nhanh chóng</span>
        </div>
      </div>
    </div>
  );
};

export default CartSummary;