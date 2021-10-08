using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class FuncExtensions
{
    public static Func<T1, R> AndThen<T1, U, R>(this Func<T1, U> func, Func<U, R> andThen)
        => (x) => andThen(func(x));
}