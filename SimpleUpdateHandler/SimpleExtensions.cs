﻿using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;

namespace SimpleUpdateHandler
{
    public static class SimpleExtensions
    {
        public static object GetInnerUpdate(this Update update)
        {
            if (update.Type == UpdateType.Unknown)
                throw new ArgumentException($"Can't resolve Update of Type {update.Type}");

            return typeof(Update).GetProperty(update.Type.ToString())?.GetValue(update, null)
                ?? throw new InvalidOperationException($"Inner update is null for {update.Type}");
        }

        public static T GetInnerUpdate<T>(this Update update)
        {
            if (update.Type == UpdateType.Unknown)
                throw new ArgumentException($"Can't resolve Update of Type {update.Type}");

            return (T)(typeof(Update).GetProperty(update.Type.ToString())?.GetValue(update, null)
                ?? throw new InvalidOperationException($"Inner update is null for {update.Type}"));
        }
    }
}