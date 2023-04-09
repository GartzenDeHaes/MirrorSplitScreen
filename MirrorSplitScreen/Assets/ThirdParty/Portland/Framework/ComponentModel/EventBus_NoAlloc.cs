using System;
using System.Collections.Generic;
#if UNITY_2017_1_OR_NEWER
using UnityEngine;
#else
using System.Diagnostics;
#endif
using Portland.Collections;
using Portland.Mathmatics;
using Portland.Threading;

namespace Portland.ComponentModel
{
	public struct EventMessage
	{
		public String10 EventName;
		public IntFloat NumArg;
		public object Data;
	}

	/// <summary>
	/// EventBus is a polled, non-threaded message bus
	/// </summary>
	public sealed class EventBus_NoAlloc : IMessageBus<EventMessage>
	{
		Dictionary<String10, Vector<Subscription<EventMessage>>> _subscriptions = new Dictionary<String10, Vector<Subscription<EventMessage>>>();
		Vector<EventMessage> _pending = new Vector<EventMessage>(5);
		bool _acceptNewMessageTypes = true;

		public void Poll()
		{
			Vector<Subscription<EventMessage>> vec;

			for (int x = 0; x < _pending.Count; x++)
			{
				var msg = _pending[x];
				vec = _subscriptions[msg.EventName];

				for (int i = 0; i < vec.Count; i++)
				{
					try
					{
						vec[i].Callback(msg);
					}
					catch (Exception ex)
					{
						Debug.LogException(ex);
					}
				}
			}

			_pending.Clear();
		}

		public void Publish(in String10 eventName, in int arg, in object data)
		{
			Debug.Assert(_subscriptions.ContainsKey(eventName));

			_pending.Add(new EventMessage { EventName = eventName, NumArg = new IntFloat { IntValue = arg }, Data = data });
		}

		public void Publish(in String10 eventName, in float arg, in object data)
		{
			Debug.Assert(_subscriptions.ContainsKey(eventName));

			_pending.Add(new EventMessage { EventName = eventName, NumArg = new IntFloat { FloatValue = arg }, Data = data });
		}

		public void Publish(in String10 eventName, in int arg)
		{
			Debug.Assert(_subscriptions.ContainsKey(eventName));

			_pending.Add(new EventMessage { EventName = eventName, NumArg = new IntFloat { IntValue = arg } });
		}

		public void Publish(in String10 eventName, in float arg)
		{
			Debug.Assert(_subscriptions.ContainsKey(eventName));

			_pending.Add(new EventMessage { EventName = eventName, NumArg = new IntFloat { FloatValue = arg } });
		}

		public void Publish(in String10 eventName, in object data)
		{
			Debug.Assert(_subscriptions.ContainsKey(eventName));

			_pending.Add(new EventMessage { EventName = eventName, Data = data });
		}

		public void Publish(in String10 eventName)
		{
			Debug.Assert(_subscriptions.ContainsKey(eventName));

			_pending.Add(new EventMessage { EventName = eventName });
		}

		public void Publish(in EventMessage msg)
		{
			Debug.Assert(_subscriptions.ContainsKey(msg.EventName));

			_pending.Add(msg);
		}

		public void Subscribe(string subscriberKey, in String10 eventName, Action<EventMessage> handler, MessageExecContext notUsed = MessageExecContext.UI_UPDATE)
		{
			Vector<Subscription<EventMessage>> vec;
			if (!_subscriptions.TryGetValue(eventName, out vec))
			{
				if (_acceptNewMessageTypes)
				{
					vec = new Vector<Subscription<EventMessage>>();
					_subscriptions.Add(eventName, vec);
				}
				else
				{
					throw new Exception($"No longer accepting new message types {subscriberKey} > {eventName}");
				}
			}

			vec.Add(new Subscription<EventMessage> { MessageName = eventName, Callback = handler, SubscriberUniqueKey = subscriberKey });
		}

		public void RemoveSubscriber(string subscriberKey)
		{
			Func<Subscription<EventMessage>, bool> rmfn = (s) => { return s.SubscriberUniqueKey == subscriberKey; };

			foreach (var vec in _subscriptions.Values)
			{
				vec.RemoveWhen(rmfn);
			}
		}

		public void UnSubscribe(string subscriberKey, in String10 eventName)
		{
			var msgName = eventName;
			Func<Subscription<EventMessage>, bool> rmfn = (s) => { return s.SubscriberUniqueKey == subscriberKey && s.MessageName == msgName; };

			foreach (var vec in _subscriptions.Values)
			{
				vec.RemoveWhen(rmfn);
			}
		}

		public void UnSubscribe(string subscriberKey)
		{
			Func<Subscription<EventMessage>, bool> rmfn = (s) => { return s.SubscriberUniqueKey == subscriberKey; };

			foreach (var vec in _subscriptions.Values)
			{
				vec.RemoveWhen(rmfn);
			}
		}

		public void Start()
		{
		}

		public void Shutdown()
		{
		}

		public void StopAcceptingNewMessageDefinitions()
		{
			_acceptNewMessageTypes = false;
		}

		public void DefineMessage(in String10 eventName)
		{
			if (!_subscriptions.TryGetValue(eventName, out var vec))
			{
				if (_acceptNewMessageTypes)
				{
					vec = new Vector<Subscription<EventMessage>>();
					_subscriptions.Add(eventName, vec);
				}
				else
				{
					throw new Exception($"No longer accepting new message types {eventName}");
				}
			}
		}

		public void Subscribe(String10 mSG_DO_JOINS, object onAllowJoins)
		{
			throw new NotImplementedException();
		}
	}
}
