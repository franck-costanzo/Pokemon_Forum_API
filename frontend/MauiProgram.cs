﻿using Smogon_MAUIapp.Entities;
using Smogon_MAUIapp.Pages;
using Smogon_MAUIapp.Tools;
using Microsoft.Extensions.Configuration;
using Microsoft.Maui;
using Microsoft.Maui.Hosting;

namespace Smogon_MAUIapp;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
            .UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
				fonts.AddFont("Roboto-Regular.ttf", "RobotoRegular");
				fonts.AddFont("Roboto-Bold.ttf", "RobotoBold");
				fonts.AddFont("Unown", "Unown");
			});

        return builder.Build();
	}
}
