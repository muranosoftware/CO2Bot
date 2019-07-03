﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace EchoBot {
	public class Program {
		public static void Main(string[] args) {
			CreateWebHostBuilder(args).Build().Run();
		}

		public static IWebHostBuilder CreateWebHostBuilder(string[] args) {
			return WebHost.CreateDefaultBuilder(args)
			              .UseStartup<Startup>();
		}
	}
}