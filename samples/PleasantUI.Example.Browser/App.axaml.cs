﻿using System;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;

namespace PleasantUI.Example.Browser;

public partial class App : PleasantUiExampleApp
{
    public override void Initialize() => AvaloniaXamlLoader.Load(this);
    
    public override void OnFrameworkInitializationCompleted()
    {
        PleasantTheme = Styles[0] as PleasantTheme ?? throw new NullReferenceException("PleasantTheme is null");
        
        if (ApplicationLifetime is ISingleViewApplicationLifetime lifetime)
        {
            lifetime.MainView = new PleasantMainView
            {
                DataContext = ViewModel
            };
        }
        
        base.OnFrameworkInitializationCompleted();
    }
}