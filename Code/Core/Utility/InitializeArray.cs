﻿namespace Core;

public static partial class Utility
{
    public static T[] InitializeArray<T>(int length) where T : new()
    {
        T[] array = new T[length];
        for (var i = 0; i < length; ++i) array[i] = new T();

        return array;
    }
}
