import { useState, useEffect } from 'react';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { useAuth } from '../contexts/AuthContext';
import ProfileTab from '../components/account/ProfileTab';
import PetsTab from '../components/account/PetsTab';
import OrdersTab from '../components/account/OrdersTab';
import AppointmentsTab from '../components/account/AppointmentsTab';
import AddressesTab from '../components/account/AddressesTab';
import LoadingSpinner from '../components/common/LoadingSpinner';

const AccountPage = () => {
  const navigate = useNavigate();
  const [searchParams, setSearchParams] = useSearchParams();
  const { isAuthenticated, loading } = useAuth();
  
  const [activeTab, setActiveTab] = useState('profile');

  useEffect(() => {
    if (!loading && !isAuthenticated) {
      navigate('/login', { state: { from: { pathname: '/account' } } });
    }
  }, [isAuthenticated, loading, navigate]);

  useEffect(() => {
    const tab = searchParams.get('tab');
    if (tab && ['profile', 'pets', 'orders', 'appointments', 'addresses'].includes(tab)) {
      setActiveTab(tab);
    }
  }, [searchParams]);

  const handleTabChange = (tab) => {
    setActiveTab(tab);
    setSearchParams({ tab });
  };

  if (loading) {
    return <LoadingSpinner fullScreen />;
  }

  if (!isAuthenticated) {
    return null;
  }

  const tabs = [
    { id: 'profile', label: 'Thông tin cá nhân', icon: '👤' },
    { id: 'pets', label: 'Thú cưng của tôi', icon: '🐾' },
    { id: 'orders', label: 'Đơn hàng', icon: '📦' },
    { id: 'appointments', label: 'Lịch hẹn Spa', icon: '📅' },
    { id: 'addresses', label: 'Sổ địa chỉ', icon: '📍' }
  ];

  return (
    <div className="bg-gray-50 min-h-screen py-8">
      <div className="container-custom">
        {/* Page Header */}
        <div className="mb-8">
          <h1 className="text-3xl font-bold text-gray-900 mb-2">Tài khoản của tôi</h1>
          <p className="text-gray-600">Quản lý thông tin, đơn hàng và lịch hẹn của bạn</p>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-4 gap-6">
          {/* Sidebar - Tabs */}
          <div className="lg:col-span-1">
            <div className="bg-white rounded-lg shadow p-4 sticky top-24">
              <nav className="space-y-1">
                {tabs.map((tab) => (
                  <button
                    key={tab.id}
                    onClick={() => handleTabChange(tab.id)}
                    className={`w-full flex items-center gap-3 px-4 py-3 text-left rounded-lg transition-colors ${
                      activeTab === tab.id
                        ? 'bg-primary-50 text-primary-600 font-medium'
                        : 'text-gray-700 hover:bg-gray-50'
                    }`}
                  >
                    <span className="text-xl">{tab.icon}</span>
                    <span className="text-sm">{tab.label}</span>
                  </button>
                ))}
              </nav>
            </div>
          </div>

          {/* Main Content */}
          <div className="lg:col-span-3">
            {activeTab === 'profile' && <ProfileTab />}
            {activeTab === 'pets' && <PetsTab />}
            {activeTab === 'orders' && <OrdersTab />}
            {activeTab === 'appointments' && <AppointmentsTab />}
            {activeTab === 'addresses' && <AddressesTab />}
          </div>
        </div>
      </div>
    </div>
  );
};

export default AccountPage;