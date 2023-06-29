﻿using FluentAssertions;
using Imageverse.Api;
using Microsoft.AspNetCore.Mvc.Testing;

namespace Imageverse.Application.IntegrationTests.Packages.GetAllPackages
{
	public class GetAllPackagesTests
	{
		private readonly HttpClient _httpClient;
		public GetAllPackagesTests()
		{
			var webApplicationFactory = new WebApplicationFactory<Program>();
			_httpClient = webApplicationFactory.CreateDefaultClient();
		}

		[Fact]
		public async Task GetAllHashtags_ValidRequest_ReturnsListOfHashtags()
		{
			var response = await _httpClient.GetAsync("/Package");
			response.IsSuccessStatusCode.Should().BeTrue();
			var result = await response.Content.ReadAsStringAsync();
			result.Should().NotBeNullOrWhiteSpace();
		}
	}
}
