import { useState, useEffect } from 'react';
import { useParams, useNavigate, Link } from 'react-router-dom';
import { productAPI } from '../services/api';
import { useCart } from '../contexts/CartContext';
import { useAuth } from '../contexts/AuthContext';
import { formatCurrency, getStockStatus, getImageUrl } from '../utils/helpers';
import LoadingSpinner from '../components/common/LoadingSpinner';
import ProductCard from '../components/product/ProductCard';

const ProductDetailPage = () => {
  const { id } = useParams();
  const navigate = useNavigate();
  const { addToCart } = useCart();
  const { isAuthenticated } = useAuth();

  const [product, setProduct] = useState(null);
  const [relatedProducts, setRelatedProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [quantity, setQuantity] = useState(1);
  const [addingToCart, setAddingToCart] = useState(false);
  const [message, setMessage] = useState({ type: '', text: '' });

  useEffect(() => {
    fetchProductDetail();
    window.scrollTo(0, 0);
  }, [id]);

  const fetchProductDetail = async () => {
    try {
      setLoading(true);
      setError(null);

      const response = await productAPI.getById(id);

      if (response.data.success) {
        const productData = response.data.data;
        setProduct(productData);

        if (productData.categoryId) {
          fetchRelatedProducts(productData.categoryId, productData.productId);
        }
      } else {
        setError('Không tìm thấy sản phẩm');
      }
    } catch (err) {
      console.error('Fetch product error:', err);
      setError('Không thể tải thông tin sản phẩm');
    } finally {
      setLoading(false);
    }
  };

  const fetchRelatedProducts = async (categoryId, excludeId) => {
    try {
      const response = await productAPI.getAll({
        categoryId,
        isActive: true,
        pageSize: 8
      });

      if (response.data.success) {
        const products = response.data.data.items || [];
        const filtered = products.filter(p => p.productId !== excludeId);
        setRelatedProducts(filtered.slice(0, 4));
      }
    } catch (err) {
      console.error('Fetch related products error:', err);
    }
  };

  const handleQuantityChange = (delta) => {
    const newQuantity = quantity + delta;
    if (newQuantity >= 1 && newQuantity <= (product?.stockQuantity || 1)) {
      setQuantity(newQuantity);
    }
  };

  const handleAddToCart = async () => {
    if (!isAuthenticated) {
      navigate('/login', { state: { from: { pathname: `/products/${id}` } } });
      return;
    }

    setAddingToCart(true);
    setMessage({ type: '', text: '' });

    const result = await addToCart(product.productId, null, quantity);

    setAddingToCart(false);

    if (result.success) {
      setMessage({ type: 'success', text: result.message });
      setTimeout(() => setMessage({ type: '', text: '' }), 3000);
    } else {
      setMessage({ type: 'error', text: result.message });
    }
  };

  const handleBuyNow = async () => {
    if (!isAuthenticated) {
      navigate('/login', { state: { from: { pathname: `/products/${id}` } } });
      return;
    }

    setAddingToCart(true);
    const result = await addToCart(product.productId, null, quantity);
    setAddingToCart(false);

    if (result.success) {
      navigate('/cart');
    } else {
      setMessage({ type: 'error', text: result.message });
    }
  };

  if (loading) {
    return <LoadingSpinner fullScreen />;
  }

  if (error || !product) {
    return (
      <div className="container-custom py-12">
        <div className="text-center">
          <svg className="w-16 h-16 text-gray-400 mx-auto mb-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8v4m0 4h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
          </svg>
          <h3 className="text-lg font-medium text-gray-900 mb-2">{error || 'Không tìm thấy sản phẩm'}</h3>
          <Link to="/products" className="text-primary-500 hover:text-primary-600">
            Quay lại danh sách sản phẩm
          </Link>
        </div>
      </div>
    );
  }

  const stockStatus = getStockStatus(product.stockQuantity);
  const imageUrl = getImageUrl(product.imageUrl);

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
                <Link to="/products" className="ml-1 text-gray-700 hover:text-primary-500 md:ml-2">
                  Sản phẩm
                </Link>
              </div>
            </li>
            <li>
              <div className="flex items-center">
                <svg className="w-6 h-6 text-gray-400" fill="currentColor" viewBox="0 0 20 20">
                  <path fillRule="evenodd" d="M7.293 14.707a1 1 0 010-1.414L10.586 10 7.293 6.707a1 1 0 011.414-1.414l4 4a1 1 0 010 1.414l-4 4a1 1 0 01-1.414 0z" clipRule="evenodd" />
                </svg>
                <span className="ml-1 text-gray-500 md:ml-2">{product.name}</span>
              </div>
            </li>
          </ol>
        </nav>

        {/* Product Detail */}
        <div className="bg-white rounded-lg shadow-lg overflow-hidden mb-12">
          <div className="grid grid-cols-1 md:grid-cols-2 gap-8 p-6 md:p-8">
            {/* Image */}
            <div>
              <div className="relative aspect-square bg-gray-100 rounded-lg overflow-hidden">
                <img
                  src={imageUrl}
                  alt={product.name}
                  className="w-full h-full object-cover"
                  onError={(e) => {
                    e.target.src = 'https://via.placeholder.com/600x600?text=No+Image';
                  }}
                />
                {product.brand && (
                  <div className="absolute top-4 right-4">
                    <span className="px-3 py-1 bg-white text-gray-800 text-sm font-semibold rounded-full shadow">
                      {product.brand}
                    </span>
                  </div>
                )}
              </div>
            </div>

            {/* Info */}
            <div>
              {/* Category */}
              <p className="text-sm text-primary-500 font-medium mb-2">{product.categoryName}</p>

              {/* Name */}
              <h1 className="text-3xl font-bold text-gray-900 mb-4">{product.name}</h1>

              {/* Stock Status */}
              <div className="mb-4">
                <span className={`inline-block px-3 py-1 text-sm font-semibold rounded ${stockStatus.className}`}>
                  {stockStatus.label}
                  {product.stockQuantity > 0 && ` (Còn ${product.stockQuantity} sản phẩm)`}
                </span>
              </div>

              {/* Price */}
              <div className="mb-6">
                <div className="flex items-baseline gap-2">
                  <span className="text-4xl font-bold text-primary-500">
                    {formatCurrency(product.price)}
                  </span>
                  {product.unit && (
                    <span className="text-lg text-gray-500">/ {product.unit}</span>
                  )}
                </div>
              </div>

              {/* Description */}
              {product.description && (
                <div className="mb-6">
                  <h3 className="text-lg font-semibold text-gray-900 mb-2">Mô tả sản phẩm</h3>
                  <p className="text-gray-600 whitespace-pre-line">{product.description}</p>
                </div>
              )}

              {/* Message */}
              {message.text && (
                <div className={`mb-4 p-3 rounded-lg ${
                  message.type === 'success' ? 'bg-green-50 text-green-800' : 'bg-red-50 text-red-800'
                }`}>
                  {message.text}
                </div>
              )}

              {/* Quantity & Actions */}
              {stockStatus.available && (
                <div className="space-y-4">
                  {/* Quantity */}
                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-2">
                      Số lượng
                    </label>
                    <div className="flex items-center gap-3">
                      <button
                        onClick={() => handleQuantityChange(-1)}
                        disabled={quantity <= 1}
                        className="w-10 h-10 flex items-center justify-center border border-gray-300 rounded-lg hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed"
                      >
                        <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M20 12H4" />
                        </svg>
                      </button>
                      <input
                        type="number"
                        value={quantity}
                        onChange={(e) => {
                          const val = parseInt(e.target.value) || 1;
                          if (val >= 1 && val <= product.stockQuantity) {
                            setQuantity(val);
                          }
                        }}
                        className="w-20 text-center border border-gray-300 rounded-lg py-2 focus:outline-none focus:ring-2 focus:ring-primary-500"
                      />
                      <button
                        onClick={() => handleQuantityChange(1)}
                        disabled={quantity >= product.stockQuantity}
                        className="w-10 h-10 flex items-center justify-center border border-gray-300 rounded-lg hover:bg-gray-50 disabled:opacity-50 disabled:cursor-not-allowed"
                      >
                        <svg className="w-4 h-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 4v16m8-8H4" />
                        </svg>
                      </button>
                    </div>
                  </div>

                  {/* Action Buttons */}
                  <div className="flex gap-3">
                    <button
                      onClick={handleAddToCart}
                      disabled={addingToCart}
                      className="flex-1 btn-outline flex items-center justify-center gap-2 disabled:opacity-50"
                    >
                      {addingToCart ? (
                        <div className="w-5 h-5 border-2 border-primary-500 border-t-transparent rounded-full animate-spin"></div>
                      ) : (
                        <>
                          <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M3 3h2l.4 2M7 13h10l4-8H5.4M7 13L5.4 5M7 13l-2.293 2.293c-.63.63-.184 1.707.707 1.707H17m0 0a2 2 0 100 4 2 2 0 000-4zm-8 2a2 2 0 11-4 0 2 2 0 014 0z" />
                          </svg>
                          Thêm vào giỏ
                        </>
                      )}
                    </button>
                    <button
                      onClick={handleBuyNow}
                      disabled={addingToCart}
                      className="flex-1 btn-primary disabled:opacity-50"
                    >
                      Mua ngay
                    </button>
                  </div>
                </div>
              )}

              {/* Out of Stock Message */}
              {!stockStatus.available && (
                <div className="bg-red-50 border border-red-200 rounded-lg p-4">
                  <p className="text-red-800 font-medium">
                    Sản phẩm tạm hết hàng. Vui lòng quay lại sau!
                  </p>
                </div>
              )}
            </div>
          </div>
        </div>

        {/* Related Products */}
        {relatedProducts.length > 0 && (
          <div>
            <h2 className="text-2xl font-bold text-gray-900 mb-6">Sản phẩm liên quan</h2>
            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
              {relatedProducts.map((relatedProduct) => (
                <ProductCard key={relatedProduct.productId} product={relatedProduct} />
              ))}
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default ProductDetailPage;