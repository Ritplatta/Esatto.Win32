﻿using System;
using System.Reflection;

namespace Esatto.Win32.NetInjector
{
    public struct EntryPointReference
    {
        public string AssemblyPath { get; private set; }
        public string TypeName { get; private set; }
        public string MethodName { get; private set; }

        public EntryPointReference(string path, string type, string method)
        {
            if (string.IsNullOrWhiteSpace(path) || path.IndexOf('\0') >= 0)
            {
                throw new ArgumentNullException(nameof(path));
            }
            if (string.IsNullOrWhiteSpace(type) || type.IndexOf('\0') >= 0)
            {
                throw new ArgumentNullException(nameof(type));
            }
            if (string.IsNullOrWhiteSpace(method) || method.IndexOf('\0') >= 0)
            {
                throw new ArgumentNullException(nameof(method));
            }

            this.AssemblyPath = path;
            this.TypeName = type;
            this.MethodName = method;
        }

        public EntryPointReference(Type t, string method)
            : this(t.Assembly.Location!, t.FullName, method)
        {
            // nop
        }

        public EntryPointReference(MethodInfo method)
            : this(method.DeclaringType, method.Name)
        {
            // nop
        }

        public EntryPointReference(Func<string, int> method)
            : this(method.Method)
        {
            // nop
        }
    }
}
