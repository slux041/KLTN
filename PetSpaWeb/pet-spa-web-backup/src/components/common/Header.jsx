import { useState, useEffect, useRef } from 'react';
import { Link, useNavigate, useLocation } from 'react-router-dom';
import { useAuth } from '../../contexts/AuthContext';
import { useCart } from '../../contexts/CartContext';
import { categoryAPI } from '../../services/api';
import { IMAGE_PATHS } from '../../utils/constants';
import SearchBar from './SearchBar';

const Header = () => {
  const { isAuthenticated, user, logout } = useAuth();
  const { cartCount } = useCart();
  const navigate = useNavigate();
  const location = useLocation();
  const [showUserMenu, setShowUserMenu] = useState(false);
  const [categories, setCategories] = useState([]);
  const [showCategories, setShowCategories] = useState(false);
  const userMenuRef = useRef(null);
  const hideMenuTimeoutRef = useRef(null);

  // Fetch categories for dropdown
  useEffect(() => {
    fetchCategories();
  }, []);

  const fetchCategories = async () => {
    try {
      const response = await categoryAPI.getAll({ type: 'product', isActive: true });
      if (response.data.success) {
        setCategories(response.data.data || []);
      }
    } catch (error) {
      console.error('Fetch categories error:', error);
    }
  };

  const handleLogout = () => {
    logout();
    navigate('/login');
  };

  const handleMouseEnterUser = () => {
    if (hideMenuTimeoutRef.current) {
      clearTimeout(hideMenuTimeoutRef.current);
    }
    setShowUserMenu(true);
  };

  const handleMouseLeaveUser = () => {
    hideMenuTimeoutRef.current = setTimeout(() => {
      setShowUserMenu(false);
    }, 200);
  };

  // Hide header on login/register pages
  const hideOnPages = ['/login', '/register'];
  if (hideOnPages.includes(location.pathname)) {
    return null;
  }

  const navLinks = [
    { path: '/', label: 'Trang chủ' },
    {
      path: '/products',
      label: 'Sản phẩm',
      hasDropdown: true
    },
    { path: '/service', label: 'Dịch vụ Spa' },
    { path: '/contact', label: 'Liên hệ' }
  ];

  const isActive = (path) => {
    if (path === '/') {
      return location.pathname === '/';
    }
    return location.pathname.startsWith(path);
  };

  return (
    <header className="bg-white shadow-md sticky top-0 z-40">
      <div className="container-custom">
        <div className="flex items-center justify-between py-4 gap-8">
          {/* Logo */}
          <Link to="/" className="flex items-center flex-shrink-0" onClick={() => window.location.href = '/'}>
            <img
              src={IMAGE_PATHS.logo}
              alt="Pet Spa Logo"
              className="h-16 w-auto object-contain"
              onError={(e) => {
                e.target.src = 'https://via.placeholder.com/150x50?text=Pet+Spa';
              }}
            />
          </Link>

          {/* Navigation Links - Desktop */}
          <nav className="hidden lg:flex items-center gap-1">
            {navLinks.map((link) => (
              <div
                key={link.path}
                className="relative"
                onMouseEnter={() => link.hasDropdown && setShowCategories(true)}
                onMouseLeave={() => link.hasDropdown && setShowCategories(false)}
              >
                <Link
                  to={link.path}
                  onClick={(e) => {
                    if (!link.hasDropdown) {
                      e.preventDefault();
                      window.location.href = link.path;
                    }
                  }}
                  className={`px-4 py-2 text-sm font-medium transition-colors whitespace-nowrap rounded-md ${isActive(link.path)
                    ? 'text-primary-500 bg-primary-50'
                    : 'text-gray-700 hover:text-primary-500 hover:bg-gray-50'
                    }`}
                >
                  {link.label}
                  {link.hasDropdown && (
                    <svg className="w-4 h-4 inline-block ml-1" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 9l-7 7-7-7" />
                    </svg>
                  )}
                </Link>

                {/* Dropdown for Products */}
                {link.hasDropdown && (
                  <div
                    className={`absolute left-0 top-full mt-1 w-48 bg-white shadow-lg rounded-lg py-2 transition-all duration-200 transform origin-top ${showCategories ? 'opacity-100 scale-y-100 visible' : 'opacity-0 scale-y-0 invisible'
                      }`}
                  >
                    {categories.map((category) => (
                      <Link
                        key={category.categoryId}
                        to={`/products?categoryId=${category.categoryId}`}
                        onClick={(e) => {
                          e.preventDefault();
                          window.location.href = `/products?categoryId=${category.categoryId}`;
                        }}
                        className="block px-4 py-2 text-sm text-gray-700 hover:bg-primary-50 hover:text-primary-600"
                      >
                        {category.name}
                      </Link>
                    ))}
                  </div>
                )}
              </div>
            ))}
          </nav>

          {/* Search Bar - Desktop */}
          <div className="hidden md:block flex-1 max-w-md">
            <SearchBar />
          </div>

          {/* Actions */}
          <div className="flex items-center gap-2">
            {/* Cart */}
            <Link
              to="/cart"
              className="relative flex items-center gap-2 p-2 rounded-lg transition-colors"
            >
              <div className="relative">
                <svg
                  className="w-6 h-6 text-gray-700"
                  fill="none"
                  stroke="currentColor"
                  viewBox="0 0 24 24"
                >
                  <path
                    strokeLinecap="round"
                    strokeLinejoin="round"
                    strokeWidth={2}
                    d="M3 3h2l.4 2M7 13h10l4-8H5.4M7 13L5.4 5M7 13l-2.293 2.293c-.63.63-.184 1.707.707 1.707H17m0 0a2 2 0 100 4 2 2 0 000-4zm-8 2a2 2 0 11-4 0 2 2 0 014 0z"
                  />
                </svg>
                {cartCount > 0 && (
                  <span className="absolute -top-1 -right-1 bg-primary-500 text-white text-xs font-bold rounded-full w-5 h-5 flex items-center justify-center">
                    {cartCount > 99 ? '99+' : cartCount}
                  </span>
                )}
              </div>
            </Link>

            {/* User Menu */}
            {isAuthenticated ? (
              <div
                className="relative"
                ref={userMenuRef}
                onMouseEnter={handleMouseEnterUser}
                onMouseLeave={handleMouseLeaveUser}
              >
                <Link
                  to="/account"
                  className="flex items-center gap-2 p-2 rounded-lg transition-colors hover:bg-gray-100 cursor-pointer"
                >
                  {/* Default User Icon */}
                  <div className="w-8 h-8 rounded-full bg-gray-200 flex items-center justify-center">
                    <svg className="w-5 h-5 text-gray-600" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
                    </svg>
                  </div>
                  <span className="hidden md:block text-sm font-medium text-gray-900">
                    {user?.fullName}
                  </span>
                </Link>
                {/* Dropdown Menu */}
                {showUserMenu && (
                  <div
                    className="absolute right-0 mt-2 w-56 bg-white rounded-lg shadow-lg py-2 border border-gray-200"
                    onMouseEnter={handleMouseEnterUser}
                    onMouseLeave={handleMouseLeaveUser}
                  >
                    <Link
                      to="/account"
                      className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                      onClick={() => setShowUserMenu(false)}
                    >
                      <div className="flex items-center gap-2">
                        <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M16 7a4 4 0 11-8 0 4 4 0 018 0zM12 14a7 7 0 00-7 7h14a7 7 0 00-7-7z" />
                        </svg>
                        Tài khoản của tôi
                      </div>
                    </Link>
                    <Link
                      to="/account?tab=orders"
                      className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                      onClick={() => setShowUserMenu(false)}
                    >
                      <div className="flex items-center gap-2">
                        <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M16 11V7a4 4 0 00-8 0v4M5 9h14l1 12H4L5 9z" />
                        </svg>
                        Đơn hàng
                      </div>
                    </Link>
                    <Link
                      to="/account?tab=appointments"
                      className="block px-4 py-2 text-sm text-gray-700 hover:bg-gray-100"
                      onClick={() => setShowUserMenu(false)}
                    >
                      <div className="flex items-center gap-2">
                        <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
                        </svg>
                        Lịch hẹn Spa
                      </div>
                    </Link>
                    <hr className="my-2" />
                    <button
                      onClick={handleLogout}
                      className="block w-full text-left px-4 py-2 text-sm text-red-600 hover:bg-red-50"
                    >
                      <div className="flex items-center gap-2">
                        <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                          <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M17 16l4-4m0 0l-4-4m4 4H7m6 4v1a3 3 0 01-3 3H6a3 3 0 01-3-3V7a3 3 0 013-3h4a3 3 0 013 3v1" />
                        </svg>
                        Đăng xuất
                      </div>
                    </button>
                  </div>
                )}
              </div>
            ) : (
              <div className="flex items-center gap-2">
                <Link
                  to="/login"
                  className="px-4 py-2 text-sm font-medium text-gray-700 hover:text-primary-500 transition-colors"
                >
                  Đăng nhập
                </Link>
                <Link
                  to="/register"
                  className="px-4 py-2 text-sm font-medium bg-primary-500 text-white rounded-lg hover:bg-primary-600 transition-colors"
                >
                  Đăng ký
                </Link>
              </div>
            )}
          </div>
        </div>

        {/* Search Bar - Mobile */}
        <div className="md:hidden pb-4">
          <SearchBar />
        </div>
      </div>
    </header>
  );
};

export default Header;