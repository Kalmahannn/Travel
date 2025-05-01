using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;

namespace Travel.AppFilter
{
	public class IEFilter : Attribute, IResourceFilter
	{
		public void OnResourceExecuted(ResourceExecutedContext context)
		{
			throw new NotImplementedException();
		}


		public void OnResourceExecuting(ResourceExecutingContext context)
		{

			string userAgent = context.HttpContext
									  .Request
									  .Headers["user-agent"].ToString();

			if (userAgent.Contains("Safari"))
			{
				context.Result = new ContentResult()
				{
					Content = "Ваш браузер устарел!"
				};
			}
		}
	}
}
