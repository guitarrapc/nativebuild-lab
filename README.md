[![Build mbedtls](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-fibo.yaml/badge.svg)](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-fibo.yaml) [![Build lz4](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-lz4.yaml/badge.svg)](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-lz4.yaml) [![Build mbedtls](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-mbedtls.yaml/badge.svg)](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-mbedtls.yaml) [![Build zstd](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-zstd.yaml/badge.svg)](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-zstd.yaml)

# nativebuild-lab

This repository indicate build native binaries for following.

* lz4
* mbedtls
* zstd
* (upcoming....)

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
**Table of Contents**

- [Build policy](#build-policy)
- [Getting started](#getting-started)
- [Summary](#summary)
  - [lz4](#lz4)
  - [mbedtls](#mbedtls)
  - [zstd](#zstd)
- [lz4](#lz4-1)
  - [Android](#android)
  - [iOS](#ios)
  - [Linux](#linux)
  - [macOS](#macos)
  - [Windows](#windows)
- [mbedtls](#mbedtls-1)
  - [Android](#android-1)
  - [iOS](#ios-1)
  - [Linux](#linux-1)
  - [macOS](#macos-1)
  - [Windows](#windows-1)
- [zstd](#zstd-1)
  - [Android](#android-2)
  - [iOS](#ios-2)
  - [Linux](#linux-2)
  - [macOS](#macos-2)
  - [Windows](#windows-2)
- [TIPS](#tips)
  - [CMake](#cmake)
  - [mingw-w64](#mingw-w64)
  - [Rename Shared Lib after build](#rename-shared-lib-after-build)
  - [cmd and bash redirection](#cmd-and-bash-redirection)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

# Build policy

This repository build with following policy.

* Linux: Use package's standard build. If nothing prefered then use `make` when available.
* Windows: Use package's standard build. If nothing prefered then use `CMake` (w/VC++) when available. Alternative is `mingw-w64` on Docker.
* macOS: Use package's standard build. If nothing prefered then use `make` when available.
* iOS: Use package's standard build. If nothing prefered then use `CMake` when availble.
* Android: Use `CMake` to build.

# Getting started

Both Local Machine and GitHub Actions supported.
Checkout repository, then run your build.

```bash
git clone https://github.com/guitarrapc/nativebuild-lab
git submodule update --init --recursive

# run your build for packages. see below instructions.
```

> **Note**
>
> Local Machine OS many have limit with...
> * Linux and Windows can build without macOS & iOS platform.
> * macOS can build for all platform.

# Summary

## lz4

OS | Architecture | Build Env| Builder | Build Script | CI
---- | ---- | ---- | ---- | ---- | ----
Android | armeabi-v7a | Docker | make | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-lz4.yaml)
Android | arm64-v8a   | Docker | make | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-lz4.yaml)
Android | x86         | Docker | make | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-lz4.yaml)
Android | x86_64      | Docker | make | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-lz4.yaml)
iOS     | arm64 | Intel Mac <br/>Apple Silicon Mac | make | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-lz4.yaml)
Linux   | arm64 | Docker | make | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-lz4.yaml)
Linux   | x64   | Docker | make | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-lz4.yaml)
macOS   | arm64 | Intel Mac <br/>Apple Silicon Mac | make | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-lz4.yaml)
macOS   | x64   | Intel Mac <br/>Apple Silicon Mac | make | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-lz4.yaml)
Windows | arm64 | Windows | CMake | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-lz4.yaml)
Windows | x64   | Windows | CMake | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-lz4.yaml)
Windows | x86   | Windows | CMake | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-lz4.yaml)

## mbedtls

OS | Architecture | Build Env| Builder | Build Script | CI
---- | ---- | ---- | ---- | ---- | ----
Android | armeabi-v7a | Docker | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-mbedtls.yaml)
Android | arm64-v8a   | Docker | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-mbedtls.yaml)
Android | x86         | Docker | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-mbedtls.yaml)
Android | x86_64      | Docker | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-mbedtls.yaml)
iOS     | arm64 | Intel Mac <br/>Apple Silicon Mac | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-mbedtls.yaml)
Linux   | arm64 | Docker | CMake<br/>make | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-mbedtls.yaml)
Linux   | x64   | Docker | CMake<br/>make | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-mbedtls.yaml)
macOS   | arm64 | Intel Mac <br/>Apple Silicon Mac | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-mbedtls.yaml)
macOS   | x64   | Intel Mac <br/>Apple Silicon Mac | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-mbedtls.yaml)
Windows | arm64 | Windows | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-mbedtls.yaml)
Windows | x64   | Windows | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-mbedtls.yaml)
Windows | x86   | Windows | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-mbedtls.yaml)

## zstd

OS | Architecture | Build Env| Builder | Build Script | CI
---- | ---- | ---- | ---- | ---- | ----
Android | armeabi-v7a | Docker | CMake | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-zstd.yaml)
Android | arm64-v8a   | Docker | CMake | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-zstd.yaml)
Android | x86         | Docker | CMake | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-zstd.yaml)
Android | x86_64      | Docker | CMake | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-zstd.yaml)
iOS     | arm64 | Intel Mac <br/>Apple Silicon Mac | CMake | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-zstd.yaml)
Linux   | arm64 | Docker | make | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-zstd.yaml)
Linux   | x64   | Docker | make | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-zstd.yaml)
macOS   | arm64 | Intel Mac <br/>Apple Silicon Mac | make | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-zstd.yaml)
macOS   | x64   | Intel Mac <br/>Apple Silicon Mac | make | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-zstd.yaml)
Windows | arm64 | Windows <br/>Docker | CMake <br/>mingw-w64 | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-zstd.yaml)
Windows | x64   | Windows <br/>Docker | CMake <br/>mingw-w64 | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-zstd.yaml)
Windows | x86   | Windows <br/>Docker | CMake <br/>mingw-w64 | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-zstd.yaml)

