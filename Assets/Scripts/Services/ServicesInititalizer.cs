using UnityEngine;

namespace Services
{
    public class ServicesInititalizer : MonoBehaviour
    {
        [SerializeField] private ServicesKeeper _servicesKeeper;

        public Service[] Services { get; private set; } 

        //foreground execution order 
        private void Awake()
        {
            Application.targetFrameRate = 30;

            DontDestroyOnLoad(this);

            var servicesCount = _servicesKeeper.Services.Length;

            var cachedTransform = transform;

            Services = new Service[servicesCount];

            for (int i = 0; i < servicesCount; i++)
            {
                Services[i] = Instantiate(_servicesKeeper.Services[i], cachedTransform);
            }

            ServicesProvider.InitializeServicesProvider(this);
        }
    }
}