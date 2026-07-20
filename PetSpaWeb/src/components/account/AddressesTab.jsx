import { useState, useEffect } from 'react';
import { customerAddressAPI, addressAPI } from '../../services/api';
import { MESSAGES } from '../../utils/constants';
import LoadingSpinner from '../common/LoadingSpinner';

const AddressesTab = () => {
  const [addresses, setAddresses] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [editingAddress, setEditingAddress] = useState(null);
  const [submitting, setSubmitting] = useState(false);
  const [message, setMessage] = useState({ type: '', text: '' });

  const [provinces, setProvinces] = useState([]);
  const [wards, setWards] = useState([]);

  const [formData, setFormData] = useState({
    fullName: '',
    phone: '',
    addressLine: '',
    provinceId: '',
    provinceName: '',
    wardId: '',
    wardName: '',
    isDefault: false
  });

  useEffect(() => {
    fetchAddresses();
    fetchProvinces();
  }, []);

  const fetchAddresses = async () => {
    try {
      setLoading(true);
      const response = await customerAddressAPI.getAll();

      if (response.data.success) {
        setAddresses(response.data.data || []);
      }
    } catch (error) {
      console.error('Fetch addresses error:', error);
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
    } catch (error) {
      console.error('Fetch provinces error:', error);
    }
  };

  const fetchWards = async (provinceId) => {
    try {
      const response = await addressAPI.getWards(provinceId);
      if (response.data.success) {
        setWards(response.data.data || []);
      }
    } catch (error) {
      console.error('Fetch wards error:', error);
    }
  };

  const handleAdd = () => {
    setEditingAddress(null);
    setFormData({
      fullName: '',
      phone: '',
      addressLine: '',
      provinceId: '',
      provinceName: '',
      wardId: '',
      wardName: '',
      isDefault: false
    });
    setWards([]);
    setMessage({ type: '', text: '' });
    setShowModal(true);
  };

  const handleEdit = async (address) => {
    setEditingAddress(address);
    setFormData({
      fullName: address.fullName || '',
      phone: address.phone || '',
      addressLine: address.addressLine || '',
      provinceId: address.provinceId || '',
      provinceName: address.provinceName || '',
      wardId: address.wardId || '',
      wardName: address.wardName || '',
      isDefault: address.isDefault || false
    });

    // Fetch wards for the selected province
    if (address.provinceId) {
      await fetchWards(address.provinceId);
    }

    setMessage({ type: '', text: '' });
    setShowModal(true);
  };

  const handleDelete = async (addressId, fullName) => {
    if (!window.confirm(`Bạn có chắc muốn xóa địa chỉ "${fullName}"?`)) {
      return;
    }

    try {
      const response = await customerAddressAPI.delete(addressId);

      if (response.data.success) {
        setMessage({ type: 'success', text: MESSAGES.SUCCESS.DELETE_ADDRESS });
        await fetchAddresses();
        setTimeout(() => setMessage({ type: '', text: '' }), 3000);
      }
    } catch (error) {
      console.error('Delete address error:', error);
      setMessage({ type: 'error', text: MESSAGES.ERROR.GENERIC });
    }
  };

  const handleSetDefault = async (addressId) => {
    try {
      const response = await customerAddressAPI.setDefault(addressId);

      if (response.data.success) {
        setMessage({ type: 'success', text: MESSAGES.SUCCESS.SET_DEFAULT_ADDRESS });
        await fetchAddresses();
        setTimeout(() => setMessage({ type: '', text: '' }), 3000);
      }
    } catch (error) {
      console.error('Set default address error:', error);
      setMessage({ type: 'error', text: MESSAGES.ERROR.GENERIC });
    }
  };

  const handleProvinceChange = async (e) => {
    const selectedProvince = provinces.find(p => p.id === e.target.value);
    setFormData({
      ...formData,
      provinceId: e.target.value,
      provinceName: selectedProvince?.name || '',
      wardId: '',
      wardName: ''
    });
    setWards([]);

    if (e.target.value) {
      await fetchWards(e.target.value);
    }
  };

  const handleWardChange = (e) => {
    const selectedWard = wards.find(w => w.id === e.target.value);
    setFormData({
      ...formData,
      wardId: e.target.value,
      wardName: selectedWard?.name || ''
    });
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!formData.fullName.trim() || !formData.phone.trim() ||
      !formData.addressLine.trim() || !formData.provinceId || !formData.wardId) {
      setMessage({ type: 'error', text: 'Vui lòng điền đầy đủ thông tin' });
      return;
    }

    setSubmitting(true);

    try {
      const addressData = {
        fullName: formData.fullName.trim(),
        phone: formData.phone.trim(),
        addressLine: formData.addressLine.trim(),
        provinceId: formData.provinceId,
        provinceName: formData.provinceName,
        wardId: formData.wardId,
        wardName: formData.wardName,
        isDefault: formData.isDefault
      };

      const response = editingAddress
        ? await customerAddressAPI.update(editingAddress.addressId, addressData)
        : await customerAddressAPI.create(addressData);

      if (response.data.success) {
        setMessage({
          type: 'success',
          text: editingAddress ? MESSAGES.SUCCESS.UPDATE_ADDRESS : MESSAGES.SUCCESS.ADD_ADDRESS
        });
        await fetchAddresses();
        setShowModal(false);
        setTimeout(() => setMessage({ type: '', text: '' }), 3000);
      } else {
        setMessage({ type: 'error', text: response.data.message || MESSAGES.ERROR.GENERIC });
      }
    } catch (error) {
      console.error('Submit address error:', error);
      setMessage({ type: 'error', text: MESSAGES.ERROR.GENERIC });
    } finally {
      setSubmitting(false);
    }
  };

  if (loading) {
    return <LoadingSpinner />;
  }

  return (
    <div className="bg-white rounded-lg shadow p-6">
      <div className="flex items-center justify-between mb-6">
        <h2 className="text-2xl font-bold text-gray-900">Sổ địa chỉ</h2>
        <button onClick={handleAdd} className="btn-primary">
          Thêm địa chỉ mới
        </button>
      </div>

      {/* Message */}
      {message.text && (
        <div className={`mb-6 p-4 rounded-lg ${message.type === 'success' ? 'bg-green-50 text-green-800' : 'bg-red-50 text-red-800'
          }`}>
          {message.text}
        </div>
      )}

      {addresses.length === 0 ? (
        <div className="text-center py-12">
          <svg className="w-16 h-16 text-gray-400 mx-auto mb-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M17.657 16.657L13.414 20.9a1.998 1.998 0 01-2.827 0l-4.244-4.243a8 8 0 1111.314 0z" />
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M15 11a3 3 0 11-6 0 3 3 0 016 0z" />
          </svg>
          <p className="text-gray-600 mb-4">Bạn chưa có địa chỉ nào</p>
          <button onClick={handleAdd} className="btn-primary">
            Thêm địa chỉ đầu tiên
          </button>
        </div>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
          {addresses.map((address) => (
            <div key={address.addressId} className="border border-gray-200 rounded-lg p-4 relative">
              {address.isDefault && (
                <span className="absolute top-4 right-4 px-2 py-1 bg-primary-100 text-primary-700 text-xs font-semibold rounded">
                  Mặc định
                </span>
              )}

              <div className="pr-20">
                <h3 className="font-semibold text-gray-900 mb-2">{address.fullName}</h3>
                <p className="text-sm text-gray-600 mb-1">{address.phone}</p>
                <p className="text-sm text-gray-600 mb-4">{address.fullAddress}</p>
              </div>

              <div className="flex gap-2">
                <button
                  onClick={() => handleEdit(address)}
                  className="flex-1 btn-outline text-sm py-2"
                >
                  Sửa
                </button>
                {!address.isDefault && (
                  <>
                    <button
                      onClick={() => handleSetDefault(address.addressId)}
                      className="flex-1 btn-secondary text-sm py-2"
                    >
                      Đặt mặc định
                    </button>
                    <button
                      onClick={() => handleDelete(address.addressId, address.fullName)}
                      className="flex-1 bg-red-500 hover:bg-red-600 text-white text-sm py-2 rounded-lg transition-colors"
                    >
                      Xóa
                    </button>
                  </>
                )}
              </div>
            </div>
          ))}
        </div>
      )}

      {showModal && (
        <div className="fixed inset-0 z-50 overflow-y-auto">
          <div className="flex items-center justify-center min-h-screen px-4 pt-4 pb-20 text-center sm:block sm:p-0">
            <div className="fixed inset-0 transition-opacity" aria-hidden="true">
              <div className="absolute inset-0 bg-gray-500 opacity-75" onClick={() => setShowModal(false)}></div>
            </div>

            <span className="hidden sm:inline-block sm:align-middle sm:h-screen" aria-hidden="true">&#8203;</span>

            <div className="relative z-10 inline-block align-bottom bg-white rounded-lg text-left overflow-hidden shadow-xl transform transition-all sm:my-8 sm:align-middle sm:max-w-2xl sm:w-full">

              <div className="bg-white px-4 pt-5 pb-4 sm:p-6 sm:pb-4">
                <div className="flex items-center justify-between mb-6">
                  <h3 className="text-xl font-bold text-gray-900">
                    {editingAddress ? 'Chỉnh sửa địa chỉ' : 'Thêm địa chỉ mới'}
                  </h3>
                  <button
                    onClick={() => setShowModal(false)}
                    className="text-gray-400 hover:text-gray-600"
                  >
                    <svg className="w-6 h-6" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                      <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M6 18L18 6M6 6l12 12" />
                    </svg>
                  </button>
                </div>

                {message.text && showModal && (
                  <div className={`mb-4 p-3 rounded-lg text-sm ${message.type === 'success' ? 'bg-green-50 text-green-800' : 'bg-red-50 text-red-800'
                    }`}>
                    {message.text}
                  </div>
                )}

                <form onSubmit={handleSubmit} className="space-y-4">
                  <div className="grid grid-cols-1 md:grid-cols-2 gap-4">
                    <div>
                      <label className="block text-sm font-medium text-gray-700 mb-1">
                        Họ và tên <span className="text-red-500">*</span>
                      </label>
                      <input
                        type="text"
                        value={formData.fullName}
                        onChange={(e) => setFormData({ ...formData, fullName: e.target.value })}
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
                        value={formData.phone}
                        onChange={(e) => setFormData({ ...formData, phone: e.target.value })}
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
                        value={formData.provinceId}
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
                        value={formData.wardId}
                        onChange={handleWardChange}
                        className="input-field"
                        disabled={!formData.provinceId}
                        required
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
                      value={formData.addressLine}
                      onChange={(e) => setFormData({ ...formData, addressLine: e.target.value })}
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
                      checked={formData.isDefault}
                      onChange={(e) => setFormData({ ...formData, isDefault: e.target.checked })}
                      className="w-4 h-4 text-primary-500 focus:ring-primary-500 border-gray-300 rounded"
                    />
                    <label htmlFor="isDefault" className="ml-2 text-sm text-gray-700">
                      Đặt làm địa chỉ mặc định
                    </label>
                  </div>

                  <div className="flex gap-3 pt-4">
                    <button
                      type="button"
                      onClick={() => setShowModal(false)}
                      disabled={submitting}
                      className="flex-1 btn-secondary disabled:opacity-50"
                    >
                      Hủy
                    </button>
                    <button
                      type="submit"
                      disabled={submitting}
                      className="flex-1 btn-primary disabled:opacity-50"
                    >
                      {submitting ? 'Đang lưu...' : editingAddress ? 'Cập nhật' : 'Thêm'}
                    </button>
                  </div>
                </form>
              </div>
            </div>
          </div>
        </div>
      )}
    </div>
  );
};

export default AddressesTab;