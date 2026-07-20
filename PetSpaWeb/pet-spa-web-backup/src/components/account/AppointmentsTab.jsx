import { useState, useEffect } from 'react';
import { appointmentAPI } from '../../services/api';
import { formatDate } from '../../utils/helpers';
import LoadingSpinner from '../common/LoadingSpinner';

const AppointmentsTab = () => {
  const [appointments, setAppointments] = useState([]);
  const [loading, setLoading] = useState(true);
  const [selectedAppointment, setSelectedAppointment] = useState(null);
  const [showDetailModal, setShowDetailModal] = useState(false);

  useEffect(() => {
    fetchAppointments();
  }, []);

  const fetchAppointments = async () => {
    try {
      setLoading(true);
      const response = await appointmentAPI.getAll();

      if (response.data.success) {
        const sortedAppointments = (response.data.data || []).sort(
          (a, b) => new Date(b.appointmentDate) - new Date(a.appointmentDate)
        );
        setAppointments(sortedAppointments);
      }
    } catch (error) {
      console.error('Fetch appointments error:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleViewDetail = async (appointmentId) => {
    try {
      const response = await appointmentAPI.getById(appointmentId);

      if (response.data.success) {
        setSelectedAppointment(response.data.data);
        setShowDetailModal(true);
      }
    } catch (error) {
      console.error('Fetch appointment detail error:', error);
      alert('Không thể tải chi tiết lịch hẹn');
    }
  };

  const handleCancel = async (appointmentId) => {
    if (!window.confirm('Bạn có chắc muốn hủy lịch hẹn này?')) {
      return;
    }

    try {
      const response = await appointmentAPI.delete(appointmentId);

      if (response.data.success) {
        alert('Đã hủy lịch hẹn thành công');
        await fetchAppointments();
      }
    } catch (error) {
      console.error('Cancel appointment error:', error);
      alert('Không thể hủy lịch hẹn');
    }
  };

  const getStatusBadge = (status) => {
    if (!status) return { label: 'Không rõ', className: 'bg-gray-100 text-gray-800' };

    const lowerStatus = status.toLowerCase();

    switch (lowerStatus) {
      case 'pending':
        return { label: 'Chờ xác nhận', className: 'bg-yellow-100 text-yellow-800' };
      case 'confirmed':
        return { label: 'Đã xác nhận', className: 'bg-blue-100 text-blue-800' };
      case 'completed':
        return { label: 'Hoàn thành', className: 'bg-green-100 text-green-800' };
      case 'canceled':
        return { label: 'Đã hủy', className: 'bg-red-100 text-red-800' };
      default:
        return { label: status, className: 'bg-gray-100 text-gray-800' };
    }
  };

  if (loading) {
    return <LoadingSpinner />;
  }

  return (
    <div className="bg-white rounded-lg shadow p-6">
      <div className="flex items-center justify-between mb-6">
        <h2 className="text-2xl font-bold text-gray-900">Lịch hẹn Spa</h2>
        <a href="/service" className="btn-primary">
          Đặt lịch mới
        </a>
      </div>

      {appointments.length === 0 ? (
        <div className="text-center py-12">
          <svg className="w-16 h-16 text-gray-400 mx-auto mb-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M8 7V3m8 4V3m-9 8h10M5 21h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v12a2 2 0 002 2z" />
          </svg>
          <p className="text-gray-600 mb-4">Bạn chưa có lịch hẹn nào</p>
          <a href="/service" className="btn-primary inline-block">
            Đặt lịch ngay
          </a>
        </div>
      ) : (
        <div className="space-y-4">
          {appointments.map((appointment) => {
            const statusBadge = getStatusBadge(appointment.status);
            const appointmentDate = new Date(appointment.appointmentDate);
            const isPast = appointmentDate < new Date();

            return (
              <div key={appointment.appointmentId} className="border border-gray-200 rounded-lg p-4 hover:shadow-md transition-shadow">
                <div className="flex flex-col md:flex-row md:items-start justify-between gap-4">
                  <div className="flex-1">
                    <div className="flex items-center gap-3 mb-3">
                      <h3 className="font-semibold text-gray-900">
                        Lịch hẹn #{appointment.appointmentId}
                      </h3>
                      <span className={`px-2 py-1 text-xs font-semibold rounded ${statusBadge.className}`}>
                        {statusBadge.label}
                      </span>
                    </div>

                    <div className="grid grid-cols-1 md:grid-cols-2 gap-3 text-sm">
                      <div>
                        <p className="text-gray-600 mb-1">
                          <span className="font-medium">Dịch vụ:</span> {appointment.serviceName}
                        </p>
                        <p className="text-gray-600 mb-1">
                          <span className="font-medium">Thú cưng:</span> {appointment.petType} - {appointment.petBreed}
                        </p>
                        {appointment.petInfo && (
                          <p className="text-gray-600">
                            <span className="font-medium">Tên:</span> {appointment.petInfo}
                          </p>
                        )}
                      </div>
                      <div>
                        <p className="text-gray-600 mb-1">
                          <span className="font-medium">Ngày:</span> {formatDate(appointment.appointmentDate)}
                        </p>
                        <p className="text-gray-600 mb-1">
                          <span className="font-medium">Giờ:</span> {appointment.timeSlot}
                        </p>
                      </div>
                    </div>
                  </div>

                  <div className="flex md:flex-col gap-2">
                    <button
                      onClick={() => handleViewDetail(appointment.appointmentId)}
                      className="flex-1 md:flex-none btn-outline text-sm py-2 px-4"
                    >
                      Chi tiết
                    </button>
                  </div>
                </div>
              </div>
            );
          })}
        </div>
      )}

      {showDetailModal && selectedAppointment && (
        <div className="fixed inset-0 z-50 overflow-y-auto">
          <div className="flex items-center justify-center min-h-screen px-4 pt-4 pb-20 text-center sm:block sm:p-0">
            <div className="fixed inset-0 transition-opacity" aria-hidden="true">
              <div className="absolute inset-0 bg-gray-500 opacity-75" onClick={() => setShowDetailModal(false)}></div>
            </div>

            <span className="hidden sm:inline-block sm:align-middle sm:h-screen" aria-hidden="true">&#8203;</span>

            <div className="relative z-10 inline-block align-bottom bg-white rounded-lg text-left overflow-hidden shadow-xl transform transition-all sm:my-8 sm:align-middle sm:max-w-2xl sm:w-full">

              <div className="bg-white px-4 pt-5 pb-4 sm:p-6 sm:pb-4">
                <div className="flex items-center justify-between mb-6">
                  <h3 className="text-2xl font-bold text-gray-900">
                    Chi tiết lịch hẹn #{selectedAppointment.appointmentId}
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

                {/* Status Badge Modal */}
                <div className="mb-6">
                  {(() => {
                    const statusBadge = getStatusBadge(selectedAppointment.status);
                    return (
                      <span className={`px-3 py-1 text-sm font-semibold rounded ${statusBadge.className}`}>
                        {statusBadge.label}
                      </span>
                    );
                  })()}
                </div>

                {/* Appointment Info */}
                <div className="space-y-6">
                  <div>
                    <h4 className="font-semibold text-gray-900 mb-3">Thông tin lịch hẹn</h4>
                    <div className="bg-gray-50 rounded-lg p-4 space-y-2">
                      <div className="flex justify-between">
                        <span className="text-gray-600">Dịch vụ</span>
                        <span className="font-medium text-gray-900">{selectedAppointment.serviceName}</span>
                      </div>
                      <div className="flex justify-between">
                        <span className="text-gray-600">Ngày hẹn</span>
                        <span className="font-medium text-gray-900">{formatDate(selectedAppointment.appointmentDate)}</span>
                      </div>
                      <div className="flex justify-between">
                        <span className="text-gray-600">Khung giờ</span>
                        <span className="font-medium text-gray-900">{selectedAppointment.timeSlot}</span>
                      </div>
                    </div>
                  </div>

                  <div>
                    <h4 className="font-semibold text-gray-900 mb-3">Thông tin thú cưng</h4>
                    <div className="bg-gray-50 rounded-lg p-4 space-y-2">
                      {selectedAppointment.petInfo && (
                        <div className="flex justify-between">
                          <span className="text-gray-600">Tên</span>
                          <span className="font-medium text-gray-900">{selectedAppointment.petInfo}</span>
                        </div>
                      )}
                      <div className="flex justify-between">
                        <span className="text-gray-600">Loại</span>
                        <span className="font-medium text-gray-900">{selectedAppointment.petType}</span>
                      </div>
                      <div className="flex justify-between">
                        <span className="text-gray-600">Giống</span>
                        <span className="font-medium text-gray-900">{selectedAppointment.petBreed}</span>
                      </div>
                    </div>
                  </div>

                  <div>
                    <h4 className="font-semibold text-gray-900 mb-3">Thông tin liên hệ</h4>
                    <div className="bg-gray-50 rounded-lg p-4 space-y-2">
                      <div className="flex justify-between">
                        <span className="text-gray-600">Khách hàng</span>
                        <span className="font-medium text-gray-900">{selectedAppointment.customerName}</span>
                      </div>
                      <div className="flex justify-between">
                        <span className="text-gray-600">Số điện thoại</span>
                        <span className="font-medium text-gray-900">{selectedAppointment.customerPhone}</span>
                      </div>
                    </div>
                  </div>
                </div>

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

export default AppointmentsTab;