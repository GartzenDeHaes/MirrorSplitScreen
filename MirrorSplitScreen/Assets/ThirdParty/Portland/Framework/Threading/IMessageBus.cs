using System;

using Portland.Text;

namespace Portland.Threading
{
	public interface IMessageBus<TMSG>
	{
		void Start();
		void Shutdown();

		void Subscribe(string subrUniqueKey, in String10 msgName, Action<TMSG> action, MessageExecContext ctx = MessageExecContext.BACKGROUND);
		void UnSubscribe(string subrUniqueKey, in String10 msgName);
		void RemoveSubscriber(string subrUniqueKey);

		void Publish(in String10 msgName);
		void Publish(in TMSG msg);
		//void Publish(in String10 msgName, in Variant8 arg);
		//void Publish(in String10 msgName, TDATA data);
		//void Publish(in String10 msgName, in Variant8 arg, TDATA data);
		
		void StopAcceptingNewMessageDefinitions();
		void DefineMessage(in String10 name);

		void Poll();
		//void SetMessageMarshaller(Action<Subscription<TMSG>, TMSG> runner);
		//void AddMessageObserver(Action<Subscription<TMSG>, TMSG> observer);
	}
}
