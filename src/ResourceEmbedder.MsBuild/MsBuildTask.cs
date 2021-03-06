﻿using Microsoft.Build.Framework;
using System;
using System.IO;

namespace ResourceEmbedder.MsBuild
{
    /// <summary>
    /// Base class for all resource embedder msbuild tasks.
    /// </summary>
    public abstract class MsBuildTask : Microsoft.Build.Utilities.Task
    {
        #region Properties

        [Required]
        public string AssemblyPath { set; get; }

        [Required]
        public string ProjectDirectory { get; set; }

        public bool SignAssembly { get; set; }

        public string KeyFilePath { get; set; }

        public string IntermediateDirectory { get; set; }

        public string TargetPath { get; set; }

        public bool DebugSymbols { get; set; }

        public string DebugType { get; set; }

        /// <summary>
        /// A semicolon seperated list of reference files.
        /// All path are full path to the reference dll files.
        /// </summary>
        public string References { get; set; }

        #endregion Properties

        #region Methods

        protected bool AssertSetup(ResourceEmbedder.Core.ILogger logger)
        {
            // override value of debug symbols based on DebugType as it takes precedence
            DebugSymbols = HasDebugSymbols();
            if (!Directory.Exists(ProjectDirectory))
            {
                logger.Error("Project directory '{0}' does not exist.", ProjectDirectory);
                return false;
            }
            var asm = Path.Combine(ProjectDirectory, AssemblyPath);
            if (!File.Exists(asm))
            {
                logger.Error("Assembly '{0}' not found", asm);
                return false;
            }
            return true;
        }

        private bool HasDebugSymbols() => !string.IsNullOrEmpty(DebugType) &&
                                          !string.Equals(DebugType, "none", StringComparison.OrdinalIgnoreCase);

        #endregion Methods
    }
}
