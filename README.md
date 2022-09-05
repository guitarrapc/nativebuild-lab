[![Build lz4](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-lz4.yaml/badge.svg)](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-lz4.yaml) [![Build zstd](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-zstd.yaml/badge.svg)](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-zstd.yaml)

# nativebuild-lab

This repository indicate build native binaries for following.

* zstd
* lz4
* mbedtls
* (upcoming....)

<!-- START doctoc generated TOC please keep comment here to allow auto update -->
<!-- DON'T EDIT THIS SECTION, INSTEAD RE-RUN doctoc TO UPDATE -->
**Table of Contents**

- [Build policy](#build-policy)
- [Getting started](#getting-started)
- [Summary](#summary)
  - [zstd](#zstd)
  - [lz4](#lz4)
  - [mbedtls](#mbedtls)
- [zstd](#zstd-1)
  - [Android](#android)
    - [Android (armeabi-v7a)](#android-armeabi-v7a)
    - [Android (arm64-v8a)](#android-arm64-v8a)
    - [Android (x86)](#android-x86)
    - [Android (x86_64)](#android-x86_64)
  - [iOS](#ios)
    - [iOS (arm64)](#ios-arm64)
  - [Linux](#linux)
    - [Linux (amd64)](#linux-amd64)
    - [Linux (aarch64)](#linux-aarch64)
  - [macOS](#macos)
    - [macOS (x86_64)](#macos-x86_64)
    - [macOS (arm64)](#macos-arm64)
  - [Windows](#windows)
    - [Windows (x64)](#windows-x64)
    - [Windows (x86)](#windows-x86)
    - [Windows (arm64)](#windows-arm64)
- [lz4](#lz4-1)
  - [Android](#android-1)
    - [Android (armeabi-v7a)](#android-armeabi-v7a-1)
    - [Android (arm64-v8a)](#android-arm64-v8a-1)
    - [Android (x86)](#android-x86-1)
    - [Android (x86_64)](#android-x86_64-1)
  - [iOS](#ios-1)
    - [iOS (arm64)](#ios-arm64-1)
  - [Linux](#linux-1)
    - [Linux (amd64)](#linux-amd64-1)
    - [Linux (aarch64)](#linux-aarch64-1)
  - [macOS](#macos-1)
    - [macOS (x86_64)](#macos-x86_64-1)
    - [macOS (arm64)](#macos-arm64-1)
  - [Windows](#windows-1)
    - [Windows (x64)](#windows-x64-1)
    - [Windows (x86)](#windows-x86-1)
    - [Windows (arm64)](#windows-arm64-1)
- [mbedtls](#mbedtls-1)
  - [Android](#android-2)
    - [Android (armeabi-v7a)](#android-armeabi-v7a-2)
    - [Android (arm64-v8a)](#android-arm64-v8a-2)
    - [Android (x86)](#android-x86-2)
    - [Android (x86_64)](#android-x86_64-2)
  - [iOS](#ios-2)
    - [iOS (arm64)](#ios-arm64-2)
  - [Linux](#linux-2)
    - [Linux (amd64)](#linux-amd64-2)
  - [macOS](#macos-2)
    - [macOS (x86_64)](#macos-x86_64-2)
    - [macOS (arm64)](#macos-arm64-2)
  - [Windows](#windows-2)
    - [Windows (x64)](#windows-x64-2)
    - [Windows (x86)](#windows-x86-2)
    - [Windows (arm64)](#windows-arm64-2)
- [REF](#ref)
  - [CMake](#cmake)
  - [mingw-w64](#mingw-w64)

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

## zstd

OS | Architecture | Build Env| Builder | Build Script | CI
---- | ---- | ---- | ---- | ---- | ----
Android | armeabi-v7a | Docker | CMake | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
Android | arm64-v8a   | Docker | CMake | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
Android | x86         | Docker | CMake | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
Android | x86_64      | Docker | CMake | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
iOS     | arm64 | Intel Mac <br/>Apple Silicon Mac | CMake | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
Linux   | arm64 | Docker | make | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
Linux   | x64   | Docker | make | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
macOS   | arm64 | Intel Mac <br/>Apple Silicon Mac | make | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
macOS   | x64   | Intel Mac <br/>Apple Silicon Mac | make | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
Windows | arm64 | Windows <br/>Docker | CMake <br/>mingw-w64 | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
Windows | x64   | Windows <br/>Docker | CMake <br/>mingw-w64 | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)
Windows | x86   | Windows <br/>Docker | CMake <br/>mingw-w64 | [builder/zstd/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/zstd/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/zstd-build.yaml)

## lz4

OS | Architecture | Build Env| Builder | Build Script | CI
---- | ---- | ---- | ---- | ---- | ----
Android | armeabi-v7a | Docker | make | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/lz4-build.yaml)
Android | arm64-v8a   | Docker | make | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/lz4-build.yaml)
Android | x86         | Docker | make | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/lz4-build.yaml)
Android | x86_64      | Docker | make | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/lz4-build.yaml)
iOS     | arm64 | Intel Mac <br/>Apple Silicon Mac | make | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/lz4-build.yaml)
Linux   | arm64 | Docker | make | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/lz4-build.yaml)
Linux   | x64   | Docker | make | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/lz4-build.yaml)
macOS   | arm64 | Intel Mac <br/>Apple Silicon Mac | make | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/lz4-build.yaml)
macOS   | x64   | Intel Mac <br/>Apple Silicon Mac | make | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/lz4-build.yaml)
Windows | arm64 | Windows | CMake | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/lz4-build.yaml)
Windows | x64   | Windows | CMake | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/lz4-build.yaml)
Windows | x86   | Windows | CMake | [builder/lz4/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/lz4/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/lz4-build.yaml)

## mbedtls

OS | Architecture | Build Env| Builder | Build Script | CI
---- | ---- | ---- | ---- | ---- | ----
Android | armeabi-v7a | Docker | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/mbedtls-build.yaml)
Android | arm64-v8a   | Docker | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/mbedtls-build.yaml)
Android | x86         | Docker | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/mbedtls-build.yaml)
Android | x86_64      | Docker | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/mbedtls-build.yaml)
iOS     | arm64 | Intel Mac <br/>Apple Silicon Mac | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/mbedtls-build.yaml)
Linux   | arm64 | Docker | make | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/mbedtls-build.yaml)
Linux   | x64   | Docker | make | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/mbedtls-build.yaml)
macOS   | arm64 | Intel Mac <br/>Apple Silicon Mac | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/mbedtls-build.yaml)
macOS   | x64   | Intel Mac <br/>Apple Silicon Mac | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/mbedtls-build.yaml)
Windows | arm64 | Windows | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/mbedtls-build.yaml)
Windows | x64   | Windows | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/mbedtls-build.yaml)
Windows | x86   | Windows | CMake | [builder/mbedtls/core](https://github.com/guitarrapc/nativebuild-lab/tree/main/builder/mbedtls/core) | [GitHub Actions](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/mbedtls-build.yaml)

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

**(Notice) Patch**

We need patch to build Shared Lib with export symbol on Windows.
Please apply following patch before build. (Run patch on git shell)

> **Note**: This patch also contains `mbedtls_wrapper.c` to access from other Programing language through Shared Lib.

```bash
cd mbedtls
patch -p1 < ../builder/mbedtls/windows.patch
```

TIPS. To create patch, spply changes to submodule then run following command.

```bash
cd mbedtls
git add -N .
git diff > ../builder/mbedtls/windows.patch
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

# REF

## CMake

**Basics**

* CMake Windows: [Visual Studio 17 2022 — CMake 3\.24\.1 Documentation](https://cmake.org/cmake/help/latest/generator/Visual%20Studio%2017%202022.html)

**Native Platform**

* CMake Android: [CMake  \|  Android NDK  \|  Android Developers](https://developer.android.com/ndk/guides/cmake?hl=ja)
* CMake iOS: [cmake\-toolchains\(7\) — CMake 3\.24\.1 Documentation](https://cmake.org/cmake/help/latest/manual/cmake-toolchains.7.html#cross-compiling-for-ios-tvos-or-watchos)

Other way to build.

* Android CMake build: [leleliu008/ndk\-pkg](https://github.com/leleliu008/ndk-pkg)
* iOS Cmake build: [leleliu008/xcpkg](https://github.com/leleliu008/xcpkg)

## mingw-w64

* Linux mingw-w64 amd64 build: [Downloads \- MinGW\-w64](https://www.mingw-w64.org/downloads/)
* Linux mingw-w64 arm64 build: [mstorsjo/llvm\-mingw](https://github.com/mstorsjo/llvm-mingw)
