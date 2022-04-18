namespace Banners.Infrastructure.Structures
{
    public class Pagination
    {
        private int _Page { get; set; }
        public int Page
        {
            get
            {
                if (_Page == 0)
                {
                    _Page = 1;
                }

                return _Page;
            }

            set
            {
                _Page = value;
                if (_Page <= 0)
                {
                    _Page = 1;
                }
            }
        }

        private int _Count { get; set; }
        public int Count
        {
            get
            {
                if (_Count == 0)
                {
                    _Count = 5;
                }
                if (_Count > 500)
                {
                    _Count = 500;
                }

                return _Count;
            }

            set
            {
                _Count = value;
                if (_Count <= 0)
                {
                    _Count = 5;
                }
                if (_Count > 500)
                {
                    _Count = 500;
                }
            }
        }
    }
}
