using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

using Portland.Text;

namespace Portland.Threading
{
	public class MessageBusService : IMessageBus<SimpleMessage>
	{
		private readonly ConcurrentDictionary<String10, List<Subscription<SimpleMessage>>> _subscriptions = new ConcurrentDictionary<String10, List<Subscription<SimpleMessage>>>();

		private readonly ConcurrentQueue<SimpleMessage> _messageQueue = new ConcurrentQueue<SimpleMessage>();

		private bool _lockNewMessages;
		private String10 _shutdownMessage;

		protected Action<Subscription<SimpleMessage>, SimpleMessage> _marshaller;

		private IThreadPool _threadPool;
		private ThreadService _thread;

		public void Publish(in String10 messageName)
		{
			var msg = new SimpleMessage() { MsgName = messageName };
			Publish(msg);
		}

		//public void Publish(in String10 messageName, in Variant16 arg)
		//{
		//	var msg = new SimpleMessage() { MsgName = messageName, Arg = arg };
		//	Publish(msg);
		//}

		//public void Publish(in String10 messageName, in Variant16 arg, object data)
		//{
		//	var msg = new SimpleMessage() { MsgName = messageName, Arg = arg, Data = data };
		//	Publish(msg);
		//}

		public void Publish(in String10 messageName, object data)
		{
			var msg = new SimpleMessage() { MsgName = messageName, Data = data };
			Publish(msg);
		}

		public void Publish(in SimpleMessage msg)
		{
			if (!_subscriptions.ContainsKey(msg.MsgName))
			{
#if UNITY_5_3_OR_NEWER
				UnityEngine.Debug.LogWarning("MessageDispatcher.Publish: No listener for " + msg.MsgName);
#else
				Debug.WriteLine("MessageDispatcher.Publish: No listener for " + msg.MsgName);
#endif
				return;
			}

			_messageQueue.Enqueue(msg);

			_thread.SignalRunState();
		}

		void RunServiceOne()
		{
			if (_messageQueue.Count == 0)
			{
				return;
			}

			SimpleMessage msg;

			while (_messageQueue.Count > 0)
			{
				if (!_messageQueue.TryDequeue(out msg))
				{
					continue;
				}

				var subs = _subscriptions[msg.MsgName];
				int sendCount = 0;

				for (int x = 0; x < subs.Count; x++)
				{
					var sub = subs.ElementAt(x);

					sendCount++;

					try
					{
						_marshaller.Invoke(sub, msg);
					}
					catch (Exception ex)
					{
#if UNITY_5_3_OR_NEWER
						UnityEngine.Debug.LogError(ex);
#else
						Debug.WriteLine(ex);
#endif
					}
				}

				if (sendCount == 0)
				{
#if UNITY_5_3_OR_NEWER
					UnityEngine.Debug.LogWarning("MessageBusService: No listeners for " + msg.MsgName);
#else
					Debug.WriteLine($"MessageBusService: No listeners for {msg.MsgName}");
#endif
				}
			}
		}

		private void RunCallback(Subscription<SimpleMessage> sub, SimpleMessage msg)
		{
			_threadPool.QueueWorkItem(() => sub.Callback(msg));
		}

		public void Shutdown()
		{
			Publish(_shutdownMessage);

			_thread.Shutdown();
		}

		public void Start()
		{
			_thread.Start();
		}

		public void StopAcceptingNewMessageDefinitions()
		{
			_lockNewMessages = true;
		}

		/// <summary>
		/// Called to dispatch message, should run Subscription.Callback(SimpleMessage)
		/// </summary>
		public void SetMessageMarshaller(Action<Subscription<SimpleMessage>, SimpleMessage> runner)
		{
			_marshaller = runner;
		}

		/// <summary>
		/// Note, SetMessageMarshaller() removes observers
		/// </summary>
		public void AddMessageObserver(Action<Subscription<SimpleMessage>, SimpleMessage> observer)
		{
			_marshaller += observer;
		}

		public void DefineMessage(in String10 msgName)
		{
			if (_lockNewMessages)
			{
				throw new Exception(msgName + " cannot add, locked");
			}
			if (!_subscriptions.ContainsKey(msgName))
			{
				_subscriptions.TryAdd(msgName, new List<Subscription<SimpleMessage>>());
			}
		}

		public void Subscribe
		(
			string subscriberUniqueKeyForRemove,
			in String10 msgName,
			Action<SimpleMessage> action,
			MessageExecContext ctx = MessageExecContext.BACKGROUND
		)
		{
			if (!_subscriptions.ContainsKey(msgName))
			{
				if (_lockNewMessages)
				{
					throw new Exception(msgName + " cannot add, locked");
				}
				_subscriptions.TryAdd(msgName, new List<Subscription<SimpleMessage>>());
			}

			_subscriptions[msgName].Add
			(
				new Subscription<SimpleMessage>()
				{
					SubscriberUniqueKey = subscriberUniqueKeyForRemove,
					MessageName = msgName,
					ThreadContext = ctx,
					Callback = action
				}
			);
		}

		public void RemoveSubscriber(string subrUniqueKey)
		{
			foreach (var service in _subscriptions.Keys)
			{
				var vec = _subscriptions[service];

				for (int x = 0; x < vec.Count; x++)
				{
					if (x < vec.Count && vec[x].SubscriberUniqueKey.Equals(subrUniqueKey))
					{
						vec.RemoveAt(x);
						break;
					}
				}
			}
		}

		public void UnSubscribe(string subscriberUniqueKey, in String10 msgName)
		{
			if (!_subscriptions.ContainsKey(msgName))
			{
				return;
			}

			bool found = false;
			var vec = _subscriptions[msgName];

			for (int x = 0; x < vec.Count; x++)
			{
				if (x < vec.Count && vec[x].SubscriberUniqueKey.Equals(subscriberUniqueKey))
				{
					vec.RemoveAt(x);
					found = true;
					break;
				}
			}

			if (!found)
			{
				Debug.WriteLine("RemoveSubscriber: {0} for {1} not found", msgName, subscriberUniqueKey);
			}
		}

		public void Poll()
		{
			throw new NotImplementedException();
		}

		public MessageBusService(IThreadPool threadPool, in String10 shutdownMessage)
		{
			_shutdownMessage = shutdownMessage;
			_marshaller = RunCallback;

			_threadPool = threadPool;

			_thread = new ThreadService(nameof(MessageBusService), RunServiceOne);
			_thread.WaitTime = 1000;
		}
	}
}
