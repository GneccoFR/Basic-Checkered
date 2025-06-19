using System.Collections.Generic;
using Core.Service_Locator;
using UnityEngine;

namespace Core.Installers
{
    [DefaultExecutionOrder(-500)]
    public class GameServicesLoader : MonoBehaviour
    {
        [SerializeField] private List<Installer> installers;
     
        private static GameServicesLoader _instance;
        
        private void Awake()
        {
            if (!_instance)
            {
                _instance = this;
                DontDestroyOnLoad(gameObject);
                InstallDependencies();
            }
            else
            {
                Destroy(gameObject);
            }	
        }

        private void InstallDependencies()
        {
            installers.ForEach(installer => installer.Install(ServiceLocator.Instance));
        }
    }
}
