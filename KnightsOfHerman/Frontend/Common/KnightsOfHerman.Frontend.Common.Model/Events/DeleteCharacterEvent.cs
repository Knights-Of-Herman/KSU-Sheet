using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Frontend.Common.Model.Events
{
    public delegate void DeleteCharacterEventHandler();

    public delegate void ShowDeleteView(string charactername);
}
