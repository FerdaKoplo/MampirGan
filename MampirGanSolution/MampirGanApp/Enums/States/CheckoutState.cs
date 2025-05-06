using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MampirGanApp.Enums.States
{
    public enum CheckoutState
    {
        Idle,
        ViewingCart,
        Confirming,      
        Processing,     
        Completed,       
        Failed
    }
}
