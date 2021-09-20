﻿// Copyright (c) Amer Koleci and contributors.
// Distributed under the MIT license. See the LICENSE file in the project root for more information.

#if !NET5_0_OR_GREATER
using System;
using System.Runtime.InteropServices;

namespace Vortice.ShaderCompiler
{
    internal static class PlatformConfiguration
    {
        public static bool IsUnix { get; }

        public static bool IsWindows { get; }

        public static bool IsMac { get; }

        public static bool IsLinux { get; }

        public static bool IsArm { get; }

        public static bool Is64Bit { get; }

        static PlatformConfiguration()
        {
#if WINDOWS_UWP
			IsMac = false;
			IsLinux = false;
			IsUnix = false;
			IsWindows = true;

			var arch = Package.Current.Id.Architecture;
			const ProcessorArchitecture arm64 = (ProcessorArchitecture)12;
			IsArm = arch == ProcessorArchitecture.Arm || arch == arm64;
#else
            IsMac = RuntimeInformation.IsOSPlatform(OSPlatform.OSX);
            IsLinux = RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
            IsUnix = IsMac || IsLinux;
            IsWindows = RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

            var arch = RuntimeInformation.ProcessArchitecture;
            IsArm = arch == Architecture.Arm || arch == Architecture.Arm64;
#endif

            Is64Bit = IntPtr.Size == 8;
        }

        private static string? linuxFlavor;

        public static string? LinuxFlavor
        {
            get
            {
                if (!IsLinux)
                    return null;

                if (!string.IsNullOrEmpty(linuxFlavor))
                    return linuxFlavor;

                // we only check for musl/glibc right now
                if (!IsGlibc)
                    return "musl";

                return null;
            }
            set => linuxFlavor = value;
        }

#if WINDOWS_UWP
		public static bool IsGlibc { get; }
#else
        private static readonly Lazy<bool> s_isGlibcLazy = new Lazy<bool>(IsGlibcImplementation);

        public static bool IsGlibc => IsLinux && s_isGlibcLazy.Value;

        private static bool IsGlibcImplementation()
        {
            try
            {
                gnu_get_libc_version();
                return true;
            }
            catch (TypeLoadException)
            {
                return false;
            }
        }

        [DllImport("c", ExactSpelling = true, CallingConvention = CallingConvention.Cdecl)]
        private static extern IntPtr gnu_get_libc_version();
#endif
    }
}
#endif