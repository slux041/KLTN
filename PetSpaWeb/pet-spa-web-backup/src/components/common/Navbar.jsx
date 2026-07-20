import { useState, useEffect } from 'react';
import { Link, useLocation } from 'react-router-dom';
import { categoryAPI } from '../../services/api';

const Navbar = () => {
  const location = useLocation();
  const [categories, setCategories] = useState([]);
  const [showCategories, setShowCategories] = useState(false);

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

  const hideOnPages = ['/login', '/register'];
  if (hideOnPages.includes(location.pathname)) {
    return null;
  }

  return (
    <nav className="bg-primary-500 shadow-sm sticky top-[112px] z-30">
      <div className="container-custom">
        <div className="flex items-center justify-center">
          <ul className="flex items-center gap-1 overflow-visible">
            {navLinks.map((link) => (
              <li key={link.path} className="relative group"
                onMouseEnter={() => link.hasDropdown && setShowCategories(true)}
                onMouseLeave={() => link.hasDropdown && setShowCategories(false)}
              >
                <Link
                  to={link.path}
                  onClick={(e) => {
                    e.preventDefault();
                    window.location.href = link.path;
                  }}
                  className={`block px-9 py-4 text-sm font-medium transition-colors whitespace-nowrap ${isActive(link.path)
                    ? 'text-white bg-primary-600'
                    : 'text-white hover:bg-primary-600'
                    }`}
                >
                  {link.label}
                  {link.hasDropdown && (
                    <svg className="w-4 h-4 inline-block ml-1" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 9l-7 7-7-7" />
                    </svg>
                  )}
                </Link>

                {/* Dropdown */}
                {link.hasDropdown && (
                  <div className={`absolute left-0 top-full w-48 bg-white shadow-lg rounded-b-lg py-2 transition-all duration-200 transform origin-top z-50 ${showCategories ? 'opacity-100 scale-y-100 visible' : 'opacity-0 scale-y-0 invisible'
                    }`}>
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
              </li>
            ))}
          </ul>
        </div>
      </div>
    </nav>
  );
};

export default Navbar;