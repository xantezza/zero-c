namespace Services
{
    public static class ServicesProvider
    {
        private static Service[] _services;
        private static int _servicesLength;

        public static void InitializeServicesProvider(ServicesInititalizer servicesInititalizer)
        {
            _services = servicesInititalizer.Services;
            _servicesLength = _services.Length;
        }

        public static T Get<T>() where T : Service
        {
            for (int i = 0; i < _servicesLength; i++)
            {
                if (_services[i] is T service)
                {
                    return service;
                }
            }

            return default;
        }
    }
}