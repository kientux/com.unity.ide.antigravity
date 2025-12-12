/*---------------------------------------------------------------------------------------------
 *  Copyright (c) Microsoft Corporation. All rights reserved.
 *  Licensed under the MIT License. See License.txt in the project root for license information.
 *--------------------------------------------------------------------------------------------*/
using System;
using System.Linq;
using Unity.CodeEditor;

namespace Antigravity.Unity.Editor
{
	internal static class Cli
	{
		internal static void Log(string message)
		{
			// Use writeline here, instead of UnityEngine.Debug.Log to not include the stacktrace in the editor.log
			Console.WriteLine($"[Antigravity.Editor.{nameof(Cli)}] {message}");
		}

		internal static string GetInstallationDetails(IAntigravityInstallation installation)
		{
			return $"{installation.ToCodeEditorInstallation().Name} Path:{installation.Path}, LanguageVersionSupport:{installation.LatestLanguageVersionSupported} AnalyzersSupport:{installation.SupportsAnalyzers}";
		}

		internal static void GenerateSolutionWith(AntigravityEditor editor, string installationPath)
		{
			if (editor != null && editor.TryGetAntigravityInstallationForPath(installationPath, lookupDiscoveredInstallations: true, out var installation))
			{
				Log($"Using {GetInstallationDetails(installation)}");
				editor.SyncAll();
			}
			else
			{
				Log($"No Antigravity installation found in ${installationPath}!");
			}
		}

		internal static void GenerateSolution()
		{
			if (CodeEditor.CurrentEditor is AntigravityEditor editor)
			{
				Log($"Using default editor settings for Antigravity installation");
				GenerateSolutionWith(editor, CodeEditor.CurrentEditorInstallation);
			}
			else
			{
				Log($"Antigravity is not set as your default editor, looking for installations");
				try
				{
					var installations = Discovery
						.GetAntigravityInstallations()
						.Cast<AntigravityInstallation>()
						.OrderByDescending(i => !i.IsPrerelease)
						.ThenBy(i => i.Version)
						.ToArray();

					foreach(var installation in installations)
					{
						Log($"Detected {GetInstallationDetails(installation)}");
					}

					var firstInstallation = installations
							.FirstOrDefault();

					if (firstInstallation != null)
					{
						var current = CodeEditor.CurrentEditorInstallation;
						try
						{
							CodeEditor.SetExternalScriptEditor(firstInstallation.Path);
							GenerateSolutionWith(CodeEditor.CurrentEditor as AntigravityEditor, firstInstallation.Path);
						}
						finally
						{
							CodeEditor.SetExternalScriptEditor(current);
						}
					} else
					{
						Log($"No Antigravity installation found!");
					}
				}
				catch (Exception ex)
				{
					Log($"Error detecting Antigravity installations: {ex}");
				}
			}
		}
	}
}
