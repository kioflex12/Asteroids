using System;
using System.Collections.Generic;
using UnityEngine;

namespace Utils
{
    public class Handler<T> : HandlerBase
    {
        List<Action<T>> _actions  = new List<Action<T>>(100);
		List<Action<T>> _removed  = new(100);

		public void Subscribe(object watcher, Action<T> action)
		{
			if ( _removed.Contains(action) )
			{
				_removed.Remove(action);
			}
			if ( !_actions.Contains(action) )
			{
				_actions.Add(action);
				Watchers.Add(watcher);
			}
			else
			{
				Debug.LogError( $"{{watcher}} tries to subscribe to {action} again.");
			}
		}

		public void Unsubscribe(Action<T> action) =>
			SafeUnsubscribe(action);

		private void SafeUnsubscribe(Action<T> action)
		{
			var index = _actions.IndexOf(action);
			SafeUnsubscribe(index);

			if ( index < 0 )
			{
				Debug.LogError( $"Trying to unsubscribe action {action} without watcher.");
			}
		}

		private void SafeUnsubscribe(int index)
		{
			if ( index >= 0 )
			{
				_removed.Add(_actions[index]);
			}
		}

		private void FullUnsubscribe(int index)
		{
			if ( index >= 0 )
			{
				_actions.RemoveAt(index);
				if ( index < Watchers.Count )
				{
					Watchers.RemoveAt(index);
				}
			}
		}

		private void FullUnsubscribe(Action<T> action)
		{
			var index = _actions.IndexOf(action);
			FullUnsubscribe(index);
		}

		public void Fire(T arg)
		{
			foreach (var current in _actions)
			{
				if ( _removed.Contains(current) )
				{
					continue;
				}
				try {
					current.Invoke(arg);
				} catch ( Exception e ) {
					Debug.LogError(new Exception("Event Calback Expetion"));
				}
			}

			CleanUp();
		}

		public override void CleanUp()
		{
			foreach ( var item in _removed )
			{
				FullUnsubscribe(item);
			}
			_removed.Clear();
		}

		public override bool FixWatchers()
		{
			CleanUp();
			var count = 0;
			for ( var i = 0; i < Watchers.Count; i++ )
			{
				var watcher = Watchers[i];
				if ( watcher is MonoBehaviour behaviour )
				{
					if ( !behaviour )
					{
						SafeUnsubscribe(i);
						count++;
					}
				}
			}
			if ( count > 0 )
			{
				CleanUp();
			}

			return count == 0;
		}
    }
}