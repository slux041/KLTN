import { useState, useEffect } from 'react';
import { useAuth } from '../../contexts/AuthContext';
import { validatePhone } from '../../utils/helpers';

const ProfileTab = () => {
  const { user, updateUserProfile } = useAuth();

  const formatDateForInput = (dateString) => {
    if (!dateString) return '';
    return dateString.split('T')[0];
  };

  const [formData, setFormData] = useState({
    fullName: '',
    phone: '',
    imageUrl: '',
    dateOfBirth: '',
    gender: 'other'
  });

  useEffect(() => {
    if (user) {
      setFormData({
        fullName: user.fullName || '',
        phone: user.phone || '',
        imageUrl: user.imageUrl || '',
        dateOfBirth: formatDateForInput(user.dateOfBirth),
        gender: user.gender || 'other'
      });
    }
  }, [user]);

  const [editing, setEditing] = useState(false);
  const [submitting, setSubmitting] = useState(false);
  const [message, setMessage] = useState({ type: '', text: '' });

  const handleChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({ ...prev, [name]: value }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!formData.fullName.trim()) {
      setMessage({ type: 'error', text: 'Vui lòng nhập họ tên' });
      return;
    }

    if (formData.phone && !validatePhone(formData.phone)) {
      setMessage({ type: 'error', text: 'Số điện thoại không hợp lệ' });
      return;
    }

    setSubmitting(true);
    setMessage({ type: '', text: '' });

    const updatePayload = {
      fullName: formData.fullName.trim(),
      phone: formData.phone.trim() || null,
      imageUrl: formData.imageUrl.trim() || null,
      gender: formData.gender,
      dateOfBirth: formData.dateOfBirth ? new Date(formData.dateOfBirth).toISOString() : null
    };

    const result = await updateUserProfile(updatePayload);

    setSubmitting(false);

    if (result.success) {
      setMessage({ type: 'success', text: result.message || 'Cập nhật thành công!' });
      setEditing(false);
    } else {
      setMessage({ type: 'error', text: result.message || 'Cập nhật thất bại' });
    }
  };

  const handleCancel = () => {
    setFormData({
      fullName: user?.fullName || '',
      phone: user?.phone || '',
      imageUrl: user?.imageUrl || '',
      dateOfBirth: formatDateForInput(user?.dateOfBirth),
      gender: user?.gender || 'other'
    });
    setEditing(false);
    setMessage({ type: '', text: '' });
  };

  return (
    <div className="bg-white rounded-lg shadow p-6">
      <div className="flex items-center justify-between mb-6">
        <h2 className="text-2xl font-bold text-gray-900">Thông tin cá nhân</h2>
        {!editing && (
          <button
            onClick={() => setEditing(true)}
            className="btn-primary"
          >
            Chỉnh sửa
          </button>
        )}
      </div>

      {/* Message */}
      {message.text && (
        <div className={`mb-6 p-4 rounded-lg ${message.type === 'success' ? 'bg-green-50 text-green-800' : 'bg-red-50 text-red-800'
          }`}>
          {message.text}
        </div>
      )}

      <form onSubmit={handleSubmit}>
        <div className="space-y-6">

          {/* Grid Layout cho các trường ngắn */}
          <div className="grid grid-cols-1 md:grid-cols-2 gap-6">

            {/* Full Name */}
            <div className="md:col-span-2">
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Họ và tên <span className="text-red-500">*</span>
              </label>
              <input
                type="text"
                name="fullName"
                value={formData.fullName}
                onChange={handleChange}
                className="input-field"
                disabled={!editing || submitting}
                required
              />
            </div>

            {/* Email (Read-only) */}
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Email
              </label>
              <input
                type="email"
                value={user?.email || ''}
                className="input-field bg-gray-50 text-gray-500"
                disabled
              />
            </div>

            {/* Phone */}
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Số điện thoại
              </label>
              <input
                type="tel"
                name="phone"
                value={formData.phone}
                onChange={handleChange}
                className="input-field"
                placeholder="0912345678"
                disabled={!editing || submitting}
              />
            </div>

            {/* Date of Birth */}
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Ngày sinh
              </label>
              <input
                type="date"
                name="dateOfBirth"
                value={formData.dateOfBirth}
                onChange={handleChange}
                className="input-field"
                disabled={!editing || submitting}
              />
            </div>

            {/* Gender */}
            <div>
              <label className="block text-sm font-medium text-gray-700 mb-1">
                Giới tính
              </label>
              <select
                name="gender"
                value={formData.gender}
                onChange={handleChange}
                className="input-field"
                disabled={!editing || submitting}
              >
                <option value="male">Nam</option>
                <option value="female">Nữ</option>
                <option value="other">Khác</option>
              </select>
            </div>
          </div>

          {/* Action Buttons */}
          {editing && (
            <div className="flex gap-3 pt-4 border-t border-gray-100 mt-6">
              <button
                type="submit"
                disabled={submitting}
                className="flex-1 btn-primary disabled:opacity-50"
              >
                {submitting ? 'Đang lưu...' : 'Lưu thay đổi'}
              </button>
              <button
                type="button"
                onClick={handleCancel}
                disabled={submitting}
                className="flex-1 btn-secondary disabled:opacity-50"
              >
                Hủy
              </button>
            </div>
          )}
        </div>
      </form>
    </div>
  );
};

export default ProfileTab;