using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Abstract.Modification
{
    /// <summary>
    /// Interaface that trackes whether the object has been modified
    /// </summary>
    public interface ITrackModifed
    {
        /// <summary>
        /// Whether any value associated with the object has been modified
        /// </summary>
        public bool Modified { get; set; }
    }
}
