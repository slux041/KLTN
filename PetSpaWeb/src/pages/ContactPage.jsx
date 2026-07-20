const ContactPage = () => {
  return (
    <div className="bg-gray-50 py-12">
      <div className="container-custom">
        <div className="mb-8">
          <h1 className="text-3xl font-bold text-gray-900">Liên hệ với chúng tôi</h1>
          <p className="text-gray-600 mt-2">Hãy ghé thăm hoặc liên hệ với chúng tôi qua các kênh bên dưới.</p>
        </div>

        <div className="grid grid-cols-1 lg:grid-cols-3 gap-8 items-start">

          <div className="lg:col-span-1 space-y-6">

            {/* Address Card */}
            <div className="bg-white rounded-lg shadow-md p-6 hover:shadow-lg transition-shadow">
              <div className="flex items-start gap-4">
                <div className="w-10 h-10 bg-primary-100 rounded-full flex items-center justify-center flex-shrink-0 text-primary-600">
                  <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M17.657 16.657L13.414 20.9a1.998 1.998 0 01-2.827 0l-4.244-4.243a8 8 0 1111.314 0z" />
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M15 11a3 3 0 11-6 0 3 3 0 016 0z" />
                  </svg>
                </div>
                <div>
                  <h3 className="font-semibold text-gray-900 mb-1">Địa chỉ</h3>
                  <p className="text-gray-600 text-sm leading-relaxed">
                    Trường ĐH Công Thương TP.HCM
                    <br />
                    140 Lê Trọng Tấn, Tây Thạnh, Tân Phú, TP.HCM
                  </p>
                </div>
              </div>
            </div>

            {/* Hotline Card */}
            <div className="bg-white rounded-lg shadow-md p-6 hover:shadow-lg transition-shadow">
              <div className="flex items-start gap-4">
                <div className="w-10 h-10 bg-primary-100 rounded-full flex items-center justify-center flex-shrink-0 text-primary-600">
                  <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M3 5a2 2 0 012-2h3.28a1 1 0 01.948.684l1.498 4.493a1 1 0 01-.502 1.21l-2.257 1.13a11.042 11.042 0 005.516 5.516l1.13-2.257a1 1 0 011.21-.502l4.493 1.498a1 1 0 01.684.949V19a2 2 0 01-2 2h-1C9.716 21 3 14.284 3 6V5z" />
                  </svg>
                </div>
                <div>
                  <h3 className="font-semibold text-gray-900 mb-1">Hotline</h3>
                  <div className="text-gray-600 text-sm space-y-1">
                    <a href="tel:19001234" className="block hover:text-primary-600 transition-colors font-medium">
                      1900-1234
                    </a>
                    <a href="tel:0917203062" className="block hover:text-primary-600 transition-colors">
                      0917-203-062
                    </a>
                  </div>
                </div>
              </div>
            </div>

            {/* Email Card */}
            <div className="bg-white rounded-lg shadow-md p-6 hover:shadow-lg transition-shadow">
              <div className="flex items-start gap-4">
                <div className="w-10 h-10 bg-primary-100 rounded-full flex items-center justify-center flex-shrink-0 text-primary-600">
                  <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M3 8l7.89 5.26a2 2 0 002.22 0L21 8M5 19h14a2 2 0 002-2V7a2 2 0 00-2-2H5a2 2 0 00-2 2v10a2 2 0 002 2z" />
                  </svg>
                </div>
                <div>
                  <h3 className="font-semibold text-gray-900 mb-1">Email</h3>
                  <div className="text-gray-600 text-sm space-y-1">
                    <a href="mailto:petshop140@gmail.com" className="block hover:text-primary-600 transition-colors">
                      petshop140@gmail.com
                    </a>
                    <a href="mailto:01663787169truong@gmail.com" className="block hover:text-primary-600 transition-colors">
                      01663787169truong@gmail.com
                    </a>
                  </div>
                </div>
              </div>
            </div>

            {/* Working Hours Card */}
            <div className="bg-white rounded-lg shadow-md p-6 hover:shadow-lg transition-shadow">
              <div className="flex items-start gap-4">
                <div className="w-10 h-10 bg-primary-100 rounded-full flex items-center justify-center flex-shrink-0 text-primary-600">
                  <svg className="w-5 h-5" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M12 8v4l3 3m6-3a9 9 0 11-18 0 9 9 0 0118 0z" />
                  </svg>
                </div>
                <div>
                  <h3 className="font-semibold text-gray-900 mb-1">Giờ mở cửa</h3>
                  <p className="text-gray-600 text-sm">
                    <span className="font-medium">Thứ 2 - Chủ nhật:</span>
                    <br />
                    8:00 - 20:00
                  </p>
                </div>
              </div>
            </div>

            {/* Social Media Card */}
            <div className="bg-white rounded-lg shadow-md p-6 hover:shadow-lg transition-shadow">
              <h3 className="font-semibold text-gray-900 mb-4 text-center lg:text-left">Kết nối với chúng tôi</h3>
              <div className="flex justify-center lg:justify-start gap-4">
                <a href="#" className="w-10 h-10 bg-blue-600 text-white rounded-full flex items-center justify-center hover:opacity-90 transition-opacity shadow-sm">
                  {/* Facebook Icon */}
                  <svg className="w-5 h-5" fill="currentColor" viewBox="0 0 24 24"><path d="M24 12.073c0-6.627-5.373-12-12-12s-12 5.373-12 12c0 5.99 4.388 10.954 10.125 11.854v-8.385H7.078v-3.47h3.047V9.43c0-3.007 1.792-4.669 4.533-4.669 1.312 0 2.686.235 2.686.235v2.953H15.83c-1.491 0-1.956.925-1.956 1.874v2.25h3.328l-.532 3.47h-2.796v8.385C19.612 23.027 24 18.062 24 12.073z" /></svg>
                </a>
                <a href="#" className="w-10 h-10 bg-pink-600 text-white rounded-full flex items-center justify-center hover:opacity-90 transition-opacity shadow-sm">
                  {/* Instagram Icon */}
                  <svg className="w-5 h-5" fill="currentColor" viewBox="0 0 24 24"><path d="M12 2.163c3.204 0 3.584.012 4.85.07 3.252.148 4.771 1.691 4.919 4.919.058 1.265.069 1.645.069 4.849 0 3.205-.012 3.584-.069 4.849-.149 3.225-1.664 4.771-4.919 4.919-1.266.058-1.644.07-4.85.07-3.204 0-3.584-.012-4.849-.07-3.26-.149-4.771-1.699-4.919-4.92-.058-1.265-.07-1.644-.07-4.849 0-3.204.013-3.583.07-4.849.149-3.227 1.664-4.771 4.919-4.919 1.266-.057 1.645-.069 4.849-.069zm0-2.163c-3.259 0-3.667.014-4.947.072-4.358.2-6.78 2.618-6.98 6.98-.059 1.281-.073 1.689-.073 4.948 0 3.259.014 3.668.072 4.948.2 4.358 2.618 6.78 6.98 6.98 1.281.058 1.689.072 4.948.072 3.259 0 3.668-.014 4.948-.072 4.354-.2 6.782-2.618 6.979-6.98.059-1.28.073-1.689.073-4.948 0-3.259-.014-3.667-.072-4.947-.196-4.354-2.617-6.78-6.979-6.98-1.281-.059-1.69-.073-4.949-.073zm0 5.838c-3.403 0-6.162 2.759-6.162 6.162s2.759 6.163 6.162 6.163 6.162-2.759 6.162-6.163c0-3.403-2.759-6.162-6.162-6.162zm0 10.162c-2.209 0-4-1.79-4-4 0-2.209 1.791-4 4-4s4 1.791 4 4c0 2.21-1.791 4-4 4zm6.406-11.845c-.796 0-1.441.645-1.441 1.44s.645 1.44 1.441 1.44c.795 0 1.439-.645 1.439-1.44s-.644-1.44-1.439-1.44z" /></svg>
                </a>
                <a href="#" className="w-10 h-10 bg-green-500 text-white rounded-full flex items-center justify-center hover:opacity-90 transition-opacity shadow-sm">
                  {/* Zalo/Phone Icon */}
                  <svg className="w-5 h-5" fill="currentColor" viewBox="0 0 24 24"><path d="M.057 24l1.687-6.163c-1.041-1.804-1.588-3.849-1.587-5.946.003-6.556 5.338-11.891 11.893-11.891 3.181.001 6.167 1.24 8.413 3.488 2.245 2.248 3.481 5.236 3.48 8.414-.003 6.557-5.338 11.892-11.893 11.892-1.99-.001-3.951-.5-5.688-1.448l-6.305 1.654zm6.597-3.807c1.676.995 3.276 1.591 5.392 1.592 5.448 0 9.886-4.434 9.889-9.885.002-5.462-4.415-9.89-9.881-9.892-5.452 0-9.887 4.434-9.889 9.884-.001 2.225.651 3.891 1.746 5.634l-.999 3.648 3.742-.981zm11.387-5.464c-.074-.124-.272-.198-.57-.347-.297-.149-1.758-.868-2.031-.967-.272-.099-.47-.149-.669.149-.198.297-.768.967-.941 1.165-.173.198-.347.223-.644.074-.297-.149-1.255-.462-2.39-1.475-.883-.788-1.48-1.761-1.653-2.059-.173-.297-.018-.458.13-.606.134-.133.297-.347.446-.521.151-.172.2-.296.3-.495.099-.198.05-.372-.025-.521-.075-.148-.669-1.611-.916-2.206-.242-.579-.487-.501-.669-.51l-.57-.01c-.198 0-.52.074-.792.372s-1.04 1.016-1.04 2.479 1.065 2.876 1.213 3.074c.149.198 2.095 3.2 5.076 4.487.709.306 1.263.489 1.694.626.712.226 1.36.194 1.872.118.571-.085 1.758-.719 2.006-1.413.248-.695.248-1.29.173-1.414z" /></svg>
                </a>
              </div>
            </div>
          </div>

          <div className="lg:col-span-2 h-full min-h-[500px]">
            <div className="bg-white rounded-lg shadow-lg overflow-hidden h-full">
              <iframe
                src="https://www.google.com/maps/embed?pb=!1m18!1m12!1m3!1d31352.804492545703!2d106.6172416!3d10.8036096!2m3!1f0!2f0!3f0!3m2!1i1024!2i768!4f13.1!3m3!1m2!1s0x31752be27d8b4f4d%3A0x92dcba2950430867!2zVHLGsOG7nW5nIMSQ4bqhaSBo4buNYyBDw7RuZyBUaMawxqFuZyBUUC4gSOG7kyBDaMOtIE1pbmggKEhVSVQp!5e0!3m2!1svi!2s!4v1763714522389!5m2!1svi!2s"
                className="w-full h-full"
                style={{ border: 0, minHeight: '500px' }}
                allowFullScreen=""
                loading="lazy"
                referrerPolicy="no-referrer-when-downgrade"
                title="Pet Spa Location Map"
              ></iframe>
            </div>
          </div>

        </div>

        <div className="mt-16">
          <div className="text-center mb-8">
            <h2 className="text-2xl font-bold text-gray-900">Câu hỏi thường gặp</h2>
            <div className="w-16 h-1 bg-primary-500 mx-auto mt-2 rounded-full"></div>
          </div>
          <div className="max-w-3xl mx-auto space-y-4">
            {[
              {
                question: 'Thời gian giao hàng là bao lâu?',
                answer: 'Đơn hàng sẽ được giao trong vòng 24-48 giờ đối với khu vực nội thành TP.HCM. Các tỉnh thành khác từ 2-5 ngày làm việc.'
              },
              {
                question: 'Tôi có thể đổi trả sản phẩm không?',
                answer: 'Bạn có thể đổi trả sản phẩm trong vòng 7 ngày kể từ ngày nhận hàng nếu sản phẩm còn nguyên tem mác, chưa qua sử dụng.'
              },
              {
                question: 'Làm sao để đặt lịch Spa cho thú cưng?',
                answer: 'Bạn có thể đặt lịch Spa trực tuyến qua trang web hoặc gọi điện đến hotline 1900-xxxx. Đội ngũ tư vấn sẽ hỗ trợ bạn chọn dịch vụ phù hợp.'
              },
              {
                question: 'Tôi có cần đặt hẹn trước khi đến Spa không?',
                answer: 'Để đảm bảo được phục vụ tốt nhất, chúng tôi khuyến khích bạn đặt hẹn trước. Tuy nhiên, chúng tôi vẫn nhận khách vãng lai nếu có chỗ trống.'
              }
            ].map((faq, index) => (
              <details key={index} className="bg-white rounded-lg p-5 shadow-sm hover:shadow-md transition-shadow group cursor-pointer border border-gray-100">
                <summary className="font-semibold text-gray-900 list-none flex items-center justify-between">
                  {faq.question}
                  <svg className="w-5 h-5 text-gray-400 group-open:rotate-180 transition-transform" fill="none" stroke="currentColor" viewBox="0 0 24 24">
                    <path strokeLinecap="round" strokeLinejoin="round" strokeWidth={2} d="M19 9l-7 7-7-7" />
                  </svg>
                </summary>
                <p className="mt-3 text-gray-600 text-sm leading-relaxed border-t pt-3 border-gray-100">{faq.answer}</p>
              </details>
            ))}
          </div>
        </div>
      </div>
    </div>
  );
};

export default ContactPage;