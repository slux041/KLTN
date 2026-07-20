import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useCart } from '../contexts/CartContext';
import { useAuth } from '../contexts/AuthContext';
import { customerAddressAPI, addressAPI, promotionAPI, orderAPI } from '../services/api';
import { formatCurrency } from '../utils/helpers';
import { PAYMENT_METHODS, MESSAGES } from '../utils/constants';
import LoadingSpinner from '../components/common/LoadingSpinner';
import CartSummary from '../components/cart/CartSummary';

const CheckoutPage = () => {
  const navigate = useNavigate();
  const { isAuthenticated } = useAuth();
  const { cart, getCartTotals, clearCart } = useCart();

  const [addresses, setAddresses] = useState([]);
  const [selectedAddressId, setSelectedAddressId] = useState(null);
  const [showAddressModal, setShowAddressModal] = useState(false);
  const [showAddAddressModal, setShowAddAddressModal] = useState(false);

  const [paymentMethod, setPaymentMethod] = useState(PAYMENT_METHODS.COD);
  const [note, setNote] = useState('');
  const [promotionCode, setPromotionCode] = useState('');
  const [appliedPromotion, setAppliedPromotion] = useState(null);
  const [validatingPromotion, setValidatingPromotion] = useState(false);

  const [loading, setLoading] = useState(true);
  const [submitting, setSubmitting] = useState(false);
  const [error, setError] = useState(null);
  const [message, setMessage] = useState({ type: '', text: '' });

  // New address form
  const [newAddress, setNewAddress] = useState({
    fullName: '',
    phone: '',
    addressLine: '',
    provinceId: '',
    provinceName: '',
    wardId: '',
    wardName: '',
    isDefault: false
  });

  const [provinces, setProvinces] = useState([]);
  const [wards, setWards] = useState([]);

  useEffect(() => {
    if (!isAuthenticated) {
      navigate('/login', { state: { from: { pathname: '/checkout' } } });
      return;
    }

    if (!cart || !cart.items || cart.items.length === 0) {
      navigate('/cart');
      return;
    }

    fetchAddresses();
    fetchProvinces();
  }, [isAuthenticated, cart, navigate]);

  const fetchAddresses = async () => {
    try {
      setLoading(true);
      const response = await customerAddressAPI.getAll();

      if (response.data.success) {
        const addressList = response.data.data || [];
        setAddresses(addressList);

        // Set default address
        const defaultAddr = addressList.find(addr => addr.isDefault);
        if (defaultAddr) {
          setSelectedAddressId(defaultAddr.addressId);
        } else if (addressList.length > 0) {
          setSelectedAddressId(addressList[0].addressId);
        }
      }
    } catch (err) {
      console.error('Fetch addresses error:', err);
      setError('Không thể tải danh sách địa chỉ');
    } finally {
      setLoading(false);
    }
  };

  const fetchProvinces = async () => {
    try {
      const response = await addressAPI.getProvinces();
      if (response.data.success) {
        setProvinces(response.data.data || []);
      }
    } catch (err) {
      console.error('Fetch provinces error:', err);
    }
  };

  const fetchWards = async (provinceId) => {
    try {
      const response = await addressAPI.getWards(provinceId);
      if (response.data.success) {
        setWards(response.data.data || []);
      }
    } catch (err) {
      console.error('Fetch wards error:', err);
    }
  };

  const handleProvinceChange = (e) => {
    const selectedProvince = provinces.find(p => p.id === e.target.value);
    setNewAddress({
      ...newAddress,
      provinceId: e.target.value,
      provinceName: selectedProvince?.name || '',
      wardId: '',
      wardName: ''
    });
    setWards([]);
    if (e.target.value) {
      fetchWards(e.target.value);
    }
  };

  const handleWardChange = (e) => {
    const selectedWard = wards.find(w => w.id === e.target.value);
    setNewAddress({
      ...newAddress,
      wardId: e.target.value,
      wardName: selectedWard?.name || ''
    });
  };

  const handleAddAddress = async (e) => {
    e.preventDefault();

    if (!newAddress.fullName || !newAddress.phone || !newAddress.addressLine ||
      !newAddress.provinceId || !newAddress.wardId) {
      setMessage({ type: 'error', text: 'Vui lòng điền đầy đủ thông tin' });
      return;
    }

    try {
      const response = await customerAddressAPI.create(newAddress);

      if (response.data.success) {
        setMessage({ type: 'success', text: MESSAGES.SUCCESS.ADD_ADDRESS });
        await fetchAddresses();
        setShowAddAddressModal(false);
        setNewAddress({
          fullName: '',
          phone: '',
          addressLine: '',
          provinceId: '',
          provinceName: '',
          wardId: '',
          wardName: '',
          isDefault: false
        });
      } else {
        setMessage({ type: 'error', text: response.data.message || MESSAGES.ERROR.GENERIC });
      }
    } catch (err) {
      console.error('Add address error:', err);
      setMessage({ type: 'error', text: MESSAGES.ERROR.GENERIC });
    }
  };

  const handleValidatePromotion = async () => {
    if (!promotionCode.trim()) return;

    setValidatingPromotion(true);
    setMessage({ type: '', text: '' });

    try {
      const response = await promotionAPI.validate(promotionCode.trim());

      if (response.data.success) {
        const promotion = response.data.data;
        if (promotion.isValid) {
          setAppliedPromotion(promotion);
          setMessage({ type: 'success', text: 'Áp dụng mã giảm giá thành công!' });
        } else {
          setAppliedPromotion(null);
          setMessage({ type: 'error', text: 'Mã giảm giá không hợp lệ hoặc đã hết hạn' });
        }
      } else {
        setAppliedPromotion(null);
        setMessage({ type: 'error', text: response.data.message || 'Mã giảm giá không hợp lệ' });
      }
    } catch (err) {
      console.error('Validate promotion error:', err);
      setAppliedPromotion(null);
      setMessage({ type: 'error', text: 'Không thể áp dụng mã giảm giá' });
    } finally {
      setValidatingPromotion(false);
    }
  };

  const handleRemovePromotion = () => {
    setAppliedPromotion(null);
    setPromotionCode('');
    setMessage({ type: '', text: '' });
  };

  const handleSubmitOrder = async () => {
    if (!selectedAddressId) {
      setMessage({ type: 'error', text: MESSAGES.ERROR.ADDRESS_REQUIRED });
      return;
    }

    const selectedAddress = addresses.find(addr => addr.addressId === selectedAddressId);
    if (!selectedAddress) {
      setMessage({ type: 'error', text: 'Địa chỉ không hợp lệ' });
      return;
    }

    setSubmitting(true);
    setMessage({ type: '', text: '' });

    try {
      const orderData = {
        paymentMethod,
        shippingAddressId: selectedAddressId,
        shippingFullName: selectedAddress.fullName,
        shippingPhone: selectedAddress.phone,
        shippingAddressLine: selectedAddress.addressLine,
        shippingProvinceId: selectedAddress.provinceId,
        shippingProvinceName: selectedAddress.provinceName,
        shippingWardId: selectedAddress.wardId,
        shippingWardName: selectedAddress.wardName,
        promotionCode: appliedPromotion ? promotionCode.trim() : null,
        note: note.trim() || null,
        items: cart.items.map(item => ({
          productId: item.productId,
          serviceId: item.serviceId,
          quantity: item.quantity
        }))
      };

      const response = await orderAPI.create(orderData);

      if (response.data.success) {
        await clearCart();
        navigate('/account?tab=orders', {
          state: { message: MESSAGES.SUCCESS.ORDER_SUCCESS }
        });
      } else {
        setMessage({ type: 'error', text: response.data.message || MESSAGES.ERROR.GENERIC });
      }
    } catch (err) {
      console.error('Create order error:', err);
      setMessage({ type: 'error', text: err.response?.data?.message || MESSAGES.ERROR.GENERIC });
    } finally {
      setSubmitting(false);
    }
  };

  if (loading) {
    return <LoadingSpinner fullScreen />;
  }

  const selectedAddress = addresses.find(addr => addr.addressId === selectedAddressId);
  const discountAmount = appliedPromotion
    ? (cart.totalAmount * appliedPromotion.discountPercent) / 100
    : 0;
  const totals = getCartTotals(discountAmount);

  return (
    <div className="bg-gray-50 py-8">
      <div className="container-custom">
        {/* Header */}
        <h1 className="text-3xl font-bold text-gray-900 mb-6">Thanh toán</h1>

        {/* Message */}
        {message.text && (
          <div className={`mb-6 p-4 rounded-lg ${message.type === 'success' ? 'bg-green-50 text-green-800' : 'bg-red-50 text-red-800'
            }`}>
            {message.text}
          </div>
        )}

        <div className="grid grid-cols-1 lg:grid-cols-3 gap-6">
          {/* Left Column */}
          <div className="lg:col-span-2 space-y-6">
            {/* Shipping Address */}
            <div className="bg-white rounded-lg shadow p-6">
              <div className="flex items-center justify-between mb-4">
                <h2 className="text-xl font-bold text-gray-900">Địa chỉ giao hàng</h2>
                <button
                  onClick={() => setShowAddressModal(true)}
                  className="text-primary-500 hover:text-primary-600 text-sm font-medium"
                >
                  Thay đổi
                </button>
              </div>

              {selectedAddress ? (
                <div className="border border-gray-200 rounded-lg p-4">
                  <div className="flex items-start justify-between">
                    <div>
                      <p className="font-semibold text-gray-900">{selectedAddress.fullName}</p>
                      <p className="text-gray-600 mt-1">{selectedAddress.phone}</p>
                      <p className="text-gray-600 mt-2">{selectedAddress.fullAddress}</p>
                    </div>
                    {selectedAddress.isDefault && (
                      <span className="px-2 py-1 bg-primary-100 text-primary-700 text-xs font-semibold rounded">
                        Mặc định
                      </span>
                    )}
                  </div>
                </div>
              ) : (
                <div className="text-center py-8">
                  <p className="text-gray-600 mb-4">Bạn chưa có địa chỉ giao hàng</p>
                  <button
                    onClick={() => setShowAddAddressModal(true)}
                    className="btn-primary"
                  >
                    Thêm địa chỉ mới
                  </button>
                </div>
              )}
            </div>

            {/* Order Items */}
            <div className="bg-white rounded-lg shadow p-6">
              <h2 className="text-xl font-bold text-gray-900 mb-4">
                Sản phẩm ({cart.totalItems})
              </h2>
              <div className="space-y-4">
                {cart.items.map((item) => (
                  <div key={item.cartItemId} className="flex gap-4 pb-4 border-b last:border-b-0">
                    <img
                      src={item.imageUrl || 'https://via.placeholder.com/80'}
                      alt={item.productName || item.serviceName}
                      className="w-20 h-20 object-cover rounded"
                      onError={(e) => {
                        e.target.src = 'https://via.placeholder.com/80?text=No+Image';
                      }}
                    />
                    <div className="flex-1">
                      <h3 className="font-medium text-gray-900">
                        {item.productName || item.serviceName}
                      </h3>
                      <p className="text-sm text-gray-500 mt-1">
                        {formatCurrency(item.price)} x {item.quantity}
                      </p>
                      <p className="text-sm font-semibold text-primary-500 mt-1">
                        {formatCurrency(item.totalPrice)}
                      </p>
                    </div>
                  </div>
                ))}
              </div>
            </div>

            {/* Payment Method */}
            <div className="bg-white rounded-lg shadow p-6">
              <h2 className="text-xl font-bold text-gray-900 mb-4">Phương thức thanh toán</h2>
              <div className="space-y-3">
                <label className="flex items-center p-4 border-2 rounded-lg cursor-pointer hover:border-primary-500 transition-colors">
                  <input
                    type="radio"
                    name="paymentMethod"
                    value={PAYMENT_METHODS.COD}
                    checked={paymentMethod === PAYMENT_METHODS.COD}
                    onChange={(e) => setPaymentMethod(e.target.value)}
                    className="w-4 h-4 text-primary-500 focus:ring-primary-500"
                  />
                  <div className="ml-3">
                    <p className="font-medium text-gray-900">Thanh toán khi nhận hàng (COD)</p>
                    <p className="text-sm text-gray-500">Thanh toán bằng tiền mặt khi nhận hàng</p>
                  </div>
                </label>
                <label className="flex items-center p-4 border-2 rounded-lg cursor-pointer hover:border-primary-500 transition-colors">
                  <input
                    type="radio"
                    name="paymentMethod"
                    value={PAYMENT_METHODS.BANK_TRANSFER}
                    checked={paymentMethod === PAYMENT_METHODS.BANK_TRANSFER}
                    onChange={(e) => setPaymentMethod(e.target.value)}
                    className="w-4 h-4 text-primary-500 focus:ring-primary-500"
                  />
                  <div className="ml-3">
                    <p className="font-medium text-gray-900">Chuyển khoản ngân hàng</p>
                    <p className="text-sm text-gray-500">Chuyển khoản qua tài khoản ngân hàng</p>
                  </div>
                </label>
              </div>
            </div>

            {/* Note */}
            <div className="bg-white rounded-lg shadow p-6">
              <h2 className="text-xl font-bold text-gray-900 mb-4">Ghi chú đơn hàng</h2>
              <textarea
                value={note}
                onChange={(e) => setNote(e.target.value)}
                rows={4}
                className="input-field resize-none"
                placeholder="Nhập ghi chú cho đơn hàng (không bắt buộc)"
              />
            </div>
          </div>

          {/* Right Column - Summary */}
          <div className="lg:col-span-1">
            <div className="bg-white rounded-lg shadow p-6 sticky top-24">
              <h2 className="text-xl font-bold text-gray-900 mb-6">Tóm tắt đơn hàng</h2>

              {/* Promotion Code */}
              <div className="mb-6">
                <label className="block text-sm font-medium text-gray-700 mb-2">
                  Mã giảm giá
                </label>
                {appliedPromotion ? (
                  <div className="flex items-center justify-between p-3 bg-green-50 border border-green-200 rounded-lg">
                    <div>
                      <p className="font-semibold text-green-800">{promotionCode}</p>
                      <p className="text-sm text-green-600">
                        Giảm {appliedPromotion.discountPercent}%
                      </p>
                    </div>
                    <button
                      onClick={handleRemovePromotion}
                      className="text-red-500 hover:text-red-700"
                    >
                      <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
                      </svg>
                    </button>
                  </div>
                ) : (
                  <div className="flex gap-2">
                    <input
                      type="text"
                      value={promotionCode}
                      onChange={(e) => setPromotionCode(e.target.value)}
                      className="input-field flex-1"
                      placeholder="Nhập mã giảm giá"
                    />
                    <button
                      onClick={handleValidatePromotion}
                      disabled={validatingPromotion || !promotionCode.trim()}
                      className="btn-secondary whitespace-nowrap disabled:opacity-50"
                    >
                      {validatingPromotion ? 'Đang kiểm tra...' : 'Áp dụng'}
                    </button>
                  </div>
                )}
              </div>

              {/* Totals */}
              <div className="space-y-3 mb-6">
                <div className="flex justify-between text-gray-600">
                  <span>Tạm tính</span>
                  <span>{formatCurrency(totals.subtotal)}</span>
                </div>
                {totals.discount > 0 && (
                  <div className="flex justify-between text-green-600">
                    <span>Giảm giá</span>
                    <span>-{formatCurrency(totals.discount)}</span>
                  </div>
                )}
                <div className="flex justify-between text-gray-600">
                  <span>Phí vận chuyển</span>
                  <span>{formatCurrency(totals.shippingFee)}</span>
                </div>
                <hr />
                <div className="flex justify-between text-lg font-bold text-gray-900">
                  <span>Tổng cộng</span>
                  <span className="text-primary-500">{formatCurrency(totals.total)}</span>
                </div>
              </div>

              {/* Submit Button */}
              <button
                onClick={handleSubmitOrder}
                disabled={submitting || !selectedAddressId}
                className="w-full btn-primary disabled:opacity-50 disabled:cursor-not-allowed"
              >
                {submitting ? 'Đang xử lý...' : 'Đặt hàng'}
              </button>

              <p className="text-xs text-gray-500 text-center mt-4">
                Bằng việc đặt hàng, bạn đồng ý với{' '}
                <a href="#" className="text-primary-500 hover:text-primary-600">
                  Điều khoản sử dụng
                </a>
              </p>
            </div>
          </div>
        </div>

        {/* Address Selection Modal */}
        {showAddressModal && (
          <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
            <div className="bg-white rounded-lg max-w-2xl w-full max-h-[80vh] overflow-y-auto">
              <div className="p-6">
                <div className="flex items-center justify-between mb-6">
                  <h3 className="text-xl font-bold text-gray-900">Chọn địa chỉ giao hàng</h3>
                  <button
                    onClick={() => setShowAddressModal(false)}
                    className="text-gray-400 hover:text-gray-600"
                  >
                    <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
                    </svg>
                  </button>
                </div>

                <div className="space-y-3 mb-6">
                  {addresses.map((addr) => (
                    <label
                      key={addr.addressId}
                      className={`block p-4 border-2 rounded-lg cursor-pointer transition-colors ${selectedAddressId === addr.addressId
                        ? 'border-primary-500 bg-primary-50'
                        : 'border-gray-200 hover:border-primary-200'
                        }`}
                    >
                      <input
                        type="radio"
                        name="address"
                        checked={selectedAddressId === addr.addressId}
                        onChange={() => {
                          setSelectedAddressId(addr.addressId);
                          setShowAddressModal(false);
                        }}
                        className="sr-only"
                      />
                      <div className="flex items-start justify-between">
                        <div>
                          <p className="font-semibold text-gray-900">{addr.fullName}</p>
                          <p className="text-gray-600 mt-1">{addr.phone}</p>
                          <p className="text-gray-600 mt-2">{addr.fullAddress}</p>
                        </div>
                        {addr.isDefault && (
                          <span className="px-2 py-1 bg-primary-100 text-primary-700 text-xs font-semibold rounded">
                            Mặc định
                          </span>
                        )}
                      </div>
                    </label>
                  ))}
                </div>

                <button
                  onClick={() => {
                    setShowAddressModal(false);
                    setShowAddAddressModal(true);
                  }}
                  className="w-full btn-outline"
                >
                  Thêm địa chỉ mới
                </button>
              </div>
            </div>
          </div>
        )}

        {/* Add Address Modal */}
        {showAddAddressModal && (
          <div className="fixed inset-0 bg-black bg-opacity-50 flex items-center justify-center z-50 p-4">
            <div className="bg-white rounded-lg max-w-2xl w-full max-h-[80vh] overflow-y-auto">
              <div className="p-6">
                <div className="flex items-center justify-between mb-6">
                  <h3 className="text-xl font-bold text-gray-900">Thêm địa chỉ mới</h3>
                  <button
                    onClick={() => setShowAddAddressModal(false)}
                    className="text-gray-400 hover:text-gray-600"
                  >
                    <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
                    </svg>
                  </button>
                </div>

                <form onSubmit={handleAddAddress} className="space-y-4">
                  <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    <div>
                      <label className="block text-sm font-medium text-gray-700 mb-1">
                        Họ và tên <span className="text-red-500">*</span>
                      </label>
                      <input
                        type="text"
                        value={newAddress.fullName}
                        onChange={(e) => setNewAddress({ ...newAddress, fullName: e.target.value })}
                        className="input-field"
                        required
                      />
                    </div>
                    <div>
                      <label className="block text-sm font-medium text-gray-700 mb-1">
                        Số điện thoại <span className="text-red-500">*</span>
                      </label>
                      <input
                        type="tel"
                        value={newAddress.phone}
                        onChange={(e) => setNewAddress({ ...newAddress, phone: e.target.value })}
                        className="input-field"
                        required
                      />
                    </div>
                  </div>

                  <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    <div>
                      <label className="block text-sm font-medium text-gray-700 mb-1">
                        Tỉnh/Thành phố <span className="text-red-500">*</span>
                      </label>
                      <select
                        value={newAddress.provinceId}
                        onChange={handleProvinceChange}
                        className="input-field"
                        required
                      >
                        <option value="">Chọn tỉnh/thành phố</option>
                        {provinces.map((province) => (
                          <option key={province.id} value={province.id}>
                            {province.name}
                          </option>
                        ))}
                      </select>
                    </div>
                    <div>
                      <label className="block text-sm font-medium text-gray-700 mb-1">
                        Phường/Xã <span className="text-red-500">*</span>
                      </label>
                      <select
                        value={newAddress.wardId}
                        onChange={handleWardChange}
                        className="input-field"
                        required
                        disabled={!newAddress.provinceId}
                      >
                        <option value="">Chọn phường/xã</option>
                        {wards.map((ward) => (
                          <option key={ward.id} value={ward.id}>
                            {ward.name}
                          </option>
                        ))}
                      </select>
                    </div>
                  </div>

                  <div>
                    <label className="block text-sm font-medium text-gray-700 mb-1">
                      Địa chỉ chi tiết <span className="text-red-500">*</span>
                    </label>
                    <textarea
                      value={newAddress.addressLine}
                      onChange={(e) => setNewAddress({ ...newAddress, addressLine: e.target.value })}
                      rows={3}
                      className="input-field resize-none"
                      placeholder="Số nhà, tên đường..."
                      required
                    />
                  </div>

                  <div className="flex items-center">
                    <input
                      type="checkbox"
                      id="isDefault"
                      checked={newAddress.isDefault}
                      onChange={(e) => setNewAddress({ ...newAddress, isDefault: e.target.checked })}
                      className="w-4 h-4 text-primary-500 focus:ring-primary-500 border-gray-300 rounded"
                    />
                    <label htmlFor="isDefault" className="ml-2 text-sm text-gray-700">
                      Đặt làm địa chỉ mặc định
                    </label>
                  </div>

                  <div className="flex gap-3 pt-4">
                    <button
                      type="button"
                      onClick={() => setShowAddAddressModal(false)}
                      className="flex-1 btn-secondary"
                    >
                      Hủy
                    </button>
                    <button type="submit" className="flex-1 btn-primary">
                      Thêm địa chỉ
                    </button>
                  </div>
                </form>
              </div>
            </div>
          </div>
        )}
      </div>
    </div>
  );
};

export default CheckoutPage;