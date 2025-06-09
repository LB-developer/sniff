# 🕵️ Sniff

**Sniff** is a lightweight, extensible CLI tool that inspects your local developer environment for common setup issues and safety concerns — before you commit, push, or deploy.

> Think: `git`, `curl`, or `npx` — but focused on helping you debug and stay safe with one quick command.


## 🧭 Usage

```bash
sniff [command]
```

Available Commands:

    sniff git 
        — Parse and summarize git status -sb

    sniff -h or sniff --help 
        — Show help message

### Example

```bash
sniff git
```

Output:

```
------------ Sniff Report for {current_location} ------------
Branch: main
Uncommitted changes?: False
Untracked files?: True
Branch is in sync with remote
```

---

## 🚀 Features

- 🔍 `sniff git` — Inspect your current Git working state
  - Branch status
  - Uncommitted changes
  - Untracked files
  - Remote sync (ahead/behind)
- 🧱 Modular command execution and parsing
- ✅ Designed for easy extension and NUnit testing

---

## 🚀 Running Locally

From the `Sniff/` project directory

```bash
dotnet run
```

This will build and run the tool in-place using the .NET SDK.

---

## 🛠️ Building for Other Platforms

You can build for other OS targets:

```bash
dotnet publish -c Release -r linux-x64 --self-contained true -p:PublishSingleFile=true -o ./dist-linux

# macOS
dotnet publish -c Release -r osx-x64 --self-contained true -p:PublishSingleFile=true -o ./dist-macos

# Windows
dotnet publish -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -o ./dist-win
```

You’ll find the executable in the respective output folders.

---

## 📋 Roadmap

- [x] Parse and summarize `git status -sb`
- [ ] Count staged, modified, deleted, untracked files
- [ ] Display relevant environment variables
- [ ] Add branch tracking summary
- [ ] Add flag options for filtering

---

## 🧪 Running Tests

```bash
dotnet test Tests
```

Make sure `.NET 8+` is installed.

---

## 📄 License

Apache 2.0
