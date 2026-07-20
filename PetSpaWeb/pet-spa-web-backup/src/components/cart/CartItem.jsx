import { useState } from 'react';
import { Link } from 'react-router-dom';
import { formatCurrency, getImageUrl } from '../../utils/helpers';

const CartItem = ({ item, onUpdateQuantity, onRemove }) => {
  const [updating, setUpdating] = useState(false);
  const [removing, setRemoving] = useState(false);

  const imageUrl = getImageUrl(item.imageUrl);
  const isProduct = item.productId !== null;
  const itemLink = isProduct ? `/products/${item.productId}` : '#';

  const handleQuantityChange = async (newQuantity) => {
    if (newQuantity < 1) return;
    setUpdating(true);
    await onUpdateQuantity(item.cartItemId, newQuantity);
    setUpdating(false);
  };

  const handleRemove = async () => {
    if (window.confirm('Bạn có chắc muốn xóa sản phẩm này khỏi giỏ hàng?')) {
      setRemoving(true);
      await onRemove(item.cartItemId);
      setRemoving(false);
    }
  };

  return (
    <div className={`bg-white rounded-lg shadow p-4 ${removing ? 'opacity-50' : ''}`}>
      <div className="flex gap-4">
        {/* Image */}
        <Link to={itemLink} className="flex-shrink-0">
          <img
            src={imageUrl}
            alt={item.productName || item.serviceName}
            className="w-24 h-24 object-cover rounded-lg"
            onError={(e) => {
              e.target.src = 'https://via.placeholder.com/100?text=No+Image';
            }}
          />
        </Link>

        {/* Info */}
        <div className="flex-1 min-w-0">
          <Link to={itemLink} className="block">
            <h3 className="font-medium text-gray-900 hover:text-primary-500 truncate">
              {item.productName || item.serviceName}
            </h3>
          </Link>
          <p className="text-sm text-gray-500 mt-1">
            {formatCurrency(item.price)}
          </p>
          <p className="text-sm text-gray-600 mt-2">
            Tổng: <span className="font-semibold text-primary-500">{formatCurrency(item.totalPrice)}</span>
          </p>
        </div>

        {/* Quantity & Remove */}
        <div className="flex flex-col items-end justify-between">
          {/* Quantity Controls */}
          <div className="flex items-center gap-2">
            <button
              onClick={() => handleQuantityChange(item.quantity - 1)}
              disabled={updating || item.quantity <= 1}
              className="w-8 h-8 flex items-center justify-center border border-gray-300 rounded hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed"
            >
              <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M20 12H4" />
              </svg>
            </button>
            <span className="w-12 text-center font-medium">{item.quantity}</span>
            <button
              onClick={() => handleQuantityChange(item.quantity + 1)}
              disabled={updating}
              className="w-8 h-8 flex items-center justify-center border border-gray-300 rounded hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed"
            >
              <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 4v16m8-8H4" />
              </svg>
            </button>
          </div>

          {/* Remove Button */}
          <button
            onClick={handleRemove}
            disabled={removing}
            className="text-red-500 hover:text-red-700 text-sm font-medium disabled:opacity-50"
          >
            {removing ? 'Đang xóa...' : 'Xóa'}
          </button>
        </div>
      </div>
    </div>
  );
};

export default CartItem;