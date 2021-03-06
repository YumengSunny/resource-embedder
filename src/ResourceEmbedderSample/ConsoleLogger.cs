﻿using ResourceEmbedder.Core;
using System;

namespace ResourceEmbedderSample
{
    public class ConsoleLogger : ILogger
    {
        #region Fields

        private int _indent;

        #endregion Fields

        #region Methods

        public void Debug(string message, params object[] args)
        {
            LogColored("Debug: " + string.Format(message, args), ConsoleColor.Blue);
        }

        public void Error(string message, params object[] args)
        {
            LogColored("Error: " + string.Format(message, args), ConsoleColor.Red);
        }

        public void Indent(int level)
        {
            _indent = level;
        }

        public void Info(string message, params object[] args)
        {
            LogColored(string.Format(message, args));
        }

        public void Warning(string message, params object[] args)
        {
            LogColored("Warning: " + string.Format(message, args), ConsoleColor.Yellow);
        }

        /// <summary>
        /// Outputs the specific message with the provided color.
        /// If no color provided leaves the systems default color.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="color">The color to use for printing. After printing the color will revert back to the previous one automatically.</param>
        private void LogColored(string message, ConsoleColor? color = null)
        {
            // swap to new color, then revert back to old after printing message
            var old = Console.ForegroundColor;
            if (color.HasValue)
            {
                Console.ForegroundColor = color.Value;
            }
            Console.WriteLine(new string('\t', _indent) + message);
            Console.ForegroundColor = old;
        }

        #endregion Methods
    }
}
