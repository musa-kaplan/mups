using MoreMountains.NiceVibrations;
using MusaUtils.Templates.ArcadeIdle.General;
using UnityEngine;
using UnityEngine.Serialization;

namespace MusaUtils.Templates.ArcadeIdle.EnvironmentalScripts
{
    public class AreaOpener : MonoBehaviour
    {
        [SerializeField] private string eventHeader;
        [SerializeField] private int myID;
        [SerializeField] private string headerText;
        [SerializeField] private GameObject areaObject;
        [SerializeField] private CurrencyInteractionArea currencyInteractionArea;
        [SerializeField] private int openingPrice;
        [SerializeField] private GameObject[] closingObjects;
        [SerializeField] private Animator doorAnimator;

        private void Start()
        {
            if (PlayerPrefsExtra.GetBool("Area" + myID + "Opened"))
            {
                if (doorAnimator != null)
                {
                    doorAnimator.SetInteger("State", 1);
                }
                
                areaObject.SetActive(true);
                foreach (var c in closingObjects)
                {
                    c.SetActive(false);
                }
            }
            else
            {
                areaObject.SetActive(false);
                currencyInteractionArea.SetHeaderText(headerText);
                currencyInteractionArea.neededAmount = openingPrice;
            }
        }
        
        public void LetsOpen()
        {
            if (doorAnimator != null)
            {
                doorAnimator.SetInteger("State", 1);
            }
            PlayerPrefsExtra.SetBool("Area" + myID + "Opened", true);

            if (PlayerPrefs.GetInt("Haptic").Equals(1)) { MMVibrationManager.Haptic(HapticTypes.MediumImpact); }
            
            ParticleContainer.ParticlePlay(InfoClasses.ParticleType.SmokeBurst, new Vector3(transform.position.x, transform.position.y + 1.5f, transform.position.z));
            
            areaObject.SetActive(true);
            foreach (var c in closingObjects)
            {
                c.SetActive(false);
            }
        }
    }
}