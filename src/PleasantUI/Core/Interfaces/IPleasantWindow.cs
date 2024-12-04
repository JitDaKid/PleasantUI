﻿using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using PleasantUI.Controls;

namespace PleasantUI.Core.Interfaces;

/// <summary>
/// Represents a PleasantUI window with specific functionalities.
/// </summary>
public interface IPleasantWindow
{
    /// <summary>
    /// Gets the snackbar queue manager for pleasant snackbars.
    /// </summary>
    SnackbarQueueManager<PleasantSnackbar> SnackbarQueueManager { get; }

    /// <summary>
    /// Gets the collection of modal windows associated with this window.
    /// </summary>
    AvaloniaList<PleasantModalWindow> ModalWindows { get; }
}