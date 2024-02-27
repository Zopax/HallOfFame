using Hall_of_fame.models;

namespace Hall_of_fame.models
{
    public class Helper
    {
        private static AppContext _db;

        public static AppContext GetContext()
        {
            if (_db == null)
            {
                _db = new AppContext();
            }

            return _db;
        }
    }
}
