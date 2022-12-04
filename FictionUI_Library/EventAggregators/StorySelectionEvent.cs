using FictionUI_Library.Models;
using Prism.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FictionUI_Library.EventAggregators
{
    public class StorySelectionEvent : PubSubEvent<StoryModel>
    {
    }
}
