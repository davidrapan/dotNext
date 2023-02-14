﻿using System.Diagnostics;

namespace DotNext.Threading.Channels;

using IO;

internal interface IChannelWriter<T> : IChannel
{
    private const string InputTypeMeterAttribute = "dotnext.threading.channels.persistentchannel.input";

    private protected static void SetTags(ref TagList tags)
        => tags.Add(InputTypeMeterAttribute, typeof(T).FullName);

    void MessageReady();

    ValueTask SerializeAsync(T input, Partition output, CancellationToken token);

    bool TryComplete(Exception? e = null);
}