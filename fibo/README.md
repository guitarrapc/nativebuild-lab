fibo

# Summary

An sample library, cli to calculate fibonacci.

# How to build

This program supports `make` to build programs.

## Makefile

use `make` to build static library and dynamic library.

```bash
make clean
make
```
You can build cli with followings.

```bash
make clean
make program
```

Use `all` to build all.

```bash
make clean
make all
```

## CMake

```bash
# Windows
rmdir /Q /S examples\build
mkdir examples\build
cd examples\build
cmake .. -G"Visual Studio 17 2022"
cmake --build .
```

Windows

```bash
builder\fibo\windows-arm64.bat
builder\fibo\windows-x64.bat
builder\fibo\windows-x86.bat
```

Linux

```bash
builder/fibo/linux-arm64.bat
builder/fibo/linux-x64.bat
```
