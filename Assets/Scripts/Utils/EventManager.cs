using System;
using System.Collections.Generic;

namespace Utils
{
    public sealed class EventManager : SingleInstance<EventManager>
    {
        readonly Dictionary<Type, HandlerBase> _handlers = new Dictionary<Type, HandlerBase>(100);

        public static void Subscribe<T>(object watcher, Action<T> action) where T: struct
        {
            Instance.Sub(watcher, action);
        }

        public static void Unsubscribe<T>(Action<T> action) where T : struct
        {
            if ( _instance != null )
            {
                Instance.Unsub(action);
            }
        }

        private void Sub<T>(object watcher, Action<T> action)
        {
            var tHandler = GetOrCreateHandler<T>();
            if ( tHandler != null )
            {
                tHandler.Subscribe(watcher, action);
            }
        }

        private void Unsub<T>(Action<T> action)
        {
            if ( !_handlers.TryGetValue(typeof(T), out var handler) )
            {
                return;
            }

            if ( handler is Handler<T> tHandler )
            {
                tHandler.Unsubscribe(action);
            }
        }

        public static void Fire<T>(T args) where T : struct =>
            Instance.FireEvent(args);

        private Handler<T> GetOrCreateHandler<T>()
        {
            if ( !_handlers.TryGetValue(typeof(T), out var handler) )
            {
                handler = new Handler<T>();
                _handlers.Add(typeof(T), handler);
            }
            return handler as Handler<T>;
        }

        private void FireEvent<T>(T args)
        {
            var tHandler = GetOrCreateHandler<T>();
            tHandler?.Fire(args);
        }
    }
}
