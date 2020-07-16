using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAN_XLVII_AndrejaKolesar
{
    class Publisher
    {
        //delegate and event based on that delegate
        public delegate void Notification(Vehicle[] vehicles);
        public event Notification onNotification;


        //raising event 
        public void Notify(Vehicle[] vehicles)
        {
            if (onNotification != null)
            {
                onNotification(vehicles);
            }
        }
    }
}
