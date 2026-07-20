import { useState, useEffect } from 'react';
import { orderAPI } from '../../services/api';
import { formatCurrency, formatDateTime } from '../../utils/helpers';
import LoadingSpinner from '../common/LoadingSpinner';

const OrdersTab = () => {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);
  const [selectedOrder, setSelectedOrder] = useState(null);
  const [showDetailModal, setShowDetailModal] = useState(false);

  useEffect(() => {
    fetchOrders();
  }, []);

  const fetchOrders = async () => {
    try {
      setLoading(true);
      const response = await orderAPI.getAll();

      if (response.data.success) {
        const sortedOrders = (response.data.data || []).sort(
          (a, b) => new Date(b.createdAt) - new Date(a.createdAt)
        );
        setOrders(sortedOrders);
      }
    } catch (error) {
      console.error('Fetch orders error:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleViewDetail = async (orderId) => {
    try {
      const response = await orderAPI.getById(orderId);

      if (response.data.success) {
        setSelectedOrder(response.data.data);
        setShowDetailModal(true);
      }
    } catch (error) {
      console.error('Fetch order detail error:', error);
      alert('Không thể tải chi tiết đơn hàng');
    }
  };

  const getStatusBadge = (status) => {
    if (!status) return { label: 'Không rõ', className: 'bg-gray-100 text-gray-800' };

    const lowerStatus = status.toLowerCase();

    switch (lowerStatus) {
      case 'pending':
        return { label: 'Chờ xử lý', className: 'bg-yellow-100 text-yellow-800' };
      case 'confirmed':
        return { label: 'Đã xác nhận', className: 'bg-blue-100 text-blue-800' };
      case 'shipping':
        return { label: 'Đang giao hàng', className: 'bg-purple-100 text-purple-800' };
      case 'completed':
        return { label: 'Hoàn thành', className: 'bg-green-100 text-green-800' };
      case 'canceled':
        return { label: 'Đã hủy', className: 'bg-red-100 text-red-800' };
      case 'paid':
        return { label: 'Đã thanh toán', className: 'bg-green-100 text-green-800' };
      case 'failed':
        return { label: 'Thất bại', className: 'bg-red-100 text-red-800' };
      default:
        return { label: status, className: 'bg-gray-100 text-gray-800' };
    }
  };

  const getPaymentMethodName = (method) => {
    if (!method) return '';
    const lowerMethod = method.toLowerCase();
    return lowerMethod === 'cod' ? 'Thanh toán khi nhận hàng (COD)' : 'Chuyển khoản ngân hàng';
  };

  if (loading) {
    return <LoadingSpinner />;
  }

  return (
    <div className="bg-white rounded-lg shadow p-6">
      <h2 className="text-2xl font-bold text-gray-900 mb-6">Lịch sử đơn hàng</h2>

      {orders.length === 0 ? (
        <div className="text-center py-12">
          <svg className="w-16 h-16 text-gray-400 mx-auto mb-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M16 11V7a4 4 0 00-8 0v4M5 9h14l1 12H4L5 9z" />
          </svg>
          <p className="text-gray-600 mb-4">Bạn chưa có đơn hàng nào</p>
          <a href="/products" className="btn-primary inline-block">
            Mua sắm ngay
          </a>
        </div>
      ) : (
        <div className="space-y-4">
          {orders.map((order) => {
            const status = getStatusBadge(order.orderStatus);
            const totalQuantity = (order.items || []).reduce((sum, item) => sum + item.quantity, 0);

            return (
              <div key={order.orderId} className="border border-gray-200 rounded-lg p-4 hover:shadow-md transition-shadow">
                <div className="flex flex-col md:flex-row md:items-center justify-between gap-4 mb-4">
                  <div>
                    <div className="flex items-center gap-3 mb-2">
                      <h3 className="font-semibold text-gray-900">
                        Đơn hàng #{order.orderId}
                      </h3>
                      <span className={`px-2 py-1 text-xs font-semibold rounded ${status.className}`}>
                        {status.label}
                      </span>
                    </div>
                    <p className="text-sm text-gray-600">
                      Ngày đặt: {formatDateTime(order.createdAt)}
                    </p>
                    <p className="text-sm text-gray-600 mt-1">
                      Địa chỉ: {order.shippingFullAddress}
                    </p>
                  </div>
                  <div className="text-right">
                    <p className="text-sm text-gray-600 mb-1">Tổng tiền ({totalQuantity} sản phẩm)</p>
                    <p className="text-xl font-bold text-primary-500">
                      {formatCurrency(order.totalAmount)}
                    </p>
                  </div>
                </div>

                {/* Order Items Preview */}
                <div className="border-t pt-4 mb-4">
                  <div className="space-y-2">
                    {(order.items || []).slice(0, 2).map((item, index) => (
                      <div key={index} className="flex items-center gap-3 text-sm">
                        <div className="flex-1">
                          <p className="text-gray-900">{item.productName || item.serviceName}</p>
                        </div>
                        <p className="text-gray-600">x{item.quantity}</p>
                        <p className="font-semibold text-gray-900">{formatCurrency(item.totalPrice)}</p>
                      </div>
                    ))}
                    {order.items && order.items.length > 2 && (
                      <p className="text-sm text-gray-500">
                        Và {order.items.length - 2} sản phẩm khác...
                      </p>
                    )}
                  </div>
                </div>

                {/* Actions */}
                <div className="flex gap-2">
                  <button
                    onClick={() => handleViewDetail(order.orderId)}
                    className="flex-1 btn-outline text-sm py-2"
                  >
                    Xem chi tiết
                  </button>
                  {(order.orderStatus?.toLowerCase() === 'delivered' || order.orderStatus?.toLowerCase() === 'completed') && (
                    <button className="flex-1 btn-primary text-sm py-2">
                      Mua lại
                    </button>
                  )}
                </div>
              </div>
            );
          })}
        </div>
      )}

      {/* Order Detail Modal */}
      {showDetailModal && selectedOrder && (
        <div className="fixed inset-0 z-50 overflow-y-auto">
          <div className="flex items-center justify-center min-h-screen px-4 pt-4 pb-20 text-center sm:block sm:p-0">
            {/* Overlay */}
            <div className="fixed inset-0 transition-opacity" aria-hidden="true">
              <div className="absolute inset-0 bg-gray-500 opacity-75" onClick={() => setShowDetailModal(false)}></div>
            </div>

            <span className="hidden sm:inline-block sm:align-middle sm:h-screen" aria-hidden="true">&#8203;</span>

            <div className="relative z-10 inline-block align-bottom bg-white rounded-lg text-left overflow-hidden shadow-xl transform transition-all sm:my-8 sm:align-middle sm:max-w-3xl sm:w-full">

              <div className="bg-white px-4 pt-5 pb-4 sm:p-6 sm:pb-4">
                {/* Header */}
                <div className="flex items-center justify-between mb-6">
                  <h3 className="text-2xl font-bold text-gray-900">
                    Chi tiết đơn hàng #{selectedOrder.orderId}
                  </h3>
                  <button
                    onClick={() => setShowDetailModal(false)}
                    className="text-gray-400 hover:text-gray-600"
                  >
                    <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
                    </svg>
                  </button>
                </div>

                {/* Status Badges */}
                <div className="flex gap-2 mb-6">
                  {(() => {
                    const status = getStatusBadge(selectedOrder.orderStatus);
                    return (
                      <span className={`px-3 py-1 text-sm font-semibold rounded ${status.className}`}>
                        {status.label}
                      </span>
                    );
                  })()}
                </div>

                {/* Order Info */}
                <div className="grid grid-cols-1 md:grid-cols-2 gap-6 mb-6">
                  <div>
                    <h4 className="font-semibold text-gray-900 mb-3">Thông tin đơn hàng</h4>
                    <div className="space-y-2 text-sm">
                      <p className="text-gray-600">
                        Ngày đặt: <span className="text-gray-900">{formatDateTime(selectedOrder.createdAt)}</span>
                      </p>

                      {/* Payment Info */}
                      <div className="flex flex-col gap-1">
                        <p className="text-gray-600">
                          Phương thức: <span className="text-gray-900">{getPaymentMethodName(selectedOrder.paymentMethod)}</span>
                        </p>
                        <div className="flex items-center gap-2">
                          <span className="text-gray-600">Trạng thái thanh toán:</span>
                          {(() => {
                            const paymentStatus = getStatusBadge(selectedOrder.paymentStatus);
                            return (
                              <span className={`inline-block px-2 py-0.5 text-xs rounded ${paymentStatus.className}`}>
                                {paymentStatus.label}
                              </span>
                            );
                          })()}
                        </div>
                      </div>

                      {selectedOrder.promotionCode && (
                        <p className="text-gray-600">
                          Mã giảm giá: <span className="text-green-600 font-medium">{selectedOrder.promotionCode}</span>
                        </p>
                      )}
                    </div>
                  </div>

                  <div>
                    <h4 className="font-semibold text-gray-900 mb-3">Địa chỉ giao hàng</h4>
                    <div className="space-y-1 text-sm">
                      <p className="font-medium text-gray-900">{selectedOrder.shippingFullName}</p>
                      <p className="text-gray-600">{selectedOrder.shippingPhone}</p>
                      <p className="text-gray-600">{selectedOrder.shippingFullAddress}</p>
                    </div>
                  </div>
                </div>

                {/* Order Items */}
                <div className="mb-6">
                  <h4 className="font-semibold text-gray-900 mb-3">Sản phẩm đã đặt</h4>
                  <div className="border border-gray-200 rounded-lg divide-y">
                    {selectedOrder.items.map((item, index) => (
                      <div key={index} className="p-4 flex items-center gap-4">
                        <div className="flex-1">
                          <p className="font-medium text-gray-900">{item.productName || item.serviceName}</p>
                          <p className="text-sm text-gray-600">{formatCurrency(item.price)} x {item.quantity}</p>
                        </div>
                        <p className="font-semibold text-gray-900">{formatCurrency(item.totalPrice)}</p>
                      </div>
                    ))}
                  </div>
                </div>

                {/* Order Summary */}
                <div className="bg-gray-50 rounded-lg p-4 space-y-2">
                  <div className="flex justify-between text-sm">
                    <span className="text-gray-600">Tạm tính</span>
                    <span className="text-gray-900">{formatCurrency(selectedOrder.subtotal)}</span>
                  </div>
                  {selectedOrder.discountAmount > 0 && (
                    <div className="flex justify-between text-sm">
                      <span className="text-gray-600">Giảm giá</span>
                      <span className="text-green-600">-{formatCurrency(selectedOrder.discountAmount)}</span>
                    </div>
                  )}
                  <div className="flex justify-between text-sm">
                    <span className="text-gray-600">Phí vận chuyển</span>
                    <span className="text-gray-900">{formatCurrency(selectedOrder.shippingFee)}</span>
                  </div>
                  <div className="border-t pt-2 flex justify-between">
                    <span className="font-semibold text-gray-900">Tổng cộng</span>
                    <span className="font-bold text-xl text-primary-500">{formatCurrency(selectedOrder.totalAmount)}</span>
                  </div>
                </div>

                {/* Note */}
                {selectedOrder.note && (
                  <div className="mt-6">
                    <h4 className="font-semibold text-gray-900 mb-2">Ghi chú</h4>
                    <p className="text-sm text-gray-600 bg-gray-50 p-3 rounded">{selectedOrder.note}</p>
                  </div>
                )}

                {/* Close Button */}
                <div className="mt-6">
                  <button
                    onClick={() => setShowDetailModal(false)}
                    className="w-full btn-secondary"
                  >
                    Đóng
                  </button>
                </div>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default OrdersTab;