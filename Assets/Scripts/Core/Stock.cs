using System;

namespace Core
{
    public class Stock
    {
        private int _caps;
        private int _roots;

        public int Caps
        {
            get => _caps;
            set
            {
                _caps = value;
                OnStockUpdated?.Invoke();
            }
        }
        
        public int Roots
        {
            get => _roots;
            set
            {
                _roots = value;
                OnStockUpdated?.Invoke();
            }
        }

        public Action OnStockUpdated;
    }
}