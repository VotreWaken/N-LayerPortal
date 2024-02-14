using Microsoft.AspNetCore.Mvc.Testing;
using RazorPagesProject.Tests;
using Xunit;

namespace MusicPortal.IntegrationTests
{
	public class IndexPageTests :
		IClassFixture<CustomWebApplicationFactory<Program>>
	{
		private readonly HttpClient _client;
		private readonly CustomWebApplicationFactory<Program> _factory;

		public IndexPageTests(CustomWebApplicationFactory<Program> factory)
		{
			_factory = factory;
			_client = factory.CreateClient(new WebApplicationFactoryClientOptions
			{
				AllowAutoRedirect = false
			});
		}


	}
}
