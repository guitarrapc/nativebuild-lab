[![Build mbedtls](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-fibo.yaml/badge.svg)](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-fibo.yaml)
[![Build lz4](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-lz4.yaml/badge.svg)](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-lz4.yaml)
[![Build mbedtls](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-mbedtls.yaml/badge.svg)](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-mbedtls.yaml)
[![Build zstd](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-zstd.yaml/badge.svg)](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-zstd.yaml)

# nativebuild-lab

This repository indicate build native binaries for following.

- lz4
- mbedtls
- zstd
- (upcoming....)

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
  - [(Notice) Windows Build](#notice-windows-build)
- [mbedtls](#mbedtls-1)
  - [(Notice) Windows Build](#notice-windows-build-1)
  - [(Notice) Windows Patch](#notice-windows-patch)
  - [(Notice) Wrapper Patch](#notice-wrapper-patch)
  - [(Notice) Filename Prefix Patch](#notice-filename-prefix-patch)
- [zstd](#zstd-1)
  - [(Notice) Windows Build](#notice-windows-build-2)
- [TIPS](#tips)
  - [CMake](#cmake)
  - [mingw-w64](#mingw-w64)
  - [Rename Shared Lib after build](#rename-shared-lib-after-build)
  - [cmd and bash redirection](#cmd-and-bash-redirection)

<!-- END doctoc generated TOC please keep comment here to allow auto update -->

# Build policy

This repository build with following policy.

- Linux: Use package's standard build. If nothing prefered then use `make` when available.
- Windows: Use package's standard build. If nothing prefered then use `CMake` (w/VC++) when available. Alternative is `mingw-w64` on Docker.
- macOS: Use package's standard build. If nothing prefered then use `make` when available.
- iOS: Use package's standard build. If nothing prefered then use `CMake` when availble.
- Android: Use `CMake` to build.

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
> Local Machine OS many have limit with BuildEnv.
>
> - Linux and Windows can build without BuildEnv `macOS`.
> - macOS can build without BuildEnv `Windows`.

# Summary

## lz4

> CI: GitHub Actions [build-lz4](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-lz4.yaml)

| OS      | Architecture | Build Machine             | Build Env | Builder | Build Script                                                                                                                          |
| ------- | ------------ | ------------------------- | --------- | ------- | ------------------------------------------------------------------------------------------------------------------------------------- |
| Android | armeabi-v7a  | Linux/macOS               | Docker    | make    | [`bash ./builder/lz4/lz4-android-arm.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/lz4/lz4-android-arm.sh)     |
| Android | armeabi-v7a  | Windows                   | Docker    | make    | [`builder\lz4\lz4-android-arm.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/lz4/lz4-android-arm.bat)          |
| Android | arm64-v8a    | Linux/macOS               | Docker    | make    | [`bash ./builder/lz4/lz4-android-arm64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/lz4/lz4-android-arm64.sh) |
| Android | arm64-v8a    | Windows                   | Docker    | make    | [`builder\lz4\lz4-android-arm64.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/lz4/lz4-android-arm64.bat)      |
| Android | x86          | Linux/macOS               | Docker    | make    | [`bash ./builder/lz4/lz4-android-x86.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/lz4/lz4-android-x86.sh)     |
| Android | x86          | Windows                   | Docker    | make    | [`builder\lz4\lz4-android-x86.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/lz4/lz4-android-x86.bat)          |
| Android | x86_64       | Linux/macOS               | Docker    | make    | [`bash ./builder/lz4/lz4-android-x64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/lz4/lz4-android-x64.sh)     |
| Android | x86_64       | Windows                   | Docker    | make    | [`builder\lz4\lz4-android-x64.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/lz4/lz4-android-x64.bat)          |
| iOS     | arm64        | macOS(Intel/AppleSilicon) | macOS     | make    | [`bash ./builder/lz4/lz4-ios-arm64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/lz4/lz4-ios-arm64.sh)         |
| Linux   | arm64        | Linux/macOS               | Docker    | make    | [`bash ./builder/lz4/lz4-linux-arm64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/lz4/lz4-linux-arm64.sh)     |
| Linux   | arm64        | Windows                   | Docker    | make    | [`builder\lz4\lz4-linux-arm64.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/lz4/lz4-linux-arm64.bat)          |
| Linux   | x64          | Linux/macOS               | Docker    | make    | [`bash ./builder/lz4/lz4-linux-x64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/lz4/lz4-linux-x64.sh)         |
| Linux   | x64          | Windows                   | Docker    | make    | [`builder\lz4\lz4-linux-x64.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/lz4/lz4-linux-x64.bat)              |
| macOS   | arm64        | macOS(Intel/AppleSilicon) | macOS     | make    | [`bash ./builder/lz4/lz4-darwin-arm64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/lz4/lz4-darwin-arm64.sh)   |
| macOS   | x64          | macOS(Intel/AppleSilicon) | macOS     | make    | [`bash ./builder/lz4/lz4-darwin-x64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/lz4/lz4-darwin-x64.sh)       |
| Windows | arm64        | Windows                   | Windows   | CMake   | [builder/\z4\lz4-windows-arm64.bat](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/lz4/lz4-windows-arm64.bat)        |
| Windows | x64          | Windows                   | Windows   | CMake   | [builder\lz4\lz4-windows-x64.bat](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/lz4/lz4-windows-x64.bat)            |
| Windows | x86          | Windows                   | Windows   | CMake   | [builder\lz4\lz4-windows-x86.bat](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/lz4/lz4-windows-x86.bat)            |

## mbedtls

> CI: GitHub Actions [build-mbedtls](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-mbedtls.yaml)

| OS      | Architecture | Build Machine             | Build Env | Builder | Build Script                                                                                                                                                          |
| ------- | ------------ | ------------------------- | --------- | ------- | --------------------------------------------------------------------------------------------------------------------------------------------------------------------- |
| Android | armeabi-v7a  | Linux/macOS               | Docker    | CMake   | [`bash ./builder/mbedtls/mbedtls-android-arm.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-android-arm.sh)                     |
| Android | armeabi-v7a  | Windows                   | Docker    | CMake   | [`builder\mbedtls\\mbedtls-android-arm.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-android-arm.bat)                         |
| Android | arm64-v8a    | Linux/macOS               | Docker    | CMake   | [`bash ./builder/mbedtls/mbedtls-android-arm64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-android-arm64.sh)                 |
| Android | arm64-v8a    | Windows                   | Docker    | CMake   | [`builder\mbedtls\mbedtls-android-arm64.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-android-arm64.bat)                      |
| Android | x86          | Linux/macOS               | Docker    | CMake   | [`bash ./builder/mbedtls/mbedtls-android-x86.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-android-x86.sh)                     |
| Android | x86          | Windows                   | Docker    | CMake   | [`builder\mbedtls\mbedtls-android-x86.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-android-x86.bat)                          |
| Android | x86_64       | Linux/macOS               | Docker    | CMake   | [`bash ./builder/mbedtls/mbedtls-android-x64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-android-x64.sh)                     |
| Android | x86_64       | Windows                   | Docker    | CMake   | [`builder\mbedtls\mbedtls-android-x64.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-android-x64.bat)                          |
| iOS     | arm64        | macOS(Intel/AppleSilicon) | macOS     | CMake   | [`bash ./builder/mbedtls/mbedtls-ios-arm64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-ios-arm64.sh)                         |
| Linux   | arm64        | Linux/macOS               | Docker    | CMake   | [`bash ./builder/mbedtls/mbedtls-linux-arm64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-linux-arm64.sh)                     |
| Linux   | arm64        | Windows                   | Docker    | CMake   | [`builder\mbedtls\mbedtls-linux-arm64.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-linux-arm64.bat)                          |
| Linux   | arm64        | Linux/macOS               | Docker    | make    | [`bash ./builder/mbedtls-linux-arm64-make.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-linux-arm64-alpine-make.sh)            |
| Linux   | arm64        | Windows                   | Docker    | make    | [`builder\mbedtls\mbedtls-linux-arm64-make.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-linux-arm64-alpine-make.bat)         |
| Linux   | x64          | Linux/macOS               | Docker    | CMake   | [`bash ./builder/mbedtls/mbedtls-linux-x64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-linux-x64.sh)                         |
| Linux   | x64          | Windows                   | Docker    | CMake   | [`builder\mbedtls\mbedtls/mbedtls-linux-x64.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-linux-x64.bat)                      |
| Linux   | x64          | Linux/macOS               | Docker    | make    | [`bash ./builder/mbedtls/mbedtls-linux-x64-alpine-make.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-linux-x64-alpine-make.sh) |
| Linux   | x64          | Windows                   | Docker    | make    | [`builder\mbedtls\mbedtls-linux-x64-alpine-make.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-linux-x64-alpine-make.bat)      |
| macOS   | arm64        | macOS(Intel/AppleSilicon) | macOS     | CMake   | [`bash ./builder/mbedtls/mbedtls-darwin-arm64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-darwin-arm64.sh)                   |
| macOS   | x64          | macOS(Intel/AppleSilicon) | macOS     | CMake   | [`bash ./builder/mbedtls/mbedtls-darwin-x64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-darwin-x64.sh)                       |
| Windows | arm64        | Windows                   | Windows   | CMake   | [`builder\mbedtls\mbedtls-windows-arm64.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-windows-arm64.bat)                      |
| Windows | x64          | Windows                   | Windows   | CMake   | [`builder\mbedtls\mbedtls-windows-x64.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-windows-x64.bat)                          |
| Windows | x86          | Windows                   | Windows   | CMake   | [`builder\mbedtls\mbedtls-windows-x86.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/mbedtls/mbedtls-windows-x86.bat)                          |

## zstd

> CI: GitHub Actions [build-zstd](https://github.com/guitarrapc/nativebuild-lab/actions/workflows/build-zstd.yaml)

| OS      | Architecture | Build Machine             | Build Env | Builder | Build Script                                                                                                                                        |
| ------- | ------------ | ------------------------- | --------- | ------- | --------------------------------------------------------------------------------------------------------------------------------------------------- |
| Android | armeabi-v7a  | Linux/macOS               | Docker    | CMake   | [`bash ./builder/zstd/zstd-android-arm.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-android-arm.sh)               |
| Android | armeabi-v7a  | Windows                   | Docker    | CMake   | [`builder\zstd\zstd-android-arm.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-android-arm.sh)                     |
| Android | arm64-v8a    | Linux/macOS               | Docker    | CMake   | [`bash ./builder/zstd/zstd-android-arm64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-android-arm64.sh)           |
| Android | arm64-v8a    | Windows                   | Docker    | CMake   | [`builder\zstd\zstd-android-arm64.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-android-arm64.sh)                 |
| Android | x86          | Linux/macOS               | Docker    | CMake   | [`bash ./builder/zstd/zstd-android-x86.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-android-x86.sh)               |
| Android | x86          | Windows                   | Docker    | CMake   | [`builder\zstd\zstd-android-x86.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-android-x86.sh)                     |
| Android | x86_64       | Linux/macOS               | Docker    | CMake   | [`bash ./builder/zstd/zstd-android-x64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-android-x64.sh)               |
| Android | x86_64       | Windows                   | Docker    | CMake   | [`builder\zstd\zstd-android-x64.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-android-x64.sh)                     |
| iOS     | arm64        | macOS(Intel/AppleSilicon) | macOS     | CMake   | [`bash ./builder/zstd/zstd-ios-arm64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-ios-arm64.sh)                   |
| Linux   | arm64        | Linux/macOS               | Docker    | CMake   | [`bash ./builder/zstd/zstd-linux-arm64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-linux-arm64.sh)               |
| Linux   | arm64        | Windows                   | Docker    | CMake   | [`builder\zstd\zstd-linux-arm64.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-linux-arm64.bat)                    |
| Linux   | arm64        | Linux/macOS               | Docker    | make    | [`bash ./builder/zstd/zstd-linux-arm64-make.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-linux-arm64-make.sh)     |
| Linux   | arm64        | Windows                   | Docker    | make    | [`builder\zstd\zstd-linux-arm64-make.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-linux-arm64-make.bat)          |
| Linux   | x64          | Linux/macOS               | Docker    | CMake   | [`bash ./builder/zstd/zstd-linux-x64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-linux-x64.sh)                   |
| Linux   | x64          | Windows                   | Docker    | CMake   | [`builder\zstd\zstd-linux-x64.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-linux-x64.bat)                        |
| Linux   | x64          | Linux/macOS               | Docker    | make    | [`bash ./builder/zstd/zstd-linux-x64-make.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-linux-x64-make.sh)         |
| Linux   | x64          | Windows                   | Docker    | make    | [`builder\zstd\zstd-linux-x64-make.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-linux-x64-make.bat)              |
| macOS   | arm64        | macOS(Intel/AppleSilicon) | macOS     | CMake   | [`bash ./builder/zstd/zstd-darwin-arm64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-darwin-arm64.sh)             |
| macOS   | arm64        | macOS(Intel/AppleSilicon) | macOS     | make    | [`bash ./builder/zstd/zstd-darwin-arm64-make.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-darwin-arm64-make.sh)   |
| macOS   | x64          | macOS(Intel/AppleSilicon) | macOS     | CMake   | [`bash ./builder/zstd/zstd-darwin-x64.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-darwin-x64.sh)                 |
| macOS   | x64          | macOS(Intel/AppleSilicon) | macOS     | make    | [`bash ./builder/zstd/zstd-darwin-x64-make.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-darwin-x64-make.sh)       |
| Windows | arm64        | Windows                   | Windows   | CMake   | [`builder\zstd\zstd-windows-arm64.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-windows-arm64.bat)                |
| Windows | arm64        | Linux/macOS               | Docker    | make    | [`bash ./builder/zstd/zstd-windows-arm64-make.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-windows-arm64-make.sh) |
| Windows | arm64        | Windows                   | Docker    | make    | [`builder\zstd\zstd-windows-arm64-make.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-windows-arm64-make.bat)      |
| Windows | x64          | Windows                   | Windows   | CMake   | [`builder\zstd\zstd-windows-x64.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-windows-x64.bat)                    |
| Windows | x64          | Linux/macOS               | Docker    | make    | [`bash ./builder/zstd/zstd-windows-x64-make.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-windows-x64-make.sh)     |
| Windows | x64          | Windows                   | Docker    | make    | [`builder\zstd\zstd-windows-x64-make.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-windows-x64-make.bat)          |
| Windows | x86          | Windows                   | Windows   | CMake   | [`builder\zstd\zstd-windows-x86.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-windows-x86.bat)                    |
| Windows | x86          | Linux/macOS               | Docker    | make    | [`bash ./builder/zstd/zstd-windows-x86-make.sh`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-windows-x86-make.sh)     |
| Windows | x86          | Windows                   | Docker    | make    | [`builder\zstd\zstd-windows-x86-make.bat`](https://github.com/guitarrapc/nativebuild-lab/blob/main/builder/zstd/zstd-windows-x86-make.bat)          |

# lz4

Building [lz4](https://github.com/lz4/lz4) for following environment.

## (Notice) Windows Build

Windows binaries are named `lz4*`. Please install Visual Studio 2022 `C++ Desktop Experience` package and `MSVC v143 - VS 2022 C++ ARM64 build tools` for cmake.

# mbedtls

Building [mbedtls](https://github.com/Mbed-TLS/mbedtls#make) for following environment.

<hr/>

## (Notice) Windows Build

Windows binaries are named `mbed*`. Please install Visual Studio 2022 `C++ Desktop Experience` package and `MSVC v143 - VS 2022 C++ ARM64 build tools` for cmake.

## (Notice) Windows Patch

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

## (Notice) Wrapper Patch

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

## (Notice) Filename Prefix Patch

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

# zstd

Building [zstd](https://github.com/facebook/zstd) for following environment.

## (Notice) Windows Build

Windows binaries are named `zstd*` for CMake and `libzstd` for make. Please install Visual Studio 2022 `C++ Desktop Experience` package and `MSVC v143 - VS 2022 C++ ARM64 build tools` for cmake.

# TIPS

## CMake

**Basics**

- CMake Windows: [Visual Studio 17 2022 — CMake 3\.24\.1 Documentation](https://cmake.org/cmake/help/latest/generator/Visual%20Studio%2017%202022.html)

**CheatSheet**

- [cmake チートシート \- Qoosky](https://www.qoosky.io/techs/814fda555d)

**Native Platform**

- CMake Android: [CMake  \|  Android NDK  \|  Android Developers](https://developer.android.com/ndk/guides/cmake?hl=ja)
- CMake iOS: [cmake\-toolchains\(7\) — CMake 3\.24\.1 Documentation](https://cmake.org/cmake/help/latest/manual/cmake-toolchains.7.html#cross-compiling-for-ios-tvos-or-watchos)

Other way to build.

- Android CMake build: [leleliu008/ndk\-pkg](https://github.com/leleliu008/ndk-pkg)
- iOS Cmake build: [leleliu008/xcpkg](https://github.com/leleliu008/xcpkg)

## mingw-w64

- Linux mingw-w64 amd64 build: [Downloads \- MinGW\-w64](https://www.mingw-w64.org/downloads/)
- Linux mingw-w64 arm64 build: [mstorsjo/llvm\-mingw](https://github.com/mstorsjo/llvm-mingw)

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
