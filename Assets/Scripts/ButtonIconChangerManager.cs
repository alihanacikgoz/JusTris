using UnityEngine;
using UnityEngine.UI;

public class ButtonIconChangerManager : MonoBehaviour
{
   public Sprite onIcon;
   public Sprite offIcon;

   private Image _iconImage;

   public bool defaultIconStatus = true;

   private void Start()
   {
      _iconImage = GetComponent<Image>();
      _iconImage.sprite = (defaultIconStatus) ? onIcon : offIcon;
   }
   

   public void IconOnOffFNC(bool iconStatus)
   {
      if (!_iconImage || !onIcon || !offIcon) 
         return;
      
      _iconImage.sprite = (iconStatus) ? onIcon : offIcon;
   }
}
