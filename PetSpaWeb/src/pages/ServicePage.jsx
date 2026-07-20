import { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import { appointmentAPI, addressAPI, petAPI, customerAPI } from '../services/api';
import { SERVICE_IDS, PET_TYPES, IMAGE_PATHS, MESSAGES } from '../utils/constants';
import LoadingSpinner from '../components/common/LoadingSpinner';

const ServicePage = () => {
  const navigate = useNavigate();
  const { isAuthenticated, user } = useAuth();

  const [formData, setFormData] = useState({
    serviceId: SERVICE_IDS.BATH,
    petInfo: '',
    petType: PET_TYPES.DOG,
    petBreed: '',
    appointmentDate: '',
    timeSlot: '',
    customerName: '',
    customerPhone: '',
    customerAddress: '',
    provinceId: '',
    provinceName: '',
    wardId: '',
    wardName: '',
    selectedPetId: null
  });

  const [provinces, setProvinces] = useState([]);
  const [wards, setWards] = useState([]);
  const [myPets, setMyPets] = useState([]);
  const [availableSlots, setAvailableSlots] = useState([]);
  const [loadingSlots, setLoadingSlots] = useState(false);

  const [loading, setLoading] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [message, setMessage] = useState({ type: '', text: '' });

  const [useExistingPet, setUseExistingPet] = useState(false);

  useEffect(() => {
    fetchInitialData();
  }, [isAuthenticated]);

  useEffect(() => {
    if (formData.appointmentDate) {
      fetchAvailableSlots(formData.appointmentDate);
    }
  }, [formData.appointmentDate]);

  const fetchInitialData = async () => {
    try {
      setLoading(true);

      const provincesRes = await addressAPI.getProvinces();
      if (provincesRes.data.success) {
        setProvinces(provincesRes.data.data || []);
      }

      if (isAuthenticated) {
        const [profileRes, petsRes] = await Promise.all([
          customerAPI.getMyProfile(),
          petAPI.getAll()
        ]);

        if (profileRes.data.success) {
          const profile = profileRes.data.data;
          setFormData(prev => ({
            ...prev,
            customerName: profile.fullName || '',
            customerPhone: profile.phone || '',
            customerAddress: profile.address || ''
          }));
        }

        if (petsRes.data.success) {
          setMyPets(petsRes.data.data || []);
        }
      }
    } catch (error) {
      console.error('Fetch initial data error:', error);
    } finally {
      setLoading(false);
    }
  };

  const fetchAvailableSlots = async (date) => {
    try {
      setLoadingSlots(true);
      const response = await appointmentAPI.getAvailableSlots(date);

      if (response.data.success) {
        setAvailableSlots(response.data.data || []);
      }
    } catch (error) {
      console.error('Fetch available slots error:', error);
      setAvailableSlots([]);
    } finally {
      setLoadingSlots(false);
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
      try {
        const response = await addressAPI.getWards(e.target.value);
        if (response.data.success) {
          setWards(response.data.data || []);
        }
      } catch (error) {
        console.error('Fetch wards error:', error);
      }
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

  const handlePetTypeChange = (petType) => {
    setFormData({
      ...formData,
      petType,
      serviceId: petType === PET_TYPES.CAT && formData.serviceId === SERVICE_IDS.BATH_TRIM
        ? SERVICE_IDS.BATH
        : formData.serviceId
    });
  };

  const handleExistingPetChange = (e) => {
    const petId = parseInt(e.target.value);
    const selectedPet = myPets.find(p => p.petId === petId);

    if (selectedPet) {
      setFormData({
        ...formData,
        selectedPetId: petId,
        petInfo: selectedPet.name,
        petType: selectedPet.type,
        petBreed: selectedPet.breed || '',
        serviceId: selectedPet.type === PET_TYPES.CAT && formData.serviceId === SERVICE_IDS.BATH_TRIM
          ? SERVICE_IDS.BATH
          : formData.serviceId
      });
    }
  };

  const validateForm = () => {
    if (!formData.customerName.trim()) {
      setMessage({ type: 'error', text: 'Vui lòng nhập họ tên' });
      return false;
    }

    if (!formData.customerPhone.trim()) {
      setMessage({ type: 'error', text: 'Vui lòng nhập số điện thoại' });
      return false;
    }

    if (!isAuthenticated && (!formData.provinceId || !formData.wardId || !formData.customerAddress.trim())) {
      setMessage({ type: 'error', text: 'Vui lòng nhập đầy đủ địa chỉ' });
      return false;
    }

    if (!formData.petType) {
      setMessage({ type: 'error', text: 'Vui lòng chọn loại thú cưng' });
      return false;
    }

    if (!formData.petBreed.trim()) {
      setMessage({ type: 'error', text: 'Vui lòng nhập giống thú cưng' });
      return false;
    }

    if (!formData.serviceId) {
      setMessage({ type: 'error', text: 'Vui lòng chọn dịch vụ' });
      return false;
    }

    if (!formData.appointmentDate) {
      setMessage({ type: 'error', text: 'Vui lòng chọn ngày hẹn' });
      return false;
    }

    if (!formData.timeSlot) {
      setMessage({ type: 'error', text: 'Vui lòng chọn khung giờ' });
      return false;
    }

    return true;
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!validateForm()) return;

    setSubmitting(true);
    setMessage({ type: '', text: '' });

    try {
      const appointmentData = {
        serviceId: formData.serviceId,
        petInfo: formData.petInfo || formData.petBreed,
        petType: formData.petType,
        petBreed: formData.petBreed,
        appointmentDate: formData.appointmentDate,
        timeSlot: formData.timeSlot,
        customerName: formData.customerName,
        customerPhone: formData.customerPhone
      };

      if (!isAuthenticated) {
        appointmentData.customerAddress = `${formData.customerAddress}, ${formData.wardName}, ${formData.provinceName}`;
      }

      const response = await appointmentAPI.create(appointmentData);

      if (response.data.success) {
        setMessage({ type: 'success', text: MESSAGES.SUCCESS.APPOINTMENT_SUCCESS });

        setFormData({
          ...formData,
          serviceId: SERVICE_IDS.BATH,
          petInfo: '',
          petBreed: '',
          appointmentDate: '',
          timeSlot: '',
          selectedPetId: null
        });

        if (isAuthenticated) {
          setTimeout(() => {
            navigate('/account?tab=appointments');
          }, 2000);
        }
      } else {
        setMessage({ type: 'error', text: response.data.message || MESSAGES.ERROR.GENERIC });
      }
    } catch (error) {
      console.error('Create appointment error:', error);
      setMessage({
        type: 'error',
        text: error.response?.data?.message || MESSAGES.ERROR.GENERIC
      });
    } finally {
      setSubmitting(false);
    }
  };

  const getMinDate = () => {
    const today = new Date();
    return today.toISOString().split('T')[0];
  };

  const serviceOptions = [
    { id: SERVICE_IDS.BATH, label: 'Tắm vệ sinh', value: SERVICE_IDS.BATH },
    { id: SERVICE_IDS.BATH_SHAVE, label: 'Tắm + Cạo lông', value: SERVICE_IDS.BATH_SHAVE }
  ];

  if (formData.petType === PET_TYPES.DOG) {
    serviceOptions.push({
      id: SERVICE_IDS.BATH_TRIM,
      label: 'Tắm + Cắt tỉa',
      value: SERVICE_IDS.BATH_TRIM
    });
  }

  if (loading) {
    return <LoadingSpinner fullScreen />;
  }

  return (
    <div className="bg-gray-50 py-8">
      <div className="container-custom">
        <h1 className="text-3xl font-bold text-gray-900 mb-6">Đặt lịch Spa cho thú cưng</h1>

        {/* Message */}
        {message.text && (
          <div className={`mb-6 p-4 rounded-lg ${message.type === 'success' ? 'bg-green-50 text-green-800' : 'bg-red-50 text-red-800'
            }`}>
            {message.text}
          </div>
        )}

        <div className="grid grid-cols-1 lg:grid-cols-2 gap-8">
          {/* Left - Service Price Image */}
          <div className="order-2 lg:order-1">
            <div className="bg-white rounded-lg shadow-lg p-6 sticky top-24">
              <h2 className="text-xl font-bold text-gray-900 mb-4">Bảng giá dịch vụ</h2>
              <img
                src={IMAGE_PATHS.servicePrice}
                alt="Bảng giá dịch vụ"
                className="w-full rounded-lg"
                onError={(e) => {
                  e.target.src = 'https://via.placeholder.com/600x800?text=Service+Price+List';
                }}
              />

              <div className="mt-6 space-y-3">
                <div className="flex items-start gap-2 text-sm text-gray-600">
                  <svg className="w-5 h-5 text-primary-500 flex-shrink-0 mt-0.5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
                  </svg>
                  <span>Sử dụng sản phẩm cao cấp, an toàn</span>
                </div>
                <div className="flex items-start gap-2 text-sm text-gray-600">
                  <svg className="w-5 h-5 text-primary-500 flex-shrink-0 mt-0.5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
                  </svg>
                  <span>Nhân viên chuyên nghiệp, yêu thương động vật</span>
                </div>
                <div className="flex items-start gap-2 text-sm text-gray-600">
                  <svg className="w-5 h-5 text-primary-500 flex-shrink-0 mt-0.5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
                  </svg>
                  <span>Không gian sạch sẽ, thoáng mát</span>
                </div>
                <div className="flex items-start gap-2 text-sm text-gray-600">
                  <svg className="w-5 h-5 text-primary-500 flex-shrink-0 mt-0.5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M5 13l4 4L19 7" />
                  </svg>
                  <span>Hỗ trợ đưa đón tận nhà (phí phụ thu)</span>
                </div>
              </div>
            </div>
          </div>

          {/* Right - Booking Form */}
          <div className="order-1 lg:order-2">
            <div className="bg-white rounded-lg shadow-lg p-6">
              <h2 className="text-xl font-bold text-gray-900 mb-6">Thông tin đặt lịch</h2>

              <form onSubmit={handleSubmit} className="space-y-6">
                {/* Customer Info */}
                <div>
                  <h3 className="font-semibold text-gray-900 mb-4">Thông tin khách hàng</h3>
                  <div className="space-y-4">
                    <div>
                      <label className="block text-sm font-medium text-gray-700 mb-1">
                        Họ và tên <span className="text-red-500">*</span>
                      </label>
                      <input
                        type="text"
                        value={formData.customerName}
                        onChange={(e) => setFormData({ ...formData, customerName: e.target.value })}
                        className="input-field"
                        placeholder="Nguyễn Văn A"
                        disabled={isAuthenticated}
                        required
                      />
                    </div>

                    <div>
                      <label className="block text-sm font-medium text-gray-700 mb-1">
                        Số điện thoại <span className="text-red-500">*</span>
                      </label>
                      <input
                        type="tel"
                        value={formData.customerPhone}
                        onChange={(e) => setFormData({ ...formData, customerPhone: e.target.value })}
                        className="input-field"
                        placeholder="0912345678"
                        disabled={isAuthenticated}
                        required
                      />
                    </div>

                    {/* Address - only for guest */}
                    {!isAuthenticated && (
                      <>
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
                          <input
                            type="text"
                            value={formData.customerAddress}
                            onChange={(e) => setFormData({ ...formData, customerAddress: e.target.value })}
                            className="input-field"
                            placeholder="Số nhà, tên đường..."
                            required
                          />
                        </div>
                      </>
                    )}
                  </div>
                </div>

                {/* Pet Info */}
                <div>
                  <h3 className="font-semibold text-gray-900 mb-4">Thông tin thú cưng</h3>

                  {/* Use existing pet - only for logged in users */}
                  {isAuthenticated && myPets.length > 0 && (
                    <div className="mb-4">
                      <label className="flex items-center cursor-pointer">
                        <input
                          type="checkbox"
                          checked={useExistingPet}
                          onChange={(e) => {
                            setUseExistingPet(e.target.checked);
                            if (!e.target.checked) {
                              setFormData({
                                ...formData,
                                selectedPetId: null,
                                petInfo: '',
                                petBreed: ''
                              });
                            }
                          }}
                          className="w-4 h-4 text-primary-500 focus:ring-primary-500 border-gray-300 rounded"
                        />
                        <span className="ml-2 text-sm text-gray-700">
                          Chọn từ danh sách thú cưng của tôi
                        </span>
                      </label>
                    </div>
                  )}

                  {useExistingPet && myPets.length > 0 ? (
                    <div>
                      <label className="block text-sm font-medium text-gray-700 mb-1">
                        Chọn thú cưng <span className="text-red-500">*</span>
                      </label>
                      <select
                        value={formData.selectedPetId || ''}
                        onChange={handleExistingPetChange}
                        className="input-field"
                        required
                      >
                        <option value="">Chọn thú cưng</option>
                        {myPets.map((pet) => (
                          <option key={pet.petId} value={pet.petId}>
                            {pet.name} - {pet.type} ({pet.breed})
                          </option>
                        ))}
                      </select>
                    </div>
                  ) : (
                    <div className="space-y-4">
                      {/* Pet Type */}
                      <div>
                        <label className="block text-sm font-medium text-gray-700 mb-2">
                          Loại thú cưng <span className="text-red-500">*</span>
                        </label>
                        <div className="grid grid-cols-2 gap-3">
                          <button
                            type="button"
                            onClick={() => handlePetTypeChange(PET_TYPES.DOG)}
                            className={`p-4 border-2 rounded-lg transition-colors ${formData.petType === PET_TYPES.DOG
                                ? 'border-primary-500 bg-primary-50'
                                : 'border-gray-200 hover:border-primary-200'
                              }`}
                          >
                            <div className="text-center">
                              <span className="text-2xl">🐕</span>
                              <p className="mt-2 font-medium">{PET_TYPES.DOG}</p>
                            </div>
                          </button>
                          <button
                            type="button"
                            onClick={() => handlePetTypeChange(PET_TYPES.CAT)}
                            className={`p-4 border-2 rounded-lg transition-colors ${formData.petType === PET_TYPES.CAT
                                ? 'border-primary-500 bg-primary-50'
                                : 'border-gray-200 hover:border-primary-200'
                              }`}
                          >
                            <div className="text-center">
                              <span className="text-2xl">🐈</span>
                              <p className="mt-2 font-medium">{PET_TYPES.CAT}</p>
                            </div>
                          </button>
                        </div>
                      </div>

                      {/* Pet Breed */}
                      <div>
                        <label className="block text-sm font-medium text-gray-700 mb-1">
                          Giống thú cưng <span className="text-red-500">*</span>
                        </label>
                        <input
                          type="text"
                          value={formData.petBreed}
                          onChange={(e) => setFormData({ ...formData, petBreed: e.target.value })}
                          className="input-field"
                          placeholder="Ví dụ: Golden Retriever, Mèo Ba Tư..."
                          required
                        />
                      </div>
                    </div>
                  )}
                </div>

                {/* Service Selection */}
                <div>
                  <h3 className="font-semibold text-gray-900 mb-4">Chọn dịch vụ</h3>
                  <div className="space-y-3">
                    {serviceOptions.map((service) => (
                      <label
                        key={service.id}
                        className={`block p-4 border-2 rounded-lg cursor-pointer transition-colors ${formData.serviceId === service.value
                            ? 'border-primary-500 bg-primary-50'
                            : 'border-gray-200 hover:border-primary-200'
                          }`}
                      >
                        <input
                          type="radio"
                          name="service"
                          value={service.value}
                          checked={formData.serviceId === service.value}
                          onChange={(e) => setFormData({ ...formData, serviceId: parseInt(e.target.value) })}
                          className="sr-only"
                        />
                        <div className="flex items-center justify-between">
                          <span className="font-medium text-gray-900">{service.label}</span>
                          <div className={`w-5 h-5 rounded-full border-2 flex items-center justify-center ${formData.serviceId === service.value
                              ? 'border-primary-500 bg-primary-500'
                              : 'border-gray-300'
                            }`}>
                            {formData.serviceId === service.value && (
                              <svg className="w-3 h-3 text-white" fill="currentColor" viewBox="0 0 12 12">
                                <path d="M10 3L4.5 8.5 2 6" />
                              </svg>
                            )}
                          </div>
                        </div>
                      </label>
                    ))}
                  </div>
                </div>

                {/* Date & Time */}
                <div>
                  <h3 className="font-semibold text-gray-900 mb-4">Chọn ngày và giờ</h3>
                  <div className="space-y-4">
                    <div>
                      <label className="block text-sm font-medium text-gray-700 mb-1">
                        Ngày hẹn <span className="text-red-500">*</span>
                      </label>
                      <input
                        type="date"
                        value={formData.appointmentDate}
                        onChange={(e) => setFormData({ ...formData, appointmentDate: e.target.value, timeSlot: '' })}
                        min={getMinDate()}
                        className="input-field"
                        required
                      />
                    </div>

                    {formData.appointmentDate && (
                      <div>
                        <label className="block text-sm font-medium text-gray-700 mb-2">
                          Khung giờ <span className="text-red-500">*</span>
                        </label>
                        {loadingSlots ? (
                          <div className="text-center py-4">
                            <div className="inline-block w-6 h-6 border-2 border-primary-500 border-t-transparent rounded-full animate-spin"></div>
                            <p className="text-sm text-gray-600 mt-2">Đang tải khung giờ...</p>
                          </div>
                        ) : availableSlots.length > 0 ? (
                          <div className="grid grid-cols-2 md:grid-cols-3 gap-2">
                            {availableSlots.map((slot, index) => (
                              <button
                                key={index}
                                type="button"
                                onClick={() => setFormData({ ...formData, timeSlot: slot.timeSlot })}
                                disabled={!slot.isAvailable}
                                className={`p-3 text-sm border-2 rounded-lg transition-colors ${formData.timeSlot === slot.timeSlot
                                    ? 'border-primary-500 bg-primary-50 text-primary-700'
                                    : slot.isAvailable
                                      ? 'border-gray-200 hover:border-primary-200'
                                      : 'border-gray-200 bg-gray-100 text-gray-400 cursor-not-allowed opacity-60'
                                  }`}
                              >
                                {slot.timeSlot}
                                {!slot.isAvailable && (
                                  <span className="block text-xs mt-1">Đã đầy</span>
                                )}
                              </button>
                            ))}
                          </div>
                        ) : (
                          <p className="text-sm text-gray-600 text-center py-4">
                            Không có khung giờ nào khả dụng cho ngày này
                          </p>
                        )}
                      </div>
                    )}
                  </div>
                </div>

                {/* Submit Button */}
                <button
                  type="submit"
                  disabled={submitting}
                  className="w-full btn-primary disabled:opacity-50 disabled:cursor-not-allowed"
                >
                  {submitting ? 'Đang xử lý...' : 'Đặt lịch hẹn'}
                </button>

                {!isAuthenticated && (
                  <p className="text-sm text-gray-500 text-center">
                    Đã có tài khoản?{' '}
                    <button
                      type="button"
                      onClick={() => navigate('/login', { state: { from: { pathname: '/service' } } })}
                      className="text-primary-500 hover:text-primary-600 font-medium"
                    >
                      Đăng nhập
                    </button>
                  </p>
                )}
              </form>
            </div>
          </div>
        </div>
      </div>
    </div>
  );
};

export default ServicePage;