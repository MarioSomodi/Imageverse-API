using PostSharp.Aspects;
using PostSharp.Serialization;

namespace Imageverse.Api.Common.Aspects
{
	[PSerializable]
	public class LogToFile : OnExceptionAspect
	{

		public override void OnException(MethodExecutionArgs args)
		{
			var fs = File.Open(@"C:\Imageverse-ExceptionsLog.log", FileMode.Append);
			using(fs)
			{
				using (StreamWriter sw = new StreamWriter(fs))
				{
					sw.WriteLine($"{DateTime.Now} - Source = <{args.Exception.Source}> message : {args.Exception.Message} -> inner message ? {args.Exception.InnerException?.Message} --> inner exception stack ? {args.Exception.InnerException?.StackTrace} --> exception stack trace {args.Exception.StackTrace}");
				}
			}
		}
	}
}
