const LoadingSpinner = ({ size = 'md', fullScreen = false }) => {
  const sizeClasses = {
    sm: 'w-6 h-6',
    md: 'w-10 h-10',
    lg: 'w-16 h-16'
  };

  if (fullScreen) {
    return (
      <div className="fixed inset-0 bg-white bg-opacity-75 flex items-center justify-center z-50">
        <div className={`spinner ${sizeClasses[size]}`}></div>
      </div>
    );
  }

  return (
    <div className="flex items-center justify-center py-8">
      <div className={`spinner ${sizeClasses[size]}`}></div>
    </div>
  );
};

export default LoadingSpinner;