﻿// File name: MemoryStreamPooling.cs
//
// Author(s): Alessio Parma <alessio.parma@gmail.com>
//
// The MIT License (MIT)
//
// Copyright (c) 2013-2016 Alessio Parma <alessio.parma@gmail.com>
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

using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Jobs;
using CodeProject.ObjectPool.Specialized;

namespace CodeProject.ObjectPool.Benchmarks
{
    [Config(typeof(Config))]
    public class MemoryStreamPooling
    {
        private readonly IObjectPool<PooledMemoryStream> _objectPool = Specialized.MemoryStreamPool.Instance;
        private readonly Microsoft.IO.RecyclableMemoryStreamManager _recManager = new Microsoft.IO.RecyclableMemoryStreamManager();

        private class Config : ManualConfig
        {
            public Config()
            {
                Add(Job.LegacyJitX86);
                Add(CsvExporter.Default, HtmlExporter.Default, MarkdownExporter.GitHub, PlainExporter.Default, CsvMeasurementsExporter.Default, RPlotExporter.Default);
                Add(new MemoryDiagnoser());
                Add(EnvironmentAnalyser.Default);
            }
        }

        [Benchmark]
        public long MemoryStreamPool()
        {
            long l;
            using (var x = _objectPool.GetObject())
            {
                l = x.MemoryStream.Length;
            }
            return l;
        }

        [Benchmark]
        public long RecyclableMemoryStreamManager()
        {
            long l;
            using (var x = _recManager.GetStream())
            {
                l = x.Length;
            }
            return l;
        }
    }
}