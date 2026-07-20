import { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import { productAPI, categoryAPI } from '../services/api';
import ProductCard from '../components/product/ProductCard';
import LoadingSpinner from '../components/common/LoadingSpinner';
import { IMAGE_PATHS } from '../utils/constants';

const HomePage = () => {
  const [bestSellingProducts, setBestSellingProducts] = useState([]);
  const [dogProducts, setDogProducts] = useState([]);
  const [catProducts, setCatProducts] = useState([]);
  const [newArrivals, setNewArrivals] = useState([]);
  const [loading, setLoading] = useState(true);

  useEffect(() => {
    fetchHomeData();
  }, []);

  const fetchHomeData = async () => {
    try {
      setLoading(true);

      const categoriesRes = await categoryAPI.getAll({ type: 'product', isActive: true });
      const categories = categoriesRes.data.success ? categoriesRes.data.data : [];

      const dogCategory = categories.find(cat =>
        cat.name.toLowerCase().includes('chó') || cat.name.toLowerCase().includes('dog')
      );
      const catCategory = categories.find(cat =>
        cat.name.toLowerCase().includes('mèo') || cat.name.toLowerCase().includes('cat')
      );

      const productsRes = await productAPI.getAll({
        isActive: true,
        pageSize: 50
      });

      if (productsRes.data.success) {
        const allProducts = productsRes.data.data.items || [];

        setBestSellingProducts(allProducts.slice(0, 8));

        if (dogCategory) {
          const dogProds = allProducts.filter(p => p.categoryId === dogCategory.categoryId);
          setDogProducts(dogProds.slice(0, 8));
        }

        if (catCategory) {
          const catProds = allProducts.filter(p => p.categoryId === catCategory.categoryId);
          setCatProducts(catProds.slice(0, 8));
        }

        const sortedByDate = [...allProducts].sort((a, b) =>
          new Date(b.createdAt) - new Date(a.createdAt)
        );
        setNewArrivals(sortedByDate.slice(0, 8));
      }
    } catch (error) {
      console.error('Fetch home data error:', error);
    } finally {
      setLoading(false);
    }
  };

  if (loading) {
    return <LoadingSpinner fullScreen />;
  }

  return (
    <div className="bg-gray-50">
      {/* Hero Banner */}
      <section className="bg-gradient-to-br from-orange-50 via-amber-50 to-orange-100">
        <div className="container-custom py-16 lg:py-20">
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-12 items-center">
            {/* Left Content */}
            <div className="text-center lg:text-left">
              <h1 className="text-4xl lg:text-5xl xl:text-6xl font-bold text-gray-900 mb-6 leading-tight">
                Chăm sóc thú cưng<br />
                Chuyên nghiệp & Tận tâm
              </h1>
              <p className="text-lg text-gray-700 mb-8 max-w-xl">
                Cung cấp sản phẩm chất lượng cao và dịch vụ spa tốt nhất cho thú cưng của bạn.
              </p>
              <div className="flex flex-col sm:flex-row gap-4 justify-center lg:justify-start">
                <Link
                  to="/products"
                  className="bg-primary-500 text-white hover:bg-primary-600 font-semibold py-3 px-8 rounded-lg transition-colors inline-block shadow-md hover:shadow-lg"
                >
                  Mua sắm ngay
                </Link>
                <Link
                  to="/service"
                  className="bg-transparent border-2 border-primary-500 text-primary-500 hover:bg-primary-500 hover:text-white font-semibold py-3 px-8 rounded-lg transition-colors inline-block"
                >
                  Đặt lịch Spa
                </Link>
              </div>
            </div>

            {/* Right Image - Circular Frame */}
            <div className="hidden lg:flex justify-center lg:justify-end">
              <div className="relative w-[500px] h-[500px]">
                {/* Large background circle */}
                <div className="absolute inset-0 bg-gradient-to-br from-blue-100 to-blue-50 rounded-full opacity-60"></div>

                {/* Main image in circular frame */}
                <div className="absolute inset-8 rounded-full overflow-hidden shadow-2xl">
                  <img
                    src="https://images.unsplash.com/photo-1450778869180-41d0601e046e?w=600&h=400&fit=crop"
                    alt="Pet Care"
                    className="w-full h-full object-cover"
                    onError={(e) => {
                      e.target.src = 'https://via.placeholder.com/500x500?text=Pet+Care';
                    }}
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>

      {/* Features */}
      <section className="py-12 bg-white">
        <div className="container-custom">
          <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6">
            <div className="text-center p-6">
              <div className="w-16 h-16 bg-primary-100 rounded-full flex items-center justify-center mx-auto mb-4">
                <svg className="w-8 h-8 text-primary-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
                </svg>
              </div>
              <h3 className="font-semibold text-gray-900 mb-2">Hàng chính hãng</h3>
              <p className="text-sm text-gray-600">100% sản phẩm chính hãng, có nguồn gốc rõ ràng</p>
            </div>

            <div className="text-center p-6">
              <div className="w-16 h-16 bg-primary-100 rounded-full flex items-center justify-center mx-auto mb-4">
                <svg className="w-8 h-8 text-primary-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
                </svg>
              </div>
              <h3 className="font-semibold text-gray-900 mb-2">Giao hàng nhanh</h3>
              <p className="text-sm text-gray-600">Giao hàng tận nơi trong 24-48h</p>
            </div>

            <div className="text-center p-6">
              <div className="w-16 h-16 bg-primary-100 rounded-full flex items-center justify-center mx-auto mb-4">
                <svg className="w-8 h-8 text-primary-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M3 10h18M7 15h1m4 0h1m-7 4h12a3 3 0 003-3V8a3 3 0 00-3-3H6a3 3 0 00-3 3v8a3 3 0 003 3z" />
                </svg>
              </div>
              <h3 className="font-semibold text-gray-900 mb-2">Thanh toán linh hoạt</h3>
              <p className="text-sm text-gray-600">COD, chuyển khoản ngân hàng</p>
            </div>

            <div className="text-center p-6">
              <div className="w-16 h-16 bg-primary-100 rounded-full flex items-center justify-center mx-auto mb-4">
                <svg className="w-8 h-8 text-primary-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M18 9v3m0 0v3m0-3h3m-3 0h-3m-2-5a4 4 0 11-8 0 4 4 0 018 0zM3 20a6 6 0 0112 0v1H3v-1z" />
                </svg>
              </div>
              <h3 className="font-semibold text-gray-900 mb-2">Hỗ trợ 24/7</h3>
              <p className="text-sm text-gray-600">Đội ngũ chăm sóc khách hàng tận tình</p>
            </div>
          </div>
        </div>
      </section>

      {/* Best Selling Products */}
      {bestSellingProducts.length > 0 && (
        <section className="py-12">
          <div className="container-custom">
            <div className="flex items-center justify-between mb-8">
              <div>
                <h2 className="text-3xl font-bold text-gray-900 mb-2">Sản phẩm bán chạy</h2>
                <p className="text-gray-600">Những sản phẩm được yêu thích nhất</p>
              </div>
              <Link to="/products" className="text-primary-500 hover:text-primary-600 font-medium flex items-center gap-2">
                Xem tất cả
                <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 5l7 7-7 7" />
                </svg>
              </Link>
            </div>
            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
              {bestSellingProducts.map((product) => (
                <ProductCard key={product.productId} product={product} />
              ))}
            </div>
          </div>
        </section>
      )}

      {/* Banner - Service */}
      <section className="py-12 bg-gradient-to-br from-blue-500 to-blue-600">
        <div className="container-custom">
          <div className="grid grid-cols-1 lg:grid-cols-2 gap-8 items-center">
            <div className="text-white">
              <h2 className="text-3xl lg:text-4xl font-bold mb-4">
                Dịch vụ Spa cho thú cưng
              </h2>
              <p className="text-lg text-blue-100 mb-6">
                Tắm gội, cắt tỉa, chăm sóc lông chuyên nghiệp với đội ngũ có kinh nghiệm
              </p>
              <Link to="/service" className="bg-white text-blue-500 hover:bg-gray-100 font-semibold py-3 px-8 rounded-lg transition-colors inline-block">
                Đặt lịch ngay
              </Link>
            </div>
            <div className="hidden lg:block">
              <img
                src="https://images.unsplash.com/photo-1587300003388-59208cc962cb?w=600&h=400&fit=crop"
                alt="Pet Spa Service"
                className="rounded-lg shadow-2xl"
                onError={(e) => {
                  e.target.style.display = 'none';
                }}
              />
            </div>
          </div>
        </div>
      </section>

      {/* Dog Products */}
      {dogProducts.length > 0 && (
        <section className="py-12 bg-white">
          <div className="container-custom">
            <div className="flex items-center justify-between mb-8">
              <div>
                <h2 className="text-3xl font-bold text-gray-900 mb-2">Sản phẩm cho chó</h2>
                <p className="text-gray-600">Thức ăn, đồ chơi và phụ kiện cho cún cưng</p>
              </div>
              <Link to="/products?categoryId=1" className="text-primary-500 hover:text-primary-600 font-medium flex items-center gap-2">
                Xem tất cả
                <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 5l7 7-7 7" />
                </svg>
              </Link>
            </div>
            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
              {dogProducts.map((product) => (
                <ProductCard key={product.productId} product={product} />
              ))}
            </div>
          </div>
        </section>
      )}

      {/* Cat Products */}
      {catProducts.length > 0 && (
        <section className="py-12">
          <div className="container-custom">
            <div className="flex items-center justify-between mb-8">
              <div>
                <h2 className="text-3xl font-bold text-gray-900 mb-2">Sản phẩm cho mèo</h2>
                <p className="text-gray-600">Thức ăn, đồ chơi và phụ kiện cho mèo yêu</p>
              </div>
              <Link to="/products?categoryId=2" className="text-primary-500 hover:text-primary-600 font-medium flex items-center gap-2">
                Xem tất cả
                <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 5l7 7-7 7" />
                </svg>
              </Link>
            </div>
            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
              {catProducts.map((product) => (
                <ProductCard key={product.productId} product={product} />
              ))}
            </div>
          </div>
        </section>
      )}

      {/* New Arrivals */}
      {newArrivals.length > 0 && (
        <section className="py-12 bg-white">
          <div className="container-custom">
            <div className="flex items-center justify-between mb-8">
              <div>
                <h2 className="text-3xl font-bold text-gray-900 mb-2">Hàng mới về</h2>
                <p className="text-gray-600">Những sản phẩm mới nhất</p>
              </div>
              <Link to="/products" className="text-primary-500 hover:text-primary-600 font-medium flex items-center gap-2">
                Xem tất cả
                <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 5l7 7-7 7" />
                </svg>
              </Link>
            </div>
            <div className="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-6">
              {newArrivals.map((product) => (
                <ProductCard key={product.productId} product={product} />
              ))}
            </div>
          </div>
        </section>
      )}

      {/* Newsletter */}
      <section className="py-16 bg-gradient-to-r from-primary-500 to-primary-600">
        <div className="container-custom text-center">
          <h2 className="text-3xl font-bold text-white mb-4">
            Đăng ký nhận tin khuyến mãi
          </h2>
          <p className="text-primary-100 mb-8 max-w-2xl mx-auto">
            Nhận thông tin về sản phẩm mới, khuyến mãi và các mẹo chăm sóc thú cưng
          </p>
          <form className="flex flex-col sm:flex-row gap-3 max-w-md mx-auto">
            <input
              type="email"
              placeholder="Nhập email của bạn"
              className="flex-1 px-4 py-3 rounded-lg focus:outline-none focus:ring-2 focus:ring-white"
            />
            <button
              type="submit"
              className="bg-white text-primary-500 hover:bg-gray-100 font-semibold px-8 py-3 rounded-lg transition-colors"
            >
              Đăng ký
            </button>
          </form>
        </div>
      </section>

      {/* Why Choose Us */}
      <section className="py-16 bg-gray-100">
        <div className="container-custom">
          <div className="text-center mb-12">
            <h2 className="text-3xl font-bold text-gray-900 mb-4">Tại sao chọn chúng tôi?</h2>
            <p className="text-gray-600 max-w-2xl mx-auto">
              Chúng tôi cam kết mang đến dịch vụ và sản phẩm tốt nhất cho thú cưng của bạn
            </p>
          </div>

          <div className="grid grid-cols-1 md:grid-cols-3 gap-8">
            <div className="bg-white rounded-lg p-8 shadow-md">
              <div className="w-12 h-12 bg-primary-100 rounded-lg flex items-center justify-center mb-4">
                <svg className="w-6 h-6 text-primary-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M9 12l2 2 4-4m5.618-4.016A11.955 11.955 0 0112 2.944a11.955 11.955 0 01-8.618 3.04A12.02 12.02 0 003 9c0 5.591 3.824 10.29 9 11.622 5.176-1.332 9-6.03 9-11.622 0-1.042-.133-2.052-.382-3.016z" />
                </svg>
              </div>
              <h3 className="text-xl font-bold text-gray-900 mb-3">Chất lượng đảm bảo</h3>
              <p className="text-gray-600">
                Tất cả sản phẩm đều được kiểm định chất lượng nghiêm ngặt, đảm bảo an toàn cho thú cưng
              </p>
            </div>

            <div className="bg-white rounded-lg p-8 shadow-md">
              <div className="w-12 h-12 bg-primary-100 rounded-lg flex items-center justify-center mb-4">
                <svg className="w-6 h-6 text-primary-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 4.354a4 4 0 110 5.292M15 21H3v-1a6 6 0 0112 0v1zm0 0h6v-1a6 6 0 00-9-5.197M13 7a4 4 0 11-8 0 4 4 0 018 0z" />
                </svg>
              </div>
              <h3 className="text-xl font-bold text-gray-900 mb-3">Đội ngũ chuyên nghiệp</h3>
              <p className="text-gray-600">
                Nhân viên được đào tạo bài bản, có kinh nghiệm và yêu thương động vật
              </p>
            </div>

            <div className="bg-white rounded-lg p-8 shadow-md">
              <div className="w-12 h-12 bg-primary-100 rounded-lg flex items-center justify-center mb-4">
                <svg className="w-6 h-6 text-primary-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                  <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M14.828 14.828a4 4 0 01-5.656 0M9 10h.01M15 10h.01M21 12a9 9 0 11-18 0 9 9 0 0118 0z" />
                </svg>
              </div>
              <h3 className="text-xl font-bold text-gray-900 mb-3">Khách hàng hài lòng</h3>
              <p className="text-gray-600">
                Hàng ngàn khách hàng tin tưởng và hài lòng với sản phẩm và dịch vụ của chúng tôi
              </p>
            </div>
          </div>
        </div>
      </section>
    </div>
  );
};

export default HomePage;