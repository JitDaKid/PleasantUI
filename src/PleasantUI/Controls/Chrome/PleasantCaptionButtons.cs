﻿using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Metadata;
using Avalonia.Controls.Primitives;
using PleasantUI.Reactive;

namespace PleasantUI.Controls.Chrome;

/// <summary>
/// Represents a set of caption buttons for a <see cref="PleasantWindow" />.
/// </summary>
[PseudoClasses(":minimized", ":normal", ":maximized", ":isactive")]
[TemplatePart("PART_CloseButton", typeof(Button))]
[TemplatePart("PART_MaximizeButton", typeof(Button))]
[TemplatePart("PART_MinimizeButton", typeof(Button))]
public class PleasantCaptionButtons : TemplatedControl
{
    /// <summary>
    /// Represents the types of pleasant caption buttons.
    /// </summary>
    public enum Type
    {
        /// <summary>
        /// All buttons are visible (Close, Collapse, Expand).
        /// </summary>
        All = 0,

        /// <summary>
        /// Close and Collapse buttons are visible.
        /// </summary>
        CloseAndCollapse = 1,

        /// <summary>
        /// Close and Expand buttons are visible.
        /// </summary>
        CloseAndExpand = 2,

        /// <summary>
        /// Only the Close button is visible.
        /// </summary>
        Close = 3,

        /// <summary>
        /// No buttons are visible.
        /// </summary>
        None = 4
    }
    
    private Button? _closeButton;
    private CompositeDisposable? _disposable;

    private Button? _maximizeButton;
    private Button? _minimizeButton;

    /// <summary>
    /// Gets or sets the host window for these caption buttons.
    /// </summary>
    public PleasantWindow? Host { get; set; }

    /// <inheritdoc />
    protected override void OnApplyTemplate(TemplateAppliedEventArgs e)
    {
        base.OnApplyTemplate(e);

        e.NameScope.Get<Button>("PART_CloseButton").Click += (_, _) => Host?.Close();

        _maximizeButton = e.NameScope.Get<Button>("PART_MaximizeButton");
        _minimizeButton = e.NameScope.Get<Button>("PART_MinimizeButton");
        _closeButton = e.NameScope.Get<Button>("PART_CloseButton");

        _maximizeButton.Click += (_, _) =>
        {
            if (Host is null) return;

            Host.WindowState = Host.WindowState == WindowState.Maximized ? WindowState.Normal : WindowState.Maximized;
        };

        _minimizeButton.Click += (_, _) =>
        {
            if (Host is null) return;

            Host.WindowState = WindowState.Minimized;
        };

        if (_disposable is null && Host is not null)
            _disposable = new CompositeDisposable
            {
                Host.GetObservable(Window.WindowStateProperty).Subscribe(x =>
                {
                    PseudoClasses.Set(":minimized", x == WindowState.Minimized);
                    PseudoClasses.Set(":normal", x == WindowState.Normal);
                    PseudoClasses.Set(":maximized", x == WindowState.Maximized);
                }),
                Host.GetObservable(Window.CanResizeProperty).Subscribe(x => { _maximizeButton.IsEnabled = x; }),
                Host.GetObservable(WindowBase.IsActiveProperty).Subscribe(x => { PseudoClasses.Set(":isactive", !x); }),
                Host.GetObservable(PleasantWindow.CaptionButtonsProperty).Subscribe(x =>
                {
                    if (x == Type.None)
                    {
                        _minimizeButton.IsVisible = false;
                        _maximizeButton.IsVisible = false;
                        _closeButton.IsVisible = false;

                        return;
                    }

                    if (x == Type.All)
                    {
                        _minimizeButton.IsVisible = true;
                        _maximizeButton.IsVisible = true;
                        _closeButton.IsVisible = true;

                        return;
                    }

                    if (x == Type.Close)
                    {
                        _minimizeButton.IsVisible = false;
                        _maximizeButton.IsVisible = false;
                        _closeButton.IsVisible = true;

                        return;
                    }

                    if (x == Type.CloseAndCollapse)
                    {
                        _minimizeButton.IsVisible = true;
                        _maximizeButton.IsVisible = false;
                        _closeButton.IsVisible = true;

                        return;
                    }

                    if (x == Type.CloseAndExpand)
                    {
                        _minimizeButton.IsVisible = false;
                        _maximizeButton.IsVisible = true;
                        _closeButton.IsVisible = true;
                    }
                })
            };
    }

    /// <summary>
    /// Detaches the object from its host by disposing the disposable object and setting it to null.
    /// Also sets the host object to null.
    /// </summary>
    public void Detach()
    {
        if (_disposable is null) return;

        _disposable.Dispose();
        _disposable = null;
        Host = null;
    }
}