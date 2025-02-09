using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KnightsOfHerman.Common.Abstract.Modification
{
    /// <summary>
    /// Interface notified and tracks the path to a modification
    /// /// </summary>
    public interface INotifyModificationPath : ITrackModifed
    {

        event TrackedModificationEventHandler? OnTrackedModification;
    }

    public delegate void TrackedModificationEventHandler(object source, TrackedModificationEventArgs args);

    /// <summary>
    /// Holds arguments for Tracked Modification Events, IE the path to the value, and the value itself, and the action of the change
    /// </summary>
    public class TrackedModificationEventArgs : EventArgs
    {
        /// <summary>
        /// Path to the property in the form XXX.XXX.XXX.....
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// New Value associated with the change at the path
        /// </summary>
        public object Value { get; set; }

        /// <summary>
        /// Action associated with the change
        /// </summary>
        public EditActions Action { get; set; }

        public string Type { get; set; }
        //public Type ValueType { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="value"></param>
        /// <param name="action"></param>
        public TrackedModificationEventArgs(string path, object value, EditActions action = EditActions.Edit)
        {
            Path = path;
            Value = value;
            Action = action;
            Type = Value.GetType().ToString();
        }

        public TrackedModificationEventArgs() { }

        public TrackedModificationEventArgs PrependPath(string path)
        {
            Path = $"{path}.{Path}";
            return this;
        }
    }
}
