using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionUI_Library.EventAggregators
{
    //Used to move string(a username) from one module to another
    public class UsernameCarrierEvent : PubSubEvent<string>
    {
    }
}
