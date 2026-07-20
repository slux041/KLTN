import { useState, useEffect } from 'react';
import { categoryAPI, productAPI } from '../../services/api';
import { PRICE_RANGES } from '../../utils/constants';

const ProductFilter = ({ filters, onFilterChange }) => {
  const [categories, setCategories] = useState([]);
  const [brands, setBrands] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showMobileFilters, setShowMobileFilters] = useState(false);

  useEffect(() => {
    fetchFilterData();
  }, []);

  const fetchFilterData = async () => {
    try {
      setLoading(true);
      const [categoriesRes, brandsRes] = await Promise.all([
        categoryAPI.getAll({ type: 'product', isActive: true }),
        productAPI.getBrands()
      ]);

      if (categoriesRes.data.success) {
        setCategories(categoriesRes.data.data || []);
      }

      if (brandsRes.data.success) {
        setBrands(brandsRes.data.data || []);
      }
    } catch (error) {
      console.error('Fetch filter data error:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleChange = (name, value) => {
    onFilterChange({ ...filters, [name]: value });
  };

  const FilterContent = () => (
    <div className="space-y-6">
      {/* Category Filter */}
      <div>
        <label className="block text-sm font-semibold text-gray-700 mb-3">
          Danh mục
        </label>
        <div className="space-y-2">
          <label className="flex items-center cursor-pointer">
            <input
              type="radio"
              name="category"
              checked={filters.categoryId === ''}
              onChange={() => handleChange('categoryId', '')}
              className="w-4 h-4 text-primary-500 focus:ring-primary-500"
            />
            <span className="ml-2 text-sm text-gray-700">Tất cả</span>
          </label>
          {categories.map((category) => (
            <label key={category.categoryId} className="flex items-center cursor-pointer">
              <input
                type="radio"
                name="category"
                checked={filters.categoryId === category.categoryId.toString()}
                onChange={() => handleChange('categoryId', category.categoryId.toString())}
                className="w-4 h-4 text-primary-500 focus:ring-primary-500"
              />
              <span className="ml-2 text-sm text-gray-700">{category.name}</span>
            </label>
          ))}
        </div>
      </div>

      {/* Price Range Filter */}
      <div>
        <label className="block text-sm font-semibold text-gray-700 mb-3">
          Khoảng giá
        </label>
        <select
          value={filters.priceRange}
          onChange={(e) => handleChange('priceRange', e.target.value)}
          className="input-field"
        >
          {PRICE_RANGES.map((range, index) => (
            <option key={index} value={index === 0 ? '' : `${range.min}-${range.max || 'max'}`}>
              {range.label}
            </option>
          ))}
        </select>
      </div>

      {/* Brand Filter */}
      <div>
        <label className="block text-sm font-semibold text-gray-700 mb-3">
          Thương hiệu
        </label>
        <div className="space-y-2 max-h-48 overflow-y-auto">
          <label className="flex items-center cursor-pointer">
            <input
              type="radio"
              name="brand"
              checked={filters.brand === ''}
              onChange={() => handleChange('brand', '')}
              className="w-4 h-4 text-primary-500 focus:ring-primary-500"
            />
            <span className="ml-2 text-sm text-gray-700">Tất cả</span>
          </label>
          {brands.map((brand, index) => (
            <label key={index} className="flex items-center cursor-pointer">
              <input
                type="radio"
                name="brand"
                checked={filters.brand === brand}
                onChange={() => handleChange('brand', brand)}
                className="w-4 h-4 text-primary-500 focus:ring-primary-500"
              />
              <span className="ml-2 text-sm text-gray-700">{brand}</span>
            </label>
          ))}
        </div>
      </div>
    </div>
  );

  if (loading) {
    return (
      <div className="bg-white rounded-lg shadow p-6">
        <div className="animate-pulse space-y-4">
          <div className="h-4 bg-gray-200 rounded w-3/4"></div>
          <div className="h-10 bg-gray-200 rounded"></div>
          <div className="h-4 bg-gray-200 rounded w-3/4"></div>
          <div className="h-10 bg-gray-200 rounded"></div>
        </div>
      </div>
    );
  }

  return (
    <>
      {/* Desktop Filter */}
      <div className="hidden lg:block bg-white rounded-lg shadow p-6">
        <h2 className="text-lg font-bold text-gray-900 mb-6">Bộ lọc</h2>
        <FilterContent />
      </div>

      {/* Mobile Filter Button */}
      <div className="lg:hidden fixed bottom-4 right-4 z-30">
        <button
          onClick={() => setShowMobileFilters(true)}
          className="bg-primary-500 text-white p-4 rounded-full shadow-lg hover:bg-primary-600 transition-colors"
        >
          <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 6V4m0 2a2 2 0 100 4m0-4a2 2 0 110 4m-6 8a2 2 0 100-4m0 4a2 2 0 110-4m0 4v2m0-6V4m6 6v10m6-2a2 2 0 100-4m0 4a2 2 0 110-4m0 4v2m0-6V4" />
          </svg>
        </button>
      </div>

      {/* Mobile Filter Drawer */}
      {showMobileFilters && (
        <div className="lg:hidden fixed inset-0 z-50 overflow-hidden">
          <div className="absolute inset-0 overflow-hidden">
            {/* Overlay */}
            <div
              className="absolute inset-0 bg-gray-500 bg-opacity-75 transition-opacity"
              onClick={() => setShowMobileFilters(false)}
            ></div>

            {/* Drawer */}
            <div className="absolute inset-y-0 right-0 max-w-full flex">
              <div className="w-screen max-w-sm">
                <div className="h-full flex flex-col bg-white shadow-xl">
                  {/* Header */}
                  <div className="px-6 py-4 border-b">
                    <div className="flex items-center justify-between">
                      <h2 className="text-lg font-bold text-gray-900">Bộ lọc</h2>
                      <button
                        onClick={() => setShowMobileFilters(false)}
                        className="text-gray-400 hover:text-gray-500"
                      >
                        <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
                        </svg>
                      </button>
                    </div>
                  </div>

                  {/* Content */}
                  <div className="flex-1 overflow-y-auto p-6">
                    <FilterContent />
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      )}
    </>
  );
};

export default ProductFilter;