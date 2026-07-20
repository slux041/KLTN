import { useState, useEffect } from 'react';
import { petAPI } from '../../services/api';
import { PET_TYPES, MESSAGES } from '../../utils/constants';
import LoadingSpinner from '../common/LoadingSpinner';

const PetsTab = () => {
  const [pets, setPets] = useState([]);
  const [loading, setLoading] = useState(true);
  const [showModal, setShowModal] = useState(false);
  const [editingPet, setEditingPet] = useState(null);
  const [submitting, setSubmitting] = useState(false);
  const [message, setMessage] = useState({ type: '', text: '' });

  const [formData, setFormData] = useState({
    name: '',
    type: PET_TYPES.DOG,
    breed: '',
    age: ''
  });

  useEffect(() => {
    fetchPets();
  }, []);

  const fetchPets = async () => {
    try {
      setLoading(true);
      const response = await petAPI.getAll();

      if (response.data.success) {
        setPets(response.data.data || []);
      }
    } catch (error) {
      console.error('Fetch pets error:', error);
    } finally {
      setLoading(false);
    }
  };

  const handleAdd = () => {
    setEditingPet(null);
    setFormData({
      name: '',
      type: PET_TYPES.DOG,
      breed: '',
      age: ''
    });
    setMessage({ type: '', text: '' });
    setShowModal(true);
  };

  const handleEdit = (pet) => {
    setEditingPet(pet);
    setFormData({
      name: pet.name || '',
      type: pet.type || PET_TYPES.DOG,
      breed: pet.breed || '',
      age: pet.age?.toString() || ''
    });
    setMessage({ type: '', text: '' });
    setShowModal(true);
  };

  const handleDelete = async (petId, petName) => {
    if (!window.confirm(`Bạn có chắc muốn xóa thú cưng "${petName}"?`)) {
      return;
    }

    try {
      const response = await petAPI.delete(petId);

      if (response.data.success) {
        setMessage({ type: 'success', text: MESSAGES.SUCCESS.DELETE_PET });
        await fetchPets();
        setTimeout(() => setMessage({ type: '', text: '' }), 3000);
      }
    } catch (error) {
      console.error('Delete pet error:', error);
      setMessage({ type: 'error', text: MESSAGES.ERROR.GENERIC });
    }
  };

  const handleSubmit = async (e) => {
    e.preventDefault();

    if (!formData.name.trim()) {
      setMessage({ type: 'error', text: 'Vui lòng nhập tên thú cưng' });
      return;
    }

    setSubmitting(true);

    try {
      const petData = {
        name: formData.name.trim(),
        type: formData.type,
        breed: formData.breed.trim() || null,
        age: formData.age ? parseInt(formData.age) : null
      };

      const response = editingPet
        ? await petAPI.update(editingPet.petId, petData)
        : await petAPI.create(petData);

      if (response.data.success) {
        setMessage({
          type: 'success',
          text: editingPet ? MESSAGES.SUCCESS.UPDATE_PET : MESSAGES.SUCCESS.ADD_PET
        });
        await fetchPets();
        setShowModal(false);
        setTimeout(() => setMessage({ type: '', text: '' }), 3000);
      } else {
        setMessage({ type: 'error', text: response.data.message || MESSAGES.ERROR.GENERIC });
      }
    } catch (error) {
      console.error('Submit pet error:', error);
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
        <h2 className="text-2xl font-bold text-gray-900">Thú cưng của tôi</h2>
        <button onClick={handleAdd} className="btn-primary">
          Thêm thú cưng
        </button>
      </div>

      {/* Message */}
      {message.text && (
        <div className={`mb-6 p-4 rounded-lg ${message.type === 'success' ? 'bg-green-50 text-green-800' : 'bg-red-50 text-red-800'
          }`}>
          {message.text}
        </div>
      )}

      {/* Pets Grid */}
      {pets.length === 0 ? (
        <div className="text-center py-12">
          <svg className="w-16 h-16 text-gray-400 mx-auto mb-4" fill="none" stroke="currentColor" viewBox="0 0 24 24">
            <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 6v6m0 0v6m0-6h6m-6 0H6" />
          </svg>
          <p className="text-gray-600 mb-4">Bạn chưa có thú cưng nào</p>
          <button onClick={handleAdd} className="btn-primary">
            Thêm thú cưng đầu tiên
          </button>
        </div>
      ) : (
        <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
          {pets.map((pet) => (
            <div key={pet.petId} className="border border-gray-200 rounded-lg overflow-hidden hover:shadow-md transition-shadow">
              <div className="p-4">
                <div className="flex justify-between items-start mb-2">
                  <h3 className="text-lg font-bold text-gray-900">{pet.name}</h3>
                  <div className="flex space-x-2">
                    <button
                      onClick={() => handleEdit(pet)}
                      className="text-blue-600 hover:text-blue-800"
                    >
                      <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M11 5H6a2 2 0 00-2 2v11a2 2 0 002 2h11a2 2 0 002-2v-5m-1.414-9.414a2 2 0 112.828 2.828L11.828 15H9v-2.828l8.586-8.586z" />
                      </svg>
                    </button>
                    <button
                      onClick={() => handleDelete(pet.petId, pet.name)}
                      className="text-red-600 hover:text-red-800"
                    >
                      <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                        <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 7l-.867 12.142A2 2 0 0116.138 21H7.862a2 2 0 01-1.995-1.858L5 7m5 4v6m4-6v6m1-10V4a1 1 0 00-1-1h-4a1 1 0 00-1 1v3M4 7h16" />
                      </svg>
                    </button>
                  </div>
                </div>

                <div className="space-y-1 text-sm text-gray-600">
                  <p><span className="font-medium">Loại:</span> {pet.type}</p>
                  {pet.breed && <p><span className="font-medium">Giống:</span> {pet.breed}</p>}
                  {pet.age && <p><span className="font-medium">Tuổi:</span> {pet.age} tuổi</p>}
                </div>
              </div>
            </div>
          ))}
        </div>
      )}

      {/* Add/Edit Modal */}
      {showModal && (
        <div className="fixed inset-0 z-50 overflow-y-auto">
          <div className="flex items-center justify-center min-h-screen px-4 pt-4 pb-20 text-center sm:block sm:p-0">
            <div className="fixed inset-0 transition-opacity" aria-hidden="true">
              <div className="absolute inset-0 bg-gray-500 opacity-75" onClick={() => setShowModal(false)}></div>
            </div>

            <span className="hidden sm:inline-block sm:align-middle sm:h-screen" aria-hidden="true">&#8203;</span>
            <div className="relative z-10 inline-block align-bottom bg-white rounded-lg text-left overflow-hidden shadow-xl transform transition-all sm:my-8 sm:align-middle sm:max-w-lg sm:w-full">

              <div className="bg-white px-4 pt-5 pb-4 sm:p-6 sm:pb-4">
                <div className="sm:flex sm:items-start">
                  <div className="mt-3 text-center sm:mt-0 sm:ml-4 sm:text-left w-full">
                    <h3 className="text-lg leading-6 font-medium text-gray-900" id="modal-title">
                      {editingPet ? 'Cập nhật thú cưng' : 'Thêm thú cưng mới'}
                    </h3>

                    {/* Modal Message */}
                    {message.text && showModal && (
                      <div className={`mb-4 p-3 rounded-lg text-sm ${message.type === 'success' ? 'bg-green-50 text-green-800' : 'bg-red-50 text-red-800'
                        }`}>
                        {message.text}
                      </div>
                    )}

                    <div className="mt-4">
                      <form onSubmit={handleSubmit}>
                        <div className="space-y-4">
                          <div>
                            <label className="block text-sm font-medium text-gray-700">Tên thú cưng <span className="text-red-500">*</span></label>
                            <input
                              type="text"
                              name="name"
                              value={formData.name}
                              onChange={(e) => setFormData({ ...formData, name: e.target.value })}
                              className="input-field mt-1"
                              placeholder="Tên thú cưng"
                              required
                            />
                          </div>

                          <div className="grid grid-cols-2 gap-4">
                            <div>
                              <label className="block text-sm font-medium text-gray-700">Loại <span className="text-red-500">*</span></label>
                              <select
                                name="type"
                                value={formData.type}
                                onChange={(e) => setFormData({ ...formData, type: e.target.value })}
                                className="input-field mt-1"
                              >
                                <option value={PET_TYPES.DOG}>Chó</option>
                                <option value={PET_TYPES.CAT}>Mèo</option>
                              </select>
                            </div>
                            <div>
                              <label className="block text-sm font-medium text-gray-700">Giống</label>
                              <input
                                type="text"
                                name="breed"
                                value={formData.breed}
                                onChange={(e) => setFormData({ ...formData, breed: e.target.value })}
                                className="input-field mt-1"
                                placeholder="Ví dụ: Golden Retriever"
                              />
                            </div>
                          </div>

                          <div>
                            <label className="block text-sm font-medium text-gray-700">Tuổi</label>
                            <input
                              type="number"
                              name="age"
                              value={formData.age}
                              onChange={(e) => setFormData({ ...formData, age: e.target.value })}
                              className="input-field mt-1"
                              placeholder="Tuổi"
                              min="0"
                            />
                          </div>
                        </div>

                        <div className="mt-5 sm:mt-6 sm:grid sm:grid-cols-2 sm:gap-3 sm:grid-flow-row-dense">
                          <button
                            type="submit"
                            disabled={submitting}
                            className="w-full inline-flex justify-center rounded-md border border-transparent shadow-sm px-4 py-2 bg-primary-600 text-base font-medium text-white hover:bg-primary-700 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-primary-500 sm:col-start-2 sm:text-sm"
                          >
                            {submitting ? 'Đang xử lý...' : (editingPet ? 'Cập nhật' : 'Thêm mới')}
                          </button>
                          <button
                            type="button"
                            onClick={() => setShowModal(false)}
                            className="mt-3 w-full inline-flex justify-center rounded-md border border-gray-300 shadow-sm px-4 py-2 bg-white text-base font-medium text-gray-700 hover:bg-gray-50 focus:outline-none focus:ring-2 focus:ring-offset-2 focus:ring-primary-500 sm:mt-0 sm:col-start-1 sm:text-sm"
                          >
                            Hủy
                          </button>
                        </div>
                      </form>
                    </div>
                  </div>
                </div>
              </div>
            </div>

          </div>
        </div>
      )}
    </div>
  );
};

export default PetsTab;