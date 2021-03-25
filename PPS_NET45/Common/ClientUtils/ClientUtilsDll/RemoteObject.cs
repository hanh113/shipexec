using OperationWCF;
using RemoteService;

namespace ClientUtilsDll
{
	public class RemoteObject
	{
		public IRemoteServiceObject New()
		{
			return HttpChannel.Get<IRemoteServiceObject>(ClientUtils.ServerUrl, 0);
		}
	}
}
