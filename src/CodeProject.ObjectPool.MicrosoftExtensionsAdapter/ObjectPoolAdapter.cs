﻿// File name: ObjectPoolAdapter.cs
//
// Author(s): Alessio Parma <alessio.parma@gmail.com>
//
// The MIT License (MIT)
//
// Copyright (c) 2013-2018 Alessio Parma <alessio.parma@gmail.com>
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and
// associated documentation files (the "Software"), to deal in the Software without restriction,
// including without limitation the rights to use, copy, modify, merge, publish, distribute,
// sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or
// substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT
// NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
// NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM,
// DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT
// OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.

using System;

namespace CodeProject.ObjectPool.MicrosoftExtensionsAdapter
{
    /// <summary>
    ///   Adapts an <see cref="IObjectPool{T}"/> implementation to
    ///   <see cref="Microsoft.Extensions.ObjectPool.ObjectPool{T}"/> abstract class.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ObjectPoolAdapter<T> : Microsoft.Extensions.ObjectPool.ObjectPool<T>
        where T : PooledObject
    {
        private readonly IObjectPool<T> _adaptedObjectPool;

        /// <summary>
        ///   Adapts given object pool.
        /// </summary>
        /// <param name="objectPool">The object pool that needs to be adapted.</param>
        public ObjectPoolAdapter(IObjectPool<T> objectPool)
        {
            _adaptedObjectPool = objectPool ?? throw new ArgumentNullException(nameof(objectPool));
        }

        /// <summary>
        ///   Retrieves an object from the pool.
        /// </summary>
        /// <returns>An object from the pool.</returns>
        public override T Get() => _adaptedObjectPool.GetObject();

        /// <summary>
        ///   Returns given object to the pool.
        /// </summary>
        /// <param name="obj">The object that should return to the pool.</param>
        public override void Return(T obj) => obj?.Dispose();
    }
}