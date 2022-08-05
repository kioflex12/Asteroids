using System.Collections.Generic;

namespace Utils
{
    public abstract class HandlerBase
    {
        protected List<object> Watchers { get; } = new List<object>(100);

        public virtual void CleanUp() {}

        public virtual bool FixWatchers() => false;
    }
}