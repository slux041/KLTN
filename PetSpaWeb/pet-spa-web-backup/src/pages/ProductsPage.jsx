import { useState, useEffect } from 'react';
import { useSearchParams } from 'react-router-dom';
import { productAPI } from '../services/api';
import ProductFilter from '../components/product/ProductFilter';
import ProductGrid from '../components/product/ProductGrid';
import Pagination from '../components/product/Pagination';
import { PAGINATION } from '../utils/constants';

const ProductsPage = () => {
  const [searchParams, setSearchParams] = useSearchParams();
  const [products, setProducts] = useState([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState(null);
  const [pagination, setPagination] = useState({
    currentPage: 1,
    totalPages: 1,
    totalCount: 0
  });

  const [filters, setFilters] = useState({
    categoryId: searchParams.get('categoryId') || '',
    brand: searchParams.get('brand') || '',
    priceRange: searchParams.get('priceRange') || '',
    search: searchParams.get('search') || ''
  });

  const [sortBy, setSortBy] = useState('newest');

  useEffect(() => {
    setFilters({
      categoryId: searchParams.get('categoryId') || '',
      brand: searchParams.get('brand') || '',
      priceRange: searchParams.get('priceRange') || '',
      search: searchParams.get('search') || ''
    });
  }, [searchParams]);

  useEffect(() => {
    window.scrollTo(0, 0);
    fetchProducts();
  }, [filters]);

  const fetchProducts = async () => {
    try {
      setLoading(true);
      setError(null);

      const params = {
        pageNumber: parseInt(searchParams.get('page')) || PAGINATION.DEFAULT_PAGE,
        pageSize: PAGINATION.PRODUCTS_PER_PAGE,
        isActive: true
      };

      if (filters.categoryId) {
        params.categoryId = parseInt(filters.categoryId);
      }

      if (filters.brand) {
        params.brand = filters.brand;
      }

      if (filters.search) {
        params.search = filters.search;
      }

      console.log('🔍 ProductsPage - Fetching products with params:', params);
      console.log('🔍 ProductsPage - Current filters:', filters);

      const response = await productAPI.getAll(params);

      console.log('✅ ProductsPage - API response:', response.data);

      if (response.data.success) {
        let productList = response.data.data.items || [];

        console.log('📦 ProductsPage - Products received:', productList.length);

        if (filters.priceRange) {
          const [min, max] = filters.priceRange.split('-');
          productList = productList.filter(product => {
            if (max === 'max') {
              return product.price >= parseInt(min);
            }
            return product.price >= parseInt(min) && product.price <= parseInt(max);
          });
          console.log('💰 ProductsPage - After price filter:', productList.length);
        }

        productList = sortProducts(productList, sortBy);

        setProducts(productList);
        setPagination({
          currentPage: response.data.data.pageNumber,
          totalPages: response.data.data.totalPages,
          totalCount: response.data.data.totalCount
        });
      }
    } catch (err) {
      console.error('❌ ProductsPage - Fetch products error:', err);
      console.error('❌ ProductsPage - Error response:', err.response?.data);
      setError('Không thể tải danh sách sản phẩm');
    } finally {
      setLoading(false);
    }
  };

  const sortProducts = (productList, sortType) => {
    const sorted = [...productList];

    switch (sortType) {
      case 'price-asc':
        return sorted.sort((a, b) => a.price - b.price);
      case 'price-desc':
        return sorted.sort((a, b) => b.price - a.price);
      case 'name-asc':
        return sorted.sort((a, b) => a.name.localeCompare(b.name));
      case 'name-desc':
        return sorted.sort((a, b) => b.name.localeCompare(a.name));
      case 'newest':
      default:
        return sorted.sort((a, b) => new Date(b.createdAt) - new Date(a.createdAt));
    }
  };

  const handleFilterChange = (newFilters) => {
    setFilters(newFilters);

    const params = { ...newFilters, page: 1 };
    Object.keys(params).forEach(key => {
      if (!params[key]) delete params[key];
    });
    setSearchParams(params);

    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  const handlePageChange = (page) => {
    const params = Object.fromEntries(searchParams);
    params.page = page;
    setSearchParams(params);
    window.scrollTo({ top: 0, behavior: 'smooth' });
  };

  const handleSortChange = (e) => {
    const newSortBy = e.target.value;
    setSortBy(newSortBy);
    const sorted = sortProducts(products, newSortBy);
    setProducts(sorted);
  };

  return (
    <div className="bg-gray-50 py-8">
      <div className="container-custom">
        {/* Breadcrumb */}
        <nav className="flex mb-6" aria-label="Breadcrumb">
          <ol className="inline-flex items-center space-x-1 md:space-x-3">
            <li className="inline-flex items-center">
              <a href="/" className="text-gray-700 hover:text-primary-500 inline-flex items-center">
                <svg className="w-4 h-4 mr-2" fill="currentColor" viewBox="0 0 20 20">
                  <path d="M10.707 2.293a1 1 0 00-1.414 0l-7 7a1 1 0 001.414 1.414L4 10.414V17a1 1 0 001 1h2a1 1 0 001-1v-2a1 1 0 011-1h2a1 1 0 011 1v2a1 1 0 001 1h2a1 1 0 001-1v-6.586l.293.293a1 1 0 001.414-1.414l-7-7z" />
                </svg>
                Trang chủ
              </a>
            </li>
            <li>
              <div className="flex items-center">
                <svg className="w-6 h-6 text-gray-400" fill="currentColor" viewBox="0 0 20 20">
                  <path fillRule="evenodd" d="M7.293 14.707a1 1 0 010-1.414L10.586 10 7.293 6.707a1 1 0 011.414-1.414l4 4a1 1 0 010 1.414l-4 4a1 1 0 01-1.414 0z" clipRule="evenodd" />
                </svg>
                <span className="ml-1 text-gray-500 md:ml-2">Sản phẩm</span>
              </div>
            </li>
          </ol>
        </nav>

        {/* Header */}
        <div className="flex flex-col md:flex-row md:items-center md:justify-between mb-6">
          <div>
            <h1 className="text-3xl font-bold text-gray-900 mb-2">Sản phẩm</h1>
            <p className="text-gray-600">
              Tìm thấy {pagination.totalCount} sản phẩm
            </p>
          </div>

          {/* Sort */}
          <div className="mt-4 md:mt-0">
            <select
              value={sortBy}
              onChange={handleSortChange}
              className="input-field w-full md:w-auto"
            >
              <option value="newest">Mới nhất</option>
              <option value="price-asc">Giá: Thấp đến cao</option>
              <option value="price-desc">Giá: Cao đến thấp</option>
              <option value="name-asc">Tên: A-Z</option>
              <option value="name-desc">Tên: Z-A</option>
            </select>
          </div>
        </div>

        {/* Main Content */}
        <div className="grid grid-cols-1 lg:grid-cols-4 gap-6">
          {/* Sidebar Filter */}
          <div className="lg:col-span-1">
            <ProductFilter
              filters={filters}
              onFilterChange={handleFilterChange}
            />
          </div>

          {/* Products Grid */}
          <div className="lg:col-span-3">
            <ProductGrid products={products} loading={loading} error={error} />

            {/* Pagination */}
            {!loading && !error && products.length > 0 && (
              <Pagination
                currentPage={pagination.currentPage}
                totalPages={pagination.totalPages}
                onPageChange={handlePageChange}
              />
            )}
          </div>
        </div>
      </div>
    </div>
  );
};

export default ProductsPage;