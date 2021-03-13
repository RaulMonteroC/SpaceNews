using Fusillade;

namespace NetworkTolerance.Connectivity
{
    public enum CallPriority
    {
        Background,
        User,
        Speculative,
        Explicit,
    }

    public static class CallPriorityExtensions
    {
        public static Priority ToFusilladePriority(this CallPriority priority)
        {
            switch (priority)
            {
                case CallPriority.Background: return Priority.Background;
                case CallPriority.User: return Priority.UserInitiated;
                case CallPriority.Speculative: return Priority.Speculative;
                case CallPriority.Explicit: return Priority.Explicit;
            }

            return Priority.Speculative;
        }        
    }
}