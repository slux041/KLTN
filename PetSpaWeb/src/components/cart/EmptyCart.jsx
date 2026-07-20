import { Link } from 'react-router-dom';

const EmptyCart = () => {
  return (
    <div className="bg-gray-50 min-h-[60vh] flex items-center justify-center py-12">
      <div className="text-center">
        <div className="mb-8">
          <svg
            className="w-32 h-32 mx-auto text-gray-300"
            fill="none"
            stroke="currentColor"
            viewBox="0 0 24 24"
          >
            <path
              strokeLinecap="round"
              strokeLinejoin="round"
              strokeWidth={1.5}
              d="M3 3h2l.4 2M7 13h10l4-8H5.4M7 13L5.4 5M7 13l-2.293 2.293c-.63.63-.184 1.707.707 1.707H17m0 0a2 2 0 100 4 2 2 0 000-4zm-8 2a2 2 0 11-4 0 2 2 0 014 0z"
            />
          </svg>
        </div>
        <h2 className="text-2xl font-bold text-gray-900 mb-4">
          Giỏ hàng của bạn đang trống
        </h2>
        <p className="text-gray-600 mb-8 max-w-md mx-auto">
          Hãy thêm những sản phẩm yêu thích vào giỏ hàng để bắt đầu mua sắm nhé!
        </p>
        <div className="flex flex-col sm:flex-row gap-4 justify-center">
          <Link to="/products" className="btn-primary">
            Khám phá sản phẩm
          </Link>
          <Link to="/service" className="btn-outline">
            Đặt lịch Spa
          </Link>
        </div>
      </div>
    </div>
  );
};

export default EmptyCart;