﻿using Avalonia.Media;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace PleasantUI.ToolKit.Messages;

/// <summary>
/// Represents a message that asynchronously requests the color from the clipboard.
/// </summary>
/// <seealso cref="AsyncRequestMessage{T}" />
public class AsyncRequestClipboardColorMessage : AsyncRequestMessage<Color?>;