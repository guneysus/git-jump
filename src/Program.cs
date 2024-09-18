using System;
using Spectre.Console;
using CliWrap;
using System.Text;

var stdOutBuffer = new StringBuilder();

/*
git branch --list --all --no-color --format="%(refname:short)"
 */
_ = await Cli.Wrap("git")
    .WithArguments(new string[] { 
        "branch",
        "--list",
        "--all",
        "--no-color",
        "--format=\"%(refname:short)\""
    }, false)
    .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
    .ExecuteAsync();

var branches = stdOutBuffer
    .ToString()
    .Split('\n', StringSplitOptions.RemoveEmptyEntries);

if (branches.Length <= 1) return;

// Ask for the branch
var branch = AnsiConsole.Prompt(
    new SelectionPrompt<string>()
        .AddChoices(branches))
    .Replace("*", string.Empty)
    .Trim();

await Cli.Wrap("git")
    .WithArguments(new string[] {
        "switch",
        branch
    })
    .WithStandardOutputPipe(PipeTarget.ToStringBuilder(stdOutBuffer))
    .ExecuteAsync();
