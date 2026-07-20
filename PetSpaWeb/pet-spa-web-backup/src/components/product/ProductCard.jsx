import { Link } from 'react-router-dom';
import { useCart } from '../../contexts/CartContext';
import { useAuth } from '../../contexts/AuthContext';
import { formatCurrency, getStockStatus, getImageUrl } from '../../utils/helpers';
import { useState } from 'react';

const ProductCard = ({ product }) => {
  const { addToCart } = useCart();
  const { isAuthenticated } = useAuth();
  const [addingToCart, setAddingToCart] = useState(false);
  const [showMessage, setShowMessage] = useState(false);

  const stockStatus = getStockStatus(product.stockQuantity);
  const imageUrl = getImageUrl(product.imageUrl);

  const handleAddToCart = async (e) => {
    e.preventDefault();
    e.stopPropagation();

    if (!stockStatus.available) return;

    if (!isAuthenticated) {
      window.location.href = '/login';
      return;
    }

    setAddingToCart(true);
    const result = await addToCart(product.productId, null, 1);
    setAddingToCart(false);

    if (result.success) {
      setShowMessage(true);
      setTimeout(() => setShowMessage(false), 2000);
    }
  };

  return (
    <Link to={`/products/${product.productId}`} className="block group">
      <div className="card overflow-hidden h-full flex flex-col">
        {/* Image */}
        <div className="relative overflow-hidden bg-gray-100 aspect-square">
          <img
            src={imageUrl}
            alt={product.name}
            className="w-full h-full object-cover group-hover:scale-110 transition-transform duration-300"
            onError={(e) => {
              e.target.src = 'https://via.placeholder.com/400x400?text=No+Image';
            }}
          />
          
          {/* Stock Badge */}
          <div className="absolute top-2 left-2">
            <span className={`px-2 py-1 text-xs font-semibold rounded ${stockStatus.className}`}>
              {stockStatus.label}
            </span>
          </div>

          {/* Brand Badge */}
          {product.brand && (
            <div className="absolute top-2 right-2">
              <span className="px-2 py-1 text-xs font-semibold bg-white text-gray-700 rounded shadow">
                {product.brand}
              </span>
            </div>
          )}

          {/* Quick Add to Cart */}
          {stockStatus.available && (
            <button
              onClick={handleAddToCart}
              disabled={addingToCart}
              className="absolute bottom-2 right-2 bg-white text-primary-500 p-2 rounded-full shadow-lg opacity-0 group-hover:opacity-100 transition-opacity duration-300 hover:bg-primary-500 hover:text-white disabled:opacity-50"
            >
              {addingToCart ? (
                <div className="w-5 h-5 border-2 border-current border-t-transparent rounded-full animate-spin"></div>
              ) : (
                <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M3 3h2l.4 2M7 13h10l4-8H5.4M7 13L5.4 5M7 13l-2.293 2.293c-.63.63-.184 1.707.707 1.707H17m0 0a2 2 0 100 4 2 2 0 000-4zm-8 2a2 2 0 11-4 0 2 2 0 014 0z" />
                </svg>
              )}
            </button>
          )}

          {/* Success Message */}
          {showMessage && (
            <div className="absolute inset-0 bg-black bg-opacity-50 flex items-center justify-center">
              <div className="bg-white px-4 py-2 rounded-lg text-sm font-medium text-green-600">
                Đã thêm vào giỏ!
              </div>
            </div>
          )}
        </div>

        {/* Content */}
        <div className="p-4 flex flex-col flex-grow">
          {/* Category */}
          <p className="text-xs text-gray-500 mb-1">{product.categoryName}</p>

          {/* Name */}
          <h3 className="font-medium text-gray-900 mb-2 line-clamp-2 group-hover:text-primary-500 transition-colors">
            {product.name}
          </h3>

          {/* Description */}
          {product.description && (
            <p className="text-sm text-gray-600 mb-3 line-clamp-2">
              {product.description}
            </p>
          )}

          {/* Price & Unit */}
          <div className="mt-auto">
            <div className="flex items-baseline justify-between">
              <div>
                <span className="text-lg font-bold text-primary-500">
                  {formatCurrency(product.price)}
                </span>
                {product.unit && (
                  <span className="text-sm text-gray-500 ml-1">/ {product.unit}</span>
                )}
              </div>
            </div>
          </div>
        </div>
      </div>
    </Link>
  );
};

export default ProductCard;