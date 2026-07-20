import { useEffect, useState } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { momoPaymentAPI } from '../services/api';
import LoadingSpinner from '../components/common/LoadingSpinner';

const PaymentResult = () => {
  const [searchParams] = useSearchParams();
  const navigate = useNavigate();
  const [status, setStatus] = useState('processing'); 
  const [message, setMessage] = useState('Đang xử lý kết quả...');

  useEffect(() => {
    const checkResult = async () => {
      const resultCode = searchParams.get('resultCode');
      const orderId = searchParams.get('orderId');
      const transId = searchParams.get('transId');
      const orderInfo = searchParams.get('message');

      if (resultCode === 'COD') {
          setStatus('success');
          setMessage('Đặt hàng thành công!');
          return;
      }

      if (!orderId || !resultCode) {
        setStatus('failed');
        setMessage('Thông tin phản hồi không hợp lệ.');
        return;
      }

      if (resultCode !== '0') {
        setStatus('failed');
        setMessage(decodeURI(orderInfo) || 'Giao dịch bị từ chối hoặc hủy bỏ.');
        return;
      }

      try {
        await momoPaymentAPI.confirmPayment({
          orderId: orderId,
          resultCode: resultCode,
          transId: transId,
          message: orderInfo
        });
        setStatus('success');
        setMessage('Thanh toán thành công! Đơn hàng của bạn đã được ghi nhận.');
      } catch (error) {
        console.error('Lỗi confirm payment:', error);
        setStatus('warning'); 
        setMessage('Giao dịch ghi nhận thành công phía MoMo, nhưng hệ thống đang cập nhật chậm. Vui lòng kiểm tra lại đơn hàng sau ít phút.');
      }
    };

    checkResult();
  }, [searchParams]);

  return (
    <div className="container-custom py-16 min-h-[60vh] flex items-center justify-center">
      <div className="max-w-md w-full bg-white shadow-lg rounded-lg p-8 text-center">
        
        {status === 'processing' && (
          <div className="flex flex-col items-center">
            <LoadingSpinner />
            <p className="mt-4 text-gray-600 text-lg">Đang xử lý...</p>
          </div>
        )}

        {status === 'success' && (
          <div className="flex flex-col items-center">
            <div className="w-20 h-20 bg-green-100 rounded-full flex items-center justify-center mb-4">
              <svg className="w-10 h-10 text-green-500" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M5 13l4 4L19 7"></path>
              </svg>
            </div>
            <h2 className="text-2xl font-bold text-gray-800 mb-2">
                {/* Thay đổi tiêu đề tùy vào việc có phải COD không */}
                {searchParams.get('resultCode') === 'COD' ? 'Đặt hàng thành công!' : 'Thanh toán thành công!'}
            </h2>
            <p className="text-gray-600 mb-6">{message}</p>
            <div className="space-y-3 w-full">
              <button onClick={() => navigate('/account?tab=orders')} className="w-full btn-primary">
                Xem đơn hàng của tôi
              </button>
              <button onClick={() => navigate('/')} className="w-full btn-outline">
                Về trang chủ
              </button>
            </div>
          </div>
        )}

        {(status === 'failed' || status === 'warning') && (
          <div className="flex flex-col items-center">
             <div className={`w-20 h-20 rounded-full flex items-center justify-center mb-4 ${status === 'warning' ? 'bg-yellow-100' : 'bg-red-100'}`}>
              <svg className={`w-10 h-10 ${status === 'warning' ? 'text-yellow-500' : 'text-red-500'}`} fill="none" stroke="currentColor" viewBox="0 0 24 24">
                <path strokeLinecap="round" strokeLinejoin="round" strokeWidth="2" d="M6 18L18 6M6 6l12 12"></path>
              </svg>
            </div>
            <h2 className="text-2xl font-bold text-gray-800 mb-2">
                {status === 'warning' ? 'Đang cập nhật trạng thái' : 'Thanh toán thất bại'}
            </h2>
            <p className="text-gray-600 mb-6">{message}</p>
            <button onClick={() => navigate('/checkout')} className="w-full btn-primary">
              Thử thanh toán lại
            </button>
            <button onClick={() => navigate('/')} className="w-full mt-3 text-gray-500 hover:text-primary-500">
                Về trang chủ
            </button>
          </div>
        )}
      </div>
    </div>
  );
};

export default PaymentResult;