# lz4

Building [lz4](https://github.com/lz4/lz4) for following environment.

## Android

use `make`.

### Android (armeabi-v7a)

```bash
bash ./builder/lz4/lz4-android-arm.sh
```

### Android (arm64-v8a)

```bash
bash ./builder/lz4/lz4-android-arm64.sh
```

### Android (x86)

```bash
bash ./builder/lz4/lz4-android-x86.sh
```

### Android (x86_64)

```bash
bash ./builder/lz4/lz4-android-x64.sh
```

## iOS

use `make` or `xcpkg`.

> **Note**: You must use macOS to build.

### iOS (arm64)

**make**

Install prerequisites.

```bash
brew install make
```

```bash
bash ./builder/lz4/lz4-ios-arm64.sh
```

## Linux

Use `make` to build.

### Linux (amd64)

```bash
# windows
builder\lz4\lz4-linux-x64.bat

# linux
bash ./builder/lz4/lz4-linux-x64.sh
```

### Linux (aarch64)

Use `make`.

```bash
# windows
builder\lz4\lz4-linux-arm64.bat

# linux
bash ./builder/lz4/lz4-linux-arm64.sh
```

## macOS

Use `make`.

> **Note**: You must use macOS to build.

### macOS (x86_64)

```bash
bash ./builder/lz4/lz4-darwin-x64.sh
```

### macOS (arm64)

```bash
bash ./builder/lz4/lz4-darwin-arm64.sh
```

## Windows

Use `cmake`.

**cmake** binaries are named `lz4*`.

> **Note**: You must use Windows to build.

> **Note**: `cmake` requires Windows Host to build. Also install Visual Studio 2022 `C++ Desktop Experience` package and `MSVC v143 - VS 2022 C++ ARM64 build tools` for cmake.

### Windows (x64)

```bash
builder\lz4\lz4-windows-x64.bat
```

### Windows (x86)

```bash
builder\lz4\lz4-windows-x86.bat
```

### Windows (arm64)

```bash
builder\lz4\lz4-windows-arm64.bat
```

# mbedtls

Building [mbedtls](https://github.com/Mbed-TLS/mbedtls#make) for following environment.

<hr/>

**(Notice) Windows Patch**

We need patch to build Shared Lib with export symbol on Windows.
Please apply following patch before build. (Run patch on git shell)

> **Note**: This patch contains `__declspec` to access from other Programing language through Windows Shared Lib, dll.

```bash
cd mbedtls
git apply ../builder/mbedtls/patch/windows.patch
```

TIPS. To create patch, spply changes to submodule then run following command.

```bash
cd mbedtls
git add -N .
git diff > ../builder/mbedtls/patch/windows.patch
```

<hr/>

**(Notice) Wrapper Patch**

We need patch to add wrapper class.
Please apply following patch before build. (Run patch on git shell)

> **Note**: This patch contains `mbedtls_wrapper.c` to access from other Programing language through Shared Lib.

```bash
cd mbedtls
git apply ../builder/mbedtls/patch/wrapper.patch
```

TIPS. To create patch, spply changes to submodule then run following command.

```bash
cd mbedtls
git add -N .
git diff > ../builder/mbedtls/patch/wrapper.patch
```

<hr/>

**(Notice) Filename Prefix Patch**

We need patch to support file name prefix.
https://github.com/Mbed-TLS/mbedtls/pull/3407 supports mbedtls to output shared lib with prefix, however missing static output.
Therefore static build will failed to reference `<prefix>mbedcrypto` on building mbedx509.

```bash
cd mbedtls
git apply ../builder/mbedtls/patch/prefix.patch
```

TIPS. To create patch, spply changes to submodule then run following command.

```bash
cd mbedtls
git add -N .
git diff > ../builder/mbedtls/patch/prefix.patch
```

## Android

use `make`.

### Android (armeabi-v7a)

```bash
bash ./builder/mbedtls/mbedtls-android-arm.sh
```

### Android (arm64-v8a)

```bash
bash ./builder/mbedtls/mbedtls-android-arm64.sh
```

### Android (x86)

```bash
bash ./builder/mbedtls/mbedtls-android-x86.sh
```

### Android (x86_64)

```bash
bash ./builder/mbedtls/mbedtls-android-x64.sh
```

## iOS

use `make`.

> **Note**: You must use macOS to build.

### iOS (arm64)

**make**

Install prerequisites.

```bash
brew install make
```

```bash
bash ./builder/mbedtls/mbedtls-ios-arm64.sh
```

## Linux

Use `make` to build.

### Linux (amd64)

```bash
# windows
builder\mbedtls\mbedtls-linux-x64.bat

# linux
bash ./builder/mbedtls/mbedtls-linux-x64.sh
```

### Linux (arm64)

```bash
# windows
builder\mbedtls\mbedtls-linux-arm64.bat

# linux
bash ./builder/mbedtls/mbedtls-linux-arm64.sh
```

## macOS

Use `cmake`.

> **Note**: You must use macOS to build.

### macOS (x86_64)

```bash
bash ./builder/mbedtls/mbedtls-darwin-x64.sh
```

### macOS (arm64)

```bash
bash ./builder/mbedtls/mbedtls-darwin-arm64.sh
```

## Windows

Use `cmake`.

**cmake** binaries are named `mbed*`.

> **Note**: You must use Windows to build.

> **Note**: `cmake` requires Windows Host to build. Also install Visual Studio 2022 `C++ Desktop Experience` package and `MSVC v143 - VS 2022 C++ ARM64 build tools` for cmake.

### Windows (x64)

```bash
builder\mbedtls\mbedtls-windows-x64.bat
```

### Windows (x86)

```bash
builder\mbedtls\mbedtls-windows-x86.bat
```

### Windows (arm64)

```bash
builder\mbedtls\mbedtls-windows-arm64.bat
```

# zstd

Building [zstd](https://github.com/facebook/zstd) for following environment.

## Android

use `cmake`.

### Android (armeabi-v7a)

```bash
bash ./builder/zstd/zstd-android-arm.sh
```

### Android (arm64-v8a)

```bash
bash ./builder/zstd/zstd-android-arm64.sh
```

### Android (x86)

```bash
bash ./builder/zstd/zstd-android-x86.sh
```

### Android (x86_64)

```bash
bash ./builder/zstd/zstd-android-x64.sh
```

## iOS

use `cmake` or `xcpkg`.

> **Note**: You must use macOS to build.

### iOS (arm64)

**cmake**

Install prerequisites.

```bash
brew install cmake ninja gsed tree
```

```bash
bash ./builder/zstd/zstd-ios-arm64.sh
```

## Linux

Use `make` to build.

### Linux (amd64)

```bash
# windows
builder\zstd\zstd-linux-x64.bat

# linux
bash ./builder/zstd/zstd-linux-x64.sh
```

### Linux (aarch64)

Use `make`.

```bash
# windows
builder\zstd\zstd-linux-arm64.bat

# linux
bash ./builder/zstd/zstd-linux-arm64.sh
```

## macOS

Use `make`.

> **Note**: You must use macOS to build.

### macOS (x86_64)

```bash
bash ./builder/zstd/zstd-darwin-x64.sh
```

### macOS (arm64)

```bash
bash ./builder/zstd/zstd-darwin-arm64.sh
```

## Windows

There are 2way to build.

1. cmake (primary)
2. make

**cmake** binaries are named `zstd*`, on the otherhand **make** binaries are `libzstd*`.

> **Note**: You must use Windows for `cmake`. Use `make` (=mingw) when using Linux or macOS.

> **Note**: `cmake` requires Windows Host to build. Also install Visual Studio 2022 `C++ Desktop Experience` package and `MSVC v143 - VS 2022 C++ ARM64 build tools` for cmake

### Windows (x64)

**cmake**

```bash
builder\zstd\zstd-windows-x64.bat
```

**make**

```bash
# windows
builder\zstd\zstd-windows-x64-mingw.bat

# linux
bash ./builder/zstd/zstd-windows-x64-mingw.sh
```

### Windows (x86)

**cmake**

```bash
builder\zstd\zstd-windows-x86.bat
```

**make**

```bash
# windows
builder\zstd\zstd-windows-x86-mingw.bat

# linux
bash ./builder/zstd/zstd-windows-x86-mingw.sh
```

### Windows (arm64)

**cmake**

```bash
builder\zstd\zstd-windows-arm64.bat
```

**make**

```bash
# windows
builder\zstd\zstd-windows-arm64-mingw.bat

# linux
bash ./builder/zstd/zstd-windows-arm64-mingw.sh
```

# TIPS

## CMake

**Basics**

* CMake Windows: [Visual Studio 17 2022 ??? CMake 3\.24\.1 Documentation](https://cmake.org/cmake/help/latest/generator/Visual%20Studio%2017%202022.html)

**CheatSheet**

* [cmake ?????????????????? \- Qoosky](https://www.qoosky.io/techs/814fda555d)

**Native Platform**

* CMake Android: [CMake ??\|?? Android NDK ??\|?? Android Developers](https://developer.android.com/ndk/guides/cmake?hl=ja)
* CMake iOS: [cmake\-toolchains\(7\) ??? CMake 3\.24\.1 Documentation](https://cmake.org/cmake/help/latest/manual/cmake-toolchains.7.html#cross-compiling-for-ios-tvos-or-watchos)

Other way to build.

* Android CMake build: [leleliu008/ndk\-pkg](https://github.com/leleliu008/ndk-pkg)
* iOS Cmake build: [leleliu008/xcpkg](https://github.com/leleliu008/xcpkg)

## mingw-w64

* Linux mingw-w64 amd64 build: [Downloads \- MinGW\-w64](https://www.mingw-w64.org/downloads/)
* Linux mingw-w64 arm64 build: [mstorsjo/llvm\-mingw](https://github.com/mstorsjo/llvm-mingw)

## Rename Shared Lib after build

**.so**

[linux \- How to rename a shared library to avoid same\-name conflict? \- Stack Overflow](https://stackoverflow.com/questions/19739828/how-to-rename-a-shared-library-to-avoid-same-name-conflict)

**.dylib**

When link .dylib and create staticlib .a, use `install_name_tool -id libmylib.dylib libmylib.dylib` if required.

> [macos \- Why can't I rename a shared library after it's been built? \- Stack Overflow](https://stackoverflow.com/questions/42877686/why-cant-i-rename-a-shared-library-after-its-been-built)


## cmd and bash redirection

Redirect output to check what happen with log.

```bash
foo.bat > error.log 2>&1
foo.bash > error.log 2>&1
```
