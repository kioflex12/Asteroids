namespace Utils
{
    public abstract class SingleInstance<TI, T> where T: TI,new()
    {
        public static T Instance => _instance;

        protected static T _instance = new T();

        public static void RecreateInstance() =>
            _instance = new T();
    }
    public abstract class SingleInstance<T> : SingleInstance<T, T> where T : new() {}

}