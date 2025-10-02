namespace MyHomeService.Services
{
    public class EditModeService
    {
        private bool _isEditMode = false;
        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                if (_isEditMode != value)
                {
                    _isEditMode = value;
                    OnChange?.Invoke();
                }
            }
        }
        public event Action? OnChange;
    }
}
